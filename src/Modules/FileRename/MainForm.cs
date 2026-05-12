using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileRename
{
    public partial class MainForm : Form
    {
        private readonly RenameEngine _engine = new RenameEngine();
        private List<RenameEntry> _previewResults = new List<RenameEntry>();
        private List<RenameEntry> _undoStack = new List<RenameEntry>();

        public MainForm()
        {
            InitializeComponent();
            UpdateFieldVisibility();
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "选择包含文件的文件夹";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = dlg.SelectedPath;
                    LoadFiles();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolder.Text) && Directory.Exists(txtFolder.Text))
                LoadFiles();
        }

        private void LoadFiles()
        {
            lvFiles.Items.Clear();
            _previewResults.Clear();
            btnApply.Enabled = false;

            var extensions = GetSelectedExtensions();
            if (extensions.Count == 0) return;

            var files = new List<FileInfo>();
            foreach (var ext in extensions)
            {
                files.AddRange(new DirectoryInfo(txtFolder.Text).GetFiles("*" + ext, SearchOption.TopDirectoryOnly));
            }

            files = files.OrderBy(f => f.Name).ToList();

            foreach (var fi in files)
            {
                var item = new ListViewItem(fi.Name);
                item.SubItems.Add("");
                item.SubItems.Add("待处理");
                item.Tag = fi.FullName;
                lvFiles.Items.Add(item);
            }

            lblTotal.Text = $"共 {files.Count} 个文件";
        }

        private List<string> GetSelectedExtensions()
        {
            var extensions = new List<string>();
            if (chkAllTypes.Checked) return new List<string> { ".*" };
            if (chkSldprt.Checked) extensions.Add(".sldprt");
            if (chkSldasm.Checked) extensions.Add(".sldasm");
            if (chkSlddrw.Checked) extensions.Add(".slddrw");
            return extensions;
        }

        private void cboRenameMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFieldVisibility();
        }

        private void UpdateFieldVisibility()
        {
            int mode = cboRenameMode.SelectedIndex;
            // prefix mode: show prefix, hide suffix/find/replace/numbering
            lblPrefix.Visible = (mode == 0);
            txtPrefix.Visible = (mode == 0);
            lblSuffix.Visible = (mode == 1);
            txtSuffix.Visible = (mode == 1);
            lblFind.Visible = (mode == 2);
            txtFind.Visible = (mode == 2);
            lblReplace.Visible = (mode == 2);
            txtReplace.Visible = (mode == 2);
            lblStartNum.Visible = (mode == 3);
            nudStartNum.Visible = (mode == 3);
            lblStep.Visible = (mode == 3);
            nudStep.Visible = (mode == 3);
            lblNumDigits.Visible = (mode == 3);
            nudNumDigits.Visible = (mode == 3);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (lvFiles.Items.Count == 0)
            {
                MessageBox.Show("请先选择文件夹并加载文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var pattern = BuildPattern();
            _previewResults = _engine.PreviewRename(GetFilePaths(), pattern);

            lvFiles.Items.Clear();
            for (int i = 0; i < _previewResults.Count; i++)
            {
                var entry = _previewResults[i];
                var item = new ListViewItem(entry.OriginalName);
                item.SubItems.Add(entry.NewName);
                item.SubItems.Add(entry.Conflict ? "冲突" : "就绪");
                item.Tag = entry.FullPath;
                lvFiles.Items.Add(item);
            }

            int conflictCount = _previewResults.Count(r => r.Conflict);
            lblPreview.Text = $"预览: {_previewResults.Count} 个文件将被重命名" +
                (conflictCount > 0 ? $"，{conflictCount} 个冲突" : "");
            btnApply.Enabled = conflictCount == 0 && _previewResults.Count > 0;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (_previewResults.Count == 0) return;

            var result = MessageBox.Show(
                $"确认要重命名 {_previewResults.Count} 个文件吗？此操作可以撤销。",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            progressBar.Visible = true;
            progressBar.Maximum = _previewResults.Count;
            progressBar.Value = 0;

            _undoStack = _engine.BatchRename(_previewResults);

            for (int i = 0; i < _previewResults.Count; i++)
            {
                lvFiles.Items[i].SubItems[2].Text = File.Exists(_previewResults[i].NewFullPath) ? "完成" : "失败";
                progressBar.Value = i + 1;
            }

            progressBar.Visible = false;
            btnUndo.Enabled = _undoStack.Count > 0;
            btnApply.Enabled = false;

            MessageBox.Show($"重命名完成！成功: {_undoStack.Count} 个", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_undoStack.Count == 0) return;

            var result = MessageBox.Show(
                $"确认要撤销上次的 {_undoStack.Count} 个重命名操作吗？",
                "撤销确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            _engine.UndoRename(_undoStack);
            _undoStack.Clear();
            btnUndo.Enabled = false;
            LoadFiles();

            MessageBox.Show("撤销完成！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<string> GetFilePaths()
        {
            var paths = new List<string>();
            foreach (ListViewItem item in lvFiles.Items)
            {
                if (item.Tag is string path)
                    paths.Add(path);
            }
            return paths;
        }

        private RenamePattern BuildPattern()
        {
            return new RenamePattern
            {
                Mode = (RenameMode)cboRenameMode.SelectedIndex,
                Prefix = txtPrefix.Text,
                Suffix = txtSuffix.Text,
                FindText = txtFind.Text,
                ReplaceText = txtReplace.Text,
                StartNumber = (int)nudStartNum.Value,
                Step = (int)nudStep.Value,
                NumberDigits = (int)nudNumDigits.Value
            };
        }
    }
}
