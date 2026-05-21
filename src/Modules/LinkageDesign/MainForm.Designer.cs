namespace LinkageDesign
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

            // Tab 1: 曲柄摇杆机构设计
            this.tabCrankRocker = new System.Windows.Forms.TabPage();
            this.panelCRInput = new System.Windows.Forms.Panel();
            this.panelCRResult = new System.Windows.Forms.Panel();
            this.lblCRPhi12 = MakeLabel("曲柄转角 φ12 (°):", 15);
            this.txtCRPhi12 = MakeTextBox(180);
            this.lblCRPsi12 = MakeLabel("摇杆摆角 ψ12 (°):", 45);
            this.txtCRPsi12 = MakeTextBox(40);
            this.lblCRGammaMin = MakeLabel("最小传动角 γmin (°):", 75);
            this.txtCRGammaMin = MakeTextBox(40);
            this.lblCRK = MakeLabel("行程速比系数 K:", 105);
            this.txtCRK = MakeTextBox(1.2);
            this.btnCRCalc = new System.Windows.Forms.Button();
            this.txtCRResult = MakeResultBox();

            // Tab 2: 偏置曲柄滑块机构设计
            this.tabCrankSlider = new System.Windows.Forms.TabPage();
            this.panelCSInput = new System.Windows.Forms.Panel();
            this.panelCSResult = new System.Windows.Forms.Panel();
            this.lblCSPhi12 = MakeLabel("曲柄转角 φ12 (°):", 15);
            this.txtCSPhi12 = MakeTextBox(180);
            this.lblCSGammaMin = MakeLabel("最小传动角 γmin (°):", 45);
            this.txtCSGammaMin = MakeTextBox(40);
            this.lblCSStroke = MakeLabel("滑块行程 s (mm):", 75);
            this.txtCSStroke = MakeTextBox(100);
            this.btnCSCalc = new System.Windows.Forms.Button();
            this.txtCSResult = MakeResultBox();

            // Tab 3: 双曲柄机构设计
            this.tabDoubleCrank = new System.Windows.Forms.TabPage();
            this.panelDCInput = new System.Windows.Forms.Panel();
            this.panelDCResult = new System.Windows.Forms.Panel();
            this.lblDCPsi = MakeLabel("输出杆转角 ψ (°):", 15);
            this.txtDCPsi = MakeTextBox(60);
            this.lblDCGammaMin = MakeLabel("最小传动角 γmin (°):", 45);
            this.txtDCGammaMin = MakeTextBox(30);
            this.btnDCCalc = new System.Windows.Forms.Button();
            this.txtDCResult = MakeResultBox();

            // Tab 4: 铰链四杆位置设计
            this.tabFourBar = new System.Windows.Forms.TabPage();
            this.panelFBInput = new System.Windows.Forms.Panel();
            this.panelFBResult = new System.Windows.Forms.Panel();
            this.lblFBPhi1 = MakeLabel("φ1 (°):", 15);
            this.txtFBPhi1 = MakeTextBox(0);
            this.lblFBPsi1 = MakeLabel("ψ1 (°):", 45);
            this.txtFBPsi1 = MakeTextBox(0);
            this.lblFBPhi2 = MakeLabel("φ2 (°):", 75);
            this.txtFBPhi2 = MakeTextBox(30);
            this.lblFBPsi2 = MakeLabel("ψ2 (°):", 105);
            this.txtFBPsi2 = MakeTextBox(20);
            this.lblFBPhi3 = MakeLabel("φ3 (°):", 135);
            this.txtFBPhi3 = MakeTextBox(60);
            this.lblFBPsi3 = MakeLabel("ψ3 (°):", 165);
            this.txtFBPsi3 = MakeTextBox(40);
            this.lblFBCrankA = MakeLabel("曲柄长度 a (mm):", 195);
            this.txtFBCrankA = MakeTextBox(50);
            this.btnFBCalc = new System.Windows.Forms.Button();
            this.txtFBResult = MakeResultBox();

            // Tab 5: 曲柄滑块位置设计
            this.tabCSPosition = new System.Windows.Forms.TabPage();
            this.panelCSPInput = new System.Windows.Forms.Panel();
            this.panelCSPResult = new System.Windows.Forms.Panel();
            this.lblCSPPhi1 = MakeLabel("φ1 (°):", 15);
            this.txtCSPPhi1 = MakeTextBox(0);
            this.lblCSPS1 = MakeLabel("s1 (mm):", 45);
            this.txtCSPS1 = MakeTextBox(0);
            this.lblCSPPhi2 = MakeLabel("φ2 (°):", 75);
            this.txtCSPPhi2 = MakeTextBox(45);
            this.lblCSPS2 = MakeLabel("s2 (mm):", 105);
            this.txtCSPS2 = MakeTextBox(50);
            this.lblCSPPhi3 = MakeLabel("φ3 (°):", 135);
            this.txtCSPPhi3 = MakeTextBox(90);
            this.lblCSPS3 = MakeLabel("s3 (mm):", 165);
            this.txtCSPS3 = MakeTextBox(80);
            this.btnCSPCalc = new System.Windows.Forms.Button();
            this.txtCSPResult = MakeResultBox();

            // Tab 6: 运动分析
            this.tabKinematics = new System.Windows.Forms.TabPage();
            this.panelKMInput = new System.Windows.Forms.Panel();
            this.panelKMResult = new System.Windows.Forms.Panel();
            this.lblKMa = MakeLabel("曲柄 a (mm):", 15);
            this.txtKMa = MakeTextBox(50);
            this.lblKMb = MakeLabel("连杆 b (mm):", 45);
            this.txtKMb = MakeTextBox(120);
            this.lblKMc = MakeLabel("摇杆 c (mm):", 75);
            this.txtKMc = MakeTextBox(100);
            this.lblKMd = MakeLabel("机架 d (mm):", 105);
            this.txtKMd = MakeTextBox(150);
            this.lblKMOmega = MakeLabel("曲柄角速度 ω (rad/s):", 135);
            this.txtKMOmega = MakeTextBox(10);
            this.lblKMTheta = MakeLabel("曲柄转角 θ (°):", 165);
            this.txtKMTheta = MakeTextBox(30);
            this.btnKMCalc = new System.Windows.Forms.Button();
            this.txtKMResult = MakeResultBox();

            this.tabMain.SuspendLayout();
            this.tabCrankRocker.SuspendLayout();
            this.tabCrankSlider.SuspendLayout();
            this.tabDoubleCrank.SuspendLayout();
            this.tabFourBar.SuspendLayout();
            this.tabCSPosition.SuspendLayout();
            this.tabKinematics.SuspendLayout();
            this.SuspendLayout();

            // tabMain
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Controls.Add(this.tabCrankRocker);
            this.tabMain.Controls.Add(this.tabCrankSlider);
            this.tabMain.Controls.Add(this.tabDoubleCrank);
            this.tabMain.Controls.Add(this.tabFourBar);
            this.tabMain.Controls.Add(this.tabCSPosition);
            this.tabMain.Controls.Add(this.tabKinematics);

            // ====== Tab 1: 曲柄摇杆 ======
            this.tabCrankRocker.Text = "曲柄摇杆";
            this.tabCrankRocker.Controls.Add(this.panelCRResult);
            this.tabCrankRocker.Controls.Add(this.panelCRInput);
            this.panelCRInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCRInput.Width = 300;
            this.panelCRInput.AutoScroll = true;
            this.panelCRResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCRResult.Controls.Add(this.txtCRResult);
            SetupPanelInput(this.panelCRInput,
                new System.Windows.Forms.Control[] {
                    this.lblCRPhi12, this.txtCRPhi12,
                    this.lblCRPsi12, this.txtCRPsi12,
                    this.lblCRGammaMin, this.txtCRGammaMin,
                    this.lblCRK, this.txtCRK,
                    this.btnCRCalc
                });
            this.btnCRCalc.Text = "计算";
            this.btnCRCalc.Location = new System.Drawing.Point(180, 140);
            this.btnCRCalc.Size = new System.Drawing.Size(90, 28);
            this.btnCRCalc.Click += new System.EventHandler(this.BtnCRCalc_Click);

            // ====== Tab 2: 曲柄滑块 ======
            this.tabCrankSlider.Text = "曲柄滑块";
            this.tabCrankSlider.Controls.Add(this.panelCSResult);
            this.tabCrankSlider.Controls.Add(this.panelCSInput);
            this.panelCSInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCSInput.Width = 300;
            this.panelCSInput.AutoScroll = true;
            this.panelCSResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCSResult.Controls.Add(this.txtCSResult);
            SetupPanelInput(this.panelCSInput,
                new System.Windows.Forms.Control[] {
                    this.lblCSPhi12, this.txtCSPhi12,
                    this.lblCSGammaMin, this.txtCSGammaMin,
                    this.lblCSStroke, this.txtCSStroke,
                    this.btnCSCalc
                });
            this.btnCSCalc.Text = "计算";
            this.btnCSCalc.Location = new System.Drawing.Point(180, 110);
            this.btnCSCalc.Size = new System.Drawing.Size(90, 28);
            this.btnCSCalc.Click += new System.EventHandler(this.BtnCSCalc_Click);

            // ====== Tab 3: 双曲柄 ======
            this.tabDoubleCrank.Text = "双曲柄";
            this.tabDoubleCrank.Controls.Add(this.panelDCResult);
            this.tabDoubleCrank.Controls.Add(this.panelDCInput);
            this.panelDCInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDCInput.Width = 300;
            this.panelDCInput.AutoScroll = true;
            this.panelDCResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDCResult.Controls.Add(this.txtDCResult);
            SetupPanelInput(this.panelDCInput,
                new System.Windows.Forms.Control[] {
                    this.lblDCPsi, this.txtDCPsi,
                    this.lblDCGammaMin, this.txtDCGammaMin,
                    this.btnDCCalc
                });
            this.btnDCCalc.Text = "计算";
            this.btnDCCalc.Location = new System.Drawing.Point(180, 80);
            this.btnDCCalc.Size = new System.Drawing.Size(90, 28);
            this.btnDCCalc.Click += new System.EventHandler(this.BtnDCCalc_Click);

            // ====== Tab 4: 铰链四杆 ======
            this.tabFourBar.Text = "铰链四杆位置";
            this.tabFourBar.Controls.Add(this.panelFBResult);
            this.tabFourBar.Controls.Add(this.panelFBInput);
            this.panelFBInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFBInput.Width = 300;
            this.panelFBInput.AutoScroll = true;
            this.panelFBResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFBResult.Controls.Add(this.txtFBResult);
            SetupPanelInput(this.panelFBInput,
                new System.Windows.Forms.Control[] {
                    this.lblFBPhi1, this.txtFBPhi1,
                    this.lblFBPsi1, this.txtFBPsi1,
                    this.lblFBPhi2, this.txtFBPhi2,
                    this.lblFBPsi2, this.txtFBPsi2,
                    this.lblFBPhi3, this.txtFBPhi3,
                    this.lblFBPsi3, this.txtFBPsi3,
                    this.lblFBCrankA, this.txtFBCrankA,
                    this.btnFBCalc
                });
            this.btnFBCalc.Text = "计算";
            this.btnFBCalc.Location = new System.Drawing.Point(180, 230);
            this.btnFBCalc.Size = new System.Drawing.Size(90, 28);
            this.btnFBCalc.Click += new System.EventHandler(this.BtnFBCalc_Click);

            // ====== Tab 5: 曲柄滑块位置 ======
            this.tabCSPosition.Text = "滑块位置设计";
            this.tabCSPosition.Controls.Add(this.panelCSPResult);
            this.tabCSPosition.Controls.Add(this.panelCSPInput);
            this.panelCSPInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCSPInput.Width = 300;
            this.panelCSPInput.AutoScroll = true;
            this.panelCSPResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCSPResult.Controls.Add(this.txtCSPResult);
            SetupPanelInput(this.panelCSPInput,
                new System.Windows.Forms.Control[] {
                    this.lblCSPPhi1, this.txtCSPPhi1,
                    this.lblCSPS1, this.txtCSPS1,
                    this.lblCSPPhi2, this.txtCSPPhi2,
                    this.lblCSPS2, this.txtCSPS2,
                    this.lblCSPPhi3, this.txtCSPPhi3,
                    this.lblCSPS3, this.txtCSPS3,
                    this.btnCSPCalc
                });
            this.btnCSPCalc.Text = "计算";
            this.btnCSPCalc.Location = new System.Drawing.Point(180, 200);
            this.btnCSPCalc.Size = new System.Drawing.Size(90, 28);
            this.btnCSPCalc.Click += new System.EventHandler(this.BtnCSPCalc_Click);

            // ====== Tab 6: 运动分析 ======
            this.tabKinematics.Text = "运动分析";
            this.tabKinematics.Controls.Add(this.panelKMResult);
            this.tabKinematics.Controls.Add(this.panelKMInput);
            this.panelKMInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelKMInput.Width = 300;
            this.panelKMInput.AutoScroll = true;
            this.panelKMResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKMResult.Controls.Add(this.txtKMResult);
            SetupPanelInput(this.panelKMInput,
                new System.Windows.Forms.Control[] {
                    this.lblKMa, this.txtKMa,
                    this.lblKMb, this.txtKMb,
                    this.lblKMc, this.txtKMc,
                    this.lblKMd, this.txtKMd,
                    this.lblKMOmega, this.txtKMOmega,
                    this.lblKMTheta, this.txtKMTheta,
                    this.btnKMCalc
                });
            this.btnKMCalc.Text = "计算";
            this.btnKMCalc.Location = new System.Drawing.Point(180, 200);
            this.btnKMCalc.Size = new System.Drawing.Size(90, 28);
            this.btnKMCalc.Click += new System.EventHandler(this.BtnKMCalc_Click);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.tabMain);
            this.Name = "MainForm";
            this.Text = "平面连杆机构设计";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.tabMain.ResumeLayout(false);
            this.tabCrankRocker.ResumeLayout(false);
            this.tabCrankSlider.ResumeLayout(false);
            this.tabDoubleCrank.ResumeLayout(false);
            this.tabFourBar.ResumeLayout(false);
            this.tabCSPosition.ResumeLayout(false);
            this.tabKinematics.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private static System.Windows.Forms.Label MakeLabel(string text, int y)
        {
            return new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(10, y),
                Text = text
            };
        }

        private static System.Windows.Forms.TextBox MakeTextBox(object defaultVal)
        {
            var tb = new System.Windows.Forms.TextBox
            {
                Size = new System.Drawing.Size(90, 22),
                Text = defaultVal != null ? defaultVal.ToString() : ""
            };
            return tb;
        }

        private static System.Windows.Forms.TextBox MakeResultBox()
        {
            return new System.Windows.Forms.TextBox
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = System.Windows.Forms.ScrollBars.Vertical,
                Font = new System.Drawing.Font("Consolas", 10F)
            };
        }

        private static void SetupPanelInput(System.Windows.Forms.Panel panel, System.Windows.Forms.Control[] controls)
        {
            foreach (var c in controls)
            {
                if (c is System.Windows.Forms.TextBox tb)
                    tb.Location = new System.Drawing.Point(180, c.Location.Y);
            }
            panel.Controls.AddRange(controls);
        }

        private System.Windows.Forms.TabControl tabMain;

        // Tab 1: 曲柄摇杆
        private System.Windows.Forms.TabPage tabCrankRocker;
        private System.Windows.Forms.Panel panelCRInput, panelCRResult;
        private System.Windows.Forms.Label lblCRPhi12, lblCRPsi12, lblCRGammaMin, lblCRK;
        private System.Windows.Forms.TextBox txtCRPhi12, txtCRPsi12, txtCRGammaMin, txtCRK;
        private System.Windows.Forms.Button btnCRCalc;
        private System.Windows.Forms.TextBox txtCRResult;

        // Tab 2: 曲柄滑块
        private System.Windows.Forms.TabPage tabCrankSlider;
        private System.Windows.Forms.Panel panelCSInput, panelCSResult;
        private System.Windows.Forms.Label lblCSPhi12, lblCSGammaMin, lblCSStroke;
        private System.Windows.Forms.TextBox txtCSPhi12, txtCSGammaMin, txtCSStroke;
        private System.Windows.Forms.Button btnCSCalc;
        private System.Windows.Forms.TextBox txtCSResult;

        // Tab 3: 双曲柄
        private System.Windows.Forms.TabPage tabDoubleCrank;
        private System.Windows.Forms.Panel panelDCInput, panelDCResult;
        private System.Windows.Forms.Label lblDCPsi, lblDCGammaMin;
        private System.Windows.Forms.TextBox txtDCPsi, txtDCGammaMin;
        private System.Windows.Forms.Button btnDCCalc;
        private System.Windows.Forms.TextBox txtDCResult;

        // Tab 4: 铰链四杆位置
        private System.Windows.Forms.TabPage tabFourBar;
        private System.Windows.Forms.Panel panelFBInput, panelFBResult;
        private System.Windows.Forms.Label lblFBPhi1, lblFBPsi1, lblFBPhi2, lblFBPsi2, lblFBPhi3, lblFBPsi3, lblFBCrankA;
        private System.Windows.Forms.TextBox txtFBPhi1, txtFBPsi1, txtFBPhi2, txtFBPsi2, txtFBPhi3, txtFBPsi3, txtFBCrankA;
        private System.Windows.Forms.Button btnFBCalc;
        private System.Windows.Forms.TextBox txtFBResult;

        // Tab 5: 曲柄滑块位置
        private System.Windows.Forms.TabPage tabCSPosition;
        private System.Windows.Forms.Panel panelCSPInput, panelCSPResult;
        private System.Windows.Forms.Label lblCSPPhi1, lblCSPS1, lblCSPPhi2, lblCSPS2, lblCSPPhi3, lblCSPS3;
        private System.Windows.Forms.TextBox txtCSPPhi1, txtCSPS1, txtCSPPhi2, txtCSPS2, txtCSPPhi3, txtCSPS3;
        private System.Windows.Forms.Button btnCSPCalc;
        private System.Windows.Forms.TextBox txtCSPResult;

        // Tab 6: 运动分析
        private System.Windows.Forms.TabPage tabKinematics;
        private System.Windows.Forms.Panel panelKMInput, panelKMResult;
        private System.Windows.Forms.Label lblKMa, lblKMb, lblKMc, lblKMd, lblKMOmega, lblKMTheta;
        private System.Windows.Forms.TextBox txtKMa, txtKMb, txtKMc, txtKMd, txtKMOmega, txtKMTheta;
        private System.Windows.Forms.Button btnKMCalc;
        private System.Windows.Forms.TextBox txtKMResult;
    }
}
