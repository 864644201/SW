namespace BoltCheck
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
            this.lblSpec = new System.Windows.Forms.Label();
            this.cboSpec = new System.Windows.Forms.ComboBox();
            this.lblGrade = new System.Windows.Forms.Label();
            this.cboGrade = new System.Windows.Forms.ComboBox();
            this.lblF = new System.Windows.Forms.Label();
            this.txtF = new System.Windows.Forms.TextBox();
            this.lblFUnit = new System.Windows.Forms.Label();
            this.lblN = new System.Windows.Forms.Label();
            this.numN = new System.Windows.Forms.NumericUpDown();
            this.lblK = new System.Windows.Forms.Label();
            this.txtK = new System.Windows.Forms.TextBox();
            this.lblKNote = new System.Windows.Forms.Label();
            this.lblK2 = new System.Windows.Forms.Label();
            this.txtK2 = new System.Windows.Forms.TextBox();
            this.lblK2Note = new System.Windows.Forms.Label();
            this.lblK1 = new System.Windows.Forms.Label();
            this.cboK1 = new System.Windows.Forms.ComboBox();
            this.lblCbRatio = new System.Windows.Forms.Label();
            this.txtCbRatio = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFatigueStatus = new System.Windows.Forms.Label();

            this.grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numN)).BeginInit();
            this.grpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();

            // grpInput
            this.grpInput.Controls.Add(this.lblSpec);
            this.grpInput.Controls.Add(this.cboSpec);
            this.grpInput.Controls.Add(this.lblGrade);
            this.grpInput.Controls.Add(this.cboGrade);
            this.grpInput.Controls.Add(this.lblF);
            this.grpInput.Controls.Add(this.txtF);
            this.grpInput.Controls.Add(this.lblFUnit);
            this.grpInput.Controls.Add(this.lblN);
            this.grpInput.Controls.Add(this.numN);
            this.grpInput.Controls.Add(this.lblK);
            this.grpInput.Controls.Add(this.txtK);
            this.grpInput.Controls.Add(this.lblKNote);
            this.grpInput.Controls.Add(this.lblK2);
            this.grpInput.Controls.Add(this.txtK2);
            this.grpInput.Controls.Add(this.lblK2Note);
            this.grpInput.Controls.Add(this.lblK1);
            this.grpInput.Controls.Add(this.cboK1);
            this.grpInput.Controls.Add(this.lblCbRatio);
            this.grpInput.Controls.Add(this.txtCbRatio);
            this.grpInput.Controls.Add(this.btnCalc);
            this.grpInput.Location = new System.Drawing.Point(12, 12);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(340, 480);
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "输入参数";

            // lblSpec
            this.lblSpec.AutoSize = true;
            this.lblSpec.Location = new System.Drawing.Point(15, 30);
            this.lblSpec.Name = "lblSpec";
            this.lblSpec.Size = new System.Drawing.Size(65, 13);
            this.lblSpec.Text = "螺栓规格：";

            // cboSpec
            this.cboSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSpec.Location = new System.Drawing.Point(120, 27);
            this.cboSpec.Name = "cboSpec";
            this.cboSpec.Size = new System.Drawing.Size(190, 21);
            this.cboSpec.TabIndex = 1;

            // lblGrade
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new System.Drawing.Point(15, 65);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(65, 13);
            this.lblGrade.Text = "材料等级：";

            // cboGrade
            this.cboGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrade.Location = new System.Drawing.Point(120, 62);
            this.cboGrade.Name = "cboGrade";
            this.cboGrade.Size = new System.Drawing.Size(190, 21);
            this.cboGrade.TabIndex = 3;

            // lblF
            this.lblF.AutoSize = true;
            this.lblF.Location = new System.Drawing.Point(15, 100);
            this.lblF.Name = "lblF";
            this.lblF.Size = new System.Drawing.Size(85, 13);
            this.lblF.Text = "轴向力 F：";

            // txtF
            this.txtF.Location = new System.Drawing.Point(120, 97);
            this.txtF.Name = "txtF";
            this.txtF.Size = new System.Drawing.Size(130, 20);
            this.txtF.TabIndex = 5;
            this.txtF.Text = "10";

            // lblFUnit
            this.lblFUnit.AutoSize = true;
            this.lblFUnit.Location = new System.Drawing.Point(255, 100);
            this.lblFUnit.Name = "lblFUnit";
            this.lblFUnit.Size = new System.Drawing.Size(28, 13);
            this.lblFUnit.Text = "kN";

            // lblN
            this.lblN.AutoSize = true;
            this.lblN.Location = new System.Drawing.Point(15, 135);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(85, 13);
            this.lblN.Text = "螺栓数量 n：";

            // numN
            this.numN.Location = new System.Drawing.Point(120, 132);
            this.numN.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numN.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numN.Value = new decimal(new int[] { 4, 0, 0, 0 });
            this.numN.Name = "numN";
            this.numN.Size = new System.Drawing.Size(130, 20);
            this.numN.TabIndex = 7;

            // lblK
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(15, 170);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(95, 13);
            this.lblK.Text = "预紧力系数 K：";

            // txtK
            this.txtK.Location = new System.Drawing.Point(120, 167);
            this.txtK.Name = "txtK";
            this.txtK.Size = new System.Drawing.Size(130, 20);
            this.txtK.TabIndex = 9;
            this.txtK.Text = "2.0";

            // lblKNote
            this.lblKNote.AutoSize = true;
            this.lblKNote.ForeColor = System.Drawing.Color.Gray;
            this.lblKNote.Location = new System.Drawing.Point(255, 170);
            this.lblKNote.Name = "lblKNote";
            this.lblKNote.Size = new System.Drawing.Size(70, 13);
            this.lblKNote.Text = "1.5~2.5";

            // lblK2
            this.lblK2.AutoSize = true;
            this.lblK2.Location = new System.Drawing.Point(15, 205);
            this.lblK2.Name = "lblK2";
            this.lblK2.Size = new System.Drawing.Size(105, 13);
            this.lblK2.Text = "残余预紧力系数K2：";

            // txtK2
            this.txtK2.Location = new System.Drawing.Point(120, 202);
            this.txtK2.Name = "txtK2";
            this.txtK2.Size = new System.Drawing.Size(130, 20);
            this.txtK2.TabIndex = 11;
            this.txtK2.Text = "1.0";

            // lblK2Note
            this.lblK2Note.AutoSize = true;
            this.lblK2Note.ForeColor = System.Drawing.Color.Gray;
            this.lblK2Note.Location = new System.Drawing.Point(255, 205);
            this.lblK2Note.Name = "lblK2Note";
            this.lblK2Note.Size = new System.Drawing.Size(70, 13);
            this.lblK2Note.Text = "静载0.6~1.0";

            // lblK1
            this.lblK1.AutoSize = true;
            this.lblK1.Location = new System.Drawing.Point(15, 240);
            this.lblK1.Name = "lblK1";
            this.lblK1.Size = new System.Drawing.Size(95, 13);
            this.lblK1.Text = "拧紧力矩系数K1：";

            // cboK1
            this.cboK1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboK1.Items.AddRange(new object[] { "干摩擦 0.20", "有润滑 0.15" });
            this.cboK1.SelectedIndex = 0;
            this.cboK1.Location = new System.Drawing.Point(120, 237);
            this.cboK1.Name = "cboK1";
            this.cboK1.Size = new System.Drawing.Size(190, 21);
            this.cboK1.TabIndex = 13;

            // lblCbRatio
            this.lblCbRatio.AutoSize = true;
            this.lblCbRatio.Location = new System.Drawing.Point(15, 275);
            this.lblCbRatio.Name = "lblCbRatio";
            this.lblCbRatio.Size = new System.Drawing.Size(105, 13);
            this.lblCbRatio.Text = "刚度比Cb/(Cb+Cm)：";

            // txtCbRatio
            this.txtCbRatio.Location = new System.Drawing.Point(120, 272);
            this.txtCbRatio.Name = "txtCbRatio";
            this.txtCbRatio.Size = new System.Drawing.Size(130, 20);
            this.txtCbRatio.TabIndex = 15;
            this.txtCbRatio.Text = "0.3";

            // btnCalc
            this.btnCalc.Location = new System.Drawing.Point(120, 310);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(190, 35);
            this.btnCalc.TabIndex = 16;
            this.btnCalc.Text = "计 算";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);

            // grpResult
            this.grpResult.Controls.Add(this.dgvResult);
            this.grpResult.Location = new System.Drawing.Point(365, 12);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(520, 420);
            this.grpResult.TabIndex = 1;
            this.grpResult.TabStop = false;
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
            this.dgvResult.Size = new System.Drawing.Size(500, 390);
            this.dgvResult.TabIndex = 0;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // colItem
            this.colItem.HeaderText = "项目";
            this.colItem.Name = "colItem";
            this.colItem.FillWeight = 30;

            // colValue
            this.colValue.HeaderText = "数值";
            this.colValue.Name = "colValue";
            this.colValue.FillWeight = 25;

            // colUnit
            this.colUnit.HeaderText = "单位";
            this.colUnit.Name = "colUnit";
            this.colUnit.FillWeight = 15;

            // colNote
            this.colNote.HeaderText = "说明";
            this.colNote.Name = "colNote";
            this.colNote.FillWeight = 30;

            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colItem, this.colValue, this.colUnit, this.colNote
            });

            // grpStatus
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Controls.Add(this.lblFatigueStatus);
            this.grpStatus.Location = new System.Drawing.Point(365, 438);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(520, 55);
            this.grpStatus.TabIndex = 2;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "校核结论";

            // lblStatus
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(10, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(240, 28);
            this.lblStatus.Text = "待计算";

            // lblFatigueStatus
            this.lblFatigueStatus.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.lblFatigueStatus.Location = new System.Drawing.Point(260, 18);
            this.lblFatigueStatus.Name = "lblFatigueStatus";
            this.lblFatigueStatus.Size = new System.Drawing.Size(250, 28);
            this.lblFatigueStatus.Text = "";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.grpInput);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.grpStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "螺栓校核计算";

            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numN)).EndInit();
            this.grpResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.ComboBox cboSpec;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.ComboBox cboGrade;
        private System.Windows.Forms.Label lblF;
        private System.Windows.Forms.TextBox txtF;
        private System.Windows.Forms.Label lblFUnit;
        private System.Windows.Forms.Label lblN;
        private System.Windows.Forms.NumericUpDown numN;
        private System.Windows.Forms.Label lblK;
        private System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Label lblKNote;
        private System.Windows.Forms.Label lblK2;
        private System.Windows.Forms.TextBox txtK2;
        private System.Windows.Forms.Label lblK2Note;
        private System.Windows.Forms.Label lblK1;
        private System.Windows.Forms.ComboBox cboK1;
        private System.Windows.Forms.Label lblCbRatio;
        private System.Windows.Forms.TextBox txtCbRatio;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblFatigueStatus;
    }
}
