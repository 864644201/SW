namespace FileRename
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

            // Top panel - folder selection
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            // Filter panel
            this.panelFilter = new System.Windows.Forms.Panel();
            this.lblFilter = new System.Windows.Forms.Label();
            this.chkSldprt = new System.Windows.Forms.CheckBox();
            this.chkSldasm = new System.Windows.Forms.CheckBox();
            this.chkSlddrw = new System.Windows.Forms.CheckBox();
            this.chkAllTypes = new System.Windows.Forms.CheckBox();

            // Pattern panel
            this.panelPattern = new System.Windows.Forms.GroupBox();
            this.lblRenameMode = new System.Windows.Forms.Label();
            this.cboRenameMode = new System.Windows.Forms.ComboBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.lblFind = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.lblReplace = new System.Windows.Forms.Label();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.lblStartNum = new System.Windows.Forms.Label();
            this.nudStartNum = new System.Windows.Forms.NumericUpDown();
            this.lblStep = new System.Windows.Forms.Label();
            this.nudStep = new System.Windows.Forms.NumericUpDown();
            this.lblNumDigits = new System.Windows.Forms.Label();
            this.nudNumDigits = new System.Windows.Forms.NumericUpDown();

            // File list
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.colOriginal = new System.Windows.Forms.ColumnHeader();
            this.colNewName = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.lblPreview = new System.Windows.Forms.Label();

            // Bottom panel
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();

            // Suspend layouts
            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelPattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumDigits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 45;
            this.panelTop.Padding = new System.Windows.Forms.Padding(8);
            this.panelTop.Controls.Add(this.lblFolder);
            this.panelTop.Controls.Add(this.txtFolder);
            this.panelTop.Controls.Add(this.btnBrowseFolder);
            this.panelTop.Controls.Add(this.btnRefresh);

            this.lblFolder.Text = "文件夹:";
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(8, 14);

            this.txtFolder.Location = new System.Drawing.Point(60, 11);
            this.txtFolder.Size = new System.Drawing.Size(500, 22);
            this.txtFolder.ReadOnly = true;

            this.btnBrowseFolder.Text = "浏览...";
            this.btnBrowseFolder.Location = new System.Drawing.Point(565, 10);
            this.btnBrowseFolder.Size = new System.Drawing.Size(70, 24);
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);

            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Location = new System.Drawing.Point(640, 10);
            this.btnRefresh.Size = new System.Drawing.Size(60, 24);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // panelFilter
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Height = 35;
            this.panelFilter.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.panelFilter.Controls.Add(this.lblFilter);
            this.panelFilter.Controls.Add(this.chkSldprt);
            this.panelFilter.Controls.Add(this.chkSldasm);
            this.panelFilter.Controls.Add(this.chkSlddrw);
            this.panelFilter.Controls.Add(this.chkAllTypes);

            this.lblFilter.Text = "文件类型:";
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(8, 9);

            this.chkSldprt.Text = ".sldprt";
            this.chkSldprt.AutoSize = true;
            this.chkSldprt.Location = new System.Drawing.Point(75, 7);
            this.chkSldprt.Checked = true;

            this.chkSldasm.Text = ".sldasm";
            this.chkSldasm.AutoSize = true;
            this.chkSldasm.Location = new System.Drawing.Point(150, 7);
            this.chkSldasm.Checked = true;

            this.chkSlddrw.Text = ".slddrw";
            this.chkSlddrw.AutoSize = true;
            this.chkSlddrw.Location = new System.Drawing.Point(225, 7);
            this.chkSlddrw.Checked = true;

            this.chkAllTypes.Text = "所有文件";
            this.chkAllTypes.AutoSize = true;
            this.chkAllTypes.Location = new System.Drawing.Point(305, 7);

            // panelPattern
            this.panelPattern.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPattern.Height = 130;
            this.panelPattern.Padding = new System.Windows.Forms.Padding(8);
            this.panelPattern.Text = "重命名规则";

            this.lblRenameMode.Text = "模式:";
            this.lblRenameMode.AutoSize = true;
            this.lblRenameMode.Location = new System.Drawing.Point(15, 25);

            this.cboRenameMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRenameMode.Items.AddRange(new object[] { "添加前缀", "添加后缀", "查找替换", "顺序编号" });
            this.cboRenameMode.SelectedIndex = 0;
            this.cboRenameMode.Location = new System.Drawing.Point(60, 22);
            this.cboRenameMode.Size = new System.Drawing.Size(120, 21);
            this.cboRenameMode.SelectedIndexChanged += new System.EventHandler(this.cboRenameMode_SelectedIndexChanged);

            this.lblPrefix.Text = "前缀:";
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(200, 25);
            this.txtPrefix.Location = new System.Drawing.Point(240, 22);
            this.txtPrefix.Size = new System.Drawing.Size(150, 22);

            this.lblSuffix.Text = "后缀:";
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(410, 25);
            this.txtSuffix.Location = new System.Drawing.Point(450, 22);
            this.txtSuffix.Size = new System.Drawing.Size(150, 22);

            this.lblFind.Text = "查找:";
            this.lblFind.AutoSize = true;
            this.lblFind.Location = new System.Drawing.Point(200, 58);
            this.txtFind.Location = new System.Drawing.Point(240, 55);
            this.txtFind.Size = new System.Drawing.Size(150, 22);

            this.lblReplace.Text = "替换:";
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(410, 58);
            this.txtReplace.Location = new System.Drawing.Point(450, 55);
            this.txtReplace.Size = new System.Drawing.Size(150, 22);

            this.lblStartNum.Text = "起始编号:";
            this.lblStartNum.AutoSize = true;
            this.lblStartNum.Location = new System.Drawing.Point(200, 91);
            this.nudStartNum.Location = new System.Drawing.Point(270, 88);
            this.nudStartNum.Size = new System.Drawing.Size(70, 22);
            this.nudStartNum.Minimum = 0;
            this.nudStartNum.Maximum = 99999;
            this.nudStartNum.Value = 1;

            this.lblStep.Text = "步长:";
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(360, 91);
            this.nudStep.Location = new System.Drawing.Point(400, 88);
            this.nudStep.Size = new System.Drawing.Size(60, 22);
            this.nudStep.Minimum = 1;
            this.nudStep.Maximum = 100;
            this.nudStep.Value = 1;

            this.lblNumDigits.Text = "位数:";
            this.lblNumDigits.AutoSize = true;
            this.lblNumDigits.Location = new System.Drawing.Point(480, 91);
            this.nudNumDigits.Location = new System.Drawing.Point(520, 88);
            this.nudNumDigits.Size = new System.Drawing.Size(60, 22);
            this.nudNumDigits.Minimum = 1;
            this.nudNumDigits.Maximum = 10;
            this.nudNumDigits.Value = 3;

            this.panelPattern.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblRenameMode, this.cboRenameMode,
                this.lblPrefix, this.txtPrefix,
                this.lblSuffix, this.txtSuffix,
                this.lblFind, this.txtFind,
                this.lblReplace, this.txtReplace,
                this.lblStartNum, this.nudStartNum,
                this.lblStep, this.nudStep,
                this.lblNumDigits, this.nudNumDigits
            });

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer.SplitterDistance = 300;

            // lvFiles
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colOriginal, this.colNewName, this.colStatus
            });
            this.colOriginal.Text = "原文件名";
            this.colOriginal.Width = 300;
            this.colNewName.Text = "新文件名";
            this.colNewName.Width = 300;
            this.colStatus.Text = "状态";
            this.colStatus.Width = 100;

            this.lblPreview.Text = "预览: 尚未生成预览";
            this.lblPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPreview.Height = 20;
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.splitContainer.Panel1.Controls.Add(this.lblPreview);
            this.splitContainer.Panel1.Controls.Add(this.lvFiles);

            // panelBottom
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 50;
            this.panelBottom.Padding = new System.Windows.Forms.Padding(8);

            this.btnPreview.Text = "预览";
            this.btnPreview.Location = new System.Drawing.Point(8, 13);
            this.btnPreview.Size = new System.Drawing.Size(80, 28);
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);

            this.btnApply.Text = "执行重命名";
            this.btnApply.Location = new System.Drawing.Point(95, 13);
            this.btnApply.Size = new System.Drawing.Size(100, 28);
            this.btnApply.Enabled = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            this.btnUndo.Text = "撤销";
            this.btnUndo.Location = new System.Drawing.Point(202, 13);
            this.btnUndo.Size = new System.Drawing.Size(70, 28);
            this.btnUndo.Enabled = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);

            this.lblTotal.Text = "共 0 个文件";
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(300, 18);

            this.progressBar.Location = new System.Drawing.Point(450, 16);
            this.progressBar.Size = new System.Drawing.Size(250, 20);
            this.progressBar.Visible = false;

            this.panelBottom.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnPreview, this.btnApply, this.btnUndo, this.lblTotal, this.progressBar
            });

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 560);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelPattern);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Text = "文件批量改名工具";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelPattern.ResumeLayout(false);
            this.panelPattern.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumDigits)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.CheckBox chkSldprt;
        private System.Windows.Forms.CheckBox chkSldasm;
        private System.Windows.Forms.CheckBox chkSlddrw;
        private System.Windows.Forms.CheckBox chkAllTypes;
        private System.Windows.Forms.GroupBox panelPattern;
        private System.Windows.Forms.Label lblRenameMode;
        private System.Windows.Forms.ComboBox cboRenameMode;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label lblStartNum;
        private System.Windows.Forms.NumericUpDown nudStartNum;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.NumericUpDown nudStep;
        private System.Windows.Forms.Label lblNumDigits;
        private System.Windows.Forms.NumericUpDown nudNumDigits;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader colOriginal;
        private System.Windows.Forms.ColumnHeader colNewName;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
