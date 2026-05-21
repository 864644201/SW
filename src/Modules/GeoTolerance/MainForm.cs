using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace GeoTolerance
{
    public partial class MainForm : Form
    {
        // 公差类型信息：名称、图样资源名、参数说明、所属表(0-3)、等级范围
        private static readonly TypeInfo[] Types = {
            new TypeInfo("直线度", "GeoTolerance.zxd", "参照L图样", 0, 1, 8),
            new TypeInfo("平面度", "GeoTolerance.pmd", "参照L图样", 0, 1, 8),
            new TypeInfo("圆度",   "GeoTolerance.yd",  "参照L图样", 1, 0, 7),
            new TypeInfo("圆柱度", "GeoTolerance.yzd", "参照L图样", 1, 0, 7),
            new TypeInfo("平行度", "GeoTolerance.pxd", "参照L,d(D)图样", 2, 1, 8),
            new TypeInfo("垂直度", "GeoTolerance.czd", "参照L,d(D)图样", 2, 1, 8),
            new TypeInfo("倾斜度", "GeoTolerance.qxd", "参照L,d(D)图样", 2, 1, 8),
            new TypeInfo("同轴度", "GeoTolerance.tzd", "参照d(D)图样", 3, 1, 8),
            new TypeInfo("对称度", "GeoTolerance.dcd", "参照B,L图样", 3, 1, 8),
            new TypeInfo("圆跳动", "GeoTolerance.ytd", "参照d(D)图样", 3, 1, 8),
            new TypeInfo("全跳动", "GeoTolerance.qtd", "参照d(D)图样", 3, 1, 8)
        };

        private int selectedType = 0;

        public MainForm()
        {
            InitializeComponent();
            PopulateGradeCombo(0);
            LoadDiagram(0);
        }

        private void PopulateGradeCombo(int typeIndex)
        {
            int minGrade = Types[typeIndex].MinGrade;
            int maxGrade = Types[typeIndex].MaxGrade;
            string prev = cmbGrade.SelectedItem?.ToString();
            cmbGrade.Items.Clear();
            for (int i = minGrade; i <= maxGrade; i++)
                cmbGrade.Items.Add(i.ToString());
            // 默认选6级（如果在范围内），否则选最后一个
            if (prev != null && cmbGrade.Items.Contains(prev))
                cmbGrade.SelectedItem = prev;
            else if (cmbGrade.Items.Contains("6"))
                cmbGrade.SelectedItem = "6";
            else
                cmbGrade.SelectedIndex = cmbGrade.Items.Count - 1;
        }

        private void OnCategoryChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk == chkShape && chk.Checked)
                chkLocation.Checked = false;
            else if (chk == chkLocation && chk.Checked)
                chkShape.Checked = false;

            // 至少选一个
            if (!chkShape.Checked && !chkLocation.Checked)
                chk.Checked = true;

            // 启用/禁用类型按钮
            for (int i = 0; i < 6; i++)
                radioTypes[i].Enabled = chkShape.Checked;
            for (int i = 6; i < 11; i++)
                radioTypes[i].Enabled = chkLocation.Checked;

            // 如果当前选中的类型被禁用了，切换到第一个可用的
            if (!radioTypes[selectedType].Enabled)
            {
                int first = chkShape.Checked ? 0 : 6;
                radioTypes[first].Checked = true;
            }
        }

        private void OnTypeChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (!rb.Checked) return;
            selectedType = (int)rb.Tag;
            PopulateGradeCombo(selectedType);
            LoadDiagram(selectedType);
            if (lblParamNote != null)
                lblParamNote.Text = "参数说明: " + Types[selectedType].ParamNote;
        }

        private void LoadDiagram(int typeIndex)
        {
            if (picDiagram == null) return;
            string resName = Types[typeIndex].ResourceName;
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resName))
                {
                    if (stream != null)
                    {
                        picDiagram.Image = Image.FromStream(stream);
                    }
                    else
                    {
                        picDiagram.Image = null;
                    }
                }
            }
            catch
            {
                picDiagram.Image = null;
            }
            if (lblDiagramTitle != null)
                lblDiagramTitle.Text = Types[selectedType].Name + " - 图示";
        }

        private void OnQuery(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            double size = double.Parse(txtBasicSize.Text);
            int grade = int.Parse(cmbGrade.SelectedItem.ToString());

            TypeInfo ti = Types[selectedType];
            double[] sizes;
            int[][] values;

            switch (ti.TableIndex)
            {
                case 0:
                    sizes = GeoToleranceData.LinePlane_Sizes;
                    values = GeoToleranceData.LinePlane_Values;
                    break;
                case 1:
                    sizes = GeoToleranceData.RoundCyl_Sizes;
                    values = GeoToleranceData.RoundCyl_Values;
                    break;
                case 2:
                    sizes = GeoToleranceData.ParVertAng_Sizes;
                    values = GeoToleranceData.ParVertAng_Values;
                    break;
                default:
                    sizes = GeoToleranceData.CoaxSymRun_Sizes;
                    values = GeoToleranceData.CoaxSymRun_Values;
                    break;
            }

            // 检查尺寸范围
            double maxSize = sizes[sizes.Length - 1];
            if (size > maxSize)
            {
                MessageBox.Show(string.Format("基本尺寸不能超过 {0} mm", maxSize),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBasicSize.Focus();
                txtBasicSize.SelectAll();
                return;
            }

            int row = GeoToleranceData.FindCeilingRow(sizes, size);
            int col = grade - ti.MinGrade;
            int valueUm = values[row][col];

            // 单位转换：微米 → 毫米
            double valueMm = Math.Round(valueUm * 1000.0) / 1000000.0;

            lblResult.Text = string.Format("{0}  公差值: {1} mm ({2} μm)",
                ti.Name + (ti.TableIndex == 1 ? " (等级" + grade + ")" : " (IT" + grade + ")"),
                valueMm.ToString("F4"), valueUm);

            lblStandardRef.Text = string.Format("主参数范围: {0}~{1} mm\n依据: GB/T 1182-1996",
                row > 0 ? sizes[row - 1].ToString() : "0", sizes[row].ToString());
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtBasicSize.Text))
            {
                MessageBox.Show("请输入基本尺寸", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBasicSize.Focus();
                return false;
            }

            double size;
            if (!double.TryParse(txtBasicSize.Text, out size) || size <= 0)
            {
                MessageBox.Show("基本尺寸必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBasicSize.Focus();
                txtBasicSize.SelectAll();
                return false;
            }

            if (cmbGrade.SelectedItem == null)
            {
                MessageBox.Show("请选择公差等级", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private class TypeInfo
        {
            public string Name;
            public string ResourceName;
            public string ParamNote;
            public int TableIndex;
            public int MinGrade;
            public int MaxGrade;

            public TypeInfo(string name, string res, string note, int table, int min, int max)
            {
                Name = name;
                ResourceName = res;
                ParamNote = note;
                TableIndex = table;
                MinGrade = min;
                MaxGrade = max;
            }
        }
    }
}
