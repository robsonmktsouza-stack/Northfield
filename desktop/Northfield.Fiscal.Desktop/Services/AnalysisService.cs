using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Services;

public static class AnalysisService
{
    public static IReadOnlyList<SegregationRow> BuildSegregation(IEnumerable<FiscalDocument> documents)
    {
        return documents
            .Where(d => d.ServiceValue != 0m)
            .GroupBy(d => new
            {
                Annex = string.IsNullOrWhiteSpace(d.Annex) ? "Pendente" : d.Annex,
                Segregation = string.IsNullOrWhiteSpace(d.Segregation) ? "Sem classificação" : d.Segregation
            })
            .Select(g => new SegregationRow
            {
                Annex = g.Key.Annex,
                Segregation = g.Key.Segregation,
                Documents = g.Count(),
                Amount = g.Sum(x => x.ServiceValue)
            })
            .OrderBy(x => x.Annex)
            .ThenBy(x => x.Segregation)
            .ToList();
    }

    public static decimal TotalRevenue(IEnumerable<FiscalDocument> documents) => documents.Sum(d => d.ServiceValue);
    public static decimal TotalIssWithheld(IEnumerable<FiscalDocument> documents) => documents.Where(d => d.IssWithheld).Sum(d => d.ServiceValue);
    public static decimal TotalPending(IEnumerable<FiscalDocument> documents) => documents.Where(d => string.IsNullOrWhiteSpace(d.Segregation)).Sum(d => d.ServiceValue);
}
