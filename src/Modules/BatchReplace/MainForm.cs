using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatchReplace
{
    public partial class MainForm : Form
    {
        private ReplaceEngine _engine;
        private List<ReplaceRule> _rules;
        private List<string> _targetFiles;
        private bool _isRunning;

        public MainForm()
        {
            InitializeComponent();
            _engine = new ReplaceEngine();
            _rules = new List<ReplaceRule>();
            _targetFiles = new List<string>();

            _engine.ProgressChanged += OnProgressChanged;
            _engine.FileProcessed += OnFileProcessed;
        }

        #region 文件/文件夹选择

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "选择SolidWorks文件";
                dlg.Filter = "SolidWorks文件 (*.sldasm;*.sldprt)|*.sldasm;*.sldprt|装配体 (*.sldasm)|*.sldasm|零件 (*.sldprt)|*.sldprt";
                dlg.Multiselect = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _targetFiles = dlg.FileNames.ToList();
                    UpdateFileList();
                }
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "选择包含SolidWorks文件的文件夹";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = dlg.SelectedPath;
                    ScanFolder(dlg.SelectedPath);
                }
            }
        }

        private void ScanFolder(string folderPath)
        {
            _targetFiles = _engine.GetSolidWorksFiles(folderPath, chkSubfolders.Checked);
            UpdateFileList();
            lblFileCount.Text = $"找到 {_targetFiles.Count} 个文件";
        }

        private void UpdateFileList()
        {
            lstFiles.Items.Clear();
            foreach (var file in _targetFiles)
            {
                lstFiles.Items.Add(Path.GetFileName(file));
            }
            lblFileCount.Text = $"共 {_targetFiles.Count} 个文件";
        }

        #endregion

        #region 规则管理

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            using (var dlg = new RuleEditForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _rules.Add(dlg.Rule);
                    UpdateRuleList();
                }
            }
        }

        private void btnEditRule_Click(object sender, EventArgs e)
        {
            if (lstRules.SelectedIndex < 0) return;
            var rule = _rules[lstRules.SelectedIndex];

            using (var dlg = new RuleEditForm(rule))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _rules[lstRules.SelectedIndex] = dlg.Rule;
                    UpdateRuleList();
                }
            }
        }

        private void btnRemoveRule_Click(object sender, EventArgs e)
        {
            if (lstRules.SelectedIndex < 0) return;
            _rules.RemoveAt(lstRules.SelectedIndex);
            UpdateRuleList();
        }

        private void UpdateRuleList()
        {
            lstRules.Items.Clear();
            foreach (var rule in _rules)
            {
                lstRules.Items.Add(rule.ToString());
            }
        }

        #endregion

        #region 执行批量替换

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (_targetFiles.Count == 0)
            {
                MessageBox.Show("请先选择要处理的文件。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_rules.Count == 0 || !_rules.Any(r => r.Enabled))
            {
                MessageBox.Show("请先添加并启用至少一条替换规则。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var activeRules = _rules.Where(r => r.Enabled).ToList();
            var confirm = MessageBox.Show(
                $"确认对 {_targetFiles.Count} 个文件执行 {activeRules.Count} 条替换规则？\n\n" +
                string.Join("\n", activeRules.Select(r => "  - " + r.ToString())),
                "确认执行", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            _isRunning = true;
            SetControlsEnabled(false);
            progressBar.Maximum = _targetFiles.Count;
            progressBar.Value = 0;
            dgvResults.Rows.Clear();

            try
            {
                var results = await Task.Run(() =>
                    _engine.ExecuteBatch(_targetFiles, activeRules));

                int success = results.Count(r => r.Success);
                int failed = results.Count(r => !r.Success);

                lblStatus.Text = $"完成: 成功 {success}, 失败 {failed}";
                MessageBox.Show(
                    $"批量替换完成!\n\n成功: {success}\n失败: {failed}",
                    "完成", MessageBoxButtons.OK,
                    failed > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"执行失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "执行失败";
            }
            finally
            {
                _isRunning = false;
                SetControlsEnabled(true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                _engine.Cancel();
                lblStatus.Text = "正在取消...";
            }
        }

        private void OnProgressChanged(ProgressInfo info)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnProgressChanged(info)));
                return;
            }

            progressBar.Value = Math.Min(info.Current, progressBar.Maximum);
            lblStatus.Text = $"[{info.Current}/{info.Total}] {info.CurrentFile}";
            lblPercent.Text = $"{info.Percent:F0}%";
        }

        private void OnFileProcessed(ReplaceResult result)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnFileProcessed(result)));
                return;
            }

            int rowIdx = dgvResults.Rows.Add(
                result.FileName,
                result.Success ? "成功" : "失败",
                result.AffectedCount,
                result.Duration.TotalSeconds.ToString("F1") + "s",
                result.Message);

            dgvResults.Rows[rowIdx].DefaultCellStyle.ForeColor =
                result.Success ? Color.DarkGreen : Color.Red;
            dgvResults.FirstDisplayedScrollingRowIndex = rowIdx;
        }

        private void SetControlsEnabled(bool enabled)
        {
            btnSelectFiles.Enabled = enabled;
            btnSelectFolder.Enabled = enabled;
            btnExecute.Enabled = enabled;
            btnAddRule.Enabled = enabled;
            btnEditRule.Enabled = enabled;
            btnRemoveRule.Enabled = enabled;
            btnCancel.Enabled = !enabled;
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isRunning)
            {
                _engine.Cancel();
                e.Cancel = true;
                return;
            }
            _engine?.Dispose();
            base.OnFormClosing(e);
        }
    }

    /// <summary>
    /// 规则编辑对话框
    /// </summary>
    internal class RuleEditForm : Form
    {
        public ReplaceRule Rule { get; private set; }

        private ComboBox cmbAction;
        private TextBox txtSearch;
        private TextBox txtReplace;
        private CheckBox chkCaseSensitive;
        private CheckBox chkUseRegex;
        private CheckBox chkEnabled;
        private TextBox txtPropertyName;
        private TextBox txtPropertyValue;
        private Panel panelProperty;
        private Panel panelReplace;

        public RuleEditForm(ReplaceRule existingRule = null)
        {
            Rule = existingRule ?? new ReplaceRule();
            InitializeForm();
            if (existingRule != null) LoadFromRule(existingRule);
        }

        private void InitializeForm()
        {
            Text = "编辑替换规则";
            Size = new Size(480, 400);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            int y = 15;
            int lx = 15;
            int ix = 130;

            AddLabel("操作类型:", lx, y);
            cmbAction = new ComboBox
            {
                Location = new Point(ix, y),
                Size = new Size(300, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbAction.Items.AddRange(new object[]
            {
                "组件替换", "特征压缩/解压缩", "配置切换", "属性修改"
            });
            cmbAction.SelectedIndex = 0;
            cmbAction.SelectedIndexChanged += CmbAction_SelectedIndexChanged;
            Controls.Add(cmbAction);
            y += 35;

            // Search pattern panel
            panelReplace = new Panel { Location = new Point(0, y), Size = new Size(460, 130) };

            var lblSearch = new Label { Text = "查找模式:", Location = new Point(lx, 3), Size = new Size(110, 20) };
            txtSearch = new TextBox { Location = new Point(ix, 0), Size = new Size(300, 23) };
            var lblReplace = new Label { Text = "替换为:", Location = new Point(lx, 33), Size = new Size(110, 20) };
            txtReplace = new TextBox { Location = new Point(ix, 30), Size = new Size(300, 23) };

            chkCaseSensitive = new CheckBox { Text = "区分大小写", Location = new Point(ix, 60), Size = new Size(120, 23) };
            chkUseRegex = new CheckBox { Text = "正则表达式", Location = new Point(ix + 130, 60), Size = new Size(120, 23) };
            chkEnabled = new CheckBox { Text = "启用此规则", Location = new Point(ix, 88), Size = new Size(120, 23), Checked = true };

            panelReplace.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblReplace, txtReplace, chkCaseSensitive, chkUseRegex, chkEnabled });
            Controls.Add(panelReplace);
            y += 140;

            // Property panel (for property change mode)
            panelProperty = new Panel { Location = new Point(0, y), Size = new Size(460, 70), Visible = false };

            var lblPropName = new Label { Text = "属性名:", Location = new Point(lx, 3), Size = new Size(110, 20) };
            txtPropertyName = new TextBox { Location = new Point(ix, 0), Size = new Size(300, 23) };
            var lblPropVal = new Label { Text = "属性值:", Location = new Point(lx, 33), Size = new Size(110, 20) };
            txtPropertyValue = new TextBox { Location = new Point(ix, 30), Size = new Size(300, 23) };

            panelProperty.Controls.AddRange(new Control[] { lblPropName, txtPropertyName, lblPropVal, txtPropertyValue });
            Controls.Add(panelProperty);
            y += 80;

            // Buttons
            var btnOk = new Button
            {
                Text = "确定",
                DialogResult = DialogResult.OK,
                Location = new Point(240, y),
                Size = new Size(80, 30)
            };
            btnOk.Click += (s, e) => SaveToRule();

            var btnCancel = new Button
            {
                Text = "取消",
                DialogResult = DialogResult.Cancel,
                Location = new Point(340, y),
                Size = new Size(80, 30)
            };

            Controls.Add(btnOk);
            Controls.Add(btnCancel);

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private void CmbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isPropChange = cmbAction.SelectedIndex == 3;
            panelProperty.Visible = isPropChange;
            panelReplace.Visible = !isPropChange;
        }

        private void LoadFromRule(ReplaceRule rule)
        {
            cmbAction.SelectedIndex = (int)rule.Action;
            txtSearch.Text = rule.SearchPattern ?? "";
            txtReplace.Text = rule.ReplaceWith ?? "";
            chkCaseSensitive.Checked = rule.CaseSensitive;
            chkUseRegex.Checked = rule.UseRegex;
            chkEnabled.Checked = rule.Enabled;
            txtPropertyName.Text = rule.PropertyName ?? "";
            txtPropertyValue.Text = rule.PropertyValue ?? "";
        }

        private void SaveToRule()
        {
            Rule = new ReplaceRule
            {
                Action = (ReplaceAction)cmbAction.SelectedIndex,
                SearchPattern = txtSearch.Text.Trim(),
                ReplaceWith = txtReplace.Text.Trim(),
                CaseSensitive = chkCaseSensitive.Checked,
                UseRegex = chkUseRegex.Checked,
                Enabled = chkEnabled.Checked,
                SuppressFeature = true,
                PropertyName = txtPropertyName.Text.Trim(),
                PropertyValue = txtPropertyValue.Text.Trim()
            };
        }

        private Label AddLabel(string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(x, y + 3),
                Size = new Size(110, 20)
            };
            Controls.Add(lbl);
            return lbl;
        }
    }
}
