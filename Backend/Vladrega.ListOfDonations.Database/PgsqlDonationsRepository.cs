using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;
using Vladrega.ListOfDonations.Application;

namespace Vladrega.ListOfDonations.Database;

/// <summary>
/// Реализация с использование PGSQL и драйвера Npgsql
/// </summary>
public class PgsqlDonationsRepository : IDonationsRepository
{
    private readonly DatabaseConfig _config;
    private readonly ScriptsProvider _scriptsProvider;
    
    /// <summary>
    /// .ctor
    /// </summary>
    public PgsqlDonationsRepository(IOptions<DatabaseConfig> config, ScriptsProvider scriptsProvider)
    {
        _scriptsProvider = scriptsProvider;
        _config = config.Value;
    }
    
    /// <inheritdoc />
    public async Task SaveChannelDataAsync(int channelId, Theme selectedTheme, IEnumerable<Donations> donations,
        CancellationToken cancellationToken)
    {
        await using var connection = await GetConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        foreach (var donation in donations)
        {
            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = _scriptsProvider.GetScriptByName("UPSERT_ChannelDonations");
            command.Parameters.Add(new NpgsqlParameter<int>("ChannelId", channelId));
            command.Parameters.Add(new NpgsqlParameter<string>("DonatorName", donation.From));
            command.Parameters.Add(new NpgsqlParameter<decimal>("Amount", donation.Amount));
            
            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var command2 = connection.CreateCommand();
        command2.Transaction = transaction;
        command2.Parameters.Add(new NpgsqlParameter<int>("ChannelId", channelId));
        command2.Parameters.Add(new NpgsqlParameter<int>("Theme", (int) selectedTheme));
        command2.CommandText = _scriptsProvider.GetScriptByName("UPSERT_ChannelSettings");

        await command2.ExecuteNonQueryAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ChannelData> GetChannelDataAsync(int channelId, CancellationToken cancellationToken)
    {
        await using var connection = await GetConnectionAsync(cancellationToken);
        await using var donationsCommand = connection.CreateCommand();

        donationsCommand.CommandText = _scriptsProvider.GetScriptByName("GET_Donations");
        donationsCommand.Parameters.Add(new NpgsqlParameter<int>("ChannelId", channelId));

        var donnationsReader = await donationsCommand.ExecuteReaderAsync(cancellationToken);

        var existDonations = new List<Donations>(); 
        while (await donnationsReader.ReadAsync(cancellationToken))
        {
            existDonations.Add(FromReader(donnationsReader));
        }
        
        await using var connection2 = await GetConnectionAsync(cancellationToken);
        await using var settingsCommand = connection2.CreateCommand();

        settingsCommand.CommandText = _scriptsProvider.GetScriptByName("GET_ChannelSettings");
        settingsCommand.Parameters.Add(new NpgsqlParameter<int>("ChannelId", channelId));

        var theme = (Theme)(int) await settingsCommand.ExecuteScalarAsync(cancellationToken);
        
        return new ChannelData(existDonations, theme);
    }

    /// <inheritdoc />
    public async Task DeleteAllRowsAsync(int channelId, CancellationToken cancellationToken)
    {
        await using var connection = await GetConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();

        command.CommandText = _scriptsProvider.GetScriptByName("DELETE_Donations");
        command.Parameters.Add(new NpgsqlParameter<int>("Channelid", channelId));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private Donations FromReader(IDataReader reader)
    {
        return new Donations(Convert.ToString(reader["DonatorName"]), Convert.ToDecimal(reader["Amount"]));
    }

    private async Task<NpgsqlConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        var connectionBuilder = new NpgsqlConnectionStringBuilder
        {
            Database = _config.Database,
            Host = _config.Host,
            Username = _config.Login,
            Password = _config.Password,
            Pooling = true,
            Multiplexing = true,
            CommandTimeout = 30
        };

        var connection = new NpgsqlConnection(connectionBuilder.ToString());
        await connection.OpenAsync(cancellationToken);

        return connection;
    }
}