using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BearingDesign
{
    public partial class MainForm : Form
    {
        private List<BearingRecord> bearings = new List<BearingRecord>();
        private List<XYCoeff> xyTable = new List<XYCoeff>();
        private string currentType = "深沟球轴承";
        private bool isThrust = false;
        private bool isRoller = false;

        public MainForm()
        {
            InitializeComponent();
            LoadBearingData();
            OnBearingTypeChanged(null, null);
        }

        private void LoadBearingData()
        {
            try
            {
                cmbBearingType.Items.AddRange(new object[] {
                    "深沟球轴承", "角接触球轴承(15°)", "角接触球轴承(25°)",
                    "角接触球轴承(40°)", "圆锥滚子轴承",
                    "推力球轴承", "推力滚子轴承", "调心球轴承"
                });
                cmbBearingType.SelectedIndex = 0;

                cmbLoadType.Items.AddRange(new object[] {
                    "平稳", "轻微冲击", "中等冲击", "强烈冲击", "剧烈冲击"
                });
                cmbLoadType.SelectedIndex = 2; // 中等冲击

                cmbLubrication.Items.AddRange(new object[] { "脂润滑", "油润滑" });
                cmbLubrication.SelectedIndex = 0;

                cmbReliability.Items.AddRange(new object[] {
                    "90%", "95%", "96%", "97%", "98%", "99%"
                });
                cmbReliability.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据出错: " + ex.Message);
            }
        }

        private void OnBearingTypeChanged(object sender, EventArgs e)
        {
            if (cmbBearingType.SelectedIndex < 0) return;
            currentType = cmbBearingType.SelectedItem.ToString();
            isThrust = currentType.Contains("推力");
            isRoller = currentType.Contains("滚子") && !currentType.Contains("球");

            // 加载对应的轴承数据
            switch (currentType)
            {
                case "深沟球轴承":
                    bearings = BearingRepository.LoadBearingTable("deep_groove.txt");
                    xyTable = BearingRepository.LoadXYTable("sgqxy.txt");
                    break;
                case "角接触球轴承(15°)":
                    bearings = BearingRepository.LoadBearingTable("angular_contact.txt");
                    xyTable = BearingRepository.LoadXYTable("jqa15xy.txt");
                    break;
                case "角接触球轴承(25°)":
                    bearings = BearingRepository.LoadBearingTable("angular_contact.txt");
                    xyTable = BearingRepository.LoadXYTable("jqa10xy.txt");
                    break;
                case "角接触球轴承(40°)":
                    bearings = BearingRepository.LoadBearingTable("angular_contact.txt");
                    xyTable = BearingRepository.LoadXYTable("jq20a45xy.txt");
                    break;
                case "圆锥滚子轴承":
                    bearings = BearingRepository.LoadBearingTable("tapered_roller.txt");
                    xyTable = BearingRepository.LoadXYTable("sgqxy.txt"); // 使用类似系数
                    break;
                case "推力球轴承":
                    bearings = BearingRepository.LoadBearingTable("thrust_ball.txt");
                    xyTable = BearingRepository.LoadXYTable("tlqxy.txt");
                    break;
                case "推力滚子轴承":
                    bearings = BearingRepository.LoadBearingTable("thrust_roller.txt");
                    xyTable = BearingRepository.LoadXYTable("tlgxy.txt");
                    break;
                case "调心球轴承":
                    bearings = BearingRepository.LoadBearingTable("self_aligning.txt");
                    xyTable = BearingRepository.LoadXYTable("sgqxy.txt");
                    break;
            }

            // 更新轴承型号列表
            cmbBearingCode.Items.Clear();
            foreach (var b in bearings)
                cmbBearingCode.Items.Add(b.Code);
            if (cmbBearingCode.Items.Count > 0)
                cmbBearingCode.SelectedIndex = 0;

            txtResult.Clear();
            lblInfo.Text = string.Format("已加载 {0} 条 {1} 数据", bearings.Count, currentType);
        }

        private void OnAutoSelect(object sender, EventArgs e)
        {
            double d1;
            if (!double.TryParse(txtShaftDia.Text, out d1) || d1 <= 0)
            {
                MessageBox.Show("请输入有效轴径"); return;
            }

            // 根据轴径筛选轴承，找出内径 >= 轴径的
            var candidates = bearings.FindAll(b => b.d >= d1);
            if (candidates.Count == 0)
            {
                MessageBox.Show("未找到合适内径的轴承"); return;
            }

            // 按内径排序，取第一个
            candidates.Sort((a, b2) => a.d.CompareTo(b2.d));
            var sel = candidates[0];
            int idx = bearings.IndexOf(sel);
            cmbBearingCode.SelectedIndex = idx;
            OnCalculate(null, null);
        }

        private void OnCalculate(object sender, EventArgs e)
        {
            if (cmbBearingCode.SelectedIndex < 0) return;
            var bearing = bearings[cmbBearingCode.SelectedIndex];

            double Fr, Fa, n, LhReq, fp, temp, reliability;
            if (!double.TryParse(txtRadialForce.Text, out Fr) || Fr < 0)
            { MessageBox.Show("请输入有效径向力"); return; }
            if (!double.TryParse(txtAxialForce.Text, out Fa) || Fa < 0)
            { MessageBox.Show("请输入有效轴向力"); return; }
            if (!double.TryParse(txtSpeed.Text, out n) || n <= 0)
            { MessageBox.Show("请输入有效转速"); return; }
            if (!double.TryParse(txtLife.Text, out LhReq) || LhReq <= 0)
            { MessageBox.Show("请输入有效要求寿命"); return; }

            fp = BearingRepository.GetLoadFactor(cmbLoadType.SelectedItem.ToString());
            temp = double.Parse(txtTemp.Text);
            reliability = double.Parse(cmbReliability.SelectedItem.ToString().Replace("%", ""));

            double ft = BearingRepository.GetTempFactor(temp);
            double a1 = BearingRepository.GetReliabilityFactor(reliability);
            double a2 = 1.0; // 特殊轴承性能修正
            double a3 = 1.0; // 运转条件修正

            var sb = new StringBuilder();
            sb.AppendLine("=== " + currentType + " 校核计算 ===");
            sb.AppendLine();
            sb.AppendFormat("轴承型号: {0}\n", bearing.Code);
            sb.AppendFormat("内径 d={0}mm, 外径 D={1}mm", bearing.d, bearing.D);
            if (bearing.B > 0) sb.AppendFormat(", 宽(高) B={0}mm", bearing.B);
            sb.AppendLine();
            sb.AppendFormat("额定动载荷 C={0} kN ({1} N)\n", bearing.Cr, bearing.Cr * 1000);
            sb.AppendFormat("额定静载荷 C0={0} kN ({1} N)\n", bearing.Cor, bearing.Cor * 1000);
            sb.AppendFormat("极限转速(脂)={0} r/min, (油)={1} r/min\n", bearing.NlimZ, bearing.NlimY);
            sb.AppendLine();

            // 当量动载荷计算
            double P, X, Y, eVal;
            double E = isRoller ? 10.0 / 3.0 : 3.0; // 寿命指数

            if (isThrust)
            {
                // 推力轴承: P = Fa (仅轴向载荷)
                P = Fa;
                X = 0; Y = 1;
                eVal = 0;
                sb.AppendFormat("推力轴承: P = Fa = {0:F1} N\n", P);
            }
            else
            {
                // 向心轴承: 判断 Fa/Fr 与 e 的关系
                double faCorRatio = (bearing.Cor > 0) ? Fa / (bearing.Cor * 1000) : 0;
                BearingRepository.FindXY(xyTable, faCorRatio, out X, out Y, out eVal);

                double ratio = (Fr > 0) ? Fa / Fr : double.MaxValue;
                if (ratio <= eVal)
                {
                    X = 1; Y = 0;
                    P = Fr;
                    sb.AppendFormat("Fa/Fr = {0:F3} <= e = {1:F2}\n", ratio, eVal);
                    sb.AppendFormat("X=1, Y=0\n");
                }
                else
                {
                    P = fp * (X * Fr + Y * Fa);
                    sb.AppendFormat("Fa/Fr = {0:F3} > e = {1:F2}\n", ratio, eVal);
                    sb.AppendFormat("X={0:F2}, Y={1:F2}\n", X, Y);
                }
                sb.AppendFormat("载荷系数 fp={0}\n", fp);
            }

            P = fp * P;
            sb.AppendFormat("\n当量动载荷 P = {0:F1} N\n", P);
            sb.AppendLine();

            // 寿命计算
            double C = bearing.Cr * 1000; // N
            double L10 = Math.Pow(C / P, E); // 10^6 转
            double Lh = L10 * 1e6 / (60.0 * n);
            double Lna = a1 * a2 * a3 * L10;

            sb.AppendFormat("温度系数 ft={0:F2}\n", ft);
            sb.AppendFormat("可靠性系数 a1={0:F2}\n", a1);
            sb.AppendFormat("寿命指数 E={0:F3}\n", E);
            sb.AppendLine();
            sb.AppendFormat("基本额定寿命 L10 = (C/P)^E\n");
            sb.AppendFormat("  = ({0:F0}/{1:F1})^{2:F3}\n", C, P, E);
            sb.AppendFormat("  = {0:F2} (×10^6 转)\n", L10);
            sb.AppendFormat("基本额定寿命 Lh = {0:F1} h\n", Lh);
            sb.AppendFormat("修正额定寿命 Lna = a1×L10 = {0:F2} (×10^6 转)\n", Lna);
            sb.AppendFormat("修正额定寿命 Lna_h = {0:F1} h\n", Lna * 1e6 / (60.0 * n));
            sb.AppendLine();

            // 极限转速校核
            int nlim = cmbLubrication.SelectedIndex == 0 ? bearing.NlimZ : bearing.NlimY;
            sb.AppendFormat("极限转速({0}) nlim = {1} r/min\n",
                cmbLubrication.SelectedIndex == 0 ? "脂" : "油", nlim);
            sb.AppendFormat("工作转速 n = {0} r/min\n", n);
            bool speedOk = n <= nlim;
            sb.AppendFormat("转速校核: n {0} nlim, {1}\n", speedOk ? "<=" : ">",
                speedOk ? "满足" : "不满足");
            sb.AppendLine();

            // 结论
            bool lifeOk = Lh >= LhReq;
            sb.AppendLine("=== 校核结论 ===");
            sb.AppendFormat("要求寿命 Lh'={0} h\n", LhReq);
            sb.AppendFormat("实际寿命 Lh={0:F1} h\n", Lh);
            sb.AppendFormat("寿命校核: {0}\n", lifeOk ? "满足" : "不满足");
            sb.AppendFormat("转速校核: {0}\n", speedOk ? "满足" : "不满足");
            sb.AppendLine();
            sb.AppendFormat("结论: {0}", (lifeOk && speedOk) ? "合格" : "不合格，需重新选型");

            txtResult.Text = sb.ToString();
        }

        private void OnBearingSelected(object sender, EventArgs e)
        {
            if (cmbBearingCode.SelectedIndex < 0) return;
            var bearing = bearings[cmbBearingCode.SelectedIndex];
            lblBearingInfo.Text = string.Format(
                "d={0}mm D={1}mm B={2}mm\nC={3}kN C0={4}kN\nnlim(脂)={5} nlim(油)={6}",
                bearing.d, bearing.D, bearing.B,
                bearing.Cr, bearing.Cor, bearing.NlimZ, bearing.NlimY);
        }
    }
}
