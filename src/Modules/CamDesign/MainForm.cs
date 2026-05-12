using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CamDesign
{
    /// <summary>
    /// 迈迪凸轮设计系统主窗体
    /// </summary>
    public partial class MainForm : Form
    {
        private CamCalculator calculator;
        private List<CamStep> camSteps;
        private CamProfileData currentProfile;

        // 当前参数
        private double baseCircleRadius = 40.0;  // 基圆半径 mm
        private double offset = 0.0;              // 偏距 mm
        private double rollerRadius = 5.0;        // 滚子半径 mm
        private double camThickness = 10.0;       // 凸轮厚度 mm
        private double angularVelocity = 10.0;    // 角速度 rad/s
        private double allowablePressureAngle = 30.0; // 许用压力角 度

        public MainForm()
        {
            InitializeComponent();
            calculator = new CamCalculator();
            camSteps = new List<CamStep>();
            InitializeDefaultCamSteps();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化界面默认值
            if (txtBaseRadius != null) txtBaseRadius.Text = baseCircleRadius.ToString("F1");
            if (txtOffset != null) txtOffset.Text = offset.ToString("F1");
            if (txtRollerRadius != null) txtRollerRadius.Text = rollerRadius.ToString("F1");
            if (txtAngularVelocity != null) txtAngularVelocity.Text = angularVelocity.ToString("F1");
            if (txtAllowPressureAngle != null) txtAllowPressureAngle.Text = allowablePressureAngle.ToString("F1");

            // 初始化凸轮类型下拉框
            if (cmbCamType != null)
            {
                cmbCamType.Items.AddRange(new object[] {
                    "盘形凸轮（平面转动凸轮）",
                    "移动凸轮（平面往复运动凸轮）",
                    "圆柱凸轮（空间圆柱凸轮）",
                    "盘形沟槽凸轮",
                    "端面凸轮（空间端面凸轮）"
                });
                cmbCamType.SelectedIndex = 0;
            }

            // 初始化从动件类型下拉框
            if (cmbFollowerType != null)
            {
                cmbFollowerType.Items.AddRange(new object[] {
                    "对心直动从动件",
                    "偏置直动从动件",
                    "摆动从动件"
                });
                cmbFollowerType.SelectedIndex = 0;
            }

            // 初始化常用凸轮模板下拉框
            if (cmbTemplate != null)
            {
                cmbTemplate.Items.AddRange(new object[] {
                    "四阶段凸轮1",
                    "四阶段凸轮2",
                    "六阶段凸轮1",
                    "六阶段凸轮2"
                });
                cmbTemplate.SelectedIndex = 0;
            }

            // 初始化运动规律下拉框
            if (cmbMotionLaw != null)
            {
                cmbMotionLaw.Items.AddRange(new object[] {
                    "等速（直线）",
                    "等加速（抛物线）",
                    "余弦加速度（简谐）",
                    "正弦加速度（摆线）",
                    "五次多项式",
                    "改进梯形加速度",
                    "改进正弦加速度"
                });
                cmbMotionLaw.SelectedIndex = 2; // 默认余弦
            }
        }

        /// <summary>
        /// 初始化默认凸轮运动阶段（四阶段凸轮1: 推程-远休-回程-近休）
        /// </summary>
        private void InitializeDefaultCamSteps()
        {
            camSteps.Clear();

            // 推程阶段: 0~110度, 位移 0~40mm
            var rise = new CamStep("推程", 0, 110, 0, 40, MotionLaw.CosineAcceleration);
            // 远休止: 110~140度, 位移 40mm 保持
            var farDwell = new CamStep("远休止", 110, 140, 40, 40, MotionLaw.Stop);
            // 回程阶段: 140~250度, 位移 40~0mm
            var @return = new CamStep("回程", 140, 250, 40, 0, MotionLaw.CosineAcceleration);
            // 近休止: 250~360度, 位移 0mm 保持
            var nearDwell = new CamStep("近休止", 250, 360, 0, 0, MotionLaw.Stop);

            // 建立链表关系
            rise.Next = farDwell;
            farDwell.Previous = rise;
            farDwell.Next = @return;
            @return.Previous = farDwell;
            @return.Next = nearDwell;
            nearDwell.Previous = @return;

            camSteps.Add(rise);
            camSteps.Add(farDwell);
            camSteps.Add(@return);
            camSteps.Add(nearDwell);
        }

        /// <summary>
        /// 加载常用凸轮模板
        /// </summary>
        private void LoadTemplate(string templateName)
        {
            camSteps.Clear();

            switch (templateName)
            {
                case "四阶段凸轮1":
                    // 推程0~110, 远休110~140, 回程140~250, 近休250~360
                    AddStep("推程", 0, 110, 0, 40, MotionLaw.CosineAcceleration, true);
                    AddStep("远休止", 110, 140, 40, 40, MotionLaw.Stop, false);
                    AddStep("回程", 140, 250, 40, 0, MotionLaw.CosineAcceleration, false);
                    AddStep("近休止", 250, 360, 0, 0, MotionLaw.Stop, false);
                    break;

                case "四阶段凸轮2":
                    // 推程0~110, 远休110~140, 回程140~250, 近休250~360 (不同角度分配)
                    AddStep("推程", 0, 110, 0, 40, MotionLaw.CosineAcceleration, true);
                    AddStep("远休止", 110, 140, 40, 40, MotionLaw.Stop, false);
                    AddStep("回程", 140, 250, 40, 0, MotionLaw.SineAcceleration, false);
                    AddStep("近休止", 250, 360, 0, 0, MotionLaw.Stop, false);
                    break;

                case "六阶段凸轮1":
                    // 推程1, 远休1, 推程2, 远休2, 回程, 近休
                    AddStep("推程", 0, 100, 0, 40, MotionLaw.CosineAcceleration, true);
                    AddStep("远休止", 100, 130, 40, 40, MotionLaw.Stop, false);
                    AddStep("推程", 130, 200, 40, 60, MotionLaw.CosineAcceleration, false);
                    AddStep("远休止", 200, 240, 60, 60, MotionLaw.Stop, false);
                    AddStep("回程", 240, 320, 60, 0, MotionLaw.CosineAcceleration, false);
                    AddStep("近休止", 320, 360, 0, 0, MotionLaw.Stop, false);
                    break;

                case "六阶段凸轮2":
                    // 双推程双回程
                    AddStep("推程", 0, 80, 0, 30, MotionLaw.SineAcceleration, true);
                    AddStep("远休止", 80, 120, 30, 30, MotionLaw.Stop, false);
                    AddStep("推程", 120, 200, 30, 50, MotionLaw.CosineAcceleration, false);
                    AddStep("远休止", 200, 240, 50, 50, MotionLaw.Stop, false);
                    AddStep("回程", 240, 320, 50, 0, MotionLaw.CosineAcceleration, false);
                    AddStep("近休止", 320, 360, 0, 0, MotionLaw.Stop, false);
                    break;
            }

            RefreshStepList();
        }

        private void AddStep(string name, double startAngle, double endAngle,
            double hStart, double hEnd, MotionLaw law, bool isFirst)
        {
            var step = new CamStep(name, startAngle, endAngle, hStart, hEnd, law);
            if (camSteps.Count > 0)
            {
                var last = camSteps[camSteps.Count - 1];
                last.Next = step;
                step.Previous = last;
            }
            camSteps.Add(step);
        }

        /// <summary>
        /// 刷新步骤列表显示
        /// </summary>
        private void RefreshStepList()
        {
            if (dgvSteps == null) return;

            dgvSteps.Rows.Clear();
            foreach (var step in camSteps)
            {
                int rowIdx = dgvSteps.Rows.Add(
                    step.StepName,
                    step.StartAngle.ToString("F1"),
                    step.EndAngle.ToString("F1"),
                    step.HStart.ToString("F2"),
                    step.HEnd.ToString("F2"),
                    GetMotionLawName(step.MotionLaw)
                );
            }
        }

        private string GetMotionLawName(MotionLaw law)
        {
            switch (law)
            {
                case MotionLaw.ConstantVelocity: return "等速（直线）";
                case MotionLaw.ConstantAcceleration: return "等加速（抛物线）";
                case MotionLaw.CosineAcceleration: return "余弦加速度（简谐）";
                case MotionLaw.SineAcceleration: return "正弦加速度（摆线）";
                case MotionLaw.FifthOrderPolynomial: return "五次多项式";
                case MotionLaw.ModifiedTrapezoidal: return "改进梯形加速度";
                case MotionLaw.ModifiedSine: return "改进正弦加速度";
                case MotionLaw.Stop: return "停止";
                default: return "未知";
            }
        }

        #region 按钮事件

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 读取参数
                if (!double.TryParse(txtBaseRadius?.Text, out baseCircleRadius) || baseCircleRadius <= 0)
                {
                    MessageBox.Show("请输入有效的基圆半径", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtOffset?.Text, out offset))
                    offset = 0;

                if (!double.TryParse(txtAngularVelocity?.Text, out angularVelocity) || angularVelocity <= 0)
                    angularVelocity = 10.0;

                if (!double.TryParse(txtAllowPressureAngle?.Text, out allowablePressureAngle))
                    allowablePressureAngle = 30.0;

                // 生成凸轮轮廓
                currentProfile = calculator.GenerateCamProfile(camSteps, baseCircleRadius, offset, angularVelocity);

                // 计算最小基圆半径
                double rbMin = calculator.CalcBaseCircleRadius(currentProfile.MaxDsDtheta, allowablePressureAngle, offset);

                // 显示结果
                DisplayResults(currentProfile, rbMin);

                // 绘制曲线
                picDisplacement?.Invalidate();
                picVelocity?.Invalidate();
                picAcceleration?.Invalidate();
                picCamProfile?.Invalidate();

                MessageBox.Show("计算完成！", "凸轮设计", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            if (camSteps.Count > 0)
            {
                var last = camSteps[camSteps.Count - 1];
                double newStart = last.EndAngle;
                double newEnd = Math.Min(newStart + 30, 360);
                var step = new CamStep("新阶段", newStart, newEnd, last.HEnd, last.HEnd, MotionLaw.CosineAcceleration);
                last.Next = step;
                step.Previous = last;
                camSteps.Add(step);
                RefreshStepList();
            }
        }

        private void btnDeleteStep_Click(object sender, EventArgs e)
        {
            if (dgvSteps?.CurrentRow != null && dgvSteps.CurrentRow.Index < camSteps.Count)
            {
                int idx = dgvSteps.CurrentRow.Index;
                var step = camSteps[idx];

                if (step.Previous != null) step.Previous.Next = step.Next;
                if (step.Next != null) step.Next.Previous = step.Previous;

                camSteps.RemoveAt(idx);
                RefreshStepList();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnCalculate_Click(sender, e);
        }

        private void cmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTemplate?.SelectedItem != null)
            {
                LoadTemplate(cmbTemplate.SelectedItem.ToString());
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (currentProfile == null || currentProfile.Points.Count == 0)
            {
                MessageBox.Show("请先进行计算", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel文件|*.xlsx|CSV文件|*.csv|文本文件|*.txt";
                dlg.Title = "导出凸轮数据";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportData(dlg.FileName);
                        MessageBox.Show("导出成功！", "导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region 结果显示

        private void DisplayResults(CamProfileData profile, double rbMin)
        {
            if (lblResultMaxPressureAngle != null)
                lblResultMaxPressureAngle.Text = string.Format("{0:F2}°", profile.MaxPressureAngle);

            if (lblResultMaxDisplacement != null)
                lblResultMaxDisplacement.Text = string.Format("{0:F2} mm", profile.MaxDisplacement);

            if (lblResultMaxVelocity != null)
                lblResultMaxVelocity.Text = string.Format("{0:F2} mm/s", profile.MaxVelocity);

            if (lblResultMaxAcceleration != null)
                lblResultMaxAcceleration.Text = string.Format("{0:F2} mm/s²", profile.MaxAcceleration);

            if (lblResultMinBaseRadius != null)
                lblResultMinBaseRadius.Text = string.Format("{0:F2} mm", rbMin);

            // 压力角校核结果
            bool pressureAngleOk = profile.MaxPressureAngle <= allowablePressureAngle;
            if (lblPressureAngleCheck != null)
            {
                lblPressureAngleCheck.Text = pressureAngleOk ? "满足要求" : "不满足要求";
                lblPressureAngleCheck.ForeColor = pressureAngleOk ? Color.Green : Color.Red;
            }

            // 基圆半径校核结果
            bool baseRadiusOk = baseCircleRadius >= rbMin;
            if (lblBaseRadiusCheck != null)
            {
                lblBaseRadiusCheck.Text = baseRadiusOk ? "满足要求" : "不满足要求";
                lblBaseRadiusCheck.ForeColor = baseRadiusOk ? Color.Green : Color.Red;
            }
        }

        #endregion

        #region 绘图

        private void picDisplacement_Paint(object sender, PaintEventArgs e)
        {
            DrawCurve(e.Graphics, picDisplacement, "位移曲线", "位移 (mm)",
                p => p.Displacement, Color.Blue);
        }

        private void picVelocity_Paint(object sender, PaintEventArgs e)
        {
            DrawCurve(e.Graphics, picVelocity, "速度曲线", "速度 (mm/s)",
                p => p.Velocity, Color.Green);
        }

        private void picAcceleration_Paint(object sender, PaintEventArgs e)
        {
            DrawCurve(e.Graphics, picAcceleration, "加速度曲线", "加速度 (mm/s²)",
                p => p.Acceleration, Color.Red);
        }

        private void DrawCurve(Graphics g, PictureBox pic, string title, string yLabel,
            Func<CamProfilePoint, double> valueSelector, Color curveColor)
        {
            if (currentProfile == null || currentProfile.Points.Count < 2)
            {
                // 无数据时绘制示意曲线
                DrawDemoCurve(g, pic, title, yLabel, curveColor);
                return;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            int margin = 40;
            int w = pic.Width - 2 * margin;
            int h = pic.Height - 2 * margin;

            if (w <= 0 || h <= 0) return;

            // 找到数据范围
            double minY = double.MaxValue, maxY = double.MinValue;
            foreach (var p in currentProfile.Points)
            {
                double val = valueSelector(p);
                if (val < minY) minY = val;
                if (val > maxY) maxY = val;
            }

            if (Math.Abs(maxY - minY) < 1e-10)
            {
                maxY = minY + 1;
            }

            double rangeY = maxY - minY;
            minY -= rangeY * 0.1;
            maxY += rangeY * 0.1;
            rangeY = maxY - minY;

            // 绘制坐标轴
            using (var axisPen = new Pen(Color.Black, 1))
            {
                g.DrawLine(axisPen, margin, margin, margin, pic.Height - margin);
                g.DrawLine(axisPen, margin, pic.Height - margin, pic.Width - margin, pic.Height - margin);
            }

            // 绘制标题和标签
            using (var font = new Font("SimSun", 9))
            using (var brush = new SolidBrush(Color.Black))
            {
                g.DrawString(title, new Font("SimSun", 10, FontStyle.Bold), brush, margin, 5);
                g.DrawString(yLabel, font, brush, 2, margin);
                g.DrawString("0°", font, brush, margin, pic.Height - margin + 5);
                g.DrawString("360°", font, brush, pic.Width - margin - 25, pic.Height - margin + 5);
            }

            // 绘制曲线
            using (var curvePen = new Pen(curveColor, 2))
            {
                var points = new PointF[currentProfile.Points.Count];
                for (int i = 0; i < currentProfile.Points.Count; i++)
                {
                    var p = currentProfile.Points[i];
                    float x = margin + (float)(p.Theta / 360.0 * w);
                    float y = (float)(pic.Height - margin - (valueSelector(p) - minY) / rangeY * h);
                    points[i] = new PointF(x, y);
                }

                if (points.Length > 1)
                {
                    g.DrawLines(curvePen, points);
                }
            }

            // 绘制网格
            using (var gridPen = new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash })
            {
                for (int i = 1; i <= 4; i++)
                {
                    float y = margin + h * i / 5;
                    g.DrawLine(gridPen, margin, y, pic.Width - margin, y);
                }
                for (int i = 1; i <= 8; i++)
                {
                    float x = margin + w * i / 8;
                    g.DrawLine(gridPen, x, margin, x, pic.Height - margin);
                }
            }
        }

        private void DrawDemoCurve(Graphics g, PictureBox pic, string title, string yLabel, Color curveColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            int margin = 40;
            int w = pic.Width - 2 * margin;
            int h = pic.Height - 2 * margin;

            if (w <= 0 || h <= 0) return;

            // 绘制坐标轴
            using (var axisPen = new Pen(Color.Black, 1))
            {
                g.DrawLine(axisPen, margin, margin, margin, pic.Height - margin);
                g.DrawLine(axisPen, margin, pic.Height - margin, pic.Width - margin, pic.Height - margin);
            }

            // 绘制标题
            using (var font = new Font("SimSun", 9))
            using (var brush = new SolidBrush(Color.Black))
            {
                g.DrawString(title, new Font("SimSun", 10, FontStyle.Bold), brush, margin, 5);
                g.DrawString(yLabel, font, brush, 2, margin);
            }

            // 根据运动规律绘制示意曲线
            using (var curvePen = new Pen(curveColor, 2))
            {
                int n = 200;
                var points = new PointF[n];
                for (int i = 0; i < n; i++)
                {
                    double theta = i * 2.0 * Math.PI / n;
                    double s = 0;

                    // 模拟四阶段凸轮的位移曲线
                    double t = theta / (2.0 * Math.PI);
                    if (t < 110.0 / 360.0)
                    {
                        double tt = t / (110.0 / 360.0);
                        s = 40.0 / 2.0 * (1 - Math.Cos(Math.PI * tt));
                    }
                    else if (t < 140.0 / 360.0)
                    {
                        s = 40.0;
                    }
                    else if (t < 250.0 / 360.0)
                    {
                        double tt = (t - 140.0 / 360.0) / (110.0 / 360.0);
                        s = 40.0 / 2.0 * (1 + Math.Cos(Math.PI * tt));
                    }

                    float x = margin + (float)(i * w / (double)n);
                    float y = (float)(pic.Height - margin - s / 50.0 * h);
                    points[i] = new PointF(x, y);
                }

                g.DrawLines(curvePen, points);
            }
        }

        /// <summary>
        /// 绘制凸轮轮廓
        /// </summary>
        private void picCamProfile_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            int cx = picCamProfile.Width / 2;
            int cy = picCamProfile.Height / 2;
            float scale = Math.Min(picCamProfile.Width, picCamProfile.Height) / (float)(baseCircleRadius * 4);

            if (currentProfile != null && currentProfile.Points.Count > 2)
            {
                // 绘制凸轮轮廓
                using (var pen = new Pen(Color.Blue, 2))
                {
                    var pts = new PointF[currentProfile.Points.Count];
                    for (int i = 0; i < currentProfile.Points.Count; i++)
                    {
                        pts[i] = new PointF(
                            cx + (float)currentProfile.Points[i].TheoreticalX * scale,
                            cy - (float)currentProfile.Points[i].TheoreticalY * scale
                        );
                    }
                    if (pts.Length > 2)
                        g.DrawClosedCurve(pen, pts);
                }

                // 绘制基圆
                using (var pen = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dash })
                {
                    float r = (float)baseCircleRadius * scale;
                    g.DrawEllipse(pen, cx - r, cy - r, 2 * r, 2 * r);
                }
            }
            else
            {
                // 无数据时绘制示意轮廓
                DrawDemoCamProfile(g, cx, cy, scale);
            }

            // 绘制中心标记
            using (var pen = new Pen(Color.Red, 1))
            {
                g.DrawLine(pen, cx - 5, cy, cx + 5, cy);
                g.DrawLine(pen, cx, cy - 5, cx, cy + 5);
            }
        }

        private void DrawDemoCamProfile(Graphics g, int cx, int cy, float scale)
        {
            // 绘制基圆
            float rb = (float)baseCircleRadius * scale;
            using (var pen = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dash })
            {
                g.DrawEllipse(pen, cx - rb, cy - rb, 2 * rb, 2 * rb);
            }

            // 绘制示意凸轮轮廓
            using (var pen = new Pen(Color.Blue, 2))
            {
                int n = 360;
                var pts = new PointF[n];
                for (int i = 0; i < n; i++)
                {
                    double theta = i * 2.0 * Math.PI / n;
                    double s = 0;
                    double t = theta / (2.0 * Math.PI);

                    if (t < 110.0 / 360.0)
                    {
                        double tt = t / (110.0 / 360.0);
                        s = 40.0 / 2.0 * (1 - Math.Cos(Math.PI * tt));
                    }
                    else if (t < 140.0 / 360.0)
                    {
                        s = 40.0;
                    }
                    else if (t < 250.0 / 360.0)
                    {
                        double tt = (t - 140.0 / 360.0) / (110.0 / 360.0);
                        s = 40.0 / 2.0 * (1 + Math.Cos(Math.PI * tt));
                    }

                    double r = (baseCircleRadius + s) * scale;
                    pts[i] = new PointF(
                        cx + (float)(r * Math.Cos(theta)),
                        cy - (float)(r * Math.Sin(theta))
                    );
                }
                g.DrawClosedCurve(pen, pts);
            }
        }

        #endregion

        #region 数据导出

        private void ExportData(string filePath)
        {
            if (filePath.EndsWith(".csv") || filePath.EndsWith(".txt"))
            {
                ExportToCsv(filePath);
            }
            else
            {
                ExportToCsv(filePath); // 默认 CSV 格式
            }
        }

        private void ExportToCsv(string filePath)
        {
            using (var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("角度(°),位移(mm),速度(mm/s),加速度(mm/s²),压力角(°)");
                foreach (var p in currentProfile.Points)
                {
                    writer.WriteLine(string.Format("{0:F2},{1:F4},{2:F4},{3:F4},{4:F4}",
                        p.Theta, p.Displacement, p.Velocity, p.Acceleration, p.PressureAngle));
                }
            }
        }

        #endregion
    }
}
