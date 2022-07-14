using System.Reflection;

namespace Vladrega.ListOfDonations.Database;

/// <summary>
/// Провайдер для получения текста скриптов по названию скрипта
/// </summary>
public class ScriptsProvider
{
    private readonly Dictionary<string, string> _scripts = new();
 
    /// <summary>
    /// .ctor
    /// </summary>
    public ScriptsProvider()
    {
        var assembly = Assembly.GetAssembly(typeof(ScriptsProvider));
        var resourceNames = assembly.GetManifestResourceNames();
        foreach (var resourceName in resourceNames)
        {
            if (!resourceName.EndsWith(".sql"))
                continue;

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);

            var scriptName = resourceName.Replace(".sql", string.Empty)
                .Split(".")
                .LastOrDefault(); 
            
            _scripts.Add(scriptName, reader.ReadToEnd()); 
        }
    }
    
    /// <summary>
    /// Получение текста скрипта по его названию
    /// </summary>
    /// <param name="scriptName">Название скрипта</param>
    /// <exception cref="FileNotFoundException">Исключение будет брошено, если по указанному названию не будет обнаружен скрипт</exception>
    public string GetScriptByName(string scriptName)
    {
        return _scripts.TryGetValue(scriptName, out var scriptText)
            ? scriptText
            : throw new FileNotFoundException($"Скрипт с названием {scriptText} не найден");
    }
}