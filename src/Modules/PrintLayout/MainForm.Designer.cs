namespace PrintLayout
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpFiles = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.lblPaper = new System.Windows.Forms.Label();
            this.cboPaper = new System.Windows.Forms.ComboBox();
            this.lblArrange = new System.Windows.Forms.Label();
            this.cboArrange = new System.Windows.Forms.ComboBox();
            this.lblDW = new System.Windows.Forms.Label();
            this.txtDW = new System.Windows.Forms.TextBox();
            this.lblDWUnit = new System.Windows.Forms.Label();
            this.lblDH = new System.Windows.Forms.Label();
            this.txtDH = new System.Windows.Forms.TextBox();
            this.lblDHUnit = new System.Windows.Forms.Label();
            this.lblMargin = new System.Windows.Forms.Label();
            this.txtMargin = new System.Windows.Forms.TextBox();
            this.lblMarginUnit = new System.Windows.Forms.Label();
            this.lblSpacing = new System.Windows.Forms.Label();
            this.txtSpacing = new System.Windows.Forms.TextBox();
            this.lblSpacingUnit = new System.Windows.Forms.Label();
            this.lblManualRows = new System.Windows.Forms.Label();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.lblManualCols = new System.Windows.Forms.Label();
            this.numCols = new System.Windows.Forms.NumericUpDown();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblLayoutInfo = new System.Windows.Forms.Label();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpFiles.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCols)).BeginInit();
            this.pnlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.SplitterDistance = 350;

            // ---- Left Panel ----
            this.splitContainer.Panel1.Controls.Add(this.grpFiles);
            this.splitContainer.Panel1.Controls.Add(this.grpSettings);

            // grpFiles
            this.grpFiles.Controls.Add(this.btnBrowse);
            this.grpFiles.Controls.Add(this.lstFiles);
            this.grpFiles.Controls.Add(this.btnRemove);
            this.grpFiles.Controls.Add(this.btnClear);
            this.grpFiles.Controls.Add(this.lblFileCount);
            this.grpFiles.Location = new System.Drawing.Point(5, 5);
            this.grpFiles.Name = "grpFiles";
            this.grpFiles.Size = new System.Drawing.Size(340, 200);
            this.grpFiles.Text = "图纸文件";

            // btnBrowse
            this.btnBrowse.Location = new System.Drawing.Point(10, 20);
            this.btnBrowse.Size = new System.Drawing.Size(100, 28);
            this.btnBrowse.Text = "添加文件...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);

            // lstFiles
            this.lstFiles.Location = new System.Drawing.Point(10, 55);
            this.lstFiles.Size = new System.Drawing.Size(320, 100);
            this.lstFiles.HorizontalScrollbar = true;

            // btnRemove
            this.btnRemove.Location = new System.Drawing.Point(120, 20);
            this.btnRemove.Size = new System.Drawing.Size(70, 28);
            this.btnRemove.Text = "移除";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(200, 20);
            this.btnClear.Size = new System.Drawing.Size(70, 28);
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // lblFileCount
            this.lblFileCount.Location = new System.Drawing.Point(10, 165);
            this.lblFileCount.Size = new System.Drawing.Size(320, 20);
            this.lblFileCount.Text = "已选择 0 个文件";

            // grpSettings
            this.grpSettings.Controls.Add(this.lblPaper);
            this.grpSettings.Controls.Add(this.cboPaper);
            this.grpSettings.Controls.Add(this.lblArrange);
            this.grpSettings.Controls.Add(this.cboArrange);
            this.grpSettings.Controls.Add(this.lblDW);
            this.grpSettings.Controls.Add(this.txtDW);
            this.grpSettings.Controls.Add(this.lblDWUnit);
            this.grpSettings.Controls.Add(this.lblDH);
            this.grpSettings.Controls.Add(this.txtDH);
            this.grpSettings.Controls.Add(this.lblDHUnit);
            this.grpSettings.Controls.Add(this.lblMargin);
            this.grpSettings.Controls.Add(this.txtMargin);
            this.grpSettings.Controls.Add(this.lblMarginUnit);
            this.grpSettings.Controls.Add(this.lblSpacing);
            this.grpSettings.Controls.Add(this.txtSpacing);
            this.grpSettings.Controls.Add(this.lblSpacingUnit);
            this.grpSettings.Controls.Add(this.lblManualRows);
            this.grpSettings.Controls.Add(this.numRows);
            this.grpSettings.Controls.Add(this.lblManualCols);
            this.grpSettings.Controls.Add(this.numCols);
            this.grpSettings.Controls.Add(this.btnPreview);
            this.grpSettings.Controls.Add(this.btnPrint);
            this.grpSettings.Controls.Add(this.lblLayoutInfo);
            this.grpSettings.Location = new System.Drawing.Point(5, 210);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(340, 370);
            this.grpSettings.Text = "排版设置";

            // lblPaper
            this.lblPaper.AutoSize = true;
            this.lblPaper.Location = new System.Drawing.Point(10, 28);
            this.lblPaper.Text = "纸张大小：";

            // cboPaper
            this.cboPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaper.Items.AddRange(new object[] { "A3 (420x297)", "A4 (297x210)" });
            this.cboPaper.SelectedIndex = 1;
            this.cboPaper.Location = new System.Drawing.Point(110, 25);
            this.cboPaper.Size = new System.Drawing.Size(210, 21);
            this.cboPaper.SelectedIndexChanged += new System.EventHandler(this.SettingsChanged);

            // lblArrange
            this.lblArrange.AutoSize = true;
            this.lblArrange.Location = new System.Drawing.Point(10, 60);
            this.lblArrange.Text = "排列方式：";

            // cboArrange
            this.cboArrange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArrange.Items.AddRange(new object[] { "自动适应", "2x1 横排", "2x2 四宫格", "手动指定" });
            this.cboArrange.SelectedIndex = 0;
            this.cboArrange.Location = new System.Drawing.Point(110, 57);
            this.cboArrange.Size = new System.Drawing.Size(210, 21);
            this.cboArrange.SelectedIndexChanged += new System.EventHandler(this.cboArrange_SelectedIndexChanged);

            // lblDW
            this.lblDW.AutoSize = true;
            this.lblDW.Location = new System.Drawing.Point(10, 95);
            this.lblDW.Text = "图纸宽度：";

            // txtDW
            this.txtDW.Location = new System.Drawing.Point(110, 92);
            this.txtDW.Size = new System.Drawing.Size(120, 20);
            this.txtDW.Text = "420";

            // lblDWUnit
            this.lblDWUnit.AutoSize = true;
            this.lblDWUnit.Location = new System.Drawing.Point(235, 95);
            this.lblDWUnit.Text = "mm";

            // lblDH
            this.lblDH.AutoSize = true;
            this.lblDH.Location = new System.Drawing.Point(10, 125);
            this.lblDH.Text = "图纸高度：";

            // txtDH
            this.txtDH.Location = new System.Drawing.Point(110, 122);
            this.txtDH.Size = new System.Drawing.Size(120, 20);
            this.txtDH.Text = "297";

            // lblDHUnit
            this.lblDHUnit.AutoSize = true;
            this.lblDHUnit.Location = new System.Drawing.Point(235, 125);
            this.lblDHUnit.Text = "mm";

            // lblMargin
            this.lblMargin.AutoSize = true;
            this.lblMargin.Location = new System.Drawing.Point(10, 155);
            this.lblMargin.Text = "页边距：";

            // txtMargin
            this.txtMargin.Location = new System.Drawing.Point(110, 152);
            this.txtMargin.Size = new System.Drawing.Size(120, 20);
            this.txtMargin.Text = "10";

            // lblMarginUnit
            this.lblMarginUnit.AutoSize = true;
            this.lblMarginUnit.Location = new System.Drawing.Point(235, 155);
            this.lblMarginUnit.Text = "mm";

            // lblSpacing
            this.lblSpacing.AutoSize = true;
            this.lblSpacing.Location = new System.Drawing.Point(10, 185);
            this.lblSpacing.Text = "图纸间距：";

            // txtSpacing
            this.txtSpacing.Location = new System.Drawing.Point(110, 182);
            this.txtSpacing.Size = new System.Drawing.Size(120, 20);
            this.txtSpacing.Text = "5";

            // lblSpacingUnit
            this.lblSpacingUnit.AutoSize = true;
            this.lblSpacingUnit.Location = new System.Drawing.Point(235, 185);
            this.lblSpacingUnit.Text = "mm";

            // lblManualRows
            this.lblManualRows.AutoSize = true;
            this.lblManualRows.Location = new System.Drawing.Point(10, 215);
            this.lblManualRows.Text = "行数：";

            // numRows
            this.numRows.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numRows.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numRows.Value = new decimal(new int[] { 2, 0, 0, 0 });
            this.numRows.Location = new System.Drawing.Point(110, 212);
            this.numRows.Size = new System.Drawing.Size(80, 20);
            this.numRows.Enabled = false;

            // lblManualCols
            this.lblManualCols.AutoSize = true;
            this.lblManualCols.Location = new System.Drawing.Point(200, 215);
            this.lblManualCols.Text = "列数：";

            // numCols
            this.numCols.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCols.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numCols.Value = new decimal(new int[] { 2, 0, 0, 0 });
            this.numCols.Location = new System.Drawing.Point(245, 212);
            this.numCols.Size = new System.Drawing.Size(75, 20);
            this.numCols.Enabled = false;

            // btnPreview
            this.btnPreview.Location = new System.Drawing.Point(10, 250);
            this.btnPreview.Size = new System.Drawing.Size(150, 35);
            this.btnPreview.Text = "生成预览";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);

            // btnPrint
            this.btnPrint.Location = new System.Drawing.Point(170, 250);
            this.btnPrint.Size = new System.Drawing.Size(150, 35);
            this.btnPrint.Text = "打印预览";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            // lblLayoutInfo
            this.lblLayoutInfo.Location = new System.Drawing.Point(10, 295);
            this.lblLayoutInfo.Size = new System.Drawing.Size(320, 65);
            this.lblLayoutInfo.Text = "";

            // ---- Right Panel (Preview) ----
            this.splitContainer.Panel2.Controls.Add(this.pnlPreview);

            // pnlPreview
            this.pnlPreview.AutoScroll = true;
            this.pnlPreview.BackColor = System.Drawing.Color.White;
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreview.Controls.Add(this.picPreview);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;

            // picPreview
            this.picPreview.Dock = System.Windows.Forms.DockStyle.None;
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPreview.BackColor = System.Drawing.Color.White;

            // openFileDialog
            this.openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.gif|PDF文件|*.pdf|所有文件|*.*";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.Title = "选择图纸文件";

            // printPreviewDialog
            this.printPreviewDialog.Document = this.printDocument;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.splitContainer);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拼图打印";

            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpFiles.ResumeLayout(false);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCols)).EndInit();
            this.pnlPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpFiles;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Label lblPaper;
        private System.Windows.Forms.ComboBox cboPaper;
        private System.Windows.Forms.Label lblArrange;
        private System.Windows.Forms.ComboBox cboArrange;
        private System.Windows.Forms.Label lblDW;
        private System.Windows.Forms.TextBox txtDW;
        private System.Windows.Forms.Label lblDWUnit;
        private System.Windows.Forms.Label lblDH;
        private System.Windows.Forms.TextBox txtDH;
        private System.Windows.Forms.Label lblDHUnit;
        private System.Windows.Forms.Label lblMargin;
        private System.Windows.Forms.TextBox txtMargin;
        private System.Windows.Forms.Label lblMarginUnit;
        private System.Windows.Forms.Label lblSpacing;
        private System.Windows.Forms.TextBox txtSpacing;
        private System.Windows.Forms.Label lblSpacingUnit;
        private System.Windows.Forms.Label lblManualRows;
        private System.Windows.Forms.NumericUpDown numRows;
        private System.Windows.Forms.Label lblManualCols;
        private System.Windows.Forms.NumericUpDown numCols;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblLayoutInfo;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
    }
}
