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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.tableLayoutInput = new System.Windows.Forms.TableLayoutPanel();
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
            this.panelRight = new System.Windows.Forms.Panel();
            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpCheck = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.panelLeft.SuspendLayout();
            this.grpInput.SuspendLayout();
            this.tableLayoutInput.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.grpOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.grpCheck.SuspendLayout();
            this.SuspendLayout();
            // panelLeft
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Width = 380;
            this.panelLeft.MinimumSize = new System.Drawing.Size(360, 0);
            this.panelLeft.Padding = new System.Windows.Forms.Padding(4);
            this.panelLeft.Controls.Add(this.grpInput);
            // grpInput
            this.grpInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = " 输入参数 ";
            this.grpInput.Font = new System.Drawing.Font("Microsoft YaHei", 9.5F);
            this.grpInput.Controls.Add(this.tableLayoutInput);
            // tableLayoutInput
            this.tableLayoutInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutInput.ColumnCount = 3;
            this.tableLayoutInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutInput.RowCount = 9;
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutInput.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            // lblPower
            this.lblPower.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPower.AutoSize = true;
            this.lblPower.Text = "传递功率 P:";
            this.lblPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPower.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // txtPower
            this.txtPower.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPower.Size = new System.Drawing.Size(100, 24);
            this.txtPower.Text = "5.5";
            this.txtPower.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblPowerUnit
            this.lblPowerUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPowerUnit.AutoSize = true;
            this.lblPowerUnit.Text = "kW";
            this.lblPowerUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPowerUnit.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblN1
            this.lblN1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblN1.AutoSize = true;
            this.lblN1.Text = "小带轮转速 n1:";
            this.lblN1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblN1.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // txtN1
            this.txtN1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtN1.Size = new System.Drawing.Size(100, 24);
            this.txtN1.Text = "1450";
            this.txtN1.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblN1Unit
            this.lblN1Unit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblN1Unit.AutoSize = true;
            this.lblN1Unit.Text = "rpm";
            this.lblN1Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblN1Unit.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblRatio
            this.lblRatio.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRatio.AutoSize = true;
            this.lblRatio.Text = "传动比 i:";
            this.lblRatio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRatio.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // txtRatio
            this.txtRatio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtRatio.Size = new System.Drawing.Size(100, 24);
            this.txtRatio.Text = "2";
            this.txtRatio.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblBeltType
            this.lblBeltType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblBeltType.AutoSize = true;
            this.lblBeltType.Text = "带型:";
            this.lblBeltType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBeltType.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // cboBeltType
            this.cboBeltType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboBeltType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBeltType.Items.AddRange(new object[] { "A", "B", "C", "D", "E" });
            this.cboBeltType.Size = new System.Drawing.Size(100, 25);
            this.cboBeltType.SelectedIndex = 0;
            this.cboBeltType.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblD1
            this.lblD1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblD1.AutoSize = true;
            this.lblD1.Text = "小带轮直径 d1:";
            this.lblD1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblD1.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // txtD1
            this.txtD1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtD1.Size = new System.Drawing.Size(100, 24);
            this.txtD1.Text = "100";
            this.txtD1.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblD1Unit
            this.lblD1Unit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblD1Unit.AutoSize = true;
            this.lblD1Unit.Text = "mm";
            this.lblD1Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblD1Unit.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblCenterDist
            this.lblCenterDist.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCenterDist.AutoSize = true;
            this.lblCenterDist.Text = "中心距 a (0=自动):";
            this.lblCenterDist.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCenterDist.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // txtCenterDist
            this.txtCenterDist.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCenterDist.Size = new System.Drawing.Size(100, 24);
            this.txtCenterDist.Text = "0";
            this.txtCenterDist.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // lblCenterDistUnit
            this.lblCenterDistUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCenterDistUnit.AutoSize = true;
            this.lblCenterDistUnit.Text = "mm";
            this.lblCenterDistUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCenterDistUnit.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // btnRecommend
            this.btnRecommend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRecommend.Size = new System.Drawing.Size(110, 34);
            this.btnRecommend.Text = "推荐带型";
            this.btnRecommend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecommend.BackColor = System.Drawing.Color.FromArgb(66, 133, 244);
            this.btnRecommend.ForeColor = System.Drawing.Color.White;
            this.btnRecommend.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.btnRecommend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecommend.Click += new System.EventHandler(this.btnRecommend_Click);
            // btnCalculate
            this.btnCalculate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCalculate.Size = new System.Drawing.Size(110, 34);
            this.btnCalculate.Text = "计 算";
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(52, 168, 83);
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // TableLayoutPanel controls
            this.tableLayoutInput.Controls.Add(this.lblPower, 0, 0);
            this.tableLayoutInput.Controls.Add(this.txtPower, 1, 0);
            this.tableLayoutInput.Controls.Add(this.lblPowerUnit, 2, 0);
            this.tableLayoutInput.Controls.Add(this.lblN1, 0, 1);
            this.tableLayoutInput.Controls.Add(this.txtN1, 1, 1);
            this.tableLayoutInput.Controls.Add(this.lblN1Unit, 2, 1);
            this.tableLayoutInput.Controls.Add(this.lblRatio, 0, 2);
            this.tableLayoutInput.Controls.Add(this.txtRatio, 1, 2);
            this.tableLayoutInput.Controls.Add(this.lblBeltType, 0, 3);
            this.tableLayoutInput.Controls.Add(this.cboBeltType, 1, 3);
            this.tableLayoutInput.Controls.Add(this.lblD1, 0, 4);
            this.tableLayoutInput.Controls.Add(this.txtD1, 1, 4);
            this.tableLayoutInput.Controls.Add(this.lblD1Unit, 2, 4);
            this.tableLayoutInput.Controls.Add(this.lblCenterDist, 0, 5);
            this.tableLayoutInput.Controls.Add(this.txtCenterDist, 1, 5);
            this.tableLayoutInput.Controls.Add(this.lblCenterDistUnit, 2, 5);
            this.tableLayoutInput.Controls.Add(this.btnRecommend, 0, 6);
            this.tableLayoutInput.SetColumnSpan(this.btnRecommend, 2);
            this.tableLayoutInput.Controls.Add(this.btnCalculate, 0, 7);
            this.tableLayoutInput.SetColumnSpan(this.btnCalculate, 2);
            // panelRight
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Padding = new System.Windows.Forms.Padding(4);
            this.panelRight.Controls.Add(this.grpOutput);
            this.panelRight.Controls.Add(this.grpCheck);
            // grpOutput
            this.grpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOutput.TabIndex = 1;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = " 计算结果 ";
            this.grpOutput.Font = new System.Drawing.Font("Microsoft YaHei", 9.5F);
            this.grpOutput.Controls.Add(this.dgvResult);
            // dgvResult
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.BackgroundColor = System.Drawing.Color.White;
            this.dgvResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResult.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);
            this.dgvResult.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.dgvResult.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.dgvResult.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 244, 252);
            this.colParam.HeaderText = "参数";
            this.colParam.Name = "colParam";
            this.colParam.FillWeight = 35;
            this.colValue.HeaderText = "数值";
            this.colValue.Name = "colValue";
            this.colValue.FillWeight = 25;
            this.colUnit.HeaderText = "单位";
            this.colUnit.Name = "colUnit";
            this.colUnit.FillWeight = 15;
            this.colCheck.HeaderText = "校核";
            this.colCheck.Name = "colCheck";
            this.colCheck.FillWeight = 25;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colParam, this.colValue, this.colUnit, this.colCheck
            });
            // grpCheck
            this.grpCheck.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpCheck.Height = 180;
            this.grpCheck.MinimumSize = new System.Drawing.Size(0, 120);
            this.grpCheck.TabIndex = 2;
            this.grpCheck.TabStop = false;
            this.grpCheck.Text = " 设计说明与校核 ";
            this.grpCheck.Font = new System.Drawing.Font("Microsoft YaHei", 9.5F);
            this.grpCheck.Controls.Add(this.txtRemarks);
            // txtRemarks
            this.txtRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.ReadOnly = true;
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.BackColor = System.Drawing.Color.White;
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            // MainForm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "带传动设计 - PulleyDesign";
            this.panelLeft.ResumeLayout(false);
            this.grpInput.ResumeLayout(false);
            this.tableLayoutInput.ResumeLayout(false);
            this.tableLayoutInput.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.grpOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.grpCheck.ResumeLayout(false);
            this.grpCheck.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutInput;
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
        private System.Windows.Forms.Panel panelRight;
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
