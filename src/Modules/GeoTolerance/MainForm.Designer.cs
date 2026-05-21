namespace GeoTolerance
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
            panelLeft.Width = 260;
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
            txtBasicSize.Text = "50";
            panelLeft.Controls.Add(txtBasicSize);

            var lblSizeUnit = new System.Windows.Forms.Label();
            lblSizeUnit.Text = "mm";
            lblSizeUnit.Location = new System.Drawing.Point(135, y + 3);
            lblSizeUnit.AutoSize = true;
            panelLeft.Controls.Add(lblSizeUnit);
            y += 35;

            // 公差等级
            var lblGrade = new System.Windows.Forms.Label();
            lblGrade.Text = "公差等级:";
            lblGrade.Location = new System.Drawing.Point(10, y);
            lblGrade.AutoSize = true;
            panelLeft.Controls.Add(lblGrade);
            y += 22;

            this.cmbGrade = new System.Windows.Forms.ComboBox();
            cmbGrade.Location = new System.Drawing.Point(10, y);
            cmbGrade.Size = new System.Drawing.Size(80, 23);
            cmbGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            panelLeft.Controls.Add(cmbGrade);
            y += 35;

            // 公差类别
            var grpCategory = new System.Windows.Forms.GroupBox();
            grpCategory.Text = "公差类别";
            grpCategory.Location = new System.Drawing.Point(10, y);
            grpCategory.Size = new System.Drawing.Size(230, 50);
            this.chkShape = new System.Windows.Forms.CheckBox();
            chkShape.Text = "形状公差";
            chkShape.Location = new System.Drawing.Point(15, 20);
            chkShape.AutoSize = true;
            chkShape.Checked = true;
            chkShape.CheckedChanged += new System.EventHandler(this.OnCategoryChanged);
            grpCategory.Controls.Add(chkShape);
            this.chkLocation = new System.Windows.Forms.CheckBox();
            chkLocation.Text = "位置公差";
            chkLocation.Location = new System.Drawing.Point(110, 20);
            chkLocation.AutoSize = true;
            chkLocation.CheckedChanged += new System.EventHandler(this.OnCategoryChanged);
            grpCategory.Controls.Add(chkLocation);
            panelLeft.Controls.Add(grpCategory);
            y += 60;

            // 公差类型
            var grpType = new System.Windows.Forms.GroupBox();
            grpType.Text = "公差类型";
            grpType.Location = new System.Drawing.Point(10, y);
            grpType.Size = new System.Drawing.Size(230, 250);

            string[] shapeTypes = { "直线度", "平面度", "圆度", "圆柱度", "平行度", "垂直度" };
            string[] locationTypes = { "倾斜度", "同轴度", "对称度", "圆跳动", "全跳动" };

            this.radioTypes = new System.Windows.Forms.RadioButton[11];
            int ry = 20;
            for (int i = 0; i < 6; i++)
            {
                radioTypes[i] = new System.Windows.Forms.RadioButton();
                radioTypes[i].Text = shapeTypes[i];
                radioTypes[i].Location = new System.Drawing.Point(15, ry);
                radioTypes[i].AutoSize = true;
                radioTypes[i].Tag = i;
                radioTypes[i].CheckedChanged += new System.EventHandler(this.OnTypeChanged);
                grpType.Controls.Add(radioTypes[i]);
                ry += 25;
            }
            ry += 5;
            for (int i = 0; i < 5; i++)
            {
                radioTypes[i + 6] = new System.Windows.Forms.RadioButton();
                radioTypes[i + 6].Text = locationTypes[i];
                radioTypes[i + 6].Location = new System.Drawing.Point(15, ry);
                radioTypes[i + 6].AutoSize = true;
                radioTypes[i + 6].Tag = i + 6;
                radioTypes[i + 6].Enabled = false;
                radioTypes[i + 6].CheckedChanged += new System.EventHandler(this.OnTypeChanged);
                grpType.Controls.Add(radioTypes[i + 6]);
                ry += 25;
            }
            radioTypes[0].Checked = true;
            panelLeft.Controls.Add(grpType);
            y += 260;

            // 查询按钮
            this.btnQuery = new System.Windows.Forms.Button();
            btnQuery.Text = "查 询";
            btnQuery.Location = new System.Drawing.Point(10, y);
            btnQuery.Size = new System.Drawing.Size(100, 35);
            btnQuery.Click += new System.EventHandler(this.OnQuery);
            panelLeft.Controls.Add(btnQuery);
            y += 45;

            // 参考标准
            var lblStandard = new System.Windows.Forms.Label();
            lblStandard.Text = "参考标准:\nGB/T 1182-1996\n形状和位置公差\n通则、定义、符号和图样表示法";
            lblStandard.Location = new System.Drawing.Point(10, y);
            lblStandard.AutoSize = true;
            lblStandard.ForeColor = System.Drawing.Color.Gray;
            panelLeft.Controls.Add(lblStandard);

            // === 右面板：结果和图示 ===
            var panelRight = new System.Windows.Forms.Panel();
            panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            panelRight.Padding = new System.Windows.Forms.Padding(10);
            panelRight.AutoScroll = true;

            // 图示标题
            this.lblDiagramTitle = new System.Windows.Forms.Label();
            lblDiagramTitle.Text = "图样示意";
            lblDiagramTitle.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            lblDiagramTitle.Location = new System.Drawing.Point(10, 10);
            lblDiagramTitle.AutoSize = true;
            panelRight.Controls.Add(lblDiagramTitle);

            // 图片框
            this.picDiagram = new System.Windows.Forms.PictureBox();
            picDiagram.Location = new System.Drawing.Point(10, 35);
            picDiagram.Size = new System.Drawing.Size(400, 250);
            picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            picDiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            picDiagram.BackColor = System.Drawing.Color.White;
            panelRight.Controls.Add(picDiagram);

            // 参数说明
            this.lblParamNote = new System.Windows.Forms.Label();
            lblParamNote.Text = "";
            lblParamNote.Location = new System.Drawing.Point(10, 290);
            lblParamNote.Size = new System.Drawing.Size(400, 20);
            lblParamNote.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            panelRight.Controls.Add(lblParamNote);

            // 查询结果
            this.lblResultTitle = new System.Windows.Forms.Label();
            lblResultTitle.Text = "查询结果";
            lblResultTitle.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            lblResultTitle.Location = new System.Drawing.Point(10, 320);
            lblResultTitle.AutoSize = true;
            panelRight.Controls.Add(lblResultTitle);

            this.lblResult = new System.Windows.Forms.Label();
            lblResult.Text = "请输入参数后点击查询";
            lblResult.Location = new System.Drawing.Point(10, 345);
            lblResult.Font = new System.Drawing.Font("Consolas", 14F);
            lblResult.AutoSize = true;
            panelRight.Controls.Add(lblResult);

            // 标准依据
            this.lblStandardRef = new System.Windows.Forms.Label();
            lblStandardRef.Text = "";
            lblStandardRef.Location = new System.Drawing.Point(10, 380);
            lblStandardRef.Size = new System.Drawing.Size(400, 40);
            lblStandardRef.ForeColor = System.Drawing.Color.Gray;
            panelRight.Controls.Add(lblStandardRef);

            // 组装
            this.Controls.Add(panelRight);
            this.Controls.Add(panelLeft);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Text = "形位公差查询";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox txtBasicSize;
        private System.Windows.Forms.ComboBox cmbGrade;
        private System.Windows.Forms.CheckBox chkShape;
        private System.Windows.Forms.CheckBox chkLocation;
        private System.Windows.Forms.RadioButton[] radioTypes;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.PictureBox picDiagram;
        private System.Windows.Forms.Label lblDiagramTitle;
        private System.Windows.Forms.Label lblParamNote;
        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblStandardRef;
    }
}
