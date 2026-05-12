using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShaftDesign
{
    public partial class MainForm : Form
    {
        private readonly ShaftCalculator _calculator;
        private List<ShaftMaterial> _materials;

        public MainForm()
        {
            InitializeComponent();
            _calculator = new ShaftCalculator();
            _materials = ShaftMaterial.GetDefaultMaterials();
            InitMaterialCombo();
            LoadDefaultData();
        }

        private void InitMaterialCombo()
        {
            comboBoxMaterial.Items.Clear();
            foreach (var mat in _materials)
            {
                comboBoxMaterial.Items.Add(mat.Name);
            }
            comboBoxMaterial.SelectedIndex = 1; // 默认 45钢正火
        }

        private void LoadDefaultData()
        {
            // 添加默认零件布局
            dgvComponents.Rows.Add("左轴承", "轴承", "0", "45");
            dgvComponents.Rows.Add("齿轮", "齿轮", "150", "50");
            dgvComponents.Rows.Add("右轴承", "轴承", "300", "45");

            // 添加默认载荷
            dgvLoads.Rows.Add("齿轮力", "150", "1200", "3500", "500");
        }

        private void ComboBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBoxMaterial.SelectedIndex;
            if (idx >= 0 && idx < _materials.Count)
            {
                var mat = _materials[idx];
                txtSigmaS.Text = mat.SigmaS.ToString("F0") + " MPa";
                txtSigmaMinus1.Text = mat.SigmaMinus1.ToString("F0") + " MPa";
                txtTauMinus1.Text = mat.TauMinus1.ToString("F0") + " MPa";
            }
        }

        private void BtnAddComponent_Click(object sender, EventArgs e)
        {
            dgvComponents.Rows.Add("新零件", "其他", "0", "40");
        }

        private void BtnRemoveComponent_Click(object sender, EventArgs e)
        {
            if (dgvComponents.CurrentRow != null && dgvComponents.CurrentRow.Index >= 0)
            {
                dgvComponents.Rows.RemoveAt(dgvComponents.CurrentRow.Index);
            }
        }

        private void BtnAddLoad_Click(object sender, EventArgs e)
        {
            dgvLoads.Rows.Add("新载荷", "0", "0", "0", "0");
        }

        private void BtnRemoveLoad_Click(object sender, EventArgs e)
        {
            if (dgvLoads.CurrentRow != null && dgvLoads.CurrentRow.Index >= 0)
            {
                dgvLoads.Rows.RemoveAt(dgvLoads.CurrentRow.Index);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtPower.Text = "5.5";
            txtSpeed.Text = "1450";
            comboBoxMaterial.SelectedIndex = 1;
            comboBoxKeyway.SelectedIndex = 0;
            txtRoughness.Text = "1.6";
            txtAllowableN.Text = "1.5";
            txtAllowableS.Text = "1.5";
            radioPulsating.Checked = true;

            dgvComponents.Rows.Clear();
            dgvLoads.Rows.Clear();
            LoadDefaultData();

            txtEstimateResult.Text = "";
            dgvStrength.Rows.Clear();
            dgvFatigue.Rows.Clear();
            toolStripStatusLabel.Text = "已重置";
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // ---- 解析输入 ----
                double power, speed;
                if (!double.TryParse(txtPower.Text, out power) || power <= 0)
                {
                    MessageBox.Show("请输入有效的传递功率 (大于0)", "输入错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPower.Focus();
                    return;
                }
                if (!double.TryParse(txtSpeed.Text, out speed) || speed <= 0)
                {
                    MessageBox.Show("请输入有效的转速 (大于0)", "输入错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSpeed.Focus();
                    return;
                }

                int matIdx = comboBoxMaterial.SelectedIndex;
                if (matIdx < 0 || matIdx >= _materials.Count)
                {
                    MessageBox.Show("请选择轴的材料", "输入错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ShaftMaterial material = _materials[matIdx];

                // 载荷循环类型
                LoadCycleType cycleType = LoadCycleType.Pulsating;
                if (radioSymmetric.Checked) cycleType = LoadCycleType.Symmetric;
                else if (radioStatic.Checked) cycleType = LoadCycleType.Static;

                int keywayCount = comboBoxKeyway.SelectedIndex;
                double roughness;
                if (!double.TryParse(txtRoughness.Text, out roughness) || roughness <= 0)
                    roughness = 1.6;

                double allowableN, allowableS;
                if (!double.TryParse(txtAllowableN.Text, out allowableN) || allowableN <= 0)
                    allowableN = 1.5;
                if (!double.TryParse(txtAllowableS.Text, out allowableS) || allowableS <= 0)
                    allowableS = 1.5;

                _calculator.AllowableSafetyFactor = allowableN;
                _calculator.AllowableFatigueSafetyFactor = allowableS;

                // ---- 收集零件数据 ----
                var components = new List<ShaftComponent>();
                foreach (DataGridViewRow row in dgvComponents.Rows)
                {
                    if (row.IsNewRow) continue;
                    var comp = new ShaftComponent
                    {
                        Name = row.Cells["colName"].Value?.ToString() ?? "",
                        Type = row.Cells["colType"].Value?.ToString() ?? "",
                        Position = ParseDouble(row.Cells["colPosition"].Value),
                        Diameter = ParseDouble(row.Cells["colDiameter"].Value)
                    };
                    components.Add(comp);
                }

                // ---- 收集载荷数据 ----
                var loads = new List<ShaftLoad>();
                foreach (DataGridViewRow row in dgvLoads.Rows)
                {
                    if (row.IsNewRow) continue;
                    var load = new ShaftLoad
                    {
                        Name = row.Cells["colLoadName"].Value?.ToString() ?? "",
                        Position = ParseDouble(row.Cells["colLoadPos"].Value),
                        RadialForce = ParseDouble(row.Cells["colFr"].Value),
                        TangentialForce = ParseDouble(row.Cells["colFt"].Value),
                        AxialForce = ParseDouble(row.Cells["colFa"].Value)
                    };
                    loads.Add(load);
                }

                if (loads.Count == 0)
                {
                    MessageBox.Show("请至少添加一个载荷", "输入错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 找到轴承位置
                double leftBearingPos = double.MaxValue;
                double rightBearingPos = double.MinValue;
                foreach (var comp in components)
                {
                    if (comp.Type == "轴承")
                    {
                        if (comp.Position < leftBearingPos) leftBearingPos = comp.Position;
                        if (comp.Position > rightBearingPos) rightBearingPos = comp.Position;
                    }
                }
                if (leftBearingPos >= rightBearingPos)
                {
                    leftBearingPos = 0;
                    rightBearingPos = 300;
                }

                // ========================================
                // 1. 初估轴径
                // ========================================
                double dEst = _calculator.EstimateDiameter(power, speed, material.CoefficientA);
                double dEstKeyway = _calculator.EstimateDiameterWithKeyway(power, speed, material.CoefficientA, keywayCount);
                double torque = _calculator.CalcTorque(power, speed);
                double torqueNm = _calculator.CalcTorqueNm(power, speed);

                var sb = new StringBuilder();
                sb.AppendLine("========== 初估轴径计算 ==========");
                sb.AppendLine();
                sb.AppendLine($"传递功率 P = {power:F2} kW");
                sb.AppendLine($"转  速 n = {speed:F0} rpm");
                sb.AppendLine($"材  料: {material.Name}");
                sb.AppendLine($"材料系数 A = {material.CoefficientA}");
                sb.AppendLine();
                sb.AppendLine("计算公式: d >= A * (P/n)^(1/3)");
                sb.AppendLine($"d >= {material.CoefficientA} * ({power}/{speed})^(1/3)");
                sb.AppendLine($"    = {material.CoefficientA} * {Math.Pow(power / speed, 1.0 / 3.0):F4}");
                sb.AppendLine($"    = {dEst:F2} mm");
                sb.AppendLine();
                sb.AppendLine($"扭矩 T = 9550 * P / n = 9550 * {power}/{speed} = {torque:F2} N.m = {torqueNm:F0} N.mm");
                sb.AppendLine();

                if (keywayCount > 0)
                {
                    double factor = keywayCount == 1 ? 1.05 : 1.10;
                    sb.AppendLine($"考虑键槽 ({keywayCount}个): d *= {factor}");
                    sb.AppendLine($"修正后 d >= {dEstKeyway:F2} mm");
                    sb.AppendLine();
                }

                // 标准直径取整 (向上取到最近的5的倍数)
                double dStandard = Math.Ceiling(dEstKeyway / 5.0) * 5;
                if (dStandard < dEstKeyway) dStandard += 5;
                sb.AppendLine($"标准圆整直径: d = {dStandard:F0} mm");
                sb.AppendLine();

                // ========================================
                // 2. 弯矩计算
                // ========================================
                sb.AppendLine("========== 弯矩图计算 ==========");
                sb.AppendLine();
                sb.AppendLine($"左轴承位置: {leftBearingPos:F0} mm");
                sb.AppendLine($"右轴承位置: {rightBearingPos:F0} mm");
                sb.AppendLine($"跨  距: {rightBearingPos - leftBearingPos:F0} mm");
                sb.AppendLine();

                ShaftLoad[] loadsArray = loads.ToArray();
                BendingMomentDiagram bmd = null;
                try
                {
                    bmd = _calculator.CalcBendingMomentDiagram(loadsArray, leftBearingPos, rightBearingPos);
                    sb.AppendLine($"最大弯矩 M_max = {bmd.MaxMoment:F0} N.mm");
                    sb.AppendLine($"最大弯矩位置: {bmd.MaxMomentPosition:F1} mm");
                }
                catch (Exception ex)
                {
                    sb.AppendLine($"弯矩计算异常: {ex.Message}");
                }
                sb.AppendLine();

                // ========================================
                // 3. 各截面强度校核
                // ========================================
                sb.AppendLine("========== 强度校核 ==========");
                sb.AppendLine();
                double alpha = _calculator.GetAlpha(cycleType);
                sb.AppendLine($"载荷循环: {(cycleType == LoadCycleType.Pulsating ? "脉动循环" : cycleType == LoadCycleType.Symmetric ? "对称循环" : "静载荷")} (alpha={alpha})");
                sb.AppendLine($"许用安全系数 [n] = {allowableN}");
                sb.AppendLine();

                txtEstimateResult.Text = sb.ToString();

                // ---- 强度校核表格 ----
                dgvStrength.Rows.Clear();
                dgvFatigue.Rows.Clear();

                bool hasKeyway = keywayCount > 0;
                bool hasShoulder = false;
                foreach (var comp in components)
                {
                    if (comp.Position > leftBearingPos && comp.Position < rightBearingPos)
                    {
                        // 齿轮/联轴器处一般有轴肩
                        if (comp.Type == "齿轮" || comp.Type == "联轴器")
                        {
                            hasShoulder = true;
                            break;
                        }
                    }
                }

                // 对每个零件位置进行校核
                foreach (var comp in components)
                {
                    if (comp.Diameter <= 0) continue;

                    double bendingMoment = 0;
                    if (bmd != null)
                    {
                        // 在弯矩图中插值求该位置的弯矩
                        bendingMoment = InterpolateBendingMoment(bmd, comp.Position);
                    }

                    SectionResult result = _calculator.CheckSection(
                        material, comp.Diameter, bendingMoment, torqueNm,
                        cycleType, hasKeyway, hasShoulder, roughness);

                    result.Position = comp.Position;
                    result.Description = comp.Name;

                    // 强度校核表格
                    int rowIdx = dgvStrength.Rows.Add(
                        comp.Position.ToString("F0"),
                        comp.Diameter.ToString("F1"),
                        result.BendingMoment.ToString("F0"),
                        result.Torque.ToString("F0"),
                        result.EquivalentMoment.ToString("F0"),
                        result.BendingStress.ToString("F2"),
                        result.TorsionStress.ToString("F2"),
                        result.CombinedStress.ToString("F2"),
                        result.SafetyFactor.ToString("F2"),
                        result.PassesStrengthCheck ? "合格" : "不合格"
                    );

                    // 设置合格/不合格颜色
                    var passCell = dgvStrength.Rows[rowIdx].Cells["colSecPass"];
                    if (result.PassesStrengthCheck)
                    {
                        passCell.Style.ForeColor = Color.Green;
                        passCell.Style.Font = new Font(dgvStrength.Font, FontStyle.Bold);
                    }
                    else
                    {
                        passCell.Style.ForeColor = Color.Red;
                        passCell.Style.Font = new Font(dgvStrength.Font, FontStyle.Bold);
                    }

                    // 疲劳校核表格
                    int fatRowIdx = dgvFatigue.Rows.Add(
                        comp.Position.ToString("F0"),
                        comp.Diameter.ToString("F1"),
                        result.FatigueSafetySigma.ToString("F2"),
                        result.FatigueSafetyTau.ToString("F2"),
                        result.FatigueSafetyFactorS.ToString("F2"),
                        result.PassesFatigueCheck ? "合格" : "不合格"
                    );

                    var fatPassCell = dgvFatigue.Rows[fatRowIdx].Cells["colFatPass"];
                    if (result.PassesFatigueCheck)
                    {
                        fatPassCell.Style.ForeColor = Color.Green;
                        fatPassCell.Style.Font = new Font(dgvFatigue.Font, FontStyle.Bold);
                    }
                    else
                    {
                        fatPassCell.Style.ForeColor = Color.Red;
                        fatPassCell.Style.Font = new Font(dgvFatigue.Font, FontStyle.Bold);
                    }
                }

                // 默认显示强度校核页
                tabControlResults.SelectedTab = tabStrength;
                toolStripStatusLabel.Text = "计算完成";
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算出错: " + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "计算出错";
            }
        }

        /// <summary>
        /// 在弯矩图中插值
        /// </summary>
        private double InterpolateBendingMoment(BendingMomentDiagram bmd, double position)
        {
            double[] pos = bmd.Positions;
            double[] mom = bmd.Moments;

            if (pos == null || pos.Length == 0) return 0;
            if (position <= pos[0]) return mom[0];
            if (position >= pos[pos.Length - 1]) return mom[pos.Length - 1];

            // 二分查找
            int lo = 0, hi = pos.Length - 1;
            while (hi - lo > 1)
            {
                int mid = (lo + hi) / 2;
                if (pos[mid] <= position)
                    lo = mid;
                else
                    hi = mid;
            }

            // 线性插值
            double t = (position - pos[lo]) / (pos[hi] - pos[lo]);
            return mom[lo] + t * (mom[hi] - mom[lo]);
        }

        private double ParseDouble(object value)
        {
            if (value == null) return 0;
            double result;
            if (double.TryParse(value.ToString(), out result))
                return result;
            return 0;
        }
    }
}
