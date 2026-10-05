#nullable disable
using System.ComponentModel;

namespace Northfield.Fiscal.Desktop.Controls;

partial class HomeWorkspaceControl
{
    private IContainer components = null;
    private TableLayoutPanel _root;
    private GroupBox _grpCompany;
    private Label _lblCompanyName;
    private Label _lblCompanyCnpj;
    private Label _lblCompetence;
    private Button _btnCompany;
    private GroupBox _grpActions;
    private FlowLayoutPanel _actionsFlow;
    private Button _btnNew;
    private Button _btnImport;
    private Button _btnOpen;
    private Button _btnSegregation;
    private Button _btnPgdas;
    private Button _btnReview;
    private SplitContainer _centerSplit;
    private GroupBox _grpRecent;
    private DataGridView _gridRecent;
    private GroupBox _grpPending;
    private ListBox _lstPending;
    private GroupBox _grpSummary;
    private TableLayoutPanel _summaryTable;
    private Label _lblRevenueCaption;
    private Label _lblRbt12Caption;
    private Label _lblFactorRCaption;
    private Label _lblDocumentsCaption;
    private Label _lblClassifiedCaption;
    private Label _lblPendingCaption;
    private Label _lblRevenueValue;
    private Label _lblRbt12Value;
    private Label _lblFactorRValue;
    private Label _lblDocumentsValue;
    private Label _lblClassifiedValue;
    private Label _lblPendingValue;
    private GroupBox _grpNotices;
    private ListBox _lstNotices;

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
        _grpCompany = new GroupBox();
        _lblCompanyName = new Label();
        _lblCompanyCnpj = new Label();
        _lblCompetence = new Label();
        _btnCompany = new Button();
        _grpActions = new GroupBox();
        _actionsFlow = new FlowLayoutPanel();
        _btnNew = new Button();
        _btnImport = new Button();
        _btnOpen = new Button();
        _btnSegregation = new Button();
        _btnPgdas = new Button();
        _btnReview = new Button();
        _centerSplit = new SplitContainer();
        _grpRecent = new GroupBox();
        _gridRecent = new DataGridView();
        _grpPending = new GroupBox();
        _lstPending = new ListBox();
        _grpSummary = new GroupBox();
        _summaryTable = new TableLayoutPanel();
        _lblRevenueCaption = new Label();
        _lblRbt12Caption = new Label();
        _lblFactorRCaption = new Label();
        _lblDocumentsCaption = new Label();
        _lblClassifiedCaption = new Label();
        _lblPendingCaption = new Label();
        _lblRevenueValue = new Label();
        _lblRbt12Value = new Label();
        _lblFactorRValue = new Label();
        _lblDocumentsValue = new Label();
        _lblClassifiedValue = new Label();
        _lblPendingValue = new Label();
        _grpNotices = new GroupBox();
        _lstNotices = new ListBox();
        _root.SuspendLayout();
        _grpCompany.SuspendLayout();
        _grpActions.SuspendLayout();
        _actionsFlow.SuspendLayout();
        ((ISupportInitialize)_centerSplit).BeginInit();
        _centerSplit.Panel1.SuspendLayout();
        _centerSplit.Panel2.SuspendLayout();
        _centerSplit.SuspendLayout();
        _grpRecent.SuspendLayout();
        ((ISupportInitialize)_gridRecent).BeginInit();
        _grpPending.SuspendLayout();
        _grpSummary.SuspendLayout();
        _summaryTable.SuspendLayout();
        _grpNotices.SuspendLayout();
        SuspendLayout();

