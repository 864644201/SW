namespace SpringDesign
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabCompression = new System.Windows.Forms.TabPage();
            this.tabExtension = new System.Windows.Forms.TabPage();
            this.tabTorsion = new System.Windows.Forms.TabPage();

            // ============ 压缩弹簧 Tab ============
            this.grpCompInput = new System.Windows.Forms.GroupBox();
            this.lblCompMaterial = new System.Windows.Forms.Label();
            this.cboCompMaterial = new System.Windows.Forms.ComboBox();
            this.lblCompLoadCat = new System.Windows.Forms.Label();
            this.cboCompLoadCat = new System.Windows.Forms.ComboBox();
            this.lblCompD = new System.Windows.Forms.Label();
            this.txtCompD = new System.Windows.Forms.TextBox();
            this.lblCompDUnit = new System.Windows.Forms.Label();
            this.lblCompDm = new System.Windows.Forms.Label();
            this.txtCompDm = new System.Windows.Forms.TextBox();
            this.lblCompDmUnit = new System.Windows.Forms.Label();
            this.lblCompN = new System.Windows.Forms.Label();
            this.txtCompN = new System.Windows.Forms.TextBox();
            this.lblCompF1 = new System.Windows.Forms.Label();
            this.txtCompF1 = new System.Windows.Forms.TextBox();
            this.lblCompF1Unit = new System.Windows.Forms.Label();
            this.lblCompFn = new System.Windows.Forms.Label();
            this.txtCompFn = new System.Windows.Forms.TextBox();
            this.lblCompFnUnit = new System.Windows.Forms.Label();
            this.lblCompEndType = new System.Windows.Forms.Label();
            this.cboCompEndType = new System.Windows.Forms.ComboBox();
            this.btnCompCalc = new System.Windows.Forms.Button();
            this.btnCompReset = new System.Windows.Forms.Button();

            this.grpCompResult = new System.Windows.Forms.GroupBox();
            this.dgvCompResult = new System.Windows.Forms.DataGridView();
            this.colCompParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.lblCompStatus = new System.Windows.Forms.Label();
            this.txtCompStatus = new System.Windows.Forms.TextBox();

            // ============ 拉伸弹簧 Tab ============
            this.grpExtInput = new System.Windows.Forms.GroupBox();
            this.lblExtMaterial = new System.Windows.Forms.Label();
            this.cboExtMaterial = new System.Windows.Forms.ComboBox();
            this.lblExtLoadCat = new System.Windows.Forms.Label();
            this.cboExtLoadCat = new System.Windows.Forms.ComboBox();
            this.lblExtD = new System.Windows.Forms.Label();
            this.txtExtD = new System.Windows.Forms.TextBox();
            this.lblExtDUnit = new System.Windows.Forms.Label();
            this.lblExtDm = new System.Windows.Forms.Label();
            this.txtExtDm = new System.Windows.Forms.TextBox();
            this.lblExtDmUnit = new System.Windows.Forms.Label();
            this.lblExtN = new System.Windows.Forms.Label();
            this.txtExtN = new System.Windows.Forms.TextBox();
            this.lblExtF1 = new System.Windows.Forms.Label();
            this.txtExtF1 = new System.Windows.Forms.TextBox();
            this.lblExtF1Unit = new System.Windows.Forms.Label();
            this.lblExtFn = new System.Windows.Forms.Label();
            this.txtExtFn = new System.Windows.Forms.TextBox();
            this.lblExtFnUnit = new System.Windows.Forms.Label();
            this.lblExtInitTension = new System.Windows.Forms.Label();
            this.txtExtInitTension = new System.Windows.Forms.TextBox();
            this.lblExtInitTensionUnit = new System.Windows.Forms.Label();
            this.btnExtCalc = new System.Windows.Forms.Button();
            this.btnExtReset = new System.Windows.Forms.Button();

            this.grpExtResult = new System.Windows.Forms.GroupBox();
            this.dgvExtResult = new System.Windows.Forms.DataGridView();
            this.colExtParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExtValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExtUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExtDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.lblExtStatus = new System.Windows.Forms.Label();
            this.txtExtStatus = new System.Windows.Forms.TextBox();

            // ============ 扭转弹簧 Tab ============
            this.grpTorInput = new System.Windows.Forms.GroupBox();
            this.lblTorMaterial = new System.Windows.Forms.Label();
            this.cboTorMaterial = new System.Windows.Forms.ComboBox();
            this.lblTorLoadCat = new System.Windows.Forms.Label();
            this.cboTorLoadCat = new System.Windows.Forms.ComboBox();
            this.lblTorD = new System.Windows.Forms.Label();
            this.txtTorD = new System.Windows.Forms.TextBox();
            this.lblTorDUnit = new System.Windows.Forms.Label();
            this.lblTorDm = new System.Windows.Forms.Label();
            this.txtTorDm = new System.Windows.Forms.TextBox();
            this.lblTorDmUnit = new System.Windows.Forms.Label();
            this.lblTorN = new System.Windows.Forms.Label();
            this.txtTorN = new System.Windows.Forms.TextBox();
            this.lblTorM = new System.Windows.Forms.Label();
            this.txtTorM = new System.Windows.Forms.TextBox();
            this.lblTorMUnit = new System.Windows.Forms.Label();
            this.btnTorCalc = new System.Windows.Forms.Button();
            this.btnTorReset = new System.Windows.Forms.Button();

            this.grpTorResult = new System.Windows.Forms.GroupBox();
            this.dgvTorResult = new System.Windows.Forms.DataGridView();
            this.colTorParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTorValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTorUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTorDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.lblTorStatus = new System.Windows.Forms.Label();
            this.txtTorStatus = new System.Windows.Forms.TextBox();

            // ============ 开始布局 ============
            this.tabMain.SuspendLayout();
            this.tabCompression.SuspendLayout();
            this.tabExtension.SuspendLayout();
            this.tabTorsion.SuspendLayout();

            this.grpCompInput.SuspendLayout();
            this.grpCompResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompResult)).BeginInit();

            this.grpExtInput.SuspendLayout();
            this.grpExtResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtResult)).BeginInit();

            this.grpTorInput.SuspendLayout();
            this.grpTorResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTorResult)).BeginInit();

            this.SuspendLayout();

            // ============================================================
            // tabMain
            // ============================================================
            this.tabMain.Controls.Add(this.tabCompression);
            this.tabMain.Controls.Add(this.tabExtension);
            this.tabMain.Controls.Add(this.tabTorsion);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(984, 661);
            this.tabMain.TabIndex = 0;

            // ============================================================
            // tabCompression - 压缩弹簧设计
            // ============================================================
            this.tabCompression.Controls.Add(this.grpCompResult);
            this.tabCompression.Controls.Add(this.grpCompInput);
            this.tabCompression.Controls.Add(this.txtCompStatus);
            this.tabCompression.Controls.Add(this.lblCompStatus);
            this.tabCompression.Location = new System.Drawing.Point(4, 29);
            this.tabCompression.Name = "tabCompression";
            this.tabCompression.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompression.Size = new System.Drawing.Size(976, 628);
            this.tabCompression.TabIndex = 0;
            this.tabCompression.Text = "压缩弹簧设计";
            this.tabCompression.UseVisualStyleBackColor = true;

            // grpCompInput
            this.grpCompInput.Controls.Add(this.lblCompMaterial);
            this.grpCompInput.Controls.Add(this.cboCompMaterial);
            this.grpCompInput.Controls.Add(this.lblCompLoadCat);
            this.grpCompInput.Controls.Add(this.cboCompLoadCat);
            this.grpCompInput.Controls.Add(this.lblCompD);
            this.grpCompInput.Controls.Add(this.txtCompD);
            this.grpCompInput.Controls.Add(this.lblCompDUnit);
            this.grpCompInput.Controls.Add(this.lblCompDm);
            this.grpCompInput.Controls.Add(this.txtCompDm);
            this.grpCompInput.Controls.Add(this.lblCompDmUnit);
            this.grpCompInput.Controls.Add(this.lblCompN);
            this.grpCompInput.Controls.Add(this.txtCompN);
            this.grpCompInput.Controls.Add(this.lblCompF1);
            this.grpCompInput.Controls.Add(this.txtCompF1);
            this.grpCompInput.Controls.Add(this.lblCompF1Unit);
            this.grpCompInput.Controls.Add(this.lblCompFn);
            this.grpCompInput.Controls.Add(this.txtCompFn);
            this.grpCompInput.Controls.Add(this.lblCompFnUnit);
            this.grpCompInput.Controls.Add(this.lblCompEndType);
            this.grpCompInput.Controls.Add(this.cboCompEndType);
            this.grpCompInput.Controls.Add(this.btnCompCalc);
            this.grpCompInput.Controls.Add(this.btnCompReset);
            this.grpCompInput.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.grpCompInput.Location = new System.Drawing.Point(8, 6);
            this.grpCompInput.Name = "grpCompInput";
            this.grpCompInput.Size = new System.Drawing.Size(960, 160);
            this.grpCompInput.TabIndex = 0;
            this.grpCompInput.TabStop = false;
            this.grpCompInput.Text = "设计参数";

            // Row 1
            int y0 = 25;
            this.lblCompMaterial.AutoSize = true;
            this.lblCompMaterial.Location = new System.Drawing.Point(15, y0);
            this.lblCompMaterial.Text = "材料:";
            this.cboCompMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompMaterial.Location = new System.Drawing.Point(65, y0 - 3);
            this.cboCompMaterial.Size = new System.Drawing.Size(150, 25);

            this.lblCompLoadCat.AutoSize = true;
            this.lblCompLoadCat.Location = new System.Drawing.Point(230, y0);
            this.lblCompLoadCat.Text = "载荷类别:";
            this.cboCompLoadCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompLoadCat.Location = new System.Drawing.Point(295, y0 - 3);
            this.cboCompLoadCat.Size = new System.Drawing.Size(200, 25);
            this.cboCompLoadCat.Items.AddRange(new object[] {
                "I类载荷 (N<10000)",
                "II类载荷 (10000<=N<1000000)",
                "III类载荷 (N<=1000000)"
            });

            this.lblCompEndType.AutoSize = true;
            this.lblCompEndType.Location = new System.Drawing.Point(515, y0);
            this.lblCompEndType.Text = "端部型式:";
            this.cboCompEndType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompEndType.Location = new System.Drawing.Point(585, y0 - 3);
            this.cboCompEndType.Size = new System.Drawing.Size(180, 25);
            this.cboCompEndType.Items.AddRange(new object[] {
                "YI 两端圈并紧磨平",
                "YII 两端圈并紧不磨平"
            });

            // Row 2
            int y1 = 65;
            this.lblCompD.AutoSize = true;
            this.lblCompD.Location = new System.Drawing.Point(15, y1);
            this.lblCompD.Text = "丝径 d:";
            this.txtCompD.Location = new System.Drawing.Point(75, y1 - 3);
            this.txtCompD.Size = new System.Drawing.Size(80, 23);
            this.lblCompDUnit.AutoSize = true;
            this.lblCompDUnit.Location = new System.Drawing.Point(158, y1);
            this.lblCompDUnit.Text = "mm";

            this.lblCompDm.AutoSize = true;
            this.lblCompDm.Location = new System.Drawing.Point(200, y1);
            this.lblCompDm.Text = "中径 D:";
            this.txtCompDm.Location = new System.Drawing.Point(260, y1 - 3);
            this.txtCompDm.Size = new System.Drawing.Size(80, 23);
            this.lblCompDmUnit.AutoSize = true;
            this.lblCompDmUnit.Location = new System.Drawing.Point(343, y1);
            this.lblCompDmUnit.Text = "mm";

            this.lblCompN.AutoSize = true;
            this.lblCompN.Location = new System.Drawing.Point(390, y1);
            this.lblCompN.Text = "有效圈数 n:";
            this.txtCompN.Location = new System.Drawing.Point(490, y1 - 3);
            this.txtCompN.Size = new System.Drawing.Size(60, 23);

            // Row 3
            int y2 = 100;
            this.lblCompF1.AutoSize = true;
            this.lblCompF1.Location = new System.Drawing.Point(15, y2);
            this.lblCompF1.Text = "最小工作载荷 P1:";
            this.txtCompF1.Location = new System.Drawing.Point(140, y2 - 3);
            this.txtCompF1.Size = new System.Drawing.Size(80, 23);
            this.lblCompF1Unit.AutoSize = true;
            this.lblCompF1Unit.Location = new System.Drawing.Point(223, y2);
            this.lblCompF1Unit.Text = "N";

            this.lblCompFn.AutoSize = true;
            this.lblCompFn.Location = new System.Drawing.Point(260, y2);
            this.lblCompFn.Text = "最大工作载荷 Pn:";
            this.txtCompFn.Location = new System.Drawing.Point(385, y2 - 3);
            this.txtCompFn.Size = new System.Drawing.Size(80, 23);
            this.lblCompFnUnit.AutoSize = true;
            this.lblCompFnUnit.Location = new System.Drawing.Point(468, y2);
            this.lblCompFnUnit.Text = "N";

            this.btnCompCalc.Location = new System.Drawing.Point(570, y2 - 5);
            this.btnCompCalc.Size = new System.Drawing.Size(90, 30);
            this.btnCompCalc.Text = "计算";
            this.btnCompCalc.UseVisualStyleBackColor = true;
            this.btnCompCalc.Click += new System.EventHandler(this.btnCompCalc_Click);

            this.btnCompReset.Location = new System.Drawing.Point(670, y2 - 5);
            this.btnCompReset.Size = new System.Drawing.Size(90, 30);
            this.btnCompReset.Text = "重置";
            this.btnCompReset.UseVisualStyleBackColor = true;
            this.btnCompReset.Click += new System.EventHandler(this.btnCompReset_Click);

            // grpCompResult
            this.grpCompResult.Controls.Add(this.dgvCompResult);
            this.grpCompResult.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.grpCompResult.Location = new System.Drawing.Point(8, 170);
            this.grpCompResult.Name = "grpCompResult";
            this.grpCompResult.Size = new System.Drawing.Size(960, 400);
            this.grpCompResult.TabIndex = 1;
            this.grpCompResult.TabStop = false;
            this.grpCompResult.Text = "计算结果";

            // dgvCompResult
            this.dgvCompResult.AllowUserToAddRows = false;
            this.dgvCompResult.AllowUserToDeleteRows = false;
            this.dgvCompResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCompResult.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.dgvCompResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colCompParam, this.colCompValue, this.colCompUnit, this.colCompDesc
            });
            this.dgvCompResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompResult.Location = new System.Drawing.Point(3, 19);
            this.dgvCompResult.Name = "dgvCompResult";
            this.dgvCompResult.ReadOnly = true;
            this.dgvCompResult.RowHeadersVisible = false;
            this.dgvCompResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.colCompParam.HeaderText = "参数名称";
            this.colCompParam.Name = "colCompParam";
            this.colCompParam.Width = 250;
            this.colCompValue.HeaderText = "数值";
            this.colCompValue.Name = "colCompValue";
            this.colCompValue.Width = 150;
            this.colCompUnit.HeaderText = "单位";
            this.colCompUnit.Name = "colCompUnit";
            this.colCompUnit.Width = 80;
            this.colCompDesc.HeaderText = "说明";
            this.colCompDesc.Name = "colCompDesc";
            this.colCompDesc.Width = 450;

            // lblCompStatus / txtCompStatus
            this.lblCompStatus.AutoSize = true;
            this.lblCompStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblCompStatus.Location = new System.Drawing.Point(8, 575);
            this.lblCompStatus.Text = "校核结果:";
            this.txtCompStatus.Location = new System.Drawing.Point(80, 572);
            this.txtCompStatus.Size = new System.Drawing.Size(885, 50);
            this.txtCompStatus.Multiline = true;
            this.txtCompStatus.ReadOnly = true;
            this.txtCompStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // ============================================================
            // tabExtension - 拉伸弹簧设计
            // ============================================================
            this.tabExtension.Controls.Add(this.grpExtResult);
            this.tabExtension.Controls.Add(this.grpExtInput);
            this.tabExtension.Controls.Add(this.txtExtStatus);
            this.tabExtension.Controls.Add(this.lblExtStatus);
            this.tabExtension.Location = new System.Drawing.Point(4, 29);
            this.tabExtension.Name = "tabExtension";
            this.tabExtension.Padding = new System.Windows.Forms.Padding(3);
            this.tabExtension.Size = new System.Drawing.Size(976, 628);
            this.tabExtension.TabIndex = 1;
            this.tabExtension.Text = "拉伸弹簧设计";
            this.tabExtension.UseVisualStyleBackColor = true;

            // grpExtInput
            this.grpExtInput.Controls.Add(this.lblExtMaterial);
            this.grpExtInput.Controls.Add(this.cboExtMaterial);
            this.grpExtInput.Controls.Add(this.lblExtLoadCat);
            this.grpExtInput.Controls.Add(this.cboExtLoadCat);
            this.grpExtInput.Controls.Add(this.lblExtD);
            this.grpExtInput.Controls.Add(this.txtExtD);
            this.grpExtInput.Controls.Add(this.lblExtDUnit);
            this.grpExtInput.Controls.Add(this.lblExtDm);
            this.grpExtInput.Controls.Add(this.txtExtDm);
            this.grpExtInput.Controls.Add(this.lblExtDmUnit);
            this.grpExtInput.Controls.Add(this.lblExtN);
            this.grpExtInput.Controls.Add(this.txtExtN);
            this.grpExtInput.Controls.Add(this.lblExtF1);
            this.grpExtInput.Controls.Add(this.txtExtF1);
            this.grpExtInput.Controls.Add(this.lblExtF1Unit);
            this.grpExtInput.Controls.Add(this.lblExtFn);
            this.grpExtInput.Controls.Add(this.txtExtFn);
            this.grpExtInput.Controls.Add(this.lblExtFnUnit);
            this.grpExtInput.Controls.Add(this.lblExtInitTension);
            this.grpExtInput.Controls.Add(this.txtExtInitTension);
            this.grpExtInput.Controls.Add(this.lblExtInitTensionUnit);
            this.grpExtInput.Controls.Add(this.btnExtCalc);
            this.grpExtInput.Controls.Add(this.btnExtReset);
            this.grpExtInput.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.grpExtInput.Location = new System.Drawing.Point(8, 6);
            this.grpExtInput.Name = "grpExtInput";
            this.grpExtInput.Size = new System.Drawing.Size(960, 160);
            this.grpExtInput.TabIndex = 0;
            this.grpExtInput.TabStop = false;
            this.grpExtInput.Text = "设计参数";

            // Row 1
            this.lblExtMaterial.AutoSize = true;
            this.lblExtMaterial.Location = new System.Drawing.Point(15, y0);
            this.lblExtMaterial.Text = "材料:";
            this.cboExtMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExtMaterial.Location = new System.Drawing.Point(65, y0 - 3);
            this.cboExtMaterial.Size = new System.Drawing.Size(150, 25);

            this.lblExtLoadCat.AutoSize = true;
            this.lblExtLoadCat.Location = new System.Drawing.Point(230, y0);
            this.lblExtLoadCat.Text = "载荷类别:";
            this.cboExtLoadCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExtLoadCat.Location = new System.Drawing.Point(295, y0 - 3);
            this.cboExtLoadCat.Size = new System.Drawing.Size(200, 25);
            this.cboExtLoadCat.Items.AddRange(new object[] {
                "I类载荷 (N<10000)",
                "II类载荷 (10000<=N<1000000)",
                "III类载荷 (N<=1000000)"
            });

            this.lblExtInitTension.AutoSize = true;
            this.lblExtInitTension.Location = new System.Drawing.Point(515, y0);
            this.lblExtInitTension.Text = "初拉力 F0:";
            this.txtExtInitTension.Location = new System.Drawing.Point(600, y0 - 3);
            this.txtExtInitTension.Size = new System.Drawing.Size(80, 23);
            this.lblExtInitTensionUnit.AutoSize = true;
            this.lblExtInitTensionUnit.Location = new System.Drawing.Point(683, y0);
            this.lblExtInitTensionUnit.Text = "N";

            // Row 2
            this.lblExtD.AutoSize = true;
            this.lblExtD.Location = new System.Drawing.Point(15, y1);
            this.lblExtD.Text = "丝径 d:";
            this.txtExtD.Location = new System.Drawing.Point(75, y1 - 3);
            this.txtExtD.Size = new System.Drawing.Size(80, 23);
            this.lblExtDUnit.AutoSize = true;
            this.lblExtDUnit.Location = new System.Drawing.Point(158, y1);
            this.lblExtDUnit.Text = "mm";

            this.lblExtDm.AutoSize = true;
            this.lblExtDm.Location = new System.Drawing.Point(200, y1);
            this.lblExtDm.Text = "中径 D:";
            this.txtExtDm.Location = new System.Drawing.Point(260, y1 - 3);
            this.txtExtDm.Size = new System.Drawing.Size(80, 23);
            this.lblExtDmUnit.AutoSize = true;
            this.lblExtDmUnit.Location = new System.Drawing.Point(343, y1);
            this.lblExtDmUnit.Text = "mm";

            this.lblExtN.AutoSize = true;
            this.lblExtN.Location = new System.Drawing.Point(390, y1);
            this.lblExtN.Text = "有效圈数 n:";
            this.txtExtN.Location = new System.Drawing.Point(490, y1 - 3);
            this.txtExtN.Size = new System.Drawing.Size(60, 23);

            // Row 3
            this.lblExtF1.AutoSize = true;
            this.lblExtF1.Location = new System.Drawing.Point(15, y2);
            this.lblExtF1.Text = "最小工作载荷 P1:";
            this.txtExtF1.Location = new System.Drawing.Point(140, y2 - 3);
            this.txtExtF1.Size = new System.Drawing.Size(80, 23);
            this.lblExtF1Unit.AutoSize = true;
            this.lblExtF1Unit.Location = new System.Drawing.Point(223, y2);
            this.lblExtF1Unit.Text = "N";

            this.lblExtFn.AutoSize = true;
            this.lblExtFn.Location = new System.Drawing.Point(260, y2);
            this.lblExtFn.Text = "最大工作载荷 Pn:";
            this.txtExtFn.Location = new System.Drawing.Point(385, y2 - 3);
            this.txtExtFn.Size = new System.Drawing.Size(80, 23);
            this.lblExtFnUnit.AutoSize = true;
            this.lblExtFnUnit.Location = new System.Drawing.Point(468, y2);
            this.lblExtFnUnit.Text = "N";

            this.btnExtCalc.Location = new System.Drawing.Point(570, y2 - 5);
            this.btnExtCalc.Size = new System.Drawing.Size(90, 30);
            this.btnExtCalc.Text = "计算";
            this.btnExtCalc.UseVisualStyleBackColor = true;
            this.btnExtCalc.Click += new System.EventHandler(this.btnExtCalc_Click);

            this.btnExtReset.Location = new System.Drawing.Point(670, y2 - 5);
            this.btnExtReset.Size = new System.Drawing.Size(90, 30);
            this.btnExtReset.Text = "重置";
            this.btnExtReset.UseVisualStyleBackColor = true;
            this.btnExtReset.Click += new System.EventHandler(this.btnExtReset_Click);

            // grpExtResult
            this.grpExtResult.Controls.Add(this.dgvExtResult);
            this.grpExtResult.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.grpExtResult.Location = new System.Drawing.Point(8, 170);
            this.grpExtResult.Name = "grpExtResult";
            this.grpExtResult.Size = new System.Drawing.Size(960, 400);
            this.grpExtResult.TabIndex = 1;
            this.grpExtResult.TabStop = false;
            this.grpExtResult.Text = "计算结果";

            this.dgvExtResult.AllowUserToAddRows = false;
            this.dgvExtResult.AllowUserToDeleteRows = false;
            this.dgvExtResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExtResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colExtParam, this.colExtValue, this.colExtUnit, this.colExtDesc
            });
            this.dgvExtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtResult.Location = new System.Drawing.Point(3, 19);
            this.dgvExtResult.Name = "dgvExtResult";
            this.dgvExtResult.ReadOnly = true;
            this.dgvExtResult.RowHeadersVisible = false;
            this.dgvExtResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.colExtParam.HeaderText = "参数名称";
            this.colExtParam.Name = "colExtParam";
            this.colExtParam.Width = 250;
            this.colExtValue.HeaderText = "数值";
            this.colExtValue.Name = "colExtValue";
            this.colExtValue.Width = 150;
            this.colExtUnit.HeaderText = "单位";
            this.colExtUnit.Name = "colExtUnit";
            this.colExtUnit.Width = 80;
            this.colExtDesc.HeaderText = "说明";
            this.colExtDesc.Name = "colExtDesc";
            this.colExtDesc.Width = 450;

            this.lblExtStatus.AutoSize = true;
            this.lblExtStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblExtStatus.Location = new System.Drawing.Point(8, 575);
            this.lblExtStatus.Text = "校核结果:";
            this.txtExtStatus.Location = new System.Drawing.Point(80, 572);
            this.txtExtStatus.Size = new System.Drawing.Size(885, 50);
            this.txtExtStatus.Multiline = true;
            this.txtExtStatus.ReadOnly = true;
            this.txtExtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // ============================================================
            // tabTorsion - 扭转弹簧设计
            // ============================================================
            this.tabTorsion.Controls.Add(this.grpTorResult);
            this.tabTorsion.Controls.Add(this.grpTorInput);
            this.tabTorsion.Controls.Add(this.txtTorStatus);
            this.tabTorsion.Controls.Add(this.lblTorStatus);
            this.tabTorsion.Location = new System.Drawing.Point(4, 29);
            this.tabTorsion.Name = "tabTorsion";
            this.tabTorsion.Padding = new System.Windows.Forms.Padding(3);
            this.tabTorsion.Size = new System.Drawing.Size(976, 628);
            this.tabTorsion.TabIndex = 2;
            this.tabTorsion.Text = "扭转弹簧设计";
            this.tabTorsion.UseVisualStyleBackColor = true;

            // grpTorInput
            this.grpTorInput.Controls.Add(this.lblTorMaterial);
            this.grpTorInput.Controls.Add(this.cboTorMaterial);
            this.grpTorInput.Controls.Add(this.lblTorLoadCat);
            this.grpTorInput.Controls.Add(this.cboTorLoadCat);
            this.grpTorInput.Controls.Add(this.lblTorD);
            this.grpTorInput.Controls.Add(this.txtTorD);
            this.grpTorInput.Controls.Add(this.lblTorDUnit);
            this.grpTorInput.Controls.Add(this.lblTorDm);
            this.grpTorInput.Controls.Add(this.txtTorDm);
            this.grpTorInput.Controls.Add(this.lblTorDmUnit);
            this.grpTorInput.Controls.Add(this.lblTorN);
            this.grpTorInput.Controls.Add(this.txtTorN);
            this.grpTorInput.Controls.Add(this.lblTorM);
            this.grpTorInput.Controls.Add(this.txtTorM);
            this.grpTorInput.Controls.Add(this.lblTorMUnit);
            this.grpTorInput.Controls.Add(this.btnTorCalc);
            this.grpTorInput.Controls.Add(this.btnTorReset);
            this.grpTorInput.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.grpTorInput.Location = new System.Drawing.Point(8, 6);
            this.grpTorInput.Name = "grpTorInput";
            this.grpTorInput.Size = new System.Drawing.Size(960, 140);
            this.grpTorInput.TabIndex = 0;
            this.grpTorInput.TabStop = false;
            this.grpTorInput.Text = "设计参数";

            // Row 1
            this.lblTorMaterial.AutoSize = true;
            this.lblTorMaterial.Location = new System.Drawing.Point(15, y0);
            this.lblTorMaterial.Text = "材料:";
            this.cboTorMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTorMaterial.Location = new System.Drawing.Point(65, y0 - 3);
            this.cboTorMaterial.Size = new System.Drawing.Size(150, 25);

            this.lblTorLoadCat.AutoSize = true;
            this.lblTorLoadCat.Location = new System.Drawing.Point(230, y0);
            this.lblTorLoadCat.Text = "载荷类别:";
            this.cboTorLoadCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTorLoadCat.Location = new System.Drawing.Point(295, y0 - 3);
            this.cboTorLoadCat.Size = new System.Drawing.Size(200, 25);
            this.cboTorLoadCat.Items.AddRange(new object[] {
                "I类载荷 (N<10000)",
                "II类载荷 (10000<=N<1000000)",
                "III类载荷 (N<=1000000)"
            });

            // Row 2
            this.lblTorD.AutoSize = true;
            this.lblTorD.Location = new System.Drawing.Point(15, y1);
            this.lblTorD.Text = "丝径 d:";
            this.txtTorD.Location = new System.Drawing.Point(75, y1 - 3);
            this.txtTorD.Size = new System.Drawing.Size(80, 23);
            this.lblTorDUnit.AutoSize = true;
            this.lblTorDUnit.Location = new System.Drawing.Point(158, y1);
            this.lblTorDUnit.Text = "mm";

            this.lblTorDm.AutoSize = true;
            this.lblTorDm.Location = new System.Drawing.Point(200, y1);
            this.lblTorDm.Text = "中径 D:";
            this.txtTorDm.Location = new System.Drawing.Point(260, y1 - 3);
            this.txtTorDm.Size = new System.Drawing.Size(80, 23);
            this.lblTorDmUnit.AutoSize = true;
            this.lblTorDmUnit.Location = new System.Drawing.Point(343, y1);
            this.lblTorDmUnit.Text = "mm";

            this.lblTorN.AutoSize = true;
            this.lblTorN.Location = new System.Drawing.Point(390, y1);
            this.lblTorN.Text = "有效圈数 n:";
            this.txtTorN.Location = new System.Drawing.Point(490, y1 - 3);
            this.txtTorN.Size = new System.Drawing.Size(60, 23);

            this.lblTorM.AutoSize = true;
            this.lblTorM.Location = new System.Drawing.Point(570, y1);
            this.lblTorM.Text = "工作扭矩 M:";
            this.txtTorM.Location = new System.Drawing.Point(670, y1 - 3);
            this.txtTorM.Size = new System.Drawing.Size(80, 23);
            this.lblTorMUnit.AutoSize = true;
            this.lblTorMUnit.Location = new System.Drawing.Point(753, y1);
            this.lblTorMUnit.Text = "N*mm";

            // Row 3 buttons
            this.btnTorCalc.Location = new System.Drawing.Point(300, y2 - 5);
            this.btnTorCalc.Size = new System.Drawing.Size(90, 30);
            this.btnTorCalc.Text = "计算";
            this.btnTorCalc.UseVisualStyleBackColor = true;
            this.btnTorCalc.Click += new System.EventHandler(this.btnTorCalc_Click);

            this.btnTorReset.Location = new System.Drawing.Point(410, y2 - 5);
            this.btnTorReset.Size = new System.Drawing.Size(90, 30);
            this.btnTorReset.Text = "重置";
            this.btnTorReset.UseVisualStyleBackColor = true;
            this.btnTorReset.Click += new System.EventHandler(this.btnTorReset_Click);

            // grpTorResult
            this.grpTorResult.Controls.Add(this.dgvTorResult);
            this.grpTorResult.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.grpTorResult.Location = new System.Drawing.Point(8, 150);
            this.grpTorResult.Name = "grpTorResult";
            this.grpTorResult.Size = new System.Drawing.Size(960, 420);
            this.grpTorResult.TabIndex = 1;
            this.grpTorResult.TabStop = false;
            this.grpTorResult.Text = "计算结果";

            this.dgvTorResult.AllowUserToAddRows = false;
            this.dgvTorResult.AllowUserToDeleteRows = false;
            this.dgvTorResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTorResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTorResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colTorParam, this.colTorValue, this.colTorUnit, this.colTorDesc
            });
            this.dgvTorResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTorResult.Location = new System.Drawing.Point(3, 19);
            this.dgvTorResult.Name = "dgvTorResult";
            this.dgvTorResult.ReadOnly = true;
            this.dgvTorResult.RowHeadersVisible = false;
            this.dgvTorResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.colTorParam.HeaderText = "参数名称";
            this.colTorParam.Name = "colTorParam";
            this.colTorParam.Width = 250;
            this.colTorValue.HeaderText = "数值";
            this.colTorValue.Name = "colTorValue";
            this.colTorValue.Width = 150;
            this.colTorUnit.HeaderText = "单位";
            this.colTorUnit.Name = "colTorUnit";
            this.colTorUnit.Width = 80;
            this.colTorDesc.HeaderText = "说明";
            this.colTorDesc.Name = "colTorDesc";
            this.colTorDesc.Width = 450;

            this.lblTorStatus.AutoSize = true;
            this.lblTorStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblTorStatus.Location = new System.Drawing.Point(8, 575);
            this.lblTorStatus.Text = "校核结果:";
            this.txtTorStatus.Location = new System.Drawing.Point(80, 572);
            this.txtTorStatus.Size = new System.Drawing.Size(885, 50);
            this.txtTorStatus.Multiline = true;
            this.txtTorStatus.ReadOnly = true;
            this.txtTorStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // ============================================================
            // MainForm
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.tabMain);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "迈迪弹簧设计系统";
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.tabMain.ResumeLayout(false);
            this.tabCompression.ResumeLayout(false);
            this.tabCompression.PerformLayout();
            this.tabExtension.ResumeLayout(false);
            this.tabExtension.PerformLayout();
            this.tabTorsion.ResumeLayout(false);
            this.tabTorsion.PerformLayout();

            this.grpCompInput.ResumeLayout(false);
            this.grpCompInput.PerformLayout();
            this.grpCompResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompResult)).EndInit();

            this.grpExtInput.ResumeLayout(false);
            this.grpExtInput.PerformLayout();
            this.grpExtResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtResult)).EndInit();

            this.grpTorInput.ResumeLayout(false);
            this.grpTorInput.PerformLayout();
            this.grpTorResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTorResult)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion

        // 主选项卡
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabCompression;
        private System.Windows.Forms.TabPage tabExtension;
        private System.Windows.Forms.TabPage tabTorsion;

        // ============ 压缩弹簧 ============
        private System.Windows.Forms.GroupBox grpCompInput;
        private System.Windows.Forms.Label lblCompMaterial;
        private System.Windows.Forms.ComboBox cboCompMaterial;
        private System.Windows.Forms.Label lblCompLoadCat;
        private System.Windows.Forms.ComboBox cboCompLoadCat;
        private System.Windows.Forms.Label lblCompD;
        private System.Windows.Forms.TextBox txtCompD;
        private System.Windows.Forms.Label lblCompDUnit;
        private System.Windows.Forms.Label lblCompDm;
        private System.Windows.Forms.TextBox txtCompDm;
        private System.Windows.Forms.Label lblCompDmUnit;
        private System.Windows.Forms.Label lblCompN;
        private System.Windows.Forms.TextBox txtCompN;
        private System.Windows.Forms.Label lblCompF1;
        private System.Windows.Forms.TextBox txtCompF1;
        private System.Windows.Forms.Label lblCompF1Unit;
        private System.Windows.Forms.Label lblCompFn;
        private System.Windows.Forms.TextBox txtCompFn;
        private System.Windows.Forms.Label lblCompFnUnit;
        private System.Windows.Forms.Label lblCompEndType;
        private System.Windows.Forms.ComboBox cboCompEndType;
        private System.Windows.Forms.Button btnCompCalc;
        private System.Windows.Forms.Button btnCompReset;
        private System.Windows.Forms.GroupBox grpCompResult;
        private System.Windows.Forms.DataGridView dgvCompResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompDesc;
        private System.Windows.Forms.Label lblCompStatus;
        private System.Windows.Forms.TextBox txtCompStatus;

        // ============ 拉伸弹簧 ============
        private System.Windows.Forms.GroupBox grpExtInput;
        private System.Windows.Forms.Label lblExtMaterial;
        private System.Windows.Forms.ComboBox cboExtMaterial;
        private System.Windows.Forms.Label lblExtLoadCat;
        private System.Windows.Forms.ComboBox cboExtLoadCat;
        private System.Windows.Forms.Label lblExtD;
        private System.Windows.Forms.TextBox txtExtD;
        private System.Windows.Forms.Label lblExtDUnit;
        private System.Windows.Forms.Label lblExtDm;
        private System.Windows.Forms.TextBox txtExtDm;
        private System.Windows.Forms.Label lblExtDmUnit;
        private System.Windows.Forms.Label lblExtN;
        private System.Windows.Forms.TextBox txtExtN;
        private System.Windows.Forms.Label lblExtF1;
        private System.Windows.Forms.TextBox txtExtF1;
        private System.Windows.Forms.Label lblExtF1Unit;
        private System.Windows.Forms.Label lblExtFn;
        private System.Windows.Forms.TextBox txtExtFn;
        private System.Windows.Forms.Label lblExtFnUnit;
        private System.Windows.Forms.Label lblExtInitTension;
        private System.Windows.Forms.TextBox txtExtInitTension;
        private System.Windows.Forms.Label lblExtInitTensionUnit;
        private System.Windows.Forms.Button btnExtCalc;
        private System.Windows.Forms.Button btnExtReset;
        private System.Windows.Forms.GroupBox grpExtResult;
        private System.Windows.Forms.DataGridView dgvExtResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExtParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExtValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExtUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExtDesc;
        private System.Windows.Forms.Label lblExtStatus;
        private System.Windows.Forms.TextBox txtExtStatus;

        // ============ 扭转弹簧 ============
        private System.Windows.Forms.GroupBox grpTorInput;
        private System.Windows.Forms.Label lblTorMaterial;
        private System.Windows.Forms.ComboBox cboTorMaterial;
        private System.Windows.Forms.Label lblTorLoadCat;
        private System.Windows.Forms.ComboBox cboTorLoadCat;
        private System.Windows.Forms.Label lblTorD;
        private System.Windows.Forms.TextBox txtTorD;
        private System.Windows.Forms.Label lblTorDUnit;
        private System.Windows.Forms.Label lblTorDm;
        private System.Windows.Forms.TextBox txtTorDm;
        private System.Windows.Forms.Label lblTorDmUnit;
        private System.Windows.Forms.Label lblTorN;
        private System.Windows.Forms.TextBox txtTorN;
        private System.Windows.Forms.Label lblTorM;
        private System.Windows.Forms.TextBox txtTorM;
        private System.Windows.Forms.Label lblTorMUnit;
        private System.Windows.Forms.Button btnTorCalc;
        private System.Windows.Forms.Button btnTorReset;
        private System.Windows.Forms.GroupBox grpTorResult;
        private System.Windows.Forms.DataGridView dgvTorResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTorParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTorValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTorUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTorDesc;
        private System.Windows.Forms.Label lblTorStatus;
        private System.Windows.Forms.TextBox txtTorStatus;
    }
}
