namespace PropertyEditingTool
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.grpFiles = new System.Windows.Forms.GroupBox();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.panelFileBtns = new System.Windows.Forms.Panel();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.btnLoadAll = new System.Windows.Forms.Button();
            this.grpFileProps = new System.Windows.Forms.GroupBox();
            this.dgvFileProps = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpBatch = new System.Windows.Forms.GroupBox();
            this.dgvBatch = new System.Windows.Forms.DataGridView();
            this.colBatchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBatchValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBatchCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.grpFindReplace = new System.Windows.Forms.GroupBox();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.btnFindReplace = new System.Windows.Forms.Button();
            this.lblFind = new System.Windows.Forms.Label();
            this.lblReplace = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnApplyBatch = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grpFiles.SuspendLayout();
            this.panelFileBtns.SuspendLayout();
            this.grpFileProps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileProps)).BeginInit();
            this.grpBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatch)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.grpFindReplace.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();

            // splitContainer1 - 主分割
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.SplitterDistance = 250;

            // splitContainer1.Panel1 - 文件列表 + 文件属性
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // splitContainer1.Panel2 - 批量编辑 + 底部操作
            this.splitContainer1.Panel2.Controls.Add(this.grpBatch);
            this.splitContainer1.Panel2.Controls.Add(this.panelBottom);

            // splitContainer2 - 文件列表和文件属性并列
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.SplitterDistance = 280;
            this.splitContainer2.Panel1.Controls.Add(this.grpFiles);
            this.splitContainer2.Panel2.Controls.Add(this.grpFileProps);

            // grpFiles
            this.grpFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFiles.Text = "文件列表 (支持拖放)";
            this.grpFiles.Padding = new System.Windows.Forms.Padding(6);
            this.grpFiles.Controls.Add(this.lstFiles);
            this.grpFiles.Controls.Add(this.panelFileBtns);

            // lstFiles
            this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFiles.IntegralHeight = false;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);

            // panelFileBtns
            this.panelFileBtns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFileBtns.Height = 36;
            this.panelFileBtns.Controls.Add(this.btnLoadAll);
            this.panelFileBtns.Controls.Add(this.btnClearFiles);
            this.panelFileBtns.Controls.Add(this.btnAddFiles);

            this.btnAddFiles.Text = "添加文件";
            this.btnAddFiles.Size = new System.Drawing.Size(80, 30);
            this.btnAddFiles.Location = new System.Drawing.Point(4, 3);
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);

            this.btnClearFiles.Text = "清空";
            this.btnClearFiles.Size = new System.Drawing.Size(60, 30);
            this.btnClearFiles.Location = new System.Drawing.Point(88, 3);
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);

            this.btnLoadAll.Text = "加载所有属性";
            this.btnLoadAll.Size = new System.Drawing.Size(110, 30);
            this.btnLoadAll.Location = new System.Drawing.Point(152, 3);
            this.btnLoadAll.Click += new System.EventHandler(this.btnLoadAll_Click);

            // grpFileProps
            this.grpFileProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFileProps.Text = "当前文件属性";
            this.grpFileProps.Padding = new System.Windows.Forms.Padding(6);
            this.grpFileProps.Controls.Add(this.dgvFileProps);

            // dgvFileProps
            this.dgvFileProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileProps.AllowUserToAddRows = false;
            this.dgvFileProps.AllowUserToDeleteRows = false;
            this.dgvFileProps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFileProps.ReadOnly = true;
            this.dgvFileProps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2 });

            this.dataGridViewTextBoxColumn1.HeaderText = "属性名称";
            this.dataGridViewTextBoxColumn1.FillWeight = 40;
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "属性值";
            this.dataGridViewTextBoxColumn2.FillWeight = 60;
            this.dataGridViewTextBoxColumn2.ReadOnly = true;

            // grpBatch
            this.grpBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBatch.Text = "批量属性编辑 (勾选要写入的属性)";
            this.grpBatch.Padding = new System.Windows.Forms.Padding(6);
            this.grpBatch.Controls.Add(this.dgvBatch);

            // dgvBatch
            this.dgvBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBatch.AllowUserToAddRows = false;
            this.dgvBatch.AllowUserToDeleteRows = false;
            this.dgvBatch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colBatchName, this.colBatchValue, this.colBatchCheck });

            this.colBatchName.HeaderText = "属性名称";
            this.colBatchName.FillWeight = 30;
            this.colBatchName.ReadOnly = true;
            this.colBatchValue.HeaderText = "属性值";
            this.colBatchValue.FillWeight = 50;
            this.colBatchCheck.HeaderText = "写入";
            this.colBatchCheck.FillWeight = 20;

            // panelBottom
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 80;
            this.panelBottom.Controls.Add(this.grpFindReplace);
            this.panelBottom.Controls.Add(this.panelActions);

            // grpFindReplace
            this.grpFindReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFindReplace.Text = "查找替换";
            this.grpFindReplace.Padding = new System.Windows.Forms.Padding(6);
            this.grpFindReplace.Controls.Add(this.lblFind);
            this.grpFindReplace.Controls.Add(this.txtFind);
            this.grpFindReplace.Controls.Add(this.lblReplace);
            this.grpFindReplace.Controls.Add(this.txtReplace);
            this.grpFindReplace.Controls.Add(this.btnFindReplace);

            this.lblFind.Text = "查找:";
            this.lblFind.Location = new System.Drawing.Point(10, 24);
            this.lblFind.AutoSize = true;

            this.txtFind.Location = new System.Drawing.Point(55, 21);
            this.txtFind.Size = new System.Drawing.Size(150, 22);

            this.lblReplace.Text = "替换:";
            this.lblReplace.Location = new System.Drawing.Point(215, 24);
            this.lblReplace.AutoSize = true;

            this.txtReplace.Location = new System.Drawing.Point(260, 21);
            this.txtReplace.Size = new System.Drawing.Size(150, 22);

            this.btnFindReplace.Text = "执行替换";
            this.btnFindReplace.Location = new System.Drawing.Point(420, 19);
            this.btnFindReplace.Size = new System.Drawing.Size(90, 28);
            this.btnFindReplace.Click += new System.EventHandler(this.btnFindReplace_Click);

            // panelActions
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelActions.Width = 320;
            this.panelActions.Controls.Add(this.btnApplyBatch);
            this.panelActions.Controls.Add(this.btnExportExcel);
            this.panelActions.Controls.Add(this.btnImportExcel);

            this.btnApplyBatch.Text = "批量写入选中属性";
            this.btnApplyBatch.Size = new System.Drawing.Size(140, 32);
            this.btnApplyBatch.Location = new System.Drawing.Point(10, 8);
            this.btnApplyBatch.Click += new System.EventHandler(this.btnApplyBatch_Click);

            this.btnExportExcel.Text = "导出 CSV";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 32);
            this.btnExportExcel.Location = new System.Drawing.Point(10, 44);
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            this.btnImportExcel.Text = "导入 CSV";
            this.btnImportExcel.Size = new System.Drawing.Size(100, 32);
            this.btnImportExcel.Location = new System.Drawing.Point(114, 44);
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);

            // MainForm
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.splitContainer1);
            this.Text = "PropertyEditingTool - 批量属性编辑";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.grpFiles.ResumeLayout(false);
            this.panelFileBtns.ResumeLayout(false);
            this.grpFileProps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileProps)).EndInit();
            this.grpBatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatch)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.grpFindReplace.ResumeLayout(false);
            this.grpFindReplace.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox grpFiles;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Panel panelFileBtns;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.Button btnLoadAll;
        private System.Windows.Forms.GroupBox grpFileProps;
        private System.Windows.Forms.DataGridView dgvFileProps;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.GroupBox grpBatch;
        private System.Windows.Forms.DataGridView dgvBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBatchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBatchValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colBatchCheck;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.GroupBox grpFindReplace;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Button btnFindReplace;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnApplyBatch;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnImportExcel;
    }
}
