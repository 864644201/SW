namespace PulleyDesign
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
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.lblPower = new System.Windows.Forms.Label();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.lblPowerUnit = new System.Windows.Forms.Label();
            this.lblN1 = new System.Windows.Forms.Label();
            this.txtN1 = new System.Windows.Forms.TextBox();
            this.lblN1Unit = new System.Windows.Forms.Label();
            this.lblRatio = new System.Windows.Forms.Label();
            this.txtRatio = new System.Windows.Forms.TextBox();
            this.lblBeltType = new System.Windows.Forms.Label();
            this.cboBeltType = new System.Windows.Forms.ComboBox();
            this.lblD1 = new System.Windows.Forms.Label();
            this.txtD1 = new System.Windows.Forms.TextBox();
            this.lblD1Unit = new System.Windows.Forms.Label();
            this.lblCenterDist = new System.Windows.Forms.Label();
            this.txtCenterDist = new System.Windows.Forms.TextBox();
            this.lblCenterDistUnit = new System.Windows.Forms.Label();
            this.btnRecommend = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();

            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.grpCheck = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();

            this.grpInput.SuspendLayout();
            this.grpOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.grpCheck.SuspendLayout();
            this.SuspendLayout();

            // grpInput
            this.grpInput.Controls.Add(this.lblPower);
            this.grpInput.Controls.Add(this.txtPower);
            this.grpInput.Controls.Add(this.lblPowerUnit);
            this.grpInput.Controls.Add(this.lblN1);
            this.grpInput.Controls.Add(this.txtN1);
            this.grpInput.Controls.Add(this.lblN1Unit);
            this.grpInput.Controls.Add(this.lblRatio);
            this.grpInput.Controls.Add(this.txtRatio);
            this.grpInput.Controls.Add(this.lblBeltType);
            this.grpInput.Controls.Add(this.cboBeltType);
            this.grpInput.Controls.Add(this.lblD1);
            this.grpInput.Controls.Add(this.txtD1);
            this.grpInput.Controls.Add(this.lblD1Unit);
            this.grpInput.Controls.Add(this.lblCenterDist);
            this.grpInput.Controls.Add(this.txtCenterDist);
            this.grpInput.Controls.Add(this.lblCenterDistUnit);
            this.grpInput.Controls.Add(this.btnRecommend);
            this.grpInput.Controls.Add(this.btnCalculate);
            this.grpInput.Location = new System.Drawing.Point(12, 12);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(350, 380);
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "输入参数";

            // lblPower
            this.lblPower.AutoSize = true;
            this.lblPower.Location = new System.Drawing.Point(15, 30);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(95, 13);
            this.lblPower.Text = "传递功率 P:";

            // txtPower
            this.txtPower.Location = new System.Drawing.Point(120, 27);
            this.txtPower.Name = "txtPower";
            this.txtPower.Size = new System.Drawing.Size(120, 20);
            this.txtPower.TabIndex = 1;
            this.txtPower.Text = "5.5";

            // lblPowerUnit
            this.lblPowerUnit.AutoSize = true;
            this.lblPowerUnit.Location = new System.Drawing.Point(246, 30);
            this.lblPowerUnit.Name = "lblPowerUnit";
            this.lblPowerUnit.Size = new System.Drawing.Size(28, 13);
            this.lblPowerUnit.Text = "kW";

            // lblN1
            this.lblN1.AutoSize = true;
            this.lblN1.Location = new System.Drawing.Point(15, 60);
            this.lblN1.Name = "lblN1";
            this.lblN1.Size = new System.Drawing.Size(105, 13);
            this.lblN1.Text = "小带轮转速 n1:";

            // txtN1
            this.txtN1.Location = new System.Drawing.Point(120, 57);
            this.txtN1.Name = "txtN1";
            this.txtN1.Size = new System.Drawing.Size(120, 20);
            this.txtN1.TabIndex = 2;
            this.txtN1.Text = "1440";

            // lblN1Unit
            this.lblN1Unit.AutoSize = true;
            this.lblN1Unit.Location = new System.Drawing.Point(246, 60);
            this.lblN1Unit.Name = "lblN1Unit";
            this.lblN1Unit.Size = new System.Drawing.Size(28, 13);
            this.lblN1Unit.Text = "rpm";

            // lblRatio
            this.lblRatio.AutoSize = true;
            this.lblRatio.Location = new System.Drawing.Point(15, 90);
            this.lblRatio.Name = "lblRatio";
            this.lblRatio.Size = new System.Drawing.Size(60, 13);
            this.lblRatio.Text = "传动比 i:";

            // txtRatio
            this.txtRatio.Location = new System.Drawing.Point(120, 87);
            this.txtRatio.Name = "txtRatio";
            this.txtRatio.Size = new System.Drawing.Size(120, 20);
            this.txtRatio.TabIndex = 3;
            this.txtRatio.Text = "3";

            // lblBeltType
            this.lblBeltType.AutoSize = true;
            this.lblBeltType.Location = new System.Drawing.Point(15, 120);
            this.lblBeltType.Name = "lblBeltType";
            this.lblBeltType.Size = new System.Drawing.Size(60, 13);
            this.lblBeltType.Text = "带类型:";

            // cboBeltType
            this.cboBeltType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBeltType.Items.AddRange(new object[] { "V带 A型", "V带 B型", "V带 C型", "V带 D型", "V带 E型", "平带", "同步带" });
            this.cboBeltType.Location = new System.Drawing.Point(120, 117);
            this.cboBeltType.Name = "cboBeltType";
            this.cboBeltType.Size = new System.Drawing.Size(120, 21);
            this.cboBeltType.TabIndex = 4;
            this.cboBeltType.SelectedIndex = 2; // 默认C型

            // lblD1
            this.lblD1.AutoSize = true;
            this.lblD1.Location = new System.Drawing.Point(15, 150);
            this.lblD1.Name = "lblD1";
            this.lblD1.Size = new System.Drawing.Size(105, 13);
            this.lblD1.Text = "小带轮直径 d1:";

            // txtD1
            this.txtD1.Location = new System.Drawing.Point(120, 147);
            this.txtD1.Name = "txtD1";
            this.txtD1.Size = new System.Drawing.Size(120, 20);
            this.txtD1.TabIndex = 5;
            this.txtD1.Text = "200";

            // lblD1Unit
            this.lblD1Unit.AutoSize = true;
            this.lblD1Unit.Location = new System.Drawing.Point(246, 150);
            this.lblD1Unit.Name = "lblD1Unit";
            this.lblD1Unit.Size = new System.Drawing.Size(23, 13);
            this.lblD1Unit.Text = "mm";

            // lblCenterDist
            this.lblCenterDist.AutoSize = true;
            this.lblCenterDist.Location = new System.Drawing.Point(15, 180);
            this.lblCenterDist.Name = "lblCenterDist";
            this.lblCenterDist.Size = new System.Drawing.Size(105, 13);
            this.lblCenterDist.Text = "中心距 a0 (0=自动):";

            // txtCenterDist
            this.txtCenterDist.Location = new System.Drawing.Point(120, 177);
            this.txtCenterDist.Name = "txtCenterDist";
            this.txtCenterDist.Size = new System.Drawing.Size(120, 20);
            this.txtCenterDist.TabIndex = 6;
            this.txtCenterDist.Text = "0";

            // lblCenterDistUnit
            this.lblCenterDistUnit.AutoSize = true;
            this.lblCenterDistUnit.Location = new System.Drawing.Point(246, 180);
            this.lblCenterDistUnit.Name = "lblCenterDistUnit";
            this.lblCenterDistUnit.Size = new System.Drawing.Size(23, 13);
            this.lblCenterDistUnit.Text = "mm";

            // btnRecommend
            this.btnRecommend.Location = new System.Drawing.Point(15, 220);
            this.btnRecommend.Name = "btnRecommend";
            this.btnRecommend.Size = new System.Drawing.Size(150, 30);
            this.btnRecommend.TabIndex = 7;
            this.btnRecommend.Text = "推荐带型";
            this.btnRecommend.UseVisualStyleBackColor = true;
            this.btnRecommend.Click += new System.EventHandler(this.btnRecommend_Click);

            // btnCalculate
            this.btnCalculate.Location = new System.Drawing.Point(175, 220);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(150, 30);
            this.btnCalculate.TabIndex = 8;
            this.btnCalculate.Text = "计算";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            // grpOutput
            this.grpOutput.Controls.Add(this.dgvResult);
            this.grpOutput.Location = new System.Drawing.Point(375, 12);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(520, 380);
            this.grpOutput.TabIndex = 1;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "计算结果";

            // dgvResult
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colParam, this.colValue, this.colUnit, this.colCheck
            });
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(3, 16);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.TabIndex = 0;

            // colParam
            this.colParam.HeaderText = "参数";
            this.colParam.Name = "colParam";
            this.colParam.Width = 150;
            this.colParam.ReadOnly = true;

            // colValue
            this.colValue.HeaderText = "数值";
            this.colValue.Name = "colValue";
            this.colValue.Width = 120;
            this.colValue.ReadOnly = true;

            // colUnit
            this.colUnit.HeaderText = "单位";
            this.colUnit.Name = "colUnit";
            this.colUnit.Width = 60;
            this.colUnit.ReadOnly = true;

            // colCheck
            this.colCheck.HeaderText = "校核";
            this.colCheck.Name = "colCheck";
            this.colCheck.Width = 160;
            this.colCheck.ReadOnly = true;

            // grpCheck
            this.grpCheck.Controls.Add(this.txtRemarks);
            this.grpCheck.Location = new System.Drawing.Point(12, 405);
            this.grpCheck.Name = "grpCheck";
            this.grpCheck.Size = new System.Drawing.Size(883, 150);
            this.grpCheck.TabIndex = 2;
            this.grpCheck.TabStop = false;
            this.grpCheck.Text = "设计说明与校核";

            // txtRemarks
            this.txtRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemarks.Location = new System.Drawing.Point(3, 16);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ReadOnly = true;
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.Size = new System.Drawing.Size(877, 131);
            this.txtRemarks.TabIndex = 0;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 570);
            this.Controls.Add(this.grpInput);
            this.Controls.Add(this.grpOutput);
            this.Controls.Add(this.grpCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "带传动设计 - PulleyDesign";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            this.grpOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.grpCheck.ResumeLayout(false);
            this.grpCheck.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.TextBox txtPower;
        private System.Windows.Forms.Label lblPowerUnit;
        private System.Windows.Forms.Label lblN1;
        private System.Windows.Forms.TextBox txtN1;
        private System.Windows.Forms.Label lblN1Unit;
        private System.Windows.Forms.Label lblRatio;
        private System.Windows.Forms.TextBox txtRatio;
        private System.Windows.Forms.Label lblBeltType;
        private System.Windows.Forms.ComboBox cboBeltType;
        private System.Windows.Forms.Label lblD1;
        private System.Windows.Forms.TextBox txtD1;
        private System.Windows.Forms.Label lblD1Unit;
        private System.Windows.Forms.Label lblCenterDist;
        private System.Windows.Forms.TextBox txtCenterDist;
        private System.Windows.Forms.Label lblCenterDistUnit;
        private System.Windows.Forms.Button btnRecommend;
        private System.Windows.Forms.Button btnCalculate;

        private System.Windows.Forms.GroupBox grpOutput;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCheck;

        private System.Windows.Forms.GroupBox grpCheck;
        private System.Windows.Forms.TextBox txtRemarks;
    }
}
