using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Services.Engine;

public sealed class ManualEngineGateway : IEngineGateway
{
    public string Name => "Classificação manual";
    public bool IsAvailable => false;

    public Task<EngineAnalysis> AnalyzeAsync(
        CompanyContext company,
        IReadOnlyList<FiscalDocument> documents,
        CancellationToken cancellationToken = default)
    {
        var decisions = documents.Select(d => new DocumentEngineDecision
        {
            DocumentId = d.Id,
            Annex = d.Annex,
            Segregation = d.Segregation,
            Status = d.ManualClassification ? "Revisado" : "Aguardando classificação",
            Memory = d.ManualClassification
                ? ["Classificação revisada pelo usuário."]
                : ["Documento aguardando classificação."]
        }).ToList();

        return Task.FromResult(new EngineAnalysis
        {
            EngineAvailable = false,
            EngineName = Name,
            Message = "A análise automática ainda não está disponível. Revise a classificação dos documentos na aba Documentos.",
            Decisions = decisions
        });
    }
}
