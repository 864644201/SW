namespace InquireTolerance
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

            // ===== 顶部面板 - 输入区域 =====
            this.panelTop = new System.Windows.Forms.Panel();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.lblNominalSize = new System.Windows.Forms.Label();
            this.txtNominalSize = new System.Windows.Forms.TextBox();
            this.lblSizeUnit = new System.Windows.Forms.Label();
            this.lblDeviationType = new System.Windows.Forms.Label();
            this.radHole = new System.Windows.Forms.RadioButton();
            this.radShaft = new System.Windows.Forms.RadioButton();
            this.lblDeviationCode = new System.Windows.Forms.Label();
            this.cmbDeviationCode = new System.Windows.Forms.ComboBox();
            this.lblITGrade = new System.Windows.Forms.Label();
            this.cmbITGrade = new System.Windows.Forms.ComboBox();
            this.btnCalculate = new System.Windows.Forms.Button();

            // ===== 中部面板 - 结果显示 =====
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.lblESCaption = new System.Windows.Forms.Label();
            this.txtES = new System.Windows.Forms.TextBox();
            this.lblEICaption = new System.Windows.Forms.Label();
            this.txtEI = new System.Windows.Forms.TextBox();
            this.lblToleranceCaption = new System.Windows.Forms.Label();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.lblToleranceUnit = new System.Windows.Forms.Label();

            // ===== 配合计算面板 =====
            this.grpFit = new System.Windows.Forms.GroupBox();
            this.lblFitHole = new System.Windows.Forms.Label();
            this.txtFitHoleCode = new System.Windows.Forms.TextBox();
            this.lblFitHoleGrade = new System.Windows.Forms.Label();
            this.txtFitHoleGrade = new System.Windows.Forms.TextBox();
            this.lblFitShaft = new System.Windows.Forms.Label();
            this.txtFitShaftCode = new System.Windows.Forms.TextBox();
            this.lblFitShaftGrade = new System.Windows.Forms.Label();
            this.txtFitShaftGrade = new System.Windows.Forms.TextBox();
            this.btnFitCalculate = new System.Windows.Forms.Button();
            this.lblFitResultCaption = new System.Windows.Forms.Label();
            this.txtFitResult = new System.Windows.Forms.TextBox();

            // ===== 公差表面板 =====
            this.panelBottom = new System.Windows.Forms.Panel();
            this.grpTable = new System.Windows.Forms.GroupBox();
            this.dgvToleranceTable = new System.Windows.Forms.DataGridView();
            this.btnShowTable = new System.Windows.Forms.Button();
            this.cmbTableGrade = new System.Windows.Forms.ComboBox();
            this.lblTableGrade = new System.Windows.Forms.Label();

            // ===== 状态栏 =====
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();

            this.panelTop.SuspendLayout();
            this.grpInput.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.grpFit.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.grpTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToleranceTable)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // ===== panelTop =====
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(8);
            this.panelTop.Size = new System.Drawing.Size(784, 120);

            // ===== grpInput =====
            this.grpInput.Controls.Add(this.lblNominalSize);
            this.grpInput.Controls.Add(this.txtNominalSize);
            this.grpInput.Controls.Add(this.lblSizeUnit);
            this.grpInput.Controls.Add(this.lblDeviationType);
            this.grpInput.Controls.Add(this.radHole);
            this.grpInput.Controls.Add(this.radShaft);
            this.grpInput.Controls.Add(this.lblDeviationCode);
            this.grpInput.Controls.Add(this.cmbDeviationCode);
            this.grpInput.Controls.Add(this.lblITGrade);
            this.grpInput.Controls.Add(this.cmbITGrade);
            this.grpInput.Controls.Add(this.btnCalculate);
            this.grpInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInput.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.grpInput.Name = "grpInput";
            this.grpInput.Text = "基本参数输入";
            this.grpInput.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            // lblNominalSize
            this.lblNominalSize.AutoSize = true;
            this.lblNominalSize.Location = new System.Drawing.Point(15, 30);
            this.lblNominalSize.Name = "lblNominalSize";
            this.lblNominalSize.Text = "基本尺寸:";

            // txtNominalSize
            this.txtNominalSize.Location = new System.Drawing.Point(90, 27);
            this.txtNominalSize.Name = "txtNominalSize";
            this.txtNominalSize.Size = new System.Drawing.Size(80, 23);
            this.txtNominalSize.Text = "50";

            // lblSizeUnit
            this.lblSizeUnit.AutoSize = true;
            this.lblSizeUnit.Location = new System.Drawing.Point(175, 30);
            this.lblSizeUnit.Name = "lblSizeUnit";
            this.lblSizeUnit.Text = "mm";

            // lblDeviationType
            this.lblDeviationType.AutoSize = true;
            this.lblDeviationType.Location = new System.Drawing.Point(215, 30);
            this.lblDeviationType.Name = "lblDeviationType";
            this.lblDeviationType.Text = "类型:";

            // radHole
            this.radHole.AutoSize = true;
            this.radHole.Checked = true;
            this.radHole.Location = new System.Drawing.Point(255, 28);
            this.radHole.Name = "radHole";
            this.radHole.Size = new System.Drawing.Size(47, 21);
            this.radHole.Text = "孔";
            this.radHole.CheckedChanged += new System.EventHandler(this.RadType_CheckedChanged);

            // radShaft
            this.radShaft.AutoSize = true;
            this.radShaft.Location = new System.Drawing.Point(310, 28);
            this.radShaft.Name = "radShaft";
            this.radShaft.Size = new System.Drawing.Size(47, 21);
            this.radShaft.Text = "轴";
            this.radShaft.CheckedChanged += new System.EventHandler(this.RadType_CheckedChanged);

            // lblDeviationCode
            this.lblDeviationCode.AutoSize = true;
            this.lblDeviationCode.Location = new System.Drawing.Point(375, 30);
            this.lblDeviationCode.Name = "lblDeviationCode";
            this.lblDeviationCode.Text = "偏差代号:";

            // cmbDeviationCode
            this.cmbDeviationCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviationCode.Location = new System.Drawing.Point(450, 27);
            this.cmbDeviationCode.Name = "cmbDeviationCode";
            this.cmbDeviationCode.Size = new System.Drawing.Size(60, 25);

            // lblITGrade
            this.lblITGrade.AutoSize = true;
            this.lblITGrade.Location = new System.Drawing.Point(525, 30);
            this.lblITGrade.Name = "lblITGrade";
            this.lblITGrade.Text = "公差等级:";

            // cmbITGrade
            this.cmbITGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbITGrade.Location = new System.Drawing.Point(600, 27);
            this.cmbITGrade.Name = "cmbITGrade";
            this.cmbITGrade.Size = new System.Drawing.Size(70, 25);

            // btnCalculate
            this.btnCalculate.Location = new System.Drawing.Point(685, 25);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 28);
            this.btnCalculate.Text = "查询";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);

            // ===== 第二行: 快速配合查询 =====
            this.lblFitHole.AutoSize = true;
            this.lblFitHole.Location = new System.Drawing.Point(15, 70);
            this.lblFitHole.Name = "lblFitHole";
            this.lblFitHole.Text = "孔:";

            this.txtFitHoleCode.Location = new System.Drawing.Point(40, 67);
            this.txtFitHoleCode.Name = "txtFitHoleCode";
            this.txtFitHoleCode.Size = new System.Drawing.Size(35, 23);
            this.txtFitHoleCode.Text = "H";

            this.lblFitHoleGrade.AutoSize = true;
            this.lblFitHoleGrade.Location = new System.Drawing.Point(78, 70);
            this.lblFitHoleGrade.Name = "lblFitHoleGrade";

            this.txtFitHoleGrade.Location = new System.Drawing.Point(80, 67);
            this.txtFitHoleGrade.Name = "txtFitHoleGrade";
            this.txtFitHoleGrade.Size = new System.Drawing.Size(25, 23);
            this.txtFitHoleGrade.Text = "7";

            this.lblFitShaft.AutoSize = true;
            this.lblFitShaft.Location = new System.Drawing.Point(120, 70);
            this.lblFitShaft.Name = "lblFitShaft";
            this.lblFitShaft.Text = "轴:";

            this.txtFitShaftCode.Location = new System.Drawing.Point(145, 67);
            this.txtFitShaftCode.Name = "txtFitShaftCode";
            this.txtFitShaftCode.Size = new System.Drawing.Size(35, 23);
            this.txtFitShaftCode.Text = "g";

            this.lblFitShaftGrade.AutoSize = true;
            this.lblFitShaftGrade.Location = new System.Drawing.Point(183, 70);
            this.lblFitShaftGrade.Name = "lblFitShaftGrade";

            this.txtFitShaftGrade.Location = new System.Drawing.Point(185, 67);
            this.txtFitShaftGrade.Name = "txtFitShaftGrade";
            this.txtFitShaftGrade.Size = new System.Drawing.Size(25, 23);
            this.txtFitShaftGrade.Text = "6";

            this.btnFitCalculate.Location = new System.Drawing.Point(225, 65);
            this.btnFitCalculate.Name = "btnFitCalculate";
            this.btnFitCalculate.Size = new System.Drawing.Size(80, 28);
            this.btnFitCalculate.Text = "配合计算";
            this.btnFitCalculate.UseVisualStyleBackColor = true;
            this.btnFitCalculate.Click += new System.EventHandler(this.BtnFitCalculate_Click);

            // ===== panelMiddle =====
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMiddle.Location = new System.Drawing.Point(0, 120);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.panelMiddle.Size = new System.Drawing.Size(784, 341);

            // ===== grpResult =====
            this.grpResult.Controls.Add(this.lblESCaption);
            this.grpResult.Controls.Add(this.txtES);
            this.grpResult.Controls.Add(this.lblEICaption);
            this.grpResult.Controls.Add(this.txtEI);
            this.grpResult.Controls.Add(this.lblToleranceCaption);
            this.grpResult.Controls.Add(this.txtTolerance);
            this.grpResult.Controls.Add(this.lblToleranceUnit);
            this.grpResult.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.grpResult.Location = new System.Drawing.Point(8, 5);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(300, 120);
            this.grpResult.Text = "公差查询结果";

            // lblESCaption
            this.lblESCaption.AutoSize = true;
            this.lblESCaption.Location = new System.Drawing.Point(15, 30);
            this.lblESCaption.Name = "lblESCaption";
            this.lblESCaption.Text = "上偏差 (ES):";

            // txtES
            this.txtES.Location = new System.Drawing.Point(110, 27);
            this.txtES.Name = "txtES";
            this.txtES.ReadOnly = true;
            this.txtES.Size = new System.Drawing.Size(100, 23);
            this.txtES.BackColor = System.Drawing.Color.White;
            this.txtES.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // lblEICaption
            this.lblEICaption.AutoSize = true;
            this.lblEICaption.Location = new System.Drawing.Point(15, 60);
            this.lblEICaption.Name = "lblEICaption";
            this.lblEICaption.Text = "下偏差 (EI):";

            // txtEI
            this.txtEI.Location = new System.Drawing.Point(110, 57);
            this.txtEI.Name = "txtEI";
            this.txtEI.ReadOnly = true;
            this.txtEI.Size = new System.Drawing.Size(100, 23);
            this.txtEI.BackColor = System.Drawing.Color.White;
            this.txtEI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // lblToleranceCaption
            this.lblToleranceCaption.AutoSize = true;
            this.lblToleranceCaption.Location = new System.Drawing.Point(15, 90);
            this.lblToleranceCaption.Name = "lblToleranceCaption";
            this.lblToleranceCaption.Text = "公差值 (T):";

            // txtTolerance
            this.txtTolerance.Location = new System.Drawing.Point(110, 87);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.ReadOnly = true;
            this.txtTolerance.Size = new System.Drawing.Size(100, 23);
            this.txtTolerance.BackColor = System.Drawing.Color.White;
            this.txtTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // lblToleranceUnit
            this.lblToleranceUnit.AutoSize = true;
            this.lblToleranceUnit.Location = new System.Drawing.Point(215, 90);
            this.lblToleranceUnit.Name = "lblToleranceUnit";
            this.lblToleranceUnit.Text = "mm";

            // ===== grpFit =====
            this.grpFit.Controls.Add(this.txtFitResult);
            this.grpFit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.grpFit.Location = new System.Drawing.Point(320, 5);
            this.grpFit.Name = "grpFit";
            this.grpFit.Size = new System.Drawing.Size(456, 120);
            this.grpFit.Text = "配合计算结果";

            // txtFitResult
            this.txtFitResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFitResult.Location = new System.Drawing.Point(10, 21);
            this.txtFitResult.Multiline = true;
            this.txtFitResult.Name = "txtFitResult";
            this.txtFitResult.ReadOnly = true;
            this.txtFitResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFitResult.BackColor = System.Drawing.Color.White;
            this.txtFitResult.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);

            // ===== panelBottom =====
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 135);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.panelBottom.Size = new System.Drawing.Size(784, 206);

            // ===== grpTable =====
            this.grpTable.Controls.Add(this.dgvToleranceTable);
            this.grpTable.Controls.Add(this.btnShowTable);
            this.grpTable.Controls.Add(this.cmbTableGrade);
            this.grpTable.Controls.Add(this.lblTableGrade);
            this.grpTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTable.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.grpTable.Name = "grpTable";
            this.grpTable.Text = "公差速查表";

            // lblTableGrade
            this.lblTableGrade.AutoSize = true;
            this.lblTableGrade.Location = new System.Drawing.Point(15, 25);
            this.lblTableGrade.Name = "lblTableGrade";
            this.lblTableGrade.Text = "公差等级:";

            // cmbTableGrade
            this.cmbTableGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTableGrade.Location = new System.Drawing.Point(85, 22);
            this.cmbTableGrade.Name = "cmbTableGrade";
            this.cmbTableGrade.Size = new System.Drawing.Size(70, 25);

            // btnShowTable
            this.btnShowTable.Location = new System.Drawing.Point(170, 20);
            this.btnShowTable.Name = "btnShowTable";
            this.btnShowTable.Size = new System.Drawing.Size(90, 28);
            this.btnShowTable.Text = "刷新表格";
            this.btnShowTable.UseVisualStyleBackColor = true;
            this.btnShowTable.Click += new System.EventHandler(this.BtnShowTable_Click);

            // dgvToleranceTable
            this.dgvToleranceTable.AllowUserToAddRows = false;
            this.dgvToleranceTable.AllowUserToDeleteRows = false;
            this.dgvToleranceTable.Location = new System.Drawing.Point(10, 55);
            this.dgvToleranceTable.Name = "dgvToleranceTable";
            this.dgvToleranceTable.ReadOnly = true;
            this.dgvToleranceTable.RowHeadersVisible = false;
            this.dgvToleranceTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvToleranceTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvToleranceTable.Size = new System.Drawing.Size(756, 140);
            this.dgvToleranceTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvToleranceTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // ===== statusStrip =====
            this.statusStrip.Items.Add(this.lblStatus);
            this.statusStrip.Name = "statusStrip";

            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "就绪";

            // ===== MainForm =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "MainForm";
            this.Text = "迈迪公差查询 - ISO 286";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelTop.ResumeLayout(false);
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            this.panelMiddle.ResumeLayout(false);
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            this.grpFit.ResumeLayout(false);
            this.grpFit.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.grpTable.ResumeLayout(false);
            this.grpTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToleranceTable)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            // 添加控件到父容器
            this.grpInput.Controls.Add(this.lblFitHole);
            this.grpInput.Controls.Add(this.txtFitHoleCode);
            this.grpInput.Controls.Add(this.txtFitHoleGrade);
            this.grpInput.Controls.Add(this.lblFitShaft);
            this.grpInput.Controls.Add(this.txtFitShaftCode);
            this.grpInput.Controls.Add(this.txtFitShaftGrade);
            this.grpInput.Controls.Add(this.btnFitCalculate);

            this.panelTop.Controls.Add(this.grpInput);
            this.panelMiddle.Controls.Add(this.grpResult);
            this.panelMiddle.Controls.Add(this.grpFit);
            this.panelBottom.Controls.Add(this.grpTable);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip);
        }

        #endregion

        // 顶部输入面板
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Label lblNominalSize;
        private System.Windows.Forms.TextBox txtNominalSize;
        private System.Windows.Forms.Label lblSizeUnit;
        private System.Windows.Forms.Label lblDeviationType;
        private System.Windows.Forms.RadioButton radHole;
        private System.Windows.Forms.RadioButton radShaft;
        private System.Windows.Forms.Label lblDeviationCode;
        private System.Windows.Forms.ComboBox cmbDeviationCode;
        private System.Windows.Forms.Label lblITGrade;
        private System.Windows.Forms.ComboBox cmbITGrade;
        private System.Windows.Forms.Button btnCalculate;

        // 配合输入
        private System.Windows.Forms.Label lblFitHole;
        private System.Windows.Forms.TextBox txtFitHoleCode;
        private System.Windows.Forms.Label lblFitHoleGrade;
        private System.Windows.Forms.TextBox txtFitHoleGrade;
        private System.Windows.Forms.Label lblFitShaft;
        private System.Windows.Forms.TextBox txtFitShaftCode;
        private System.Windows.Forms.Label lblFitShaftGrade;
        private System.Windows.Forms.TextBox txtFitShaftGrade;
        private System.Windows.Forms.Button btnFitCalculate;

        // 中部结果面板
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.Label lblESCaption;
        private System.Windows.Forms.TextBox txtES;
        private System.Windows.Forms.Label lblEICaption;
        private System.Windows.Forms.TextBox txtEI;
        private System.Windows.Forms.Label lblToleranceCaption;
        private System.Windows.Forms.TextBox txtTolerance;
        private System.Windows.Forms.Label lblToleranceUnit;

        // 配合结果
        private System.Windows.Forms.GroupBox grpFit;
        private System.Windows.Forms.Label lblFitResultCaption;
        private System.Windows.Forms.TextBox txtFitResult;

        // 底部表格面板
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.GroupBox grpTable;
        private System.Windows.Forms.DataGridView dgvToleranceTable;
        private System.Windows.Forms.Button btnShowTable;
        private System.Windows.Forms.ComboBox cmbTableGrade;
        private System.Windows.Forms.Label lblTableGrade;

        // 状态栏
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}
