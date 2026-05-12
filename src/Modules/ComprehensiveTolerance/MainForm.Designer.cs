namespace ComprehensiveTolerance
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDimensional = new System.Windows.Forms.TabPage();
            this.tabGeometric = new System.Windows.Forms.TabPage();
            this.tabSurface = new System.Windows.Forms.TabPage();
            this.tabThread = new System.Windows.Forms.TabPage();

            // ===== 尺寸公差 Tab =====
            this.grpDimInput = new System.Windows.Forms.GroupBox();
            this.lblNominalSize = new System.Windows.Forms.Label();
            this.cboNominalSize = new System.Windows.Forms.ComboBox();
            this.lblToleranceGrade = new System.Windows.Forms.Label();
            this.cboToleranceGrade = new System.Windows.Forms.ComboBox();
            this.lblFitType = new System.Windows.Forms.Label();
            this.cboFitType = new System.Windows.Forms.ComboBox();
            this.lblBaseSystem = new System.Windows.Forms.Label();
            this.cboBaseSystem = new System.Windows.Forms.ComboBox();
            this.btnCalculateDimTol = new System.Windows.Forms.Button();

            this.grpDimResult = new System.Windows.Forms.GroupBox();
            this.lblToleranceValue = new System.Windows.Forms.Label();
            this.lblUpperDeviation = new System.Windows.Forms.Label();
            this.lblLowerDeviation = new System.Windows.Forms.Label();
            this.lblMaxSize = new System.Windows.Forms.Label();
            this.lblMinSize = new System.Windows.Forms.Label();

            // ===== 形位公差 Tab =====
            this.grpGdtInput = new System.Windows.Forms.GroupBox();
            this.lblGdtFeature = new System.Windows.Forms.Label();
            this.cboGdtFeature = new System.Windows.Forms.ComboBox();
            this.lblGdtToleranceValue = new System.Windows.Forms.Label();
            this.txtGdtToleranceValue = new System.Windows.Forms.TextBox();
            this.lblGdtNominalSize = new System.Windows.Forms.Label();
            this.txtGdtNominalSize = new System.Windows.Forms.TextBox();
            this.lblToleranceZone = new System.Windows.Forms.Label();
            this.cboToleranceZone = new System.Windows.Forms.ComboBox();
            this.lblMaterialCondition = new System.Windows.Forms.Label();
            this.cboMaterialCondition = new System.Windows.Forms.ComboBox();
            this.btnCalculateGdt = new System.Windows.Forms.Button();

            this.grpGdtInfo = new System.Windows.Forms.GroupBox();
            this.lblGdtSymbol = new System.Windows.Forms.Label();
            this.lblGdtCategory = new System.Windows.Forms.Label();
            this.lblGdtZoneType = new System.Windows.Forms.Label();
            this.lblGdtDescription = new System.Windows.Forms.Label();

            this.grpGdtResult = new System.Windows.Forms.GroupBox();
            this.lblGdtResultZone = new System.Windows.Forms.Label();
            this.lblGdtResultSize = new System.Windows.Forms.Label();
            this.lblGdtResultBonus = new System.Windows.Forms.Label();
            this.lblGdtResultTotal = new System.Windows.Forms.Label();
            this.lblGdtResultMMC = new System.Windows.Forms.Label();
            this.lblGdtResultLMC = new System.Windows.Forms.Label();

            // ===== 表面粗糙度 Tab =====
            this.grpSurfaceInput = new System.Windows.Forms.GroupBox();
            this.lblRaValue = new System.Windows.Forms.Label();
            this.cboRaValue = new System.Windows.Forms.ComboBox();
            this.lblRoughnessSymbol = new System.Windows.Forms.Label();
            this.cboRoughnessSymbol = new System.Windows.Forms.ComboBox();

            this.grpSurfaceResult = new System.Windows.Forms.GroupBox();
            this.lblRzValue = new System.Windows.Forms.Label();
            this.lblRqValue = new System.Windows.Forms.Label();
            this.lblRoughnessGrade = new System.Windows.Forms.Label();
            this.lblManufacturingMethod = new System.Windows.Forms.Label();
            this.lblSurfaceApplication = new System.Windows.Forms.Label();
            this.lblRoughnessDesc = new System.Windows.Forms.Label();

            this.grpRaConvert = new System.Windows.Forms.GroupBox();
            this.lblRaInput = new System.Windows.Forms.Label();
            this.txtRaInput = new System.Windows.Forms.TextBox();
            this.btnConvertRaRz = new System.Windows.Forms.Button();
            this.lblRzOutput = new System.Windows.Forms.Label();
            this.txtRzOutput = new System.Windows.Forms.TextBox();
            this.lblRqOutput = new System.Windows.Forms.Label();
            this.txtRqOutput = new System.Windows.Forms.TextBox();

            // ===== 螺纹公差 Tab =====
            this.grpThreadInput = new System.Windows.Forms.GroupBox();
            this.lblThreadSpec = new System.Windows.Forms.Label();
            this.cboThreadSpec = new System.Windows.Forms.ComboBox();
            this.lblPitchType = new System.Windows.Forms.Label();
            this.cboPitchType = new System.Windows.Forms.ComboBox();
            this.lblThreadTolerance = new System.Windows.Forms.Label();
            this.cboThreadTolerance = new System.Windows.Forms.ComboBox();
            this.btnCalculateThreadTol = new System.Windows.Forms.Button();

            this.grpThreadInfo = new System.Windows.Forms.GroupBox();
            this.lblThreadMajorDia = new System.Windows.Forms.Label();
            this.lblThreadPitch = new System.Windows.Forms.Label();
            this.lblThreadPitchDia = new System.Windows.Forms.Label();
            this.lblThreadMinorDia = new System.Windows.Forms.Label();
            this.lblThreadHeight = new System.Windows.Forms.Label();

            this.grpThreadResult = new System.Windows.Forms.GroupBox();
            this.lblThreadTolGrade = new System.Windows.Forms.Label();
            this.lblThreadMajorTol = new System.Windows.Forms.Label();
            this.lblThreadPitchTol = new System.Windows.Forms.Label();
            this.lblThreadMinorTol = new System.Windows.Forms.Label();
            this.lblThreadMajorRange = new System.Windows.Forms.Label();
            this.lblThreadPitchRange = new System.Windows.Forms.Label();
            this.lblThreadMinorRange = new System.Windows.Forms.Label();
            this.lblThreadFitInfo = new System.Windows.Forms.Label();

            this.tabControl.SuspendLayout();
            this.tabDimensional.SuspendLayout();
            this.tabGeometric.SuspendLayout();
            this.tabSurface.SuspendLayout();
            this.tabThread.SuspendLayout();
            this.SuspendLayout();

            // ===== tabControl =====
            this.tabControl.Controls.Add(this.tabDimensional);
            this.tabControl.Controls.Add(this.tabGeometric);
            this.tabControl.Controls.Add(this.tabSurface);
            this.tabControl.Controls.Add(this.tabThread);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(784, 561);

            // ===== tabDimensional (尺寸公差) =====
            this.tabDimensional.Text = "尺寸公差";
            this.tabDimensional.Padding = new System.Windows.Forms.Padding(3);
            this.tabDimensional.Controls.Add(this.grpDimResult);
            this.tabDimensional.Controls.Add(this.grpDimInput);

            // grpDimInput
            this.grpDimInput.Text = "输入参数";
            this.grpDimInput.Location = new System.Drawing.Point(10, 10);
            this.grpDimInput.Size = new System.Drawing.Size(750, 180);
            this.grpDimInput.Controls.Add(this.lblNominalSize);
            this.grpDimInput.Controls.Add(this.cboNominalSize);
            this.grpDimInput.Controls.Add(this.lblToleranceGrade);
            this.grpDimInput.Controls.Add(this.cboToleranceGrade);
            this.grpDimInput.Controls.Add(this.lblFitType);
            this.grpDimInput.Controls.Add(this.cboFitType);
            this.grpDimInput.Controls.Add(this.lblBaseSystem);
            this.grpDimInput.Controls.Add(this.cboBaseSystem);
            this.grpDimInput.Controls.Add(this.btnCalculateDimTol);

            this.lblNominalSize.Text = "公称尺寸 (mm):";
            this.lblNominalSize.Location = new System.Drawing.Point(15, 30);
            this.lblNominalSize.AutoSize = true;
            this.cboNominalSize.Location = new System.Drawing.Point(130, 27);
            this.cboNominalSize.Size = new System.Drawing.Size(120, 21);
            this.cboNominalSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblToleranceGrade.Text = "公差等级:";
            this.lblToleranceGrade.Location = new System.Drawing.Point(280, 30);
            this.lblToleranceGrade.AutoSize = true;
            this.cboToleranceGrade.Location = new System.Drawing.Point(370, 27);
            this.cboToleranceGrade.Size = new System.Drawing.Size(100, 21);
            this.cboToleranceGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblFitType.Text = "配合类型:";
            this.lblFitType.Location = new System.Drawing.Point(15, 70);
            this.lblFitType.AutoSize = true;
            this.cboFitType.Location = new System.Drawing.Point(130, 67);
            this.cboFitType.Size = new System.Drawing.Size(120, 21);
            this.cboFitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblBaseSystem.Text = "基准制:";
            this.lblBaseSystem.Location = new System.Drawing.Point(280, 70);
            this.lblBaseSystem.AutoSize = true;
            this.cboBaseSystem.Location = new System.Drawing.Point(370, 67);
            this.cboBaseSystem.Size = new System.Drawing.Size(100, 21);
            this.cboBaseSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnCalculateDimTol.Text = "计算";
            this.btnCalculateDimTol.Location = new System.Drawing.Point(130, 110);
            this.btnCalculateDimTol.Size = new System.Drawing.Size(100, 30);
            this.btnCalculateDimTol.Click += new System.EventHandler(this.btnCalculateDimTol_Click);

            // grpDimResult
            this.grpDimResult.Text = "计算结果";
            this.grpDimResult.Location = new System.Drawing.Point(10, 200);
            this.grpDimResult.Size = new System.Drawing.Size(750, 200);
            this.grpDimResult.Controls.Add(this.lblToleranceValue);
            this.grpDimResult.Controls.Add(this.lblUpperDeviation);
            this.grpDimResult.Controls.Add(this.lblLowerDeviation);
            this.grpDimResult.Controls.Add(this.lblMaxSize);
            this.grpDimResult.Controls.Add(this.lblMinSize);

            this.lblToleranceValue.Text = "公差值: --";
            this.lblToleranceValue.Location = new System.Drawing.Point(15, 30);
            this.lblToleranceValue.AutoSize = true;
            this.lblToleranceValue.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblUpperDeviation.Text = "上偏差 (es/EI): --";
            this.lblUpperDeviation.Location = new System.Drawing.Point(15, 60);
            this.lblUpperDeviation.AutoSize = true;
            this.lblLowerDeviation.Text = "下偏差 (ei/ES): --";
            this.lblLowerDeviation.Location = new System.Drawing.Point(15, 90);
            this.lblLowerDeviation.AutoSize = true;
            this.lblMaxSize.Text = "最大极限尺寸: --";
            this.lblMaxSize.Location = new System.Drawing.Point(15, 120);
            this.lblMaxSize.AutoSize = true;
            this.lblMinSize.Text = "最小极限尺寸: --";
            this.lblMinSize.Location = new System.Drawing.Point(15, 150);
            this.lblMinSize.AutoSize = true;

            // ===== tabGeometric (形位公差) =====
            this.tabGeometric.Text = "形位公差";
            this.tabGeometric.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeometric.Controls.Add(this.grpGdtResult);
            this.tabGeometric.Controls.Add(this.grpGdtInfo);
            this.tabGeometric.Controls.Add(this.grpGdtInput);

            // grpGdtInput
            this.grpGdtInput.Text = "输入参数";
            this.grpGdtInput.Location = new System.Drawing.Point(10, 10);
            this.grpGdtInput.Size = new System.Drawing.Size(370, 220);
            this.grpGdtInput.Controls.Add(this.lblGdtFeature);
            this.grpGdtInput.Controls.Add(this.cboGdtFeature);
            this.grpGdtInput.Controls.Add(this.lblGdtToleranceValue);
            this.grpGdtInput.Controls.Add(this.txtGdtToleranceValue);
            this.grpGdtInput.Controls.Add(this.lblGdtNominalSize);
            this.grpGdtInput.Controls.Add(this.txtGdtNominalSize);
            this.grpGdtInput.Controls.Add(this.lblToleranceZone);
            this.grpGdtInput.Controls.Add(this.cboToleranceZone);
            this.grpGdtInput.Controls.Add(this.lblMaterialCondition);
            this.grpGdtInput.Controls.Add(this.cboMaterialCondition);
            this.grpGdtInput.Controls.Add(this.btnCalculateGdt);

            this.lblGdtFeature.Text = "形位公差项目:";
            this.lblGdtFeature.Location = new System.Drawing.Point(15, 30);
            this.lblGdtFeature.AutoSize = true;
            this.cboGdtFeature.Location = new System.Drawing.Point(120, 27);
            this.cboGdtFeature.Size = new System.Drawing.Size(120, 21);
            this.cboGdtFeature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGdtFeature.SelectedIndexChanged += new System.EventHandler(this.cboGdtFeature_SelectedIndexChanged);

            this.lblGdtToleranceValue.Text = "公差值 (mm):";
            this.lblGdtToleranceValue.Location = new System.Drawing.Point(15, 65);
            this.lblGdtToleranceValue.AutoSize = true;
            this.txtGdtToleranceValue.Location = new System.Drawing.Point(120, 62);
            this.txtGdtToleranceValue.Size = new System.Drawing.Size(100, 21);

            this.lblGdtNominalSize.Text = "公称尺寸 (mm):";
            this.lblGdtNominalSize.Location = new System.Drawing.Point(15, 100);
            this.lblGdtNominalSize.AutoSize = true;
            this.txtGdtNominalSize.Location = new System.Drawing.Point(120, 97);
            this.txtGdtNominalSize.Size = new System.Drawing.Size(100, 21);
            this.txtGdtNominalSize.Text = "50";

            this.lblToleranceZone.Text = "公差带类型:";
            this.lblToleranceZone.Location = new System.Drawing.Point(15, 135);
            this.lblToleranceZone.AutoSize = true;
            this.cboToleranceZone.Location = new System.Drawing.Point(120, 132);
            this.cboToleranceZone.Size = new System.Drawing.Size(150, 21);
            this.cboToleranceZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblMaterialCondition.Text = "材料条件:";
            this.lblMaterialCondition.Location = new System.Drawing.Point(15, 170);
            this.lblMaterialCondition.AutoSize = true;
            this.cboMaterialCondition.Location = new System.Drawing.Point(120, 167);
            this.cboMaterialCondition.Size = new System.Drawing.Size(150, 21);
            this.cboMaterialCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnCalculateGdt.Text = "计算";
            this.btnCalculateGdt.Location = new System.Drawing.Point(280, 167);
            this.btnCalculateGdt.Size = new System.Drawing.Size(70, 25);
            this.btnCalculateGdt.Click += new System.EventHandler(this.btnCalculateGdt_Click);

            // grpGdtInfo
            this.grpGdtInfo.Text = "公差项目信息";
            this.grpGdtInfo.Location = new System.Drawing.Point(390, 10);
            this.grpGdtInfo.Size = new System.Drawing.Size(370, 220);
            this.grpGdtInfo.Controls.Add(this.lblGdtSymbol);
            this.grpGdtInfo.Controls.Add(this.lblGdtCategory);
            this.grpGdtInfo.Controls.Add(this.lblGdtZoneType);
            this.grpGdtInfo.Controls.Add(this.lblGdtDescription);

            this.lblGdtSymbol.Text = "符号: --";
            this.lblGdtSymbol.Location = new System.Drawing.Point(15, 30);
            this.lblGdtSymbol.AutoSize = true;
            this.lblGdtSymbol.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            this.lblGdtCategory.Text = "分类: --";
            this.lblGdtCategory.Location = new System.Drawing.Point(15, 65);
            this.lblGdtCategory.AutoSize = true;
            this.lblGdtZoneType.Text = "公差带: --";
            this.lblGdtZoneType.Location = new System.Drawing.Point(15, 95);
            this.lblGdtZoneType.AutoSize = true;
            this.lblGdtDescription.Text = "描述: --";
            this.lblGdtDescription.Location = new System.Drawing.Point(15, 125);
            this.lblGdtDescription.Size = new System.Drawing.Size(340, 80);
            this.lblGdtDescription.AutoEllipsis = true;

            // grpGdtResult
            this.grpGdtResult.Text = "计算结果";
            this.grpGdtResult.Location = new System.Drawing.Point(10, 240);
            this.grpGdtResult.Size = new System.Drawing.Size(750, 180);
            this.grpGdtResult.Controls.Add(this.lblGdtResultZone);
            this.grpGdtResult.Controls.Add(this.lblGdtResultSize);
            this.grpGdtResult.Controls.Add(this.lblGdtResultBonus);
            this.grpGdtResult.Controls.Add(this.lblGdtResultTotal);
            this.grpGdtResult.Controls.Add(this.lblGdtResultMMC);
            this.grpGdtResult.Controls.Add(this.lblGdtResultLMC);

            this.lblGdtResultZone.Text = "公差带形状: --";
            this.lblGdtResultZone.Location = new System.Drawing.Point(15, 30);
            this.lblGdtResultZone.AutoSize = true;
            this.lblGdtResultSize.Text = "公差带大小: --";
            this.lblGdtResultSize.Location = new System.Drawing.Point(15, 55);
            this.lblGdtResultSize.AutoSize = true;
            this.lblGdtResultBonus.Text = "附加公差 (Bonus): --";
            this.lblGdtResultBonus.Location = new System.Drawing.Point(15, 80);
            this.lblGdtResultBonus.AutoSize = true;
            this.lblGdtResultTotal.Text = "实际可用公差: --";
            this.lblGdtResultTotal.Location = new System.Drawing.Point(15, 105);
            this.lblGdtResultTotal.AutoSize = true;
            this.lblGdtResultTotal.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblGdtResultMMC.Text = "MMC 尺寸: --";
            this.lblGdtResultMMC.Location = new System.Drawing.Point(15, 130);
            this.lblGdtResultMMC.AutoSize = true;
            this.lblGdtResultLMC.Text = "LMC 尺寸: --";
            this.lblGdtResultLMC.Location = new System.Drawing.Point(15, 155);
            this.lblGdtResultLMC.AutoSize = true;

            // ===== tabSurface (表面粗糙度) =====
            this.tabSurface.Text = "表面粗糙度";
            this.tabSurface.Padding = new System.Windows.Forms.Padding(3);
            this.tabSurface.Controls.Add(this.grpRaConvert);
            this.tabSurface.Controls.Add(this.grpSurfaceResult);
            this.tabSurface.Controls.Add(this.grpSurfaceInput);

            // grpSurfaceInput
            this.grpSurfaceInput.Text = "输入参数";
            this.grpSurfaceInput.Location = new System.Drawing.Point(10, 10);
            this.grpSurfaceInput.Size = new System.Drawing.Size(370, 100);
            this.grpSurfaceInput.Controls.Add(this.lblRaValue);
            this.grpSurfaceInput.Controls.Add(this.cboRaValue);
            this.grpSurfaceInput.Controls.Add(this.lblRoughnessSymbol);
            this.grpSurfaceInput.Controls.Add(this.cboRoughnessSymbol);

            this.lblRaValue.Text = "Ra 值 (μm):";
            this.lblRaValue.Location = new System.Drawing.Point(15, 30);
            this.lblRaValue.AutoSize = true;
            this.cboRaValue.Location = new System.Drawing.Point(120, 27);
            this.cboRaValue.Size = new System.Drawing.Size(100, 21);
            this.cboRaValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRaValue.SelectedIndexChanged += new System.EventHandler(this.cboRaValue_SelectedIndexChanged);

            this.lblRoughnessSymbol.Text = "符号类型:";
            this.lblRoughnessSymbol.Location = new System.Drawing.Point(15, 65);
            this.lblRoughnessSymbol.AutoSize = true;
            this.cboRoughnessSymbol.Location = new System.Drawing.Point(120, 62);
            this.cboRoughnessSymbol.Size = new System.Drawing.Size(180, 21);
            this.cboRoughnessSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // grpSurfaceResult
            this.grpSurfaceResult.Text = "查询结果";
            this.grpSurfaceResult.Location = new System.Drawing.Point(10, 120);
            this.grpSurfaceResult.Size = new System.Drawing.Size(370, 250);
            this.grpSurfaceResult.Controls.Add(this.lblRzValue);
            this.grpSurfaceResult.Controls.Add(this.lblRqValue);
            this.grpSurfaceResult.Controls.Add(this.lblRoughnessGrade);
            this.grpSurfaceResult.Controls.Add(this.lblManufacturingMethod);
            this.grpSurfaceResult.Controls.Add(this.lblSurfaceApplication);
            this.grpSurfaceResult.Controls.Add(this.lblRoughnessDesc);

            this.lblRzValue.Text = "Rz (参考值): --";
            this.lblRzValue.Location = new System.Drawing.Point(15, 30);
            this.lblRzValue.AutoSize = true;
            this.lblRqValue.Text = "Rq (参考值): --";
            this.lblRqValue.Location = new System.Drawing.Point(15, 55);
            this.lblRqValue.AutoSize = true;
            this.lblRoughnessGrade.Text = "表面粗糙度等级: --";
            this.lblRoughnessGrade.Location = new System.Drawing.Point(15, 80);
            this.lblRoughnessGrade.AutoSize = true;
            this.lblRoughnessGrade.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblManufacturingMethod.Text = "推荐加工方法: --";
            this.lblManufacturingMethod.Location = new System.Drawing.Point(15, 110);
            this.lblManufacturingMethod.AutoSize = true;
            this.lblSurfaceApplication.Text = "典型应用: --";
            this.lblSurfaceApplication.Location = new System.Drawing.Point(15, 140);
            this.lblSurfaceApplication.AutoSize = true;
            this.lblSurfaceApplication.Size = new System.Drawing.Size(340, 40);
            this.lblSurfaceApplication.AutoEllipsis = true;
            this.lblRoughnessDesc.Text = "";
            this.lblRoughnessDesc.Location = new System.Drawing.Point(15, 185);
            this.lblRoughnessDesc.Size = new System.Drawing.Size(340, 50);
            this.lblRoughnessDesc.AutoEllipsis = true;

            // grpRaConvert
            this.grpRaConvert.Text = "Ra/Rz/Rq 转换";
            this.grpRaConvert.Location = new System.Drawing.Point(390, 10);
            this.grpRaConvert.Size = new System.Drawing.Size(370, 150);
            this.grpRaConvert.Controls.Add(this.lblRaInput);
            this.grpRaConvert.Controls.Add(this.txtRaInput);
            this.grpRaConvert.Controls.Add(this.btnConvertRaRz);
            this.grpRaConvert.Controls.Add(this.lblRzOutput);
            this.grpRaConvert.Controls.Add(this.txtRzOutput);
            this.grpRaConvert.Controls.Add(this.lblRqOutput);
            this.grpRaConvert.Controls.Add(this.txtRqOutput);

            this.lblRaInput.Text = "Ra (μm):";
            this.lblRaInput.Location = new System.Drawing.Point(15, 30);
            this.lblRaInput.AutoSize = true;
            this.txtRaInput.Location = new System.Drawing.Point(80, 27);
            this.txtRaInput.Size = new System.Drawing.Size(80, 21);
            this.btnConvertRaRz.Text = "转换";
            this.btnConvertRaRz.Location = new System.Drawing.Point(175, 25);
            this.btnConvertRaRz.Size = new System.Drawing.Size(60, 25);
            this.btnConvertRaRz.Click += new System.EventHandler(this.btnConvertRaRz_Click);
            this.lblRzOutput.Text = "Rz (μm):";
            this.lblRzOutput.Location = new System.Drawing.Point(15, 70);
            this.lblRzOutput.AutoSize = true;
            this.txtRzOutput.Location = new System.Drawing.Point(80, 67);
            this.txtRzOutput.Size = new System.Drawing.Size(80, 21);
            this.txtRzOutput.ReadOnly = true;
            this.lblRqOutput.Text = "Rq (μm):";
            this.lblRqOutput.Location = new System.Drawing.Point(15, 105);
            this.lblRqOutput.AutoSize = true;
            this.txtRqOutput.Location = new System.Drawing.Point(80, 102);
            this.txtRqOutput.Size = new System.Drawing.Size(80, 21);
            this.txtRqOutput.ReadOnly = true;

            // ===== tabThread (螺纹公差) =====
            this.tabThread.Text = "螺纹公差";
            this.tabThread.Padding = new System.Windows.Forms.Padding(3);
            this.tabThread.Controls.Add(this.grpThreadResult);
            this.tabThread.Controls.Add(this.grpThreadInfo);
            this.tabThread.Controls.Add(this.grpThreadInput);

            // grpThreadInput
            this.grpThreadInput.Text = "输入参数";
            this.grpThreadInput.Location = new System.Drawing.Point(10, 10);
            this.grpThreadInput.Size = new System.Drawing.Size(370, 180);
            this.grpThreadInput.Controls.Add(this.lblThreadSpec);
            this.grpThreadInput.Controls.Add(this.cboThreadSpec);
            this.grpThreadInput.Controls.Add(this.lblPitchType);
            this.grpThreadInput.Controls.Add(this.cboPitchType);
            this.grpThreadInput.Controls.Add(this.lblThreadTolerance);
            this.grpThreadInput.Controls.Add(this.cboThreadTolerance);
            this.grpThreadInput.Controls.Add(this.btnCalculateThreadTol);

            this.lblThreadSpec.Text = "螺纹规格:";
            this.lblThreadSpec.Location = new System.Drawing.Point(15, 30);
            this.lblThreadSpec.AutoSize = true;
            this.cboThreadSpec.Location = new System.Drawing.Point(100, 27);
            this.cboThreadSpec.Size = new System.Drawing.Size(100, 21);
            this.cboThreadSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThreadSpec.SelectedIndexChanged += new System.EventHandler(this.cboThreadSpec_SelectedIndexChanged);

            this.lblPitchType.Text = "螺距类型:";
            this.lblPitchType.Location = new System.Drawing.Point(15, 65);
            this.lblPitchType.AutoSize = true;
            this.cboPitchType.Location = new System.Drawing.Point(100, 62);
            this.cboPitchType.Size = new System.Drawing.Size(100, 21);
            this.cboPitchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPitchType.SelectedIndexChanged += new System.EventHandler(this.cboPitchType_SelectedIndexChanged);

            this.lblThreadTolerance.Text = "公差带:";
            this.lblThreadTolerance.Location = new System.Drawing.Point(15, 100);
            this.lblThreadTolerance.AutoSize = true;
            this.cboThreadTolerance.Location = new System.Drawing.Point(100, 97);
            this.cboThreadTolerance.Size = new System.Drawing.Size(100, 21);
            this.cboThreadTolerance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnCalculateThreadTol.Text = "计算";
            this.btnCalculateThreadTol.Location = new System.Drawing.Point(100, 135);
            this.btnCalculateThreadTol.Size = new System.Drawing.Size(100, 30);
            this.btnCalculateThreadTol.Click += new System.EventHandler(this.btnCalculateThreadTol_Click);

            // grpThreadInfo
            this.grpThreadInfo.Text = "螺纹基本尺寸";
            this.grpThreadInfo.Location = new System.Drawing.Point(390, 10);
            this.grpThreadInfo.Size = new System.Drawing.Size(370, 180);
            this.grpThreadInfo.Controls.Add(this.lblThreadMajorDia);
            this.grpThreadInfo.Controls.Add(this.lblThreadPitch);
            this.grpThreadInfo.Controls.Add(this.lblThreadPitchDia);
            this.grpThreadInfo.Controls.Add(this.lblThreadMinorDia);
            this.grpThreadInfo.Controls.Add(this.lblThreadHeight);

            this.lblThreadMajorDia.Text = "大径 (d/D): --";
            this.lblThreadMajorDia.Location = new System.Drawing.Point(15, 30);
            this.lblThreadMajorDia.AutoSize = true;
            this.lblThreadPitch.Text = "螺距 (P): --";
            this.lblThreadPitch.Location = new System.Drawing.Point(15, 55);
            this.lblThreadPitch.AutoSize = true;
            this.lblThreadPitchDia.Text = "中径 (d2/D2): --";
            this.lblThreadPitchDia.Location = new System.Drawing.Point(15, 80);
            this.lblThreadPitchDia.AutoSize = true;
            this.lblThreadMinorDia.Text = "小径 (d1/D1): --";
            this.lblThreadMinorDia.Location = new System.Drawing.Point(15, 105);
            this.lblThreadMinorDia.AutoSize = true;
            this.lblThreadHeight.Text = "牙高 (H): --";
            this.lblThreadHeight.Location = new System.Drawing.Point(15, 130);
            this.lblThreadHeight.AutoSize = true;

            // grpThreadResult
            this.grpThreadResult.Text = "公差计算结果";
            this.grpThreadResult.Location = new System.Drawing.Point(10, 200);
            this.grpThreadResult.Size = new System.Drawing.Size(750, 220);
            this.grpThreadResult.Controls.Add(this.lblThreadTolGrade);
            this.grpThreadResult.Controls.Add(this.lblThreadMajorTol);
            this.grpThreadResult.Controls.Add(this.lblThreadPitchTol);
            this.grpThreadResult.Controls.Add(this.lblThreadMinorTol);
            this.grpThreadResult.Controls.Add(this.lblThreadMajorRange);
            this.grpThreadResult.Controls.Add(this.lblThreadPitchRange);
            this.grpThreadResult.Controls.Add(this.lblThreadMinorRange);
            this.grpThreadResult.Controls.Add(this.lblThreadFitInfo);

            this.lblThreadTolGrade.Text = "公差等级: --";
            this.lblThreadTolGrade.Location = new System.Drawing.Point(15, 30);
            this.lblThreadTolGrade.AutoSize = true;
            this.lblThreadTolGrade.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblThreadMajorTol.Text = "大径公差: --";
            this.lblThreadMajorTol.Location = new System.Drawing.Point(15, 60);
            this.lblThreadMajorTol.AutoSize = true;
            this.lblThreadPitchTol.Text = "中径公差: --";
            this.lblThreadPitchTol.Location = new System.Drawing.Point(15, 85);
            this.lblThreadPitchTol.AutoSize = true;
            this.lblThreadMinorTol.Text = "小径公差: --";
            this.lblThreadMinorTol.Location = new System.Drawing.Point(15, 110);
            this.lblThreadMinorTol.AutoSize = true;
            this.lblThreadMajorRange.Text = "大径范围: --";
            this.lblThreadMajorRange.Location = new System.Drawing.Point(350, 60);
            this.lblThreadMajorRange.AutoSize = true;
            this.lblThreadPitchRange.Text = "中径范围: --";
            this.lblThreadPitchRange.Location = new System.Drawing.Point(350, 85);
            this.lblThreadPitchRange.AutoSize = true;
            this.lblThreadMinorRange.Text = "小径范围: --";
            this.lblThreadMinorRange.Location = new System.Drawing.Point(350, 110);
            this.lblThreadMinorRange.AutoSize = true;
            this.lblThreadFitInfo.Text = "";
            this.lblThreadFitInfo.Location = new System.Drawing.Point(15, 145);
            this.lblThreadFitInfo.Size = new System.Drawing.Size(720, 60);
            this.lblThreadFitInfo.AutoEllipsis = true;

            // ===== MainForm =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "迈迪综合公差查询系统";
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.tabControl.ResumeLayout(false);
            this.tabDimensional.ResumeLayout(false);
            this.tabGeometric.ResumeLayout(false);
            this.tabSurface.ResumeLayout(false);
            this.tabThread.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDimensional;
        private System.Windows.Forms.TabPage tabGeometric;
        private System.Windows.Forms.TabPage tabSurface;
        private System.Windows.Forms.TabPage tabThread;

        // 尺寸公差
        private System.Windows.Forms.GroupBox grpDimInput;
        private System.Windows.Forms.Label lblNominalSize;
        private System.Windows.Forms.ComboBox cboNominalSize;
        private System.Windows.Forms.Label lblToleranceGrade;
        private System.Windows.Forms.ComboBox cboToleranceGrade;
        private System.Windows.Forms.Label lblFitType;
        private System.Windows.Forms.ComboBox cboFitType;
        private System.Windows.Forms.Label lblBaseSystem;
        private System.Windows.Forms.ComboBox cboBaseSystem;
        private System.Windows.Forms.Button btnCalculateDimTol;
        private System.Windows.Forms.GroupBox grpDimResult;
        private System.Windows.Forms.Label lblToleranceValue;
        private System.Windows.Forms.Label lblUpperDeviation;
        private System.Windows.Forms.Label lblLowerDeviation;
        private System.Windows.Forms.Label lblMaxSize;
        private System.Windows.Forms.Label lblMinSize;

        // 形位公差
        private System.Windows.Forms.GroupBox grpGdtInput;
        private System.Windows.Forms.Label lblGdtFeature;
        private System.Windows.Forms.ComboBox cboGdtFeature;
        private System.Windows.Forms.Label lblGdtToleranceValue;
        private System.Windows.Forms.TextBox txtGdtToleranceValue;
        private System.Windows.Forms.Label lblGdtNominalSize;
        private System.Windows.Forms.TextBox txtGdtNominalSize;
        private System.Windows.Forms.Label lblToleranceZone;
        private System.Windows.Forms.ComboBox cboToleranceZone;
        private System.Windows.Forms.Label lblMaterialCondition;
        private System.Windows.Forms.ComboBox cboMaterialCondition;
        private System.Windows.Forms.Button btnCalculateGdt;
        private System.Windows.Forms.GroupBox grpGdtInfo;
        private System.Windows.Forms.Label lblGdtSymbol;
        private System.Windows.Forms.Label lblGdtCategory;
        private System.Windows.Forms.Label lblGdtZoneType;
        private System.Windows.Forms.Label lblGdtDescription;
        private System.Windows.Forms.GroupBox grpGdtResult;
        private System.Windows.Forms.Label lblGdtResultZone;
        private System.Windows.Forms.Label lblGdtResultSize;
        private System.Windows.Forms.Label lblGdtResultBonus;
        private System.Windows.Forms.Label lblGdtResultTotal;
        private System.Windows.Forms.Label lblGdtResultMMC;
        private System.Windows.Forms.Label lblGdtResultLMC;

        // 表面粗糙度
        private System.Windows.Forms.GroupBox grpSurfaceInput;
        private System.Windows.Forms.Label lblRaValue;
        private System.Windows.Forms.ComboBox cboRaValue;
        private System.Windows.Forms.Label lblRoughnessSymbol;
        private System.Windows.Forms.ComboBox cboRoughnessSymbol;
        private System.Windows.Forms.GroupBox grpSurfaceResult;
        private System.Windows.Forms.Label lblRzValue;
        private System.Windows.Forms.Label lblRqValue;
        private System.Windows.Forms.Label lblRoughnessGrade;
        private System.Windows.Forms.Label lblManufacturingMethod;
        private System.Windows.Forms.Label lblSurfaceApplication;
        private System.Windows.Forms.Label lblRoughnessDesc;
        private System.Windows.Forms.GroupBox grpRaConvert;
        private System.Windows.Forms.Label lblRaInput;
        private System.Windows.Forms.TextBox txtRaInput;
        private System.Windows.Forms.Button btnConvertRaRz;
        private System.Windows.Forms.Label lblRzOutput;
        private System.Windows.Forms.TextBox txtRzOutput;
        private System.Windows.Forms.Label lblRqOutput;
        private System.Windows.Forms.TextBox txtRqOutput;

        // 螺纹公差
        private System.Windows.Forms.GroupBox grpThreadInput;
        private System.Windows.Forms.Label lblThreadSpec;
        private System.Windows.Forms.ComboBox cboThreadSpec;
        private System.Windows.Forms.Label lblPitchType;
        private System.Windows.Forms.ComboBox cboPitchType;
        private System.Windows.Forms.Label lblThreadTolerance;
        private System.Windows.Forms.ComboBox cboThreadTolerance;
        private System.Windows.Forms.Button btnCalculateThreadTol;
        private System.Windows.Forms.GroupBox grpThreadInfo;
        private System.Windows.Forms.Label lblThreadMajorDia;
        private System.Windows.Forms.Label lblThreadPitch;
        private System.Windows.Forms.Label lblThreadPitchDia;
        private System.Windows.Forms.Label lblThreadMinorDia;
        private System.Windows.Forms.Label lblThreadHeight;
        private System.Windows.Forms.GroupBox grpThreadResult;
        private System.Windows.Forms.Label lblThreadTolGrade;
        private System.Windows.Forms.Label lblThreadMajorTol;
        private System.Windows.Forms.Label lblThreadPitchTol;
        private System.Windows.Forms.Label lblThreadMinorTol;
        private System.Windows.Forms.Label lblThreadMajorRange;
        private System.Windows.Forms.Label lblThreadPitchRange;
        private System.Windows.Forms.Label lblThreadMinorRange;
        private System.Windows.Forms.Label lblThreadFitInfo;
    }
}
