namespace Northfield.Fiscal.Desktop.Models;

public sealed class CompanyListItem
{
    public CompanyContext Company { get; init; } = new();
    public DateTime LastCompetence { get; init; }
    public DateTime LastUpdatedAt { get; init; }
}
