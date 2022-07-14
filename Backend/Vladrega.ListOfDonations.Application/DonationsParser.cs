using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Vladrega.ListOfDonations.Application;

/// <summary>
/// Парсер текста со списов донатов
/// </summary>
public class DonationsParser
{
    private readonly Regex _parseRegex = new(@"^(?<name>.*?)\s*(-|\s)\s*(?<amount>[\d\s,]+).*$", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
    
    /// <summary>
    /// Распарсить список донатов по переданному тексту
    /// </summary>
    /// <param name="commandDonations">Текстовый список донатеров</param>
    /// <returns>Список донатеров, который удалось вычленить из текста</returns>
    public IEnumerable<Donations> Parse(string commandDonations)
    {
        var donations = new List<Donations>();
        
        var matches = _parseRegex.Matches(commandDonations);

        foreach (var matchObject in matches)
        {
            if (matchObject is not Match match)
                continue;

            var donatorName = match.Groups["name"].ToString();
            var donatorStringAmount = match.Groups["amount"].ToString().Replace(" ", string.Empty);
            
            if (string.IsNullOrWhiteSpace(donatorName))
                continue;
            
            if (!decimal.TryParse(donatorStringAmount, out var amount))
                continue;
            
            donations.Add(new Donations(donatorName, amount));
        }

        return donations;
    }
}