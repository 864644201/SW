using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DrawingColorTool
{
    public partial class MainForm : Form
    {
        private readonly LayerColorManager _manager = new LayerColorManager();
        private List<string> _drawingFiles = new List<string>();
        private string _selectedFile;

        public MainForm()
        {
            InitializeComponent();
            LoadDefaultScheme();
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "选择包含工程图文件的文件夹";
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtFolder.Text = dlg.SelectedPath;
            }
        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolder.Text) || !Directory.Exists(txtFolder.Text))
            {
                MessageBox.Show("请先选择有效的文件夹。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _drawingFiles = Directory.GetFiles(txtFolder.Text, "*.slddrw", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(txtFolder.Text, "*.SLDDRW", SearchOption.TopDirectoryOnly))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(f => f)
                .ToList();

            lvFiles.Items.Clear();
            foreach (var file in _drawingFiles)
            {
                var item = new ListViewItem(Path.GetFileName(file)) { Tag = file };
                lvFiles.Items.Add(item);
            }

            lblStatus.Text = $"已加载 {_drawingFiles.Count} 个工程图文件";
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 0) return;

            _selectedFile = lvFiles.SelectedItems[0].Tag as string;
            LoadLayersForFile(_selectedFile);
        }

        private void LoadLayersForFile(string filePath)
        {
            lvLayers.Items.Clear();
            cboLineTypeLayer.Items.Clear();

            // In a real implementation, this would read layer info from the SolidWorks drawing.
            // Here we show a simulation with common layer names.
            var layers = _manager.GetLayers(filePath);

            foreach (var layer in layers)
            {
                var item = new ListViewItem(layer.Name);
                item.SubItems.Add(layer.ColorName);
                item.SubItems.Add(layer.LineTypeName);
                item.Tag = layer;
                lvLayers.Items.Add(item);

                cboLineTypeLayer.Items.Add(layer.Name);
            }

            if (cboLineTypeLayer.Items.Count > 0)
                cboLineTypeLayer.SelectedIndex = 0;
        }

        private void btnApplyScheme_Click(object sender, EventArgs e)
        {
            string schemeName = cboScheme.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(schemeName)) return;

            var scheme = _manager.GetColorScheme(schemeName);
            if (scheme == null) return;

            // Apply the color scheme to all layers in the view
            foreach (ListViewItem item in lvLayers.Items)
            {
                if (item.Tag is LayerInfo layer)
                {
                    if (scheme.TryGetValue(layer.Name, out var colorInfo))
                    {
                        layer.Color = colorInfo.Color;
                        layer.ColorName = colorInfo.Name;
                        item.SubItems[1].Text = colorInfo.Name;
                    }
                }
            }

            lblStatus.Text = $"已应用配色方案: {schemeName}";
        }

        private void btnEditColor_Click(object sender, EventArgs e)
        {
            if (lvLayers.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var layer = lvLayers.SelectedItems[0].Tag as LayerInfo;
            if (layer == null) return;

            using (var dlg = new ColorDialog())
            {
                dlg.Color = layer.Color;
                dlg.FullOpen = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    layer.Color = dlg.Color;
                    layer.ColorName = $"RGB({dlg.Color.R},{dlg.Color.G},{dlg.Color.B})";
                    lvLayers.SelectedItems[0].SubItems[1].Text = layer.ColorName;
                    lvLayers.SelectedItems[0].SubItems[1].BackColor = dlg.Color;
                    lvLayers.SelectedItems[0].SubItems[1].ForeColor = GetContrastColor(dlg.Color);
                }
            }
        }

        private void btnApplyLineType_Click(object sender, EventArgs e)
        {
            if (cboLineTypeLayer.SelectedIndex < 0 || cboLineType.SelectedIndex < 0) return;

            string layerName = cboLineTypeLayer.SelectedItem.ToString();
            string lineType = cboLineType.SelectedItem.ToString();

            foreach (ListViewItem item in lvLayers.Items)
            {
                if (item.Tag is LayerInfo layer && layer.Name == layerName)
                {
                    layer.LineTypeName = lineType;
                    layer.LineType = cboLineType.SelectedIndex;
                    item.SubItems[2].Text = lineType;
                    break;
                }
            }

            lblStatus.Text = $"已设置图层 {layerName} 的线型为 {lineType}";
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            if (_drawingFiles.Count == 0)
            {
                MessageBox.Show("没有可处理的文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Collect current layer settings
            var layerSettings = new List<LayerInfo>();
            foreach (ListViewItem item in lvLayers.Items)
            {
                if (item.Tag is LayerInfo layer)
                    layerSettings.Add(layer);
            }

            if (layerSettings.Count == 0)
            {
                MessageBox.Show("请先选择一个文件并配置图层颜色。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"确认要将当前颜色设置应用到 {_drawingFiles.Count} 个工程图文件吗？",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            progressBar.Visible = true;
            progressBar.Maximum = _drawingFiles.Count;
            progressBar.Value = 0;

            int success = 0;
            for (int i = 0; i < _drawingFiles.Count; i++)
            {
                try
                {
                    _manager.ApplyLayerColors(_drawingFiles[i], layerSettings);
                    success++;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error applying to {_drawingFiles[i]}: {ex.Message}");
                }
                progressBar.Value = i + 1;
            }

            progressBar.Visible = false;
            lblStatus.Text = $"完成: {success}/{_drawingFiles.Count} 个文件";
            MessageBox.Show($"应用完成！成功: {success}/{_drawingFiles.Count}", "完成",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_selectedFile != null)
                LoadLayersForFile(_selectedFile);
            lblStatus.Text = "已重置为默认设置";
        }

        private void LoadDefaultScheme()
        {
            // Populate with default layer color scheme
            _manager.RegisterScheme("默认", new Dictionary<string, ColorInfo>
            {
                ["Visible"] = new ColorInfo("黑色", Color.Black),
                ["Hidden"] = new ColorInfo("灰色", Color.Gray),
                ["Center"] = new ColorInfo("红色", Color.Red),
                ["Dimension"] = new ColorInfo("蓝色", Color.Blue),
                ["Annotation"] = new ColorInfo("绿色", Color.Green)
            });

            _manager.RegisterScheme("黑白打印", new Dictionary<string, ColorInfo>
            {
                ["Visible"] = new ColorInfo("黑色", Color.Black),
                ["Hidden"] = new ColorInfo("深灰", Color.DarkGray),
                ["Center"] = new ColorInfo("黑色", Color.Black),
                ["Dimension"] = new ColorInfo("黑色", Color.Black),
                ["Annotation"] = new ColorInfo("黑色", Color.Black)
            });

            _manager.RegisterScheme("彩色标准", new Dictionary<string, ColorInfo>
            {
                ["Visible"] = new ColorInfo("白色", Color.White),
                ["Hidden"] = new ColorInfo("黄色", Color.Yellow),
                ["Center"] = new ColorInfo("青色", Color.Cyan),
                ["Dimension"] = new ColorInfo("绿色", Color.Lime),
                ["Annotation"] = new ColorInfo("品红", Color.Magenta)
            });

            _manager.RegisterScheme("高对比度", new Dictionary<string, ColorInfo>
            {
                ["Visible"] = new ColorInfo("白色", Color.White),
                ["Hidden"] = new ColorInfo("红色", Color.Red),
                ["Center"] = new ColorInfo("黄色", Color.Yellow),
                ["Dimension"] = new ColorInfo("青色", Color.Cyan),
                ["Annotation"] = new ColorInfo("橙色", Color.Orange)
            });
        }

        private static Color GetContrastColor(Color bg)
        {
            // Returns black or white depending on background brightness
            int brightness = (bg.R * 299 + bg.G * 587 + bg.B * 114) / 1000;
            return brightness > 128 ? Color.Black : Color.White;
        }
    }
}
