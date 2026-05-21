namespace BearingDesign
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

            // 左面板：输入参数
            var panelLeft = new System.Windows.Forms.Panel();
            panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            panelLeft.Width = 280;
            panelLeft.Padding = new System.Windows.Forms.Padding(8);
            panelLeft.AutoScroll = true;

            int y = 5;

            // 标题
            var lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = "滚动轴承设计";
            lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(8, y);
            lblTitle.AutoSize = true;
            panelLeft.Controls.Add(lblTitle);
            y += 30;

            // 轴承类型
            var lblType = new System.Windows.Forms.Label();
            lblType.Text = "轴承类型:";
            lblType.Location = new System.Drawing.Point(8, y); lblType.AutoSize = true;
            panelLeft.Controls.Add(lblType); y += 22;
            this.cmbBearingType = new System.Windows.Forms.ComboBox();
            cmbBearingType.Location = new System.Drawing.Point(8, y);
            cmbBearingType.Size = new System.Drawing.Size(245, 23);
            cmbBearingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBearingType.SelectedIndexChanged += new System.EventHandler(this.OnBearingTypeChanged);
            panelLeft.Controls.Add(cmbBearingType); y += 30;

            // 轴承型号
            var lblCode = new System.Windows.Forms.Label();
            lblCode.Text = "轴承型号:";
            lblCode.Location = new System.Drawing.Point(8, y); lblCode.AutoSize = true;
            panelLeft.Controls.Add(lblCode); y += 22;
            this.cmbBearingCode = new System.Windows.Forms.ComboBox();
            cmbBearingCode.Location = new System.Drawing.Point(8, y);
            cmbBearingCode.Size = new System.Drawing.Size(200, 23);
            cmbBearingCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBearingCode.SelectedIndexChanged += new System.EventHandler(this.OnBearingSelected);
            panelLeft.Controls.Add(cmbBearingCode);

            this.btnAutoSelect = new System.Windows.Forms.Button();
            btnAutoSelect.Text = "自动";
            btnAutoSelect.Location = new System.Drawing.Point(210, y);
            btnAutoSelect.Size = new System.Drawing.Size(43, 23);
            btnAutoSelect.Click += new System.EventHandler(this.OnAutoSelect);
            panelLeft.Controls.Add(btnAutoSelect); y += 26;

            this.lblBearingInfo = new System.Windows.Forms.Label();
            lblBearingInfo.Text = "";
            lblBearingInfo.Location = new System.Drawing.Point(8, y);
            lblBearingInfo.Size = new System.Drawing.Size(245, 50);
            lblBearingInfo.ForeColor = System.Drawing.Color.Blue;
            panelLeft.Controls.Add(lblBearingInfo); y += 55;

            // 轴径
            AddLabel(panelLeft, "轴颈直径 d1 (mm):", ref y);
            this.txtShaftDia = AddTextBox(panelLeft, "35", ref y);

            // 径向力
            AddLabel(panelLeft, "径向力 Fr (N):", ref y);
            this.txtRadialForce = AddTextBox(panelLeft, "2000", ref y);

            // 轴向力
            AddLabel(panelLeft, "轴向力 Fa (N):", ref y);
            this.txtAxialForce = AddTextBox(panelLeft, "500", ref y);

            // 转速
            AddLabel(panelLeft, "工作转速 n (r/min):", ref y);
            this.txtSpeed = AddTextBox(panelLeft, "1000", ref y);

            // 要求寿命
            AddLabel(panelLeft, "要求寿命 Lh' (h):", ref y);
            this.txtLife = AddTextBox(panelLeft, "10000", ref y);

            // 工作温度
            AddLabel(panelLeft, "工作温度 (°C):", ref y);
            this.txtTemp = AddTextBox(panelLeft, "40", ref y);

            // 载荷类型
            var lblLoad = new System.Windows.Forms.Label();
            lblLoad.Text = "载荷类型:";
            lblLoad.Location = new System.Drawing.Point(8, y); lblLoad.AutoSize = true;
            panelLeft.Controls.Add(lblLoad); y += 22;
            this.cmbLoadType = new System.Windows.Forms.ComboBox();
            cmbLoadType.Location = new System.Drawing.Point(8, y);
            cmbLoadType.Size = new System.Drawing.Size(245, 23);
            cmbLoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            panelLeft.Controls.Add(cmbLoadType); y += 30;

            // 润滑方式
            var lblLub = new System.Windows.Forms.Label();
            lblLub.Text = "润滑方式:";
            lblLub.Location = new System.Drawing.Point(8, y); lblLub.AutoSize = true;
            panelLeft.Controls.Add(lblLub); y += 22;
            this.cmbLubrication = new System.Windows.Forms.ComboBox();
            cmbLubrication.Location = new System.Drawing.Point(8, y);
            cmbLubrication.Size = new System.Drawing.Size(120, 23);
            cmbLubrication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            panelLeft.Controls.Add(cmbLubrication);

            // 可靠度
            var lblRel = new System.Windows.Forms.Label();
            lblRel.Text = "可靠度:";
            lblRel.Location = new System.Drawing.Point(135, y); lblRel.AutoSize = true;
            panelLeft.Controls.Add(lblRel);
            this.cmbReliability = new System.Windows.Forms.ComboBox();
            cmbReliability.Location = new System.Drawing.Point(185, y);
            cmbReliability.Size = new System.Drawing.Size(68, 23);
            cmbReliability.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            panelLeft.Controls.Add(cmbReliability); y += 35;

            // 计算按钮
            this.btnCalc = new System.Windows.Forms.Button();
            btnCalc.Text = "校核计算";
            btnCalc.Location = new System.Drawing.Point(8, y);
            btnCalc.Size = new System.Drawing.Size(120, 32);
            btnCalc.Click += new System.EventHandler(this.OnCalculate);
            panelLeft.Controls.Add(btnCalc);

            // 信息标签
            this.lblInfo = new System.Windows.Forms.Label();
            lblInfo.Text = "";
            lblInfo.Location = new System.Drawing.Point(135, y + 5);
            lblInfo.AutoSize = true;
            panelLeft.Controls.Add(lblInfo);

            // 右面板：结果
            var panelRight = new System.Windows.Forms.Panel();
            panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            panelRight.Padding = new System.Windows.Forms.Padding(8);

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
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Text = "滚动轴承设计";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
        }

        private void AddLabel(System.Windows.Forms.Panel panel, string text, ref int y)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(8, y);
            lbl.AutoSize = true;
            panel.Controls.Add(lbl);
            y += 22;
        }

        private System.Windows.Forms.TextBox AddTextBox(System.Windows.Forms.Panel panel, string defaultVal, ref int y)
        {
            var txt = new System.Windows.Forms.TextBox();
            txt.Location = new System.Drawing.Point(8, y);
            txt.Size = new System.Drawing.Size(245, 23);
            txt.Text = defaultVal;
            panel.Controls.Add(txt);
            y += 28;
            return txt;
        }

        private System.Windows.Forms.ComboBox cmbBearingType;
        private System.Windows.Forms.ComboBox cmbBearingCode;
        private System.Windows.Forms.Button btnAutoSelect;
        private System.Windows.Forms.Label lblBearingInfo;
        private System.Windows.Forms.TextBox txtShaftDia;
        private System.Windows.Forms.TextBox txtRadialForce;
        private System.Windows.Forms.TextBox txtAxialForce;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.TextBox txtLife;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.ComboBox cmbLoadType;
        private System.Windows.Forms.ComboBox cmbLubrication;
        private System.Windows.Forms.ComboBox cmbReliability;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtResult;
    }
}
