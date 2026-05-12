using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ModelColorTool
{
    public enum ColorScheme
    {
        Random,
        Sequential,
        ByType
    }

    public partial class MainForm : Form
    {
        private List<Color> _palette = new List<Color>();
        private Random _rng = new Random();
        private ColorScheme _scheme = ColorScheme.Random;
        private readonly List<ComponentInfo> _components = new List<ComponentInfo>();

        public MainForm()
        {
            InitializeComponent();
            InitDefaultPalette();
            PopulateSchemeCombo();
        }

        private void InitDefaultPalette()
        {
            _palette = new List<Color>
            {
                Color.FromArgb(220, 50, 50),    // 红
                Color.FromArgb(50, 150, 220),   // 蓝
                Color.FromArgb(50, 180, 80),    // 绿
                Color.FromArgb(230, 160, 40),   // 橙
                Color.FromArgb(150, 80, 200),   // 紫
                Color.FromArgb(40, 200, 200),   // 青
                Color.FromArgb(230, 200, 50),   // 黄
                Color.FromArgb(200, 100, 150),  // 粉
                Color.FromArgb(100, 100, 100),  // 灰
                Color.FromArgb(180, 120, 60),   // 棕
                Color.FromArgb(60, 60, 160),    // 深蓝
                Color.FromArgb(120, 200, 120),  // 浅绿
            };
            RefreshPalettePreview();
        }

        private void PopulateSchemeCombo()
        {
            cboScheme.Items.Add("随机上色");
            cboScheme.Items.Add("顺序上色");
            cboScheme.Items.Add("按类型上色");
            cboScheme.SelectedIndex = 0;
        }

        private void RefreshPalettePreview()
        {
            pnlPalettePreview.Invalidate();
        }

        private void pnlPalettePreview_Paint(object sender, PaintEventArgs e)
        {
            if (_palette.Count == 0) return;
            var g = e.Graphics;
            int w = pnlPalettePreview.Width / _palette.Count;
            for (int i = 0; i < _palette.Count; i++)
            {
                using (var brush = new SolidBrush(_palette[i]))
                {
                    g.FillRectangle(brush, i * w, 0, w, pnlPalettePreview.Height);
                }
            }
        }

        private void btnAddColor_Click(object sender, EventArgs e)
        {
            using (var dlg = new ColorDialog())
            {
                dlg.FullOpen = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _palette.Add(dlg.Color);
                    RefreshPalettePreview();
                    UpdateColorList();
                }
            }
        }

        private void btnRemoveColor_Click(object sender, EventArgs e)
        {
            if (lstColors.SelectedIndex >= 0 && lstColors.SelectedIndex < _palette.Count)
            {
                _palette.RemoveAt(lstColors.SelectedIndex);
                RefreshPalettePreview();
                UpdateColorList();
            }
        }

        private void btnRandomPalette_Click(object sender, EventArgs e)
        {
            _palette.Clear();
            for (int i = 0; i < 12; i++)
            {
                _palette.Add(Color.FromArgb(
                    _rng.Next(50, 240),
                    _rng.Next(50, 240),
                    _rng.Next(50, 240)));
            }
            RefreshPalettePreview();
            UpdateColorList();
        }

        private void UpdateColorList()
        {
            lstColors.Items.Clear();
            for (int i = 0; i < _palette.Count; i++)
            {
                var c = _palette[i];
                lstColors.Items.Add($"#{i + 1}: R={c.R} G={c.G} B={c.B}");
            }
        }

        private void cboScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            _scheme = (ColorScheme)cboScheme.SelectedIndex;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var swApp = GetSwApp();
            if (swApp == null)
            {
                lblStatus.Text = "状态: 无法连接 SolidWorks";
                return;
            }

            dynamic model = swApp.ActiveDoc;
            if (model == null)
            {
                lblStatus.Text = "状态: 没有打开的文档";
                return;
            }

            _components.Clear();
            lstComponents.Items.Clear();

            int docType = model.GetType();
            if (docType == 2) // 装配体
            {
                EnumerateComponents(model);
            }
            else if (docType == 1) // 零件
            {
                _components.Add(new ComponentInfo
                {
                    Name = System.IO.Path.GetFileName((string)model.GetPathName()),
                    Path = (string)model.GetPathName(),
                    IsAssembly = false,
                    Component = null
                });
                lstComponents.Items.Add(_components.Last().Name);
            }

            lblStatus.Text = $"状态: 已加载 {_components.Count} 个组件";
            if (_components.Count > 0)
                UpdateColorList();
        }

        private void EnumerateComponents(dynamic assembly)
        {
            dynamic compMgr = assembly.ConfigurationManager.ActiveConfiguration.GetRootComponent3(true);
            if (compMgr == null) return;
            EnumerateChildren(compMgr, 0);
        }

        private void EnumerateChildren(dynamic component, int depth)
        {
            object[] children = (object[])component.GetChildren();
            if (children == null) return;

            foreach (dynamic child in children)
            {
                string name = (string)child.Name2;
                string path = (string)child.GetPathName();
                int childType = child.GetType2(); // 1=part, 2=assembly
                bool isAsm = childType == 2;

                _components.Add(new ComponentInfo
                {
                    Name = name,
                    Path = path,
                    IsAssembly = isAsm,
                    Component = child
                });
                lstComponents.Items.Add(new string(' ', depth * 2) + name);

                if (isAsm)
                    EnumerateChildren(child, depth + 1);

                Marshal.ReleaseComObject(child);
            }
        }

        private void btnApplySelected_Click(object sender, EventArgs e)
        {
            if (_palette.Count == 0)
            {
                MessageBox.Show("请先设置调色板。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var indices = new List<int>();
            foreach (int idx in lstComponents.SelectedIndices)
            {
                if (idx < _components.Count)
                    indices.Add(idx);
            }

            if (indices.Count == 0)
            {
                MessageBox.Show("请在组件列表中选择要上色的组件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int count = ApplyColorsToComponents(indices);
            lblStatus.Text = $"状态: 已为 {count} 个组件上色";
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            if (_palette.Count == 0)
            {
                MessageBox.Show("请先设置调色板。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_components.Count == 0)
            {
                MessageBox.Show("请先连接并加载组件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var indices = Enumerable.Range(0, _components.Count).ToList();
            int count = ApplyColorsToComponents(indices);
            lblStatus.Text = $"状态: 已为 {count} 个组件上色";
        }

        private int ApplyColorsToComponents(List<int> indices)
        {
            int count = 0;
            int paletteIdx = 0;

            foreach (int idx in indices)
            {
                var comp = _components[idx];
                Color color = PickColor(comp, ref paletteIdx, idx);
                try
                {
                    ApplyColorToComponent(comp, color);
                    count++;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"上色失败 {comp.Name}: {ex.Message}");
                }
            }

            // 刷新 SolidWorks 视图
            try
            {
                var swApp = GetSwApp();
                swApp?.ActiveDoc?.GraphicsRedraw2();
            }
            catch { }

            return count;
        }

        private Color PickColor(ComponentInfo comp, ref int paletteIdx, int globalIdx)
        {
            switch (_scheme)
            {
                case ColorScheme.Random:
                    return _palette[_rng.Next(_palette.Count)];

                case ColorScheme.Sequential:
                    var c = _palette[paletteIdx % _palette.Count];
                    paletteIdx++;
                    return c;

                case ColorScheme.ByType:
                    return comp.IsAssembly ? _palette[0] : _palette[1 % _palette.Count];

                default:
                    return _palette[0];
            }
        }

        private void ApplyColorToComponent(ComponentInfo comp, Color color)
        {
            if (comp.Component == null) return;

            dynamic swModel = comp.Component.GetModelDoc2();
            if (swModel == null) return;

            // 通过渲染材质/外观设置颜色
            // swDisplayStateOpts_e
            dynamic visProps = swModel.Extension.GetDisplayStateSetting(1); // swDisplayStateOpts_AllDisplayState
            // 使用 Appearance 设置颜色

            // 方法: 通过 MaterialProperty 设置 RGB 颜色
            // 转换为 SolidWorks 颜色值 (0-1 范围)
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            // 使用 ModelDoc2 的 Appearance 设置
            // 先尝试通过渲染材质 API
            try
            {
                dynamic renderMaterial = swModel.Extension.CreateRenderMaterial("");
                if (renderMaterial != null)
                {
                    renderMaterial.PrimaryColor = System.Convert.ToInt32(color.R) |
                        (System.Convert.ToInt32(color.G) << 8) |
                        (System.Convert.ToInt32(color.B) << 16);
                }
            }
            catch
            {
                // 备用方案: 通过 SceneMaterial 方法
                try
                {
                    dynamic appMgr = swModel.Extension;
                    // 设置组件外观颜色 - 使用底层 API
                    int swColorRef = System.Convert.ToInt32(color.R) |
                        (System.Convert.ToInt32(color.G) << 8) |
                        (System.Convert.ToInt32(color.B) << 16);

                    // 通过 Component 属性设置
                    comp.Component.Color = swColorRef;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"颜色设置失败: {ex.Message}");
                }
            }

            Marshal.ReleaseComObject(swModel);
        }

        private dynamic GetSwApp()
        {
            try { return Marshal.GetActiveObject("SldWorks.Application"); }
            catch { return null; }
        }
    }

    public class ComponentInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsAssembly { get; set; }
        public dynamic Component { get; set; }

        public override string ToString() => Name;
    }
}
