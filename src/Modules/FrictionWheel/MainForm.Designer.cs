namespace FrictionWheel
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabCylindrical = new System.Windows.Forms.TabPage();
            this.tabGroove = new System.Windows.Forms.TabPage();
            this.tabEndFace = new System.Windows.Forms.TabPage();
            this.tabConical = new System.Windows.Forms.TabPage();

            // ====== Common input labels/values reused per tab ======
            // Cylindrical tab
            this.panelCylInput = new System.Windows.Forms.Panel();
            this.panelCylResult = new System.Windows.Forms.Panel();
            this.txtCylP1 = new System.Windows.Forms.TextBox();
            this.txtCyln1 = new System.Windows.Forms.TextBox();
            this.txtCyln2 = new System.Windows.Forms.TextBox();
            this.txtCyli = new System.Windows.Forms.TextBox();
            this.txtCylMu = new System.Windows.Forms.TextBox();
            this.txtCylKa = new System.Windows.Forms.TextBox();
            this.txtCylSigmaH = new System.Windows.Forms.TextBox();
            this.txtCylEpsilon = new System.Windows.Forms.TextBox();
            this.txtCylDelta = new System.Windows.Forms.TextBox();
            this.txtCylPsi = new System.Windows.Forms.TextBox();
            this.cboCylContact = new System.Windows.Forms.ComboBox();
            this.btnCylCalc = new System.Windows.Forms.Button();
            this.txtCylResult = new System.Windows.Forms.TextBox();

            // Groove tab
            this.panelGroInput = new System.Windows.Forms.Panel();
            this.panelGroResult = new System.Windows.Forms.Panel();
            this.txtGroP1 = new System.Windows.Forms.TextBox();
            this.txtGron1 = new System.Windows.Forms.TextBox();
            this.txtGron2 = new System.Windows.Forms.TextBox();
            this.txtGroi = new System.Windows.Forms.TextBox();
            this.txtGroMu = new System.Windows.Forms.TextBox();
            this.txtGroKa = new System.Windows.Forms.TextBox();
            this.txtGroSigmaH = new System.Windows.Forms.TextBox();
            this.txtGroEpsilon = new System.Windows.Forms.TextBox();
            this.txtGroDelta = new System.Windows.Forms.TextBox();
            this.txtGroZ = new System.Windows.Forms.TextBox();
            this.txtGroBeta = new System.Windows.Forms.TextBox();
            this.cboGroContact = new System.Windows.Forms.ComboBox();
            this.btnGroCalc = new System.Windows.Forms.Button();
            this.txtGroResult = new System.Windows.Forms.TextBox();

            // EndFace tab
            this.panelEfInput = new System.Windows.Forms.Panel();
            this.panelEfResult = new System.Windows.Forms.Panel();
            this.txtEfP1 = new System.Windows.Forms.TextBox();
            this.txtEfn1 = new System.Windows.Forms.TextBox();
            this.txtEfn2 = new System.Windows.Forms.TextBox();
            this.txtEfi = new System.Windows.Forms.TextBox();
            this.txtEfMu = new System.Windows.Forms.TextBox();
            this.txtEfKa = new System.Windows.Forms.TextBox();
            this.txtEfSigmaH = new System.Windows.Forms.TextBox();
            this.txtEfEpsilon = new System.Windows.Forms.TextBox();
            this.txtEfPsi = new System.Windows.Forms.TextBox();
            this.btnEfCalc = new System.Windows.Forms.Button();
            this.txtEfResult = new System.Windows.Forms.TextBox();

            // Conical tab
            this.panelConInput = new System.Windows.Forms.Panel();
            this.panelConResult = new System.Windows.Forms.Panel();
            this.txtConP1 = new System.Windows.Forms.TextBox();
            this.txtConn1 = new System.Windows.Forms.TextBox();
            this.txtConn2 = new System.Windows.Forms.TextBox();
            this.txtConi = new System.Windows.Forms.TextBox();
            this.txtConMu = new System.Windows.Forms.TextBox();
            this.txtConKa = new System.Windows.Forms.TextBox();
            this.txtConSigmaH = new System.Windows.Forms.TextBox();
            this.txtConEpsilon = new System.Windows.Forms.TextBox();
            this.txtConPsi = new System.Windows.Forms.TextBox();
            this.txtConDelta1 = new System.Windows.Forms.TextBox();
            this.btnConCalc = new System.Windows.Forms.Button();
            this.txtConResult = new System.Windows.Forms.TextBox();

            this.tabMain.SuspendLayout();
            this.tabCylindrical.SuspendLayout();
            this.tabGroove.SuspendLayout();
            this.tabEndFace.SuspendLayout();
            this.tabConical.SuspendLayout();
            this.SuspendLayout();

            // tabMain
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Controls.Add(this.tabCylindrical);
            this.tabMain.Controls.Add(this.tabGroove);
            this.tabMain.Controls.Add(this.tabEndFace);
            this.tabMain.Controls.Add(this.tabConical);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Size = new System.Drawing.Size(900, 600);
            this.tabMain.TabIndex = 0;

            // ====== Cylindrical tab ======
            this.tabCylindrical.Text = "圆柱摩擦轮";
            this.tabCylindrical.Controls.Add(this.panelCylResult);
            this.tabCylindrical.Controls.Add(this.panelCylInput);

            this.panelCylInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCylInput.Width = 320;
            this.panelCylInput.AutoScroll = true;
            BuildInputPanel(this.panelCylInput, new System.Windows.Forms.Control[] {
                MakeLabel("传动功率 P1 (kW):", 10), this.txtCylP1,
                MakeLabel("主动轴转速 n1 (r/min):", 40), this.txtCyln1,
                MakeLabel("从动轴转速 n2 (r/min):", 70), this.txtCyln2,
                MakeLabel("传动比 i:", 100), this.txtCyli,
                MakeLabel("接触形式:", 130), this.cboCylContact,
                MakeLabel("摩擦系数 μ:", 160), this.txtCylMu,
                MakeLabel("许用接触应力 [σH] (MPa):", 190), this.txtCylSigmaH,
                MakeLabel("工况系数 Ka:", 220), this.txtCylKa,
                MakeLabel("滑动率 ε (%):", 250), this.txtCylEpsilon,
                MakeLabel("间隙 δ (mm):", 280), this.txtCylDelta,
                MakeLabel("宽度系数 ψ:", 310), this.txtCylPsi,
                this.btnCylCalc
            });
            this.txtCylP1.Location = new System.Drawing.Point(180, 10);
            this.txtCyln1.Location = new System.Drawing.Point(180, 40);
            this.txtCyln2.Location = new System.Drawing.Point(180, 70);
            this.txtCyli.Location = new System.Drawing.Point(180, 100);
            this.cboCylContact.Location = new System.Drawing.Point(180, 130);
            this.cboCylContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCylContact.Items.AddRange(new object[] { "外接触", "内接触" });
            this.cboCylContact.SelectedIndex = 0;
            this.txtCylMu.Location = new System.Drawing.Point(180, 160);
            this.txtCylSigmaH.Location = new System.Drawing.Point(180, 190);
            this.txtCylKa.Location = new System.Drawing.Point(180, 220);
            this.txtCylEpsilon.Location = new System.Drawing.Point(180, 250);
            this.txtCylDelta.Location = new System.Drawing.Point(180, 280);
            this.txtCylPsi.Location = new System.Drawing.Point(180, 310);
            this.btnCylCalc.Location = new System.Drawing.Point(180, 345);
            this.btnCylCalc.Text = "计算";
            this.btnCylCalc.Size = new System.Drawing.Size(100, 30);
            this.btnCylCalc.Click += new System.EventHandler(this.BtnCylCalc_Click);

            SetDefaults(new[] {
                this.txtCylP1, this.txtCyln1, this.txtCyln2, this.txtCyli,
                this.txtCylMu, this.txtCylSigmaH, this.txtCylKa,
                this.txtCylEpsilon, this.txtCylDelta, this.txtCylPsi
            });

            this.panelCylResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCylResult.Controls.Add(this.txtCylResult);
            this.txtCylResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCylResult.Multiline = true;
            this.txtCylResult.ReadOnly = true;
            this.txtCylResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCylResult.Font = new System.Drawing.Font("Consolas", 10F);

            // ====== Groove tab ======
            this.tabGroove.Text = "槽形摩擦轮";
            this.tabGroove.Controls.Add(this.panelGroResult);
            this.tabGroove.Controls.Add(this.panelGroInput);

            this.panelGroInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelGroInput.Width = 320;
            this.panelGroInput.AutoScroll = true;
            BuildInputPanel(this.panelGroInput, new System.Windows.Forms.Control[] {
                MakeLabel("传动功率 P1 (kW):", 10), this.txtGroP1,
                MakeLabel("主动轴转速 n1 (r/min):", 40), this.txtGron1,
                MakeLabel("从动轴转速 n2 (r/min):", 70), this.txtGron2,
                MakeLabel("传动比 i:", 100), this.txtGroi,
                MakeLabel("接触形式:", 130), this.cboGroContact,
                MakeLabel("摩擦系数 μ:", 160), this.txtGroMu,
                MakeLabel("许用接触应力 [σH] (MPa):", 190), this.txtGroSigmaH,
                MakeLabel("工况系数 Ka:", 220), this.txtGroKa,
                MakeLabel("滑动率 ε (%):", 250), this.txtGroEpsilon,
                MakeLabel("间隙 δ (mm):", 280), this.txtGroDelta,
                MakeLabel("沟槽数 z:", 310), this.txtGroZ,
                MakeLabel("楔角 β (°):", 340), this.txtGroBeta,
                this.btnGroCalc
            });
            this.txtGroP1.Location = new System.Drawing.Point(180, 10);
            this.txtGron1.Location = new System.Drawing.Point(180, 40);
            this.txtGron2.Location = new System.Drawing.Point(180, 70);
            this.txtGroi.Location = new System.Drawing.Point(180, 100);
            this.cboGroContact.Location = new System.Drawing.Point(180, 130);
            this.cboGroContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroContact.Items.AddRange(new object[] { "外接触", "内接触" });
            this.cboGroContact.SelectedIndex = 0;
            this.txtGroMu.Location = new System.Drawing.Point(180, 160);
            this.txtGroSigmaH.Location = new System.Drawing.Point(180, 190);
            this.txtGroKa.Location = new System.Drawing.Point(180, 220);
            this.txtGroEpsilon.Location = new System.Drawing.Point(180, 250);
            this.txtGroDelta.Location = new System.Drawing.Point(180, 280);
            this.txtGroZ.Location = new System.Drawing.Point(180, 310);
            this.txtGroBeta.Location = new System.Drawing.Point(180, 340);
            this.btnGroCalc.Location = new System.Drawing.Point(180, 375);
            this.btnGroCalc.Text = "计算";
            this.btnGroCalc.Size = new System.Drawing.Size(100, 30);
            this.btnGroCalc.Click += new System.EventHandler(this.BtnGroCalc_Click);

            SetDefaults(new[] {
                this.txtGroP1, this.txtGron1, this.txtGron2, this.txtGroi,
                this.txtGroMu, this.txtGroSigmaH, this.txtGroKa,
                this.txtGroEpsilon, this.txtGroDelta, this.txtGroZ, this.txtGroBeta
            });

            this.panelGroResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGroResult.Controls.Add(this.txtGroResult);
            this.txtGroResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGroResult.Multiline = true;
            this.txtGroResult.ReadOnly = true;
            this.txtGroResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGroResult.Font = new System.Drawing.Font("Consolas", 10F);

            // ====== EndFace tab ======
            this.tabEndFace.Text = "端面摩擦轮";
            this.tabEndFace.Controls.Add(this.panelEfResult);
            this.tabEndFace.Controls.Add(this.panelEfInput);

            this.panelEfInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEfInput.Width = 320;
            this.panelEfInput.AutoScroll = true;
            BuildInputPanel(this.panelEfInput, new System.Windows.Forms.Control[] {
                MakeLabel("传动功率 P1 (kW):", 10), this.txtEfP1,
                MakeLabel("主动轴转速 n1 (r/min):", 40), this.txtEfn1,
                MakeLabel("从动轴转速 n2 (r/min):", 70), this.txtEfn2,
                MakeLabel("传动比 i:", 100), this.txtEfi,
                MakeLabel("摩擦系数 μ:", 130), this.txtEfMu,
                MakeLabel("许用接触应力 [σH] (MPa):", 160), this.txtEfSigmaH,
                MakeLabel("工况系数 Ka:", 190), this.txtEfKa,
                MakeLabel("滑动率 ε (%):", 220), this.txtEfEpsilon,
                MakeLabel("宽度系数 ψ:", 250), this.txtEfPsi,
                this.btnEfCalc
            });
            this.txtEfP1.Location = new System.Drawing.Point(180, 10);
            this.txtEfn1.Location = new System.Drawing.Point(180, 40);
            this.txtEfn2.Location = new System.Drawing.Point(180, 70);
            this.txtEfi.Location = new System.Drawing.Point(180, 100);
            this.txtEfMu.Location = new System.Drawing.Point(180, 130);
            this.txtEfSigmaH.Location = new System.Drawing.Point(180, 160);
            this.txtEfKa.Location = new System.Drawing.Point(180, 190);
            this.txtEfEpsilon.Location = new System.Drawing.Point(180, 220);
            this.txtEfPsi.Location = new System.Drawing.Point(180, 250);
            this.btnEfCalc.Location = new System.Drawing.Point(180, 285);
            this.btnEfCalc.Text = "计算";
            this.btnEfCalc.Size = new System.Drawing.Size(100, 30);
            this.btnEfCalc.Click += new System.EventHandler(this.BtnEfCalc_Click);

            SetDefaults(new[] {
                this.txtEfP1, this.txtEfn1, this.txtEfn2, this.txtEfi,
                this.txtEfMu, this.txtEfSigmaH, this.txtEfKa,
                this.txtEfEpsilon, this.txtEfPsi
            });

            this.panelEfResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEfResult.Controls.Add(this.txtEfResult);
            this.txtEfResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEfResult.Multiline = true;
            this.txtEfResult.ReadOnly = true;
            this.txtEfResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEfResult.Font = new System.Drawing.Font("Consolas", 10F);

            // ====== Conical tab ======
            this.tabConical.Text = "圆锥摩擦轮";
            this.tabConical.Controls.Add(this.panelConResult);
            this.tabConical.Controls.Add(this.panelConInput);

            this.panelConInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelConInput.Width = 320;
            this.panelConInput.AutoScroll = true;
            BuildInputPanel(this.panelConInput, new System.Windows.Forms.Control[] {
                MakeLabel("传动功率 P1 (kW):", 10), this.txtConP1,
                MakeLabel("主动轴转速 n1 (r/min):", 40), this.txtConn1,
                MakeLabel("从动轴转速 n2 (r/min):", 70), this.txtConn2,
                MakeLabel("传动比 i:", 100), this.txtConi,
                MakeLabel("摩擦系数 μ:", 130), this.txtConMu,
                MakeLabel("许用接触应力 [σH] (MPa):", 160), this.txtConSigmaH,
                MakeLabel("工况系数 Ka:", 190), this.txtConKa,
                MakeLabel("滑动率 ε (%):", 220), this.txtConEpsilon,
                MakeLabel("宽度系数 ψ:", 250), this.txtConPsi,
                MakeLabel("主动轮锥角 δ1 (°):", 280), this.txtConDelta1,
                this.btnConCalc
            });
            this.txtConP1.Location = new System.Drawing.Point(180, 10);
            this.txtConn1.Location = new System.Drawing.Point(180, 40);
            this.txtConn2.Location = new System.Drawing.Point(180, 70);
            this.txtConi.Location = new System.Drawing.Point(180, 100);
            this.txtConMu.Location = new System.Drawing.Point(180, 130);
            this.txtConSigmaH.Location = new System.Drawing.Point(180, 160);
            this.txtConKa.Location = new System.Drawing.Point(180, 190);
            this.txtConEpsilon.Location = new System.Drawing.Point(180, 220);
            this.txtConPsi.Location = new System.Drawing.Point(180, 250);
            this.txtConDelta1.Location = new System.Drawing.Point(180, 280);
            this.btnConCalc.Location = new System.Drawing.Point(180, 315);
            this.btnConCalc.Text = "计算";
            this.btnConCalc.Size = new System.Drawing.Size(100, 30);
            this.btnConCalc.Click += new System.EventHandler(this.BtnConCalc_Click);

            SetDefaults(new[] {
                this.txtConP1, this.txtConn1, this.txtConn2, this.txtConi,
                this.txtConMu, this.txtConSigmaH, this.txtConKa,
                this.txtConEpsilon, this.txtConPsi, this.txtConDelta1
            });

            this.panelConResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConResult.Controls.Add(this.txtConResult);
            this.txtConResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConResult.Multiline = true;
            this.txtConResult.ReadOnly = true;
            this.txtConResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConResult.Font = new System.Drawing.Font("Consolas", 10F);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.tabMain);
            this.Name = "MainForm";
            this.Text = "摩擦轮设计";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.tabMain.ResumeLayout(false);
            this.tabCylindrical.ResumeLayout(false);
            this.tabGroove.ResumeLayout(false);
            this.tabEndFace.ResumeLayout(false);
            this.tabConical.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private static System.Windows.Forms.Label MakeLabel(string text, int y)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(10, y);
            lbl.Text = text;
            return lbl;
        }

        private static void BuildInputPanel(System.Windows.Forms.Panel panel, System.Windows.Forms.Control[] controls)
        {
            panel.Controls.AddRange(controls);
        }

        private static void SetDefaults(System.Windows.Forms.TextBox[] boxes)
        {
            foreach (var tb in boxes)
            {
                tb.Size = new System.Drawing.Size(100, 22);
                tb.Text = "";
            }
        }

        // Tabs
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabCylindrical;
        private System.Windows.Forms.TabPage tabGroove;
        private System.Windows.Forms.TabPage tabEndFace;
        private System.Windows.Forms.TabPage tabConical;

        // Cylindrical
        private System.Windows.Forms.Panel panelCylInput;
        private System.Windows.Forms.Panel panelCylResult;
        private System.Windows.Forms.TextBox txtCylP1, txtCyln1, txtCyln2, txtCyli;
        private System.Windows.Forms.TextBox txtCylMu, txtCylSigmaH, txtCylKa;
        private System.Windows.Forms.TextBox txtCylEpsilon, txtCylDelta, txtCylPsi;
        private System.Windows.Forms.ComboBox cboCylContact;
        private System.Windows.Forms.Button btnCylCalc;
        private System.Windows.Forms.TextBox txtCylResult;

        // Groove
        private System.Windows.Forms.Panel panelGroInput;
        private System.Windows.Forms.Panel panelGroResult;
        private System.Windows.Forms.TextBox txtGroP1, txtGron1, txtGron2, txtGroi;
        private System.Windows.Forms.TextBox txtGroMu, txtGroSigmaH, txtGroKa;
        private System.Windows.Forms.TextBox txtGroEpsilon, txtGroDelta;
        private System.Windows.Forms.TextBox txtGroZ, txtGroBeta;
        private System.Windows.Forms.ComboBox cboGroContact;
        private System.Windows.Forms.Button btnGroCalc;
        private System.Windows.Forms.TextBox txtGroResult;

        // EndFace
        private System.Windows.Forms.Panel panelEfInput;
        private System.Windows.Forms.Panel panelEfResult;
        private System.Windows.Forms.TextBox txtEfP1, txtEfn1, txtEfn2, txtEfi;
        private System.Windows.Forms.TextBox txtEfMu, txtEfSigmaH, txtEfKa;
        private System.Windows.Forms.TextBox txtEfEpsilon, txtEfPsi;
        private System.Windows.Forms.Button btnEfCalc;
        private System.Windows.Forms.TextBox txtEfResult;

        // Conical
        private System.Windows.Forms.Panel panelConInput;
        private System.Windows.Forms.Panel panelConResult;
        private System.Windows.Forms.TextBox txtConP1, txtConn1, txtConn2, txtConi;
        private System.Windows.Forms.TextBox txtConMu, txtConSigmaH, txtConKa;
        private System.Windows.Forms.TextBox txtConEpsilon, txtConPsi, txtConDelta1;
        private System.Windows.Forms.Button btnConCalc;
        private System.Windows.Forms.TextBox txtConResult;
    }
}
