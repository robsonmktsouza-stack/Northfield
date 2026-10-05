using Northfield.Fiscal.Desktop.Forms;

namespace Northfield.Fiscal.Desktop;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
