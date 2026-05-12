using System;
using System.Text;
using System.Windows.Forms;

namespace SpringDesign
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "迈迪弹簧设计系统";
            InitMaterialComboBoxes();
            SetDefaults();
        }

        /// <summary>
        /// 初始化材料下拉框
        /// </summary>
        private void InitMaterialComboBoxes()
        {
            var materials = SpringMaterialDatabase.GetAllMaterials();

            cboCompMaterial.DataSource = new System.Collections.Generic.List<SpringMaterial>(materials);
            cboCompMaterial.DisplayMember = "Name";
            cboCompMaterial.SelectedIndex = 4; // 默认选中 65Mn

            cboExtMaterial.DataSource = new System.Collections.Generic.List<SpringMaterial>(materials);
            cboExtMaterial.DisplayMember = "Name";
            cboExtMaterial.SelectedIndex = 4;

            cboTorMaterial.DataSource = new System.Collections.Generic.List<SpringMaterial>(materials);
            cboTorMaterial.DisplayMember = "Name";
            cboTorMaterial.SelectedIndex = 4;
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        private void SetDefaults()
        {
            // 压缩弹簧默认值
            cboCompLoadCat.SelectedIndex = 1; // II类载荷
            cboCompEndType.SelectedIndex = 0; // YI型
            txtCompD.Text = "3";
            txtCompDm.Text = "20";
            txtCompN.Text = "8";
            txtCompF1.Text = "50";
            txtCompFn.Text = "150";

            // 拉伸弹簧默认值
            cboExtLoadCat.SelectedIndex = 1;
            txtExtD.Text = "3";
            txtExtDm.Text = "20";
            txtExtN.Text = "8";
            txtExtF1.Text = "50";
            txtExtFn.Text = "150";
            txtExtInitTension.Text = "10";

            // 扭转弹簧默认值
            cboTorLoadCat.SelectedIndex = 1;
            txtTorD.Text = "3";
            txtTorDm.Text = "20";
            txtTorN.Text = "8";
            txtTorM.Text = "500";
        }

        #region 压缩弹簧事件

        private void btnCompCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double d = ParseDouble(txtCompD, "丝径 d");
                double D = ParseDouble(txtCompDm, "中径 D");
                int n = ParseInt(txtCompN, "有效圈数 n");
                double F1 = 0, Fn = 0;

                if (!string.IsNullOrWhiteSpace(txtCompF1.Text))
                    F1 = ParseDouble(txtCompF1, "最小工作载荷 P1");
                if (!string.IsNullOrWhiteSpace(txtCompFn.Text))
                    Fn = ParseDouble(txtCompFn, "最大工作载荷 Pn");

                if (D <= d)
                {
                    MessageBox.Show("中径 D 必须大于丝径 d", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var mat = (SpringMaterial)cboCompMaterial.SelectedItem;
                int loadCat = cboCompLoadCat.SelectedIndex + 1;
                double tauAllow = SpringMaterialDatabase.GetAllowableShearStress(mat, loadCat);

                string endType = (cboCompEndType.SelectedIndex == 0) ? "yI" : "yII";

                var result = SpringCalculator.CheckCompression(d, D, n, mat.G, tauAllow, F1, Fn, endType);
                ShowCompResult(result, mat, loadCat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "计算错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCompReset_Click(object sender, EventArgs e)
        {
            txtCompD.Text = "";
            txtCompDm.Text = "";
            txtCompN.Text = "";
            txtCompF1.Text = "";
            txtCompFn.Text = "";
            dgvCompResult.Rows.Clear();
            txtCompStatus.Text = "";
            cboCompLoadCat.SelectedIndex = 1;
            cboCompEndType.SelectedIndex = 0;
            if (cboCompMaterial.Items.Count > 4)
                cboCompMaterial.SelectedIndex = 4;
        }

        private void ShowCompResult(CompressionExtensionResult r, SpringMaterial mat, int loadCat)
        {
            dgvCompResult.Rows.Clear();

            AddRow(dgvCompResult, "材料", mat.Name, "-", mat.Description);
            AddRow(dgvCompResult, "载荷类别", GetLoadCatDesc(loadCat), "-", "");
            AddRow(dgvCompResult, "许用切应力 [tau]", r.tauAllow.ToString("F1"), "MPa", "");
            AddRow(dgvCompResult, "", "", "", ""); // 分隔行
            AddRow(dgvCompResult, "丝径 d", r.d.ToString("F3"), "mm", "");
            AddRow(dgvCompResult, "中径 D", r.D.ToString("F3"), "mm", "");
            AddRow(dgvCompResult, "有效圈数 n", r.n.ToString(), "-", "");
            AddRow(dgvCompResult, "端部型式", r.endType, "-", "");
            AddRow(dgvCompResult, "总圈数 n1", r.n_total.ToString("F1"), "-", "");
            AddRow(dgvCompResult, "", "", "", "");
            AddRow(dgvCompResult, "旋绕比 C = D/d", r.C.ToString("F3"), "-", "推荐范围 4~16");
            AddRow(dgvCompResult, "曲度系数 K (Wahl)", r.K.ToString("F4"), "-", "K=(4C-1)/(4C-4)+0.615/C");
            AddRow(dgvCompResult, "弹簧刚度 P'", r.k.ToString("F3"), "N/mm", "k=G*d^4/(8*D^3*n)");
            AddRow(dgvCompResult, "内径 D1 = D-d", r.D1.ToString("F3"), "mm", "");
            AddRow(dgvCompResult, "外径 D2 = D+d", r.D2.ToString("F3"), "mm", "");
            AddRow(dgvCompResult, "", "", "", "");
            AddRow(dgvCompResult, "最大工作载荷 P_max", r.F_max.ToString("F2"), "N", "F_max=tau_allow*pi*d^3/(8*K*D)");
            AddRow(dgvCompResult, "自由高度 H0", r.H0.ToString("F2"), "mm", "H0=n*t+2*d (YI型)");
            AddRow(dgvCompResult, "压并高度 Hs", r.Hs.ToString("F2"), "mm", "Hs=(n+1.5)*d (YI型)");
            AddRow(dgvCompResult, "高径比 H0/D", r.stabilityRatio.ToString("F3"), "-", "要求 <=5.3 (两端固定)");
            AddRow(dgvCompResult, "展开长度 L", r.L_wire.ToString("F1"), "mm", "");

            if (r.F1 > 0)
            {
                AddRow(dgvCompResult, "", "", "", "");
                AddRow(dgvCompResult, "最小工作载荷 P1", r.F1.ToString("F2"), "N", "");
                AddRow(dgvCompResult, "P1 下变形量 f1", r.f1.ToString("F3"), "mm", "f1=P1/k");
                AddRow(dgvCompResult, "P1 下切应力 tau1",
                    (8.0 * r.K * r.F1 * r.D / (Math.PI * Math.Pow(r.d, 3))).ToString("F1"), "MPa", "");
            }

            if (r.Fn > 0)
            {
                AddRow(dgvCompResult, "最大工作载荷 Pn", r.Fn.ToString("F2"), "N", "");
                AddRow(dgvCompResult, "Pn 下变形量 fn", r.fn.ToString("F3"), "mm", "fn=Pn/k");
                AddRow(dgvCompResult, "Pn 下切应力 tau_n", r.tau_max.ToString("F1"), "MPa", "tau=8*K*F*D/(pi*d^3)");
            }

            AddRow(dgvCompResult, "", "", "", "");
            AddRow(dgvCompResult, "工作极限载荷 Pj", r.Fj.ToString("F2"), "N", "Pj=1.25*P_max");
            AddRow(dgvCompResult, "Pj 下变形量 fj", r.fj.ToString("F3"), "mm", "");
            AddRow(dgvCompResult, "Pj 下切应力 tau_j", r.tau_j.ToString("F1"), "MPa", "");

            if (r.F1 > 0 && r.Fn > 0)
            {
                AddRow(dgvCompResult, "", "", "", "");
                AddRow(dgvCompResult, "平均切应力 tau_m", r.tau_m.ToString("F1"), "MPa", "疲劳校核参数");
                AddRow(dgvCompResult, "切应力幅 tau_a", r.tau_a.ToString("F1"), "MPa", "疲劳校核参数");
            }

            // 校核结果
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("旋绕比 C={0:F2}", r.C);
            if (r.C < 4 || r.C > 16)
                sb.Append(" [警告: 推荐范围 4~16]");
            else
                sb.Append(" [合格]");

            sb.AppendFormat("  |  高径比 b={0:F2}", r.stabilityRatio);
            if (!r.stabilityOk)
                sb.Append(" [警告: 超过5.3，需加导杆或导套]");
            else
                sb.Append(" [合格]");

            if (r.Fn > 0)
            {
                double safetyFactor = SpringCalculator.CalcStaticSafetyFactor(r.tauAllow, r.tau_max);
                sb.AppendFormat("  |  静强度安全系数 n={0:F2}", safetyFactor);
                if (safetyFactor < 1.0)
                    sb.Append(" [不合格]");
                else
                    sb.Append(" [合格]");
            }

            txtCompStatus.Text = sb.ToString();
        }

        #endregion

        #region 拉伸弹簧事件

        private void btnExtCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double d = ParseDouble(txtExtD, "丝径 d");
                double D = ParseDouble(txtExtDm, "中径 D");
                int n = ParseInt(txtExtN, "有效圈数 n");
                double F1 = 0, Fn = 0, F0 = 0;

                if (!string.IsNullOrWhiteSpace(txtExtF1.Text))
                    F1 = ParseDouble(txtExtF1, "最小工作载荷 P1");
                if (!string.IsNullOrWhiteSpace(txtExtFn.Text))
                    Fn = ParseDouble(txtExtFn, "最大工作载荷 Pn");
                if (!string.IsNullOrWhiteSpace(txtExtInitTension.Text))
                    F0 = ParseDouble(txtExtInitTension, "初拉力 F0");

                if (D <= d)
                {
                    MessageBox.Show("中径 D 必须大于丝径 d", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var mat = (SpringMaterial)cboExtMaterial.SelectedItem;
                int loadCat = cboExtLoadCat.SelectedIndex + 1;
                // 拉伸弹簧许用应力取压缩的 0.8 倍
                double tauAllow = SpringMaterialDatabase.GetAllowableShearStress(mat, loadCat) * 0.8;

                var result = SpringCalculator.CheckExtension(d, D, n, mat.G, tauAllow, F1, Fn, F0);
                ShowExtResult(result, mat, loadCat, F0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "计算错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExtReset_Click(object sender, EventArgs e)
        {
            txtExtD.Text = "";
            txtExtDm.Text = "";
            txtExtN.Text = "";
            txtExtF1.Text = "";
            txtExtFn.Text = "";
            txtExtInitTension.Text = "";
            dgvExtResult.Rows.Clear();
            txtExtStatus.Text = "";
            cboExtLoadCat.SelectedIndex = 1;
            if (cboExtMaterial.Items.Count > 4)
                cboExtMaterial.SelectedIndex = 4;
        }

        private void ShowExtResult(CompressionExtensionResult r, SpringMaterial mat, int loadCat, double F0)
        {
            dgvExtResult.Rows.Clear();

            AddRow(dgvExtResult, "材料", mat.Name, "-", mat.Description);
            AddRow(dgvExtResult, "载荷类别", GetLoadCatDesc(loadCat), "-", "拉伸弹簧许用应力取0.8倍");
            AddRow(dgvExtResult, "许用切应力 [tau]", r.tauAllow.ToString("F1"), "MPa", "");
            AddRow(dgvExtResult, "初拉力 F0", F0.ToString("F2"), "N", "");
            AddRow(dgvExtResult, "", "", "", "");
            AddRow(dgvExtResult, "丝径 d", r.d.ToString("F3"), "mm", "");
            AddRow(dgvExtResult, "中径 D", r.D.ToString("F3"), "mm", "");
            AddRow(dgvExtResult, "有效圈数 n", r.n.ToString(), "-", "拉伸弹簧总圈数=有效圈数");
            AddRow(dgvExtResult, "", "", "", "");
            AddRow(dgvExtResult, "旋绕比 C = D/d", r.C.ToString("F3"), "-", "推荐范围 4~16");
            AddRow(dgvExtResult, "曲度系数 K (Wahl)", r.K.ToString("F4"), "-", "");
            AddRow(dgvExtResult, "弹簧刚度 P'", r.k.ToString("F3"), "N/mm", "k=G*d^4/(8*D^3*n)");
            AddRow(dgvExtResult, "内径 D1", r.D1.ToString("F3"), "mm", "");
            AddRow(dgvExtResult, "外径 D2", r.D2.ToString("F3"), "mm", "");
            AddRow(dgvExtResult, "", "", "", "");
            AddRow(dgvExtResult, "最大工作载荷 P_max", r.F_max.ToString("F2"), "N", "");
            AddRow(dgvExtResult, "自由长度 L0", r.H0.ToString("F2"), "mm", "含钩环");

            if (r.F1 > 0)
            {
                AddRow(dgvExtResult, "", "", "", "");
                AddRow(dgvExtResult, "最小工作载荷 P1", r.F1.ToString("F2"), "N", "");
                AddRow(dgvExtResult, "P1 下变形量 f1", r.f1.ToString("F3"), "mm", "f1=(P1-F0)/k");
            }

            if (r.Fn > 0)
            {
                AddRow(dgvExtResult, "最大工作载荷 Pn", r.Fn.ToString("F2"), "N", "");
                AddRow(dgvExtResult, "Pn 下变形量 fn", r.fn.ToString("F3"), "mm", "fn=(Pn-F0)/k");
                AddRow(dgvExtResult, "Pn 下切应力 tau_n", r.tau_max.ToString("F1"), "MPa", "");
            }

            AddRow(dgvExtResult, "", "", "", "");
            AddRow(dgvExtResult, "工作极限载荷 Pj", r.Fj.ToString("F2"), "N", "");
            AddRow(dgvExtResult, "展开长度 L", r.L_wire.ToString("F1"), "mm", "含钩环展开");

            // 校核结果
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("旋绕比 C={0:F2}", r.C);
            if (r.C < 4 || r.C > 16)
                sb.Append(" [警告: 推荐范围 4~16]");
            else
                sb.Append(" [合格]");

            if (r.Fn > 0)
            {
                double safetyFactor = SpringCalculator.CalcStaticSafetyFactor(r.tauAllow, r.tau_max);
                sb.AppendFormat("  |  静强度安全系数 n={0:F2}", safetyFactor);
                if (safetyFactor < 1.0)
                    sb.Append(" [不合格]");
                else
                    sb.Append(" [合格]");
            }

            txtExtStatus.Text = sb.ToString();
        }

        #endregion

        #region 扭转弹簧事件

        private void btnTorCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double d = ParseDouble(txtTorD, "丝径 d");
                double D = ParseDouble(txtTorDm, "中径 D");
                int n = ParseInt(txtTorN, "有效圈数 n");
                double M = 0;

                if (!string.IsNullOrWhiteSpace(txtTorM.Text))
                    M = ParseDouble(txtTorM, "工作扭矩 M");

                if (D <= d)
                {
                    MessageBox.Show("中径 D 必须大于丝径 d", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var mat = (SpringMaterial)cboTorMaterial.SelectedItem;
                int loadCat = cboTorLoadCat.SelectedIndex + 1;
                double sigmaAllow = SpringMaterialDatabase.GetAllowableBendingStress(mat, loadCat);

                var result = SpringCalculator.CalcTorsion(d, D, n, mat.E, sigmaAllow, M);
                ShowTorResult(result, mat, loadCat, M);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "计算错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTorReset_Click(object sender, EventArgs e)
        {
            txtTorD.Text = "";
            txtTorDm.Text = "";
            txtTorN.Text = "";
            txtTorM.Text = "";
            dgvTorResult.Rows.Clear();
            txtTorStatus.Text = "";
            cboTorLoadCat.SelectedIndex = 1;
            if (cboTorMaterial.Items.Count > 4)
                cboTorMaterial.SelectedIndex = 4;
        }

        private void ShowTorResult(TorsionResult r, SpringMaterial mat, int loadCat, double M)
        {
            dgvTorResult.Rows.Clear();

            AddRow(dgvTorResult, "材料", mat.Name, "-", mat.Description);
            AddRow(dgvTorResult, "载荷类别", GetLoadCatDesc(loadCat), "-", "");
            AddRow(dgvTorResult, "许用弯曲应力 [sigma]", r.sigmaAllow.ToString("F1"), "MPa", "");
            AddRow(dgvTorResult, "", "", "", "");
            AddRow(dgvTorResult, "丝径 d", r.d.ToString("F3"), "mm", "");
            AddRow(dgvTorResult, "中径 D", r.D.ToString("F3"), "mm", "");
            AddRow(dgvTorResult, "有效圈数 n", r.n.ToString(), "-", "");
            AddRow(dgvTorResult, "", "", "", "");
            AddRow(dgvTorResult, "旋绕比 C = D/d", r.C.ToString("F3"), "-", "推荐范围 4~16");
            AddRow(dgvTorResult, "曲度系数 Ki", r.Ki.ToString("F4"), "-", "Ki=(4C^2-C-1)/(4C*(C-1))");
            AddRow(dgvTorResult, "扭转刚度 T'", r.k.ToString("F4"), "N*mm/deg", "T'=E*d^4/(10.186*D*n)");
            AddRow(dgvTorResult, "扭转刚度 (弧度)", r.k_rad.ToString("F2"), "N*mm/rad", "");
            AddRow(dgvTorResult, "内径 D1", r.D1.ToString("F3"), "mm", "");
            AddRow(dgvTorResult, "外径 D2", r.D2.ToString("F3"), "mm", "");
            AddRow(dgvTorResult, "", "", "", "");
            AddRow(dgvTorResult, "最大扭矩 M_max", r.M_max.ToString("F2"), "N*mm", "M_max=sigma_allow*pi*d^3/(32*Ki)");
            AddRow(dgvTorResult, "最大扭转角 alpha_max", r.alpha_max.ToString("F2"), "deg", "alpha_max=M_max/T'");
            AddRow(dgvTorResult, "自由长度 L0", r.L0.ToString("F2"), "mm", "紧密缠绕时");
            AddRow(dgvTorResult, "钢丝展开长度 L", r.L_wire.ToString("F1"), "mm", "");

            if (M > 0)
            {
                AddRow(dgvTorResult, "", "", "", "");
                AddRow(dgvTorResult, "工作扭矩 M", M.ToString("F2"), "N*mm", "");
                AddRow(dgvTorResult, "工作弯曲应力 sigma", r.sigma_max.ToString("F1"), "MPa", "sigma=32*M*Ki/(pi*d^3)");
                double torsionAngle = M / r.k;
                AddRow(dgvTorResult, "工作扭转角 alpha", torsionAngle.ToString("F2"), "deg", "alpha=M/T'");
            }

            // 校核结果
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("旋绕比 C={0:F2}", r.C);
            if (r.C < 4 || r.C > 16)
                sb.Append(" [警告: 推荐范围 4~16]");
            else
                sb.Append(" [合格]");

            if (M > 0)
            {
                double safetyFactor = r.sigmaAllow / r.sigma_max;
                sb.AppendFormat("  |  弯曲强度安全系数 n={0:F2}", safetyFactor);
                if (safetyFactor < 1.0)
                    sb.Append(" [不合格]");
                else
                    sb.Append(" [合格]");
            }

            txtTorStatus.Text = sb.ToString();
        }

        #endregion

        #region 辅助方法

        private double ParseDouble(TextBox txt, string paramName)
        {
            double val;
            if (!double.TryParse(txt.Text.Trim(), out val))
                throw new ArgumentException(string.Format("参数 \"{0}\" 的值 \"{1}\" 不是有效的数字", paramName, txt.Text));
            if (double.IsNaN(val) || double.IsInfinity(val))
                throw new ArgumentException(string.Format("参数 \"{0}\" 的值无效", paramName));
            return val;
        }

        private int ParseInt(TextBox txt, string paramName)
        {
            int val;
            if (!int.TryParse(txt.Text.Trim(), out val))
                throw new ArgumentException(string.Format("参数 \"{0}\" 的值 \"{1}\" 不是有效的整数", paramName, txt.Text));
            return val;
        }

        private void AddRow(DataGridView dgv, string param, string value, string unit, string desc)
        {
            dgv.Rows.Add(param, value, unit, desc);
        }

        private string GetLoadCatDesc(int cat)
        {
            switch (cat)
            {
                case 1: return "I类载荷 (N<10000)";
                case 2: return "II类载荷 (10000<=N<1000000)";
                case 3: return "III类载荷 (N<=1000000)";
                default: return "";
            }
        }

        #endregion
    }
}
