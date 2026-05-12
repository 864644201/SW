namespace PropertyCardV2
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
            this.grpFiles = new System.Windows.Forms.GroupBox();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.dgvProperties = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnRemoveRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnLoadTemplate = new System.Windows.Forms.Button();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.btnLoadProperties = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpFiles.SuspendLayout();
            this.grpProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).BeginInit();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.SplitterDistance = 250;
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;

            // splitContainer.Panel1 - 文件列表
            this.splitContainer.Panel1.Controls.Add(this.grpFiles);
            this.grpFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFiles.Text = "文件列表";
            this.grpFiles.Padding = new System.Windows.Forms.Padding(8);

            this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFiles.IntegralHeight = false;
            this.lstFiles.Location = new System.Drawing.Point(8, 22);
            this.lstFiles.Size = new System.Drawing.Size(234, 300);

            this.btnClearFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClearFiles.Text = "清空文件";
            this.btnClearFiles.Height = 32;
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);

            this.btnAddFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddFiles.Text = "添加文件";
            this.btnAddFiles.Height = 32;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);

            this.grpFiles.Controls.Add(this.lstFiles);
            this.grpFiles.Controls.Add(this.btnClearFiles);
            this.grpFiles.Controls.Add(this.btnAddFiles);

            // splitContainer.Panel2 - 属性编辑
            this.splitContainer.Panel2.Controls.Add(this.dgvProperties);
            this.splitContainer.Panel2.Controls.Add(this.panelActions);

            // panelActions
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Height = 80;

            this.btnApply.Text = "批量应用";
            this.btnApply.Size = new System.Drawing.Size(100, 32);
            this.btnApply.Location = new System.Drawing.Point(8, 8);
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            this.btnLoadProperties.Text = "读取属性";
            this.btnLoadProperties.Size = new System.Drawing.Size(100, 32);
            this.btnLoadProperties.Location = new System.Drawing.Point(114, 8);
            this.btnLoadProperties.Click += new System.EventHandler(this.btnLoadProperties_Click);

            this.btnSaveTemplate.Text = "保存模板";
            this.btnSaveTemplate.Size = new System.Drawing.Size(100, 32);
            this.btnSaveTemplate.Location = new System.Drawing.Point(220, 8);
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);

            this.btnLoadTemplate.Text = "加载模板";
            this.btnLoadTemplate.Size = new System.Drawing.Size(100, 32);
            this.btnLoadTemplate.Location = new System.Drawing.Point(326, 8);
            this.btnLoadTemplate.Click += new System.EventHandler(this.btnLoadTemplate_Click);

            this.btnAddRow.Text = "添加属性";
            this.btnAddRow.Size = new System.Drawing.Size(100, 32);
            this.btnAddRow.Location = new System.Drawing.Point(8, 44);
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);

            this.btnRemoveRow.Text = "删除属性";
            this.btnRemoveRow.Size = new System.Drawing.Size(100, 32);
            this.btnRemoveRow.Location = new System.Drawing.Point(114, 44);
            this.btnRemoveRow.Click += new System.EventHandler(this.btnRemoveRow_Click);

            this.panelActions.Controls.Add(this.btnApply);
            this.panelActions.Controls.Add(this.btnLoadProperties);
            this.panelActions.Controls.Add(this.btnSaveTemplate);
            this.panelActions.Controls.Add(this.btnLoadTemplate);
            this.panelActions.Controls.Add(this.btnAddRow);
            this.panelActions.Controls.Add(this.btnRemoveRow);

            // dgvProperties
            this.dgvProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProperties.AllowUserToAddRows = false;
            this.dgvProperties.AllowUserToDeleteRows = false;
            this.dgvProperties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colName, this.colValue });

            this.colName.HeaderText = "属性名称";
            this.colName.Name = "colName";
            this.colName.FillWeight = 40;
            this.colValue.HeaderText = "属性值";
            this.colValue.Name = "colValue";
            this.colValue.FillWeight = 60;

            // MainForm
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.splitContainer);
            this.Text = "PropertyCardV2 - SolidWorks 属性填写";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpFiles.ResumeLayout(false);
            this.grpProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpFiles;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.DataGridView dgvProperties;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnLoadProperties;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.Button btnLoadTemplate;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnRemoveRow;
    }
}
