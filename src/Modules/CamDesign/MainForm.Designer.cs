namespace CamDesign
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
            this.SuspendLayout();

            // ============================================
            // 主窗体设置
            // ============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 750);
            this.Name = "MainForm";
            this.Text = "迈迪凸轮设计系统";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);

            // ============================================
            // TabControl - 主选项卡控件
            // ============================================
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Size = new System.Drawing.Size(1076, 726);
            this.tabMain.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // ============================================
            // TabPage 1: 凸轮轮廓设计
            // ============================================
            this.tabProfileDesign = new System.Windows.Forms.TabPage();
            this.tabProfileDesign.Text = "凸轮轮廓设计";
            this.tabProfileDesign.Padding = new System.Windows.Forms.Padding(3);

            // 凸轮类型选择
            this.lblCamType = new System.Windows.Forms.Label();
            this.lblCamType.Text = "凸轮类型：";
            this.lblCamType.Location = new System.Drawing.Point(15, 20);
            this.lblCamType.AutoSize = true;

            this.cmbCamType = new System.Windows.Forms.ComboBox();
            this.cmbCamType.Location = new System.Drawing.Point(90, 17);
            this.cmbCamType.Size = new System.Drawing.Size(200, 20);
            this.cmbCamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // 从动件类型
            this.lblFollowerType = new System.Windows.Forms.Label();
            this.lblFollowerType.Text = "从动件类型：";
            this.lblFollowerType.Location = new System.Drawing.Point(310, 20);
            this.lblFollowerType.AutoSize = true;

            this.cmbFollowerType = new System.Windows.Forms.ComboBox();
            this.cmbFollowerType.Location = new System.Drawing.Point(400, 17);
            this.cmbFollowerType.Size = new System.Drawing.Size(150, 20);
            this.cmbFollowerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // 常用凸轮模板
            this.lblTemplate = new System.Windows.Forms.Label();
            this.lblTemplate.Text = "常用凸轮：";
            this.lblTemplate.Location = new System.Drawing.Point(570, 20);
            this.lblTemplate.AutoSize = true;

            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.cmbTemplate.Location = new System.Drawing.Point(650, 17);
            this.cmbTemplate.Size = new System.Drawing.Size(150, 20);
            this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplate.SelectedIndexChanged += new System.EventHandler(this.cmbTemplate_SelectedIndexChanged);

            // 参数输入区
            this.groupParameters = new System.Windows.Forms.GroupBox();
            this.groupParameters.Text = "凸轮参数";
            this.groupParameters.Location = new System.Drawing.Point(15, 50);
            this.groupParameters.Size = new System.Drawing.Size(350, 180);

            // 基圆半径
            this.lblBaseRadius = new System.Windows.Forms.Label();
            this.lblBaseRadius.Text = "基圆半径：        mm";
            this.lblBaseRadius.Location = new System.Drawing.Point(15, 30);
            this.lblBaseRadius.AutoSize = true;

            this.txtBaseRadius = new System.Windows.Forms.TextBox();
            this.txtBaseRadius.Location = new System.Drawing.Point(90, 27);
            this.txtBaseRadius.Size = new System.Drawing.Size(80, 20);

            // 偏距
            this.lblOffset = new System.Windows.Forms.Label();
            this.lblOffset.Text = "偏距：        mm";
            this.lblOffset.Location = new System.Drawing.Point(15, 60);
            this.lblOffset.AutoSize = true;

            this.txtOffset = new System.Windows.Forms.TextBox();
            this.txtOffset.Location = new System.Drawing.Point(90, 57);
            this.txtOffset.Size = new System.Drawing.Size(80, 20);

            // 滚子半径
            this.lblRollerRadius = new System.Windows.Forms.Label();
            this.lblRollerRadius.Text = "滚子半径：        mm";
            this.lblRollerRadius.Location = new System.Drawing.Point(15, 90);
            this.lblRollerRadius.AutoSize = true;

            this.txtRollerRadius = new System.Windows.Forms.TextBox();
            this.txtRollerRadius.Location = new System.Drawing.Point(90, 87);
            this.txtRollerRadius.Size = new System.Drawing.Size(80, 20);

            // 角速度
            this.lblAngularVelocity = new System.Windows.Forms.Label();
            this.lblAngularVelocity.Text = "角速度：        rad/s";
            this.lblAngularVelocity.Location = new System.Drawing.Point(15, 120);
            this.lblAngularVelocity.AutoSize = true;

            this.txtAngularVelocity = new System.Windows.Forms.TextBox();
            this.txtAngularVelocity.Location = new System.Drawing.Point(90, 117);
            this.txtAngularVelocity.Size = new System.Drawing.Size(80, 20);

            // 许用压力角
            this.lblAllowPressureAngle = new System.Windows.Forms.Label();
            this.lblAllowPressureAngle.Text = "许用压力角：        °";
            this.lblAllowPressureAngle.Location = new System.Drawing.Point(15, 150);
            this.lblAllowPressureAngle.AutoSize = true;

            this.txtAllowPressureAngle = new System.Windows.Forms.TextBox();
            this.txtAllowPressureAngle.Location = new System.Drawing.Point(105, 147);
            this.txtAllowPressureAngle.Size = new System.Drawing.Size(65, 20);

            this.groupParameters.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBaseRadius, this.txtBaseRadius,
                this.lblOffset, this.txtOffset,
                this.lblRollerRadius, this.txtRollerRadius,
                this.lblAngularVelocity, this.txtAngularVelocity,
                this.lblAllowPressureAngle, this.txtAllowPressureAngle
            });

            // 步骤列表
            this.groupSteps = new System.Windows.Forms.GroupBox();
            this.groupSteps.Text = "运动阶段";
            this.groupSteps.Location = new System.Drawing.Point(15, 240);
            this.groupSteps.Size = new System.Drawing.Size(600, 200);

            this.dgvSteps = new System.Windows.Forms.DataGridView();
            this.dgvSteps.Location = new System.Drawing.Point(10, 20);
            this.dgvSteps.Size = new System.Drawing.Size(580, 140);
            this.dgvSteps.AllowUserToAddRows = false;
            this.dgvSteps.AllowUserToDeleteRows = false;
            this.dgvSteps.ReadOnly = true;
            this.dgvSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSteps.Columns.Add("colName", "阶段名称");
            this.dgvSteps.Columns.Add("colStartAngle", "起始角度(°)");
            this.dgvSteps.Columns.Add("colEndAngle", "终止角度(°)");
            this.dgvSteps.Columns.Add("colHStart", "起始位移(mm)");
            this.dgvSteps.Columns.Add("colHEnd", "终止位移(mm)");
            this.dgvSteps.Columns.Add("colMotionLaw", "运动规律");
            this.dgvSteps.Columns[0].Width = 80;
            this.dgvSteps.Columns[1].Width = 85;
            this.dgvSteps.Columns[2].Width = 85;
            this.dgvSteps.Columns[3].Width = 90;
            this.dgvSteps.Columns[4].Width = 90;
            this.dgvSteps.Columns[5].Width = 120;

            this.btnAddStep = new System.Windows.Forms.Button();
            this.btnAddStep.Text = "添加阶段";
            this.btnAddStep.Location = new System.Drawing.Point(10, 168);
            this.btnAddStep.Size = new System.Drawing.Size(80, 25);
            this.btnAddStep.Click += new System.EventHandler(this.btnAddStep_Click);

            this.btnDeleteStep = new System.Windows.Forms.Button();
            this.btnDeleteStep.Text = "删除阶段";
            this.btnDeleteStep.Location = new System.Drawing.Point(100, 168);
            this.btnDeleteStep.Size = new System.Drawing.Size(80, 25);
            this.btnDeleteStep.Click += new System.EventHandler(this.btnDeleteStep_Click);

            this.groupSteps.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvSteps, this.btnAddStep, this.btnDeleteStep
            });

            // 凸轮轮廓预览
            this.groupProfilePreview = new System.Windows.Forms.GroupBox();
            this.groupProfilePreview.Text = "凸轮轮廓预览";
            this.groupProfilePreview.Location = new System.Drawing.Point(380, 50);
            this.groupProfilePreview.Size = new System.Drawing.Size(300, 180);

            this.picCamProfile = new System.Windows.Forms.PictureBox();
            this.picCamProfile.Location = new System.Drawing.Point(10, 20);
            this.picCamProfile.Size = new System.Drawing.Size(280, 150);
            this.picCamProfile.BackColor = System.Drawing.Color.White;
            this.picCamProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCamProfile.Paint += new System.Windows.Forms.PaintEventHandler(this.picCamProfile_Paint);

            this.groupProfilePreview.Controls.Add(this.picCamProfile);

            // 计算按钮
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnCalculate.Text = "计算";
            this.btnCalculate.Location = new System.Drawing.Point(700, 250);
            this.btnCalculate.Size = new System.Drawing.Size(80, 30);
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            this.btnOK = new System.Windows.Forms.Button();
            this.btnOK.Text = "确定";
            this.btnOK.Location = new System.Drawing.Point(700, 290);
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportExcel.Text = "导出数据";
            this.btnExportExcel.Location = new System.Drawing.Point(700, 330);
            this.btnExportExcel.Size = new System.Drawing.Size(80, 30);
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            // 结果显示区
            this.groupResults = new System.Windows.Forms.GroupBox();
            this.groupResults.Text = "计算结果";
            this.groupResults.Location = new System.Drawing.Point(15, 450);
            this.groupResults.Size = new System.Drawing.Size(1040, 240);

            // 位移曲线
            this.groupDisplacement = new System.Windows.Forms.GroupBox();
            this.groupDisplacement.Text = "从动件运动规律 - 位移曲线";
            this.groupDisplacement.Location = new System.Drawing.Point(10, 20);
            this.groupDisplacement.Size = new System.Drawing.Size(330, 210);

            this.picDisplacement = new System.Windows.Forms.PictureBox();
            this.picDisplacement.Location = new System.Drawing.Point(5, 15);
            this.picDisplacement.Size = new System.Drawing.Size(320, 190);
            this.picDisplacement.BackColor = System.Drawing.Color.White;
            this.picDisplacement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDisplacement.Paint += new System.Windows.Forms.PaintEventHandler(this.picDisplacement_Paint);

            this.groupDisplacement.Controls.Add(this.picDisplacement);

            // 速度曲线
            this.groupVelocity = new System.Windows.Forms.GroupBox();
            this.groupVelocity.Text = "速度曲线";
            this.groupVelocity.Location = new System.Drawing.Point(350, 20);
            this.groupVelocity.Size = new System.Drawing.Size(330, 210);

            this.picVelocity = new System.Windows.Forms.PictureBox();
            this.picVelocity.Location = new System.Drawing.Point(5, 15);
            this.picVelocity.Size = new System.Drawing.Size(320, 190);
            this.picVelocity.BackColor = System.Drawing.Color.White;
            this.picVelocity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVelocity.Paint += new System.Windows.Forms.PaintEventHandler(this.picVelocity_Paint);

            this.groupVelocity.Controls.Add(this.picVelocity);

            // 加速度曲线
            this.groupAcceleration = new System.Windows.Forms.GroupBox();
            this.groupAcceleration.Text = "加速度曲线";
            this.groupAcceleration.Location = new System.Drawing.Point(690, 20);
            this.groupAcceleration.Size = new System.Drawing.Size(340, 210);

            this.picAcceleration = new System.Windows.Forms.PictureBox();
            this.picAcceleration.Location = new System.Drawing.Point(5, 15);
            this.picAcceleration.Size = new System.Drawing.Size(330, 190);
            this.picAcceleration.BackColor = System.Drawing.Color.White;
            this.picAcceleration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAcceleration.Paint += new System.Windows.Forms.PaintEventHandler(this.picAcceleration_Paint);

            this.groupAcceleration.Controls.Add(this.picAcceleration);

            this.groupResults.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.groupDisplacement, this.groupVelocity, this.groupAcceleration
            });

            // 结果标签
            this.lblResultMaxPressureAngle = CreateResultLabel("最大压力角：--", 700, 380);
            this.lblResultMaxDisplacement = CreateResultLabel("最大位移：--", 700, 400);
            this.lblResultMaxVelocity = CreateResultLabel("最大速度：--", 700, 420);
            this.lblResultMaxAcceleration = CreateResultLabel("最大加速度：--", 700, 440);
            this.lblResultMinBaseRadius = CreateResultLabel("最小基圆半径：--", 850, 380);
            this.lblPressureAngleCheck = CreateResultLabel("压力角校核：--", 850, 400);
            this.lblBaseRadiusCheck = CreateResultLabel("基圆半径校核：--", 850, 420);

            // 添加控件到 tabProfileDesign
            this.tabProfileDesign.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCamType, this.cmbCamType,
                this.lblFollowerType, this.cmbFollowerType,
                this.lblTemplate, this.cmbTemplate,
                this.groupParameters,
                this.groupSteps,
                this.groupProfilePreview,
                this.btnCalculate, this.btnOK, this.btnExportExcel,
                this.groupResults,
                this.lblResultMaxPressureAngle,
                this.lblResultMaxDisplacement,
                this.lblResultMaxVelocity,
                this.lblResultMaxAcceleration,
                this.lblResultMinBaseRadius,
                this.lblPressureAngleCheck,
                this.lblBaseRadiusCheck
            });

            // ============================================
            // TabPage 2: 运动规律选择
            // ============================================
            this.tabMotionLaw = new System.Windows.Forms.TabPage();
            this.tabMotionLaw.Text = "运动规律选择";
            this.tabMotionLaw.Padding = new System.Windows.Forms.Padding(3);

            this.groupMotionLawSelect = new System.Windows.Forms.GroupBox();
            this.groupMotionLawSelect.Text = "从动件运动规律";
            this.groupMotionLawSelect.Location = new System.Drawing.Point(15, 15);
            this.groupMotionLawSelect.Size = new System.Drawing.Size(400, 400);

            this.cmbMotionLaw = new System.Windows.Forms.ComboBox();
            this.cmbMotionLaw.Location = new System.Drawing.Point(15, 30);
            this.cmbMotionLaw.Size = new System.Drawing.Size(200, 20);
            this.cmbMotionLaw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblMotionLawDesc = new System.Windows.Forms.Label();
            this.lblMotionLawDesc.Text = "运动规律说明：\n\n" +
                "等速运动：从动件做匀速直线运动，起止点有刚性冲击。\n\n" +
                "等加速等减速：加速度为常数，起止点有柔性冲击。\n\n" +
                "余弦加速度（简谐）：加速度按余弦规律变化，起止点有柔性冲击。\n\n" +
                "正弦加速度（摆线）：加速度按正弦规律变化，无冲击。\n\n" +
                "五次多项式：速度、加速度在起止点均为零，无冲击。\n\n" +
                "改进梯形加速度：组合运动规律，改善冲击特性。\n\n" +
                "改进正弦加速度：改善正弦加速度的峰值特性。";
            this.lblMotionLawDesc.Location = new System.Drawing.Point(15, 60);
            this.lblMotionLawDesc.Size = new System.Drawing.Size(370, 330);

            this.groupMotionLawSelect.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.cmbMotionLaw, this.lblMotionLawDesc
            });

            // 运动规律对比图
            this.groupMotionLawCompare = new System.Windows.Forms.GroupBox();
            this.groupMotionLawCompare.Text = "运动规律对比";
            this.groupMotionLawCompare.Location = new System.Drawing.Point(430, 15);
            this.groupMotionLawCompare.Size = new System.Drawing.Size(620, 400);

            this.picMotionLawCompare = new System.Windows.Forms.PictureBox();
            this.picMotionLawCompare.Location = new System.Drawing.Point(10, 20);
            this.picMotionLawCompare.Size = new System.Drawing.Size(600, 370);
            this.picMotionLawCompare.BackColor = System.Drawing.Color.White;
            this.picMotionLawCompare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.groupMotionLawCompare.Controls.Add(this.picMotionLawCompare);

            this.tabMotionLaw.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.groupMotionLawSelect, this.groupMotionLawCompare
            });

            // ============================================
            // TabPage 3: 压力角校核
            // ============================================
            this.tabPressureAngle = new System.Windows.Forms.TabPage();
            this.tabPressureAngle.Text = "压力角校核";
            this.tabPressureAngle.Padding = new System.Windows.Forms.Padding(3);

            this.groupPressureAngleInput = new System.Windows.Forms.GroupBox();
            this.groupPressureAngleInput.Text = "压力角校核参数";
            this.groupPressureAngleInput.Location = new System.Drawing.Point(15, 15);
            this.groupPressureAngleInput.Size = new System.Drawing.Size(350, 200);

            this.lblPAResult = new System.Windows.Forms.Label();
            this.lblPAResult.Text = "请先在凸轮轮廓设计页面进行计算。\n\n" +
                "压力角校核说明：\n\n" +
                "许用压力角一般取值：\n" +
                "  直动从动件推程: [α] = 30°\n" +
                "  摆动从动件推程: [α] = 35°\n" +
                "  回程: [α] = 70°~80°\n\n" +
                "若压力角超过许用值，应增大基圆半径。";
            this.lblPAResult.Location = new System.Drawing.Point(15, 30);
            this.lblPAResult.Size = new System.Drawing.Size(320, 160);

            this.groupPressureAngleInput.Controls.Add(this.lblPAResult);

            this.groupPressureAngleChart = new System.Windows.Forms.GroupBox();
            this.groupPressureAngleChart.Text = "压力角变化曲线";
            this.groupPressureAngleChart.Location = new System.Drawing.Point(380, 15);
            this.groupPressureAngleChart.Size = new System.Drawing.Size(670, 400);

            this.picPressureAngleChart = new System.Windows.Forms.PictureBox();
            this.picPressureAngleChart.Location = new System.Drawing.Point(10, 20);
            this.picPressureAngleChart.Size = new System.Drawing.Size(650, 370);
            this.picPressureAngleChart.BackColor = System.Drawing.Color.White;
            this.picPressureAngleChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.groupPressureAngleChart.Controls.Add(this.picPressureAngleChart);

            this.tabPressureAngle.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.groupPressureAngleInput, this.groupPressureAngleChart
            });

            // ============================================
            // TabPage 4: 凸轮轮廓绘制
            // ============================================
            this.tabCamPlot = new System.Windows.Forms.TabPage();
            this.tabCamPlot.Text = "凸轮轮廓绘制";
            this.tabCamPlot.Padding = new System.Windows.Forms.Padding(3);

            this.groupCamPlotMain = new System.Windows.Forms.GroupBox();
            this.groupCamPlotMain.Text = "凸轮轮廓图";
            this.groupCamPlotMain.Location = new System.Drawing.Point(15, 15);
            this.groupCamPlotMain.Size = new System.Drawing.Size(700, 650);

            this.picCamPlotMain = new System.Windows.Forms.PictureBox();
            this.picCamPlotMain.Location = new System.Drawing.Point(10, 20);
            this.picCamPlotMain.Size = new System.Drawing.Size(680, 620);
            this.picCamPlotMain.BackColor = System.Drawing.Color.White;
            this.picCamPlotMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.groupCamPlotMain.Controls.Add(this.picCamPlotMain);

            this.groupCamPlotInfo = new System.Windows.Forms.GroupBox();
            this.groupCamPlotInfo.Text = "轮廓参数";
            this.groupCamPlotInfo.Location = new System.Drawing.Point(730, 15);
            this.groupCamPlotInfo.Size = new System.Drawing.Size(320, 300);

            this.lblCamPlotInfo = new System.Windows.Forms.Label();
            this.lblCamPlotInfo.Text = "请先在凸轮轮廓设计页面进行计算。\n\n" +
                "凸轮轮廓绘制说明：\n\n" +
                "1. 蓝色曲线为凸轮理论轮廓\n" +
                "2. 灰色虚线圆为基圆\n" +
                "3. 红色十字为凸轮旋转中心\n\n" +
                "理论轮廓：\n" +
                "  x = (rb + s)·cos(θ) - e·sin(θ)\n" +
                "  y = (rb + s)·sin(θ) + e·cos(θ)\n\n" +
                "其中：\n" +
                "  rb = 基圆半径\n" +
                "  s = 从动件位移\n" +
                "  θ = 凸轮转角\n" +
                "  e = 偏距";
            this.lblCamPlotInfo.Location = new System.Drawing.Point(15, 25);
            this.lblCamPlotInfo.Size = new System.Drawing.Size(290, 265);

            this.groupCamPlotInfo.Controls.Add(this.lblCamPlotInfo);

            this.tabCamPlot.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.groupCamPlotMain, this.groupCamPlotInfo
            });

            // ============================================
            // 添加 TabPage 到 TabControl
            // ============================================
            this.tabMain.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
                this.tabProfileDesign,
                this.tabMotionLaw,
                this.tabPressureAngle,
                this.tabCamPlot
            });

            // ============================================
            // 添加控件到主窗体
            // ============================================
            this.Controls.Add(this.tabMain);

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label CreateResultLabel(string text, int x, int y)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.AutoSize = true;
            return lbl;
        }

        #endregion

        // 主选项卡控件
        private System.Windows.Forms.TabControl tabMain;

        // TabPage 1: 凸轮轮廓设计
        private System.Windows.Forms.TabPage tabProfileDesign;
        private System.Windows.Forms.Label lblCamType;
        private System.Windows.Forms.ComboBox cmbCamType;
        private System.Windows.Forms.Label lblFollowerType;
        private System.Windows.Forms.ComboBox cmbFollowerType;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.GroupBox groupParameters;
        private System.Windows.Forms.Label lblBaseRadius;
        private System.Windows.Forms.TextBox txtBaseRadius;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Label lblRollerRadius;
        private System.Windows.Forms.TextBox txtRollerRadius;
        private System.Windows.Forms.Label lblAngularVelocity;
        private System.Windows.Forms.TextBox txtAngularVelocity;
        private System.Windows.Forms.Label lblAllowPressureAngle;
        private System.Windows.Forms.TextBox txtAllowPressureAngle;
        private System.Windows.Forms.GroupBox groupSteps;
        private System.Windows.Forms.DataGridView dgvSteps;
        private System.Windows.Forms.Button btnAddStep;
        private System.Windows.Forms.Button btnDeleteStep;
        private System.Windows.Forms.GroupBox groupProfilePreview;
        private System.Windows.Forms.PictureBox picCamProfile;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.GroupBox groupResults;
        private System.Windows.Forms.GroupBox groupDisplacement;
        private System.Windows.Forms.PictureBox picDisplacement;
        private System.Windows.Forms.GroupBox groupVelocity;
        private System.Windows.Forms.PictureBox picVelocity;
        private System.Windows.Forms.GroupBox groupAcceleration;
        private System.Windows.Forms.PictureBox picAcceleration;
        private System.Windows.Forms.Label lblResultMaxPressureAngle;
        private System.Windows.Forms.Label lblResultMaxDisplacement;
        private System.Windows.Forms.Label lblResultMaxVelocity;
        private System.Windows.Forms.Label lblResultMaxAcceleration;
        private System.Windows.Forms.Label lblResultMinBaseRadius;
        private System.Windows.Forms.Label lblPressureAngleCheck;
        private System.Windows.Forms.Label lblBaseRadiusCheck;

        // TabPage 2: 运动规律选择
        private System.Windows.Forms.TabPage tabMotionLaw;
        private System.Windows.Forms.GroupBox groupMotionLawSelect;
        private System.Windows.Forms.ComboBox cmbMotionLaw;
        private System.Windows.Forms.Label lblMotionLawDesc;
        private System.Windows.Forms.GroupBox groupMotionLawCompare;
        private System.Windows.Forms.PictureBox picMotionLawCompare;

        // TabPage 3: 压力角校核
        private System.Windows.Forms.TabPage tabPressureAngle;
        private System.Windows.Forms.GroupBox groupPressureAngleInput;
        private System.Windows.Forms.Label lblPAResult;
        private System.Windows.Forms.GroupBox groupPressureAngleChart;
        private System.Windows.Forms.PictureBox picPressureAngleChart;

        // TabPage 4: 凸轮轮廓绘制
        private System.Windows.Forms.TabPage tabCamPlot;
        private System.Windows.Forms.GroupBox groupCamPlotMain;
        private System.Windows.Forms.PictureBox picCamPlotMain;
        private System.Windows.Forms.GroupBox groupCamPlotInfo;
        private System.Windows.Forms.Label lblCamPlotInfo;
    }
}
