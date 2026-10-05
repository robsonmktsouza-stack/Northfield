namespace Northfield.Fiscal.Desktop.Models;

public sealed class SegregationRow
{
    public string Annex { get; init; } = string.Empty;
    public string Segregation { get; init; } = string.Empty;
    public int Documents { get; init; }
    public decimal Amount { get; init; }
}
