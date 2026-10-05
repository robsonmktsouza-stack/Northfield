#nullable disable
using System.ComponentModel;
using Northfield.Fiscal.Desktop.Controls;

namespace Northfield.Fiscal.Desktop.Forms;

partial class CompaniesListForm
{
    private IContainer components = null;
    private TableLayoutPanel _root;
    private NorthfieldPanel _commands;
    private Label _lblTitle;
    private NorthfieldSearchBox _txtSearch;
    private NorthfieldStatusBadge _lblCount;
    private NorthfieldGrid _grid;
    private FlowLayoutPanel _bottom;
    private NorthfieldButton _btnSelect;
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
        _commands = new NorthfieldPanel();
        _lblTitle = new Label();
        _txtSearch = new NorthfieldSearchBox();
        _lblCount = new NorthfieldStatusBadge();
        _grid = new NorthfieldGrid();
        _bottom = new FlowLayoutPanel();
        _btnSelect = new NorthfieldButton();
        _btnClose = new NorthfieldButton();

        _root.SuspendLayout();
        _commands.SuspendLayout();
        ((ISupportInitialize)_grid).BeginInit();
        _bottom.SuspendLayout();
        SuspendLayout();

        _root.ColumnCount = 1;
        _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _root.Controls.Add(_commands, 0, 0);
        _root.Controls.Add(_grid, 0, 1);
        _root.Controls.Add(_bottom, 0, 2);
        _root.Dock = DockStyle.Fill;
        _root.Location = new Point(0, 0);
        _root.Margin = new Padding(0);
        _root.Name = "_root";
        _root.RowCount = 3;
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        _root.Size = new Size(1100, 650);

        _commands.Controls.Add(_lblTitle);
        _commands.Controls.Add(_txtSearch);
        _commands.Controls.Add(_lblCount);
        _commands.Dock = DockStyle.Fill;
        _commands.Location = new Point(0, 0);
        _commands.Margin = new Padding(0);
        _commands.Name = "_commands";
        _commands.Size = new Size(1100, 70);

        _lblTitle.AutoSize = true;
        _lblTitle.Location = new Point(14, 10);
        _lblTitle.Name = "_lblTitle";
        _lblTitle.Text = "Empresas";

        _txtSearch.Location = new Point(14, 34);
        _txtSearch.Name = "_txtSearch";
        _txtSearch.PlaceholderText = "Pesquisar por CNPJ, razão social ou município...";
        _txtSearch.Size = new Size(390, 30);

        _lblCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblCount.AutoSize = true;
        _lblCount.Location = new Point(978, 34);
        _lblCount.Name = "_lblCount";
        _lblCount.Text = "0 empresa(s)";

        _grid.Dock = DockStyle.Fill;
        _grid.Location = new Point(0, 70);
        _grid.Margin = new Padding(0);
        _grid.Name = "_grid";
        _grid.Size = new Size(1100, 530);

        _bottom.Controls.Add(_btnClose);
        _bottom.Controls.Add(_btnSelect);
        _bottom.Dock = DockStyle.Fill;
        _bottom.FlowDirection = FlowDirection.RightToLeft;
        _bottom.Location = new Point(0, 600);
        _bottom.Margin = new Padding(0);
        _bottom.Name = "_bottom";
        _bottom.Padding = new Padding(8, 9, 8, 8);
        _bottom.Size = new Size(1100, 50);
        _bottom.WrapContents = false;

        _btnClose.DialogResult = DialogResult.Cancel;
        _btnClose.Margin = new Padding(6, 0, 0, 0);
        _btnClose.Name = "_btnClose";
        _btnClose.Size = new Size(92, 30);
        _btnClose.Text = "Fechar";

        _btnSelect.Enabled = false;
        _btnSelect.Margin = new Padding(6, 0, 0, 0);
        _btnSelect.Name = "_btnSelect";
        _btnSelect.Size = new Size(112, 30);
        _btnSelect.Text = "Selecionar";

        AcceptButton = _btnSelect;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnClose;
        ClientSize = new Size(1100, 650);
        Controls.Add(_root);
        MinimumSize = new Size(850, 500);
        Name = "CompaniesListForm";
        ShowIcon = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Empresas";

        _root.ResumeLayout(false);
        _commands.ResumeLayout(false);
        _commands.PerformLayout();
        ((ISupportInitialize)_grid).EndInit();
        _bottom.ResumeLayout(false);
        ResumeLayout(false);
    }
}
