using System.IO.Compression;
using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Services;

public sealed class ImportService
{
    private readonly NfseXmlReader _reader = new();

    public IReadOnlyList<FiscalDocument> Import(IEnumerable<string> paths, out List<string> errors)
    {
        var documents = new List<FiscalDocument>();
        errors = [];

        foreach (var path in paths)
        {
            try
            {
                if (path.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                    ImportZip(path, documents, errors);
                else if (path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    documents.Add(_reader.Read(File.ReadAllText(path), path));
                else
                    errors.Add($"Arquivo ignorado: {Path.GetFileName(path)} (use XML ou ZIP).");
            }
            catch (Exception ex)
            {
                errors.Add($"{Path.GetFileName(path)}: {ex.Message}");
            }
        }

        return documents;
    }

    private void ImportZip(string path, List<FiscalDocument> documents, List<string> errors)
    {
        using var zip = ZipFile.OpenRead(path);
        foreach (var entry in zip.Entries.Where(e => e.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)))
        {
            try
            {
                using var stream = entry.Open();
                using var reader = new StreamReader(stream);
                var xml = reader.ReadToEnd();
                documents.Add(_reader.Read(xml, $"{path}|{entry.FullName}"));
            }
            catch (Exception ex)
            {
                errors.Add($"{Path.GetFileName(path)} / {entry.FullName}: {ex.Message}");
            }
        }
    }
}
