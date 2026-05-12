namespace WormGearAndWormDesign
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
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.tabResult = new System.Windows.Forms.TabPage();

            // ==================== 输入选项卡控件 ====================
            this.grpBasic = new System.Windows.Forms.GroupBox();
            this.lblP = new System.Windows.Forms.Label();
            this.txtP = new System.Windows.Forms.TextBox();
            this.lblPUnit = new System.Windows.Forms.Label();
            this.lblN1 = new System.Windows.Forms.Label();
            this.txtN1 = new System.Windows.Forms.TextBox();
            this.lblN1Unit = new System.Windows.Forms.Label();
            this.lblI = new System.Windows.Forms.Label();
            this.txtI = new System.Windows.Forms.TextBox();
            this.lblIUnit = new System.Windows.Forms.Label();

            this.grpGear = new System.Windows.Forms.GroupBox();
            this.lblZ1 = new System.Windows.Forms.Label();
            this.cmbZ1 = new System.Windows.Forms.ComboBox();
            this.lblZ2 = new System.Windows.Forms.Label();
            this.txtZ2 = new System.Windows.Forms.TextBox();
            this.lblM = new System.Windows.Forms.Label();
            this.cmbM = new System.Windows.Forms.ComboBox();
            this.lblMUnit = new System.Windows.Forms.Label();
            this.lblQ = new System.Windows.Forms.Label();
            this.cmbQ = new System.Windows.Forms.ComboBox();

            this.grpMaterial = new System.Windows.Forms.GroupBox();
            this.lblWormMaterial = new System.Windows.Forms.Label();
            this.cmbWormMaterial = new System.Windows.Forms.ComboBox();
            this.lblWormGearMaterial = new System.Windows.Forms.Label();
            this.cmbWormGearMaterial = new System.Windows.Forms.ComboBox();

            this.grpStrength = new System.Windows.Forms.GroupBox();
            this.lblKa = new System.Windows.Forms.Label();
            this.txtKa = new System.Windows.Forms.TextBox();
            this.lblZe = new System.Windows.Forms.Label();
            this.txtZe = new System.Windows.Forms.TextBox();
            this.lblSigmaH = new System.Windows.Forms.Label();
            this.txtSigmaH = new System.Windows.Forms.TextBox();
            this.lblSigmaHUnit = new System.Windows.Forms.Label();
            this.lblSigmaF = new System.Windows.Forms.Label();
            this.txtSigmaF = new System.Windows.Forms.TextBox();
            this.lblSigmaFUnit = new System.Windows.Forms.Label();
            this.lblYFa2 = new System.Windows.Forms.Label();
            this.txtYFa2 = new System.Windows.Forms.TextBox();
            this.lblYBeta = new System.Windows.Forms.Label();
            this.txtYBeta = new System.Windows.Forms.TextBox();
            this.lblK = new System.Windows.Forms.Label();
            this.txtK = new System.Windows.Forms.TextBox();
            this.lblYAllow = new System.Windows.Forms.Label();
            this.txtYAllow = new System.Windows.Forms.TextBox();
            this.lblYAllowUnit = new System.Windows.Forms.Label();

            this.grpMaterialParam = new System.Windows.Forms.GroupBox();
            this.lblE = new System.Windows.Forms.Label();
            this.txtE = new System.Windows.Forms.TextBox();
            this.lblEUnit = new System.Windows.Forms.Label();
            this.lblNu = new System.Windows.Forms.Label();
            this.txtNu = new System.Windows.Forms.TextBox();
            this.lblF = new System.Windows.Forms.Label();
            this.txtF = new System.Windows.Forms.TextBox();

            this.grpHeat = new System.Windows.Forms.GroupBox();
            this.lblKHeat = new System.Windows.Forms.Label();
            this.txtKHeat = new System.Windows.Forms.TextBox();
            this.lblKHeatUnit = new System.Windows.Forms.Label();
            this.lblAHeat = new System.Windows.Forms.Label();
            this.txtAHeat = new System.Windows.Forms.TextBox();
            this.lblAHeatUnit = new System.Windows.Forms.Label();
            this.lblT1Oil = new System.Windows.Forms.Label();
            this.txtT1Oil = new System.Windows.Forms.TextBox();
            this.lblT1OilUnit = new System.Windows.Forms.Label();
            this.lblT0Env = new System.Windows.Forms.Label();
            this.txtT0Env = new System.Windows.Forms.TextBox();
            this.lblT0EnvUnit = new System.Windows.Forms.Label();

            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();

            // ==================== 结果选项卡控件 ====================
            this.grpGeometry = new System.Windows.Forms.GroupBox();
            this.lblD1 = new System.Windows.Forms.Label();
            this.txtD1 = new System.Windows.Forms.TextBox();
            this.lblD2 = new System.Windows.Forms.Label();
            this.txtD2 = new System.Windows.Forms.TextBox();
            this.lblA = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.lblGamma = new System.Windows.Forms.Label();
            this.txtGamma = new System.Windows.Forms.TextBox();
            this.lblGammaUnit = new System.Windows.Forms.Label();
            this.lblPx = new System.Windows.Forms.Label();
            this.txtPx = new System.Windows.Forms.TextBox();
            this.lblL = new System.Windows.Forms.Label();
            this.txtL = new System.Windows.Forms.TextBox();
            this.lblDa1 = new System.Windows.Forms.Label();
            this.txtDa1 = new System.Windows.Forms.TextBox();
            this.lblDf1 = new System.Windows.Forms.Label();
            this.txtDf1 = new System.Windows.Forms.TextBox();
            this.lblDa2 = new System.Windows.Forms.Label();
            this.txtDa2 = new System.Windows.Forms.TextBox();
            this.lblDf2 = new System.Windows.Forms.Label();
            this.txtDf2 = new System.Windows.Forms.TextBox();
            this.lblB1 = new System.Windows.Forms.Label();
            this.txtB1 = new System.Windows.Forms.TextBox();
            this.lblB2 = new System.Windows.Forms.Label();
            this.txtB2 = new System.Windows.Forms.TextBox();

            this.grpKinematics = new System.Windows.Forms.GroupBox();
            this.lblN2 = new System.Windows.Forms.Label();
            this.txtN2 = new System.Windows.Forms.TextBox();
            this.lblV1 = new System.Windows.Forms.Label();
            this.txtV1 = new System.Windows.Forms.TextBox();
            this.lblVs = new System.Windows.Forms.Label();
            this.txtVs = new System.Windows.Forms.TextBox();
            this.lblV2 = new System.Windows.Forms.Label();
            this.txtV2 = new System.Windows.Forms.TextBox();
            this.lblEta = new System.Windows.Forms.Label();
            this.txtEta = new System.Windows.Forms.TextBox();

            this.grpForces = new System.Windows.Forms.GroupBox();
            this.lblT1 = new System.Windows.Forms.Label();
            this.txtT1 = new System.Windows.Forms.TextBox();
            this.lblT2 = new System.Windows.Forms.Label();
            this.txtT2 = new System.Windows.Forms.TextBox();
            this.lblFt1 = new System.Windows.Forms.Label();
            this.txtFt1 = new System.Windows.Forms.TextBox();
            this.lblFa1 = new System.Windows.Forms.Label();
            this.txtFa1 = new System.Windows.Forms.TextBox();
            this.lblFr1 = new System.Windows.Forms.Label();
            this.txtFr1 = new System.Windows.Forms.TextBox();
            this.lblFt2 = new System.Windows.Forms.Label();
            this.txtFt2 = new System.Windows.Forms.TextBox();
            this.lblFa2 = new System.Windows.Forms.Label();
            this.txtFa2 = new System.Windows.Forms.TextBox();
            this.lblFr2 = new System.Windows.Forms.Label();
            this.txtFr2 = new System.Windows.Forms.TextBox();

            this.grpCheck = new System.Windows.Forms.GroupBox();
            this.lblContactTitle = new System.Windows.Forms.Label();
            this.txtSigmaH_Calc = new System.Windows.Forms.TextBox();
            this.lblContactStatus = new System.Windows.Forms.Label();
            this.lblBendingTitle = new System.Windows.Forms.Label();
            this.txtSigmaF_Calc = new System.Windows.Forms.TextBox();
            this.lblBendingStatus = new System.Windows.Forms.Label();
            this.lblHeatTitle = new System.Windows.Forms.Label();
            this.txtQr = new System.Windows.Forms.TextBox();
            this.txtQc = new System.Windows.Forms.TextBox();
            this.lblHeatStatus = new System.Windows.Forms.Label();
            this.lblStiffnessTitle = new System.Windows.Forms.Label();
            this.txtDeflection = new System.Windows.Forms.TextBox();
            this.lblStiffnessStatus = new System.Windows.Forms.Label();
            this.lblOverall = new System.Windows.Forms.Label();

            this.txtResult = new System.Windows.Forms.TextBox();

            // ==================== 初始化布局 ====================
            this.tabControl.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.SuspendLayout();

            // ---- tabControl ----
            this.tabControl.Controls.Add(this.tabInput);
            this.tabControl.Controls.Add(this.tabResult);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Size = new System.Drawing.Size(784, 561);
            this.tabControl.SelectedIndex = 0;

            // ==================== tabInput ====================
            this.tabInput.Controls.Add(this.grpBasic);
            this.tabInput.Controls.Add(this.grpGear);
            this.tabInput.Controls.Add(this.grpMaterial);
            this.tabInput.Controls.Add(this.grpStrength);
            this.tabInput.Controls.Add(this.grpMaterialParam);
            this.tabInput.Controls.Add(this.grpHeat);
            this.tabInput.Controls.Add(this.btnCalculate);
            this.tabInput.Controls.Add(this.btnClear);
            this.tabInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabInput.Text = "输入参数";
            this.tabInput.UseVisualStyleBackColor = true;

            // ---- grpBasic ----
            this.grpBasic.Text = "基本参数";
            this.grpBasic.Location = new System.Drawing.Point(12, 6);
            this.grpBasic.Size = new System.Drawing.Size(370, 110);

            // lblP
            this.lblP.Text = "传递功率 P:";
            this.lblP.Location = new System.Drawing.Point(15, 25);
            this.lblP.AutoSize = true;
            this.txtP.Location = new System.Drawing.Point(120, 22);
            this.txtP.Size = new System.Drawing.Size(80, 21);
            this.lblPUnit.Text = "kW";
            this.lblPUnit.Location = new System.Drawing.Point(205, 25);
            this.lblPUnit.AutoSize = true;

            // lblN1
            this.lblN1.Text = "蜗杆转速 n1:";
            this.lblN1.Location = new System.Drawing.Point(15, 52);
            this.lblN1.AutoSize = true;
            this.txtN1.Location = new System.Drawing.Point(120, 49);
            this.txtN1.Size = new System.Drawing.Size(80, 21);
            this.lblN1Unit.Text = "rpm";
            this.lblN1Unit.Location = new System.Drawing.Point(205, 52);
            this.lblN1Unit.AutoSize = true;

            // lblI
            this.lblI.Text = "传动比 i:";
            this.lblI.Location = new System.Drawing.Point(15, 79);
            this.lblI.AutoSize = true;
            this.txtI.Location = new System.Drawing.Point(120, 76);
            this.txtI.Size = new System.Drawing.Size(80, 21);
            this.txtI.TextChanged += new System.EventHandler(this.txtI_TextChanged);
            this.lblIUnit.Text = "";
            this.lblIUnit.Location = new System.Drawing.Point(205, 79);
            this.lblIUnit.AutoSize = true;

            this.grpBasic.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblP, this.txtP, this.lblPUnit,
                this.lblN1, this.txtN1, this.lblN1Unit,
                this.lblI, this.txtI, this.lblIUnit
            });

            // ---- grpGear ----
            this.grpGear.Text = "蜗杆蜗轮参数";
            this.grpGear.Location = new System.Drawing.Point(12, 122);
            this.grpGear.Size = new System.Drawing.Size(370, 110);

            this.lblZ1.Text = "蜗杆头数 z1:";
            this.lblZ1.Location = new System.Drawing.Point(15, 25);
            this.lblZ1.AutoSize = true;
            this.cmbZ1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZ1.Items.AddRange(new object[] { "1", "2", "3", "4" });
            this.cmbZ1.Location = new System.Drawing.Point(120, 22);
            this.cmbZ1.Size = new System.Drawing.Size(80, 20);
            this.cmbZ1.SelectedIndexChanged += new System.EventHandler(this.cmbZ1_SelectedIndexChanged);

            this.lblZ2.Text = "蜗轮齿数 z2:";
            this.lblZ2.Location = new System.Drawing.Point(15, 52);
            this.lblZ2.AutoSize = true;
            this.txtZ2.Location = new System.Drawing.Point(120, 49);
            this.txtZ2.Size = new System.Drawing.Size(80, 21);

            this.lblM.Text = "模数 m:";
            this.lblM.Location = new System.Drawing.Point(15, 79);
            this.lblM.AutoSize = true;
            this.cmbM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbM.Items.AddRange(new object[] { "1", "1.25", "1.6", "2", "2.5", "3.15", "4", "5", "6.3", "8", "10", "12.5", "16", "20" });
            this.cmbM.Location = new System.Drawing.Point(120, 76);
            this.cmbM.Size = new System.Drawing.Size(80, 20);
            this.lblMUnit.Text = "mm";
            this.lblMUnit.Location = new System.Drawing.Point(205, 79);
            this.lblMUnit.AutoSize = true;

            this.lblQ.Text = "直径系数 q:";
            this.lblQ.Location = new System.Drawing.Point(220, 25);
            this.lblQ.AutoSize = true;
            this.cmbQ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQ.Items.AddRange(new object[] { "7.1", "8", "9", "10", "11.2", "12.5", "14", "16", "18", "20" });
            this.cmbQ.Location = new System.Drawing.Point(300, 22);
            this.cmbQ.Size = new System.Drawing.Size(60, 20);

            this.grpGear.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblZ1, this.cmbZ1,
                this.lblZ2, this.txtZ2,
                this.lblM, this.cmbM, this.lblMUnit,
                this.lblQ, this.cmbQ
            });

            // ---- grpMaterial ----
            this.grpMaterial.Text = "材料选择";
            this.grpMaterial.Location = new System.Drawing.Point(400, 6);
            this.grpMaterial.Size = new System.Drawing.Size(370, 80);

            this.lblWormMaterial.Text = "蜗杆材料:";
            this.lblWormMaterial.Location = new System.Drawing.Point(15, 25);
            this.lblWormMaterial.AutoSize = true;
            this.cmbWormMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWormMaterial.Items.AddRange(new object[] {
                "20Cr 渗碳淬火 HRC58-62",
                "20CrMnTi 渗碳淬火 HRC58-62",
                "45钢 淬火 HRC45-50",
                "40Cr 表面淬火 HRC48-55"
            });
            this.cmbWormMaterial.Location = new System.Drawing.Point(100, 22);
            this.cmbWormMaterial.Size = new System.Drawing.Size(250, 20);

            this.lblWormGearMaterial.Text = "蜗轮材料:";
            this.lblWormGearMaterial.Location = new System.Drawing.Point(15, 52);
            this.lblWormGearMaterial.AutoSize = true;
            this.cmbWormGearMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWormGearMaterial.Items.AddRange(new object[] {
                "ZCuSn10P1 锡青铜 (砂模)",
                "ZCuSn10P1 锡青铜 (金属模)",
                "ZCuAl10Fe3 铝青铜",
                "HT200 灰铸铁"
            });
            this.cmbWormGearMaterial.Location = new System.Drawing.Point(100, 49);
            this.cmbWormGearMaterial.Size = new System.Drawing.Size(250, 20);

            this.grpMaterial.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblWormMaterial, this.cmbWormMaterial,
                this.lblWormGearMaterial, this.cmbWormGearMaterial
            });

            // ---- grpStrength ----
            this.grpStrength.Text = "强度参数";
            this.grpStrength.Location = new System.Drawing.Point(400, 92);
            this.grpStrength.Size = new System.Drawing.Size(370, 170);

            int sx = 15, sy = 22, sdx = 100, sdy = 27;

            this.lblKa.Text = "使用系数 Ka:";
            this.lblKa.Location = new System.Drawing.Point(sx, sy); this.lblKa.AutoSize = true;
            this.txtKa.Location = new System.Drawing.Point(sx + sdx, sy - 3); this.txtKa.Size = new System.Drawing.Size(60, 21);

            this.lblZe.Text = "弹性系数 Ze:";
            this.lblZe.Location = new System.Drawing.Point(sx, sy + sdy); this.lblZe.AutoSize = true;
            this.txtZe.Location = new System.Drawing.Point(sx + sdx, sy + sdy - 3); this.txtZe.Size = new System.Drawing.Size(60, 21);
            this.lblSigmaH.Text = "[σ_H]:";
            this.lblSigmaH.Location = new System.Drawing.Point(sx, sy + 2 * sdy); this.lblSigmaH.AutoSize = true;
            this.txtSigmaH.Location = new System.Drawing.Point(sx + sdx, sy + 2 * sdy - 3); this.txtSigmaH.Size = new System.Drawing.Size(60, 21);
            this.lblSigmaHUnit.Text = "MPa";
            this.lblSigmaHUnit.Location = new System.Drawing.Point(sx + sdx + 65, sy + 2 * sdy); this.lblSigmaHUnit.AutoSize = true;

            this.lblSigmaF.Text = "[σ_F]:";
            this.lblSigmaF.Location = new System.Drawing.Point(200, sy); this.lblSigmaF.AutoSize = true;
            this.txtSigmaF.Location = new System.Drawing.Point(270, sy - 3); this.txtSigmaF.Size = new System.Drawing.Size(60, 21);
            this.lblSigmaFUnit.Text = "MPa";
            this.lblSigmaFUnit.Location = new System.Drawing.Point(335, sy); this.lblSigmaFUnit.AutoSize = true;

            this.lblYFa2.Text = "齿形系数 Y_Fa2:";
            this.lblYFa2.Location = new System.Drawing.Point(200, sy + sdy); this.lblYFa2.AutoSize = true;
            this.txtYFa2.Location = new System.Drawing.Point(310, sy + sdy - 3); this.txtYFa2.Size = new System.Drawing.Size(50, 21);

            this.lblYBeta.Text = "螺旋角系数 Y_β:";
            this.lblYBeta.Location = new System.Drawing.Point(200, sy + 2 * sdy); this.lblYBeta.AutoSize = true;
            this.txtYBeta.Location = new System.Drawing.Point(310, sy + 2 * sdy - 3); this.txtYBeta.Size = new System.Drawing.Size(50, 21);

            this.lblK.Text = "载荷系数 K:";
            this.lblK.Location = new System.Drawing.Point(sx, sy + 3 * sdy); this.lblK.AutoSize = true;
            this.txtK.Location = new System.Drawing.Point(sx + sdx, sy + 3 * sdy - 3); this.txtK.Size = new System.Drawing.Size(60, 21);

            this.lblYAllow.Text = "许用挠度 [y]:";
            this.lblYAllow.Location = new System.Drawing.Point(200, sy + 3 * sdy); this.lblYAllow.AutoSize = true;
            this.txtYAllow.Location = new System.Drawing.Point(310, sy + 3 * sdy - 3); this.txtYAllow.Size = new System.Drawing.Size(50, 21);
            this.lblYAllowUnit.Text = "mm";
            this.lblYAllowUnit.Location = new System.Drawing.Point(335, sy + 3 * sdy); this.lblYAllowUnit.AutoSize = true;

            this.grpStrength.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblKa, this.txtKa,
                this.lblZe, this.txtZe,
                this.lblSigmaH, this.txtSigmaH, this.lblSigmaHUnit,
                this.lblSigmaF, this.txtSigmaF, this.lblSigmaFUnit,
                this.lblYFa2, this.txtYFa2,
                this.lblYBeta, this.txtYBeta,
                this.lblK, this.txtK,
                this.lblYAllow, this.txtYAllow, this.lblYAllowUnit
            });

            // ---- grpMaterialParam ----
            this.grpMaterialParam.Text = "材料力学参数";
            this.grpMaterialParam.Location = new System.Drawing.Point(12, 238);
            this.grpMaterialParam.Size = new System.Drawing.Size(370, 80);

            this.lblE.Text = "弹性模量 E:";
            this.lblE.Location = new System.Drawing.Point(15, 25); this.lblE.AutoSize = true;
            this.txtE.Location = new System.Drawing.Point(120, 22); this.txtE.Size = new System.Drawing.Size(80, 21);
            this.lblEUnit.Text = "MPa";
            this.lblEUnit.Location = new System.Drawing.Point(205, 25); this.lblEUnit.AutoSize = true;

            this.lblNu.Text = "泊松比 ν:";
            this.lblNu.Location = new System.Drawing.Point(15, 52); this.lblNu.AutoSize = true;
            this.txtNu.Location = new System.Drawing.Point(120, 49); this.txtNu.Size = new System.Drawing.Size(80, 21);

            this.lblF.Text = "摩擦系数 f:";
            this.lblF.Location = new System.Drawing.Point(220, 25); this.lblF.AutoSize = true;
            this.txtF.Location = new System.Drawing.Point(300, 22); this.txtF.Size = new System.Drawing.Size(60, 21);

            this.grpMaterialParam.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblE, this.txtE, this.lblEUnit,
                this.lblNu, this.txtNu,
                this.lblF, this.txtF
            });

            // ---- grpHeat ----
            this.grpHeat.Text = "热平衡参数";
            this.grpHeat.Location = new System.Drawing.Point(12, 324);
            this.grpHeat.Size = new System.Drawing.Size(370, 110);

            this.lblKHeat.Text = "散热系数 K:";
            this.lblKHeat.Location = new System.Drawing.Point(15, 25); this.lblKHeat.AutoSize = true;
            this.txtKHeat.Location = new System.Drawing.Point(120, 22); this.txtKHeat.Size = new System.Drawing.Size(80, 21);
            this.lblKHeatUnit.Text = "W/(m²·℃)";
            this.lblKHeatUnit.Location = new System.Drawing.Point(205, 25); this.lblKHeatUnit.AutoSize = true;

            this.lblAHeat.Text = "散热面积 A:";
            this.lblAHeat.Location = new System.Drawing.Point(15, 52); this.lblAHeat.AutoSize = true;
            this.txtAHeat.Location = new System.Drawing.Point(120, 49); this.txtAHeat.Size = new System.Drawing.Size(80, 21);
            this.lblAHeatUnit.Text = "m²";
            this.lblAHeatUnit.Location = new System.Drawing.Point(205, 52); this.lblAHeatUnit.AutoSize = true;

            this.lblT1Oil.Text = "允许油温 t1:";
            this.lblT1Oil.Location = new System.Drawing.Point(15, 79); this.lblT1Oil.AutoSize = true;
            this.txtT1Oil.Location = new System.Drawing.Point(120, 76); this.txtT1Oil.Size = new System.Drawing.Size(80, 21);
            this.lblT1OilUnit.Text = "℃";
            this.lblT1OilUnit.Location = new System.Drawing.Point(205, 79); this.lblT1OilUnit.AutoSize = true;

            this.lblT0Env.Text = "环境温度 t0:";
            this.lblT0Env.Location = new System.Drawing.Point(220, 25); this.lblT0Env.AutoSize = true;
            this.txtT0Env.Location = new System.Drawing.Point(300, 22); this.txtT0Env.Size = new System.Drawing.Size(60, 21);
            this.lblT0EnvUnit.Text = "℃";
            this.lblT0EnvUnit.Location = new System.Drawing.Point(335, 25); this.lblT0EnvUnit.AutoSize = true;

            this.grpHeat.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblKHeat, this.txtKHeat, this.lblKHeatUnit,
                this.lblAHeat, this.txtAHeat, this.lblAHeatUnit,
                this.lblT1Oil, this.txtT1Oil, this.lblT1OilUnit,
                this.lblT0Env, this.txtT0Env, this.lblT0EnvUnit
            });

            // ---- btnCalculate ----
            this.btnCalculate.Text = "计算";
            this.btnCalculate.Location = new System.Drawing.Point(400, 440);
            this.btnCalculate.Size = new System.Drawing.Size(100, 35);
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            // ---- btnClear ----
            this.btnClear.Text = "清除结果";
            this.btnClear.Location = new System.Drawing.Point(520, 440);
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // ==================== tabResult ====================
            this.tabResult.Controls.Add(this.grpGeometry);
            this.tabResult.Controls.Add(this.grpKinematics);
            this.tabResult.Controls.Add(this.grpForces);
            this.tabResult.Controls.Add(this.grpCheck);
            this.tabResult.Controls.Add(this.txtResult);
            this.tabResult.Text = "计算结果";
            this.tabResult.UseVisualStyleBackColor = true;

            // ---- grpGeometry ----
            this.grpGeometry.Text = "几何参数";
            this.grpGeometry.Location = new System.Drawing.Point(6, 6);
            this.grpGeometry.Size = new System.Drawing.Size(380, 220);

            int gx = 15, gy = 22, gdx = 110, gdy = 27;

            CreateResultLabelAndTextBox(this.grpGeometry, "d1 =", ref this.lblD1, ref this.txtD1, gx, gy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "d2 =", ref this.lblD2, ref this.txtD2, gx, gy + gdy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "中心距 a =", ref this.lblA, ref this.txtA, gx, gy + 2 * gdy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "导程角 γ =", ref this.lblGamma, ref this.txtGamma, gx, gy + 3 * gdy, "°");
            CreateResultLabelAndTextBox(this.grpGeometry, "轴向齿距 px =", ref this.lblPx, ref this.txtPx, gx, gy + 4 * gdy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "导程 L =", ref this.lblL, ref this.txtL, gx, gy + 5 * gdy, "mm");

            CreateResultLabelAndTextBox(this.grpGeometry, "da1 =", ref this.lblDa1, ref this.txtDa1, 200, gy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "df1 =", ref this.lblDf1, ref this.txtDf1, 200, gy + gdy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "da2 =", ref this.lblDa2, ref this.txtDa2, 200, gy + 2 * gdy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "df2 =", ref this.lblDf2, ref this.txtDf2, 200, gy + 3 * gdy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "b1 =", ref this.lblB1, ref this.txtB1, 200, gy + 4 * gdy, "mm");
            CreateResultLabelAndTextBox(this.grpGeometry, "b2 =", ref this.lblB2, ref this.txtB2, 200, gy + 5 * gdy, "mm");

            // ---- grpKinematics ----
            this.grpKinematics.Text = "运动学参数";
            this.grpKinematics.Location = new System.Drawing.Point(6, 232);
            this.grpKinematics.Size = new System.Drawing.Size(380, 160);

            int ky = 22, kdy = 27;

            CreateResultLabelAndTextBox(this.grpKinematics, "蜗轮转速 n2 =", ref this.lblN2, ref this.txtN2, gx, ky, "rpm");
            CreateResultLabelAndTextBox(this.grpKinematics, "v1 =", ref this.lblV1, ref this.txtV1, gx, ky + kdy, "m/s");
            CreateResultLabelAndTextBox(this.grpKinematics, "滑动速度 vs =", ref this.lblVs, ref this.txtVs, gx, ky + 2 * kdy, "m/s");
            CreateResultLabelAndTextBox(this.grpKinematics, "v2 =", ref this.lblV2, ref this.txtV2, 200, ky, "m/s");
            CreateResultLabelAndTextBox(this.grpKinematics, "效率 η =", ref this.lblEta, ref this.txtEta, 200, ky + kdy, "");

            // ---- grpForces ----
            this.grpForces.Text = "力的分析";
            this.grpForces.Location = new System.Drawing.Point(6, 398);
            this.grpForces.Size = new System.Drawing.Size(380, 120);

            int fy = 22, fdy = 27;

            CreateResultLabelAndTextBox(this.grpForces, "T1 =", ref this.lblT1, ref this.txtT1, gx, fy, "N·mm");
            CreateResultLabelAndTextBox(this.grpForces, "T2 =", ref this.lblT2, ref this.txtT2, gx, fy + fdy, "N·mm");
            CreateResultLabelAndTextBox(this.grpForces, "Ft1 =", ref this.lblFt1, ref this.txtFt1, 200, fy, "N");
            CreateResultLabelAndTextBox(this.grpForces, "Fa1 =", ref this.lblFa1, ref this.txtFa1, 200, fy + fdy, "N");

            // ---- grpCheck ----
            this.grpCheck.Text = "校核结果";
            this.grpCheck.Location = new System.Drawing.Point(400, 6);
            this.grpCheck.Size = new System.Drawing.Size(370, 250);

            int cx = 15, cy = 25, cdy = 55;

            this.lblContactTitle.Text = "齿面接触强度:";
            this.lblContactTitle.Location = new System.Drawing.Point(cx, cy); this.lblContactTitle.AutoSize = true;
            this.txtSigmaH_Calc.Location = new System.Drawing.Point(cx, cy + 20); this.txtSigmaH_Calc.Size = new System.Drawing.Size(100, 21);
            this.txtSigmaH_Calc.ReadOnly = true;
            this.lblContactStatus.Text = "--";
            this.lblContactStatus.Location = new System.Drawing.Point(cx + 110, cy + 23); this.lblContactStatus.AutoSize = true;
            this.lblContactStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);

            this.lblBendingTitle.Text = "齿根弯曲强度:";
            this.lblBendingTitle.Location = new System.Drawing.Point(cx, cy + cdy); this.lblBendingTitle.AutoSize = true;
            this.txtSigmaF_Calc.Location = new System.Drawing.Point(cx, cy + cdy + 20); this.txtSigmaF_Calc.Size = new System.Drawing.Size(100, 21);
            this.txtSigmaF_Calc.ReadOnly = true;
            this.lblBendingStatus.Text = "--";
            this.lblBendingStatus.Location = new System.Drawing.Point(cx + 110, cy + cdy + 23); this.lblBendingStatus.AutoSize = true;
            this.lblBendingStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);

            this.lblHeatTitle.Text = "热平衡 (Qr / Qc):";
            this.lblHeatTitle.Location = new System.Drawing.Point(cx, cy + 2 * cdy); this.lblHeatTitle.AutoSize = true;
            this.txtQr.Location = new System.Drawing.Point(cx, cy + 2 * cdy + 20); this.txtQr.Size = new System.Drawing.Size(80, 21);
            this.txtQr.ReadOnly = true;
            this.txtQc.Location = new System.Drawing.Point(cx + 90, cy + 2 * cdy + 20); this.txtQc.Size = new System.Drawing.Size(80, 21);
            this.txtQc.ReadOnly = true;
            this.lblHeatStatus.Text = "--";
            this.lblHeatStatus.Location = new System.Drawing.Point(cx + 180, cy + 2 * cdy + 23); this.lblHeatStatus.AutoSize = true;
            this.lblHeatStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);

            this.lblStiffnessTitle.Text = "蜗杆刚度 (挠度 y):";
            this.lblStiffnessTitle.Location = new System.Drawing.Point(cx, cy + 3 * cdy); this.lblStiffnessTitle.AutoSize = true;
            this.txtDeflection.Location = new System.Drawing.Point(cx, cy + 3 * cdy + 20); this.txtDeflection.Size = new System.Drawing.Size(100, 21);
            this.txtDeflection.ReadOnly = true;
            this.lblStiffnessStatus.Text = "--";
            this.lblStiffnessStatus.Location = new System.Drawing.Point(cx + 110, cy + 3 * cdy + 23); this.lblStiffnessStatus.AutoSize = true;
            this.lblStiffnessStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);

            this.lblOverall.Text = "";
            this.lblOverall.Location = new System.Drawing.Point(cx, 225); this.lblOverall.AutoSize = true;
            this.lblOverall.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);

            this.grpCheck.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblContactTitle, this.txtSigmaH_Calc, this.lblContactStatus,
                this.lblBendingTitle, this.txtSigmaF_Calc, this.lblBendingStatus,
                this.lblHeatTitle, this.txtQr, this.txtQc, this.lblHeatStatus,
                this.lblStiffnessTitle, this.txtDeflection, this.lblStiffnessStatus,
                this.lblOverall
            });

            // ---- txtResult ----
            this.txtResult.Location = new System.Drawing.Point(400, 262);
            this.txtResult.Multiline = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(370, 260);
            this.txtResult.ReadOnly = true;
            this.txtResult.Font = new System.Drawing.Font("Consolas", 9F);

            // ==================== MainForm ====================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "蜗轮蜗杆设计计算";

            this.tabControl.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void CreateResultLabelAndTextBox(
            System.Windows.Forms.Control parent, string text,
            ref System.Windows.Forms.Label lbl, ref System.Windows.Forms.TextBox txt,
            int x, int y, string unit)
        {
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.AutoSize = true;

            txt.Location = new System.Drawing.Point(x + 90, y - 3);
            txt.Size = new System.Drawing.Size(80, 21);
            txt.ReadOnly = true;

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);

            if (!string.IsNullOrEmpty(unit))
            {
                var lblU = new System.Windows.Forms.Label();
                lblU.Text = unit;
                lblU.Location = new System.Drawing.Point(x + 175, y);
                lblU.AutoSize = true;
                parent.Controls.Add(lblU);
            }
        }

        // ==================== 控件声明 ====================
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.TabPage tabResult;

        // 输入选项卡
        private System.Windows.Forms.GroupBox grpBasic;
        private System.Windows.Forms.Label lblP;
        private System.Windows.Forms.TextBox txtP;
        private System.Windows.Forms.Label lblPUnit;
        private System.Windows.Forms.Label lblN1;
        private System.Windows.Forms.TextBox txtN1;
        private System.Windows.Forms.Label lblN1Unit;
        private System.Windows.Forms.Label lblI;
        private System.Windows.Forms.TextBox txtI;
        private System.Windows.Forms.Label lblIUnit;

        private System.Windows.Forms.GroupBox grpGear;
        private System.Windows.Forms.Label lblZ1;
        private System.Windows.Forms.ComboBox cmbZ1;
        private System.Windows.Forms.Label lblZ2;
        private System.Windows.Forms.TextBox txtZ2;
        private System.Windows.Forms.Label lblM;
        private System.Windows.Forms.ComboBox cmbM;
        private System.Windows.Forms.Label lblMUnit;
        private System.Windows.Forms.Label lblQ;
        private System.Windows.Forms.ComboBox cmbQ;

        private System.Windows.Forms.GroupBox grpMaterial;
        private System.Windows.Forms.Label lblWormMaterial;
        private System.Windows.Forms.ComboBox cmbWormMaterial;
        private System.Windows.Forms.Label lblWormGearMaterial;
        private System.Windows.Forms.ComboBox cmbWormGearMaterial;

        private System.Windows.Forms.GroupBox grpStrength;
        private System.Windows.Forms.Label lblKa;
        private System.Windows.Forms.TextBox txtKa;
        private System.Windows.Forms.Label lblZe;
        private System.Windows.Forms.TextBox txtZe;
        private System.Windows.Forms.Label lblSigmaH;
        private System.Windows.Forms.TextBox txtSigmaH;
        private System.Windows.Forms.Label lblSigmaHUnit;
        private System.Windows.Forms.Label lblSigmaF;
        private System.Windows.Forms.TextBox txtSigmaF;
        private System.Windows.Forms.Label lblSigmaFUnit;
        private System.Windows.Forms.Label lblYFa2;
        private System.Windows.Forms.TextBox txtYFa2;
        private System.Windows.Forms.Label lblYBeta;
        private System.Windows.Forms.TextBox txtYBeta;
        private System.Windows.Forms.Label lblK;
        private System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Label lblYAllow;
        private System.Windows.Forms.TextBox txtYAllow;
        private System.Windows.Forms.Label lblYAllowUnit;

        private System.Windows.Forms.GroupBox grpMaterialParam;
        private System.Windows.Forms.Label lblE;
        private System.Windows.Forms.TextBox txtE;
        private System.Windows.Forms.Label lblEUnit;
        private System.Windows.Forms.Label lblNu;
        private System.Windows.Forms.TextBox txtNu;
        private System.Windows.Forms.Label lblF;
        private System.Windows.Forms.TextBox txtF;

        private System.Windows.Forms.GroupBox grpHeat;
        private System.Windows.Forms.Label lblKHeat;
        private System.Windows.Forms.TextBox txtKHeat;
        private System.Windows.Forms.Label lblKHeatUnit;
        private System.Windows.Forms.Label lblAHeat;
        private System.Windows.Forms.TextBox txtAHeat;
        private System.Windows.Forms.Label lblAHeatUnit;
        private System.Windows.Forms.Label lblT1Oil;
        private System.Windows.Forms.TextBox txtT1Oil;
        private System.Windows.Forms.Label lblT1OilUnit;
        private System.Windows.Forms.Label lblT0Env;
        private System.Windows.Forms.TextBox txtT0Env;
        private System.Windows.Forms.Label lblT0EnvUnit;

        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnClear;

        // 结果选项卡
        private System.Windows.Forms.GroupBox grpGeometry;
        private System.Windows.Forms.Label lblD1;
        private System.Windows.Forms.TextBox txtD1;
        private System.Windows.Forms.Label lblD2;
        private System.Windows.Forms.TextBox txtD2;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Label lblGamma;
        private System.Windows.Forms.TextBox txtGamma;
        private System.Windows.Forms.Label lblGammaUnit;
        private System.Windows.Forms.Label lblPx;
        private System.Windows.Forms.TextBox txtPx;
        private System.Windows.Forms.Label lblL;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.Label lblDa1;
        private System.Windows.Forms.TextBox txtDa1;
        private System.Windows.Forms.Label lblDf1;
        private System.Windows.Forms.TextBox txtDf1;
        private System.Windows.Forms.Label lblDa2;
        private System.Windows.Forms.TextBox txtDa2;
        private System.Windows.Forms.Label lblDf2;
        private System.Windows.Forms.TextBox txtDf2;
        private System.Windows.Forms.Label lblB1;
        private System.Windows.Forms.TextBox txtB1;
        private System.Windows.Forms.Label lblB2;
        private System.Windows.Forms.TextBox txtB2;

        private System.Windows.Forms.GroupBox grpKinematics;
        private System.Windows.Forms.Label lblN2;
        private System.Windows.Forms.TextBox txtN2;
        private System.Windows.Forms.Label lblV1;
        private System.Windows.Forms.TextBox txtV1;
        private System.Windows.Forms.Label lblVs;
        private System.Windows.Forms.TextBox txtVs;
        private System.Windows.Forms.Label lblV2;
        private System.Windows.Forms.TextBox txtV2;
        private System.Windows.Forms.Label lblEta;
        private System.Windows.Forms.TextBox txtEta;

        private System.Windows.Forms.GroupBox grpForces;
        private System.Windows.Forms.Label lblT1;
        private System.Windows.Forms.TextBox txtT1;
        private System.Windows.Forms.Label lblT2;
        private System.Windows.Forms.TextBox txtT2;
        private System.Windows.Forms.Label lblFt1;
        private System.Windows.Forms.TextBox txtFt1;
        private System.Windows.Forms.Label lblFa1;
        private System.Windows.Forms.TextBox txtFa1;
        private System.Windows.Forms.Label lblFr1;
        private System.Windows.Forms.TextBox txtFr1;
        private System.Windows.Forms.Label lblFt2;
        private System.Windows.Forms.TextBox txtFt2;
        private System.Windows.Forms.Label lblFa2;
        private System.Windows.Forms.TextBox txtFa2;
        private System.Windows.Forms.Label lblFr2;
        private System.Windows.Forms.TextBox txtFr2;

        private System.Windows.Forms.GroupBox grpCheck;
        private System.Windows.Forms.Label lblContactTitle;
        private System.Windows.Forms.TextBox txtSigmaH_Calc;
        private System.Windows.Forms.Label lblContactStatus;
        private System.Windows.Forms.Label lblBendingTitle;
        private System.Windows.Forms.TextBox txtSigmaF_Calc;
        private System.Windows.Forms.Label lblBendingStatus;
        private System.Windows.Forms.Label lblHeatTitle;
        private System.Windows.Forms.TextBox txtQr;
        private System.Windows.Forms.TextBox txtQc;
        private System.Windows.Forms.Label lblHeatStatus;
        private System.Windows.Forms.Label lblStiffnessTitle;
        private System.Windows.Forms.TextBox txtDeflection;
        private System.Windows.Forms.Label lblStiffnessStatus;
        private System.Windows.Forms.Label lblOverall;

        private System.Windows.Forms.TextBox txtResult;
    }
}
