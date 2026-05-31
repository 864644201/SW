namespace BevelGearDesign
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
            this.SuspendLayout();

            // ===== 基本参数 GroupBox =====
            this.grpBasic = new System.Windows.Forms.GroupBox();
            this.lblPower = new System.Windows.Forms.Label();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.lblPowerUnit = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.lblSpeedUnit = new System.Windows.Forms.Label();
            this.lblGearRatio = new System.Windows.Forms.Label();
            this.txtGearRatio = new System.Windows.Forms.TextBox();
            this.lblZ1 = new System.Windows.Forms.Label();
            this.txtZ1 = new System.Windows.Forms.TextBox();
            this.lblZ2 = new System.Windows.Forms.Label();
            this.txtZ2 = new System.Windows.Forms.TextBox();

            this.grpBasic.Location = new System.Drawing.Point(12, 12);
            this.grpBasic.Name = "grpBasic";
            this.grpBasic.Size = new System.Drawing.Size(270, 180);
            this.grpBasic.TabIndex = 0;
            this.grpBasic.TabStop = false;
            this.grpBasic.Text = "基本参数";

            // 输入功率
            this.lblPower.AutoSize = true;
            this.lblPower.Location = new System.Drawing.Point(15, 28);
            this.lblPower.Text = "输入功率 P:";
            this.txtPower.Location = new System.Drawing.Point(120, 25);
            this.txtPower.Size = new System.Drawing.Size(80, 21);
            this.txtPower.TabIndex = 0;
            this.lblPowerUnit.AutoSize = true;
            this.lblPowerUnit.Location = new System.Drawing.Point(205, 28);
            this.lblPowerUnit.Text = "kW";

            // 输入转速
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(15, 58);
            this.lblSpeed.Text = "小齿轮转速 n1:";
            this.txtSpeed.Location = new System.Drawing.Point(120, 55);
            this.txtSpeed.Size = new System.Drawing.Size(80, 21);
            this.txtSpeed.TabIndex = 1;
            this.lblSpeedUnit.AutoSize = true;
            this.lblSpeedUnit.Location = new System.Drawing.Point(205, 58);
            this.lblSpeedUnit.Text = "rpm";

            // 齿数比
            this.lblGearRatio.AutoSize = true;
            this.lblGearRatio.Location = new System.Drawing.Point(15, 88);
            this.lblGearRatio.Text = "齿数比 u:";
            this.txtGearRatio.Location = new System.Drawing.Point(120, 85);
            this.txtGearRatio.Size = new System.Drawing.Size(80, 21);
            this.txtGearRatio.TabIndex = 2;

            // Z1
            this.lblZ1.AutoSize = true;
            this.lblZ1.Location = new System.Drawing.Point(15, 118);
            this.lblZ1.Text = "小齿轮齿数 Z1:";
            this.txtZ1.Location = new System.Drawing.Point(120, 115);
            this.txtZ1.Size = new System.Drawing.Size(80, 21);
            this.txtZ1.TabIndex = 3;

            // Z2
            this.lblZ2.AutoSize = true;
            this.lblZ2.Location = new System.Drawing.Point(15, 148);
            this.lblZ2.Text = "大齿轮齿数 Z2:";
            this.txtZ2.Location = new System.Drawing.Point(120, 145);
            this.txtZ2.Size = new System.Drawing.Size(80, 21);
            this.txtZ2.TabIndex = 4;

            this.grpBasic.Controls.Add(this.lblPower);
            this.grpBasic.Controls.Add(this.txtPower);
            this.grpBasic.Controls.Add(this.lblPowerUnit);
            this.grpBasic.Controls.Add(this.lblSpeed);
            this.grpBasic.Controls.Add(this.txtSpeed);
            this.grpBasic.Controls.Add(this.lblSpeedUnit);
            this.grpBasic.Controls.Add(this.lblGearRatio);
            this.grpBasic.Controls.Add(this.txtGearRatio);
            this.grpBasic.Controls.Add(this.lblZ1);
            this.grpBasic.Controls.Add(this.txtZ1);
            this.grpBasic.Controls.Add(this.lblZ2);
            this.grpBasic.Controls.Add(this.txtZ2);

            // ===== 几何参数 GroupBox =====
            this.grpGeometry = new System.Windows.Forms.GroupBox();
            this.lblModule = new System.Windows.Forms.Label();
            this.txtModule = new System.Windows.Forms.TextBox();
            this.lblModuleUnit = new System.Windows.Forms.Label();
            this.lblPressureAngle = new System.Windows.Forms.Label();
            this.txtPressureAngle = new System.Windows.Forms.TextBox();
            this.lblPressureAngleUnit = new System.Windows.Forms.Label();
            this.lblFaceWidthCoeff = new System.Windows.Forms.Label();
            this.txtFaceWidthCoeff = new System.Windows.Forms.TextBox();
            this.lblShaftAngle = new System.Windows.Forms.Label();
            this.txtShaftAngle = new System.Windows.Forms.TextBox();
            this.lblShaftAngleUnit = new System.Windows.Forms.Label();

            this.grpGeometry.Location = new System.Drawing.Point(290, 12);
            this.grpGeometry.Name = "grpGeometry";
            this.grpGeometry.Size = new System.Drawing.Size(270, 150);
            this.grpGeometry.TabIndex = 1;
            this.grpGeometry.TabStop = false;
            this.grpGeometry.Text = "几何参数";

            // 模数
            this.lblModule.AutoSize = true;
            this.lblModule.Location = new System.Drawing.Point(15, 28);
            this.lblModule.Text = "模数 m:";
            this.txtModule.Location = new System.Drawing.Point(120, 25);
            this.txtModule.Size = new System.Drawing.Size(80, 21);
            this.txtModule.TabIndex = 0;
            this.lblModuleUnit.AutoSize = true;
            this.lblModuleUnit.Location = new System.Drawing.Point(205, 28);
            this.lblModuleUnit.Text = "mm";

            // 压力角
            this.lblPressureAngle.AutoSize = true;
            this.lblPressureAngle.Location = new System.Drawing.Point(15, 58);
            this.lblPressureAngle.Text = "压力角 alpha:";
            this.txtPressureAngle.Location = new System.Drawing.Point(120, 55);
            this.txtPressureAngle.Size = new System.Drawing.Size(80, 21);
            this.txtPressureAngle.TabIndex = 1;
            this.lblPressureAngleUnit.AutoSize = true;
            this.lblPressureAngleUnit.Location = new System.Drawing.Point(205, 58);
            this.lblPressureAngleUnit.Text = "deg";

            // 齿宽系数
            this.lblFaceWidthCoeff.AutoSize = true;
            this.lblFaceWidthCoeff.Location = new System.Drawing.Point(15, 88);
            this.lblFaceWidthCoeff.Text = "齿宽系数 phi_R:";
            this.txtFaceWidthCoeff.Location = new System.Drawing.Point(120, 85);
            this.txtFaceWidthCoeff.Size = new System.Drawing.Size(80, 21);
            this.txtFaceWidthCoeff.TabIndex = 2;

            // 轴交角
            this.lblShaftAngle.AutoSize = true;
            this.lblShaftAngle.Location = new System.Drawing.Point(15, 118);
            this.lblShaftAngle.Text = "轴交角 Sigma:";
            this.txtShaftAngle.Location = new System.Drawing.Point(120, 115);
            this.txtShaftAngle.Size = new System.Drawing.Size(80, 21);
            this.txtShaftAngle.TabIndex = 3;
            this.lblShaftAngleUnit.AutoSize = true;
            this.lblShaftAngleUnit.Location = new System.Drawing.Point(205, 118);
            this.lblShaftAngleUnit.Text = "deg";

            this.grpGeometry.Controls.Add(this.lblModule);
            this.grpGeometry.Controls.Add(this.txtModule);
            this.grpGeometry.Controls.Add(this.lblModuleUnit);
            this.grpGeometry.Controls.Add(this.lblPressureAngle);
            this.grpGeometry.Controls.Add(this.txtPressureAngle);
            this.grpGeometry.Controls.Add(this.lblPressureAngleUnit);
            this.grpGeometry.Controls.Add(this.lblFaceWidthCoeff);
            this.grpGeometry.Controls.Add(this.txtFaceWidthCoeff);
            this.grpGeometry.Controls.Add(this.lblShaftAngle);
            this.grpGeometry.Controls.Add(this.txtShaftAngle);
            this.grpGeometry.Controls.Add(this.lblShaftAngleUnit);

            // ===== 载荷系数 GroupBox =====
            this.grpLoadCoeff = new System.Windows.Forms.GroupBox();
            this.lblKA = new System.Windows.Forms.Label();
            this.txtKA = new System.Windows.Forms.TextBox();
            this.lblKv = new System.Windows.Forms.Label();
            this.txtKv = new System.Windows.Forms.TextBox();
            this.lblKbeta = new System.Windows.Forms.Label();
            this.txtKbeta = new System.Windows.Forms.TextBox();
            this.lblKalpha = new System.Windows.Forms.Label();
            this.txtKalpha = new System.Windows.Forms.TextBox();

            this.grpLoadCoeff.Location = new System.Drawing.Point(290, 168);
            this.grpLoadCoeff.Name = "grpLoadCoeff";
            this.grpLoadCoeff.Size = new System.Drawing.Size(270, 130);
            this.grpLoadCoeff.TabIndex = 2;
            this.grpLoadCoeff.TabStop = false;
            this.grpLoadCoeff.Text = "载荷系数";

            this.lblKA.AutoSize = true;
            this.lblKA.Location = new System.Drawing.Point(15, 28);
            this.lblKA.Text = "使用系数 KA:";
            this.txtKA.Location = new System.Drawing.Point(120, 25);
            this.txtKA.Size = new System.Drawing.Size(80, 21);
            this.txtKA.TabIndex = 0;

            this.lblKv.AutoSize = true;
            this.lblKv.Location = new System.Drawing.Point(15, 55);
            this.lblKv.Text = "动载系数 Kv:";
            this.txtKv.Location = new System.Drawing.Point(120, 52);
            this.txtKv.Size = new System.Drawing.Size(80, 21);
            this.txtKv.TabIndex = 1;

            this.lblKbeta.AutoSize = true;
            this.lblKbeta.Location = new System.Drawing.Point(15, 82);
            this.lblKbeta.Text = "齿向载荷系数 Kbeta:";
            this.txtKbeta.Location = new System.Drawing.Point(145, 79);
            this.txtKbeta.Size = new System.Drawing.Size(55, 21);
            this.txtKbeta.TabIndex = 2;

            this.lblKalpha.AutoSize = true;
            this.lblKalpha.Location = new System.Drawing.Point(15, 109);
            this.lblKalpha.Text = "齿间载荷系数 Kalpha:";
            this.txtKalpha.Location = new System.Drawing.Point(145, 106);
            this.txtKalpha.Size = new System.Drawing.Size(55, 21);
            this.txtKalpha.TabIndex = 3;

            this.grpLoadCoeff.Controls.Add(this.lblKA);
            this.grpLoadCoeff.Controls.Add(this.txtKA);
            this.grpLoadCoeff.Controls.Add(this.lblKv);
            this.grpLoadCoeff.Controls.Add(this.txtKv);
            this.grpLoadCoeff.Controls.Add(this.lblKbeta);
            this.grpLoadCoeff.Controls.Add(this.txtKbeta);
            this.grpLoadCoeff.Controls.Add(this.lblKalpha);
            this.grpLoadCoeff.Controls.Add(this.txtKalpha);

            // ===== 材料选择 GroupBox =====
            this.grpMaterial = new System.Windows.Forms.GroupBox();
            this.lblPinionMaterial = new System.Windows.Forms.Label();
            this.cmbPinionMaterial = new System.Windows.Forms.ComboBox();
            this.lblWheelMaterial = new System.Windows.Forms.Label();
            this.cmbWheelMaterial = new System.Windows.Forms.ComboBox();

            this.grpMaterial.Location = new System.Drawing.Point(12, 198);
            this.grpMaterial.Name = "grpMaterial";
            this.grpMaterial.Size = new System.Drawing.Size(270, 100);
            this.grpMaterial.TabIndex = 3;
            this.grpMaterial.TabStop = false;
            this.grpMaterial.Text = "材料选择";

            this.lblPinionMaterial.AutoSize = true;
            this.lblPinionMaterial.Location = new System.Drawing.Point(15, 28);
            this.lblPinionMaterial.Text = "小齿轮材料:";
            this.cmbPinionMaterial.Location = new System.Drawing.Point(100, 25);
            this.cmbPinionMaterial.Size = new System.Drawing.Size(155, 20);
            this.cmbPinionMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPinionMaterial.TabIndex = 0;

            this.lblWheelMaterial.AutoSize = true;
            this.lblWheelMaterial.Location = new System.Drawing.Point(15, 60);
            this.lblWheelMaterial.Text = "大齿轮材料:";
            this.cmbWheelMaterial.Location = new System.Drawing.Point(100, 57);
            this.cmbWheelMaterial.Size = new System.Drawing.Size(155, 20);
            this.cmbWheelMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWheelMaterial.TabIndex = 1;

            this.grpMaterial.Controls.Add(this.lblPinionMaterial);
            this.grpMaterial.Controls.Add(this.cmbPinionMaterial);
            this.grpMaterial.Controls.Add(this.lblWheelMaterial);
            this.grpMaterial.Controls.Add(this.cmbWheelMaterial);

            // ===== 按钮 =====
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnCalculate.Location = new System.Drawing.Point(12, 310);
            this.btnCalculate.Size = new System.Drawing.Size(90, 35);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "计算";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            this.btnDefault = new System.Windows.Forms.Button();
            this.btnDefault.Location = new System.Drawing.Point(112, 310);
            this.btnDefault.Size = new System.Drawing.Size(90, 35);
            this.btnDefault.TabIndex = 5;
            this.btnDefault.Text = "默认值";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);

            this.btnClear = new System.Windows.Forms.Button();
            this.btnClear.Location = new System.Drawing.Point(212, 310);
            this.btnClear.Size = new System.Drawing.Size(90, 35);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "清空结果";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // ===== 结果状态标签 =====
            this.lblContactResult = new System.Windows.Forms.Label();
            this.lblContactResult.AutoSize = true;
            this.lblContactResult.Location = new System.Drawing.Point(320, 310);
            this.lblContactResult.Size = new System.Drawing.Size(120, 16);
            this.lblContactResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblContactResult.TabIndex = 7;

            this.lblBendingResult = new System.Windows.Forms.Label();
            this.lblBendingResult.AutoSize = true;
            this.lblBendingResult.Location = new System.Drawing.Point(320, 332);
            this.lblBendingResult.Size = new System.Drawing.Size(120, 16);
            this.lblBendingResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBendingResult.TabIndex = 8;

            // ===== 结果显示 =====
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtResult.Location = new System.Drawing.Point(12, 355);
            this.txtResult.Multiline = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(548, 300);
            this.txtResult.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResult.TabIndex = 9;

            // ===== Form =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1100, 760);
            this.Controls.Add(this.grpBasic);
            this.Controls.Add(this.grpGeometry);
            this.Controls.Add(this.grpLoadCoeff);
            this.Controls.Add(this.grpMaterial);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblContactResult);
            this.Controls.Add(this.lblBendingResult);
            this.Controls.Add(this.txtResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(1020, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "圆锥齿轮设计 - 直齿圆锥齿轮几何参数与强度计算";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // 基本参数
        private System.Windows.Forms.GroupBox grpBasic;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.TextBox txtPower;
        private System.Windows.Forms.Label lblPowerUnit;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Label lblSpeedUnit;
        private System.Windows.Forms.Label lblGearRatio;
        private System.Windows.Forms.TextBox txtGearRatio;
        private System.Windows.Forms.Label lblZ1;
        private System.Windows.Forms.TextBox txtZ1;
        private System.Windows.Forms.Label lblZ2;
        private System.Windows.Forms.TextBox txtZ2;

        // 几何参数
        private System.Windows.Forms.GroupBox grpGeometry;
        private System.Windows.Forms.Label lblModule;
        private System.Windows.Forms.TextBox txtModule;
        private System.Windows.Forms.Label lblModuleUnit;
        private System.Windows.Forms.Label lblPressureAngle;
        private System.Windows.Forms.TextBox txtPressureAngle;
        private System.Windows.Forms.Label lblPressureAngleUnit;
        private System.Windows.Forms.Label lblFaceWidthCoeff;
        private System.Windows.Forms.TextBox txtFaceWidthCoeff;
        private System.Windows.Forms.Label lblShaftAngle;
        private System.Windows.Forms.TextBox txtShaftAngle;
        private System.Windows.Forms.Label lblShaftAngleUnit;

        // 载荷系数
        private System.Windows.Forms.GroupBox grpLoadCoeff;
        private System.Windows.Forms.Label lblKA;
        private System.Windows.Forms.TextBox txtKA;
        private System.Windows.Forms.Label lblKv;
        private System.Windows.Forms.TextBox txtKv;
        private System.Windows.Forms.Label lblKbeta;
        private System.Windows.Forms.TextBox txtKbeta;
        private System.Windows.Forms.Label lblKalpha;
        private System.Windows.Forms.TextBox txtKalpha;

        // 材料选择
        private System.Windows.Forms.GroupBox grpMaterial;
        private System.Windows.Forms.Label lblPinionMaterial;
        private System.Windows.Forms.ComboBox cmbPinionMaterial;
        private System.Windows.Forms.Label lblWheelMaterial;
        private System.Windows.Forms.ComboBox cmbWheelMaterial;

        // 按钮
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnClear;

        // 结果
        private System.Windows.Forms.Label lblContactResult;
        private System.Windows.Forms.Label lblBendingResult;
        private System.Windows.Forms.TextBox txtResult;
    }
}
