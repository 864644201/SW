using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReplaceDrawingTemplate
{
    public partial class MainForm : Form
    {
        private readonly TemplateReplacer _replacer = new TemplateReplacer();
        private List<string> _targetFiles = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowseOld_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "选择旧模板文件";
                dlg.Filter = "SolidWorks 模板 (*.drwdot;*.slddrt)|*.drwdot;*.slddrt|所有文件 (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtOldTemplate.Text = dlg.FileName;
                    var info = _replacer.GetTemplateInfo(dlg.FileName);
                    lblOldInfo.Text = info != null ? $"版本: {info.Version} | 图幅: {info.SheetSize}" : "无法读取模板信息";
                }
            }
        }

        private void btnBrowseNew_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "选择新模板文件";
                dlg.Filter = "SolidWorks 模板 (*.drwdot;*.slddrt)|*.drwdot;*.slddrt|所有文件 (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtNewTemplate.Text = dlg.FileName;
                    var info = _replacer.GetTemplateInfo(dlg.FileName);
                    lblNewInfo.Text = info != null ? $"版本: {info.Version} | 图幅: {info.SheetSize}" : "无法读取模板信息";
                }
            }
        }

        private void btnBrowseFiles_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "选择要替换模板的工程图文件";
                dlg.Filter = "SolidWorks 工程图 (*.slddrw)|*.slddrw|所有文件 (*.*)|*.*";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    AddFiles(dlg.FileNames);
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "选择包含工程图文件的文件夹";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var files = Directory.GetFiles(dlg.SelectedPath, "*.slddrw", SearchOption.AllDirectories)
                        .Concat(Directory.GetFiles(dlg.SelectedPath, "*.SLDDRW", SearchOption.AllDirectories))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToArray();
                    AddFiles(files);
                }
            }
        }

        private void AddFiles(string[] filePaths)
        {
            foreach (var path in filePaths)
            {
                if (_targetFiles.Contains(path, StringComparer.OrdinalIgnoreCase))
                    continue;

                _targetFiles.Add(path);

                var item = new ListViewItem(Path.GetFileName(path));
                item.SubItems.Add(Path.GetDirectoryName(path));
                item.SubItems.Add("(未检测)");
                item.SubItems.Add("待处理");
                item.Tag = path;
                item.Checked = true;
                lvFiles.Items.Add(item);
            }

            lblFileCount.Text = $"共 {_targetFiles.Count} 个文件";
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            _targetFiles.Clear();
            lvFiles.Items.Clear();
            lblFileCount.Text = "共 0 个文件";
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOldTemplate.Text) || string.IsNullOrEmpty(txtNewTemplate.Text))
            {
                MessageBox.Show("请先选择旧模板和新模板。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lvFiles.Items.Count == 0)
            {
                MessageBox.Show("请先添加目标文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtPreview.Clear();
            txtPreview.AppendText($"旧模板: {Path.GetFileName(txtOldTemplate.Text)}\r\n");
            txtPreview.AppendText($"新模板: {Path.GetFileName(txtNewTemplate.Text)}\r\n");
            txtPreview.AppendText($"目标文件: {lvFiles.Items.Count} 个\r\n");
            txtPreview.AppendText("---\r\n");
            txtPreview.AppendText("替换说明:\r\n");
            txtPreview.AppendText("  1. 打开每个工程图文件\r\n");
            txtPreview.AppendText("  2. 将图纸模板从旧模板替换为新模板\r\n");
            txtPreview.AppendText("  3. 保留原有的视图和标注内容\r\n");
            txtPreview.AppendText("  4. 保存并关闭文件\r\n");

            // Simulate checking current templates
            foreach (ListViewItem item in lvFiles.Items)
            {
                string filePath = item.Tag as string;
                var currentTemplate = _replacer.DetectCurrentTemplate(filePath);
                item.SubItems[2].Text = currentTemplate ?? "(未知)";
            }

            txtPreview.AppendText("\r\n预览完成。检查第三列查看各文件当前使用的模板。\r\n");
            lblStatus.Text = "预览完成";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyReplacement(false);
        }

        private void btnApplySelected_Click(object sender, EventArgs e)
        {
            ApplyReplacement(true);
        }

        private void ApplyReplacement(bool selectedOnly)
        {
            if (string.IsNullOrEmpty(txtOldTemplate.Text) || string.IsNullOrEmpty(txtNewTemplate.Text))
            {
                MessageBox.Show("请先选择旧模板和新模板。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var filesToProcess = new List<string>();
            foreach (ListViewItem item in lvFiles.Items)
            {
                if (selectedOnly && !item.Checked) continue;
                filesToProcess.Add(item.Tag as string);
            }

            if (filesToProcess.Count == 0)
            {
                MessageBox.Show("没有需要处理的文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"确认要替换 {filesToProcess.Count} 个文件的图纸模板吗？\n" +
                $"旧模板: {Path.GetFileName(txtOldTemplate.Text)}\n" +
                $"新模板: {Path.GetFileName(txtNewTemplate.Text)}",
                "确认替换", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            progressBar.Visible = true;
            progressBar.Maximum = filesToProcess.Count;
            progressBar.Value = 0;
            txtPreview.Clear();
            txtPreview.AppendText($"开始替换 {filesToProcess.Count} 个文件的模板...\r\n");

            int success = 0;
            int failed = 0;

            for (int i = 0; i < filesToProcess.Count; i++)
            {
                string filePath = filesToProcess[i];
                string fileName = Path.GetFileName(filePath);

                // Find corresponding ListView item
                ListViewItem lvItem = null;
                foreach (ListViewItem li in lvFiles.Items)
                {
                    if ((li.Tag as string) == filePath)
                    {
                        lvItem = li;
                        break;
                    }
                }

                try
                {
                    bool replaced = _replacer.ReplaceTemplate(filePath, txtOldTemplate.Text, txtNewTemplate.Text);
                    if (replaced)
                    {
                        success++;
                        txtPreview.AppendText($"  [OK] {fileName}\r\n");
                        if (lvItem != null)
                        {
                            lvItem.SubItems[3].Text = "完成";
                            lvItem.SubItems[2].Text = Path.GetFileName(txtNewTemplate.Text);
                        }
                    }
                    else
                    {
                        failed++;
                        txtPreview.AppendText($"  [跳过] {fileName} - 未使用旧模板\r\n");
                        if (lvItem != null)
                            lvItem.SubItems[3].Text = "未匹配";
                    }
                }
                catch (Exception ex)
                {
                    failed++;
                    txtPreview.AppendText($"  [失败] {fileName} - {ex.Message}\r\n");
                    if (lvItem != null)
                        lvItem.SubItems[3].Text = "失败";
                }

                progressBar.Value = i + 1;
            }

            progressBar.Visible = false;
            txtPreview.AppendText($"\r\n替换完成。成功: {success}, 失败/跳过: {failed}\r\n");
            lblStatus.Text = $"完成: {success} 成功, {failed} 失败/跳过";

            MessageBox.Show($"模板替换完成！\n成功: {success}\n失败/跳过: {failed}",
                "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
