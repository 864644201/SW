namespace EngCalculator
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
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.treeFormulas = new System.Windows.Forms.TreeView();
            this.picFormula = new System.Windows.Forms.PictureBox();
            this.panelCalc = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.lblExpression = new System.Windows.Forms.Label();
            this.txtVarInput = new System.Windows.Forms.TextBox();
            this.lblVarHint = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
            this.splitContainerRight.Panel1.SuspendLayout();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFormula)).BeginInit();
            this.panelCalc.SuspendLayout();
            this.SuspendLayout();

            // splitContainerMain: Left=Tree, Right=PictureBox+Calc
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Panel1.Controls.Add(this.treeFormulas);
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerRight);
            this.splitContainerMain.Size = new System.Drawing.Size(1100, 650);
            this.splitContainerMain.SplitterDistance = 260;
            this.splitContainerMain.TabIndex = 0;

            // treeFormulas
            this.treeFormulas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFormulas.Location = new System.Drawing.Point(0, 0);
            this.treeFormulas.Name = "treeFormulas";
            this.treeFormulas.Size = new System.Drawing.Size(260, 650);
            this.treeFormulas.TabIndex = 0;
            this.treeFormulas.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeFormulas_AfterSelect);

            // splitContainerRight: Top=PictureBox, Bottom=Calculator
            this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRight.Name = "splitContainerRight";
            this.splitContainerRight.Panel1.Controls.Add(this.picFormula);
            this.splitContainerRight.Panel2.Controls.Add(this.panelCalc);
            this.splitContainerRight.Size = new System.Drawing.Size(836, 650);
            this.splitContainerRight.SplitterDistance = 380;
            this.splitContainerRight.TabIndex = 0;

            // picFormula
            this.picFormula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picFormula.Location = new System.Drawing.Point(0, 0);
            this.picFormula.Name = "picFormula";
            this.picFormula.Size = new System.Drawing.Size(836, 380);
            this.picFormula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFormula.TabIndex = 0;
            this.picFormula.TabStop = false;
            this.picFormula.BackColor = System.Drawing.Color.White;

            // panelCalc
            this.panelCalc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCalc.Location = new System.Drawing.Point(0, 0);
            this.panelCalc.Name = "panelCalc";
            this.panelCalc.Size = new System.Drawing.Size(836, 266);
            this.panelCalc.TabIndex = 0;

            // lblExpression
            this.lblExpression.AutoSize = true;
            this.lblExpression.Location = new System.Drawing.Point(10, 10);
            this.lblExpression.Name = "lblExpression";
            this.lblExpression.Size = new System.Drawing.Size(65, 12);
            this.lblExpression.TabIndex = 0;
            this.lblExpression.Text = "表达式:";

            // txtExpression
            this.txtExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpression.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtExpression.Location = new System.Drawing.Point(80, 6);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(650, 26);
            this.txtExpression.TabIndex = 1;
            this.txtExpression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtExpression_KeyDown);

            // lblVarHint
            this.lblVarHint.AutoSize = true;
            this.lblVarHint.Location = new System.Drawing.Point(10, 40);
            this.lblVarHint.Name = "lblVarHint";
            this.lblVarHint.Size = new System.Drawing.Size(65, 12);
            this.lblVarHint.TabIndex = 2;
            this.lblVarHint.Text = "变量赋值:";

            // txtVarInput
            this.txtVarInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVarInput.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtVarInput.Location = new System.Drawing.Point(80, 36);
            this.txtVarInput.Name = "txtVarInput";
            this.txtVarInput.Size = new System.Drawing.Size(650, 23);
            this.txtVarInput.TabIndex = 3;
            this.txtVarInput.Text = "如: a=10, b=20, h=5";

            // btnCalc
            this.btnCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalc.Location = new System.Drawing.Point(740, 6);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(85, 53);
            this.btnCalc.TabIndex = 4;
            this.btnCalc.Text = "计算 (=)";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.BtnCalc_Click);

            // lblResult
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(10, 70);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(41, 12);
            this.lblResult.TabIndex = 5;
            this.lblResult.Text = "结果:";

            // txtResult
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Font = new System.Drawing.Font("Consolas", 11F);
            this.txtResult.Location = new System.Drawing.Point(10, 88);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(815, 170);
            this.txtResult.TabIndex = 6;

            // panelCalc controls
            this.panelCalc.Controls.Add(this.lblExpression);
            this.panelCalc.Controls.Add(this.txtExpression);
            this.panelCalc.Controls.Add(this.lblVarHint);
            this.panelCalc.Controls.Add(this.txtVarInput);
            this.panelCalc.Controls.Add(this.btnCalc);
            this.panelCalc.Controls.Add(this.lblResult);
            this.panelCalc.Controls.Add(this.txtResult);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.splitContainerMain);
            this.Name = "MainForm";
            this.Text = "工程计算器";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerRight.Panel1.ResumeLayout(false);
            this.splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
            this.splitContainerRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFormula)).EndInit();
            this.panelCalc.ResumeLayout(false);
            this.panelCalc.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerRight;
        private System.Windows.Forms.TreeView treeFormulas;
        private System.Windows.Forms.PictureBox picFormula;
        private System.Windows.Forms.Panel panelCalc;
        private System.Windows.Forms.Label lblExpression;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label lblVarHint;
        private System.Windows.Forms.TextBox txtVarInput;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtResult;
    }
}
