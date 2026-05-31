using System.Drawing;
using System.Windows.Forms;

namespace MDSolids
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
            this.tabControl = new TabControl();
            this.tabBending = new TabPage();
            this.tabTorsion = new TabPage();
            this.tabCombined = new TabPage();
            this.tabDeflection = new TabPage();
            this.tabSafety = new TabPage();

            // 材料选择面板
            this.pnlMaterial = new Panel();
            this.lblMaterial = new Label();
            this.cmbMaterial = new ComboBox();
            this.lblSigmaYield = new Label();
            this.txtSigmaYield = new TextBox();
            this.lblSigmaB = new Label();
            this.txtSigmaB = new TextBox();
            this.lblElasticModulus = new Label();
            this.txtElasticModulus = new TextBox();

            // === 弯曲应力 Tab ===
            this.lblBendingMoment = new Label();
            this.txtBendingMoment = new TextBox();
            this.lblBendingMomentUnit = new Label();
            this.grpSection = new GroupBox();
            this.rbRectSection = new RadioButton();
            this.rbCircleSection = new RadioButton();
            this.pnlRect = new Panel();
            this.lblRectB = new Label();
            this.txtRectB = new TextBox();
            this.lblRectH = new Label();
            this.txtRectH = new TextBox();
            this.pnlCircle = new Panel();
            this.lblCircleD = new Label();
            this.txtCircleD = new TextBox();
            this.lblSectionInertia = new Label();
            this.txtSectionInertia = new TextBox();
            this.lblSectionInertiaUnit = new Label();
            this.btnCalcBending = new Button();
            this.lblBendingResult = new Label();
            this.txtBendingResult = new TextBox();
            this.lblBendingResultUnit = new Label();

            // === 扭转应力 Tab ===
            this.lblTorque = new Label();
            this.txtTorque = new TextBox();
            this.lblTorqueUnit = new Label();
            this.lblTorsionD = new Label();
            this.txtTorsionD = new TextBox();
            this.lblTorsionDUnit = new Label();
            this.lblPolarInertia = new Label();
            this.txtPolarInertia = new TextBox();
            this.lblPolarInertiaUnit = new Label();
            this.btnCalcTorsion = new Button();
            this.lblTorsionResult = new Label();
            this.txtTorsionResult = new TextBox();
            this.lblTorsionResultUnit = new Label();

            // === 组合应力 Tab ===
            this.lblNormalStress = new Label();
            this.txtNormalStress = new TextBox();
            this.lblNormalStressUnit = new Label();
            this.lblShearStress = new Label();
            this.txtShearStress = new TextBox();
            this.lblShearStressUnit = new Label();
            this.btnCalcVonMises = new Button();
            this.lblVonMisesResult = new Label();
            this.txtVonMisesResult = new TextBox();
            this.lblVonMisesResultUnit = new Label();
            this.lblSafetyFactor = new Label();
            this.txtSafetyFactor = new TextBox();

            // === 挠度计算 Tab ===
            this.grpBeamType = new GroupBox();
            this.rbCantilever = new RadioButton();
            this.rbDistributed = new RadioButton();
            this.lblDeflectionF = new Label();
            this.txtDeflectionF = new TextBox();
            this.lblDeflectionFUnit = new Label();
            this.lblDistributedLoad = new Label();
            this.txtDistributedLoad = new TextBox();
            this.lblDistributedLoadUnit = new Label();
            this.lblDeflectionL = new Label();
            this.txtDeflectionL = new TextBox();
            this.lblDeflectionLUnit = new Label();
            this.lblDeflectionE = new Label();
            this.txtDeflectionE = new TextBox();
            this.lblDeflectionEUnit = new Label();
            this.lblDeflectionI = new Label();
            this.txtDeflectionI = new TextBox();
            this.lblDeflectionIUnit = new Label();
            this.btnCalcDeflection = new Button();
            this.lblDeflectionResult = new Label();
            this.txtDeflectionResult = new TextBox();
            this.lblDeflectionResultUnit = new Label();
            this.pnlPointLoad = new Panel();
            this.pnlDistLoad = new Panel();

            // === 安全系数 Tab ===
            this.lblSafetyYield = new Label();
            this.txtSafetyYield = new TextBox();
            this.lblSafetyYieldUnit = new Label();
            this.lblSafetyVM = new Label();
            this.txtSafetyVM = new TextBox();
            this.lblSafetyVMUnit = new Label();
            this.btnCalcSafety = new Button();
            this.lblSafetyResultLabel = new Label();
            this.txtSafetyResult = new TextBox();
            this.lblSafetyJudgment = new Label();

            // 状态栏
            this.statusStrip = new StatusStrip();
            this.toolStripStatusLabel = new ToolStripStatusLabel();

            this.tabControl.SuspendLayout();
            this.tabBending.SuspendLayout();
            this.tabTorsion.SuspendLayout();
            this.tabCombined.SuspendLayout();
            this.tabDeflection.SuspendLayout();
            this.tabSafety.SuspendLayout();
            this.pnlMaterial.SuspendLayout();
            this.grpSection.SuspendLayout();
            this.pnlRect.SuspendLayout();
            this.pnlCircle.SuspendLayout();
            this.grpBeamType.SuspendLayout();
            this.pnlPointLoad.SuspendLayout();
            this.pnlDistLoad.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // === tabControl ===
            this.tabControl.Controls.Add(this.tabBending);
            this.tabControl.Controls.Add(this.tabTorsion);
            this.tabControl.Controls.Add(this.tabCombined);
            this.tabControl.Controls.Add(this.tabDeflection);
            this.tabControl.Controls.Add(this.tabSafety);
            this.tabControl.Location = new Point(12, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(660, 420);
            this.tabControl.TabIndex = 0;

            // === pnlMaterial (顶部材料选择) ===
            this.pnlMaterial.BorderStyle = BorderStyle.FixedSingle;
            this.pnlMaterial.Controls.Add(this.lblMaterial);
            this.pnlMaterial.Controls.Add(this.cmbMaterial);
            this.pnlMaterial.Controls.Add(this.lblSigmaYield);
            this.pnlMaterial.Controls.Add(this.txtSigmaYield);
            this.pnlMaterial.Controls.Add(this.lblSigmaB);
            this.pnlMaterial.Controls.Add(this.txtSigmaB);
            this.pnlMaterial.Controls.Add(this.lblElasticModulus);
            this.pnlMaterial.Controls.Add(this.txtElasticModulus);
            this.pnlMaterial.Location = new Point(12, 8);
            this.pnlMaterial.Name = "pnlMaterial";
            this.pnlMaterial.Size = new Size(660, 46);
            this.pnlMaterial.TabIndex = 1;

            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Location = new Point(8, 14);
            this.lblMaterial.Text = "材料:";

            this.cmbMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbMaterial.Location = new Point(45, 10);
            this.cmbMaterial.Size = new Size(150, 21);
            this.cmbMaterial.SelectedIndexChanged += new System.EventHandler(this.cmbMaterial_SelectedIndexChanged);

            this.lblSigmaYield.AutoSize = true;
            this.lblSigmaYield.Location = new Point(210, 14);
            this.lblSigmaYield.Text = "σs(MPa):";

            this.txtSigmaYield.Location = new Point(270, 10);
            this.txtSigmaYield.Size = new Size(55, 21);
            this.txtSigmaYield.ReadOnly = true;
            this.txtSigmaYield.BackColor = SystemColors.Control;

            this.lblSigmaB.AutoSize = true;
            this.lblSigmaB.Location = new Point(335, 14);
            this.lblSigmaB.Text = "σb(MPa):";

            this.txtSigmaB.Location = new Point(395, 10);
            this.txtSigmaB.Size = new Size(55, 21);
            this.txtSigmaB.ReadOnly = true;
            this.txtSigmaB.BackColor = SystemColors.Control;

            this.lblElasticModulus.AutoSize = true;
            this.lblElasticModulus.Location = new Point(460, 14);
            this.lblElasticModulus.Text = "E(MPa):";

            this.txtElasticModulus.Location = new Point(515, 10);
            this.txtElasticModulus.Size = new Size(70, 21);
            this.txtElasticModulus.ReadOnly = true;
            this.txtElasticModulus.BackColor = SystemColors.Control;

            // =============================================
            // tabBending - 弯曲应力计算
            // =============================================
            this.tabBending.Text = "弯曲应力";
            this.tabBending.Padding = new Padding(10);
            this.tabBending.Controls.Add(this.lblBendingMoment);
            this.tabBending.Controls.Add(this.txtBendingMoment);
            this.tabBending.Controls.Add(this.lblBendingMomentUnit);
            this.tabBending.Controls.Add(this.grpSection);
            this.tabBending.Controls.Add(this.lblSectionInertia);
            this.tabBending.Controls.Add(this.txtSectionInertia);
            this.tabBending.Controls.Add(this.lblSectionInertiaUnit);
            this.tabBending.Controls.Add(this.btnCalcBending);
            this.tabBending.Controls.Add(this.lblBendingResult);
            this.tabBending.Controls.Add(this.txtBendingResult);
            this.tabBending.Controls.Add(this.lblBendingResultUnit);

            this.lblBendingMoment.AutoSize = true;
            this.lblBendingMoment.Location = new Point(15, 20);
            this.lblBendingMoment.Text = "弯矩 M:";

            this.txtBendingMoment.Location = new Point(80, 16);
            this.txtBendingMoment.Size = new Size(100, 21);

            this.lblBendingMomentUnit.AutoSize = true;
            this.lblBendingMomentUnit.Location = new Point(185, 20);
            this.lblBendingMomentUnit.Text = "N·mm";

            // grpSection - 截面选择
            this.grpSection.Text = "截面类型";
            this.grpSection.Location = new Point(15, 50);
            this.grpSection.Size = new Size(620, 130);
            this.grpSection.Controls.Add(this.rbRectSection);
            this.grpSection.Controls.Add(this.rbCircleSection);
            this.grpSection.Controls.Add(this.pnlRect);
            this.grpSection.Controls.Add(this.pnlCircle);

            this.rbRectSection.AutoSize = true;
            this.rbRectSection.Location = new Point(15, 25);
            this.rbRectSection.Text = "矩形截面";
            this.rbRectSection.Checked = true;
            this.rbRectSection.TabIndex = 0;
            this.rbRectSection.CheckedChanged += new System.EventHandler(this.rbRectSection_CheckedChanged);

            this.rbCircleSection.AutoSize = true;
            this.rbCircleSection.Location = new Point(300, 25);
            this.rbCircleSection.Text = "圆形截面";
            this.rbCircleSection.TabIndex = 1;
            this.rbCircleSection.CheckedChanged += new System.EventHandler(this.rbCircleSection_CheckedChanged);

            // pnlRect
            this.pnlRect.Location = new Point(15, 52);
            this.pnlRect.Size = new Size(260, 65);
            this.pnlRect.Controls.Add(this.lblRectB);
            this.pnlRect.Controls.Add(this.txtRectB);
            this.pnlRect.Controls.Add(this.lblRectH);
            this.pnlRect.Controls.Add(this.txtRectH);

            this.lblRectB.AutoSize = true;
            this.lblRectB.Location = new Point(5, 10);
            this.lblRectB.Text = "宽度 b (mm):";

            this.txtRectB.Location = new Point(85, 6);
            this.txtRectB.Size = new Size(80, 21);
            this.txtRectB.Text = "50";

            this.lblRectH.AutoSize = true;
            this.lblRectH.Location = new Point(5, 38);
            this.lblRectH.Text = "高度 h (mm):";

            this.txtRectH.Location = new Point(85, 34);
            this.txtRectH.Size = new Size(80, 21);
            this.txtRectH.Text = "100";

            // pnlCircle
            this.pnlCircle.Location = new Point(300, 52);
            this.pnlCircle.Size = new Size(260, 65);
            this.pnlCircle.Enabled = false;
            this.pnlCircle.Controls.Add(this.lblCircleD);
            this.pnlCircle.Controls.Add(this.txtCircleD);

            this.lblCircleD.AutoSize = true;
            this.lblCircleD.Location = new Point(5, 10);
            this.lblCircleD.Text = "直径 d (mm):";

            this.txtCircleD.Location = new Point(85, 6);
            this.txtCircleD.Size = new Size(80, 21);
            this.txtCircleD.Text = "50";

            // 惯性矩显示
            this.lblSectionInertia.AutoSize = true;
            this.lblSectionInertia.Location = new Point(15, 195);
            this.lblSectionInertia.Text = "截面惯性矩 I:";

            this.txtSectionInertia.Location = new Point(110, 191);
            this.txtSectionInertia.Size = new Size(120, 21);
            this.txtSectionInertia.ReadOnly = true;
            this.txtSectionInertia.BackColor = SystemColors.Control;

            this.lblSectionInertiaUnit.AutoSize = true;
            this.lblSectionInertiaUnit.Location = new Point(235, 195);
            this.lblSectionInertiaUnit.Text = "mm^4";

            // 计算按钮和结果
            this.btnCalcBending.Location = new Point(15, 230);
            this.btnCalcBending.Size = new Size(120, 35);
            this.btnCalcBending.Text = "计算弯曲应力";
            this.btnCalcBending.UseVisualStyleBackColor = true;
            this.btnCalcBending.Click += new System.EventHandler(this.btnCalcBending_Click);

            this.lblBendingResult.AutoSize = true;
            this.lblBendingResult.Location = new Point(15, 285);
            this.lblBendingResult.Text = "弯曲应力 σ:";
            this.lblBendingResult.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.txtBendingResult.Location = new Point(110, 281);
            this.txtBendingResult.Size = new Size(120, 21);
            this.txtBendingResult.ReadOnly = true;
            this.txtBendingResult.BackColor = Color.LightYellow;
            this.txtBendingResult.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.lblBendingResultUnit.AutoSize = true;
            this.lblBendingResultUnit.Location = new Point(235, 285);
            this.lblBendingResultUnit.Text = "MPa";

            // =============================================
            // tabTorsion - 扭转应力计算
            // =============================================
            this.tabTorsion.Text = "扭转应力";
            this.tabTorsion.Padding = new Padding(10);
            this.tabTorsion.Controls.Add(this.lblTorque);
            this.tabTorsion.Controls.Add(this.txtTorque);
            this.tabTorsion.Controls.Add(this.lblTorqueUnit);
            this.tabTorsion.Controls.Add(this.lblTorsionD);
            this.tabTorsion.Controls.Add(this.txtTorsionD);
            this.tabTorsion.Controls.Add(this.lblTorsionDUnit);
            this.tabTorsion.Controls.Add(this.lblPolarInertia);
            this.tabTorsion.Controls.Add(this.txtPolarInertia);
            this.tabTorsion.Controls.Add(this.lblPolarInertiaUnit);
            this.tabTorsion.Controls.Add(this.btnCalcTorsion);
            this.tabTorsion.Controls.Add(this.lblTorsionResult);
            this.tabTorsion.Controls.Add(this.txtTorsionResult);
            this.tabTorsion.Controls.Add(this.lblTorsionResultUnit);

            this.lblTorque.AutoSize = true;
            this.lblTorque.Location = new Point(15, 25);
            this.lblTorque.Text = "扭矩 T:";

            this.txtTorque.Location = new Point(80, 21);
            this.txtTorque.Size = new Size(100, 21);
            this.txtTorque.Text = "1000";

            this.lblTorqueUnit.AutoSize = true;
            this.lblTorqueUnit.Location = new Point(185, 25);
            this.lblTorqueUnit.Text = "N·mm";

            this.lblTorsionD.AutoSize = true;
            this.lblTorsionD.Location = new Point(15, 60);
            this.lblTorsionD.Text = "直径 d:";

            this.txtTorsionD.Location = new Point(80, 56);
            this.txtTorsionD.Size = new Size(100, 21);
            this.txtTorsionD.Text = "40";

            this.lblTorsionDUnit.AutoSize = true;
            this.lblTorsionDUnit.Location = new Point(185, 60);
            this.lblTorsionDUnit.Text = "mm";

            this.lblPolarInertia.AutoSize = true;
            this.lblPolarInertia.Location = new Point(15, 100);
            this.lblPolarInertia.Text = "极惯性矩 J:";

            this.txtPolarInertia.Location = new Point(100, 96);
            this.txtPolarInertia.Size = new Size(120, 21);
            this.txtPolarInertia.ReadOnly = true;
            this.txtPolarInertia.BackColor = SystemColors.Control;

            this.lblPolarInertiaUnit.AutoSize = true;
            this.lblPolarInertiaUnit.Location = new Point(225, 100);
            this.lblPolarInertiaUnit.Text = "mm^4";

            this.btnCalcTorsion.Location = new Point(15, 140);
            this.btnCalcTorsion.Size = new Size(120, 35);
            this.btnCalcTorsion.Text = "计算扭转应力";
            this.btnCalcTorsion.UseVisualStyleBackColor = true;
            this.btnCalcTorsion.Click += new System.EventHandler(this.btnCalcTorsion_Click);

            this.lblTorsionResult.AutoSize = true;
            this.lblTorsionResult.Location = new Point(15, 200);
            this.lblTorsionResult.Text = "切应力 τ:";
            this.lblTorsionResult.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.txtTorsionResult.Location = new Point(100, 196);
            this.txtTorsionResult.Size = new Size(120, 21);
            this.txtTorsionResult.ReadOnly = true;
            this.txtTorsionResult.BackColor = Color.LightYellow;
            this.txtTorsionResult.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.lblTorsionResultUnit.AutoSize = true;
            this.lblTorsionResultUnit.Location = new Point(225, 200);
            this.lblTorsionResultUnit.Text = "MPa";

            // 公式说明
            var lblTorsionFormula = new Label();
            lblTorsionFormula.Location = new Point(300, 25);
            lblTorsionFormula.Size = new Size(300, 120);
            lblTorsionFormula.Text = "扭转切应力公式:\n\nτ = T·c / J\n\n其中:\nT = 扭矩 (N·mm)\nc = 半径 = d/2 (mm)\nJ = π·d⁴/32 (mm⁴)";
            lblTorsionFormula.ForeColor = Color.DarkBlue;
            this.tabTorsion.Controls.Add(lblTorsionFormula);

            // =============================================
            // tabCombined - 组合应力 (Von Mises)
            // =============================================
            this.tabCombined.Text = "组合应力";
            this.tabCombined.Padding = new Padding(10);
            this.tabCombined.Controls.Add(this.lblNormalStress);
            this.tabCombined.Controls.Add(this.txtNormalStress);
            this.tabCombined.Controls.Add(this.lblNormalStressUnit);
            this.tabCombined.Controls.Add(this.lblShearStress);
            this.tabCombined.Controls.Add(this.txtShearStress);
            this.tabCombined.Controls.Add(this.lblShearStressUnit);
            this.tabCombined.Controls.Add(this.btnCalcVonMises);
            this.tabCombined.Controls.Add(this.lblVonMisesResult);
            this.tabCombined.Controls.Add(this.txtVonMisesResult);
            this.tabCombined.Controls.Add(this.lblVonMisesResultUnit);
            this.tabCombined.Controls.Add(this.lblSafetyFactor);
            this.tabCombined.Controls.Add(this.txtSafetyFactor);

            this.lblNormalStress.AutoSize = true;
            this.lblNormalStress.Location = new Point(15, 25);
            this.lblNormalStress.Text = "正应力 σ:";

            this.txtNormalStress.Location = new Point(100, 21);
            this.txtNormalStress.Size = new Size(100, 21);
            this.txtNormalStress.Text = "100";

            this.lblNormalStressUnit.AutoSize = true;
            this.lblNormalStressUnit.Location = new Point(205, 25);
            this.lblNormalStressUnit.Text = "MPa";

            this.lblShearStress.AutoSize = true;
            this.lblShearStress.Location = new Point(15, 60);
            this.lblShearStress.Text = "切应力 τ:";

            this.txtShearStress.Location = new Point(100, 56);
            this.txtShearStress.Size = new Size(100, 21);
            this.txtShearStress.Text = "50";

            this.lblShearStressUnit.AutoSize = true;
            this.lblShearStressUnit.Location = new Point(205, 60);
            this.lblShearStressUnit.Text = "MPa";

            this.btnCalcVonMises.Location = new Point(15, 100);
            this.btnCalcVonMises.Size = new Size(150, 35);
            this.btnCalcVonMises.Text = "计算Von Mises应力";
            this.btnCalcVonMises.UseVisualStyleBackColor = true;
            this.btnCalcVonMises.Click += new System.EventHandler(this.btnCalcVonMises_Click);

            this.lblVonMisesResult.AutoSize = true;
            this.lblVonMisesResult.Location = new Point(15, 160);
            this.lblVonMisesResult.Text = "等效应力 σ_vm:";
            this.lblVonMisesResult.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.txtVonMisesResult.Location = new Point(120, 156);
            this.txtVonMisesResult.Size = new Size(120, 21);
            this.txtVonMisesResult.ReadOnly = true;
            this.txtVonMisesResult.BackColor = Color.LightYellow;
            this.txtVonMisesResult.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.lblVonMisesResultUnit.AutoSize = true;
            this.lblVonMisesResultUnit.Location = new Point(245, 160);
            this.lblVonMisesResultUnit.Text = "MPa";

            this.lblSafetyFactor.AutoSize = true;
            this.lblSafetyFactor.Location = new Point(15, 200);
            this.lblSafetyFactor.Text = "安全系数 n:";
            this.lblSafetyFactor.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.txtSafetyFactor.Location = new Point(120, 196);
            this.txtSafetyFactor.Size = new Size(120, 21);
            this.txtSafetyFactor.ReadOnly = true;
            this.txtSafetyFactor.BackColor = Color.LightYellow;
            this.txtSafetyFactor.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            // 公式说明
            var lblCombinedFormula = new Label();
            lblCombinedFormula.Location = new Point(300, 25);
            lblCombinedFormula.Size = new Size(300, 120);
            lblCombinedFormula.Text = "Von Mises 等效应力:\n\nσ_vm = √(σ² + 3τ²)\n\n安全系数:\nn = σs / σ_vm\n\n自动使用材料库中的\n屈服强度计算安全系数";
            lblCombinedFormula.ForeColor = Color.DarkBlue;
            this.tabCombined.Controls.Add(lblCombinedFormula);

            // =============================================
            // tabDeflection - 挠度计算
            // =============================================
            this.tabDeflection.Text = "挠度计算";
            this.tabDeflection.Padding = new Padding(10);
            this.tabDeflection.Controls.Add(this.grpBeamType);
            this.tabDeflection.Controls.Add(this.pnlPointLoad);
            this.tabDeflection.Controls.Add(this.pnlDistLoad);
            this.tabDeflection.Controls.Add(this.lblDeflectionL);
            this.tabDeflection.Controls.Add(this.txtDeflectionL);
            this.tabDeflection.Controls.Add(this.lblDeflectionLUnit);
            this.tabDeflection.Controls.Add(this.lblDeflectionE);
            this.tabDeflection.Controls.Add(this.txtDeflectionE);
            this.tabDeflection.Controls.Add(this.lblDeflectionEUnit);
            this.tabDeflection.Controls.Add(this.lblDeflectionI);
            this.tabDeflection.Controls.Add(this.txtDeflectionI);
            this.tabDeflection.Controls.Add(this.lblDeflectionIUnit);
            this.tabDeflection.Controls.Add(this.btnCalcDeflection);
            this.tabDeflection.Controls.Add(this.lblDeflectionResult);
            this.tabDeflection.Controls.Add(this.txtDeflectionResult);
            this.tabDeflection.Controls.Add(this.lblDeflectionResultUnit);

            // grpBeamType
            this.grpBeamType.Text = "梁类型";
            this.grpBeamType.Location = new Point(15, 10);
            this.grpBeamType.Size = new Size(280, 55);
            this.grpBeamType.Controls.Add(this.rbCantilever);
            this.grpBeamType.Controls.Add(this.rbDistributed);

            this.rbCantilever.AutoSize = true;
            this.rbCantilever.Location = new Point(15, 24);
            this.rbCantilever.Text = "悬臂梁(集中力)";
            this.rbCantilever.Checked = true;
            this.rbCantilever.CheckedChanged += new System.EventHandler(this.rbCantilever_CheckedChanged);

            this.rbDistributed.AutoSize = true;
            this.rbDistributed.Location = new Point(140, 24);
            this.rbDistributed.Text = "简支梁(均布载荷)";
            this.rbDistributed.CheckedChanged += new System.EventHandler(this.rbDistributed_CheckedChanged);

            // pnlPointLoad - 集中力输入
            this.pnlPointLoad.Location = new Point(15, 75);
            this.pnlPointLoad.Size = new Size(280, 35);
            this.pnlPointLoad.Controls.Add(this.lblDeflectionF);
            this.pnlPointLoad.Controls.Add(this.txtDeflectionF);
            this.pnlPointLoad.Controls.Add(this.lblDeflectionFUnit);

            this.lblDeflectionF.AutoSize = true;
            this.lblDeflectionF.Location = new Point(5, 8);
            this.lblDeflectionF.Text = "集中力 F:";

            this.txtDeflectionF.Location = new Point(75, 4);
            this.txtDeflectionF.Size = new Size(100, 21);
            this.txtDeflectionF.Text = "1000";

            this.lblDeflectionFUnit.AutoSize = true;
            this.lblDeflectionFUnit.Location = new Point(180, 8);
            this.lblDeflectionFUnit.Text = "N";

            // pnlDistLoad - 均布载荷输入
            this.pnlDistLoad.Location = new Point(15, 75);
            this.pnlDistLoad.Size = new Size(280, 35);
            this.pnlDistLoad.Enabled = false;
            this.pnlDistLoad.Visible = false;
            this.pnlDistLoad.Controls.Add(this.lblDistributedLoad);
            this.pnlDistLoad.Controls.Add(this.txtDistributedLoad);
            this.pnlDistLoad.Controls.Add(this.lblDistributedLoadUnit);

            this.lblDistributedLoad.AutoSize = true;
            this.lblDistributedLoad.Location = new Point(5, 8);
            this.lblDistributedLoad.Text = "均布载荷 w:";

            this.txtDistributedLoad.Location = new Point(85, 4);
            this.txtDistributedLoad.Size = new Size(100, 21);
            this.txtDistributedLoad.Text = "10";

            this.lblDistributedLoadUnit.AutoSize = true;
            this.lblDistributedLoadUnit.Location = new Point(190, 8);
            this.lblDistributedLoadUnit.Text = "N/mm";

            // 公共参数
            this.lblDeflectionL.AutoSize = true;
            this.lblDeflectionL.Location = new Point(15, 125);
            this.lblDeflectionL.Text = "梁长 L:";

            this.txtDeflectionL.Location = new Point(80, 121);
            this.txtDeflectionL.Size = new Size(100, 21);
            this.txtDeflectionL.Text = "500";

            this.lblDeflectionLUnit.AutoSize = true;
            this.lblDeflectionLUnit.Location = new Point(185, 125);
            this.lblDeflectionLUnit.Text = "mm";

            this.lblDeflectionE.AutoSize = true;
            this.lblDeflectionE.Location = new Point(15, 160);
            this.lblDeflectionE.Text = "弹性模量 E:";

            this.txtDeflectionE.Location = new Point(100, 156);
            this.txtDeflectionE.Size = new Size(100, 21);
            this.txtDeflectionE.Text = "206000";

            this.lblDeflectionEUnit.AutoSize = true;
            this.lblDeflectionEUnit.Location = new Point(205, 160);
            this.lblDeflectionEUnit.Text = "MPa";

            this.lblDeflectionI.AutoSize = true;
            this.lblDeflectionI.Location = new Point(15, 195);
            this.lblDeflectionI.Text = "惯性矩 I:";

            this.txtDeflectionI.Location = new Point(100, 191);
            this.txtDeflectionI.Size = new Size(100, 21);
            this.txtDeflectionI.Text = "4166666.67";

            this.lblDeflectionIUnit.AutoSize = true;
            this.lblDeflectionIUnit.Location = new Point(205, 195);
            this.lblDeflectionIUnit.Text = "mm^4";

            this.btnCalcDeflection.Location = new Point(15, 235);
            this.btnCalcDeflection.Size = new Size(120, 35);
            this.btnCalcDeflection.Text = "计算挠度";
            this.btnCalcDeflection.UseVisualStyleBackColor = true;
            this.btnCalcDeflection.Click += new System.EventHandler(this.btnCalcDeflection_Click);

            this.lblDeflectionResult.AutoSize = true;
            this.lblDeflectionResult.Location = new Point(15, 295);
            this.lblDeflectionResult.Text = "最大挠度 δ:";
            this.lblDeflectionResult.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.txtDeflectionResult.Location = new Point(110, 291);
            this.txtDeflectionResult.Size = new Size(120, 21);
            this.txtDeflectionResult.ReadOnly = true;
            this.txtDeflectionResult.BackColor = Color.LightYellow;
            this.txtDeflectionResult.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);

            this.lblDeflectionResultUnit.AutoSize = true;
            this.lblDeflectionResultUnit.Location = new Point(235, 295);
            this.lblDeflectionResultUnit.Text = "mm";

            // 公式说明
            var lblDeflectionFormula = new Label();
            lblDeflectionFormula.Location = new Point(320, 15);
            lblDeflectionFormula.Size = new Size(310, 250);
            lblDeflectionFormula.Text =
                "挠度计算公式:\n\n" +
                "悬臂梁(端部集中力):\n" +
                "  δ = F·L³ / (3·E·I)\n\n" +
                "简支梁(均布载荷):\n" +
                "  δ = 5·w·L⁴ / (384·E·I)\n\n" +
                "其中:\n" +
                "F = 集中力 (N)\n" +
                "w = 均布载荷 (N/mm)\n" +
                "L = 梁长 (mm)\n" +
                "E = 弹性模量 (MPa)\n" +
                "I = 截面惯性矩 (mm⁴)";
            lblDeflectionFormula.ForeColor = Color.DarkBlue;
            this.tabDeflection.Controls.Add(lblDeflectionFormula);

            // =============================================
            // tabSafety - 安全系数计算
            // =============================================
            this.tabSafety.Text = "安全系数";
            this.tabSafety.Padding = new Padding(10);
            this.tabSafety.Controls.Add(this.lblSafetyYield);
            this.tabSafety.Controls.Add(this.txtSafetyYield);
            this.tabSafety.Controls.Add(this.lblSafetyYieldUnit);
            this.tabSafety.Controls.Add(this.lblSafetyVM);
            this.tabSafety.Controls.Add(this.txtSafetyVM);
            this.tabSafety.Controls.Add(this.lblSafetyVMUnit);
            this.tabSafety.Controls.Add(this.btnCalcSafety);
            this.tabSafety.Controls.Add(this.lblSafetyResultLabel);
            this.tabSafety.Controls.Add(this.txtSafetyResult);
            this.tabSafety.Controls.Add(this.lblSafetyJudgment);

            this.lblSafetyYield.AutoSize = true;
            this.lblSafetyYield.Location = new Point(15, 25);
            this.lblSafetyYield.Text = "屈服强度 σs:";

            this.txtSafetyYield.Location = new Point(110, 21);
            this.txtSafetyYield.Size = new Size(100, 21);
            this.txtSafetyYield.Text = "235";

            this.lblSafetyYieldUnit.AutoSize = true;
            this.lblSafetyYieldUnit.Location = new Point(215, 25);
            this.lblSafetyYieldUnit.Text = "MPa";

            this.lblSafetyVM.AutoSize = true;
            this.lblSafetyVM.Location = new Point(15, 60);
            this.lblSafetyVM.Text = "等效应力 σ_vm:";

            this.txtSafetyVM.Location = new Point(110, 56);
            this.txtSafetyVM.Size = new Size(100, 21);
            this.txtSafetyVM.Text = "150";

            this.lblSafetyVMUnit.AutoSize = true;
            this.lblSafetyVMUnit.Location = new Point(215, 60);
            this.lblSafetyVMUnit.Text = "MPa";

            this.btnCalcSafety.Location = new Point(15, 100);
            this.btnCalcSafety.Size = new Size(120, 35);
            this.btnCalcSafety.Text = "计算安全系数";
            this.btnCalcSafety.UseVisualStyleBackColor = true;
            this.btnCalcSafety.Click += new System.EventHandler(this.btnCalcSafety_Click);

            this.lblSafetyResultLabel.AutoSize = true;
            this.lblSafetyResultLabel.Location = new Point(15, 160);
            this.lblSafetyResultLabel.Text = "安全系数 n:";
            this.lblSafetyResultLabel.Font = new Font("Microsoft YaHei", 11F, FontStyle.Bold);

            this.txtSafetyResult.Location = new Point(120, 158);
            this.txtSafetyResult.Size = new Size(100, 25);
            this.txtSafetyResult.ReadOnly = true;
            this.txtSafetyResult.BackColor = Color.LightYellow;
            this.txtSafetyResult.Font = new Font("Microsoft YaHei", 11F, FontStyle.Bold);

            this.lblSafetyJudgment.AutoSize = true;
            this.lblSafetyJudgment.Location = new Point(15, 200);
            this.lblSafetyJudgment.Size = new Size(200, 25);
            this.lblSafetyJudgment.Text = "";
            this.lblSafetyJudgment.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold);

            // 公式说明
            var lblSafetyFormula = new Label();
            lblSafetyFormula.Location = new Point(300, 25);
            lblSafetyFormula.Size = new Size(300, 200);
            lblSafetyFormula.Text =
                "安全系数计算:\n\n" +
                "n = σs / σ_vm\n\n" +
                "判定标准:\n" +
                "  n >= 2.0 : 安全\n" +
                "  1.5 <= n < 2.0 : 注意\n" +
                "  1.0 <= n < 1.5 : 警告\n" +
                "  n < 1.0 : 危险!\n\n" +
                "注意: 铸铁等脆性材料\n" +
                "使用抗拉强度 σb 代替 σs";
            lblSafetyFormula.ForeColor = Color.DarkBlue;
            this.tabSafety.Controls.Add(lblSafetyFormula);

            // === statusStrip ===
            this.statusStrip.Items.Add(this.toolStripStatusLabel);
            this.toolStripStatusLabel.Text = "力学分析模块 - 就绪";

            // === MainForm ===
            this.AutoScaleDimensions = new SizeF(7F, 17F);
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(684, 511);
            this.Controls.Add(this.pnlMaterial);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Font = new Font("Microsoft YaHei", 9F);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new Size(700, 550);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "力学分析";

            this.tabControl.ResumeLayout(false);
            this.tabBending.ResumeLayout(false);
            this.tabBending.PerformLayout();
            this.tabTorsion.ResumeLayout(false);
            this.tabTorsion.PerformLayout();
            this.tabCombined.ResumeLayout(false);
            this.tabCombined.PerformLayout();
            this.tabDeflection.ResumeLayout(false);
            this.tabDeflection.PerformLayout();
            this.tabSafety.ResumeLayout(false);
            this.tabSafety.PerformLayout();
            this.pnlMaterial.ResumeLayout(false);
            this.pnlMaterial.PerformLayout();
            this.grpSection.ResumeLayout(false);
            this.grpSection.PerformLayout();
            this.pnlRect.ResumeLayout(false);
            this.pnlRect.PerformLayout();
            this.pnlCircle.ResumeLayout(false);
            this.pnlCircle.PerformLayout();
            this.grpBeamType.ResumeLayout(false);
            this.grpBeamType.PerformLayout();
            this.pnlPointLoad.ResumeLayout(false);
            this.pnlPointLoad.PerformLayout();
            this.pnlDistLoad.ResumeLayout(false);
            this.pnlDistLoad.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // 主框架
        private TabControl tabControl;
        private TabPage tabBending;
        private TabPage tabTorsion;
        private TabPage tabCombined;
        private TabPage tabDeflection;
        private TabPage tabSafety;

        // 材料选择
        private Panel pnlMaterial;
        private Label lblMaterial;
        private ComboBox cmbMaterial;
        private Label lblSigmaYield;
        private TextBox txtSigmaYield;
        private Label lblSigmaB;
        private TextBox txtSigmaB;
        private Label lblElasticModulus;
        private TextBox txtElasticModulus;

        // 弯曲应力
        private Label lblBendingMoment;
        private TextBox txtBendingMoment;
        private Label lblBendingMomentUnit;
        private GroupBox grpSection;
        private RadioButton rbRectSection;
        private RadioButton rbCircleSection;
        private Panel pnlRect;
        private Label lblRectB;
        private TextBox txtRectB;
        private Label lblRectH;
        private TextBox txtRectH;
        private Panel pnlCircle;
        private Label lblCircleD;
        private TextBox txtCircleD;
        private Label lblSectionInertia;
        private TextBox txtSectionInertia;
        private Label lblSectionInertiaUnit;
        private Button btnCalcBending;
        private Label lblBendingResult;
        private TextBox txtBendingResult;
        private Label lblBendingResultUnit;

        // 扭转应力
        private Label lblTorque;
        private TextBox txtTorque;
        private Label lblTorqueUnit;
        private Label lblTorsionD;
        private TextBox txtTorsionD;
        private Label lblTorsionDUnit;
        private Label lblPolarInertia;
        private TextBox txtPolarInertia;
        private Label lblPolarInertiaUnit;
        private Button btnCalcTorsion;
        private Label lblTorsionResult;
        private TextBox txtTorsionResult;
        private Label lblTorsionResultUnit;

        // 组合应力
        private Label lblNormalStress;
        private TextBox txtNormalStress;
        private Label lblNormalStressUnit;
        private Label lblShearStress;
        private TextBox txtShearStress;
        private Label lblShearStressUnit;
        private Button btnCalcVonMises;
        private Label lblVonMisesResult;
        private TextBox txtVonMisesResult;
        private Label lblVonMisesResultUnit;
        private Label lblSafetyFactor;
        private TextBox txtSafetyFactor;

        // 挠度计算
        private GroupBox grpBeamType;
        private RadioButton rbCantilever;
        private RadioButton rbDistributed;
        private Panel pnlPointLoad;
        private Label lblDeflectionF;
        private TextBox txtDeflectionF;
        private Label lblDeflectionFUnit;
        private Panel pnlDistLoad;
        private Label lblDistributedLoad;
        private TextBox txtDistributedLoad;
        private Label lblDistributedLoadUnit;
        private Label lblDeflectionL;
        private TextBox txtDeflectionL;
        private Label lblDeflectionLUnit;
        private Label lblDeflectionE;
        private TextBox txtDeflectionE;
        private Label lblDeflectionEUnit;
        private Label lblDeflectionI;
        private TextBox txtDeflectionI;
        private Label lblDeflectionIUnit;
        private Button btnCalcDeflection;
        private Label lblDeflectionResult;
        private TextBox txtDeflectionResult;
        private Label lblDeflectionResultUnit;

        // 安全系数
        private Label lblSafetyYield;
        private TextBox txtSafetyYield;
        private Label lblSafetyYieldUnit;
        private Label lblSafetyVM;
        private TextBox txtSafetyVM;
        private Label lblSafetyVMUnit;
        private Button btnCalcSafety;
        private Label lblSafetyResultLabel;
        private TextBox txtSafetyResult;
        private Label lblSafetyJudgment;

        // 状态栏
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
    }
}
