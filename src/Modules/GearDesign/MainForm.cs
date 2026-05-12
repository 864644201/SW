using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GearDesign
{
    public partial class MainForm : Form
    {
        private readonly GearCalculator _calculator;
        private readonly List<GearMaterial> _materials;

        public MainForm()
        {
            InitializeComponent();
            _calculator = new GearCalculator();
            _materials = GearMaterial.GetPredefinedMaterials();
            LoadMaterials();
        }

        /// <summary>
        /// 加载材料列表到下拉框
        /// </summary>
        private void LoadMaterials()
        {
            cmbMat1.Items.Clear();
            cmbMat2.Items.Clear();

            foreach (var mat in _materials)
            {
                cmbMat1.Items.Add(mat);
                cmbMat2.Items.Add(mat);
            }

            // 默认选择: 小齿轮 40Cr调质+表面淬火, 大齿轮 45钢调质
            cmbMat1.SelectedIndex = FindMaterialIndex("40Cr", "调质+表面淬火");
            cmbMat2.SelectedIndex = FindMaterialIndex("45钢", "调质");

            cmbMat1.SelectedIndexChanged += cmbMat1_SelectedIndexChanged;
            cmbMat2.SelectedIndexChanged += cmbMat2_SelectedIndexChanged;
            UpdateMaterialInfo();
        }

        private int FindMaterialIndex(string name, string heatTreatment)
        {
            for (int i = 0; i < _materials.Count; i++)
            {
                if (_materials[i].Name == name && _materials[i].HeatTreatment == heatTreatment)
                    return i;
            }
            return 0;
        }

        private void cmbMat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaterialInfo();
        }

        private void cmbMat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaterialInfo();
        }

        private void UpdateMaterialInfo()
        {
            if (cmbMat1.SelectedIndex >= 0)
            {
                var mat = _materials[cmbMat1.SelectedIndex];
                labelMat1Info.Text = $"sigma_Hlim={mat.SigmaHlim}MPa  sigma_Flim={mat.SigmaFlim}MPa  硬度={mat.Hardness}";
            }
            if (cmbMat2.SelectedIndex >= 0)
            {
                var mat = _materials[cmbMat2.SelectedIndex];
                labelMat2Info.Text = $"sigma_Hlim={mat.SigmaHlim}MPa  sigma_Flim={mat.SigmaFlim}MPa  硬度={mat.Hardness}";
            }
        }

        /// <summary>
        /// 计算按钮点击事件
        /// </summary>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 读取输入参数
                ReadInputs();

                // 执行计算
                _calculator.Calculate();

                // 显示结果
                DisplayGeometryResults();
                DisplayLoadResults();
                DisplayStrengthResults();
                DisplayConclusion();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读取所有输入参数
        /// </summary>
        private void ReadInputs()
        {
            _calculator.Power = ParseDouble(txtPower, "传递功率");
            _calculator.SpeedN1 = ParseDouble(txtSpeed, "小齿轮转速");
            _calculator.RatioI = ParseDouble(txtRatio, "传动比");
            _calculator.Z1 = ParseInt(txtZ1, "小齿轮齿数");
            _calculator.Z2 = ParseInt(txtZ2, "大齿轮齿数");
            _calculator.ModuleM = ParseDouble(txtModule, "模数");
            _calculator.Alpha = ParseDouble(txtAlpha, "压力角");
            _calculator.PhiD = ParseDouble(txtPhiD, "齿宽系数");
            _calculator.PrecisionGrade = int.Parse(cmbPrecision.SelectedItem.ToString());
            _calculator.Material1 = _materials[cmbMat1.SelectedIndex];
            _calculator.Material2 = _materials[cmbMat2.SelectedIndex];

            // 使用系数 KA
            switch (cmbKA.SelectedIndex)
            {
                case 0: _calculator.KA = 1.0; break;
                case 1: _calculator.KA = 1.25; break;
                case 2: _calculator.KA = 1.50; break;
                case 3: _calculator.KA = 1.75; break;
            }
        }

        private double ParseDouble(TextBox txt, string name)
        {
            if (double.TryParse(txt.Text, out double val))
                return val;
            throw new ArgumentException(name + " 必须是有效的数字");
        }

        private int ParseInt(TextBox txt, string name)
        {
            if (int.TryParse(txt.Text, out int val))
                return val;
            throw new ArgumentException(name + " 必须是有效的整数");
        }

        /// <summary>
        /// 显示几何参数结果
        /// </summary>
        private void DisplayGeometryResults()
        {
            dgvGeometry.Rows.Clear();

            AddGeometryRow("分度圆直径 d (mm)", _calculator.D1, _calculator.D2);
            AddGeometryRow("齿顶圆直径 da (mm)", _calculator.Da1, _calculator.Da2);
            AddGeometryRow("齿根圆直径 df (mm)", _calculator.Df1, _calculator.Df2);
            AddGeometryRow("基圆直径 db (mm)", _calculator.Db1, _calculator.Db2);
            AddGeometryRow("齿距 p (mm)", _calculator.Pitch, _calculator.Pitch);

            // 其他参数 (不分大小齿轮)
            dgvGeometry.Rows.Add("齿顶高 ha (mm)", _calculator.Ha.ToString("F3"), _calculator.Ha.ToString("F3"));
            dgvGeometry.Rows.Add("齿根高 hf (mm)", _calculator.Hf.ToString("F3"), _calculator.Hf.ToString("F3"));
            dgvGeometry.Rows.Add("全齿高 h (mm)", _calculator.TotalHeight.ToString("F3"), _calculator.TotalHeight.ToString("F3"));
            dgvGeometry.Rows.Add("中心距 a (mm)", _calculator.CenterDist.ToString("F3"), "-");
            dgvGeometry.Rows.Add("齿宽 b (mm)", _calculator.FaceWidth.ToString("F1"), _calculator.FaceWidth.ToString("F1"));
            dgvGeometry.Rows.Add("端面重合度 eps_a", _calculator.EpsilonAlpha.ToString("F4"), "-");

            // 齿轮2的齿宽 (通常小齿轮比大齿轮宽5~10mm)
            double b2 = _calculator.FaceWidth;
            dgvGeometry.Rows[dgvGeometry.Rows.Count - 3].Cells[2].Value = b2.ToString("F1");
        }

        private void AddGeometryRow(string name, double val1, double val2)
        {
            dgvGeometry.Rows.Add(name, val1.ToString("F3"), val2.ToString("F3"));
        }

        /// <summary>
        /// 显示载荷参数结果
        /// </summary>
        private void DisplayLoadResults()
        {
            txtLoadResult.Text =
                $"小齿轮转矩  T1 = {_calculator.T1:F2} N·mm ({_calculator.T1 / 1000.0:F2} N·m)" + Environment.NewLine +
                $"大齿轮转速  n2 = {_calculator.SpeedN2:F2} rpm" + Environment.NewLine +
                $"圆周力      Ft = {_calculator.Ft:F2} N" + Environment.NewLine +
                $"径向力      Fr = {_calculator.Fr:F2} N" + Environment.NewLine +
                $"法向力      Fn = {_calculator.Fn:F2} N" + Environment.NewLine +
                $"圆周速度    v  = {_calculator.Velocity:F3} m/s" + Environment.NewLine +
                $"────────────────────────────────" + Environment.NewLine +
                $"使用系数    KA = {_calculator.KA:F2}" + Environment.NewLine +
                $"动载系数    Kv = {_calculator.Kv:F4}" + Environment.NewLine +
                $"齿向载荷系数 KHbeta = {_calculator.KHbeta:F4}" + Environment.NewLine +
                $"齿间载荷系数 KHalpha = {_calculator.KHalpha:F4}" + Environment.NewLine +
                $"总载荷系数  K  = {_calculator.K:F4}";
        }

        /// <summary>
        /// 显示强度校核结果
        /// </summary>
        private void DisplayStrengthResults()
        {
            dgvStrength.Rows.Clear();

            // 接触疲劳强度
            string contactResult = _calculator.ContactStrengthOK ? "通过" : "不通过";
            Color contactColor = _calculator.ContactStrengthOK ? Color.Green : Color.Red;
            dgvStrength.Rows.Add(
                "接触疲劳强度",
                _calculator.SigmaH.ToString("F2"),
                _calculator.SigmaHP.ToString("F2"),
                _calculator.SH.ToString("F3"),
                contactResult
            );
            dgvStrength.Rows[0].DefaultCellStyle.ForeColor = contactColor;

            // 小齿轮弯曲疲劳强度
            string bend1Result = _calculator.SF1 >= 1.0 ? "通过" : "不通过";
            Color bend1Color = _calculator.SF1 >= 1.0 ? Color.Green : Color.Red;
            dgvStrength.Rows.Add(
                "弯曲疲劳强度(小齿轮)",
                _calculator.SigmaF1.ToString("F2"),
                _calculator.SigmaFP1.ToString("F2"),
                _calculator.SF1.ToString("F3"),
                bend1Result
            );
            dgvStrength.Rows[1].DefaultCellStyle.ForeColor = bend1Color;

            // 大齿轮弯曲疲劳强度
            string bend2Result = _calculator.SF2 >= 1.0 ? "通过" : "不通过";
            Color bend2Color = _calculator.SF2 >= 1.0 ? Color.Green : Color.Red;
            dgvStrength.Rows.Add(
                "弯曲疲劳强度(大齿轮)",
                _calculator.SigmaF2.ToString("F2"),
                _calculator.SigmaFP2.ToString("F2"),
                _calculator.SF2.ToString("F3"),
                bend2Result
            );
            dgvStrength.Rows[2].DefaultCellStyle.ForeColor = bend2Color;
        }

        /// <summary>
        /// 显示校核结论
        /// </summary>
        private void DisplayConclusion()
        {
            bool allOK = _calculator.ContactStrengthOK && _calculator.BendingStrengthOK;

            if (allOK)
            {
                labelConclusion.Text = "校核结论: 全部通过 -- 齿轮强度满足要求";
                labelConclusion.ForeColor = Color.Green;
            }
            else
            {
                string msg = "校核结论: ";
                if (!_calculator.ContactStrengthOK)
                    msg += "接触强度不足 ";
                if (!_calculator.BendingStrengthOK)
                    msg += "弯曲强度不足";
                msg += " -- 请调整齿轮参数或材料";

                labelConclusion.Text = msg;
                labelConclusion.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 重置按钮
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPower.Text = "10";
            txtSpeed.Text = "1450";
            txtRatio.Text = "3.5";
            txtZ1.Text = "20";
            txtZ2.Text = "70";
            txtModule.Text = "3";
            txtAlpha.Text = "20";
            txtPhiD.Text = "1.0";
            cmbPrecision.SelectedIndex = 1;
            cmbKA.SelectedIndex = 0;
            cmbMat1.SelectedIndex = FindMaterialIndex("40Cr", "调质+表面淬火");
            cmbMat2.SelectedIndex = FindMaterialIndex("45钢", "调质");

            dgvGeometry.Rows.Clear();
            dgvStrength.Rows.Clear();
            txtLoadResult.Text = "";
            labelConclusion.Text = "";
        }
    }
}
