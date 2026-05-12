namespace ModelColorTool
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpPalette = new System.Windows.Forms.GroupBox();
            this.pnlPalettePreview = new System.Windows.Forms.Panel();
            this.lstColors = new System.Windows.Forms.ListBox();
            this.panelPaletteBtns = new System.Windows.Forms.Panel();
            this.btnAddColor = new System.Windows.Forms.Button();
            this.btnRemoveColor = new System.Windows.Forms.Button();
            this.btnRandomPalette = new System.Windows.Forms.Button();
            this.grpComponents = new System.Windows.Forms.GroupBox();
            this.lstComponents = new System.Windows.Forms.ListBox();
            this.panelCompBtns = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.grpScheme = new System.Windows.Forms.GroupBox();
            this.cboScheme = new System.Windows.Forms.ComboBox();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.btnApplySelected = new System.Windows.Forms.Button();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpPalette.SuspendLayout();
            this.panelPaletteBtns.SuspendLayout();
            this.grpComponents.SuspendLayout();
            this.panelCompBtns.SuspendLayout();
            this.grpScheme.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.SuspendLayout();

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.SplitterDistance = 280;
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;

            // ---- 左侧: 调色板 ----
            this.splitContainer.Panel1.Controls.Add(this.grpPalette);

            this.grpPalette.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPalette.Text = "调色板";
            this.grpPalette.Padding = new System.Windows.Forms.Padding(6);
            this.grpPalette.Controls.Add(this.lstColors);
            this.grpPalette.Controls.Add(this.pnlPalettePreview);
            this.grpPalette.Controls.Add(this.panelPaletteBtns);

            // pnlPalettePreview - 颜色条
            this.pnlPalettePreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPalettePreview.Height = 30;
            this.pnlPalettePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPalettePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPalettePreview_Paint);

            // lstColors - 颜色列表
            this.lstColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstColors.IntegralHeight = false;

            // panelPaletteBtns
            this.panelPaletteBtns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPaletteBtns.Height = 40;

            this.btnAddColor.Text = "添加颜色";
            this.btnAddColor.Size = new System.Drawing.Size(90, 30);
            this.btnAddColor.Location = new System.Drawing.Point(6, 5);
            this.btnAddColor.Click += new System.EventHandler(this.btnAddColor_Click);

            this.btnRemoveColor.Text = "移除颜色";
            this.btnRemoveColor.Size = new System.Drawing.Size(90, 30);
            this.btnRemoveColor.Location = new System.Drawing.Point(100, 5);
            this.btnRemoveColor.Click += new System.EventHandler(this.btnRemoveColor_Click);

            this.btnRandomPalette.Text = "随机生成";
            this.btnRandomPalette.Size = new System.Drawing.Size(90, 30);
            this.btnRandomPalette.Location = new System.Drawing.Point(194, 5);
            this.btnRandomPalette.Click += new System.EventHandler(this.btnRandomPalette_Click);

            this.panelPaletteBtns.Controls.Add(this.btnAddColor);
            this.panelPaletteBtns.Controls.Add(this.btnRemoveColor);
            this.panelPaletteBtns.Controls.Add(this.btnRandomPalette);

            // ---- 右侧: 组件 + 设置 ----
            this.splitContainer.Panel2.Controls.Add(this.grpComponents);
            this.splitContainer.Panel2.Controls.Add(this.grpScheme);
            this.splitContainer.Panel2.Controls.Add(this.grpActions);
            this.splitContainer.Panel2.Controls.Add(this.lblStatus);

            // grpComponents
            this.grpComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpComponents.Text = "组件列表";
            this.grpComponents.Padding = new System.Windows.Forms.Padding(6);
            this.grpComponents.Controls.Add(this.lstComponents);
            this.grpComponents.Controls.Add(this.panelCompBtns);

            this.lstComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstComponents.IntegralHeight = false;
            this.lstComponents.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

            this.panelCompBtns.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCompBtns.Height = 40;

            this.btnConnect.Text = "连接 SolidWorks / 刷新组件";
            this.btnConnect.Size = new System.Drawing.Size(220, 32);
            this.btnConnect.Location = new System.Drawing.Point(6, 4);
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            this.panelCompBtns.Controls.Add(this.btnConnect);

            // grpScheme
            this.grpScheme.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpScheme.Height = 55;
            this.grpScheme.Text = "上色方案";
            this.grpScheme.Padding = new System.Windows.Forms.Padding(6);
            this.grpScheme.Controls.Add(this.cboScheme);

            this.cboScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboScheme.Location = new System.Drawing.Point(6, 21);
            this.cboScheme.Size = new System.Drawing.Size(268, 24);
            this.cboScheme.SelectedIndexChanged += new System.EventHandler(this.cboScheme_SelectedIndexChanged);

            // grpActions
            this.grpActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpActions.Height = 65;
            this.grpActions.Text = "操作";
            this.grpActions.Padding = new System.Windows.Forms.Padding(6);
            this.grpActions.Controls.Add(this.btnApplySelected);
            this.grpActions.Controls.Add(this.btnApplyAll);

            this.btnApplySelected.Text = "为选中组件上色";
            this.btnApplySelected.Size = new System.Drawing.Size(130, 32);
            this.btnApplySelected.Location = new System.Drawing.Point(10, 22);
            this.btnApplySelected.Click += new System.EventHandler(this.btnApplySelected_Click);

            this.btnApplyAll.Text = "为所有组件上色";
            this.btnApplyAll.Size = new System.Drawing.Size(130, 32);
            this.btnApplyAll.Location = new System.Drawing.Point(146, 22);
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);

            // lblStatus
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Text = "状态: 未连接";
            this.lblStatus.Height = 25;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);

            // MainForm
            this.ClientSize = new System.Drawing.Size(700, 480);
            this.Controls.Add(this.splitContainer);
            this.Text = "ModelColorTool - 模型随机上色";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpPalette.ResumeLayout(false);
            this.panelPaletteBtns.ResumeLayout(false);
            this.grpComponents.ResumeLayout(false);
            this.panelCompBtns.ResumeLayout(false);
            this.grpScheme.ResumeLayout(false);
            this.grpActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpPalette;
        private System.Windows.Forms.Panel pnlPalettePreview;
        private System.Windows.Forms.ListBox lstColors;
        private System.Windows.Forms.Panel panelPaletteBtns;
        private System.Windows.Forms.Button btnAddColor;
        private System.Windows.Forms.Button btnRemoveColor;
        private System.Windows.Forms.Button btnRandomPalette;
        private System.Windows.Forms.GroupBox grpComponents;
        private System.Windows.Forms.ListBox lstComponents;
        private System.Windows.Forms.Panel panelCompBtns;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox grpScheme;
        private System.Windows.Forms.ComboBox cboScheme;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button btnApplySelected;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.Label lblStatus;
    }
}
