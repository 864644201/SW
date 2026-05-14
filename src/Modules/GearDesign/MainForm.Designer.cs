namespace GearDesign
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

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ===== 输入参数区 =====
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.labelPower = new System.Windows.Forms.Label();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.labelPowerUnit = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.labelSpeedUnit = new System.Windows.Forms.Label();
            this.labelRatio = new System.Windows.Forms.Label();
            this.txtRatio = new System.Windows.Forms.TextBox();
            this.labelRatioUnit = new System.Windows.Forms.Label();

            // ===== 齿轮参数区 =====
            this.groupBoxGear = new System.Windows.Forms.GroupBox();
            this.labelZ1 = new System.Windows.Forms.Label();
            this.txtZ1 = new System.Windows.Forms.TextBox();
            this.labelZ2 = new System.Windows.Forms.Label();
            this.txtZ2 = new System.Windows.Forms.TextBox();
            this.labelModule = new System.Windows.Forms.Label();
            this.txtModule = new System.Windows.Forms.TextBox();
            this.labelModuleUnit = new System.Windows.Forms.Label();
            this.labelAlpha = new System.Windows.Forms.Label();
            this.txtAlpha = new System.Windows.Forms.TextBox();
            this.labelAlphaUnit = new System.Windows.Forms.Label();
            this.labelPhiD = new System.Windows.Forms.Label();
            this.txtPhiD = new System.Windows.Forms.TextBox();
            this.labelPrecision = new System.Windows.Forms.Label();
            this.cmbPrecision = new System.Windows.Forms.ComboBox();
            this.labelPrecisionUnit = new System.Windows.Forms.Label();

            // ===== 材料选择区 =====
            this.groupBoxMaterial = new System.Windows.Forms.GroupBox();
            this.labelMat1 = new System.Windows.Forms.Label();
            this.cmbMat1 = new System.Windows.Forms.ComboBox();
            this.labelMat1Info = new System.Windows.Forms.Label();
            this.labelMat2 = new System.Windows.Forms.Label();
            this.cmbMat2 = new System.Windows.Forms.ComboBox();
            this.labelMat2Info = new System.Windows.Forms.Label();

            // ===== 工况系数区 =====
            this.groupBoxLoadCondition = new System.Windows.Forms.GroupBox();
            this.labelKA = new System.Windows.Forms.Label();
            this.cmbKA = new System.Windows.Forms.ComboBox();

            // ===== 按钮 =====
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();

            // ===== 结果区 =====
            this.groupBoxGeometry = new System.Windows.Forms.GroupBox();
            this.dgvGeometry = new System.Windows.Forms.DataGridView();
            this.colParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPinion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGear = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.groupBoxLoad = new System.Windows.Forms.GroupBox();
            this.txtLoadResult = new System.Windows.Forms.TextBox();

            this.groupBoxStrength = new System.Windows.Forms.GroupBox();
            this.dgvStrength = new System.Windows.Forms.DataGridView();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAllow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSafety = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResult = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.labelConclusion = new System.Windows.Forms.Label();

            // ===== 面板布局 =====
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.splitMain = new System.Windows.Forms.SplitContainer();

            this.groupBoxInput.SuspendLayout();
            this.groupBoxGear.SuspendLayout();
            this.groupBoxMaterial.SuspendLayout();
            this.groupBoxLoadCondition.SuspendLayout();
            this.groupBoxGeometry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeometry)).BeginInit();
            this.groupBoxLoad.SuspendLayout();
            this.groupBoxStrength.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStrength)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();

            // ===== groupBoxInput =====
            this.groupBoxInput.Controls.Add(this.labelPower);
            this.groupBoxInput.Controls.Add(this.txtPower);
            this.groupBoxInput.Controls.Add(this.labelPowerUnit);
            this.groupBoxInput.Controls.Add(this.labelSpeed);
            this.groupBoxInput.Controls.Add(this.txtSpeed);
            this.groupBoxInput.Controls.Add(this.labelSpeedUnit);
            this.groupBoxInput.Controls.Add(this.labelRatio);
            this.groupBoxInput.Controls.Add(this.txtRatio);
            this.groupBoxInput.Controls.Add(this.labelRatioUnit);
            this.groupBoxInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInput.Location = new System.Drawing.Point(5, 5);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxInput.Size = new System.Drawing.Size(340, 120);
            this.groupBoxInput.TabIndex = 0;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "工况参数";

            // labelPower
            this.labelPower.AutoSize = true;
            this.labelPower.Location = new System.Drawing.Point(15, 28);
            this.labelPower.Text = "传递功率 P：";
            // txtPower
            this.txtPower.Location = new System.Drawing.Point(110, 25);
            this.txtPower.Size = new System.Drawing.Size(100, 23);
            this.txtPower.Text = "10";
            // labelPowerUnit
            this.labelPowerUnit.AutoSize = true;
            this.labelPowerUnit.Location = new System.Drawing.Point(215, 28);
            this.labelPowerUnit.Text = "kW";

            // labelSpeed
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(15, 55);
            this.labelSpeed.Text = "小齿轮转速 n1：";
            // txtSpeed
            this.txtSpeed.Location = new System.Drawing.Point(120, 52);
            this.txtSpeed.Size = new System.Drawing.Size(100, 23);
            this.txtSpeed.Text = "1450";
            // labelSpeedUnit
            this.labelSpeedUnit.AutoSize = true;
            this.labelSpeedUnit.Location = new System.Drawing.Point(225, 55);
            this.labelSpeedUnit.Text = "rpm";

            // labelRatio
            this.labelRatio.AutoSize = true;
            this.labelRatio.Location = new System.Drawing.Point(15, 82);
            this.labelRatio.Text = "传动比 i：";
            // txtRatio
            this.txtRatio.Location = new System.Drawing.Point(110, 79);
            this.txtRatio.Size = new System.Drawing.Size(100, 23);
            this.txtRatio.Text = "3.5";
            // labelRatioUnit
            this.labelRatioUnit.AutoSize = true;
            this.labelRatioUnit.Location = new System.Drawing.Point(215, 82);
            this.labelRatioUnit.Text = "";

            // ===== groupBoxGear =====
            this.groupBoxGear.Controls.Add(this.labelZ1);
            this.groupBoxGear.Controls.Add(this.txtZ1);
            this.groupBoxGear.Controls.Add(this.labelZ2);
            this.groupBoxGear.Controls.Add(this.txtZ2);
            this.groupBoxGear.Controls.Add(this.labelModule);
            this.groupBoxGear.Controls.Add(this.txtModule);
            this.groupBoxGear.Controls.Add(this.labelModuleUnit);
            this.groupBoxGear.Controls.Add(this.labelAlpha);
            this.groupBoxGear.Controls.Add(this.txtAlpha);
            this.groupBoxGear.Controls.Add(this.labelAlphaUnit);
            this.groupBoxGear.Controls.Add(this.labelPhiD);
            this.groupBoxGear.Controls.Add(this.txtPhiD);
            this.groupBoxGear.Controls.Add(this.labelPrecision);
            this.groupBoxGear.Controls.Add(this.cmbPrecision);
            this.groupBoxGear.Controls.Add(this.labelPrecisionUnit);
            this.groupBoxGear.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxGear.Location = new System.Drawing.Point(5, 125);
            this.groupBoxGear.Name = "groupBoxGear";
            this.groupBoxGear.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxGear.Size = new System.Drawing.Size(340, 170);
            this.groupBoxGear.TabIndex = 1;
            this.groupBoxGear.TabStop = false;
            this.groupBoxGear.Text = "齿轮参数";

            // Z1
            this.labelZ1.AutoSize = true;
            this.labelZ1.Location = new System.Drawing.Point(15, 28);
            this.labelZ1.Text = "小齿轮齿数 Z1：";
            this.txtZ1.Location = new System.Drawing.Point(120, 25);
            this.txtZ1.Size = new System.Drawing.Size(60, 23);
            this.txtZ1.Text = "20";

            // Z2
            this.labelZ2.AutoSize = true;
            this.labelZ2.Location = new System.Drawing.Point(190, 28);
            this.labelZ2.Text = "Z2：";
            this.txtZ2.Location = new System.Drawing.Point(225, 25);
            this.txtZ2.Size = new System.Drawing.Size(60, 23);
            this.txtZ2.Text = "70";

            // Module
            this.labelModule.AutoSize = true;
            this.labelModule.Location = new System.Drawing.Point(15, 55);
            this.labelModule.Text = "模数 m：";
            this.txtModule.Location = new System.Drawing.Point(120, 52);
            this.txtModule.Size = new System.Drawing.Size(60, 23);
            this.txtModule.Text = "3";
            this.labelModuleUnit.AutoSize = true;
            this.labelModuleUnit.Location = new System.Drawing.Point(185, 55);
            this.labelModuleUnit.Text = "mm";

            // Alpha
            this.labelAlpha.AutoSize = true;
            this.labelAlpha.Location = new System.Drawing.Point(15, 82);
            this.labelAlpha.Text = "压力角 alpha：";
            this.txtAlpha.Location = new System.Drawing.Point(120, 79);
            this.txtAlpha.Size = new System.Drawing.Size(60, 23);
            this.txtAlpha.Text = "20";
            this.labelAlphaUnit.AutoSize = true;
            this.labelAlphaUnit.Location = new System.Drawing.Point(185, 82);
            this.labelAlphaUnit.Text = "度";

            // PhiD
            this.labelPhiD.AutoSize = true;
            this.labelPhiD.Location = new System.Drawing.Point(15, 109);
            this.labelPhiD.Text = "齿宽系数 phi_d：";
            this.txtPhiD.Location = new System.Drawing.Point(120, 106);
            this.txtPhiD.Size = new System.Drawing.Size(60, 23);
            this.txtPhiD.Text = "1.0";

            // Precision
            this.labelPrecision.AutoSize = true;
            this.labelPrecision.Location = new System.Drawing.Point(15, 136);
            this.labelPrecision.Text = "精度等级：";
            this.cmbPrecision.Location = new System.Drawing.Point(120, 133);
            this.cmbPrecision.Size = new System.Drawing.Size(60, 23);
            this.cmbPrecision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrecision.Items.AddRange(new object[] { "6", "7", "8", "9", "10" });
            this.cmbPrecision.SelectedIndex = 1; // 默认7级
            this.labelPrecisionUnit.AutoSize = true;
            this.labelPrecisionUnit.Location = new System.Drawing.Point(185, 136);
            this.labelPrecisionUnit.Text = "级";

            // ===== groupBoxMaterial =====
            this.groupBoxMaterial.Controls.Add(this.labelMat1);
            this.groupBoxMaterial.Controls.Add(this.cmbMat1);
            this.groupBoxMaterial.Controls.Add(this.labelMat1Info);
            this.groupBoxMaterial.Controls.Add(this.labelMat2);
            this.groupBoxMaterial.Controls.Add(this.cmbMat2);
            this.groupBoxMaterial.Controls.Add(this.labelMat2Info);
            this.groupBoxMaterial.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxMaterial.Location = new System.Drawing.Point(5, 295);
            this.groupBoxMaterial.Name = "groupBoxMaterial";
            this.groupBoxMaterial.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxMaterial.Size = new System.Drawing.Size(340, 120);
            this.groupBoxMaterial.TabIndex = 2;
            this.groupBoxMaterial.TabStop = false;
            this.groupBoxMaterial.Text = "材料选择";

            // Mat1
            this.labelMat1.AutoSize = true;
            this.labelMat1.Location = new System.Drawing.Point(15, 28);
            this.labelMat1.Text = "小齿轮：";
            this.cmbMat1.Location = new System.Drawing.Point(80, 25);
            this.cmbMat1.Size = new System.Drawing.Size(240, 23);
            this.cmbMat1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // labelMat1Info
            this.labelMat1Info.AutoSize = true;
            this.labelMat1Info.Location = new System.Drawing.Point(80, 50);
            this.labelMat1Info.ForeColor = System.Drawing.Color.Gray;
            this.labelMat1Info.Size = new System.Drawing.Size(250, 15);
            this.labelMat1Info.Text = "";

            // Mat2
            this.labelMat2.AutoSize = true;
            this.labelMat2.Location = new System.Drawing.Point(15, 72);
            this.labelMat2.Text = "大齿轮：";
            this.cmbMat2.Location = new System.Drawing.Point(80, 69);
            this.cmbMat2.Size = new System.Drawing.Size(240, 23);
            this.cmbMat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // labelMat2Info
            this.labelMat2Info.AutoSize = true;
            this.labelMat2Info.Location = new System.Drawing.Point(80, 94);
            this.labelMat2Info.ForeColor = System.Drawing.Color.Gray;
            this.labelMat2Info.Size = new System.Drawing.Size(250, 15);
            this.labelMat2Info.Text = "";

            // ===== groupBoxLoadCondition =====
            this.groupBoxLoadCondition.Controls.Add(this.labelKA);
            this.groupBoxLoadCondition.Controls.Add(this.cmbKA);
            this.groupBoxLoadCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxLoadCondition.Location = new System.Drawing.Point(5, 415);
            this.groupBoxLoadCondition.Name = "groupBoxLoadCondition";
            this.groupBoxLoadCondition.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxLoadCondition.Size = new System.Drawing.Size(340, 55);
            this.groupBoxLoadCondition.TabIndex = 3;
            this.groupBoxLoadCondition.TabStop = false;
            this.groupBoxLoadCondition.Text = "使用工况";

            // KA
            this.labelKA.AutoSize = true;
            this.labelKA.Location = new System.Drawing.Point(15, 25);
            this.labelKA.Text = "使用系数 KA：";
            this.cmbKA.Location = new System.Drawing.Point(120, 22);
            this.cmbKA.Size = new System.Drawing.Size(200, 23);
            this.cmbKA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKA.Items.AddRange(new object[] {
                "1.0 - 均匀平稳（电机、汽轮机驱动）",
                "1.25 - 轻微冲击（多缸内燃机驱动）",
                "1.50 - 中等冲击（单缸内燃机驱动）",
                "1.75 - 严重冲击（破碎机、冲床等）"
            });
            this.cmbKA.SelectedIndex = 0;

            // ===== btnCalculate =====
            this.btnCalculate.Location = new System.Drawing.Point(20, 480);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(140, 35);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "计算";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            // ===== btnReset =====
            this.btnReset.Location = new System.Drawing.Point(180, 480);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 35);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

            // ===== panelLeft =====
            this.panelLeft.AutoScroll = true;
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.MinimumSize = new System.Drawing.Size(350, 0);
            this.panelLeft.Padding = new System.Windows.Forms.Padding(5);
            this.panelLeft.Controls.Add(this.btnReset);
            this.panelLeft.Controls.Add(this.btnCalculate);
            this.panelLeft.Controls.Add(this.groupBoxLoadCondition);
            this.panelLeft.Controls.Add(this.groupBoxMaterial);
            this.panelLeft.Controls.Add(this.groupBoxGear);
            this.panelLeft.Controls.Add(this.groupBoxInput);

            // ===== groupBoxGeometry =====
            this.groupBoxGeometry.Controls.Add(this.dgvGeometry);
            this.groupBoxGeometry.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxGeometry.Location = new System.Drawing.Point(5, 5);
            this.groupBoxGeometry.Name = "groupBoxGeometry";
            this.groupBoxGeometry.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxGeometry.Size = new System.Drawing.Size(500, 220);
            this.groupBoxGeometry.TabIndex = 0;
            this.groupBoxGeometry.TabStop = false;
            this.groupBoxGeometry.Text = "几何参数";

            // dgvGeometry
            this.dgvGeometry.AllowUserToAddRows = false;
            this.dgvGeometry.AllowUserToDeleteRows = false;
            this.dgvGeometry.ReadOnly = true;
            this.dgvGeometry.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvGeometry.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvGeometry.Location = new System.Drawing.Point(11, 22);
            this.dgvGeometry.Size = new System.Drawing.Size(478, 190);
            this.dgvGeometry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGeometry.RowHeadersVisible = false;
            this.dgvGeometry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colParam, this.colPinion, this.colGear
            });
            // colParam
            this.colParam.HeaderText = "参数";
            this.colParam.FillWeight = 40;
            // colPinion
            this.colPinion.HeaderText = "小齿轮";
            this.colPinion.FillWeight = 30;
            // colGear
            this.colGear.HeaderText = "大齿轮";
            this.colGear.FillWeight = 30;

            // ===== groupBoxLoad =====
            this.groupBoxLoad.Controls.Add(this.txtLoadResult);
            this.groupBoxLoad.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxLoad.Location = new System.Drawing.Point(5, 225);
            this.groupBoxLoad.Name = "groupBoxLoad";
            this.groupBoxLoad.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxLoad.Size = new System.Drawing.Size(500, 130);
            this.groupBoxLoad.TabIndex = 1;
            this.groupBoxLoad.TabStop = false;
            this.groupBoxLoad.Text = "载荷参数";

            // txtLoadResult
            this.txtLoadResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLoadResult.Multiline = true;
            this.txtLoadResult.ReadOnly = true;
            this.txtLoadResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLoadResult.BackColor = System.Drawing.Color.White;
            this.txtLoadResult.Font = new System.Drawing.Font("Consolas", 9F);

            // ===== groupBoxStrength =====
            this.groupBoxStrength.Controls.Add(this.dgvStrength);
            this.groupBoxStrength.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxStrength.Location = new System.Drawing.Point(5, 355);
            this.groupBoxStrength.Name = "groupBoxStrength";
            this.groupBoxStrength.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxStrength.Size = new System.Drawing.Size(500, 250);
            this.groupBoxStrength.TabIndex = 2;
            this.groupBoxStrength.TabStop = false;
            this.groupBoxStrength.Text = "强度校核结果";

            // dgvStrength
            this.dgvStrength.AllowUserToAddRows = false;
            this.dgvStrength.AllowUserToDeleteRows = false;
            this.dgvStrength.ReadOnly = true;
            this.dgvStrength.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvStrength.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvStrength.Location = new System.Drawing.Point(11, 22);
            this.dgvStrength.Size = new System.Drawing.Size(478, 220);
            this.dgvStrength.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStrength.RowHeadersVisible = false;
            this.dgvStrength.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colItem, this.colValue, this.colAllow, this.colSafety, this.colResult
            });
            // colItem
            this.colItem.HeaderText = "校核项目";
            this.colItem.FillWeight = 25;
            // colValue
            this.colValue.HeaderText = "计算应力 (MPa)";
            this.colValue.FillWeight = 20;
            // colAllow
            this.colAllow.HeaderText = "许用应力 (MPa)";
            this.colAllow.FillWeight = 20;
            // colSafety
            this.colSafety.HeaderText = "安全系数";
            this.colSafety.FillWeight = 15;
            // colResult
            this.colResult.HeaderText = "结论";
            this.colResult.FillWeight = 15;

            // ===== labelConclusion =====
            this.labelConclusion.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelConclusion.Location = new System.Drawing.Point(5, 605);
            this.labelConclusion.Name = "labelConclusion";
            this.labelConclusion.Size = new System.Drawing.Size(500, 50);
            this.labelConclusion.TabIndex = 3;
            this.labelConclusion.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelConclusion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelConclusion.Text = "";

            // ===== panelRight =====
            this.panelRight.AutoScroll = true;
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.MinimumSize = new System.Drawing.Size(400, 0);
            this.panelRight.Padding = new System.Windows.Forms.Padding(5);
            this.panelRight.Controls.Add(this.labelConclusion);
            this.panelRight.Controls.Add(this.groupBoxStrength);
            this.panelRight.Controls.Add(this.groupBoxLoad);
            this.panelRight.Controls.Add(this.groupBoxGeometry);

            // ===== splitMain =====
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1.Controls.Add(this.panelLeft);
            this.splitMain.Panel1MinSize = 350;
            this.splitMain.Panel2.Controls.Add(this.panelRight);
            this.splitMain.Panel2MinSize = 400;
            this.splitMain.Size = new System.Drawing.Size(984, 661);
            this.splitMain.SplitterDistance = 360;
            this.splitMain.TabIndex = 0;

            // ===== MainForm =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.splitMain);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "圆柱齿轮设计 - 麦豆宝";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxGear.ResumeLayout(false);
            this.groupBoxGear.PerformLayout();
            this.groupBoxMaterial.ResumeLayout(false);
            this.groupBoxMaterial.PerformLayout();
            this.groupBoxLoadCondition.ResumeLayout(false);
            this.groupBoxLoadCondition.PerformLayout();
            this.groupBoxGeometry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeometry)).EndInit();
            this.groupBoxLoad.ResumeLayout(false);
            this.groupBoxLoad.PerformLayout();
            this.groupBoxStrength.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStrength)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // 输入区
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.Label labelPower;
        private System.Windows.Forms.TextBox txtPower;
        private System.Windows.Forms.Label labelPowerUnit;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Label labelSpeedUnit;
        private System.Windows.Forms.Label labelRatio;
        private System.Windows.Forms.TextBox txtRatio;
        private System.Windows.Forms.Label labelRatioUnit;

        // 齿轮参数区
        private System.Windows.Forms.GroupBox groupBoxGear;
        private System.Windows.Forms.Label labelZ1;
        private System.Windows.Forms.TextBox txtZ1;
        private System.Windows.Forms.Label labelZ2;
        private System.Windows.Forms.TextBox txtZ2;
        private System.Windows.Forms.Label labelModule;
        private System.Windows.Forms.TextBox txtModule;
        private System.Windows.Forms.Label labelModuleUnit;
        private System.Windows.Forms.Label labelAlpha;
        private System.Windows.Forms.TextBox txtAlpha;
        private System.Windows.Forms.Label labelAlphaUnit;
        private System.Windows.Forms.Label labelPhiD;
        private System.Windows.Forms.TextBox txtPhiD;
        private System.Windows.Forms.Label labelPrecision;
        private System.Windows.Forms.ComboBox cmbPrecision;
        private System.Windows.Forms.Label labelPrecisionUnit;

        // 材料选择区
        private System.Windows.Forms.GroupBox groupBoxMaterial;
        private System.Windows.Forms.Label labelMat1;
        private System.Windows.Forms.ComboBox cmbMat1;
        private System.Windows.Forms.Label labelMat1Info;
        private System.Windows.Forms.Label labelMat2;
        private System.Windows.Forms.ComboBox cmbMat2;
        private System.Windows.Forms.Label labelMat2Info;

        // 工况系数
        private System.Windows.Forms.GroupBox groupBoxLoadCondition;
        private System.Windows.Forms.Label labelKA;
        private System.Windows.Forms.ComboBox cmbKA;

        // 按钮
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnReset;

        // 结果区
        private System.Windows.Forms.GroupBox groupBoxGeometry;
        private System.Windows.Forms.DataGridView dgvGeometry;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPinion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGear;

        private System.Windows.Forms.GroupBox groupBoxLoad;
        private System.Windows.Forms.TextBox txtLoadResult;

        private System.Windows.Forms.GroupBox groupBoxStrength;
        private System.Windows.Forms.DataGridView dgvStrength;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAllow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSafety;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResult;

        private System.Windows.Forms.Label labelConclusion;

        // 布局
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.SplitContainer splitMain;
    }
}
