using System;
using System.Drawing;
using System.Windows.Forms;

namespace ComprehensiveTolerance
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = "迈迪综合公差查询系统";
            LoadDimensionalToleranceTab();
            LoadGeometricToleranceTab();
            LoadSurfaceRoughnessTab();
            LoadThreadToleranceTab();
        }

        #region 尺寸公差

        private void LoadDimensionalToleranceTab()
        {
            // 公称尺寸范围
            cboNominalSize.Items.AddRange(new object[]
            {
                ">0~3", ">3~6", ">6~10", ">10~18", ">18~30",
                ">30~50", ">50~80", ">80~120", ">120~180",
                ">180~250", ">250~315", ">315~400", ">400~500"
            });
            cboNominalSize.SelectedIndex = 4;

            // 公差等级
            cboToleranceGrade.Items.AddRange(new object[]
            {
                "IT01", "IT0", "IT1", "IT2", "IT3", "IT4", "IT5",
                "IT6", "IT7", "IT8", "IT9", "IT10", "IT11",
                "IT12", "IT13", "IT14", "IT15", "IT16", "IT17", "IT18"
            });
            cboToleranceGrade.SelectedIndex = 7; // IT7

            // 配合类型
            cboFitType.Items.AddRange(new object[]
            {
                "间隙配合", "过渡配合", "过盈配合"
            });
            cboFitType.SelectedIndex = 0;

            // 基准制
            cboBaseSystem.Items.AddRange(new object[] { "基孔制", "基轴制" });
            cboBaseSystem.SelectedIndex = 0;
        }

        private void btnCalculateDimTol_Click(object sender, EventArgs e)
        {
            if (cboNominalSize.SelectedIndex < 0 || cboToleranceGrade.SelectedIndex < 0)
            {
                MessageBox.Show("请选择公称尺寸和公差等级", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double nominalSize = GetNominalSizeMid(cboNominalSize.SelectedIndex);
            int grade = cboToleranceGrade.SelectedIndex; // 0=IT01, 1=IT0, ... 7=IT7, ...

            double tolerance = DimensionalToleranceCalculator.CalculateTolerance(nominalSize, grade);

            lblToleranceValue.Text = $"公差值: {tolerance:F3} mm ({tolerance * 1000:F1} μm)";

            // 计算上下偏差
            string fitType = cboFitType.SelectedItem?.ToString() ?? "间隙配合";
            string baseSystem = cboBaseSystem.SelectedItem?.ToString() ?? "基孔制";

            double upperDev, lowerDev;
            CalculateDeviations(nominalSize, grade, fitType, baseSystem, out upperDev, out lowerDev);

            lblUpperDeviation.Text = $"上偏差 (es/EI): {upperDev:F3} mm";
            lblLowerDeviation.Text = $"下偏差 (ei/ES): {lowerDev:F3} mm";
            lblMaxSize.Text = $"最大极限尺寸: {nominalSize + upperDev:F3} mm";
            lblMinSize.Text = $"最小极限尺寸: {nominalSize + lowerDev:F3} mm";
        }

        private double GetNominalSizeMid(int index)
        {
            double[] mids = { 1.5, 4.5, 8, 14, 24, 40, 65, 100, 150, 200, 275, 350, 450 };
            return index < mids.Length ? mids[index] : 24;
        }

        private void CalculateDeviations(double nominalSize, int grade, string fitType, string baseSystem,
            out double upperDev, out double lowerDev)
        {
            double tolerance = DimensionalToleranceCalculator.CalculateTolerance(nominalSize, grade);

            if (baseSystem == "基孔制")
            {
                // 基孔制: 孔的下偏差 EI = 0
                if (fitType == "间隙配合")
                {
                    upperDev = tolerance;
                    lowerDev = -tolerance;
                }
                else if (fitType == "过渡配合")
                {
                    upperDev = tolerance * 0.5;
                    lowerDev = -tolerance * 0.5;
                }
                else // 过盈配合
                {
                    upperDev = -tolerance * 0.1;
                    lowerDev = -tolerance;
                }
            }
            else
            {
                // 基轴制: 轴的上偏差 es = 0
                if (fitType == "间隙配合")
                {
                    upperDev = tolerance;
                    lowerDev = 0;
                }
                else if (fitType == "过渡配合")
                {
                    upperDev = tolerance * 0.5;
                    lowerDev = -tolerance * 0.5;
                }
                else
                {
                    upperDev = 0;
                    lowerDev = -tolerance;
                }
            }
        }

        #endregion

        #region 形位公差 (GD&T)

        private GeometricToleranceCalculator _gdtCalc = new GeometricToleranceCalculator();

        private void LoadGeometricToleranceTab()
        {
            // 形位公差项目
            cboGdtFeature.Items.AddRange(new object[]
            {
                "直线度", "平面度", "圆度", "圆柱度",
                "线轮廓度", "面轮廓度",
                "平行度", "垂直度", "倾斜度",
                "位置度", "同轴度", "对称度",
                "圆跳动", "全跳动"
            });
            cboGdtFeature.SelectedIndex = 0;

            // 公差带类型
            cboToleranceZone.Items.AddRange(new object[]
            {
                "两平行直线", "两平行平面", "圆柱面内", "两同心圆之间",
                "两同轴圆柱面之间", "圆内", "球内"
            });
            cboToleranceZone.SelectedIndex = 0;

            // 材料条件修饰符
            cboMaterialCondition.Items.AddRange(new object[] { "无 (RFS)", "M (最大实体)", "L (最小实体)", "R (可逆)" });
            cboMaterialCondition.SelectedIndex = 0;
        }

        private void cboGdtFeature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGdtFeature.SelectedIndex < 0) return;

            string feature = cboGdtFeature.SelectedItem.ToString();
            var info = _gdtCalc.GetToleranceInfo(feature);

            lblGdtSymbol.Text = $"符号: {info.Symbol}";
            lblGdtCategory.Text = $"分类: {info.Category}";
            lblGdtZoneType.Text = $"公差带: {info.DefaultZoneType}";
            lblGdtDescription.Text = info.Description;

            // 更新公差带选项
            cboToleranceZone.Items.Clear();
            cboToleranceZone.Items.AddRange(info.AllowedZoneTypes);
            if (cboToleranceZone.Items.Count > 0)
                cboToleranceZone.SelectedIndex = 0;
        }

        private void btnCalculateGdt_Click(object sender, EventArgs e)
        {
            if (cboGdtFeature.SelectedIndex < 0) return;

            double toleranceValue;
            if (!double.TryParse(txtGdtToleranceValue.Text, out toleranceValue) || toleranceValue <= 0)
            {
                MessageBox.Show("请输入有效的公差值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double nominalSize;
            if (!double.TryParse(txtGdtNominalSize.Text, out nominalSize) || nominalSize <= 0)
                nominalSize = 50;

            string feature = cboGdtFeature.SelectedItem.ToString();
            string zoneType = cboToleranceZone.SelectedItem?.ToString() ?? "";
            string mc = cboMaterialCondition.SelectedIndex.ToString();

            var result = _gdtCalc.Calculate(feature, toleranceValue, nominalSize, zoneType, mc);

            lblGdtResultZone.Text = $"公差带形状: {result.ZoneShape}";
            lblGdtResultSize.Text = $"公差带大小: {result.ZoneSize:F4} mm";

            if (result.HasBonusTolerance)
                lblGdtResultBonus.Text = $"附加公差 (Bonus): {result.BonusTolerance:F4} mm";
            else
                lblGdtResultBonus.Text = "附加公差: 无";

            lblGdtResultTotal.Text = $"实际可用公差: {result.TotalTolerance:F4} mm";
            lblGdtResultMMC.Text = $"MMC 尺寸: {result.MMCSize:F4} mm";
            lblGdtResultLMC.Text = $"LMC 尺寸: {result.LMCSize:F4} mm";
        }

        #endregion

        #region 表面粗糙度

        private void LoadSurfaceRoughnessTab()
        {
            // Ra 值
            cboRaValue.Items.AddRange(new object[]
            {
                "0.012", "0.025", "0.05", "0.1", "0.2", "0.4",
                "0.8", "1.6", "3.2", "6.3", "12.5", "25", "50", "100"
            });
            cboRaValue.SelectedIndex = 6; // 0.8

            // 粗糙度符号类型
            cboRoughnessSymbol.Items.AddRange(new object[]
            {
                "基本符号 (√)", "去除材料 (√̲)", "不去除材料 (√̅)"
            });
            cboRoughnessSymbol.SelectedIndex = 0;
        }

        private void cboRaValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRaValue.SelectedIndex < 0) return;

            double ra = double.Parse(cboRaValue.SelectedItem.ToString());
            var result = SurfaceRoughnessCalculator.Calculate(ra);

            lblRzValue.Text = $"Rz (参考值): {result.Rz:F2} μm";
            lblRqValue.Text = $"Rq (参考值): {result.Rq:F2} μm";
            lblRoughnessGrade.Text = $"表面粗糙度等级: {result.Grade}";
            lblManufacturingMethod.Text = $"推荐加工方法: {result.ManufacturingMethod}";
            lblSurfaceApplication.Text = $"典型应用: {result.Application}";
            lblRoughnessDesc.Text = result.Description;
        }

        private void btnConvertRaRz_Click(object sender, EventArgs e)
        {
            double ra;
            if (!double.TryParse(txtRaInput.Text, out ra) || ra <= 0)
            {
                MessageBox.Show("请输入有效的 Ra 值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double rz = SurfaceRoughnessCalculator.RaToRz(ra);
            double rq = SurfaceRoughnessCalculator.RaToRq(ra);
            txtRzOutput.Text = rz.ToString("F2");
            txtRqOutput.Text = rq.ToString("F2");
        }

        #endregion

        #region 螺纹公差

        private void LoadThreadToleranceTab()
        {
            // 螺纹规格
            cboThreadSpec.Items.AddRange(new object[]
            {
                "M1", "M1.2", "M1.4", "M1.6", "M2", "M2.5", "M3",
                "M4", "M5", "M6", "M8", "M10", "M12", "M14", "M16",
                "M18", "M20", "M22", "M24", "M27", "M30", "M33",
                "M36", "M39", "M42", "M45", "M48", "M52", "M56",
                "M60", "M64", "M68"
            });
            cboThreadSpec.SelectedIndex = 9; // M6

            // 公差带
            cboThreadTolerance.Items.AddRange(new object[]
            {
                "4h", "4H", "5g6g", "5H", "5H6H", "6g", "6e",
                "6f", "6h", "6H", "6G", "7g6g", "7H", "8g"
            });
            cboThreadTolerance.SelectedIndex = 9; // 6H

            // 螺距类型
            cboPitchType.Items.AddRange(new object[] { "粗牙", "细牙" });
            cboPitchType.SelectedIndex = 0;
        }

        private void cboThreadSpec_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateThreadInfo();
        }

        private void cboPitchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateThreadInfo();
        }

        private void UpdateThreadInfo()
        {
            if (cboThreadSpec.SelectedIndex < 0) return;

            string spec = cboThreadSpec.SelectedItem.ToString();
            bool isCoarse = cboPitchType.SelectedIndex == 0;

            var info = ThreadToleranceCalculator.GetThreadInfo(spec, isCoarse);
            if (info == null) return;

            lblThreadMajorDia.Text = $"大径 (d/D): {info.MajorDiameter:F3} mm";
            lblThreadPitch.Text = $"螺距 (P): {info.Pitch:F3} mm";
            lblThreadPitchDia.Text = $"中径 (d2/D2): {info.PitchDiameter:F3} mm";
            lblThreadMinorDia.Text = $"小径 (d1/D1): {info.MinorDiameter:F3} mm";
            lblThreadHeight.Text = $"牙高 (H): {info.ThreadHeight:F3} mm";
        }

        private void btnCalculateThreadTol_Click(object sender, EventArgs e)
        {
            if (cboThreadSpec.SelectedIndex < 0 || cboThreadTolerance.SelectedIndex < 0)
            {
                MessageBox.Show("请选择螺纹规格和公差带", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string spec = cboThreadSpec.SelectedItem.ToString();
            string tolerance = cboThreadTolerance.SelectedItem.ToString();
            bool isCoarse = cboPitchType.SelectedIndex == 0;

            var result = ThreadToleranceCalculator.CalculateTolerance(spec, tolerance, isCoarse);
            if (result == null)
            {
                MessageBox.Show("无法计算该螺纹公差", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblThreadTolGrade.Text = $"公差等级: {tolerance}";
            lblThreadMajorTol.Text = $"大径公差: {result.MajorDiaTolerance:F3} mm";
            lblThreadPitchTol.Text = $"中径公差: {result.PitchDiaTolerance:F3} mm";
            lblThreadMinorTol.Text = $"小径公差: {result.MinorDiaTolerance:F3} mm";

            lblThreadMajorRange.Text = $"大径范围: {result.MajorDiaMin:F3} ~ {result.MajorDiaMax:F3} mm";
            lblThreadPitchRange.Text = $"中径范围: {result.PitchDiaMin:F3} ~ {result.PitchDiaMax:F3} mm";
            lblThreadMinorRange.Text = $"小径范围: {result.MinorDiaMin:F3} ~ {result.MinorDiaMax:F3} mm";

            lblThreadFitInfo.Text = result.FitDescription;
        }

        #endregion
    }
}
