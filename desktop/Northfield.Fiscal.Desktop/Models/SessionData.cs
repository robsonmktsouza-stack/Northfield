namespace Northfield.Fiscal.Desktop.Models;

public sealed class SessionData
{
    public CompanyContext Company { get; set; } = new();
    public List<FiscalDocument> Documents { get; set; } = [];
    public DateTime SavedAt { get; set; } = DateTime.Now;
}
