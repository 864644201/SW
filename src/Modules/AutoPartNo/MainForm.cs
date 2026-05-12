using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoPartNo
{
    public partial class MainForm : Form
    {
        private dynamic _swApp;
        private PartNumberGenerator _generator;
        private List<ComponentInfo> _components;

        public MainForm()
        {
            InitializeComponent();
            _generator = new PartNumberGenerator(PartNumberGenerator.PresetRules.Standard());
            _components = new List<ComponentInfo>();
            UpdateRuleDisplay();
        }

        #region SolidWorks连接

        private bool ConnectToSolidWorks()
        {
            try
            {
                _swApp = Marshal.GetActiveObject("SldWorks.Application");
                return true;
            }
            catch (COMException)
            {
                MessageBox.Show("请先启动SolidWorks，然后重试。", "连接失败",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        #endregion

        #region 组件扫描

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!ConnectToSolidWorks()) return;

            try
            {
                dynamic swModel = _swApp.ActiveDoc;
                if (swModel == null)
                {
                    MessageBox.Show("请先在SolidWorks中打开一个装配体。", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (swModel.GetType() != 2) // swDocASSEMBLY
                {
                    MessageBox.Show("当前文档不是装配体。", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _components.Clear();
                dynamic swAssembly = swModel;
                ScanComponents(swAssembly, 0);

                dgvComponents.Rows.Clear();
                foreach (var comp in _components)
                {
                    int rowIdx = dgvComponents.Rows.Add(
                        comp.Selected,
                        comp.ComponentName,
                        comp.CurrentPartNo,
                        comp.NewPartNo,
                        comp.FilePath,
                        comp.IsAssembly ? "装配体" : "零件"
                    );
                    dgvComponents.Rows[rowIdx].Tag = comp;
                }

                lblStatus.Text = $"扫描完成: 找到 {_components.Count} 个组件";
                GeneratePreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"扫描失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScanComponents(dynamic assembly, int level)
        {
            object[] components = null;
            try
            {
                components = (object[])assembly.GetComponents(false);
            }
            catch (COMException) { return; }

            if (components == null) return;

            foreach (dynamic comp in components)
            {
                try
                {
                    dynamic compModel = comp.GetModelDoc2();
                    if (compModel == null) continue;

                    bool isAsm = compModel.GetType() == 2; // swDocASSEMBLY
                    string configName = comp.ReferencedConfiguration ?? "";

                    // 读取当前零件号
                    string currentPartNo = "";
                    dynamic custProp = compModel.Extension.CustomPropertyManager[configName];
                    if (custProp == null)
                        custProp = compModel.Extension.CustomPropertyManager[""];

                    if (custProp != null)
                    {
                        string valOut = "";
                        object unused1 = null, unused2 = null, unused3 = null;
                        custProp.Get6("零件号", false, out valOut, out unused1, out unused2, out unused3);
                        currentPartNo = valOut?.Trim() ?? "";
                    }

                    if (string.IsNullOrEmpty(currentPartNo))
                    {
                        string valOut = "";
                        object unused1 = null, unused2 = null, unused3 = null;
                        custProp?.Get6("PartNo", false, out valOut, out unused1, out unused2, out unused3);
                        currentPartNo = valOut?.Trim() ?? "";
                    }

                    var info = new ComponentInfo
                    {
                        Selected = true,
                        ComponentName = comp.Name2 ?? Path.GetFileNameWithoutExtension(compModel.GetPathName()),
                        CurrentPartNo = currentPartNo,
                        NewPartNo = "",
                        FilePath = compModel.GetPathName(),
                        IsAssembly = isAsm,
                        Level = level,
                        SwComponent = comp,
                        SwModel = compModel,
                        ConfigName = configName
                    };

                    // 如果规则要求跳过已有零件号
                    if (_generator.Rule.SkipExisting && !string.IsNullOrEmpty(currentPartNo))
                    {
                        info.Selected = false;
                        _generator.RegisterExisting(currentPartNo);
                    }

                    _components.Add(info);

                    // 递归扫描子装配体
                    if (isAsm && chkRecursive.Checked)
                    {
                        ScanComponents((dynamic)compModel, level + 1);
                    }
                }
                catch (COMException) { }
            }
        }

        #endregion

        #region 零件号生成

        private void GeneratePreview()
        {
            var selected = _components.Where(c => c.Selected).ToList();
            _generator.Reset();

            // 注册所有已存在的零件号
            _generator.RegisterExistingRange(
                _components.Where(c => !c.Selected).Select(c => c.CurrentPartNo));

            foreach (var comp in selected)
            {
                comp.NewPartNo = _generator.GenerateNext();
            }

            // 更新表格显示
            for (int i = 0; i < dgvComponents.Rows.Count; i++)
            {
                var comp = (ComponentInfo)dgvComponents.Rows[i].Tag;
                dgvComponents.Rows[i].Cells["colNewPartNo"].Value = comp.NewPartNo;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GeneratePreview();
            lblStatus.Text = "预览已更新";
        }

        #endregion

        #region 应用零件号

        private void btnApply_Click(object sender, EventArgs e)
        {
            var selected = _components.Where(c => c.Selected && !string.IsNullOrEmpty(c.NewPartNo)).ToList();

            if (selected.Count == 0)
            {
                MessageBox.Show("没有需要分配零件号的组件。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"确认为 {selected.Count} 个组件分配零件号？\n此操作将修改SolidWorks文件属性。",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            int success = 0;
            int failed = 0;
            var errors = new List<string>();

            foreach (var comp in selected)
            {
                try
                {
                    dynamic custProp = comp.SwModel.Extension.CustomPropertyManager[comp.ConfigName];
                    if (custProp == null)
                        custProp = comp.SwModel.Extension.CustomPropertyManager[""];

                    if (custProp != null)
                    {
                        // 尝试设置 "零件号" 属性
                        custProp.Add3("零件号",
                            30, // swCustomInfoText
                            comp.NewPartNo,
                            1); // swCustomInfoAddResult_OK

                        // 同时设置 "PartNo" 属性
                        custProp.Add3("PartNo",
                            30, // swCustomInfoText
                            comp.NewPartNo,
                            1); // swCustomInfoAddResult_OK

                        comp.SwModel.SaveSilent(comp.SwModel.GetPathName());
                        success++;
                    }
                    else
                    {
                        errors.Add($"{comp.ComponentName}: 无法获取属性管理器");
                        failed++;
                    }
                }
                catch (COMException ex)
                {
                    errors.Add($"{comp.ComponentName}: {ex.Message}");
                    failed++;
                }
            }

            string msg = $"分配完成: 成功 {success}, 失败 {failed}";
            if (errors.Count > 0)
            {
                msg += "\n\n失败详情:\n" + string.Join("\n", errors.Take(10));
                if (errors.Count > 10)
                    msg += $"\n...还有 {errors.Count - 10} 个错误";
            }

            MessageBox.Show(msg, failed > 0 ? "部分完成" : "完成",
                MessageBoxButtons.OK,
                failed > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            lblStatus.Text = $"分配完成: 成功 {success}, 失败 {failed}";
        }

        #endregion

        #region 规则配置

        private void btnRuleSettings_Click(object sender, EventArgs e)
        {
            using (var dlg = new RuleSettingsForm(_generator.Rule))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _generator.Rule = dlg.Rule;
                    UpdateRuleDisplay();
                    GeneratePreview();
                }
            }
        }

        private void UpdateRuleDisplay()
        {
            lblRuleDisplay.Text = $"规则: {_generator.Rule.Prefix}{_generator.Rule.Separator}" +
                                  $"{"0".PadLeft(_generator.Rule.DigitCount, '0')}" +
                                  $"{(string.IsNullOrEmpty(_generator.Rule.Suffix) ? "" : _generator.Rule.Suffix + _generator.Rule.Suffix)}";
        }

        #endregion

        #region 辅助类

        private class ComponentInfo
        {
            public bool Selected { get; set; }
            public string ComponentName { get; set; }
            public string CurrentPartNo { get; set; }
            public string NewPartNo { get; set; }
            public string FilePath { get; set; }
            public bool IsAssembly { get; set; }
            public int Level { get; set; }
            public dynamic SwComponent { get; set; }
            public dynamic SwModel { get; set; }
            public string ConfigName { get; set; }
        }

        #endregion
    }

    /// <summary>
    /// 编号规则设置对话框
    /// </summary>
    internal class RuleSettingsForm : Form
    {
        public NumberingRule Rule { get; private set; }

        private TextBox txtPrefix;
        private TextBox txtSeparator;
        private NumericUpDown nudStart;
        private NumericUpDown nudStep;
        private NumericUpDown nudDigits;
        private TextBox txtSuffix;
        private CheckBox chkSkipExisting;
        private TextBox txtPattern;

        public RuleSettingsForm(NumberingRule rule)
        {
            Rule = rule;
            InitializeForm();
            LoadFromRule(rule);
        }

        private void InitializeForm()
        {
            Text = "编号规则设置";
            Size = new System.Drawing.Size(400, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            int y = 15;
            int labelX = 15;
            int inputX = 140;

            AddLabel("前缀:", labelX, y);
            txtPrefix = AddTextBox(inputX, y); y += 35;

            AddLabel("分隔符:", labelX, y);
            txtSeparator = AddTextBox(inputX, y, 80); y += 35;

            AddLabel("起始序号:", labelX, y);
            nudStart = AddNumericUpDown(inputX, y, 0, 999999, 1); y += 35;

            AddLabel("步长:", labelX, y);
            nudStep = AddNumericUpDown(inputX, y, 1, 100, 1); y += 35;

            AddLabel("序号位数:", labelX, y);
            nudDigits = AddNumericUpDown(inputX, y, 1, 10, 4); y += 35;

            AddLabel("后缀:", labelX, y);
            txtSuffix = AddTextBox(inputX, y); y += 35;

            chkSkipExisting = new CheckBox
            {
                Text = "跳过已分配零件号的组件",
                Location = new System.Drawing.Point(inputX, y),
                Size = new System.Drawing.Size(230, 23),
                Checked = Rule.SkipExisting
            };
            Controls.Add(chkSkipExisting); y += 35;

            AddLabel("验证正则:", labelX, y);
            txtPattern = AddTextBox(inputX, y, 200); y += 45;

            // 按钮
            var btnOk = new Button
            {
                Text = "确定",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(160, y),
                Size = new System.Drawing.Size(80, 30)
            };
            btnOk.Click += (s, e) => SaveToRule();

            var btnCancel = new Button
            {
                Text = "取消",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(260, y),
                Size = new System.Drawing.Size(80, 30)
            };

            Controls.Add(btnOk);
            Controls.Add(btnCancel);

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private Label AddLabel(string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new System.Drawing.Point(x, y + 3),
                Size = new System.Drawing.Size(120, 20)
            };
            Controls.Add(lbl);
            return lbl;
        }

        private TextBox AddTextBox(int x, int y, int width = 200)
        {
            var txt = new TextBox
            {
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(width, 23)
            };
            Controls.Add(txt);
            return txt;
        }

        private NumericUpDown AddNumericUpDown(int x, int y, int min, int max, int value)
        {
            var nud = new NumericUpDown
            {
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(120, 23),
                Minimum = min,
                Maximum = max,
                Value = value
            };
            Controls.Add(nud);
            return nud;
        }

        private void LoadFromRule(NumberingRule rule)
        {
            txtPrefix.Text = rule.Prefix;
            txtSeparator.Text = rule.Separator;
            nudStart.Value = rule.StartNumber;
            nudStep.Value = rule.Step;
            nudDigits.Value = rule.DigitCount;
            txtSuffix.Text = rule.Suffix;
            chkSkipExisting.Checked = rule.SkipExisting;
            txtPattern.Text = rule.ValidationPattern ?? "";
        }

        private void SaveToRule()
        {
            Rule = new NumberingRule
            {
                Prefix = txtPrefix.Text.Trim(),
                Separator = txtSeparator.Text,
                StartNumber = (int)nudStart.Value,
                Step = (int)nudStep.Value,
                DigitCount = (int)nudDigits.Value,
                Suffix = txtSuffix.Text.Trim(),
                SkipExisting = chkSkipExisting.Checked,
                ValidationPattern = txtPattern.Text.Trim()
            };
        }
    }
}
