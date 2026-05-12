using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PropertyCardV2
{
    public partial class MainForm : Form
    {
        private PropertyEditor _editor;
        private List<string> _filePaths = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            _editor = new PropertyEditor();
            InitCommonProperties();
        }

        private void InitCommonProperties()
        {
            string[] commonProps = { "材料", "重量", "表面处理", "零件号", "描述", "作者", "创建日期" };
            foreach (var prop in commonProps)
            {
                int row = dgvProperties.Rows.Add(prop, "");
                dgvProperties.Rows[row].Tag = "common";
            }
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "SolidWorks 文件 (*.sldprt;*.sldasm)|*.sldprt;*.sldasm|所有文件 (*.*)|*.*";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in ofd.FileNames)
                    {
                        if (!_filePaths.Contains(file))
                        {
                            _filePaths.Add(file);
                            lstFiles.Items.Add(Path.GetFileName(file));
                        }
                    }
                }
            }
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            _filePaths.Clear();
            lstFiles.Items.Clear();
        }

        private void btnLoadProperties_Click(object sender, EventArgs e)
        {
            if (lstFiles.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string filePath = _filePaths[lstFiles.SelectedIndex];
            var props = _editor.ReadProperties(filePath);
            dgvProperties.Rows.Clear();
            foreach (var kv in props)
            {
                dgvProperties.Rows.Add(kv.Key, kv.Value);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (_filePaths.Count == 0)
            {
                MessageBox.Show("请先添加文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var props = new Dictionary<string, string>();
            foreach (DataGridViewRow row in dgvProperties.Rows)
            {
                if (row.IsNewRow) continue;
                string name = row.Cells[0].Value?.ToString()?.Trim();
                string value = row.Cells[1].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    props[name] = value ?? "";
                }
            }
            int success = 0;
            int fail = 0;
            foreach (var filePath in _filePaths)
            {
                try
                {
                    _editor.WriteProperties(filePath, props);
                    success++;
                }
                catch (Exception ex)
                {
                    fail++;
                    System.Diagnostics.Debug.WriteLine($"写入失败 {filePath}: {ex.Message}");
                }
            }
            MessageBox.Show($"批量应用完成。\n成功: {success}\n失败: {fail}", "结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "属性模板 (*.proptmpl)|*.proptmpl";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(sfd.FileName))
                    {
                        foreach (DataGridViewRow row in dgvProperties.Rows)
                        {
                            if (row.IsNewRow) continue;
                            string name = row.Cells[0].Value?.ToString() ?? "";
                            string value = row.Cells[1].Value?.ToString() ?? "";
                            writer.WriteLine($"{name}\t{value}");
                        }
                    }
                    MessageBox.Show("模板已保存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "属性模板 (*.proptmpl)|*.proptmpl";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    dgvProperties.Rows.Clear();
                    foreach (var line in File.ReadAllLines(ofd.FileName))
                    {
                        var parts = line.Split('\t');
                        if (parts.Length >= 2)
                        {
                            dgvProperties.Rows.Add(parts[0], parts[1]);
                        }
                        else if (parts.Length == 1)
                        {
                            dgvProperties.Rows.Add(parts[0], "");
                        }
                    }
                    MessageBox.Show("模板已加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            dgvProperties.Rows.Add("", "");
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            if (dgvProperties.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProperties.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dgvProperties.Rows.Remove(row);
                }
            }
        }
    }
}
