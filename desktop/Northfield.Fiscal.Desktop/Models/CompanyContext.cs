namespace Northfield.Fiscal.Desktop.Models;

public sealed class CompanyContext
{
    public DateTime Competence { get; set; } = new(DateTime.Today.Year, DateTime.Today.Month, 1);
    public string Cnpj { get; set; } = string.Empty;
    public string CorporateName { get; set; } = string.Empty;
    public string TaxRegime { get; set; } = "Simples Nacional";
    public string Municipality { get; set; } = string.Empty;
    public decimal Rbt12 { get; set; }
    public decimal Payroll12m { get; set; }
    public bool ConsiderIssWithheld { get; set; } = true;
    public bool GroupBySegregation { get; set; }

    public decimal FactorRPercent => Rbt12 <= 0m ? 0m : Math.Round((Payroll12m / Rbt12) * 100m, 2);
}
