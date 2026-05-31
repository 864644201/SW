namespace Leadscrew
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
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.lblScrewType = new System.Windows.Forms.Label();
            this.cboScrewType = new System.Windows.Forms.ComboBox();
            this.lblSupport = new System.Windows.Forms.Label();
            this.cboSupport = new System.Windows.Forms.ComboBox();
            this.lblD = new System.Windows.Forms.Label();
            this.txtD = new System.Windows.Forms.TextBox();
            this.lblDUnit = new System.Windows.Forms.Label();
            this.lblPh = new System.Windows.Forms.Label();
            this.txtPh = new System.Windows.Forms.TextBox();
            this.lblPhUnit = new System.Windows.Forms.Label();
            this.lblF = new System.Windows.Forms.Label();
            this.txtF = new System.Windows.Forms.TextBox();
            this.lblFUnit = new System.Windows.Forms.Label();
            this.lblRPM = new System.Windows.Forms.Label();
            this.txtRPM = new System.Windows.Forms.TextBox();
            this.lblRPMUnit = new System.Windows.Forms.Label();
            this.lblL = new System.Windows.Forms.Label();
            this.txtL = new System.Windows.Forms.TextBox();
            this.lblLUnit = new System.Windows.Forms.Label();
            this.lblCa = new System.Windows.Forms.Label();
            this.txtCa = new System.Windows.Forms.TextBox();
            this.lblCaUnit = new System.Windows.Forms.Label();
            this.btnCalc = new System.Windows.Forms.Button();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.lblStrength = new System.Windows.Forms.Label();
            this.lblStability = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblLife = new System.Windows.Forms.Label();

            this.grpInput.SuspendLayout();
            this.grpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();

            // grpInput
            this.grpInput.Controls.Add(this.lblScrewType);
            this.grpInput.Controls.Add(this.cboScrewType);
            this.grpInput.Controls.Add(this.lblSupport);
            this.grpInput.Controls.Add(this.cboSupport);
            this.grpInput.Controls.Add(this.lblD);
            this.grpInput.Controls.Add(this.txtD);
            this.grpInput.Controls.Add(this.lblDUnit);
            this.grpInput.Controls.Add(this.lblPh);
            this.grpInput.Controls.Add(this.txtPh);
            this.grpInput.Controls.Add(this.lblPhUnit);
            this.grpInput.Controls.Add(this.lblF);
            this.grpInput.Controls.Add(this.txtF);
            this.grpInput.Controls.Add(this.lblFUnit);
            this.grpInput.Controls.Add(this.lblRPM);
            this.grpInput.Controls.Add(this.txtRPM);
            this.grpInput.Controls.Add(this.lblRPMUnit);
            this.grpInput.Controls.Add(this.lblL);
            this.grpInput.Controls.Add(this.txtL);
            this.grpInput.Controls.Add(this.lblLUnit);
            this.grpInput.Controls.Add(this.lblCa);
            this.grpInput.Controls.Add(this.txtCa);
            this.grpInput.Controls.Add(this.lblCaUnit);
            this.grpInput.Controls.Add(this.btnCalc);
            this.grpInput.Location = new System.Drawing.Point(12, 12);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(340, 500);
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "输入参数";

            // lblScrewType
            this.lblScrewType.AutoSize = true;
            this.lblScrewType.Location = new System.Drawing.Point(15, 30);
            this.lblScrewType.Text = "丝杠类型：";

            // cboScrewType
            this.cboScrewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScrewType.Items.AddRange(new object[] { "梯形螺纹", "锯齿形螺纹", "滚珠丝杠" });
            this.cboScrewType.SelectedIndex = 0;
            this.cboScrewType.Location = new System.Drawing.Point(120, 27);
            this.cboScrewType.Size = new System.Drawing.Size(190, 21);
            this.cboScrewType.SelectedIndexChanged += new System.EventHandler(this.cboScrewType_SelectedIndexChanged);

            // lblSupport
            this.lblSupport.AutoSize = true;
            this.lblSupport.Location = new System.Drawing.Point(15, 65);
            this.lblSupport.Text = "支撑方式：";

            // cboSupport
            this.cboSupport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupport.Items.AddRange(new object[] { "一端固定一端自由", "一端固定一端铰支", "两端铰支", "两端固定" });
            this.cboSupport.SelectedIndex = 1;
            this.cboSupport.Location = new System.Drawing.Point(120, 62);
            this.cboSupport.Size = new System.Drawing.Size(190, 21);

            // lblD
            this.lblD.AutoSize = true;
            this.lblD.Location = new System.Drawing.Point(15, 100);
            this.lblD.Text = "公称直径 d：";

            // txtD
            this.txtD.Location = new System.Drawing.Point(120, 97);
            this.txtD.Size = new System.Drawing.Size(130, 20);
            this.txtD.Text = "40";

            // lblDUnit
            this.lblDUnit.AutoSize = true;
            this.lblDUnit.Location = new System.Drawing.Point(255, 100);
            this.lblDUnit.Text = "mm";

            // lblPh
            this.lblPh.AutoSize = true;
            this.lblPh.Location = new System.Drawing.Point(15, 135);
            this.lblPh.Text = "导程 Ph：";

            // txtPh
            this.txtPh.Location = new System.Drawing.Point(120, 132);
            this.txtPh.Size = new System.Drawing.Size(130, 20);
            this.txtPh.Text = "6";

            // lblPhUnit
            this.lblPhUnit.AutoSize = true;
            this.lblPhUnit.Location = new System.Drawing.Point(255, 135);
            this.lblPhUnit.Text = "mm";

            // lblF
            this.lblF.AutoSize = true;
            this.lblF.Location = new System.Drawing.Point(15, 170);
            this.lblF.Text = "轴向载荷 F：";

            // txtF
            this.txtF.Location = new System.Drawing.Point(120, 167);
            this.txtF.Size = new System.Drawing.Size(130, 20);
            this.txtF.Text = "5000";

            // lblFUnit
            this.lblFUnit.AutoSize = true;
            this.lblFUnit.Location = new System.Drawing.Point(255, 170);
            this.lblFUnit.Text = "N";

            // lblRPM
            this.lblRPM.AutoSize = true;
            this.lblRPM.Location = new System.Drawing.Point(15, 205);
            this.lblRPM.Text = "转速 n：";

            // txtRPM
            this.txtRPM.Location = new System.Drawing.Point(120, 202);
            this.txtRPM.Size = new System.Drawing.Size(130, 20);
            this.txtRPM.Text = "300";

            // lblRPMUnit
            this.lblRPMUnit.AutoSize = true;
            this.lblRPMUnit.Location = new System.Drawing.Point(255, 205);
            this.lblRPMUnit.Text = "rpm";

            // lblL
            this.lblL.AutoSize = true;
            this.lblL.Location = new System.Drawing.Point(15, 240);
            this.lblL.Text = "丝杠长度 L：";

            // txtL
            this.txtL.Location = new System.Drawing.Point(120, 237);
            this.txtL.Size = new System.Drawing.Size(130, 20);
            this.txtL.Text = "800";

            // lblLUnit
            this.lblLUnit.AutoSize = true;
            this.lblLUnit.Location = new System.Drawing.Point(255, 240);
            this.lblLUnit.Text = "mm";

            // lblCa
            this.lblCa.AutoSize = true;
            this.lblCa.Location = new System.Drawing.Point(15, 275);
            this.lblCa.Text = "额定动载荷Ca：";

            // txtCa
            this.txtCa.Location = new System.Drawing.Point(120, 272);
            this.txtCa.Size = new System.Drawing.Size(130, 20);
            this.txtCa.Text = "20000";

            // lblCaUnit
            this.lblCaUnit.AutoSize = true;
            this.lblCaUnit.Location = new System.Drawing.Point(255, 275);
            this.lblCaUnit.Text = "N";
            this.lblCaUnit.ForeColor = System.Drawing.Color.Gray;

            // btnCalc
            this.btnCalc.Location = new System.Drawing.Point(120, 310);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(190, 35);
            this.btnCalc.Text = "计 算";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);

            // grpResult
            this.grpResult.Controls.Add(this.dgvResult);
            this.grpResult.Location = new System.Drawing.Point(365, 12);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(530, 420);
            this.grpResult.Text = "计算结果";

            // dgvResult
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvResult.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvResult.Location = new System.Drawing.Point(10, 22);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.Size = new System.Drawing.Size(510, 390);
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.colItem.HeaderText = "项目";
            this.colItem.FillWeight = 30;
            this.colValue.HeaderText = "数值";
            this.colValue.FillWeight = 25;
            this.colUnit.HeaderText = "单位";
            this.colUnit.FillWeight = 15;
            this.colNote.HeaderText = "说明";
            this.colNote.FillWeight = 30;

            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colItem, this.colValue, this.colUnit, this.colNote
            });

            // grpStatus
            this.grpStatus.Controls.Add(this.lblStrength);
            this.grpStatus.Controls.Add(this.lblStability);
            this.grpStatus.Controls.Add(this.lblSpeed);
            this.grpStatus.Controls.Add(this.lblLife);
            this.grpStatus.Location = new System.Drawing.Point(365, 438);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(530, 75);
            this.grpStatus.Text = "校核结论";

            this.lblStrength.Location = new System.Drawing.Point(10, 18);
            this.lblStrength.Size = new System.Drawing.Size(250, 18);
            this.lblStrength.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblStrength.Text = "强度: 待计算";

            this.lblStability.Location = new System.Drawing.Point(270, 18);
            this.lblStability.Size = new System.Drawing.Size(250, 18);
            this.lblStability.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblStability.Text = "稳定性: 待计算";

            this.lblSpeed.Location = new System.Drawing.Point(10, 42);
            this.lblSpeed.Size = new System.Drawing.Size(250, 18);
            this.lblSpeed.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblSpeed.Text = "转速: 待计算";

            this.lblLife.Location = new System.Drawing.Point(270, 42);
            this.lblLife.Size = new System.Drawing.Size(250, 18);
            this.lblLife.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblLife.Text = "寿命: -";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(930, 560);
            this.Controls.Add(this.grpInput);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.grpStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(920, 560);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "丝杠设计计算";

            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            this.grpResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Label lblScrewType;
        private System.Windows.Forms.ComboBox cboScrewType;
        private System.Windows.Forms.Label lblSupport;
        private System.Windows.Forms.ComboBox cboSupport;
        private System.Windows.Forms.Label lblD;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.Label lblDUnit;
        private System.Windows.Forms.Label lblPh;
        private System.Windows.Forms.TextBox txtPh;
        private System.Windows.Forms.Label lblPhUnit;
        private System.Windows.Forms.Label lblF;
        private System.Windows.Forms.TextBox txtF;
        private System.Windows.Forms.Label lblFUnit;
        private System.Windows.Forms.Label lblRPM;
        private System.Windows.Forms.TextBox txtRPM;
        private System.Windows.Forms.Label lblRPMUnit;
        private System.Windows.Forms.Label lblL;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.Label lblLUnit;
        private System.Windows.Forms.Label lblCa;
        private System.Windows.Forms.TextBox txtCa;
        private System.Windows.Forms.Label lblCaUnit;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.Label lblStability;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblLife;
    }
}
