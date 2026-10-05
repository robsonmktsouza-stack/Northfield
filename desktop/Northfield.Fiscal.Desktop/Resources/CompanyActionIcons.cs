namespace Northfield.Fiscal.Desktop.Resources;

internal static class CompanyActionIcons
{
    public static Image Novo() => Load("Novo.png");
    public static Image Editar() => Load("Editar.png");
    public static Image Excluir() => Load("Excluir.png");
    public static Image Selecionar() => Load("Selecionar.png");
    public static Image Duplicar() => Load("Duplicar.png");
    public static Image AtivarInativar() => Load("AtivarInativar.png");
    public static Image Historico() => Load("Historico.png");
    public static Image Certificado() => Load("Certificado.png");
    public static Image Importar() => Load("Importar.png");
    public static Image Exportar() => Load("Exportar.png");

    private static Image Load(string fileName)
    {
        var assembly = typeof(CompanyActionIcons).Assembly;
        var suffix = $".Resources.CompanyActions.{fileName}";

        var resourceName = assembly
            .GetManifestResourceNames()
            .FirstOrDefault(name => name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException($"Recurso de ícone não encontrado: {fileName}");

        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Não foi possível abrir o recurso de ícone: {fileName}");

        using var image = Image.FromStream(stream);
        return new Bitmap(image);
    }
}