        _root.ColumnCount = 1;
        _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _root.Controls.Add(_grpCompany, 0, 0);
        _root.Controls.Add(_grpActions, 0, 1);
        _root.Controls.Add(_centerSplit, 0, 2);
        _root.Controls.Add(_grpSummary, 0, 3);
        _root.Controls.Add(_grpNotices, 0, 4);
        _root.Dock = DockStyle.Fill;
        _root.Location = new Point(0, 0);
        _root.Margin = new Padding(0);
        _root.Name = "_root";
        _root.Padding = new Padding(8);
        _root.RowCount = 5;
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
        _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 125F));
        _root.Size = new Size(1280, 760);

        _grpCompany.Controls.Add(_lblCompanyName);
        _grpCompany.Controls.Add(_lblCompanyCnpj);
        _grpCompany.Controls.Add(_lblCompetence);
        _grpCompany.Controls.Add(_btnCompany);
        _grpCompany.Dock = DockStyle.Fill;
        _grpCompany.Location = new Point(11, 11);
        _grpCompany.Name = "_grpCompany";
        _grpCompany.Padding = new Padding(10, 8, 10, 8);
        _grpCompany.Size = new Size(1258, 80);
        _grpCompany.TabIndex = 0;
        _grpCompany.TabStop = false;
        _grpCompany.Text = "Empresa ativa";

        _lblCompanyName.AutoSize = true;
        _lblCompanyName.Location = new Point(18, 25);
        _lblCompanyName.Name = "_lblCompanyName";
        _lblCompanyName.Text = "Nenhuma empresa informada";

        _lblCompanyCnpj.AutoSize = true;
        _lblCompanyCnpj.Location = new Point(18, 50);
        _lblCompanyCnpj.Name = "_lblCompanyCnpj";
        _lblCompanyCnpj.Text = "CNPJ: -";

        _lblCompetence.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblCompetence.AutoSize = true;
        _lblCompetence.Location = new Point(1010, 29);
        _lblCompetence.Name = "_lblCompetence";
        _lblCompetence.Text = "Competência: -";

        _btnCompany.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnCompany.Location = new Point(1106, 46);
        _btnCompany.Name = "_btnCompany";
        _btnCompany.Size = new Size(132, 26);
        _btnCompany.TabIndex = 0;
        _btnCompany.Text = "Dados da empresa";
        _btnCompany.UseVisualStyleBackColor = true;

        _grpActions.Controls.Add(_actionsFlow);
        _grpActions.Dock = DockStyle.Fill;
        _grpActions.Location = new Point(11, 97);
        _grpActions.Name = "_grpActions";
        _grpActions.Padding = new Padding(8, 8, 8, 8);
        _grpActions.Size = new Size(1258, 84);
        _grpActions.TabIndex = 1;
        _grpActions.TabStop = false;
        _grpActions.Text = "Rotinas";

        _actionsFlow.Controls.Add(_btnNew);
        _actionsFlow.Controls.Add(_btnImport);
        _actionsFlow.Controls.Add(_btnOpen);
        _actionsFlow.Controls.Add(_btnReview);
        _actionsFlow.Controls.Add(_btnSegregation);
        _actionsFlow.Controls.Add(_btnPgdas);
        _actionsFlow.Dock = DockStyle.Fill;
        _actionsFlow.Location = new Point(8, 24);
        _actionsFlow.Name = "_actionsFlow";
        _actionsFlow.Padding = new Padding(4, 5, 4, 4);
        _actionsFlow.Size = new Size(1242, 52);
        _actionsFlow.TabIndex = 0;
        _actionsFlow.WrapContents = false;

        _btnNew.Location = new Point(7, 8);
        _btnNew.Margin = new Padding(3, 3, 8, 3);
        _btnNew.Name = "_btnNew";
        _btnNew.Size = new Size(132, 34);
        _btnNew.TabIndex = 0;
        _btnNew.Text = "Nova apuração";
        _btnNew.UseVisualStyleBackColor = true;

        _btnImport.Location = new Point(150, 8);
        _btnImport.Margin = new Padding(3, 3, 8, 3);
        _btnImport.Name = "_btnImport";
        _btnImport.Size = new Size(145, 34);
        _btnImport.TabIndex = 1;
        _btnImport.Text = "Importar documentos";
        _btnImport.UseVisualStyleBackColor = true;

        _btnOpen.Location = new Point(306, 8);
        _btnOpen.Margin = new Padding(3, 3, 8, 3);
        _btnOpen.Name = "_btnOpen";
        _btnOpen.Size = new Size(132, 34);
        _btnOpen.TabIndex = 2;
        _btnOpen.Text = "Abrir apuração";
        _btnOpen.UseVisualStyleBackColor = true;

        _btnReview.Location = new Point(449, 8);
        _btnReview.Margin = new Padding(3, 3, 8, 3);
        _btnReview.Name = "_btnReview";
        _btnReview.Size = new Size(120, 34);
        _btnReview.TabIndex = 3;
        _btnReview.Text = "Conferências";
        _btnReview.UseVisualStyleBackColor = true;

        _btnSegregation.Location = new Point(580, 8);
        _btnSegregation.Margin = new Padding(3, 3, 8, 3);
        _btnSegregation.Name = "_btnSegregation";
        _btnSegregation.Size = new Size(120, 34);
        _btnSegregation.TabIndex = 4;
        _btnSegregation.Text = "Segregação";
        _btnSegregation.UseVisualStyleBackColor = true;

        _btnPgdas.Location = new Point(711, 8);
        _btnPgdas.Margin = new Padding(3, 3, 8, 3);
        _btnPgdas.Name = "_btnPgdas";
        _btnPgdas.Size = new Size(120, 34);
        _btnPgdas.TabIndex = 5;
        _btnPgdas.Text = "PGDAS-D";
        _btnPgdas.UseVisualStyleBackColor = true;

        _centerSplit.Dock = DockStyle.Fill;
        _centerSplit.Location = new Point(11, 187);
        _centerSplit.Name = "_centerSplit";
        _centerSplit.Panel1.Controls.Add(_grpRecent);
        _centerSplit.Panel2.Controls.Add(_grpPending);
        _centerSplit.Size = new Size(1258, 332);
        _centerSplit.SplitterDistance = 840;
        _centerSplit.SplitterWidth = 6;
        _centerSplit.TabIndex = 2;

        _grpRecent.Controls.Add(_gridRecent);
        _grpRecent.Dock = DockStyle.Fill;
        _grpRecent.Location = new Point(0, 0);
        _grpRecent.Name = "_grpRecent";
        _grpRecent.Padding = new Padding(8, 8, 8, 8);
        _grpRecent.Size = new Size(840, 332);
        _grpRecent.TabIndex = 0;
        _grpRecent.TabStop = false;
        _grpRecent.Text = "Apurações recentes";

        _gridRecent.Dock = DockStyle.Fill;
        _gridRecent.Location = new Point(8, 24);
        _gridRecent.Name = "_gridRecent";
        _gridRecent.Size = new Size(824, 300);
        _gridRecent.TabIndex = 0;

        _grpPending.Controls.Add(_lstPending);
        _grpPending.Dock = DockStyle.Fill;
        _grpPending.Location = new Point(0, 0);
        _grpPending.Name = "_grpPending";
        _grpPending.Padding = new Padding(8, 8, 8, 8);
        _grpPending.Size = new Size(412, 332);
        _grpPending.TabIndex = 0;
        _grpPending.TabStop = false;
        _grpPending.Text = "Pendências";

        _lstPending.Dock = DockStyle.Fill;
        _lstPending.FormattingEnabled = true;
        _lstPending.IntegralHeight = false;
        _lstPending.Location = new Point(8, 24);
        _lstPending.Name = "_lstPending";
        _lstPending.Size = new Size(396, 300);
        _lstPending.TabIndex = 0;

        _grpSummary.Controls.Add(_summaryTable);
        _grpSummary.Dock = DockStyle.Fill;
        _grpSummary.Location = new Point(11, 525);
        _grpSummary.Name = "_grpSummary";
        _grpSummary.Padding = new Padding(8, 8, 8, 8);
        _grpSummary.Size = new Size(1258, 99);
        _grpSummary.TabIndex = 3;
        _grpSummary.TabStop = false;
        _grpSummary.Text = "Resumo da competência";

        _summaryTable.ColumnCount = 6;
        _summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
        _summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
        _summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
        _summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
        _summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
        _summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
        _summaryTable.Controls.Add(_lblRevenueCaption, 0, 0);
        _summaryTable.Controls.Add(_lblRbt12Caption, 1, 0);
        _summaryTable.Controls.Add(_lblFactorRCaption, 2, 0);
        _summaryTable.Controls.Add(_lblDocumentsCaption, 3, 0);
        _summaryTable.Controls.Add(_lblClassifiedCaption, 4, 0);
        _summaryTable.Controls.Add(_lblPendingCaption, 5, 0);
        _summaryTable.Controls.Add(_lblRevenueValue, 0, 1);
        _summaryTable.Controls.Add(_lblRbt12Value, 1, 1);
        _summaryTable.Controls.Add(_lblFactorRValue, 2, 1);
        _summaryTable.Controls.Add(_lblDocumentsValue, 3, 1);
        _summaryTable.Controls.Add(_lblClassifiedValue, 4, 1);
        _summaryTable.Controls.Add(_lblPendingValue, 5, 1);
        _summaryTable.Dock = DockStyle.Fill;
        _summaryTable.Location = new Point(8, 24);
        _summaryTable.Name = "_summaryTable";
        _summaryTable.RowCount = 2;
        _summaryTable.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
        _summaryTable.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
        _summaryTable.Size = new Size(1242, 67);

        _lblRevenueCaption.Dock = DockStyle.Fill;
        _lblRevenueCaption.Text = "Receita";
        _lblRevenueCaption.TextAlign = ContentAlignment.BottomCenter;
        _lblRbt12Caption.Dock = DockStyle.Fill;
        _lblRbt12Caption.Text = "RBT12";
        _lblRbt12Caption.TextAlign = ContentAlignment.BottomCenter;
        _lblFactorRCaption.Dock = DockStyle.Fill;
        _lblFactorRCaption.Text = "Fator R";
        _lblFactorRCaption.TextAlign = ContentAlignment.BottomCenter;
        _lblDocumentsCaption.Dock = DockStyle.Fill;
        _lblDocumentsCaption.Text = "Documentos";
        _lblDocumentsCaption.TextAlign = ContentAlignment.BottomCenter;
        _lblClassifiedCaption.Dock = DockStyle.Fill;
        _lblClassifiedCaption.Text = "Classificados";
        _lblClassifiedCaption.TextAlign = ContentAlignment.BottomCenter;
        _lblPendingCaption.Dock = DockStyle.Fill;
        _lblPendingCaption.Text = "Pendentes";
        _lblPendingCaption.TextAlign = ContentAlignment.BottomCenter;

        _lblRevenueValue.Dock = DockStyle.Fill;
        _lblRevenueValue.Text = "R$ 0,00";
        _lblRevenueValue.TextAlign = ContentAlignment.TopCenter;
        _lblRbt12Value.Dock = DockStyle.Fill;
        _lblRbt12Value.Text = "R$ 0,00";
        _lblRbt12Value.TextAlign = ContentAlignment.TopCenter;
        _lblFactorRValue.Dock = DockStyle.Fill;
        _lblFactorRValue.Text = "-";
        _lblFactorRValue.TextAlign = ContentAlignment.TopCenter;
        _lblDocumentsValue.Dock = DockStyle.Fill;
        _lblDocumentsValue.Text = "0";
        _lblDocumentsValue.TextAlign = ContentAlignment.TopCenter;
        _lblClassifiedValue.Dock = DockStyle.Fill;
        _lblClassifiedValue.Text = "0";
        _lblClassifiedValue.TextAlign = ContentAlignment.TopCenter;
        _lblPendingValue.Dock = DockStyle.Fill;
        _lblPendingValue.Text = "0";
        _lblPendingValue.TextAlign = ContentAlignment.TopCenter;

        _grpNotices.Controls.Add(_lstNotices);
        _grpNotices.Dock = DockStyle.Fill;
        _grpNotices.Location = new Point(11, 630);
        _grpNotices.Name = "_grpNotices";
        _grpNotices.Padding = new Padding(8, 8, 8, 8);
        _grpNotices.Size = new Size(1258, 119);
        _grpNotices.TabIndex = 4;
        _grpNotices.TabStop = false;
        _grpNotices.Text = "Avisos";

        _lstNotices.Dock = DockStyle.Fill;
        _lstNotices.FormattingEnabled = true;
        _lstNotices.IntegralHeight = false;
        _lstNotices.Location = new Point(8, 24);
        _lstNotices.Name = "_lstNotices";
        _lstNotices.Size = new Size(1242, 87);
        _lstNotices.TabIndex = 0;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_root);
        Name = "HomeWorkspaceControl";
        Size = new Size(1280, 760);
        _root.ResumeLayout(false);
        _grpCompany.ResumeLayout(false);
        _grpCompany.PerformLayout();
        _grpActions.ResumeLayout(false);
        _actionsFlow.ResumeLayout(false);
        _centerSplit.Panel1.ResumeLayout(false);
        _centerSplit.Panel2.ResumeLayout(false);
        ((ISupportInitialize)_centerSplit).EndInit();
        _centerSplit.ResumeLayout(false);
        _grpRecent.ResumeLayout(false);
        ((ISupportInitialize)_gridRecent).EndInit();
        _grpPending.ResumeLayout(false);
        _grpSummary.ResumeLayout(false);
        _summaryTable.ResumeLayout(false);
        _grpNotices.ResumeLayout(false);
        ResumeLayout(false);
    }
}
