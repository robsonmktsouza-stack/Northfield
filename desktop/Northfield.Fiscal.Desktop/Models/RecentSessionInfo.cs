namespace Northfield.Fiscal.Desktop.Models;

public sealed class RecentSessionInfo
{
    public DateTime Competence { get; init; }
    public string CompanyName { get; init; } = string.Empty;
    public string Cnpj { get; init; } = string.Empty;
    public decimal Revenue { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime SavedAt { get; init; }
    public string FilePath { get; init; } = string.Empty;
}
