namespace BatchDetail
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

        private void InitializeComponent()
        {
            // Top panel
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.chkSubdirs = new System.Windows.Forms.CheckBox();
            this.lblFileCount = new System.Windows.Forms.Label();

            // File list
            this.lblFiles = new System.Windows.Forms.Label();
            this.lstFiles = new System.Windows.Forms.ListBox();

            // Extract button
            this.btnExtract = new System.Windows.Forms.Button();

            // DataGridView for BOM
            this.dgvBom = new System.Windows.Forms.DataGridView();
            this.colPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // Export panel
            this.panelExport = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.chkOpenAfterExport = new System.Windows.Forms.CheckBox();

            // Log & Status
            this.lstLog = new System.Windows.Forms.ListBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();

            // Split container
            this.splitContainer = new System.Windows.Forms.SplitContainer();

            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).BeginInit();
            this.panelExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 50;
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelTop.Padding = new System.Windows.Forms.Padding(10, 8, 10, 5);

            this.btnAddFiles.Text = "添加文件";
            this.btnAddFiles.Location = new System.Drawing.Point(10, 10);
            this.btnAddFiles.Size = new System.Drawing.Size(80, 30);
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);

            this.btnAddFolder.Text = "添加文件夹";
            this.btnAddFolder.Location = new System.Drawing.Point(100, 10);
            this.btnAddFolder.Size = new System.Drawing.Size(90, 30);
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);

            this.btnRemoveSelected.Text = "移除选中";
            this.btnRemoveSelected.Location = new System.Drawing.Point(200, 10);
            this.btnRemoveSelected.Size = new System.Drawing.Size(80, 30);
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);

            this.btnClearFiles.Text = "清空列表";
            this.btnClearFiles.Location = new System.Drawing.Point(290, 10);
            this.btnClearFiles.Size = new System.Drawing.Size(80, 30);
            this.btnClearFiles.UseVisualStyleBackColor = true;
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);

            this.chkSubdirs.Text = "包含子文件夹";
            this.chkSubdirs.Location = new System.Drawing.Point(385, 14);
            this.chkSubdirs.Size = new System.Drawing.Size(110, 23);
            this.chkSubdirs.Checked = true;

            this.lblFileCount.Text = "共 0 个装配体";
            this.lblFileCount.Location = new System.Drawing.Point(505, 16);
            this.lblFileCount.Size = new System.Drawing.Size(150, 20);
            this.lblFileCount.ForeColor = System.Drawing.Color.DarkBlue;

            this.panelTop.Controls.Add(this.btnAddFiles);
            this.panelTop.Controls.Add(this.btnAddFolder);
            this.panelTop.Controls.Add(this.btnRemoveSelected);
            this.panelTop.Controls.Add(this.btnClearFiles);
            this.panelTop.Controls.Add(this.chkSubdirs);
            this.panelTop.Controls.Add(this.lblFileCount);

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer.SplitterDistance = 160;

            // Top half: file list + extract button
            this.lblFiles.Text = "装配体文件列表:";
            this.lblFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFiles.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiles.Height = 22;
            this.lblFiles.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);

            this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFiles.IntegralHeight = false;

            this.btnExtract.Text = "开始提取 BOM";
            this.btnExtract.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExtract.Height = 35;
            this.btnExtract.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnExtract.ForeColor = System.Drawing.Color.White;
            this.btnExtract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtract.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);

            this.splitContainer.Panel1.Controls.Add(this.lstFiles);
            this.splitContainer.Panel1.Controls.Add(this.lblFiles);
            this.splitContainer.Panel1.Controls.Add(this.btnExtract);

            // Bottom half: BOM grid + export
            this.dgvBom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBom.AllowUserToAddRows = false;
            this.dgvBom.AllowUserToDeleteRows = false;
            this.dgvBom.ReadOnly = true;
            this.dgvBom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBom.BackgroundColor = System.Drawing.Color.White;
            this.dgvBom.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);

            this.dgvBom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colPartNo, this.colName, this.colMaterial,
                this.colQuantity, this.colWeight, this.colFileName
            });

            this.colPartNo.HeaderText = "零件号";
            this.colPartNo.Name = "colPartNo";
            this.colPartNo.Width = 120;

            this.colName.HeaderText = "名称";
            this.colName.Name = "colName";
            this.colName.FillWeight = 120;

            this.colMaterial.HeaderText = "材料";
            this.colMaterial.Name = "colMaterial";
            this.colMaterial.Width = 100;

            this.colQuantity.HeaderText = "数量";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 60;

            this.colWeight.HeaderText = "重量(kg)";
            this.colWeight.Name = "colWeight";
            this.colWeight.Width = 80;

            this.colFileName.HeaderText = "文件名";
            this.colFileName.Name = "colFileName";
            this.colFileName.FillWeight = 80;

            // panelExport
            this.panelExport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelExport.Height = 45;
            this.panelExport.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.panelExport.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);

            this.btnExportExcel.Text = "导出 Excel";
            this.btnExportExcel.Location = new System.Drawing.Point(10, 7);
            this.btnExportExcel.Size = new System.Drawing.Size(100, 30);
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(46, 139, 87);
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            this.btnExportCsv.Text = "导出 CSV";
            this.btnExportCsv.Location = new System.Drawing.Point(120, 7);
            this.btnExportCsv.Size = new System.Drawing.Size(100, 30);
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);

            this.chkOpenAfterExport.Text = "导出后自动打开";
            this.chkOpenAfterExport.Location = new System.Drawing.Point(240, 10);
            this.chkOpenAfterExport.Size = new System.Drawing.Size(130, 23);
            this.chkOpenAfterExport.Checked = true;

            this.panelExport.Controls.Add(this.btnExportExcel);
            this.panelExport.Controls.Add(this.btnExportCsv);
            this.panelExport.Controls.Add(this.chkOpenAfterExport);

            this.splitContainer.Panel2.Controls.Add(this.dgvBom);
            this.splitContainer.Panel2.Controls.Add(this.panelExport);

            // lstLog
            this.lblLog.Text = "日志:";
            this.lblLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLog.Height = 20;
            this.lblLog.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lblLog.ForeColor = System.Drawing.Color.Gray;
            this.lblLog.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);

            this.lstLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstLog.Height = 80;
            this.lstLog.IntegralHeight = false;
            this.lstLog.Font = new System.Drawing.Font("Consolas", 8F);

            // progressBar & lblStatus
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Height = 18;

            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Text = "就绪 - 请添加装配体文件";
            this.lblStatus.Height = 22;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkBlue;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Text = "麦豆宝 - 批量明细提取";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).EndInit();
            this.panelExport.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.CheckBox chkSubdirs;
        private System.Windows.Forms.Label lblFileCount;

        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnExtract;

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvBom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;

        private System.Windows.Forms.Panel panelExport;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.CheckBox chkOpenAfterExport;

        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
    }
}
