namespace DrawingColorTool
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelTop = new System.Windows.Forms.Panel();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.btnLoadFiles = new System.Windows.Forms.Button();

            this.splitContainer = new System.Windows.Forms.SplitContainer();

            // Left: file list
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblFiles = new System.Windows.Forms.Label();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.colFileName = new System.Windows.Forms.ColumnHeader();

            // Right: color settings
            this.panelRight = new System.Windows.Forms.Panel();
            this.grpLayers = new System.Windows.Forms.GroupBox();
            this.lvLayers = new System.Windows.Forms.ListView();
            this.colLayerName = new System.Windows.Forms.ColumnHeader();
            this.colLayerColor = new System.Windows.Forms.ColumnHeader();
            this.colLayerLineType = new System.Windows.Forms.ColumnHeader();

            this.grpColorScheme = new System.Windows.Forms.GroupBox();
            this.lblScheme = new System.Windows.Forms.Label();
            this.cboScheme = new System.Windows.Forms.ComboBox();
            this.btnApplyScheme = new System.Windows.Forms.Button();
            this.btnEditColor = new System.Windows.Forms.Button();

            this.grpLineType = new System.Windows.Forms.GroupBox();
            this.lblLineTypeLayer = new System.Windows.Forms.Label();
            this.cboLineTypeLayer = new System.Windows.Forms.ComboBox();
            this.lblLineType = new System.Windows.Forms.Label();
            this.cboLineType = new System.Windows.Forms.ComboBox();
            this.btnApplyLineType = new System.Windows.Forms.Button();

            // Bottom
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();

            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.grpLayers.SuspendLayout();
            this.grpColorScheme.SuspendLayout();
            this.grpLineType.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 45;
            this.panelTop.Padding = new System.Windows.Forms.Padding(8);
            this.panelTop.Controls.Add(this.lblFolder);
            this.panelTop.Controls.Add(this.txtFolder);
            this.panelTop.Controls.Add(this.btnBrowseFolder);
            this.panelTop.Controls.Add(this.btnLoadFiles);

            this.lblFolder.Text = "文件夹:";
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(8, 14);
            this.txtFolder.Location = new System.Drawing.Point(60, 11);
            this.txtFolder.Size = new System.Drawing.Size(480, 22);
            this.txtFolder.ReadOnly = true;
            this.btnBrowseFolder.Text = "浏览...";
            this.btnBrowseFolder.Location = new System.Drawing.Point(545, 10);
            this.btnBrowseFolder.Size = new System.Drawing.Size(70, 24);
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            this.btnLoadFiles.Text = "加载";
            this.btnLoadFiles.Location = new System.Drawing.Point(620, 10);
            this.btnLoadFiles.Size = new System.Drawing.Size(60, 24);
            this.btnLoadFiles.Click += new System.EventHandler(this.btnLoadFiles_Click);

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.SplitterDistance = 250;

            // panelLeft
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Controls.Add(this.lvFiles);
            this.panelLeft.Controls.Add(this.lblFiles);

            this.lblFiles.Text = "工程图文件列表";
            this.lblFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFiles.Height = 20;
            this.lblFiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.CheckBoxes = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colFileName });
            this.colFileName.Text = "文件名";
            this.colFileName.Width = 230;
            this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);

            // panelRight
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Padding = new System.Windows.Forms.Padding(4);
            this.panelRight.AutoScroll = true;

            // grpLayers
            this.grpLayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpLayers.Height = 200;
            this.grpLayers.Text = "图层颜色设置";
            this.grpLayers.Controls.Add(this.lvLayers);

            this.lvLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLayers.View = System.Windows.Forms.View.Details;
            this.lvLayers.FullRowSelect = true;
            this.lvLayers.GridLines = true;
            this.lvLayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colLayerName, this.colLayerColor, this.colLayerLineType
            });
            this.colLayerName.Text = "图层名称";
            this.colLayerName.Width = 120;
            this.colLayerColor.Text = "颜色";
            this.colLayerColor.Width = 100;
            this.colLayerLineType.Text = "线型";
            this.colLayerLineType.Width = 100;

            // grpColorScheme
            this.grpColorScheme.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpColorScheme.Height = 80;
            this.grpColorScheme.Text = "配色方案";
            this.grpColorScheme.Controls.Add(this.lblScheme);
            this.grpColorScheme.Controls.Add(this.cboScheme);
            this.grpColorScheme.Controls.Add(this.btnApplyScheme);
            this.grpColorScheme.Controls.Add(this.btnEditColor);

            this.lblScheme.Text = "方案:";
            this.lblScheme.AutoSize = true;
            this.lblScheme.Location = new System.Drawing.Point(15, 25);
            this.cboScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScheme.Items.AddRange(new object[] { "默认", "黑白打印", "彩色标准", "高对比度", "自定义" });
            this.cboScheme.SelectedIndex = 0;
            this.cboScheme.Location = new System.Drawing.Point(55, 22);
            this.cboScheme.Size = new System.Drawing.Size(120, 21);
            this.btnApplyScheme.Text = "应用方案";
            this.btnApplyScheme.Location = new System.Drawing.Point(190, 21);
            this.btnApplyScheme.Size = new System.Drawing.Size(80, 24);
            this.btnApplyScheme.Click += new System.EventHandler(this.btnApplyScheme_Click);
            this.btnEditColor.Text = "编辑颜色";
            this.btnEditColor.Location = new System.Drawing.Point(280, 21);
            this.btnEditColor.Size = new System.Drawing.Size(80, 24);
            this.btnEditColor.Click += new System.EventHandler(this.btnEditColor_Click);

            // grpLineType
            this.grpLineType.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpLineType.Height = 80;
            this.grpLineType.Text = "线型映射";
            this.grpLineType.Controls.Add(this.lblLineTypeLayer);
            this.grpLineType.Controls.Add(this.cboLineTypeLayer);
            this.grpLineType.Controls.Add(this.lblLineType);
            this.grpLineType.Controls.Add(this.cboLineType);
            this.grpLineType.Controls.Add(this.btnApplyLineType);

            this.lblLineTypeLayer.Text = "图层:";
            this.lblLineTypeLayer.AutoSize = true;
            this.lblLineTypeLayer.Location = new System.Drawing.Point(15, 25);
            this.cboLineTypeLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLineTypeLayer.Location = new System.Drawing.Point(55, 22);
            this.cboLineTypeLayer.Size = new System.Drawing.Size(120, 21);
            this.lblLineType.Text = "线型:";
            this.lblLineType.AutoSize = true;
            this.lblLineType.Location = new System.Drawing.Point(190, 25);
            this.cboLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLineType.Items.AddRange(new object[] { "实线", "虚线", "点划线", "双点划线", "细实线" });
            this.cboLineType.SelectedIndex = 0;
            this.cboLineType.Location = new System.Drawing.Point(230, 22);
            this.cboLineType.Size = new System.Drawing.Size(100, 21);
            this.btnApplyLineType.Text = "应用";
            this.btnApplyLineType.Location = new System.Drawing.Point(345, 21);
            this.btnApplyLineType.Size = new System.Drawing.Size(60, 24);
            this.btnApplyLineType.Click += new System.EventHandler(this.btnApplyLineType_Click);

            this.panelRight.Controls.Add(this.grpLineType);
            this.panelRight.Controls.Add(this.grpColorScheme);
            this.panelRight.Controls.Add(this.grpLayers);

            this.splitContainer.Panel1.Controls.Add(this.panelLeft);
            this.splitContainer.Panel2.Controls.Add(this.panelRight);

            // panelBottom
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 50;
            this.panelBottom.Padding = new System.Windows.Forms.Padding(8);
            this.panelBottom.Controls.Add(this.btnApplyAll);
            this.panelBottom.Controls.Add(this.btnReset);
            this.panelBottom.Controls.Add(this.progressBar);
            this.panelBottom.Controls.Add(this.lblStatus);

            this.btnApplyAll.Text = "应用到所有文件";
            this.btnApplyAll.Location = new System.Drawing.Point(8, 13);
            this.btnApplyAll.Size = new System.Drawing.Size(120, 28);
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);

            this.btnReset.Text = "重置";
            this.btnReset.Location = new System.Drawing.Point(135, 13);
            this.btnReset.Size = new System.Drawing.Size(60, 28);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

            this.progressBar.Location = new System.Drawing.Point(210, 16);
            this.progressBar.Size = new System.Drawing.Size(300, 20);
            this.progressBar.Visible = false;

            this.lblStatus.Text = "就绪";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(520, 18);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 580);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Text = "工程图分层上色工具";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.grpLayers.ResumeLayout(false);
            this.grpColorScheme.ResumeLayout(false);
            this.grpColorScheme.PerformLayout();
            this.grpLineType.ResumeLayout(false);
            this.grpLineType.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Button btnLoadFiles;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.GroupBox grpLayers;
        private System.Windows.Forms.ListView lvLayers;
        private System.Windows.Forms.ColumnHeader colLayerName;
        private System.Windows.Forms.ColumnHeader colLayerColor;
        private System.Windows.Forms.ColumnHeader colLayerLineType;
        private System.Windows.Forms.GroupBox grpColorScheme;
        private System.Windows.Forms.Label lblScheme;
        private System.Windows.Forms.ComboBox cboScheme;
        private System.Windows.Forms.Button btnApplyScheme;
        private System.Windows.Forms.Button btnEditColor;
        private System.Windows.Forms.GroupBox grpLineType;
        private System.Windows.Forms.Label lblLineTypeLayer;
        private System.Windows.Forms.ComboBox cboLineTypeLayer;
        private System.Windows.Forms.Label lblLineType;
        private System.Windows.Forms.ComboBox cboLineType;
        private System.Windows.Forms.Button btnApplyLineType;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
    }
}
