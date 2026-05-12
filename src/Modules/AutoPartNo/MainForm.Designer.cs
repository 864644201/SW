namespace AutoPartNo
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

            // Top panel - controls
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnRuleSettings = new System.Windows.Forms.Button();
            this.chkRecursive = new System.Windows.Forms.CheckBox();
            this.lblRuleDisplay = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();

            // DataGridView
            this.dgvComponents = new System.Windows.Forms.DataGridView();
            this.colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrentPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNewPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // StatusStrip
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponents)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 90;
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelTop.Padding = new System.Windows.Forms.Padding(10, 8, 10, 5);

            this.btnScan.Text = "扫描装配体";
            this.btnScan.Location = new System.Drawing.Point(10, 10);
            this.btnScan.Size = new System.Drawing.Size(100, 32);
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);

            this.btnGenerate.Text = "生成预览";
            this.btnGenerate.Location = new System.Drawing.Point(120, 10);
            this.btnGenerate.Size = new System.Drawing.Size(100, 32);
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            this.btnApply.Text = "应用零件号";
            this.btnApply.Location = new System.Drawing.Point(230, 10);
            this.btnApply.Size = new System.Drawing.Size(100, 32);
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            this.btnRuleSettings.Text = "规则设置";
            this.btnRuleSettings.Location = new System.Drawing.Point(340, 10);
            this.btnRuleSettings.Size = new System.Drawing.Size(100, 32);
            this.btnRuleSettings.UseVisualStyleBackColor = true;
            this.btnRuleSettings.Click += new System.EventHandler(this.btnRuleSettings_Click);

            this.chkRecursive.Text = "递归扫描子装配体";
            this.chkRecursive.Location = new System.Drawing.Point(10, 52);
            this.chkRecursive.Size = new System.Drawing.Size(160, 23);
            this.chkRecursive.Checked = true;

            this.lblRuleDisplay.Text = "规则: MD-0001";
            this.lblRuleDisplay.Location = new System.Drawing.Point(180, 55);
            this.lblRuleDisplay.Size = new System.Drawing.Size(300, 20);
            this.lblRuleDisplay.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblRuleDisplay.Font = new System.Drawing.Font("Consolas", 9F);

            this.panelTop.Controls.Add(this.btnScan);
            this.panelTop.Controls.Add(this.btnGenerate);
            this.panelTop.Controls.Add(this.btnApply);
            this.panelTop.Controls.Add(this.btnRuleSettings);
            this.panelTop.Controls.Add(this.chkRecursive);
            this.panelTop.Controls.Add(this.lblRuleDisplay);
            this.panelTop.Controls.Add(this.lblStatus);

            // dgvComponents
            this.dgvComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComponents.AllowUserToAddRows = false;
            this.dgvComponents.AllowUserToDeleteRows = false;
            this.dgvComponents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvComponents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComponents.BackgroundColor = System.Drawing.Color.White;
            this.dgvComponents.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);

            this.dgvComponents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSelected, this.colName, this.colCurrentPartNo,
                this.colNewPartNo, this.colFilePath, this.colType
            });

            this.colSelected.HeaderText = "选择";
            this.colSelected.Name = "colSelected";
            this.colSelected.Width = 50;
            this.colSelected.TrueValue = true;
            this.colSelected.FalseValue = false;

            this.colName.HeaderText = "组件名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;

            this.colCurrentPartNo.HeaderText = "当前零件号";
            this.colCurrentPartNo.Name = "colCurrentPartNo";
            this.colCurrentPartNo.ReadOnly = true;

            this.colNewPartNo.HeaderText = "新零件号";
            this.colNewPartNo.Name = "colNewPartNo";
            this.colNewPartNo.ReadOnly = true;
            this.colNewPartNo.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkBlue;
            this.colNewPartNo.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);

            this.colFilePath.HeaderText = "文件路径";
            this.colFilePath.Name = "colFilePath";
            this.colFilePath.ReadOnly = true;

            this.colType.HeaderText = "类型";
            this.colType.Name = "colType";
            this.colType.Width = 70;
            this.colType.ReadOnly = true;

            // statusStrip
            this.statusStrip.Items.Add(this.toolStripStatusLabel);
            this.toolStripStatusLabel.Text = "就绪 - 请扫描装配体";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.dgvComponents);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Text = "麦豆宝 - 自动零件号生成器";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponents)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnRuleSettings;
        private System.Windows.Forms.CheckBox chkRecursive;
        private System.Windows.Forms.Label lblRuleDisplay;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dgvComponents;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrentPartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNewPartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}
