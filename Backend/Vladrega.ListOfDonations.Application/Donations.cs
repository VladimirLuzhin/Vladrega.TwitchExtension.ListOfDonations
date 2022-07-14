namespace Vladrega.ListOfDonations.Application;

/// <summary>
/// Донаты пользователя
/// </summary>
public class Donations
{
    /// <summary>
    /// От кого был донат
    /// </summary>
    public string From { get; }
    
    /// <summary>
    /// Общая сумма донатов
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// .ctor
    /// </summary>
    public Donations(string from, decimal amount)
    {
        From = from;
        Amount = amount;
    }
}