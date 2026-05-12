namespace BatchReplace
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Top controls
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.chkSubfolders = new System.Windows.Forms.CheckBox();
            this.lblFileCount = new System.Windows.Forms.Label();

            // Rules panel (left)
            this.panelRules = new System.Windows.Forms.Panel();
            this.lblRules = new System.Windows.Forms.Label();
            this.lstRules = new System.Windows.Forms.ListBox();
            this.panelRuleButtons = new System.Windows.Forms.Panel();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnEditRule = new System.Windows.Forms.Button();
            this.btnRemoveRule = new System.Windows.Forms.Button();

            // Files panel (middle)
            this.panelFiles = new System.Windows.Forms.Panel();
            this.lblFiles = new System.Windows.Forms.Label();
            this.lstFiles = new System.Windows.Forms.ListBox();

            // Results panel (right)
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAffected = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // Bottom controls
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();

            // Split containers
            this.splitContainer = new System.Windows.Forms.SplitContainer();

            this.panelTop.SuspendLayout();
            this.panelRules.SuspendLayout();
            this.panelRuleButtons.SuspendLayout();
            this.panelFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 55;
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelTop.Padding = new System.Windows.Forms.Padding(10, 8, 10, 5);

            this.btnSelectFiles.Text = "选择文件";
            this.btnSelectFiles.Location = new System.Drawing.Point(10, 12);
            this.btnSelectFiles.Size = new System.Drawing.Size(85, 30);
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);

            this.btnSelectFolder.Text = "选择文件夹";
            this.btnSelectFolder.Location = new System.Drawing.Point(105, 12);
            this.btnSelectFolder.Size = new System.Drawing.Size(85, 30);
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);

            this.txtFolder.Location = new System.Drawing.Point(200, 15);
            this.txtFolder.Size = new System.Drawing.Size(300, 23);
            this.txtFolder.ReadOnly = true;
            this.txtFolder.BackColor = System.Drawing.Color.White;

            this.chkSubfolders.Text = "包含子文件夹";
            this.chkSubfolders.Location = new System.Drawing.Point(510, 16);
            this.chkSubfolders.Size = new System.Drawing.Size(120, 23);
            this.chkSubfolders.Checked = true;

            this.lblFileCount.Text = "共 0 个文件";
            this.lblFileCount.Location = new System.Drawing.Point(640, 18);
            this.lblFileCount.Size = new System.Drawing.Size(120, 20);
            this.lblFileCount.ForeColor = System.Drawing.Color.DarkBlue;

            this.panelTop.Controls.Add(this.btnSelectFiles);
            this.panelTop.Controls.Add(this.btnSelectFolder);
            this.panelTop.Controls.Add(this.txtFolder);
            this.panelTop.Controls.Add(this.chkSubfolders);
            this.panelTop.Controls.Add(this.lblFileCount);

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.SplitterDistance = 280;
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;

            // Left panel: rules + files
            var panelLeft = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill };

            // panelRules
            this.panelRules.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRules.Height = 200;
            this.panelRules.Padding = new System.Windows.Forms.Padding(8);

            this.lblRules.Text = "替换规则";
            this.lblRules.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRules.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRules.Height = 25;

            this.lstRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRules.IntegralHeight = false;

            this.panelRuleButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRuleButtons.Height = 35;

            this.btnAddRule.Text = "添加";
            this.btnAddRule.Location = new System.Drawing.Point(5, 5);
            this.btnAddRule.Size = new System.Drawing.Size(60, 25);
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);

            this.btnEditRule.Text = "编辑";
            this.btnEditRule.Location = new System.Drawing.Point(70, 5);
            this.btnEditRule.Size = new System.Drawing.Size(60, 25);
            this.btnEditRule.Click += new System.EventHandler(this.btnEditRule_Click);

            this.btnRemoveRule.Text = "删除";
            this.btnRemoveRule.Location = new System.Drawing.Point(135, 5);
            this.btnRemoveRule.Size = new System.Drawing.Size(60, 25);
            this.btnRemoveRule.Click += new System.EventHandler(this.btnRemoveRule_Click);

            this.panelRuleButtons.Controls.Add(this.btnAddRule);
            this.panelRuleButtons.Controls.Add(this.btnEditRule);
            this.panelRuleButtons.Controls.Add(this.btnRemoveRule);

            this.panelRules.Controls.Add(this.lstRules);
            this.panelRules.Controls.Add(this.lblRules);
            this.panelRules.Controls.Add(this.panelRuleButtons);

            // panelFiles
            this.panelFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiles.Padding = new System.Windows.Forms.Padding(8);

            this.lblFiles.Text = "目标文件";
            this.lblFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFiles.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFiles.Height = 25;

            this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFiles.IntegralHeight = false;

            this.panelFiles.Controls.Add(this.lstFiles);
            this.panelFiles.Controls.Add(this.lblFiles);

            panelLeft.Controls.Add(this.panelFiles);
            panelLeft.Controls.Add(this.panelRules);

            this.splitContainer.Panel1.Controls.Add(panelLeft);

            // Right panel: results
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ReadOnly = true;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;

            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colFileName, this.colStatus, this.colAffected, this.colDuration, this.colMessage
            });

            this.colFileName.HeaderText = "文件名";
            this.colFileName.Name = "colFileName";
            this.colFileName.Width = 150;

            this.colStatus.HeaderText = "状态";
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 55;

            this.colAffected.HeaderText = "处理数";
            this.colAffected.Name = "colAffected";
            this.colAffected.Width = 60;

            this.colDuration.HeaderText = "耗时";
            this.colDuration.Name = "colDuration";
            this.colDuration.Width = 65;

            this.colMessage.HeaderText = "消息";
            this.colMessage.Name = "colMessage";
            this.colMessage.FillWeight = 150;

            this.splitContainer.Panel2.Controls.Add(this.dgvResults);

            // panelBottom
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 50;
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.panelBottom.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);

            this.btnExecute.Text = "执行替换";
            this.btnExecute.Location = new System.Drawing.Point(10, 10);
            this.btnExecute.Size = new System.Drawing.Size(100, 30);
            this.btnExecute.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnExecute.ForeColor = System.Drawing.Color.White;
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);

            this.btnCancel.Text = "取消";
            this.btnCancel.Location = new System.Drawing.Point(120, 10);
            this.btnCancel.Size = new System.Drawing.Size(70, 30);
            this.btnCancel.Enabled = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.progressBar.Location = new System.Drawing.Point(210, 14);
            this.progressBar.Size = new System.Drawing.Size(300, 22);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            this.lblPercent.Location = new System.Drawing.Point(520, 16);
            this.lblPercent.Size = new System.Drawing.Size(50, 20);
            this.lblPercent.Text = "0%";

            this.lblStatus.Location = new System.Drawing.Point(580, 16);
            this.lblStatus.Size = new System.Drawing.Size(300, 20);
            this.lblStatus.Text = "就绪";

            this.panelBottom.Controls.Add(this.btnExecute);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.progressBar);
            this.panelBottom.Controls.Add(this.lblPercent);
            this.panelBottom.Controls.Add(this.lblStatus);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Text = "麦豆宝 - 批量替换工具";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelRules.ResumeLayout(false);
            this.panelRuleButtons.ResumeLayout(false);
            this.panelFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.CheckBox chkSubfolders;
        private System.Windows.Forms.Label lblFileCount;

        private System.Windows.Forms.Panel panelRules;
        private System.Windows.Forms.Label lblRules;
        private System.Windows.Forms.ListBox lstRules;
        private System.Windows.Forms.Panel panelRuleButtons;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnEditRule;
        private System.Windows.Forms.Button btnRemoveRule;

        private System.Windows.Forms.Panel panelFiles;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.ListBox lstFiles;

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAffected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblStatus;
    }
}
