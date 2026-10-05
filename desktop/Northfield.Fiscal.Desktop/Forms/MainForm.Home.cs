namespace Northfield.Fiscal.Desktop.Forms;

public sealed partial class MainForm
{
    private TabPage _tabHome = null!;

    private void InitializeHomeWorkspace()
    {
        _tabHome = new TabPage
        {
            Text = "Início",
            Name = "_tabHome",
            Padding = new Padding(0),
            BackColor = Theme.AppBack,
            UseVisualStyleBackColor = false
        };

        _tabs.TabPages.Insert(0, _tabHome);
        _tabs.SelectedTab = _tabHome;
    }

    private void RefreshHomeWorkspace()
    {
        // A tela inicial permanece propositalmente limpa.
    }
}
