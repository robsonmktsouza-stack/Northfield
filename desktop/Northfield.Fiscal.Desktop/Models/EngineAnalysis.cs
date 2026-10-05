namespace Northfield.Fiscal.Desktop.Models;

public sealed class EngineAnalysis
{
    public bool EngineAvailable { get; init; }
    public string EngineName { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public List<DocumentEngineDecision> Decisions { get; init; } = [];
}

public sealed class DocumentEngineDecision
{
    public Guid DocumentId { get; init; }
    public string Annex { get; init; } = string.Empty;
    public string Segregation { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public List<string> Memory { get; init; } = [];
}
