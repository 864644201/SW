namespace ReplaceDrawingTemplate
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

            this.panelTemplate = new System.Windows.Forms.GroupBox();
            this.lblOldTemplate = new System.Windows.Forms.Label();
            this.txtOldTemplate = new System.Windows.Forms.TextBox();
            this.btnBrowseOld = new System.Windows.Forms.Button();
            this.lblNewTemplate = new System.Windows.Forms.Label();
            this.txtNewTemplate = new System.Windows.Forms.TextBox();
            this.btnBrowseNew = new System.Windows.Forms.Button();
            this.lblOldInfo = new System.Windows.Forms.Label();
            this.lblNewInfo = new System.Windows.Forms.Label();

            this.panelFiles = new System.Windows.Forms.GroupBox();
            this.btnBrowseFiles = new System.Windows.Forms.Button();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.colFileName = new System.Windows.Forms.ColumnHeader();
            this.colFilePath = new System.Windows.Forms.ColumnHeader();
            this.colCurrentTemplate = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.lblFileCount = new System.Windows.Forms.Label();

            this.panelPreview = new System.Windows.Forms.GroupBox();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.btnPreview = new System.Windows.Forms.Button();

            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnApplySelected = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();

            this.panelTemplate.SuspendLayout();
            this.panelFiles.SuspendLayout();
            this.panelPreview.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // panelTemplate
            this.panelTemplate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTemplate.Height = 120;
            this.panelTemplate.Padding = new System.Windows.Forms.Padding(8);
            this.panelTemplate.Text = "模板设置";
            this.panelTemplate.Controls.Add(this.lblOldTemplate);
            this.panelTemplate.Controls.Add(this.txtOldTemplate);
            this.panelTemplate.Controls.Add(this.btnBrowseOld);
            this.panelTemplate.Controls.Add(this.lblNewTemplate);
            this.panelTemplate.Controls.Add(this.txtNewTemplate);
            this.panelTemplate.Controls.Add(this.btnBrowseNew);
            this.panelTemplate.Controls.Add(this.lblOldInfo);
            this.panelTemplate.Controls.Add(this.lblNewInfo);

            this.lblOldTemplate.Text = "旧模板:";
            this.lblOldTemplate.AutoSize = true;
            this.lblOldTemplate.Location = new System.Drawing.Point(15, 28);
            this.txtOldTemplate.Location = new System.Drawing.Point(80, 25);
            this.txtOldTemplate.Size = new System.Drawing.Size(450, 22);
            this.txtOldTemplate.ReadOnly = true;
            this.btnBrowseOld.Text = "浏览...";
            this.btnBrowseOld.Location = new System.Drawing.Point(535, 24);
            this.btnBrowseOld.Size = new System.Drawing.Size(70, 24);
            this.btnBrowseOld.Click += new System.EventHandler(this.btnBrowseOld_Click);
            this.lblOldInfo.Text = "";
            this.lblOldInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblOldInfo.AutoSize = true;
            this.lblOldInfo.Location = new System.Drawing.Point(80, 50);

            this.lblNewTemplate.Text = "新模板:";
            this.lblNewTemplate.AutoSize = true;
            this.lblNewTemplate.Location = new System.Drawing.Point(15, 75);
            this.txtNewTemplate.Location = new System.Drawing.Point(80, 72);
            this.txtNewTemplate.Size = new System.Drawing.Size(450, 22);
            this.txtNewTemplate.ReadOnly = true;
            this.btnBrowseNew.Text = "浏览...";
            this.btnBrowseNew.Location = new System.Drawing.Point(535, 71);
            this.btnBrowseNew.Size = new System.Drawing.Size(70, 24);
            this.btnBrowseNew.Click += new System.EventHandler(this.btnBrowseNew_Click);
            this.lblNewInfo.Text = "";
            this.lblNewInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblNewInfo.AutoSize = true;
            this.lblNewInfo.Location = new System.Drawing.Point(80, 97);

            // panelFiles
            this.panelFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiles.Padding = new System.Windows.Forms.Padding(8);
            this.panelFiles.Text = "目标文件列表";

            this.btnBrowseFiles.Text = "添加文件";
            this.btnBrowseFiles.Location = new System.Drawing.Point(15, 22);
            this.btnBrowseFiles.Size = new System.Drawing.Size(80, 24);
            this.btnBrowseFiles.Click += new System.EventHandler(this.btnBrowseFiles_Click);

            this.btnAddFolder.Text = "添加文件夹";
            this.btnAddFolder.Location = new System.Drawing.Point(100, 22);
            this.btnAddFolder.Size = new System.Drawing.Size(90, 24);
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);

            this.btnClearFiles.Text = "清空";
            this.btnClearFiles.Location = new System.Drawing.Point(195, 22);
            this.btnClearFiles.Size = new System.Drawing.Size(60, 24);
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);

            this.lblFileCount.Text = "共 0 个文件";
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Location = new System.Drawing.Point(270, 26);

            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvFiles.Location = new System.Drawing.Point(8, 50);
            this.lvFiles.Size = new System.Drawing.Size(684, 250);
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.CheckBoxes = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colFileName, this.colFilePath, this.colCurrentTemplate, this.colStatus
            });
            this.colFileName.Text = "文件名";
            this.colFileName.Width = 150;
            this.colFilePath.Text = "路径";
            this.colFilePath.Width = 250;
            this.colCurrentTemplate.Text = "当前模板";
            this.colCurrentTemplate.Width = 150;
            this.colStatus.Text = "状态";
            this.colStatus.Width = 100;

            this.panelFiles.Controls.Add(this.btnBrowseFiles);
            this.panelFiles.Controls.Add(this.btnAddFolder);
            this.panelFiles.Controls.Add(this.btnClearFiles);
            this.panelFiles.Controls.Add(this.lblFileCount);

            // panelPreview
            this.panelPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPreview.Height = 100;
            this.panelPreview.Padding = new System.Windows.Forms.Padding(8);
            this.panelPreview.Text = "操作日志";
            this.panelPreview.Controls.Add(this.txtPreview);
            this.panelPreview.Controls.Add(this.btnPreview);

            this.btnPreview.Text = "预览差异";
            this.btnPreview.Location = new System.Drawing.Point(15, 22);
            this.btnPreview.Size = new System.Drawing.Size(80, 24);
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);

            this.txtPreview.Location = new System.Drawing.Point(8, 50);
            this.txtPreview.Size = new System.Drawing.Size(684, 42);
            this.txtPreview.Multiline = true;
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // panelBottom
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 50;
            this.panelBottom.Padding = new System.Windows.Forms.Padding(8);
            this.panelBottom.Controls.Add(this.btnApply);
            this.panelBottom.Controls.Add(this.btnApplySelected);
            this.panelBottom.Controls.Add(this.progressBar);
            this.panelBottom.Controls.Add(this.lblStatus);

            this.btnApply.Text = "批量替换";
            this.btnApply.Location = new System.Drawing.Point(8, 13);
            this.btnApply.Size = new System.Drawing.Size(100, 28);
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            this.btnApplySelected.Text = "替换选中";
            this.btnApplySelected.Location = new System.Drawing.Point(115, 13);
            this.btnApplySelected.Size = new System.Drawing.Size(90, 28);
            this.btnApplySelected.Click += new System.EventHandler(this.btnApplySelected_Click);

            this.progressBar.Location = new System.Drawing.Point(220, 16);
            this.progressBar.Size = new System.Drawing.Size(300, 20);
            this.progressBar.Visible = false;

            this.lblStatus.Text = "就绪";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(530, 18);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 600);
            this.Controls.Add(this.panelFiles);
            this.Controls.Add(this.panelTemplate);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.panelBottom);
            this.Text = "批量替换图纸模板工具";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelTemplate.ResumeLayout(false);
            this.panelTemplate.PerformLayout();
            this.panelFiles.ResumeLayout(false);
            this.panelFiles.PerformLayout();
            this.panelPreview.ResumeLayout(false);
            this.panelPreview.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox panelTemplate;
        private System.Windows.Forms.Label lblOldTemplate;
        private System.Windows.Forms.TextBox txtOldTemplate;
        private System.Windows.Forms.Button btnBrowseOld;
        private System.Windows.Forms.Label lblNewTemplate;
        private System.Windows.Forms.TextBox txtNewTemplate;
        private System.Windows.Forms.Button btnBrowseNew;
        private System.Windows.Forms.Label lblOldInfo;
        private System.Windows.Forms.Label lblNewInfo;
        private System.Windows.Forms.GroupBox panelFiles;
        private System.Windows.Forms.Button btnBrowseFiles;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colFilePath;
        private System.Windows.Forms.ColumnHeader colCurrentTemplate;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.GroupBox panelPreview;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnApplySelected;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
    }
}
