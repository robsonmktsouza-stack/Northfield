namespace Northfield.Fiscal.Desktop.Forms;

public sealed partial class MainForm
{
    private void InitializeHomeWorkspace()
    {
        // A área inicial é o próprio fundo da janela, não uma aba.
        _tabs.Visible = false;
    }

    private void ShowRoutineTab(TabPage tab)
    {
        _tabs.Visible = true;
        _tabs.SelectedTab = tab;
        tab.Focus();
    }

    private void ShowHomeWorkspace()
    {
        _tabs.Visible = false;
        ActiveControl = null;
    }

    private void RefreshHomeWorkspace()
    {
        // A área inicial permanece propositalmente vazia.
    }
}
