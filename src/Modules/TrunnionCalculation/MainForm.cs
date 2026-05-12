using System;
using System.Windows.Forms;

namespace Interop.Office.Core
{
    public partial class zhouj : Form
    {
        // 计算参数字段
        private double zd1;    // 许用剪切应力 A
        private double zd2;    // 许用剪切应力 tau
        private double zoop;   // 许用弯曲应力 sigma
        private double zoo1p;  // 辅助参数
        private double zoo0p;  // 辅助参数
        private double zoo11p; // 空心轴系数 (1-α^4)^(1/3) 或 (1-α^4)^(1/4)
        private double zooo;   // 指数 (1/3 或 1/4)
        private double tt1;    // 计算结果轴径
        private double tt2;    // 辅助计算值
        private double tt3;    // 中间计算值

        public zhouj()
        {
            InitializeComponent();
        }

        private void zhouj_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = get_mdico();
            }
            catch { }
            caizhi.Text = "碳素钢";
        }

        /// <summary>
        /// 获取图标（插件环境）
        /// </summary>
        private static System.Drawing.Icon get_mdico()
        {
            return System.Drawing.SystemIcons.Application;
        }

        // ========== 材质选择事件 ==========
        private void caizhi_SelectedIndexChanged(object sender, EventArgs e)
        {
            rm.Items.Clear();
            if (caizhi.Text == "碳素钢")
            {
                rm.Items.Add("400");
                rm.Items.Add("500");
                rm.Items.Add("600");
                rm.Items.Add("700");
                rm.Text = "400";
            }
            else if (caizhi.Text == "合金钢")
            {
                rm.Items.Add("800");
                rm.Items.Add("1000");
                rm.Text = "800";
            }
            else // 铸钢
            {
                rm.Items.Add("400");
                rm.Items.Add("500");
                rm.Text = "400";
            }
        }

        // ========== 转速等级选择事件 - 设置材料系数 ==========
        private void rm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rpmText = rm.Text;
            if (rpmText == "400")
            {
                if (caizhi.Text == "碳素钢")
                {
                    zd1 = 130; zd2 = 70; zoop = 40;
                }
                else // 合金钢
                {
                    zd1 = 100; zd2 = 50; zoop = 30;
                }
            }
            else if (rpmText == "500")
            {
                if (caizhi.Text == "碳素钢")
                {
                    zd1 = 170; zd2 = 75; zoop = 45;
                }
                else
                {
                    zd1 = 120; zd2 = 70; zoop = 40;
                }
            }
            else if (rpmText == "600")
            {
                zd1 = 200; zd2 = 95; zoop = 55;
            }
            else if (rpmText == "700")
            {
                zd1 = 230; zd2 = 110; zoop = 65;
            }
            else if (rpmText == "800")
            {
                zd1 = 270; zd2 = 130; zoop = 75;
            }
            else if (rpmText == "1000")
            {
                zd1 = 330; zd2 = 150; zoop = 90;
            }

            jisuan1();
        }

        // ========== 材料牌号选择事件 ==========
        private void c1_CheckedChanged(object sender, EventArgs e)
        {
            if (c1.Checked)
            {
                oo.Items.Clear();
                oo.Items.AddRange(new object[] { "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" });
                oo.Text = "20";
                a.Items.Clear();
                a.Items.AddRange(new object[] { "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "137", "138", "139", "140", "141", "142", "143", "144", "145", "146", "147", "148", "149" });
                a.Text = "135";
                jisuan();
            }
        }

        private void c2_CheckedChanged(object sender, EventArgs e)
        {
            if (c2.Checked)
            {
                oo.Items.Clear();
                oo.Items.AddRange(new object[] { "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35" });
                oo.Text = "25";
                a.Items.Clear();
                a.Items.AddRange(new object[] { "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135" });
                a.Text = "120";
                jisuan();
            }
        }

        private void c3_CheckedChanged(object sender, EventArgs e)
        {
            if (c3.Checked)
            {
                oo.Items.Clear();
                oo.Items.AddRange(new object[] { "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55" });
                oo.Text = "40";
                a.Items.Clear();
                a.Items.AddRange(new object[] { "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112" });
                a.Text = "105";
                jisuan();
            }
        }

        private void c4_CheckedChanged(object sender, EventArgs e)
        {
            if (c4.Checked)
            {
                oo.Items.Clear();
                oo.Items.AddRange(new object[] { "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60" });
                oo.Text = "50";
                a.Items.Clear();
                a.Items.AddRange(new object[] { "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105" });
                a.Text = "95";
                jisuan();
            }
        }

        private void c5_CheckedChanged(object sender, EventArgs e)
        {
            if (c5.Checked)
            {
                oo.Items.Clear();
                oo.Items.AddRange(new object[] { "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65" });
                oo.Text = "55";
                a.Items.Clear();
                a.Items.AddRange(new object[] { "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95" });
                a.Text = "85";
                jisuan();
            }
        }

        // ========== 实心/空心轴切换（tabPage13） ==========
        private void ns_CheckedChanged(object sender, EventArgs e)
        {
            if (ns.Checked)
            {
                // 实心轴：禁用α输入
                aa3.Enabled = false;
                aa3.Text = "";
            }
            else
            {
                // 空心轴：启用α输入
                aa3.Enabled = true;
            }
            jisuan();
        }

        // ========== 实心/空心轴切换（tabPage12） ==========
        private void xinzhou_CheckedChanged(object sender, EventArgs e)
        {
            if (xinzhou.Checked)
            {
                // 实心轴：隐藏键槽相关控件
                groupBox7.Visible = false;
            }
            else
            {
                // 转轴：显示键槽相关控件
                groupBox7.Visible = true;
            }
            jisuan1();
        }

        // ========== 传动/弯曲切换 ==========
        private void ws_CheckedChanged(object sender, EventArgs e)
        {
            if (ws.Checked)
            {
                // 传动：禁用弯矩输入
                m.Enabled = false;
                m.Text = "";
                t2.Enabled = true;
            }
            else
            {
                // 弯曲：启用弯矩输入
                m.Enabled = true;
                t2.Enabled = true;
            }
            jisuan1();
        }

        // ========== 键槽类型切换事件 ==========
        private void jiancaowan_CheckedChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        private void jiancao1wan_CheckedChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        private void jiancao2wan_CheckedChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        // ========== 载荷类型切换事件 ==========
        private void zhuandong_CheckedChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        private void pingwen_CheckedChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        private void bianhua_CheckedChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        private void bubian_CheckedChanged(object sender, EventArgs e)
        {
            jisuan();
        }

        private void xunhuan_CheckedChanged(object sender, EventArgs e)
        {
            jisuan();
        }

        private void shuangxiang_CheckedChanged(object sender, EventArgs e)
        {
            jisuan();
        }

        // ========== 输入变化事件 ==========
        private void t2_TextChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        private void m_TextChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        private void aa3_TextChanged(object sender, EventArgs e)
        {
            jisuan1();
        }

        private void p_TextChanged(object sender, EventArgs e)
        {
            jisuan();
        }

        private void n_TextChanged(object sender, EventArgs e)
        {
            jisuan();
        }

        private void aa2_TextChanged(object sender, EventArgs e)
        {
            jisuan();
        }

        private void a_SelectedIndexChanged(object sender, EventArgs e)
        {
            jisuan();
        }

        // ========== 扭转强度/刚度切换 ==========
        private void niuzhuanqiangdu_CheckedChanged(object sender, EventArgs e)
        {
            if (niuzhuanqiangdu.Checked)
            {
                // 扭转强度：显示许用切应力
                rr1.Visible = true;
                rr.Visible = true;
                b1.Visible = false;
                b.Visible = false;
                button1.Visible = false;
            }
            else
            {
                // 扭转刚度：显示B值
                rr1.Visible = false;
                rr.Visible = false;
                b1.Visible = true;
                b.Visible = true;
                button1.Visible = true;
            }
            jisuan();
        }

        // ========== 参考按钮 ==========
        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "对于要求精密、稳定的传动，可取φ=0.25~0.5(°)/m(B=109~129);";
            msg += System.Environment.NewLine + "对于一般传动，可取φp=0.5~1(°)/m(B=91.5~109);";
            msg += System.Environment.NewLine + "对于要求不高的传动，可取φp大于1(°)/m(B<91.5);";
            msg += System.Environment.NewLine + "起重机传动轴，φp=15'~20'/m;";
            msg += System.Environment.NewLine + "重型机床走刀轴，φp=5'/m。";
            MessageBox.Show(msg);
        }

        // ====================================================================
        // jisuan1() - 初步计算轴径（tabPage12：心轴/转轴计算）
        // ====================================================================
        private void jisuan1()
        {
            try
            {
                zooo = 1.0 / 3.0;

                if (xinzhou.Checked)
                {
                    // ===== 心轴模式 =====
                    if (ws.Checked)
                    {
                        // 实心轴 + 传动
                        // 检查扭矩输入
                        if (string.IsNullOrEmpty(t2.Text) || t2.Text == "-") { zhoujing1.Text = ""; return; }
                        double t2Val;
                        if (!double.TryParse(t2.Text, out t2Val)) { zhoujing1.Text = ""; return; }

                        // 选择应力系数
                        double stressCoeff;
                        if (shuangxiang.Checked)
                            stressCoeff = zoop;   // 双向旋转用σ
                        else if (zhuandong.Checked)
                            stressCoeff = zd1;    // 转动心轴用A
                        else
                            stressCoeff = zd2;    // 固定心轴用τ

                        if (stressCoeff <= 0) { zhoujing1.Text = ""; return; }

                        tt3 = t2Val / stressCoeff;
                        tt1 = 21.68 * Math.Pow(tt3, zooo);
                    }
                    else
                    {
                        // 心轴 + 弯曲（wk模式）
                        if (string.IsNullOrEmpty(m.Text) || m.Text == "-") { zhoujing1.Text = ""; return; }
                        if (string.IsNullOrEmpty(t2.Text) || t2.Text == "-") { zhoujing1.Text = ""; return; }
                        double mVal, t2Val;
                        if (!double.TryParse(m.Text, out mVal)) { zhoujing1.Text = ""; return; }
                        if (!double.TryParse(t2.Text, out t2Val)) { zhoujing1.Text = ""; return; }

                        double stressCoeff;
                        double loadFactor = 1.0;
                        if (shuangxiang.Checked)
                        {
                            stressCoeff = zoop;
                        }
                        else if (zhuandong.Checked)
                        {
                            stressCoeff = zd1;
                        }
                        else
                        {
                            stressCoeff = zd2;
                        }

                        // 弯扭合成: tt3 = sqrt(M² + (α*T)²) / stressCoeff
                        // 对于双向旋转 α=1, 对于脉动循环 α=0.3, 对于不变 α=0.6
                        if (bianhua.Checked)
                            loadFactor = 0.6;
                        else if (pingwen.Checked || bubian.Checked)
                            loadFactor = 1.0;
                        else if (xunhuan.Checked)
                            loadFactor = 0.3;

                        tt2 = loadFactor * t2Val;
                        tt3 = Math.Sqrt(mVal * mVal + tt2 * tt2) / stressCoeff;
                        tt1 = 21.68 * Math.Pow(tt3, zooo);
                    }

                    // 键槽调整（心轴模式不需要键槽调整）
                    zhoujing1.Text = tt1.ToString("F2");
                }
                else
                {
                    // ===== 转轴模式 =====
                    // 检查扭矩输入
                    if (string.IsNullOrEmpty(t2.Text) || t2.Text == "-") { zhoujing1.Text = ""; return; }
                    double t2Val;
                    if (!double.TryParse(t2.Text, out t2Val)) { zhoujing1.Text = ""; return; }

                    // 选择应力系数
                    double stressCoeff;
                    if (shuangxiang.Checked)
                        stressCoeff = zoop;
                    else if (zhuandong.Checked)
                        stressCoeff = zd1;
                    else
                        stressCoeff = zd2;

                    if (stressCoeff <= 0) { zhoujing1.Text = ""; return; }

                    if (ws.Checked)
                    {
                        // 转轴 + 传动
                        tt3 = t2Val / stressCoeff;
                        tt1 = 21.68 * Math.Pow(tt3, zooo);
                    }
                    else
                    {
                        // 转轴 + 弯曲
                        if (string.IsNullOrEmpty(m.Text) || m.Text == "-") { zhoujing1.Text = ""; return; }
                        double mVal;
                        if (!double.TryParse(m.Text, out mVal)) { zhoujing1.Text = ""; return; }

                        double loadFactor = 1.0;
                        if (bianhua.Checked)
                            loadFactor = 0.6;
                        else if (pingwen.Checked)
                            loadFactor = 1.0;
                        else if (xunhuan.Checked)
                            loadFactor = 0.3;

                        tt2 = loadFactor * t2Val;
                        tt3 = Math.Sqrt(mVal * mVal + tt2 * tt2) / stressCoeff;
                        tt1 = 21.68 * Math.Pow(tt3, zooo);
                    }

                    // 空心轴修正
                    if (wk.Checked)
                    {
                        if (!string.IsNullOrEmpty(aa3.Text) && aa3.Text != "-")
                        {
                            double alpha;
                            if (double.TryParse(aa3.Text, out alpha) && alpha > 0 && alpha < 1)
                            {
                                zoo11p = Math.Pow(1.0 - Math.Pow(alpha, 4), zooo);
                                if (zoo11p > 0) tt1 = tt1 / zoo11p;
                            }
                        }
                    }

                    // 键槽调整
                    if (jiancao1wan.Checked)
                    {
                        // 有一个键槽
                        if (tt1 > 30)
                            tt1 = tt1 * 1.07;  // d>30: +7%
                        else
                            tt1 = tt1 * 1.03;  // d<=30: +3%
                    }
                    else if (jiancao2wan.Checked)
                    {
                        // 有两个键槽
                        if (tt1 > 30)
                            tt1 = tt1 * 1.15;  // d>30: +15%
                        else
                            tt1 = tt1 * 1.07;  // d<=30: +7%
                    }

                    zhoujing1.Text = tt1.ToString("F2");
                }
            }
            catch
            {
                zhoujing1.Text = "";
            }
        }

        // ====================================================================
        // jisuan() - 主计算（tabPage13：按扭转强度或刚度计算）
        // ====================================================================
        private void jisuan()
        {
            try
            {
                double coeff;
                double exp;

                if (niuzhuanqiangdu.Checked)
                {
                    // 扭转强度
                    coeff = 17.2;
                    exp = 1.0 / 3.0;
                }
                else
                {
                    // 扭转刚度
                    coeff = 9.3;
                    exp = 1.0 / 4.0;
                }

                zooo = exp;

                if (ns.Checked)
                {
                    // ===== 实心轴 =====
                    if (tabControl3.SelectedTab == tabPage5)
                    {
                        // 按许用切应力/扭转角计算
                        if (string.IsNullOrEmpty(p.Text) || p.Text == "-") { t.Text = ""; return; }
                        if (string.IsNullOrEmpty(n.Text) || n.Text == "-") { t.Text = ""; return; }
                        double pVal, nVal;
                        if (!double.TryParse(p.Text, out pVal)) { t.Text = ""; return; }
                        if (!double.TryParse(n.Text, out nVal) || nVal <= 0) { t.Text = ""; return; }

                        double d = coeff * Math.Pow(pVal / nVal, exp);
                        t.Text = d.ToString("F2");
                    }
                    else
                    {
                        // 按系数计算
                        if (string.IsNullOrEmpty(p.Text) || p.Text == "-") { t.Text = ""; return; }
                        if (string.IsNullOrEmpty(n.Text) || n.Text == "-") { t.Text = ""; return; }
                        if (string.IsNullOrEmpty(a.Text)) { t.Text = ""; return; }
                        double pVal, nVal, aVal;
                        if (!double.TryParse(p.Text, out pVal)) { t.Text = ""; return; }
                        if (!double.TryParse(n.Text, out nVal) || nVal <= 0) { t.Text = ""; return; }
                        if (!double.TryParse(a.Text, out aVal)) { t.Text = ""; return; }

                        double d = aVal * Math.Pow(pVal / nVal, exp);
                        t.Text = d.ToString("F2");
                    }
                }
                else
                {
                    // ===== 空心轴 =====
                    if (string.IsNullOrEmpty(aa3.Text) || aa3.Text == "-") { t.Text = ""; return; }
                    double alpha;
                    if (!double.TryParse(aa3.Text, out alpha) || alpha <= 0 || alpha >= 1) { t.Text = ""; return; }

                    zoo11p = Math.Pow(1.0 - Math.Pow(alpha, 4), exp);

                    if (tabControl3.SelectedTab == tabPage5)
                    {
                        if (string.IsNullOrEmpty(p.Text) || p.Text == "-") { t.Text = ""; return; }
                        if (string.IsNullOrEmpty(n.Text) || n.Text == "-") { t.Text = ""; return; }
                        double pVal, nVal;
                        if (!double.TryParse(p.Text, out pVal)) { t.Text = ""; return; }
                        if (!double.TryParse(n.Text, out nVal) || nVal <= 0) { t.Text = ""; return; }

                        double d = coeff * Math.Pow(pVal / nVal, exp) / zoo11p;
                        t.Text = d.ToString("F2");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(p.Text) || p.Text == "-") { t.Text = ""; return; }
                        if (string.IsNullOrEmpty(n.Text) || n.Text == "-") { t.Text = ""; return; }
                        if (string.IsNullOrEmpty(a.Text)) { t.Text = ""; return; }
                        double pVal, nVal, aVal;
                        if (!double.TryParse(p.Text, out pVal)) { t.Text = ""; return; }
                        if (!double.TryParse(n.Text, out nVal) || nVal <= 0) { t.Text = ""; return; }
                        if (!double.TryParse(a.Text, out aVal)) { t.Text = ""; return; }

                        double d = aVal * Math.Pow(pVal / nVal, exp) / zoo11p;
                        t.Text = d.ToString("F2");
                    }
                }

                // 键槽调整（对t.Text结果）
                if (!string.IsNullOrEmpty(t.Text))
                {
                    double currentD;
                    if (double.TryParse(t.Text, out currentD))
                    {
                        if (jiancao1niu.Checked)
                        {
                            if (currentD > 30)
                                currentD = currentD * 1.07;
                            else
                                currentD = currentD * 1.03;
                        }
                        else if (jiancao2niu.Checked)
                        {
                            if (currentD > 30)
                                currentD = currentD * 1.15;
                            else
                                currentD = currentD * 1.07;
                        }
                        t.Text = currentD.ToString("F2");
                    }
                }
            }
            catch
            {
                t.Text = "";
            }
        }
    }
}
