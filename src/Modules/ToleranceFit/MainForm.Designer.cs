namespace ToleranceFit
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
            this.SuspendLayout();

            // === 左面板：输入参数 ===
            var panelLeft = new System.Windows.Forms.Panel();
            panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            panelLeft.Width = 280;
            panelLeft.Padding = new System.Windows.Forms.Padding(10);
            panelLeft.AutoScroll = true;

            int y = 10;

            // 基本尺寸
            var lblSize = new System.Windows.Forms.Label();
            lblSize.Text = "基本尺寸 (mm):";
            lblSize.Location = new System.Drawing.Point(10, y);
            lblSize.AutoSize = true;
            panelLeft.Controls.Add(lblSize);
            y += 22;

            this.txtBasicSize = new System.Windows.Forms.TextBox();
            txtBasicSize.Location = new System.Drawing.Point(10, y);
            txtBasicSize.Size = new System.Drawing.Size(120, 23);
            txtBasicSize.Text = "20";
            panelLeft.Controls.Add(txtBasicSize);

            var lblSizeUnit = new System.Windows.Forms.Label();
            lblSizeUnit.Text = "mm";
            lblSizeUnit.Location = new System.Drawing.Point(135, y + 3);
            lblSizeUnit.AutoSize = true;
            panelLeft.Controls.Add(lblSizeUnit);
            y += 35;

            // 基准制
            var grpBasis = new System.Windows.Forms.GroupBox();
            grpBasis.Text = "基准制";
            grpBasis.Location = new System.Drawing.Point(10, y);
            grpBasis.Size = new System.Drawing.Size(250, 50);
            this.radHoleBasis = new System.Windows.Forms.RadioButton();
            radHoleBasis.Text = "基孔制";
            radHoleBasis.Location = new System.Drawing.Point(15, 20);
            radHoleBasis.AutoSize = true;
            radHoleBasis.Checked = true;
            radHoleBasis.CheckedChanged += new System.EventHandler(this.OnBasisChanged);
            grpBasis.Controls.Add(radHoleBasis);
            this.radShaftBasis = new System.Windows.Forms.RadioButton();
            radShaftBasis.Text = "基轴制";
            radShaftBasis.Location = new System.Drawing.Point(120, 20);
            radShaftBasis.AutoSize = true;
            radShaftBasis.CheckedChanged += new System.EventHandler(this.OnBasisChanged);
            grpBasis.Controls.Add(radShaftBasis);
            panelLeft.Controls.Add(grpBasis);
            y += 60;

            // 配合类型
            var grpFit = new System.Windows.Forms.GroupBox();
            grpFit.Text = "配合类型";
            grpFit.Location = new System.Drawing.Point(10, y);
            grpFit.Size = new System.Drawing.Size(250, 50);
            this.radClearance = new System.Windows.Forms.RadioButton();
            radClearance.Text = "间隙";
            radClearance.Location = new System.Drawing.Point(15, 20);
            radClearance.AutoSize = true;
            radClearance.Checked = true;
            radClearance.CheckedChanged += new System.EventHandler(this.OnFitTypeChanged);
            grpFit.Controls.Add(radClearance);
            this.radTransition = new System.Windows.Forms.RadioButton();
            radTransition.Text = "过渡";
            radTransition.Location = new System.Drawing.Point(80, 20);
            radTransition.AutoSize = true;
            radTransition.CheckedChanged += new System.EventHandler(this.OnFitTypeChanged);
            grpFit.Controls.Add(radTransition);
            this.radInterference = new System.Windows.Forms.RadioButton();
            radInterference.Text = "过盈";
            radInterference.Location = new System.Drawing.Point(145, 20);
            radInterference.AutoSize = true;
            radInterference.CheckedChanged += new System.EventHandler(this.OnFitTypeChanged);
            grpFit.Controls.Add(radInterference);
            panelLeft.Controls.Add(grpFit);
            y += 60;

            // 孔偏差选择
            var grpHole = new System.Windows.Forms.GroupBox();
            grpHole.Text = "孔公差带";
            grpHole.Location = new System.Drawing.Point(10, y);
            grpHole.Size = new System.Drawing.Size(250, 55);

            var lblHoleLetter = new System.Windows.Forms.Label();
            lblHoleLetter.Text = "偏差:";
            lblHoleLetter.Location = new System.Drawing.Point(10, 23);
            lblHoleLetter.AutoSize = true;
            grpHole.Controls.Add(lblHoleLetter);

            this.cmbHoleLetter = new System.Windows.Forms.ComboBox();
            cmbHoleLetter.Location = new System.Drawing.Point(45, 20);
            cmbHoleLetter.Size = new System.Drawing.Size(70, 23);
            cmbHoleLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbHoleLetter.SelectedIndexChanged += new System.EventHandler(this.OnHoleLetterChanged);
            grpHole.Controls.Add(cmbHoleLetter);

            var lblHoleGrade = new System.Windows.Forms.Label();
            lblHoleGrade.Text = "等级:";
            lblHoleGrade.Location = new System.Drawing.Point(125, 23);
            lblHoleGrade.AutoSize = true;
            grpHole.Controls.Add(lblHoleGrade);

            this.cmbHoleGrade = new System.Windows.Forms.ComboBox();
            cmbHoleGrade.Location = new System.Drawing.Point(160, 20);
            cmbHoleGrade.Size = new System.Drawing.Size(55, 23);
            cmbHoleGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            grpHole.Controls.Add(cmbHoleGrade);

            panelLeft.Controls.Add(grpHole);
            y += 65;

            // 轴偏差选择
            var grpShaft = new System.Windows.Forms.GroupBox();
            grpShaft.Text = "轴公差带";
            grpShaft.Location = new System.Drawing.Point(10, y);
            grpShaft.Size = new System.Drawing.Size(250, 55);

            var lblShaftLetter = new System.Windows.Forms.Label();
            lblShaftLetter.Text = "偏差:";
            lblShaftLetter.Location = new System.Drawing.Point(10, 23);
            lblShaftLetter.AutoSize = true;
            grpShaft.Controls.Add(lblShaftLetter);

            this.cmbShaftLetter = new System.Windows.Forms.ComboBox();
            cmbShaftLetter.Location = new System.Drawing.Point(45, 20);
            cmbShaftLetter.Size = new System.Drawing.Size(70, 23);
            cmbShaftLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbShaftLetter.SelectedIndexChanged += new System.EventHandler(this.OnShaftLetterChanged);
            grpShaft.Controls.Add(cmbShaftLetter);

            var lblShaftGrade = new System.Windows.Forms.Label();
            lblShaftGrade.Text = "等级:";
            lblShaftGrade.Location = new System.Drawing.Point(125, 23);
            lblShaftGrade.AutoSize = true;
            grpShaft.Controls.Add(lblShaftGrade);

            this.cmbShaftGrade = new System.Windows.Forms.ComboBox();
            cmbShaftGrade.Location = new System.Drawing.Point(160, 20);
            cmbShaftGrade.Size = new System.Drawing.Size(55, 23);
            cmbShaftGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            grpShaft.Controls.Add(cmbShaftGrade);

            panelLeft.Controls.Add(grpShaft);
            y += 65;

            // 按钮
            this.btnQuery = new System.Windows.Forms.Button();
            btnQuery.Text = "查 询";
            btnQuery.Location = new System.Drawing.Point(10, y);
            btnQuery.Size = new System.Drawing.Size(110, 35);
            btnQuery.Click += new System.EventHandler(this.OnQuery);
            panelLeft.Controls.Add(btnQuery);

            this.btnExport = new System.Windows.Forms.Button();
            btnExport.Text = "导出HTML";
            btnExport.Location = new System.Drawing.Point(130, y);
            btnExport.Size = new System.Drawing.Size(110, 35);
            btnExport.Click += new System.EventHandler(this.OnExportHtml);
            panelLeft.Controls.Add(btnExport);
            y += 45;

            // 参考标准
            var lblStandard = new System.Windows.Forms.Label();
            lblStandard.Text = "参考标准:\nGB/T 1800.1-2009\nGB/T 1800.2-2009\nGB/T 1801-2009";
            lblStandard.Location = new System.Drawing.Point(10, y);
            lblStandard.AutoSize = true;
            lblStandard.ForeColor = System.Drawing.Color.Gray;
            panelLeft.Controls.Add(lblStandard);
            y += 80;

            // 推荐配合
            var lblRecommend = new System.Windows.Forms.Label();
            lblRecommend.Text = "推荐配合:";
            lblRecommend.Location = new System.Drawing.Point(10, y);
            lblRecommend.AutoSize = true;
            lblRecommend.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            panelLeft.Controls.Add(lblRecommend);
            y += 20;

            this.txtRecommend = new System.Windows.Forms.TextBox();
            txtRecommend.Location = new System.Drawing.Point(10, y);
            txtRecommend.Size = new System.Drawing.Size(250, 80);
            txtRecommend.Multiline = true;
            txtRecommend.ReadOnly = true;
            txtRecommend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            panelLeft.Controls.Add(txtRecommend);

            // === 右面板：查询结果 ===
            var panelRight = new System.Windows.Forms.Panel();
            panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            panelRight.Padding = new System.Windows.Forms.Padding(10);
            panelRight.AutoScroll = true;

            this.txtResult = new System.Windows.Forms.TextBox();
            txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            txtResult.Multiline = true;
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtResult.Font = new System.Drawing.Font("Consolas", 10F);
            txtResult.WordWrap = false;
            panelRight.Controls.Add(txtResult);

            // 组装
            this.Controls.Add(panelRight);
            this.Controls.Add(panelLeft);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Text = "公差与配合查询";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox txtBasicSize;
        private System.Windows.Forms.RadioButton radHoleBasis;
        private System.Windows.Forms.RadioButton radShaftBasis;
        private System.Windows.Forms.RadioButton radClearance;
        private System.Windows.Forms.RadioButton radTransition;
        private System.Windows.Forms.RadioButton radInterference;
        private System.Windows.Forms.ComboBox cmbHoleLetter;
        private System.Windows.Forms.ComboBox cmbHoleGrade;
        private System.Windows.Forms.ComboBox cmbShaftLetter;
        private System.Windows.Forms.ComboBox cmbShaftGrade;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtRecommend;
        private System.Windows.Forms.TextBox txtResult;
    }
}
