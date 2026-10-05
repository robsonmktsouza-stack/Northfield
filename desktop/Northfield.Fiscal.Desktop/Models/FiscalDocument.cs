namespace Northfield.Fiscal.Desktop.Models;

public sealed class FiscalDocument
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string DocumentType { get; set; } = "NFS-e";
    public string Number { get; set; } = string.Empty;
    public DateTime? IssueDate { get; set; }
    public string RecipientName { get; set; } = string.Empty;
    public string RecipientTaxId { get; set; } = string.Empty;
    public string Municipality { get; set; } = string.Empty;
    public string MunicipalityCode { get; set; } = string.Empty;
    public string ServiceCode { get; set; } = string.Empty;
    public string MunicipalTaxCode { get; set; } = string.Empty;
    public string Nbs { get; set; } = string.Empty;
    public string ServiceDescription { get; set; } = string.Empty;
    public decimal ServiceValue { get; set; }
    public decimal IssBase { get; set; }
    public decimal IssWithheldValue { get; set; }
    public bool IssWithheld { get; set; }
    public string Annex { get; set; } = string.Empty;
    public string Segregation { get; set; } = string.Empty;
    public string Status { get; set; } = "Importado";
    public string SourceFile { get; set; } = string.Empty;
    public bool ManualClassification { get; set; }
    public List<string> Memory { get; set; } = [];

    public string DisplayDocument => string.IsNullOrWhiteSpace(Number) ? DocumentType : $"{DocumentType} {Number}";
}
