namespace BatchConvertPDF
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
            this.lblSource = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();

            this.panelOptions = new System.Windows.Forms.GroupBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.chkIncludeSubfolders = new System.Windows.Forms.CheckBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.chkSldprt = new System.Windows.Forms.CheckBox();
            this.chkSldasm = new System.Windows.Forms.CheckBox();
            this.chkSlddrw = new System.Windows.Forms.CheckBox();

            this.panelFiles = new System.Windows.Forms.Panel();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.colFileName = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.lblFileCount = new System.Windows.Forms.Label();

            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();

            this.panelTop.SuspendLayout();
            this.panelOptions.SuspendLayout();
            this.panelFiles.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 80;
            this.panelTop.Padding = new System.Windows.Forms.Padding(8);
            this.panelTop.Controls.Add(this.lblSource);
            this.panelTop.Controls.Add(this.txtSource);
            this.panelTop.Controls.Add(this.btnBrowseSource);
            this.panelTop.Controls.Add(this.lblOutput);
            this.panelTop.Controls.Add(this.txtOutput);
            this.panelTop.Controls.Add(this.btnBrowseOutput);

            this.lblSource.Text = "源文件夹:";
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(8, 14);
            this.txtSource.Location = new System.Drawing.Point(80, 11);
            this.txtSource.Size = new System.Drawing.Size(480, 22);
            this.txtSource.ReadOnly = true;
            this.btnBrowseSource.Text = "浏览...";
            this.btnBrowseSource.Location = new System.Drawing.Point(565, 10);
            this.btnBrowseSource.Size = new System.Drawing.Size(70, 24);
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);

            this.lblOutput.Text = "输出目录:";
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(8, 45);
            this.txtOutput.Location = new System.Drawing.Point(80, 42);
            this.txtOutput.Size = new System.Drawing.Size(480, 22);
            this.txtOutput.ReadOnly = true;
            this.btnBrowseOutput.Text = "浏览...";
            this.btnBrowseOutput.Location = new System.Drawing.Point(565, 41);
            this.btnBrowseOutput.Size = new System.Drawing.Size(70, 24);
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);

            // panelOptions
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOptions.Height = 90;
            this.panelOptions.Padding = new System.Windows.Forms.Padding(8);
            this.panelOptions.Text = "转换选项";
            this.panelOptions.Controls.Add(this.lblFormat);
            this.panelOptions.Controls.Add(this.cboFormat);
            this.panelOptions.Controls.Add(this.chkIncludeSubfolders);
            this.panelOptions.Controls.Add(this.chkOverwrite);
            this.panelOptions.Controls.Add(this.lblFilter);
            this.panelOptions.Controls.Add(this.chkSldprt);
            this.panelOptions.Controls.Add(this.chkSldasm);
            this.panelOptions.Controls.Add(this.chkSlddrw);

            this.lblFormat.Text = "输出格式:";
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(15, 25);
            this.cboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormat.Items.AddRange(new object[] { "PDF", "DWG", "DXF", "STEP", "IGES" });
            this.cboFormat.SelectedIndex = 0;
            this.cboFormat.Location = new System.Drawing.Point(85, 22);
            this.cboFormat.Size = new System.Drawing.Size(100, 21);

            this.chkIncludeSubfolders.Text = "包含子文件夹";
            this.chkIncludeSubfolders.AutoSize = true;
            this.chkIncludeSubfolders.Location = new System.Drawing.Point(210, 24);

            this.chkOverwrite.Text = "覆盖已有文件";
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Location = new System.Drawing.Point(330, 24);

            this.lblFilter.Text = "源类型:";
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(15, 55);
            this.chkSldprt.Text = ".sldprt";
            this.chkSldprt.AutoSize = true;
            this.chkSldprt.Checked = true;
            this.chkSldprt.Location = new System.Drawing.Point(85, 54);
            this.chkSldasm.Text = ".sldasm";
            this.chkSldasm.AutoSize = true;
            this.chkSldasm.Checked = true;
            this.chkSldasm.Location = new System.Drawing.Point(160, 54);
            this.chkSlddrw.Text = ".slddrw";
            this.chkSlddrw.AutoSize = true;
            this.chkSlddrw.Checked = true;
            this.chkSlddrw.Location = new System.Drawing.Point(235, 54);

            // panelFiles
            this.panelFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiles.Controls.Add(this.lvFiles);
            this.panelFiles.Controls.Add(this.lblFileCount);

            this.lblFileCount.Text = "共 0 个文件";
            this.lblFileCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFileCount.Height = 20;
            this.lblFileCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colFileName, this.colSize, this.colStatus
            });
            this.colFileName.Text = "文件名";
            this.colFileName.Width = 400;
            this.colSize.Text = "大小";
            this.colSize.Width = 100;
            this.colStatus.Text = "状态";
            this.colStatus.Width = 120;

            // panelBottom
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 120;
            this.panelBottom.Padding = new System.Windows.Forms.Padding(8);
            this.panelBottom.Controls.Add(this.btnConvert);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.progressBar);
            this.panelBottom.Controls.Add(this.lblProgress);
            this.panelBottom.Controls.Add(this.txtLog);

            this.btnConvert.Text = "开始转换";
            this.btnConvert.Location = new System.Drawing.Point(8, 8);
            this.btnConvert.Size = new System.Drawing.Size(100, 28);
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);

            this.btnCancel.Text = "取消";
            this.btnCancel.Location = new System.Drawing.Point(115, 8);
            this.btnCancel.Size = new System.Drawing.Size(70, 28);
            this.btnCancel.Enabled = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.progressBar.Location = new System.Drawing.Point(200, 12);
            this.progressBar.Size = new System.Drawing.Size(350, 20);

            this.lblProgress.Text = "";
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(560, 14);

            this.txtLog.Location = new System.Drawing.Point(8, 42);
            this.txtLog.Size = new System.Drawing.Size(700, 70);
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 560);
            this.Controls.Add(this.panelFiles);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Text = "文件格式批量转换工具";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelOptions.ResumeLayout(false);
            this.panelOptions.PerformLayout();
            this.panelFiles.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.GroupBox panelOptions;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.ComboBox cboFormat;
        private System.Windows.Forms.CheckBox chkIncludeSubfolders;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.CheckBox chkSldprt;
        private System.Windows.Forms.CheckBox chkSldasm;
        private System.Windows.Forms.CheckBox chkSlddrw;
        private System.Windows.Forms.Panel panelFiles;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtLog;
    }
}
