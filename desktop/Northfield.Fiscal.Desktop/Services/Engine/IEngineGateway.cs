using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Services.Engine;

public interface IEngineGateway
{
    string Name { get; }
    bool IsAvailable { get; }
    Task<EngineAnalysis> AnalyzeAsync(CompanyContext company, IReadOnlyList<FiscalDocument> documents, CancellationToken cancellationToken = default);
}
