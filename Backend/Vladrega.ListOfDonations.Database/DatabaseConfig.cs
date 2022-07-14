namespace Vladrega.ListOfDonations.Database;

/// <summary>
/// Конфигурации для базы данных
/// </summary>
public record DatabaseConfig
{
    /// <summary>
    /// Хост СУБД
    /// </summary>
    public string Host { get; init; }
    
    /// <summary>
    /// Используемая БД
    /// </summary>
    public string Database { get; init; }
    
    /// <summary>
    /// Логин для авторизации в БД
    /// </summary>
    public string Login { get; init; }
    
    /// <summary>
    /// Пароль для авторизации в БД
    /// </summary>
    public string Password { get; init; }
}