using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Services.Engine;

public sealed class ManualEngineGateway : IEngineGateway
{
    public string Name => "Modo manual (motor externo não conectado)";
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
            Status = d.ManualClassification ? "Classificação manual" : "Aguardando motor",
            Memory = d.ManualClassification
                ? ["Classificação informada manualmente no programa hospedeiro.", "Nenhuma regra fiscal automática foi executada."]
                : ["Documento preparado para análise.", "A biblioteca do Simples ainda não está conectada ao programa hospedeiro."]
        }).ToList();

        return Task.FromResult(new EngineAnalysis
        {
            EngineAvailable = false,
            EngineName = Name,
            Message = "O programa hospedeiro está pronto. A decisão tributária automática ficará na biblioteca do Simples, sem regras fiscais embutidas na interface.",
            Decisions = decisions
        });
    }
}
