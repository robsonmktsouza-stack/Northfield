using System.Globalization;
using System.Xml.Linq;
using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Services;

public sealed class NfseXmlReader
{
    public FiscalDocument Read(string xml, string sourceFile)
    {
        var root = XDocument.Parse(xml, LoadOptions.PreserveWhitespace).Root
            ?? throw new InvalidDataException("XML sem elemento raiz.");

        var doc = new FiscalDocument
        {
            SourceFile = sourceFile,
            DocumentType = DetectDocumentType(root),
            Number = FirstValue(root, "Numero", "nNFSe", "NumeroNfse"),
            IssueDate = ParseDate(FirstValue(root, "DataEmissao", "dhEmi", "dhProc")),
            ServiceCode = FirstValue(root, "ItemListaServico", "cTribNac", "CodigoServico"),
            MunicipalTaxCode = FirstValue(root, "CodigoTributacaoMunicipio", "cTribMun"),
            Nbs = FirstValue(root, "CodigoNbs", "cNBS"),
            ServiceDescription = FirstValue(root, "Discriminacao", "xDescServ", "DescricaoServico")
        };

        var serviceNode = Descendant(root, "Servico", "serv", "infNFSe") ?? root;
        var valuesNode = Descendant(serviceNode, "Valores", "valores", "vServPrest") ?? serviceNode;

        doc.ServiceValue = FirstDecimal(valuesNode, "ValorServicos", "vServ", "vServPrest", "ValorLiquidoNfse");
        if (doc.ServiceValue == 0m)
            doc.ServiceValue = FirstDecimal(root, "ValorServicos", "vServ", "ValorLiquidoNfse", "BaseCalculo");

        doc.IssBase = FirstDecimal(valuesNode, "BaseCalculo", "vBC", "vBCISSQN");
        if (doc.IssBase == 0m)
            doc.IssBase = FirstDecimal(root, "BaseCalculo", "vBCISSQN");
        if (doc.IssBase == 0m)
            doc.IssBase = doc.ServiceValue;

        doc.IssWithheldValue = FirstDecimal(valuesNode, "ValorIss", "vISSQN", "vISSRet");
        var issFlag = FirstValue(serviceNode, "IssRetido", "indISSRet", "ISSRetido");
        doc.IssWithheld = IsWithheld(issFlag, doc.IssWithheldValue);

        var recipient = Descendant(root, "TomadorServico", "toma", "Tomador", "tomador");
        if (recipient is not null)
        {
            doc.RecipientName = FirstValue(recipient, "RazaoSocial", "xNome", "NomeRazaoSocial");
            doc.RecipientTaxId = FirstValue(recipient, "Cnpj", "CNPJ", "Cpf", "CPF", "NIF");
            var address = Descendant(recipient, "Endereco", "end", "EnderecoTomador") ?? recipient;
            doc.MunicipalityCode = FirstValue(address, "CodigoMunicipio", "cMun", "cLocEmi");
            var cityName = FirstValue(address, "Municipio", "xMun", "Cidade");
            var uf = FirstValue(address, "Uf", "UF");
            doc.Municipality = BuildMunicipality(cityName, uf, doc.MunicipalityCode);
        }

        if (string.IsNullOrWhiteSpace(doc.MunicipalityCode))
            doc.MunicipalityCode = FirstValue(serviceNode, "CodigoMunicipio", "MunicipioIncidencia", "cLocPrestacao");

        if (string.IsNullOrWhiteSpace(doc.Municipality))
            doc.Municipality = doc.MunicipalityCode;

        if (string.IsNullOrWhiteSpace(doc.Number))
            doc.Number = Path.GetFileNameWithoutExtension(sourceFile);

        doc.Memory.Add($"Documento importado de {Path.GetFileName(sourceFile)}.");
        doc.Memory.Add("Dados fiscais extraídos do XML; nenhuma regra de segregação foi aplicada pelo programa hospedeiro.");
        return doc;
    }

    private static string DetectDocumentType(XElement root)
    {
        var names = root.DescendantsAndSelf().Select(x => x.Name.LocalName).ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (names.Contains("Nfse") || names.Contains("NFSe") || names.Contains("infNFSe") || names.Contains("CompNfse"))
            return "NFS-e";
        if (names.Contains("NFe") || names.Contains("infNFe"))
            return "NF-e";
        return "Documento";
    }

    private static XElement? Descendant(XElement root, params string[] names) =>
        root.DescendantsAndSelf().FirstOrDefault(x => names.Contains(x.Name.LocalName, StringComparer.OrdinalIgnoreCase));

    private static string FirstValue(XElement root, params string[] names)
    {
        foreach (var name in names)
        {
            var node = root.DescendantsAndSelf().FirstOrDefault(x => string.Equals(x.Name.LocalName, name, StringComparison.OrdinalIgnoreCase));
            if (node is not null && !string.IsNullOrWhiteSpace(node.Value))
                return node.Value.Trim();
        }
        return string.Empty;
    }

    private static decimal FirstDecimal(XElement root, params string[] names)
    {
        var value = FirstValue(root, names);
        if (string.IsNullOrWhiteSpace(value)) return 0m;

        if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var invariant))
            return invariant;
        if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("pt-BR"), out var br))
            return br;
        return 0m;
    }

    private static DateTime? ParseDate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out var dto))
            return dto.LocalDateTime;
        if (DateTime.TryParse(value, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.AllowWhiteSpaces, out var dt))
            return dt;
        return null;
    }

    private static bool IsWithheld(string value, decimal withheldValue)
    {
        if (withheldValue > 0m) return true;
        if (string.IsNullOrWhiteSpace(value)) return false;
        return value.Trim().ToUpperInvariant() switch
        {
            "1" or "S" or "SIM" or "TRUE" => true,
            "2" or "N" or "NAO" or "NÃO" or "FALSE" => false,
            _ => false
        };
    }

    private static string BuildMunicipality(string city, string uf, string code)
    {
        if (!string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(uf)) return $"{city} - {uf}";
        if (!string.IsNullOrWhiteSpace(city)) return city;
        return code;
    }
}
