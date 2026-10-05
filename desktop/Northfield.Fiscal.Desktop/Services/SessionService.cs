using System.Text.Json;
using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Services;

public sealed class SessionService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public string AppDataDirectory { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "NorthfieldFiscal");

    public string LastSessionPath => Path.Combine(AppDataDirectory, "last-session.northfield.json");

    public void Save(SessionData data, string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? AppDataDirectory);
        data.SavedAt = DateTime.Now;
        File.WriteAllText(path, JsonSerializer.Serialize(data, JsonOptions));
    }

    public SessionData Load(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<SessionData>(json, JsonOptions)
            ?? throw new InvalidDataException("Sessão inválida.");
    }

    public void SaveLast(SessionData data) => Save(data, LastSessionPath);

    public SessionData? TryLoadLast()
    {
        if (!File.Exists(LastSessionPath)) return null;
        try { return Load(LastSessionPath); }
        catch { return null; }
    }
}
