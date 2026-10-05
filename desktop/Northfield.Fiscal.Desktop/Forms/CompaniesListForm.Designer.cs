#nullable disable
using System.ComponentModel;
using Northfield.Fiscal.Desktop.Controls;

namespace Northfield.Fiscal.Desktop.Forms;

partial class CompaniesListForm
{
    private IContainer components = null;
    private TableLayoutPanel _root;
    private NorthfieldPanel _header;
    private Label _lblTitle;
    private FlowLayoutPanel _actions;
    private NorthfieldButton _btnNew;
    private NorthfieldButton _btnEdit;
    private NorthfieldButton _btnDelete;
    private NorthfieldButton _btnSelect;
    private Panel _searchPanel;
    private Label _lblSearch;
    private NorthfieldSearchBox _txtSearch;
    private NorthfieldGrid _grid;
    private Panel _footer;
    private Label _lblCount;
    private NorthfieldButton _btnClose;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        _root = new TableLayoutPanel();
        _header = new NorthfieldPanel();
        _lblTitle = new Label();
        _actions = new FlowLayoutPanel();
        _btnNew = new NorthfieldButton();
        _btnEdit = new NorthfieldButton();
        _btnDelete = new NorthfieldButton();
        _btnSelect = new NorthfieldButton();
        _searchPanel = new Panel();
        _lblSearch = new Label();
        _txtSearch = new NorthfieldSearchBox();
        _grid = new NorthfieldGrid();
        _footer = new Panel();
        _lblCount = new Label();
        _btnClose = new NorthfieldButton();

        _root.SuspendLayout();
        _header.SuspendLayout();
        _actions.SuspendLayout();
        _searchPanel.SuspendLayout();
        ((ISupportInitialize)_grid).BeginInit();
        _footer.SuspendLayout();
        SuspendLayout();

        _root.ColumnCount = 1;
        _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _root.Controls.Add(_header, 0, 0);
        _root.Controls.Add(_searchPanel, 0, 1);
        _root.Controls.Add(_grid, 0, 2);
        _root.Controls.Add(_footer, 0, 3);
        _root.Dock = DockStyle.Fill;
        _root.Location = new Point(0, 0);
        _root.Margin = new Padding(0);
        _root.Name = "_root";
        _root.RowCount = 4;
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        _root.Size = new Size(1000, 560);

        _header.Controls.Add(_lblTitle);
        _header.Controls.Add(_actions);
        _header.Dock = DockStyle.Fill;
        _header.Location = new Point(0, 0);
        _header.Margin = new Padding(0);
        _header.Name = "_header";
        _header.Size = new Size(1000, 36);

        _lblTitle.AutoSize = true;
        _lblTitle.Location = new Point(10, 10);
        _lblTitle.Name = "_lblTitle";
        _lblTitle.Text = "Empresas";

        _actions.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        _actions.AutoSize = true;
        _actions.Controls.Add(_btnNew);
        _actions.Controls.Add(_btnEdit);
        _actions.Controls.Add(_btnDelete);
        _actions.Controls.Add(_btnSelect);
        _actions.FlowDirection = FlowDirection.LeftToRight;
        _actions.Location = new Point(90, 6);
        _actions.Margin = new Padding(0);
        _actions.Name = "_actions";
        _actions.Size = new Size(336, 24);
        _actions.WrapContents = false;

        _btnSelect.Margin = new Padding(4, 0, 0, 0);
        _btnSelect.Name = "_btnSelect";
        _btnSelect.Size = new Size(94, 24);
        _btnSelect.Text = "Selecionar";

        _btnNew.Margin = new Padding(0, 0, 4, 0);
        _btnNew.Name = "_btnNew";
        _btnNew.Size = new Size(72, 24);
        _btnNew.Text = "Novo";

        _btnEdit.Enabled = false;
        _btnEdit.Margin = new Padding(0, 0, 4, 0);
        _btnEdit.Name = "_btnEdit";
        _btnEdit.Size = new Size(74, 24);
        _btnEdit.Text = "Editar";

        _btnDelete.Enabled = false;
        _btnDelete.Margin = new Padding(0, 0, 4, 0);
        _btnDelete.Name = "_btnDelete";
        _btnDelete.Size = new Size(78, 24);
        _btnDelete.Text = "Excluir";

        _searchPanel.Controls.Add(_lblSearch);
        _searchPanel.Controls.Add(_txtSearch);
        _searchPanel.Dock = DockStyle.Fill;
        _searchPanel.Location = new Point(0, 36);
        _searchPanel.Margin = new Padding(0);
        _searchPanel.Name = "_searchPanel";
        _searchPanel.Padding = new Padding(10, 4, 10, 4);
        _searchPanel.Size = new Size(1000, 34);

        _lblSearch.AutoSize = true;
        _lblSearch.Location = new Point(11, 10);
        _lblSearch.Name = "_lblSearch";
        _lblSearch.Text = "Pesquisar:";

        _txtSearch.Location = new Point(72, 4);
        _txtSearch.Name = "_txtSearch";
        _txtSearch.PlaceholderText = "CNPJ, razão social, município ou UF";
        _txtSearch.Size = new Size(320, 26);

        _grid.Dock = DockStyle.Fill;
        _grid.Location = new Point(0, 70);
        _grid.Margin = new Padding(0);
        _grid.Name = "_grid";
        _grid.Size = new Size(1000, 460);

        _footer.Controls.Add(_lblCount);
        _footer.Controls.Add(_btnClose);
        _footer.Dock = DockStyle.Fill;
        _footer.Location = new Point(0, 530);
        _footer.Margin = new Padding(0);
        _footer.Name = "_footer";
        _footer.Size = new Size(1000, 30);

        _lblCount.AutoSize = true;
        _lblCount.Location = new Point(10, 8);
        _lblCount.Name = "_lblCount";
        _lblCount.Text = "0 registros";

        _btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnClose.DialogResult = DialogResult.Cancel;
        _btnClose.Location = new Point(920, 3);
        _btnClose.Name = "_btnClose";
        _btnClose.Size = new Size(70, 24);
        _btnClose.Text = "Fechar";

        AcceptButton = _btnSelect;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnClose;
        ClientSize = new Size(1000, 560);
        Controls.Add(_root);
        MinimumSize = new Size(820, 460);
        Name = "CompaniesListForm";
        ShowIcon = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Empresas";

        _root.ResumeLayout(false);
        _header.ResumeLayout(false);
        _header.PerformLayout();
        _actions.ResumeLayout(false);
        _searchPanel.ResumeLayout(false);
        _searchPanel.PerformLayout();
        ((ISupportInitialize)_grid).EndInit();
        _footer.ResumeLayout(false);
        _footer.PerformLayout();
        ResumeLayout(false);
    }
}
