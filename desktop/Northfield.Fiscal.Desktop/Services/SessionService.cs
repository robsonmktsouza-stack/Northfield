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

    public IReadOnlyList<RecentSessionInfo> GetRecentSessions(string? additionalPath = null, int limit = 10)
    {
        Directory.CreateDirectory(AppDataDirectory);

        var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var path in Directory.EnumerateFiles(AppDataDirectory, "*.northfield.json", SearchOption.TopDirectoryOnly))
            paths.Add(Path.GetFullPath(path));

        if (!string.IsNullOrWhiteSpace(additionalPath) && File.Exists(additionalPath))
            paths.Add(Path.GetFullPath(additionalPath));

        var result = new List<RecentSessionInfo>();

        foreach (var path in paths)
        {
            try
            {
                var data = Load(path);
                var documents = data.Documents ?? [];
                var classified = documents.Count(d => !string.IsNullOrWhiteSpace(d.Segregation));

                var status = documents.Count == 0
                    ? "Sem documentos"
                    : classified == documents.Count
                        ? "Conferida"
                        : "Em andamento";

                result.Add(new RecentSessionInfo
                {
                    Competence = data.Company.Competence,
                    CompanyName = string.IsNullOrWhiteSpace(data.Company.CorporateName)
                        ? "Empresa não informada"
                        : data.Company.CorporateName,
                    Cnpj = data.Company.Cnpj,
                    Revenue = documents.Sum(d => d.ServiceValue),
                    Status = status,
                    SavedAt = data.SavedAt,
                    FilePath = path
                });
            }
            catch
            {
                // Arquivos inválidos não aparecem na lista de recentes.
            }
        }

        return result
            .OrderByDescending(x => x.SavedAt)
            .Take(Math.Max(1, limit))
            .ToList();
    }
}
