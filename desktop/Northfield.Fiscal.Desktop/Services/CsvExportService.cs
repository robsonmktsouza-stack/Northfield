using System.Text;
using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Services;

public sealed class CsvExportService
{
    public void Export(IEnumerable<FiscalDocument> documents, string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Documento;Emissao;Tomador;CNPJ_CPF;Municipio;CodigoServico;ValorServico;BaseISS;ISSRetido;Anexo;Segregacao;Situacao");
        foreach (var d in documents)
        {
            sb.AppendLine(string.Join(';', new[]
            {
                Escape(d.DisplayDocument),
                Escape(d.IssueDate?.ToString("dd/MM/yyyy") ?? string.Empty),
                Escape(d.RecipientName),
                Escape(d.RecipientTaxId),
                Escape(d.Municipality),
                Escape(d.ServiceCode),
                d.ServiceValue.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                d.IssBase.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                d.IssWithheldValue.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                Escape(d.Annex),
                Escape(d.Segregation),
                Escape(d.Status)
            }));
        }
        File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
    }

    private static string Escape(string value)
    {
        if (value.Contains(';') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }
}
