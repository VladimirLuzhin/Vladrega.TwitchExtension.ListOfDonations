using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Caching.Memory;
using Vladrega.ListOfDonations.Application;
using Vladrega.ListOfDonations.Application.Handlers;
using Vladrega.ListOfDonations.Config;
using Vladrega.ListOfDonations.Database;
using Vladrega.ListOfDonations.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(30500, configure =>
    {
        const string certPemLocation = "./certs/cert.pem"; 
        if (File.Exists(certPemLocation))
        {
            Console.WriteLine($"Файл с сертификатом cert.pem обнаружен");
            const string keyPemLocation = "./certs/privkey.pem";
            configure.UseHttps(X509Certificate2.CreateFromPemFile(certPemLocation, keyPemLocation));
        }
        else
        {
            Console.WriteLine($"Файл с сертификатом cert.pem не обнаружен");
            configure.UseHttps();
        }
    });
});

builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection(nameof(DatabaseConfig)));
builder.Services.Configure<TwitchConfig>(builder.Configuration.GetSection(nameof(TwitchConfig)));

builder.Services.AddSingleton<PgsqlDonationsRepository>();
builder.Services.AddSingleton<IDonationsRepository>(provider =>
    new InMemoryDonationsProxyRepository(
        provider.GetRequiredService<IMemoryCache>(),
        provider.GetRequiredService<PgsqlDonationsRepository>()));

builder.Services.AddHealthChecks();
builder.Services.AddSingleton<DonationsParser>();
builder.Services.AddSingleton<ScriptsProvider>();
builder.Services.AddMemoryCache();
builder.Services.AddCors();

builder.Services.AddSingleton<GetDonationsQueryHandler>();
builder.Services.AddSingleton<SaveDonationsCommandHandler>();
builder.Services.AddSingleton<DeleteDonationsCommandHandler>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<CSPMiddleware>();

app.UseCors(configure =>
{
    configure.AllowAnyOrigin();
    configure.AllowAnyHeader();
    configure.AllowAnyMethod();
});

app.UseHealthChecks("/health");
app.MapControllers();
app.Run();