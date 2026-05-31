namespace ShaftDesign
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);

            // === 主布局 ===
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();

            // === 输入参数组 ===
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.labelPower = new System.Windows.Forms.Label();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.labelPowerUnit = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.labelSpeedUnit = new System.Windows.Forms.Label();

            // === 材料选择 ===
            this.groupBoxMaterial = new System.Windows.Forms.GroupBox();
            this.comboBoxMaterial = new System.Windows.Forms.ComboBox();
            this.labelMaterialInfo = new System.Windows.Forms.Label();
            this.txtSigmaS = new System.Windows.Forms.TextBox();
            this.txtSigmaMinus1 = new System.Windows.Forms.TextBox();
            this.txtTauMinus1 = new System.Windows.Forms.TextBox();
            this.labelSigmaS = new System.Windows.Forms.Label();
            this.labelSigmaMinus1 = new System.Windows.Forms.Label();
            this.labelTauMinus1 = new System.Windows.Forms.Label();

            // === 零件布局 ===
            this.groupBoxLayout = new System.Windows.Forms.GroupBox();
            this.dgvComponents = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelComponentButtons = new System.Windows.Forms.Panel();
            this.btnAddComponent = new System.Windows.Forms.Button();
            this.btnRemoveComponent = new System.Windows.Forms.Button();

            // === 载荷输入 ===
            this.groupBoxLoads = new System.Windows.Forms.GroupBox();
            this.dgvLoads = new System.Windows.Forms.DataGridView();
            this.colLoadName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoadPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelLoadButtons = new System.Windows.Forms.Panel();
            this.btnAddLoad = new System.Windows.Forms.Button();
            this.btnRemoveLoad = new System.Windows.Forms.Button();

            // === 载荷循环类型 ===
            this.groupBoxCycle = new System.Windows.Forms.GroupBox();
            this.radioPulsating = new System.Windows.Forms.RadioButton();
            this.radioSymmetric = new System.Windows.Forms.RadioButton();
            this.radioStatic = new System.Windows.Forms.RadioButton();

            // === 其他参数 ===
            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this.labelKeyway = new System.Windows.Forms.Label();
            this.comboBoxKeyway = new System.Windows.Forms.ComboBox();
            this.labelRoughness = new System.Windows.Forms.Label();
            this.txtRoughness = new System.Windows.Forms.TextBox();
            this.labelAllowableN = new System.Windows.Forms.Label();
            this.txtAllowableN = new System.Windows.Forms.TextBox();
            this.labelAllowableS = new System.Windows.Forms.Label();
            this.txtAllowableS = new System.Windows.Forms.TextBox();

            // === 按钮 ===
            this.panelActionButtons = new System.Windows.Forms.Panel();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();

            // === 结果输出 ===
            this.tabControlResults = new System.Windows.Forms.TabControl();
            this.tabEstimate = new System.Windows.Forms.TabPage();
            this.tabStrength = new System.Windows.Forms.TabPage();
            this.tabFatigue = new System.Windows.Forms.TabPage();

            // --- 初估轴径 ---
            this.txtEstimateResult = new System.Windows.Forms.TextBox();

            // --- 强度校核 ---
            this.dgvStrength = new System.Windows.Forms.DataGridView();
            this.colSecPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecMe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecSigma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecTau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecSigmaE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecPass = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // --- 疲劳校核 ---
            this.dgvFatigue = new System.Windows.Forms.DataGridView();
            this.colFatPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFatD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFatSSigma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFatSTau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFatS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFatPass = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // === 状态栏 ===
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            // === 初始化 ===
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxMaterial.SuspendLayout();
            this.groupBoxLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponents)).BeginInit();
            this.groupBoxLoads.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).BeginInit();
            this.panelComponentButtons.SuspendLayout();
            this.panelLoadButtons.SuspendLayout();
            this.panelActionButtons.SuspendLayout();
            this.groupBoxCycle.SuspendLayout();
            this.groupBoxOther.SuspendLayout();
            this.tabControlResults.SuspendLayout();
            this.tabEstimate.SuspendLayout();
            this.tabStrength.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStrength)).BeginInit();
            this.tabFatigue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFatigue)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // ========================================
            // ========================================

            // ========================================
            // panelLeft (输入面板)
            // ========================================
            this.panelLeft.AutoScroll = true;
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Width = 420;
            this.panelLeft.MinimumSize = new System.Drawing.Size(400, 0);
            this.panelLeft.Padding = new System.Windows.Forms.Padding(4);
            this.panelLeft.Padding = new System.Windows.Forms.Padding(8);

            // --- groupBoxInput ---
            this.groupBoxInput.Text = "基本参数";
            this.groupBoxInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInput.Height = 80;
            this.groupBoxInput.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.labelPower.Text = "传递功率 P:";
            this.labelPower.Location = new System.Drawing.Point(15, 25);
            this.labelPower.AutoSize = true;
            this.txtPower.Location = new System.Drawing.Point(100, 22);
            this.txtPower.Size = new System.Drawing.Size(80, 23);
            this.txtPower.Text = "5.5";
            this.labelPowerUnit.Text = "kW";
            this.labelPowerUnit.Location = new System.Drawing.Point(185, 25);
            this.labelPowerUnit.AutoSize = true;

            this.labelSpeed.Text = "转速 n:";
            this.labelSpeed.Location = new System.Drawing.Point(220, 25);
            this.labelSpeed.AutoSize = true;
            this.txtSpeed.Location = new System.Drawing.Point(280, 22);
            this.txtSpeed.Size = new System.Drawing.Size(80, 23);
            this.txtSpeed.Text = "1450";
            this.labelSpeedUnit.Text = "rpm";
            this.labelSpeedUnit.Location = new System.Drawing.Point(365, 25);
            this.labelSpeedUnit.AutoSize = true;

            this.groupBoxInput.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.labelPower, this.txtPower, this.labelPowerUnit,
                this.labelSpeed, this.txtSpeed, this.labelSpeedUnit
            });

            // --- groupBoxMaterial ---
            this.groupBoxMaterial.Text = "轴的材料";
            this.groupBoxMaterial.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxMaterial.Top = 80;
            this.groupBoxMaterial.Height = 140;
            this.groupBoxMaterial.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.comboBoxMaterial.Location = new System.Drawing.Point(15, 25);
            this.comboBoxMaterial.Size = new System.Drawing.Size(200, 23);
            this.comboBoxMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaterial.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMaterial_SelectedIndexChanged);

            this.labelMaterialInfo.Text = "材料力学性能:";
            this.labelMaterialInfo.Location = new System.Drawing.Point(15, 55);
            this.labelMaterialInfo.AutoSize = true;
            this.labelMaterialInfo.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold);

            this.labelSigmaS.Text = "sigma_s:";
            this.labelSigmaS.Location = new System.Drawing.Point(15, 78);
            this.labelSigmaS.AutoSize = true;
            this.txtSigmaS.Location = new System.Drawing.Point(80, 75);
            this.txtSigmaS.Size = new System.Drawing.Size(70, 23);
            this.txtSigmaS.ReadOnly = true;
            this.txtSigmaS.BackColor = System.Drawing.SystemColors.Control;

            this.labelSigmaMinus1.Text = "sigma_-1:";
            this.labelSigmaMinus1.Location = new System.Drawing.Point(165, 78);
            this.labelSigmaMinus1.AutoSize = true;
            this.txtSigmaMinus1.Location = new System.Drawing.Point(240, 75);
            this.txtSigmaMinus1.Size = new System.Drawing.Size(70, 23);
            this.txtSigmaMinus1.ReadOnly = true;
            this.txtSigmaMinus1.BackColor = System.Drawing.SystemColors.Control;

            this.labelTauMinus1.Text = "tau_-1:";
            this.labelTauMinus1.Location = new System.Drawing.Point(325, 78);
            this.labelTauMinus1.AutoSize = true;
            this.txtTauMinus1.Location = new System.Drawing.Point(380, 75);
            this.txtTauMinus1.Size = new System.Drawing.Size(70, 23);
            this.txtTauMinus1.ReadOnly = true;
            this.txtTauMinus1.BackColor = System.Drawing.SystemColors.Control;

            this.groupBoxMaterial.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.comboBoxMaterial, this.labelMaterialInfo,
                this.labelSigmaS, this.txtSigmaS,
                this.labelSigmaMinus1, this.txtSigmaMinus1,
                this.labelTauMinus1, this.txtTauMinus1
            });

            // --- groupBoxLayout ---
            this.groupBoxLayout.Text = "轴上零件布局";
            this.groupBoxLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxLayout.Top = 220;
            this.groupBoxLayout.Height = 220;
            this.groupBoxLayout.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.dgvComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComponents.AllowUserToAddRows = false;
            this.dgvComponents.AllowUserToDeleteRows = false;
            this.dgvComponents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvComponents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComponents.RowHeadersVisible = false;

            this.colName.HeaderText = "名称";
            this.colName.Name = "colName";
            this.colName.FillWeight = 60;

            this.colType.HeaderText = "类型";
            this.colType.Name = "colType";
            this.colType.Items.AddRange(new object[] { "齿轮", "轴承", "联轴器", "带轮", "链轮", "其他" });
            this.colType.FillWeight = 50;

            this.colPosition.HeaderText = "位置(mm)";
            this.colPosition.Name = "colPosition";
            this.colPosition.FillWeight = 50;

            this.colDiameter.HeaderText = "轴径(mm)";
            this.colDiameter.Name = "colDiameter";
            this.colDiameter.FillWeight = 50;

            this.dgvComponents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colName, this.colType, this.colPosition, this.colDiameter
            });

            this.panelComponentButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelComponentButtons.Height = 36;

            this.btnAddComponent.Text = "添加";
            this.btnAddComponent.Location = new System.Drawing.Point(4, 4);
            this.btnAddComponent.Size = new System.Drawing.Size(60, 28);
            this.btnAddComponent.Click += new System.EventHandler(this.BtnAddComponent_Click);

            this.btnRemoveComponent.Text = "删除";
            this.btnRemoveComponent.Location = new System.Drawing.Point(70, 4);
            this.btnRemoveComponent.Size = new System.Drawing.Size(60, 28);
            this.btnRemoveComponent.Click += new System.EventHandler(this.BtnRemoveComponent_Click);

            this.panelComponentButtons.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnAddComponent, this.btnRemoveComponent
            });

            this.groupBoxLayout.Controls.Add(this.dgvComponents);
            this.groupBoxLayout.Controls.Add(this.panelComponentButtons);

            // --- groupBoxLoads ---
            this.groupBoxLoads.Text = "载荷输入";
            this.groupBoxLoads.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxLoads.Top = 420;
            this.groupBoxLoads.Height = 220;
            this.groupBoxLoads.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.dgvLoads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoads.AllowUserToAddRows = false;
            this.dgvLoads.AllowUserToDeleteRows = false;
            this.dgvLoads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoads.RowHeadersVisible = false;

            this.colLoadName.HeaderText = "名称";
            this.colLoadName.Name = "colLoadName";
            this.colLoadName.FillWeight = 50;

            this.colLoadPos.HeaderText = "位置(mm)";
            this.colLoadPos.Name = "colLoadPos";
            this.colLoadPos.FillWeight = 40;

            this.colFr.HeaderText = "Fr(N)";
            this.colFr.Name = "colFr";
            this.colFr.FillWeight = 40;

            this.colFt.HeaderText = "Ft(N)";
            this.colFt.Name = "colFt";
            this.colFt.FillWeight = 40;

            this.colFa.HeaderText = "Fa(N)";
            this.colFa.Name = "colFa";
            this.colFa.FillWeight = 40;

            this.dgvLoads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colLoadName, this.colLoadPos, this.colFr, this.colFt, this.colFa
            });

            this.panelLoadButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLoadButtons.Height = 36;

            this.btnAddLoad.Text = "添加";
            this.btnAddLoad.Location = new System.Drawing.Point(4, 4);
            this.btnAddLoad.Size = new System.Drawing.Size(60, 28);
            this.btnAddLoad.Click += new System.EventHandler(this.BtnAddLoad_Click);

            this.btnRemoveLoad.Text = "删除";
            this.btnRemoveLoad.Location = new System.Drawing.Point(70, 4);
            this.btnRemoveLoad.Size = new System.Drawing.Size(60, 28);
            this.btnRemoveLoad.Click += new System.EventHandler(this.BtnRemoveLoad_Click);

            this.panelLoadButtons.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnAddLoad, this.btnRemoveLoad
            });

            this.groupBoxLoads.Controls.Add(this.dgvLoads);
            this.groupBoxLoads.Controls.Add(this.panelLoadButtons);

            // --- groupBoxCycle ---
            this.groupBoxCycle.Text = "载荷循环类型";
            this.groupBoxCycle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCycle.Top = 660;
            this.groupBoxCycle.Height = 55;
            this.groupBoxCycle.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);

            this.radioPulsating.Text = "脉动循环 (alpha=0.6)";
            this.radioPulsating.Location = new System.Drawing.Point(15, 22);
            this.radioPulsating.AutoSize = true;
            this.radioPulsating.Checked = true;

            this.radioSymmetric.Text = "对称循环 (alpha=1.0)";
            this.radioSymmetric.Location = new System.Drawing.Point(170, 22);
            this.radioSymmetric.AutoSize = true;

            this.radioStatic.Text = "静载荷 (alpha=0.3)";
            this.radioStatic.Location = new System.Drawing.Point(325, 22);
            this.radioStatic.AutoSize = true;

            this.groupBoxCycle.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.radioPulsating, this.radioSymmetric, this.radioStatic
            });

            // --- groupBoxOther ---
            this.groupBoxOther.Text = "其他参数";
            this.groupBoxOther.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOther.Top = 715;
            this.groupBoxOther.Height = 120;
            this.groupBoxOther.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.labelKeyway.Text = "键槽数:";
            this.labelKeyway.Location = new System.Drawing.Point(15, 25);
            this.labelKeyway.AutoSize = true;
            this.comboBoxKeyway.Location = new System.Drawing.Point(80, 22);
            this.comboBoxKeyway.Size = new System.Drawing.Size(60, 23);
            this.comboBoxKeyway.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyway.Items.AddRange(new object[] { "0", "1", "2" });
            this.comboBoxKeyway.SelectedIndex = 0;

            this.labelRoughness.Text = "表面粗糙度 Ra:";
            this.labelRoughness.Location = new System.Drawing.Point(160, 25);
            this.labelRoughness.AutoSize = true;
            this.txtRoughness.Location = new System.Drawing.Point(260, 22);
            this.txtRoughness.Size = new System.Drawing.Size(50, 23);
            this.txtRoughness.Text = "1.6";
            this.labelRoughness2 = new System.Windows.Forms.Label();
            this.labelRoughness2.Text = "um";
            this.labelRoughness2.Location = new System.Drawing.Point(315, 25);
            this.labelRoughness2.AutoSize = true;

            this.labelAllowableN.Text = "[n]:";
            this.labelAllowableN.Location = new System.Drawing.Point(15, 55);
            this.labelAllowableN.AutoSize = true;
            this.txtAllowableN.Location = new System.Drawing.Point(80, 52);
            this.txtAllowableN.Size = new System.Drawing.Size(50, 23);
            this.txtAllowableN.Text = "1.5";

            this.labelAllowableS.Text = "[S]:";
            this.labelAllowableS.Location = new System.Drawing.Point(160, 55);
            this.labelAllowableS.AutoSize = true;
            this.txtAllowableS.Location = new System.Drawing.Point(200, 52);
            this.txtAllowableS.Size = new System.Drawing.Size(50, 23);
            this.txtAllowableS.Text = "1.5";

            this.groupBoxOther.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.labelKeyway, this.comboBoxKeyway,
                this.labelRoughness, this.txtRoughness, this.labelRoughness2,
                this.labelAllowableN, this.txtAllowableN,
                this.labelAllowableS, this.txtAllowableS
            });

            // --- panelActionButtons ---
            this.panelActionButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActionButtons.Height = 50;
            this.panelActionButtons.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);

            this.btnCalculate.Text = "计算";
            this.btnCalculate.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCalculate.Width = 180;
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);

            this.btnReset.Text = "重置";
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReset.Width = 80;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);

            this.panelActionButtons.Controls.Add(this.btnReset);
            this.panelActionButtons.Controls.Add(this.btnCalculate);

            // 组装左侧
            this.panelLeft.Controls.Add(this.panelActionButtons);
            this.panelLeft.Controls.Add(this.groupBoxOther);
            this.panelLeft.Controls.Add(this.groupBoxCycle);
            this.panelLeft.Controls.Add(this.groupBoxLoads);
            this.panelLeft.Controls.Add(this.groupBoxLayout);
            this.panelLeft.Controls.Add(this.groupBoxMaterial);
            this.panelLeft.Controls.Add(this.groupBoxInput);

            // ========================================
            // panelRight (结果面板)
            // ========================================
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Padding = new System.Windows.Forms.Padding(4);

            // --- tabControlResults ---
            this.tabControlResults.Dock = System.Windows.Forms.DockStyle.Fill;

            // --- tabEstimate ---
            this.tabEstimate.Text = "初估轴径";
            this.tabEstimate.Padding = new System.Windows.Forms.Padding(8);
            this.txtEstimateResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEstimateResult.Multiline = true;
            this.txtEstimateResult.ReadOnly = true;
            this.txtEstimateResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEstimateResult.Font = new System.Drawing.Font("Consolas", 10F);
            this.tabEstimate.Controls.Add(this.txtEstimateResult);

            // --- tabStrength ---
            this.tabStrength.Text = "强度校核";
            this.tabStrength.Padding = new System.Windows.Forms.Padding(4);
            this.dgvStrength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStrength.ReadOnly = true;
            this.dgvStrength.AllowUserToAddRows = false;
            this.dgvStrength.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStrength.RowHeadersVisible = false;
            this.dgvStrength.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);

            this.colSecPos.Name = "colSecPos";
            colSecPos.HeaderText = "位置(mm)";
            this.colSecPos.FillWeight = 40;
            this.colSecD.Name = "colSecD";
            colSecD.HeaderText = "直径(mm)";
            this.colSecD.FillWeight = 35;
            this.colSecM.Name = "colSecM";
            colSecM.HeaderText = "M(N.mm)";
            this.colSecM.FillWeight = 40;
            this.colSecT.Name = "colSecT";
            colSecT.HeaderText = "T(N.mm)";
            this.colSecT.FillWeight = 40;
            this.colSecMe.Name = "colSecMe";
            colSecMe.HeaderText = "Me(N.mm)";
            this.colSecMe.FillWeight = 40;
            this.colSecSigma.Name = "colSecSigma";
            colSecSigma.HeaderText = "sigma(MPa)";
            this.colSecSigma.FillWeight = 35;
            this.colSecTau.Name = "colSecTau";
            colSecTau.HeaderText = "tau(MPa)";
            this.colSecTau.FillWeight = 35;
            this.colSecSigmaE.Name = "colSecSigmaE";
            colSecSigmaE.HeaderText = "sigma_e(MPa)";
            this.colSecSigmaE.FillWeight = 35;
            this.colSecN.Name = "colSecN";
            colSecN.HeaderText = "n";
            this.colSecN.FillWeight = 20;
            this.colSecPass.Name = "colSecPass";
            colSecPass.HeaderText = "结论";
            this.colSecPass.FillWeight = 30;

            this.dgvStrength.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSecPos, this.colSecD, this.colSecM, this.colSecT, this.colSecMe,
                this.colSecSigma, this.colSecTau, this.colSecSigmaE, this.colSecN, this.colSecPass
            });
            this.tabStrength.Controls.Add(this.dgvStrength);

            // --- tabFatigue ---
            this.tabFatigue.Text = "疲劳强度校核";
            this.tabFatigue.Padding = new System.Windows.Forms.Padding(4);
            this.dgvFatigue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFatigue.ReadOnly = true;
            this.dgvFatigue.AllowUserToAddRows = false;
            this.dgvFatigue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFatigue.RowHeadersVisible = false;
            this.dgvFatigue.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 248, 240);

            this.colFatPos.Name = "colFatPos";
            colFatPos.HeaderText = "位置(mm)";
            this.colFatPos.FillWeight = 40;
            this.colFatD.Name = "colFatD";
            colFatD.HeaderText = "直径(mm)";
            this.colFatD.FillWeight = 35;
            this.colFatSSigma.Name = "colFatSSigma";
            colFatSSigma.HeaderText = "S_sigma";
            this.colFatSSigma.FillWeight = 35;
            this.colFatSTau.Name = "colFatSTau";
            colFatSTau.HeaderText = "S_tau";
            this.colFatSTau.FillWeight = 35;
            this.colFatS.Name = "colFatS";
            colFatS.HeaderText = "S(综合)";
            this.colFatS.FillWeight = 35;
            this.colFatPass.Name = "colFatPass";
            colFatPass.HeaderText = "结论";
            this.colFatPass.FillWeight = 30;

            this.dgvFatigue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colFatPos, this.colFatD, this.colFatSSigma, this.colFatSTau, this.colFatS, this.colFatPass
            });
            this.tabFatigue.Controls.Add(this.dgvFatigue);

            this.tabControlResults.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.tabEstimate, this.tabStrength, this.tabFatigue
            });

            this.panelRight.Controls.Add(this.tabControlResults);

            // ========================================
            // ========================================

            // ========================================
            // statusStrip
            // ========================================
            this.toolStripStatusLabel.Text = "就绪";
            this.statusStrip.Items.Add(this.toolStripStatusLabel);

            // ========================================
            // MainForm
            // ========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.MinimumSize = new System.Drawing.Size(1080, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "轴设计 - 麦豆宝";

            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxMaterial.ResumeLayout(false);
            this.groupBoxMaterial.PerformLayout();
            this.groupBoxLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponents)).EndInit();
            this.groupBoxLoads.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).EndInit();
            this.panelComponentButtons.ResumeLayout(false);
            this.panelLoadButtons.ResumeLayout(false);
            this.panelActionButtons.ResumeLayout(false);
            this.groupBoxCycle.ResumeLayout(false);
            this.groupBoxCycle.PerformLayout();
            this.groupBoxOther.ResumeLayout(false);
            this.groupBoxOther.PerformLayout();
            this.tabControlResults.ResumeLayout(false);
            this.tabEstimate.ResumeLayout(false);
            this.tabEstimate.PerformLayout();
            this.tabStrength.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStrength)).EndInit();
            this.tabFatigue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFatigue)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;

        // 输入
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.Label labelPower;
        private System.Windows.Forms.TextBox txtPower;
        private System.Windows.Forms.Label labelPowerUnit;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Label labelSpeedUnit;

        // 材料
        private System.Windows.Forms.GroupBox groupBoxMaterial;
        private System.Windows.Forms.ComboBox comboBoxMaterial;
        private System.Windows.Forms.Label labelMaterialInfo;
        private System.Windows.Forms.TextBox txtSigmaS;
        private System.Windows.Forms.TextBox txtSigmaMinus1;
        private System.Windows.Forms.TextBox txtTauMinus1;
        private System.Windows.Forms.Label labelSigmaS;
        private System.Windows.Forms.Label labelSigmaMinus1;
        private System.Windows.Forms.Label labelTauMinus1;

        // 零件布局
        private System.Windows.Forms.GroupBox groupBoxLayout;
        private System.Windows.Forms.DataGridView dgvComponents;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiameter;
        private System.Windows.Forms.Panel panelComponentButtons;
        private System.Windows.Forms.Button btnAddComponent;
        private System.Windows.Forms.Button btnRemoveComponent;

        // 载荷
        private System.Windows.Forms.GroupBox groupBoxLoads;
        private System.Windows.Forms.DataGridView dgvLoads;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoadName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoadPos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFa;
        private System.Windows.Forms.Panel panelLoadButtons;
        private System.Windows.Forms.Button btnAddLoad;
        private System.Windows.Forms.Button btnRemoveLoad;

        // 循环类型
        private System.Windows.Forms.GroupBox groupBoxCycle;
        private System.Windows.Forms.RadioButton radioPulsating;
        private System.Windows.Forms.RadioButton radioSymmetric;
        private System.Windows.Forms.RadioButton radioStatic;

        // 其他参数
        private System.Windows.Forms.GroupBox groupBoxOther;
        private System.Windows.Forms.Label labelKeyway;
        private System.Windows.Forms.ComboBox comboBoxKeyway;
        private System.Windows.Forms.Label labelRoughness;
        private System.Windows.Forms.TextBox txtRoughness;
        private System.Windows.Forms.Label labelRoughness2;
        private System.Windows.Forms.Label labelAllowableN;
        private System.Windows.Forms.TextBox txtAllowableN;
        private System.Windows.Forms.Label labelAllowableS;
        private System.Windows.Forms.TextBox txtAllowableS;

        // 按钮
        private System.Windows.Forms.Panel panelActionButtons;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnReset;

        // 结果
        private System.Windows.Forms.TabControl tabControlResults;
        private System.Windows.Forms.TabPage tabEstimate;
        private System.Windows.Forms.TabPage tabStrength;
        private System.Windows.Forms.TabPage tabFatigue;
        private System.Windows.Forms.TextBox txtEstimateResult;
        private System.Windows.Forms.DataGridView dgvStrength;
        private System.Windows.Forms.DataGridView dgvFatigue;

        private System.Windows.Forms.DataGridViewTextBoxColumn colSecPos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecMe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecSigma;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecTau;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecSigmaE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFatPos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFatD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFatSSigma;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFatSTau;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFatS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFatPass;

        // 状态栏
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}
