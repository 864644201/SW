namespace BoltCheck
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
            // === Main layout ===
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabTension = new System.Windows.Forms.TabPage();
            this.tabShear = new System.Windows.Forms.TabPage();
            this.tabCombined = new System.Windows.Forms.TabPage();
            this.tabEccentric = new System.Windows.Forms.TabPage();
            this.tabFlange = new System.Windows.Forms.TabPage();
            this.tabRecommend = new System.Windows.Forms.TabPage();

            // === Shared result controls ===
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFatigueStatus = new System.Windows.Forms.Label();
            this.lblStandard = new System.Windows.Forms.Label();

            // === Tab1: 轴向拉伸 input controls ===
            this.grpTension = new System.Windows.Forms.GroupBox();
            this.lblTensionConn = new System.Windows.Forms.Label();
            this.cboTensionConn = new System.Windows.Forms.ComboBox();
            this.lblTensionSpec = new System.Windows.Forms.Label();
            this.cboTensionSpec = new System.Windows.Forms.ComboBox();
            this.lblTensionGrade = new System.Windows.Forms.Label();
            this.cboTensionGrade = new System.Windows.Forms.ComboBox();
            this.lblTensionF = new System.Windows.Forms.Label();
            this.txtTensionF = new System.Windows.Forms.TextBox();
            this.lblTensionFUnit = new System.Windows.Forms.Label();
            this.lblTensionN = new System.Windows.Forms.Label();
            this.numTensionN = new System.Windows.Forms.NumericUpDown();
            this.lblTensionK = new System.Windows.Forms.Label();
            this.txtTensionK = new System.Windows.Forms.TextBox();
            this.lblTensionKNote = new System.Windows.Forms.Label();
            this.lblTensionK2 = new System.Windows.Forms.Label();
            this.txtTensionK2 = new System.Windows.Forms.TextBox();
            this.lblTensionK2Note = new System.Windows.Forms.Label();
            this.lblTensionK1 = new System.Windows.Forms.Label();
            this.cboTensionK1 = new System.Windows.Forms.ComboBox();
            this.lblTensionCb = new System.Windows.Forms.Label();
            this.txtTensionCb = new System.Windows.Forms.TextBox();
            this.lblTensionCond = new System.Windows.Forms.Label();
            this.cboTensionCond = new System.Windows.Forms.ComboBox();
            this.lblTensionSF = new System.Windows.Forms.Label();
            this.txtTensionSF = new System.Windows.Forms.TextBox();
            this.btnTensionCalc = new System.Windows.Forms.Button();
            this.btnTensionExport = new System.Windows.Forms.Button();

            // === Tab2: 横向剪切 input controls ===
            this.grpShear = new System.Windows.Forms.GroupBox();
            this.lblShearConn = new System.Windows.Forms.Label();
            this.cboShearConn = new System.Windows.Forms.ComboBox();
            this.lblShearSpec = new System.Windows.Forms.Label();
            this.cboShearSpec = new System.Windows.Forms.ComboBox();
            this.lblShearGrade = new System.Windows.Forms.Label();
            this.cboShearGrade = new System.Windows.Forms.ComboBox();
            this.lblShearFs = new System.Windows.Forms.Label();
            this.txtShearFs = new System.Windows.Forms.TextBox();
            this.lblShearFsUnit = new System.Windows.Forms.Label();
            this.lblShearN = new System.Windows.Forms.Label();
            this.numShearN = new System.Windows.Forms.NumericUpDown();
            this.lblShearMu = new System.Windows.Forms.Label();
            this.txtShearMu = new System.Windows.Forms.TextBox();
            this.lblShearMuNote = new System.Windows.Forms.Label();
            this.lblShearM = new System.Windows.Forms.Label();
            this.numShearM = new System.Windows.Forms.NumericUpDown();
            this.lblShearSF = new System.Windows.Forms.Label();
            this.txtShearSF = new System.Windows.Forms.TextBox();
            this.btnShearCalc = new System.Windows.Forms.Button();

            // === Tab3: 拉剪组合 input controls ===
            this.grpCombined = new System.Windows.Forms.GroupBox();
            this.lblCombConn = new System.Windows.Forms.Label();
            this.cboCombConn = new System.Windows.Forms.ComboBox();
            this.lblCombSpec = new System.Windows.Forms.Label();
            this.cboCombSpec = new System.Windows.Forms.ComboBox();
            this.lblCombGrade = new System.Windows.Forms.Label();
            this.cboCombGrade = new System.Windows.Forms.ComboBox();
            this.lblCombF = new System.Windows.Forms.Label();
            this.txtCombF = new System.Windows.Forms.TextBox();
            this.lblCombFUnit = new System.Windows.Forms.Label();
            this.lblCombFs = new System.Windows.Forms.Label();
            this.txtCombFs = new System.Windows.Forms.TextBox();
            this.lblCombFsUnit = new System.Windows.Forms.Label();
            this.lblCombN = new System.Windows.Forms.Label();
            this.numCombN = new System.Windows.Forms.NumericUpDown();
            this.lblCombK = new System.Windows.Forms.Label();
            this.txtCombK = new System.Windows.Forms.TextBox();
            this.lblCombCb = new System.Windows.Forms.Label();
            this.txtCombCb = new System.Windows.Forms.TextBox();
            this.lblCombMu = new System.Windows.Forms.Label();
            this.txtCombMu = new System.Windows.Forms.TextBox();
            this.lblCombM = new System.Windows.Forms.Label();
            this.numCombM = new System.Windows.Forms.NumericUpDown();
            this.lblCombSF = new System.Windows.Forms.Label();
            this.txtCombSF = new System.Windows.Forms.TextBox();
            this.btnCombCalc = new System.Windows.Forms.Button();

            // === Tab4: 偏心载荷 input controls ===
            this.grpEccentric = new System.Windows.Forms.GroupBox();
            this.lblEccSpec = new System.Windows.Forms.Label();
            this.cboEccSpec = new System.Windows.Forms.ComboBox();
            this.lblEccGrade = new System.Windows.Forms.Label();
            this.cboEccGrade = new System.Windows.Forms.ComboBox();
            this.lblEccF = new System.Windows.Forms.Label();
            this.txtEccF = new System.Windows.Forms.TextBox();
            this.lblEccFUnit = new System.Windows.Forms.Label();
            this.lblEccE = new System.Windows.Forms.Label();
            this.txtEccE = new System.Windows.Forms.TextBox();
            this.lblEccEUnit = new System.Windows.Forms.Label();
            this.lblEccN = new System.Windows.Forms.Label();
            this.numEccN = new System.Windows.Forms.NumericUpDown();
            this.lblEccSpacing = new System.Windows.Forms.Label();
            this.txtEccSpacing = new System.Windows.Forms.TextBox();
            this.lblEccSpacingUnit = new System.Windows.Forms.Label();
            this.lblEccK = new System.Windows.Forms.Label();
            this.txtEccK = new System.Windows.Forms.TextBox();
            this.lblEccCb = new System.Windows.Forms.Label();
            this.txtEccCb = new System.Windows.Forms.TextBox();
            this.lblEccSF = new System.Windows.Forms.Label();
            this.txtEccSF = new System.Windows.Forms.TextBox();
            this.btnEccCalc = new System.Windows.Forms.Button();

            // === Tab5: 法兰连接 input controls ===
            this.grpFlange = new System.Windows.Forms.GroupBox();
            this.lblFlanSpec = new System.Windows.Forms.Label();
            this.cboFlanSpec = new System.Windows.Forms.ComboBox();
            this.lblFlanGrade = new System.Windows.Forms.Label();
            this.cboFlanGrade = new System.Windows.Forms.ComboBox();
            this.lblFlanP = new System.Windows.Forms.Label();
            this.txtFlanP = new System.Windows.Forms.TextBox();
            this.lblFlanPUnit = new System.Windows.Forms.Label();
            this.lblFlanDf = new System.Windows.Forms.Label();
            this.txtFlanDf = new System.Windows.Forms.TextBox();
            this.lblFlanDfUnit = new System.Windows.Forms.Label();
            this.lblFlanDb = new System.Windows.Forms.Label();
            this.txtFlanDb = new System.Windows.Forms.TextBox();
            this.lblFlanDbUnit = new System.Windows.Forms.Label();
            this.lblFlanN = new System.Windows.Forms.Label();
            this.numFlanN = new System.Windows.Forms.NumericUpDown();
            this.lblFlanK = new System.Windows.Forms.Label();
            this.txtFlanK = new System.Windows.Forms.TextBox();
            this.lblFlanCb = new System.Windows.Forms.Label();
            this.txtFlanCb = new System.Windows.Forms.TextBox();
            this.lblFlanGasket = new System.Windows.Forms.Label();
            this.txtFlanGasket = new System.Windows.Forms.TextBox();
            this.lblFlanGasketNote = new System.Windows.Forms.Label();
            this.lblFlanSF = new System.Windows.Forms.Label();
            this.txtFlanSF = new System.Windows.Forms.TextBox();
            this.btnFlanCalc = new System.Windows.Forms.Button();

            // === Tab6: 推荐直径 input controls ===
            this.grpRecommend = new System.Windows.Forms.GroupBox();
            this.lblRecGrade = new System.Windows.Forms.Label();
            this.cboRecGrade = new System.Windows.Forms.ComboBox();
            this.lblRecF = new System.Windows.Forms.Label();
            this.txtRecF = new System.Windows.Forms.TextBox();
            this.lblRecFUnit = new System.Windows.Forms.Label();
            this.lblRecN = new System.Windows.Forms.Label();
            this.numRecN = new System.Windows.Forms.NumericUpDown();
            this.lblRecSF = new System.Windows.Forms.Label();
            this.txtRecSF = new System.Windows.Forms.TextBox();
            this.lblRecK = new System.Windows.Forms.Label();
            this.txtRecK = new System.Windows.Forms.TextBox();
            this.lblRecCb = new System.Windows.Forms.Label();
            this.txtRecCb = new System.Windows.Forms.TextBox();
            this.chkRecFine = new System.Windows.Forms.CheckBox();
            this.btnRecCalc = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // ============================
            // TabControl
            // ============================
            this.tabMain.Location = new System.Drawing.Point(8, 8);
            this.tabMain.Size = new System.Drawing.Size(360, 520);
            this.tabMain.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
                this.tabTension, this.tabShear, this.tabCombined,
                this.tabEccentric, this.tabFlange, this.tabRecommend
            });

            // ============================
            // Tab1: 轴向拉伸
            // ============================
            this.tabTension.Text = "轴向拉伸";
            this.tabTension.Padding = new System.Windows.Forms.Padding(3);
            this.tabTension.Controls.Add(this.grpTension);

            int y = 12;
            this.grpTension.Text = "输入参数";
            this.grpTension.Location = new System.Drawing.Point(3, 3);
            this.grpTension.Size = new System.Drawing.Size(346, 490);

            AddLabelRow(this.grpTension, this.lblTensionConn, "连接形式:", 15, ref y);
            AddComboRow(this.grpTension, this.cboTensionConn, 120, ref y, new object[] { "普通螺栓", "铰制孔螺栓", "双头螺柱", "螺钉" });
            AddLabelRow(this.grpTension, this.lblTensionSpec, "螺栓规格:", 15, ref y);
            AddComboRow(this.grpTension, this.cboTensionSpec, 120, ref y);
            AddLabelRow(this.grpTension, this.lblTensionGrade, "材料等级:", 15, ref y);
            AddComboRow(this.grpTension, this.cboTensionGrade, 120, ref y);
            AddLabelRow(this.grpTension, this.lblTensionF, "轴向力 F:", 15, ref y);
            AddTextBoxRow(this.grpTension, this.txtTensionF, "10", 120, ref y);
            this.lblTensionFUnit.Text = "kN"; this.lblTensionFUnit.AutoSize = true;
            this.lblTensionFUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpTension.Controls.Add(this.lblTensionFUnit);
            AddLabelRow(this.grpTension, this.lblTensionN, "螺栓数量 n:", 15, ref y);
            AddNumericRow(this.grpTension, this.numTensionN, 4, 1, 100, 120, ref y);
            AddLabelRow(this.grpTension, this.lblTensionK, "预紧力系数 K:", 15, ref y);
            AddTextBoxRow(this.grpTension, this.txtTensionK, "2.0", 120, ref y);
            this.lblTensionKNote.Text = "1.5~2.5"; this.lblTensionKNote.AutoSize = true;
            this.lblTensionKNote.ForeColor = System.Drawing.Color.Gray;
            this.lblTensionKNote.Location = new System.Drawing.Point(255, y - 23);
            this.grpTension.Controls.Add(this.lblTensionKNote);
            AddLabelRow(this.grpTension, this.lblTensionK2, "残余预紧力K2:", 15, ref y);
            AddTextBoxRow(this.grpTension, this.txtTensionK2, "1.0", 120, ref y);
            this.lblTensionK2Note.Text = "静载0.6~1.0"; this.lblTensionK2Note.AutoSize = true;
            this.lblTensionK2Note.ForeColor = System.Drawing.Color.Gray;
            this.lblTensionK2Note.Location = new System.Drawing.Point(255, y - 23);
            this.grpTension.Controls.Add(this.lblTensionK2Note);
            AddLabelRow(this.grpTension, this.lblTensionK1, "拧紧系数K1:", 15, ref y);
            AddComboRow(this.grpTension, this.cboTensionK1, 120, ref y, new object[] { "干摩擦 0.20", "有润滑 0.15" });
            AddLabelRow(this.grpTension, this.lblTensionCb, "刚度比Cb/(Cb+Cm):", 15, ref y);
            AddTextBoxRow(this.grpTension, this.txtTensionCb, "0.3", 120, ref y);
            AddLabelRow(this.grpTension, this.lblTensionCond, "工况:", 15, ref y);
            AddComboRow(this.grpTension, this.cboTensionCond, 120, ref y, new object[] { "静载", "脉动循环", "对称循环" });
            AddLabelRow(this.grpTension, this.lblTensionSF, "许用安全系数:", 15, ref y);
            AddTextBoxRow(this.grpTension, this.txtTensionSF, "1.5", 120, ref y);
            this.btnTensionCalc.Text = "计 算"; this.btnTensionCalc.Location = new System.Drawing.Point(30, y + 5);
            this.btnTensionCalc.Size = new System.Drawing.Size(130, 32);
            this.btnTensionCalc.UseVisualStyleBackColor = true;
            this.btnTensionCalc.Click += new System.EventHandler(this.btnTensionCalc_Click);
            this.grpTension.Controls.Add(this.btnTensionCalc);
            this.btnTensionExport.Text = "导出HTML"; this.btnTensionExport.Location = new System.Drawing.Point(170, y + 5);
            this.btnTensionExport.Size = new System.Drawing.Size(130, 32);
            this.btnTensionExport.UseVisualStyleBackColor = true;
            this.btnTensionExport.Click += new System.EventHandler(this.btnExport_Click);
            this.grpTension.Controls.Add(this.btnTensionExport);

            // ============================
            // Tab2: 横向剪切
            // ============================
            this.tabShear.Text = "横向剪切";
            this.tabShear.Padding = new System.Windows.Forms.Padding(3);
            this.tabShear.Controls.Add(this.grpShear);

            y = 12;
            this.grpShear.Text = "输入参数";
            this.grpShear.Location = new System.Drawing.Point(3, 3);
            this.grpShear.Size = new System.Drawing.Size(346, 490);

            AddLabelRow(this.grpShear, this.lblShearConn, "连接形式:", 15, ref y);
            AddComboRow(this.grpShear, this.cboShearConn, 120, ref y, new object[] { "普通螺栓(摩擦型)", "铰制孔螺栓(承压型)" });
            AddLabelRow(this.grpShear, this.lblShearSpec, "螺栓规格:", 15, ref y);
            AddComboRow(this.grpShear, this.cboShearSpec, 120, ref y);
            AddLabelRow(this.grpShear, this.lblShearGrade, "材料等级:", 15, ref y);
            AddComboRow(this.grpShear, this.cboShearGrade, 120, ref y);
            AddLabelRow(this.grpShear, this.lblShearFs, "横向力 Fs:", 15, ref y);
            AddTextBoxRow(this.grpShear, this.txtShearFs, "20", 120, ref y);
            this.lblShearFsUnit.Text = "kN"; this.lblShearFsUnit.AutoSize = true;
            this.lblShearFsUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpShear.Controls.Add(this.lblShearFsUnit);
            AddLabelRow(this.grpShear, this.lblShearN, "螺栓数量 n:", 15, ref y);
            AddNumericRow(this.grpShear, this.numShearN, 4, 1, 100, 120, ref y);
            AddLabelRow(this.grpShear, this.lblShearMu, "摩擦系数 μ:", 15, ref y);
            AddTextBoxRow(this.grpShear, this.txtShearMu, "0.15", 120, ref y);
            this.lblShearMuNote.Text = "钢0.15~0.25"; this.lblShearMuNote.AutoSize = true;
            this.lblShearMuNote.ForeColor = System.Drawing.Color.Gray;
            this.lblShearMuNote.Location = new System.Drawing.Point(255, y - 23);
            this.grpShear.Controls.Add(this.lblShearMuNote);
            AddLabelRow(this.grpShear, this.lblShearM, "接合面数 m:", 15, ref y);
            AddNumericRow(this.grpShear, this.numShearM, 1, 1, 4, 120, ref y);
            AddLabelRow(this.grpShear, this.lblShearSF, "许用安全系数:", 15, ref y);
            AddTextBoxRow(this.grpShear, this.txtShearSF, "1.5", 120, ref y);
            this.btnShearCalc.Text = "计 算"; this.btnShearCalc.Location = new System.Drawing.Point(80, y + 5);
            this.btnShearCalc.Size = new System.Drawing.Size(180, 32);
            this.btnShearCalc.UseVisualStyleBackColor = true;
            this.btnShearCalc.Click += new System.EventHandler(this.btnShearCalc_Click);
            this.grpShear.Controls.Add(this.btnShearCalc);

            // ============================
            // Tab3: 拉剪组合
            // ============================
            this.tabCombined.Text = "拉剪组合";
            this.tabCombined.Padding = new System.Windows.Forms.Padding(3);
            this.tabCombined.Controls.Add(this.grpCombined);

            y = 12;
            this.grpCombined.Text = "输入参数";
            this.grpCombined.Location = new System.Drawing.Point(3, 3);
            this.grpCombined.Size = new System.Drawing.Size(346, 490);

            AddLabelRow(this.grpCombined, this.lblCombConn, "连接形式:", 15, ref y);
            AddComboRow(this.grpCombined, this.cboCombConn, 120, ref y, new object[] { "普通螺栓", "铰制孔螺栓" });
            AddLabelRow(this.grpCombined, this.lblCombSpec, "螺栓规格:", 15, ref y);
            AddComboRow(this.grpCombined, this.cboCombSpec, 120, ref y);
            AddLabelRow(this.grpCombined, this.lblCombGrade, "材料等级:", 15, ref y);
            AddComboRow(this.grpCombined, this.cboCombGrade, 120, ref y);
            AddLabelRow(this.grpCombined, this.lblCombF, "轴向力 F:", 15, ref y);
            AddTextBoxRow(this.grpCombined, this.txtCombF, "20", 120, ref y);
            this.lblCombFUnit.Text = "kN"; this.lblCombFUnit.AutoSize = true;
            this.lblCombFUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpCombined.Controls.Add(this.lblCombFUnit);
            AddLabelRow(this.grpCombined, this.lblCombFs, "横向力 Fs:", 15, ref y);
            AddTextBoxRow(this.grpCombined, this.txtCombFs, "10", 120, ref y);
            this.lblCombFsUnit.Text = "kN"; this.lblCombFsUnit.AutoSize = true;
            this.lblCombFsUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpCombined.Controls.Add(this.lblCombFsUnit);
            AddLabelRow(this.grpCombined, this.lblCombN, "螺栓数量 n:", 15, ref y);
            AddNumericRow(this.grpCombined, this.numCombN, 4, 1, 100, 120, ref y);
            AddLabelRow(this.grpCombined, this.lblCombK, "预紧力系数 K:", 15, ref y);
            AddTextBoxRow(this.grpCombined, this.txtCombK, "2.0", 120, ref y);
            AddLabelRow(this.grpCombined, this.lblCombCb, "刚度比 Cb/(Cb+Cm):", 15, ref y);
            AddTextBoxRow(this.grpCombined, this.txtCombCb, "0.3", 120, ref y);
            AddLabelRow(this.grpCombined, this.lblCombMu, "摩擦系数 μ:", 15, ref y);
            AddTextBoxRow(this.grpCombined, this.txtCombMu, "0.15", 120, ref y);
            AddLabelRow(this.grpCombined, this.lblCombM, "接合面数 m:", 15, ref y);
            AddNumericRow(this.grpCombined, this.numCombM, 1, 1, 4, 120, ref y);
            AddLabelRow(this.grpCombined, this.lblCombSF, "许用安全系数:", 15, ref y);
            AddTextBoxRow(this.grpCombined, this.txtCombSF, "1.5", 120, ref y);
            this.btnCombCalc.Text = "计 算"; this.btnCombCalc.Location = new System.Drawing.Point(80, y + 5);
            this.btnCombCalc.Size = new System.Drawing.Size(180, 32);
            this.btnCombCalc.UseVisualStyleBackColor = true;
            this.btnCombCalc.Click += new System.EventHandler(this.btnCombCalc_Click);
            this.grpCombined.Controls.Add(this.btnCombCalc);

            // ============================
            // Tab4: 偏心载荷
            // ============================
            this.tabEccentric.Text = "偏心载荷";
            this.tabEccentric.Padding = new System.Windows.Forms.Padding(3);
            this.tabEccentric.Controls.Add(this.grpEccentric);

            y = 12;
            this.grpEccentric.Text = "输入参数";
            this.grpEccentric.Location = new System.Drawing.Point(3, 3);
            this.grpEccentric.Size = new System.Drawing.Size(346, 490);

            AddLabelRow(this.grpEccentric, this.lblEccSpec, "螺栓规格:", 15, ref y);
            AddComboRow(this.grpEccentric, this.cboEccSpec, 120, ref y);
            AddLabelRow(this.grpEccentric, this.lblEccGrade, "材料等级:", 15, ref y);
            AddComboRow(this.grpEccentric, this.cboEccGrade, 120, ref y);
            AddLabelRow(this.grpEccentric, this.lblEccF, "载荷 F:", 15, ref y);
            AddTextBoxRow(this.grpEccentric, this.txtEccF, "50", 120, ref y);
            this.lblEccFUnit.Text = "kN"; this.lblEccFUnit.AutoSize = true;
            this.lblEccFUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpEccentric.Controls.Add(this.lblEccFUnit);
            AddLabelRow(this.grpEccentric, this.lblEccE, "偏心距 e:", 15, ref y);
            AddTextBoxRow(this.grpEccentric, this.txtEccE, "200", 120, ref y);
            this.lblEccEUnit.Text = "mm"; this.lblEccEUnit.AutoSize = true;
            this.lblEccEUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpEccentric.Controls.Add(this.lblEccEUnit);
            AddLabelRow(this.grpEccentric, this.lblEccN, "螺栓数量 n:", 15, ref y);
            AddNumericRow(this.grpEccentric, this.numEccN, 4, 2, 100, 120, ref y);
            AddLabelRow(this.grpEccentric, this.lblEccSpacing, "螺栓间距:", 15, ref y);
            AddTextBoxRow(this.grpEccentric, this.txtEccSpacing, "80", 120, ref y);
            this.lblEccSpacingUnit.Text = "mm"; this.lblEccSpacingUnit.AutoSize = true;
            this.lblEccSpacingUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpEccentric.Controls.Add(this.lblEccSpacingUnit);
            AddLabelRow(this.grpEccentric, this.lblEccK, "预紧力系数 K:", 15, ref y);
            AddTextBoxRow(this.grpEccentric, this.txtEccK, "2.0", 120, ref y);
            AddLabelRow(this.grpEccentric, this.lblEccCb, "刚度比 Cb/(Cb+Cm):", 15, ref y);
            AddTextBoxRow(this.grpEccentric, this.txtEccCb, "0.3", 120, ref y);
            AddLabelRow(this.grpEccentric, this.lblEccSF, "许用安全系数:", 15, ref y);
            AddTextBoxRow(this.grpEccentric, this.txtEccSF, "1.5", 120, ref y);
            this.btnEccCalc.Text = "计 算"; this.btnEccCalc.Location = new System.Drawing.Point(80, y + 5);
            this.btnEccCalc.Size = new System.Drawing.Size(180, 32);
            this.btnEccCalc.UseVisualStyleBackColor = true;
            this.btnEccCalc.Click += new System.EventHandler(this.btnEccCalc_Click);
            this.grpEccentric.Controls.Add(this.btnEccCalc);

            // ============================
            // Tab5: 法兰连接
            // ============================
            this.tabFlange.Text = "法兰连接";
            this.tabFlange.Padding = new System.Windows.Forms.Padding(3);
            this.tabFlange.Controls.Add(this.grpFlange);

            y = 12;
            this.grpFlange.Text = "输入参数";
            this.grpFlange.Location = new System.Drawing.Point(3, 3);
            this.grpFlange.Size = new System.Drawing.Size(346, 490);

            AddLabelRow(this.grpFlange, this.lblFlanSpec, "螺栓规格:", 15, ref y);
            AddComboRow(this.grpFlange, this.cboFlanSpec, 120, ref y);
            AddLabelRow(this.grpFlange, this.lblFlanGrade, "材料等级:", 15, ref y);
            AddComboRow(this.grpFlange, this.cboFlanGrade, 120, ref y);
            AddLabelRow(this.grpFlange, this.lblFlanP, "设计压力 P:", 15, ref y);
            AddTextBoxRow(this.grpFlange, this.txtFlanP, "1.6", 120, ref y);
            this.lblFlanPUnit.Text = "MPa"; this.lblFlanPUnit.AutoSize = true;
            this.lblFlanPUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpFlange.Controls.Add(this.lblFlanPUnit);
            AddLabelRow(this.grpFlange, this.lblFlanDf, "法兰内径 Df:", 15, ref y);
            AddTextBoxRow(this.grpFlange, this.txtFlanDf, "200", 120, ref y);
            this.lblFlanDfUnit.Text = "mm"; this.lblFlanDfUnit.AutoSize = true;
            this.lblFlanDfUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpFlange.Controls.Add(this.lblFlanDfUnit);
            AddLabelRow(this.grpFlange, this.lblFlanDb, "螺栓分布圆Db:", 15, ref y);
            AddTextBoxRow(this.grpFlange, this.txtFlanDb, "280", 120, ref y);
            this.lblFlanDbUnit.Text = "mm"; this.lblFlanDbUnit.AutoSize = true;
            this.lblFlanDbUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpFlange.Controls.Add(this.lblFlanDbUnit);
            AddLabelRow(this.grpFlange, this.lblFlanN, "螺栓数量 n:", 15, ref y);
            AddNumericRow(this.grpFlange, this.numFlanN, 8, 1, 100, 120, ref y);
            AddLabelRow(this.grpFlange, this.lblFlanK, "预紧力系数 K:", 15, ref y);
            AddTextBoxRow(this.grpFlange, this.txtFlanK, "2.5", 120, ref y);
            AddLabelRow(this.grpFlange, this.lblFlanCb, "刚度比 Cb/(Cb+Cm):", 15, ref y);
            AddTextBoxRow(this.grpFlange, this.txtFlanCb, "0.3", 120, ref y);
            AddLabelRow(this.grpFlange, this.lblFlanGasket, "垫片系数 m:", 15, ref y);
            AddTextBoxRow(this.grpFlange, this.txtFlanGasket, "2.0", 120, ref y);
            this.lblFlanGasketNote.Text = "金属垫2~3"; this.lblFlanGasketNote.AutoSize = true;
            this.lblFlanGasketNote.ForeColor = System.Drawing.Color.Gray;
            this.lblFlanGasketNote.Location = new System.Drawing.Point(255, y - 23);
            this.grpFlange.Controls.Add(this.lblFlanGasketNote);
            AddLabelRow(this.grpFlange, this.lblFlanSF, "许用安全系数:", 15, ref y);
            AddTextBoxRow(this.grpFlange, this.txtFlanSF, "1.5", 120, ref y);
            this.btnFlanCalc.Text = "计 算"; this.btnFlanCalc.Location = new System.Drawing.Point(80, y + 5);
            this.btnFlanCalc.Size = new System.Drawing.Size(180, 32);
            this.btnFlanCalc.UseVisualStyleBackColor = true;
            this.btnFlanCalc.Click += new System.EventHandler(this.btnFlanCalc_Click);
            this.grpFlange.Controls.Add(this.btnFlanCalc);

            // ============================
            // Tab6: 推荐直径
            // ============================
            this.tabRecommend.Text = "推荐直径";
            this.tabRecommend.Padding = new System.Windows.Forms.Padding(3);
            this.tabRecommend.Controls.Add(this.grpRecommend);

            y = 12;
            this.grpRecommend.Text = "输入参数";
            this.grpRecommend.Location = new System.Drawing.Point(3, 3);
            this.grpRecommend.Size = new System.Drawing.Size(346, 490);

            AddLabelRow(this.grpRecommend, this.lblRecGrade, "材料等级:", 15, ref y);
            AddComboRow(this.grpRecommend, this.cboRecGrade, 120, ref y);
            AddLabelRow(this.grpRecommend, this.lblRecF, "轴向力 F:", 15, ref y);
            AddTextBoxRow(this.grpRecommend, this.txtRecF, "100", 120, ref y);
            this.lblRecFUnit.Text = "kN"; this.lblRecFUnit.AutoSize = true;
            this.lblRecFUnit.Location = new System.Drawing.Point(255, y - 23);
            this.grpRecommend.Controls.Add(this.lblRecFUnit);
            AddLabelRow(this.grpRecommend, this.lblRecN, "螺栓数量 n:", 15, ref y);
            AddNumericRow(this.grpRecommend, this.numRecN, 4, 1, 100, 120, ref y);
            AddLabelRow(this.grpRecommend, this.lblRecSF, "安全系数:", 15, ref y);
            AddTextBoxRow(this.grpRecommend, this.txtRecSF, "1.5", 120, ref y);
            AddLabelRow(this.grpRecommend, this.lblRecK, "预紧力系数 K:", 15, ref y);
            AddTextBoxRow(this.grpRecommend, this.txtRecK, "2.0", 120, ref y);
            AddLabelRow(this.grpRecommend, this.lblRecCb, "刚度比 Cb/(Cb+Cm):", 15, ref y);
            AddTextBoxRow(this.grpRecommend, this.txtRecCb, "0.3", 120, ref y);
            this.chkRecFine.Text = "包含细牙系列"; this.chkRecFine.AutoSize = true;
            this.chkRecFine.Location = new System.Drawing.Point(120, y);
            this.chkRecFine.Checked = false;
            this.grpRecommend.Controls.Add(this.chkRecFine);
            y += 28;
            this.btnRecCalc.Text = "推荐计算"; this.btnRecCalc.Location = new System.Drawing.Point(80, y + 5);
            this.btnRecCalc.Size = new System.Drawing.Size(180, 32);
            this.btnRecCalc.UseVisualStyleBackColor = true;
            this.btnRecCalc.Click += new System.EventHandler(this.btnRecCalc_Click);
            this.grpRecommend.Controls.Add(this.btnRecCalc);

            // ============================
            // grpResult
            // ============================
            this.grpResult.Controls.Add(this.dgvResult);
            this.grpResult.Location = new System.Drawing.Point(378, 8);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(520, 420);
            this.grpResult.Text = "计算结果";

            // dgvResult
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvResult.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvResult.Location = new System.Drawing.Point(10, 22);
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.Size = new System.Drawing.Size(500, 390);
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.colItem.HeaderText = "项目"; this.colItem.Name = "colItem"; this.colItem.FillWeight = 30;
            this.colValue.HeaderText = "数值"; this.colValue.Name = "colValue"; this.colValue.FillWeight = 25;
            this.colUnit.HeaderText = "单位"; this.colUnit.Name = "colUnit"; this.colUnit.FillWeight = 15;
            this.colNote.HeaderText = "说明/公式"; this.colNote.Name = "colNote"; this.colNote.FillWeight = 30;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colItem, this.colValue, this.colUnit, this.colNote
            });

            // ============================
            // grpStatus
            // ============================
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Controls.Add(this.lblFatigueStatus);
            this.grpStatus.Location = new System.Drawing.Point(378, 434);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(520, 48);
            this.grpStatus.Text = "校核结论";

            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(10, 16);
            this.lblStatus.Size = new System.Drawing.Size(240, 26);
            this.lblStatus.Text = "待计算";

            this.lblFatigueStatus.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.lblFatigueStatus.Location = new System.Drawing.Point(260, 16);
            this.lblFatigueStatus.Size = new System.Drawing.Size(250, 26);
            this.lblFatigueStatus.Text = "";

            // lblStandard
            this.lblStandard.Location = new System.Drawing.Point(378, 488);
            this.lblStandard.Size = new System.Drawing.Size(520, 16);
            this.lblStandard.Font = new System.Drawing.Font("Microsoft YaHei", 8F);
            this.lblStandard.ForeColor = System.Drawing.Color.Gray;
            this.lblStandard.Text = "参考标准: GB/T 196-2003 | GB/T 3098.1 | GB 50017-2017 | GB/T 1228~1231";

            // ============================
            // MainForm
            // ============================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(940, 560);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.lblStandard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(930, 560);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "螺栓校核计算";
            this.Name = "MainForm";

            this.ResumeLayout(false);
        }

        // Helper methods for UI layout
        private void AddLabelRow(System.Windows.Forms.GroupBox grp, System.Windows.Forms.Label lbl, string text, int x, ref int y)
        {
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(x, y + 3);
            grp.Controls.Add(lbl);
        }

        private void AddComboRow(System.Windows.Forms.GroupBox grp, System.Windows.Forms.ComboBox cbo, int x, ref int y, object[] items = null)
        {
            cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbo.Location = new System.Drawing.Point(x, y);
            cbo.Size = new System.Drawing.Size(125, 21);
            if (items != null) cbo.Items.AddRange(items);
            if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
            grp.Controls.Add(cbo);
            y += 30;
        }

        private void AddTextBoxRow(System.Windows.Forms.GroupBox grp, System.Windows.Forms.TextBox txt, string defaultVal, int x, ref int y)
        {
            txt.Location = new System.Drawing.Point(x, y);
            txt.Size = new System.Drawing.Size(125, 20);
            txt.Text = defaultVal;
            grp.Controls.Add(txt);
            y += 30;
        }

        private void AddNumericRow(System.Windows.Forms.GroupBox grp, System.Windows.Forms.NumericUpDown num, int val, int min, int max, int x, ref int y)
        {
            num.Location = new System.Drawing.Point(x, y);
            num.Size = new System.Drawing.Size(125, 20);
            num.Minimum = min;
            num.Maximum = max;
            num.Value = val;
            grp.Controls.Add(num);
            y += 30;
        }

        // === Main controls ===
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabTension;
        private System.Windows.Forms.TabPage tabShear;
        private System.Windows.Forms.TabPage tabCombined;
        private System.Windows.Forms.TabPage tabEccentric;
        private System.Windows.Forms.TabPage tabFlange;
        private System.Windows.Forms.TabPage tabRecommend;

        // === Shared result ===
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblFatigueStatus;
        private System.Windows.Forms.Label lblStandard;

        // === Tab1: 轴向拉伸 ===
        private System.Windows.Forms.GroupBox grpTension;
        private System.Windows.Forms.Label lblTensionConn;
        private System.Windows.Forms.ComboBox cboTensionConn;
        private System.Windows.Forms.Label lblTensionSpec;
        private System.Windows.Forms.ComboBox cboTensionSpec;
        private System.Windows.Forms.Label lblTensionGrade;
        private System.Windows.Forms.ComboBox cboTensionGrade;
        private System.Windows.Forms.Label lblTensionF;
        private System.Windows.Forms.TextBox txtTensionF;
        private System.Windows.Forms.Label lblTensionFUnit;
        private System.Windows.Forms.Label lblTensionN;
        private System.Windows.Forms.NumericUpDown numTensionN;
        private System.Windows.Forms.Label lblTensionK;
        private System.Windows.Forms.TextBox txtTensionK;
        private System.Windows.Forms.Label lblTensionKNote;
        private System.Windows.Forms.Label lblTensionK2;
        private System.Windows.Forms.TextBox txtTensionK2;
        private System.Windows.Forms.Label lblTensionK2Note;
        private System.Windows.Forms.Label lblTensionK1;
        private System.Windows.Forms.ComboBox cboTensionK1;
        private System.Windows.Forms.Label lblTensionCb;
        private System.Windows.Forms.TextBox txtTensionCb;
        private System.Windows.Forms.Label lblTensionCond;
        private System.Windows.Forms.ComboBox cboTensionCond;
        private System.Windows.Forms.Label lblTensionSF;
        private System.Windows.Forms.TextBox txtTensionSF;
        private System.Windows.Forms.Button btnTensionCalc;
        private System.Windows.Forms.Button btnTensionExport;

        // === Tab2: 横向剪切 ===
        private System.Windows.Forms.GroupBox grpShear;
        private System.Windows.Forms.Label lblShearConn;
        private System.Windows.Forms.ComboBox cboShearConn;
        private System.Windows.Forms.Label lblShearSpec;
        private System.Windows.Forms.ComboBox cboShearSpec;
        private System.Windows.Forms.Label lblShearGrade;
        private System.Windows.Forms.ComboBox cboShearGrade;
        private System.Windows.Forms.Label lblShearFs;
        private System.Windows.Forms.TextBox txtShearFs;
        private System.Windows.Forms.Label lblShearFsUnit;
        private System.Windows.Forms.Label lblShearN;
        private System.Windows.Forms.NumericUpDown numShearN;
        private System.Windows.Forms.Label lblShearMu;
        private System.Windows.Forms.TextBox txtShearMu;
        private System.Windows.Forms.Label lblShearMuNote;
        private System.Windows.Forms.Label lblShearM;
        private System.Windows.Forms.NumericUpDown numShearM;
        private System.Windows.Forms.Label lblShearSF;
        private System.Windows.Forms.TextBox txtShearSF;
        private System.Windows.Forms.Button btnShearCalc;

        // === Tab3: 拉剪组合 ===
        private System.Windows.Forms.GroupBox grpCombined;
        private System.Windows.Forms.Label lblCombConn;
        private System.Windows.Forms.ComboBox cboCombConn;
        private System.Windows.Forms.Label lblCombSpec;
        private System.Windows.Forms.ComboBox cboCombSpec;
        private System.Windows.Forms.Label lblCombGrade;
        private System.Windows.Forms.ComboBox cboCombGrade;
        private System.Windows.Forms.Label lblCombF;
        private System.Windows.Forms.TextBox txtCombF;
        private System.Windows.Forms.Label lblCombFUnit;
        private System.Windows.Forms.Label lblCombFs;
        private System.Windows.Forms.TextBox txtCombFs;
        private System.Windows.Forms.Label lblCombFsUnit;
        private System.Windows.Forms.Label lblCombN;
        private System.Windows.Forms.NumericUpDown numCombN;
        private System.Windows.Forms.Label lblCombK;
        private System.Windows.Forms.TextBox txtCombK;
        private System.Windows.Forms.Label lblCombCb;
        private System.Windows.Forms.TextBox txtCombCb;
        private System.Windows.Forms.Label lblCombMu;
        private System.Windows.Forms.TextBox txtCombMu;
        private System.Windows.Forms.Label lblCombM;
        private System.Windows.Forms.NumericUpDown numCombM;
        private System.Windows.Forms.Label lblCombSF;
        private System.Windows.Forms.TextBox txtCombSF;
        private System.Windows.Forms.Button btnCombCalc;

        // === Tab4: 偏心载荷 ===
        private System.Windows.Forms.GroupBox grpEccentric;
        private System.Windows.Forms.Label lblEccSpec;
        private System.Windows.Forms.ComboBox cboEccSpec;
        private System.Windows.Forms.Label lblEccGrade;
        private System.Windows.Forms.ComboBox cboEccGrade;
        private System.Windows.Forms.Label lblEccF;
        private System.Windows.Forms.TextBox txtEccF;
        private System.Windows.Forms.Label lblEccFUnit;
        private System.Windows.Forms.Label lblEccE;
        private System.Windows.Forms.TextBox txtEccE;
        private System.Windows.Forms.Label lblEccEUnit;
        private System.Windows.Forms.Label lblEccN;
        private System.Windows.Forms.NumericUpDown numEccN;
        private System.Windows.Forms.Label lblEccSpacing;
        private System.Windows.Forms.TextBox txtEccSpacing;
        private System.Windows.Forms.Label lblEccSpacingUnit;
        private System.Windows.Forms.Label lblEccK;
        private System.Windows.Forms.TextBox txtEccK;
        private System.Windows.Forms.Label lblEccCb;
        private System.Windows.Forms.TextBox txtEccCb;
        private System.Windows.Forms.Label lblEccSF;
        private System.Windows.Forms.TextBox txtEccSF;
        private System.Windows.Forms.Button btnEccCalc;

        // === Tab5: 法兰连接 ===
        private System.Windows.Forms.GroupBox grpFlange;
        private System.Windows.Forms.Label lblFlanSpec;
        private System.Windows.Forms.ComboBox cboFlanSpec;
        private System.Windows.Forms.Label lblFlanGrade;
        private System.Windows.Forms.ComboBox cboFlanGrade;
        private System.Windows.Forms.Label lblFlanP;
        private System.Windows.Forms.TextBox txtFlanP;
        private System.Windows.Forms.Label lblFlanPUnit;
        private System.Windows.Forms.Label lblFlanDf;
        private System.Windows.Forms.TextBox txtFlanDf;
        private System.Windows.Forms.Label lblFlanDfUnit;
        private System.Windows.Forms.Label lblFlanDb;
        private System.Windows.Forms.TextBox txtFlanDb;
        private System.Windows.Forms.Label lblFlanDbUnit;
        private System.Windows.Forms.Label lblFlanN;
        private System.Windows.Forms.NumericUpDown numFlanN;
        private System.Windows.Forms.Label lblFlanK;
        private System.Windows.Forms.TextBox txtFlanK;
        private System.Windows.Forms.Label lblFlanCb;
        private System.Windows.Forms.TextBox txtFlanCb;
        private System.Windows.Forms.Label lblFlanGasket;
        private System.Windows.Forms.TextBox txtFlanGasket;
        private System.Windows.Forms.Label lblFlanGasketNote;
        private System.Windows.Forms.Label lblFlanSF;
        private System.Windows.Forms.TextBox txtFlanSF;
        private System.Windows.Forms.Button btnFlanCalc;

        // === Tab6: 推荐直径 ===
        private System.Windows.Forms.GroupBox grpRecommend;
        private System.Windows.Forms.Label lblRecGrade;
        private System.Windows.Forms.ComboBox cboRecGrade;
        private System.Windows.Forms.Label lblRecF;
        private System.Windows.Forms.TextBox txtRecF;
        private System.Windows.Forms.Label lblRecFUnit;
        private System.Windows.Forms.Label lblRecN;
        private System.Windows.Forms.NumericUpDown numRecN;
        private System.Windows.Forms.Label lblRecSF;
        private System.Windows.Forms.TextBox txtRecSF;
        private System.Windows.Forms.Label lblRecK;
        private System.Windows.Forms.TextBox txtRecK;
        private System.Windows.Forms.Label lblRecCb;
        private System.Windows.Forms.TextBox txtRecCb;
        private System.Windows.Forms.CheckBox chkRecFine;
        private System.Windows.Forms.Button btnRecCalc;
    }
}
