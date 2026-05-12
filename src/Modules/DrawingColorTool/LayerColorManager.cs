using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace DrawingColorTool
{
    public class ColorInfo
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public ColorInfo(string name, Color color)
        {
            Name = name;
            Color = color;
        }
    }

    public class LayerInfo
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public string ColorName { get; set; }
        public int LineType { get; set; }
        public string LineTypeName { get; set; }
        public bool Visible { get; set; } = true;

        public LayerInfo(string name)
        {
            Name = name;
            Color = Color.White;
            ColorName = "白色";
            LineType = 0;
            LineTypeName = "实线";
        }
    }

    /// <summary>
    /// Manages layer colors and line types for SolidWorks drawing files.
    /// Uses SolidWorks API to read/modify layer properties.
    /// </summary>
    public class LayerColorManager
    {
        private readonly Dictionary<string, Dictionary<string, ColorInfo>> _schemes
            = new Dictionary<string, Dictionary<string, ColorInfo>>(StringComparer.OrdinalIgnoreCase);

        // Standard SolidWorks drawing layer names
        private static readonly string[] DefaultLayers = new[]
        {
            "Visible", "Hidden", "Center", "Dimension", "Annotation",
            "Section", "Detail", "TitleBlock", "Border", "Notes"
        };

        /// <summary>
        /// Registers a named color scheme.
        /// </summary>
        public void RegisterScheme(string name, Dictionary<string, ColorInfo> scheme)
        {
            _schemes[name] = scheme;
        }

        /// <summary>
        /// Gets a registered color scheme by name.
        /// </summary>
        public Dictionary<string, ColorInfo> GetColorScheme(string name)
        {
            _schemes.TryGetValue(name, out var scheme);
            return scheme;
        }

        /// <summary>
        /// Returns available scheme names.
        /// </summary>
        public IEnumerable<string> GetSchemeNames()
        {
            return _schemes.Keys;
        }

        /// <summary>
        /// Reads layer information from a SolidWorks drawing file.
        /// Uses SolidWorks COM API when available; returns default layers otherwise.
        /// </summary>
        public List<LayerInfo> GetLayers(string filePath)
        {
            var layers = new List<LayerInfo>();

            try
            {
                var swApp = GetSolidWorksApp();
                if (swApp != null)
                {
                    int errors = 0, warnings = 0;
                    dynamic swDoc = swApp.OpenDoc6(filePath, 3, 1, "", ref errors, ref warnings);

                    if (swDoc != null)
                    {
                        try
                        {
                            dynamic layerMgr = swDoc.GetLayerManager();
                            if (layerMgr != null)
                            {
                                object layerNames = layerMgr.GetLayerNames();
                                if (layerNames is string[] names)
                                {
                                    foreach (string name in names)
                                    {
                                        var layer = new LayerInfo(name);

                                        try
                                        {
                                            dynamic swLayer = layerMgr.GetLayer(name);
                                            if (swLayer != null)
                                            {
                                                int colorVal = swLayer.Color;
                                                layer.Color = ColorTranslator.FromWin32(colorVal);
                                                layer.ColorName = $"RGB({layer.Color.R},{layer.Color.G},{layer.Color.B})";
                                                layer.LineType = swLayer.Style;
                                                layer.LineTypeName = GetLineTypeName(layer.LineType);
                                                layer.Visible = swLayer.Visible;
                                            }
                                        }
                                        catch { }

                                        layers.Add(layer);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            swApp.CloseDoc(swDoc.GetTitle());
                        }
                    }
                }
            }
            catch
            {
                // SolidWorks not available; return simulated layers
            }

            // If no layers were read (SolidWorks not available), return defaults
            if (layers.Count == 0)
            {
                layers = DefaultLayers.Select(name => new LayerInfo(name)).ToList();
            }

            return layers;
        }

        /// <summary>
        /// Applies layer color settings to a SolidWorks drawing file.
        /// </summary>
        public void ApplyLayerColors(string filePath, List<LayerInfo> layerSettings)
        {
            var swApp = GetSolidWorksApp();
            if (swApp == null)
                throw new InvalidOperationException("SolidWorks 未安装或无法启动。需要安装 SolidWorks 才能使用此功能。");

            int errors = 0, warnings = 0;
            dynamic swDoc = swApp.OpenDoc6(filePath, 3, 1, "", ref errors, ref warnings);

            if (swDoc == null)
                throw new InvalidOperationException($"无法打开文件: {filePath} (错误码: {errors})");

            try
            {
                dynamic layerMgr = swDoc.GetLayerManager();
                if (layerMgr == null)
                    throw new InvalidOperationException("无法获取图层管理器。");

                foreach (var layerInfo in layerSettings)
                {
                    try
                    {
                        dynamic swLayer = layerMgr.GetLayer(layerInfo.Name);
                        if (swLayer != null)
                        {
                            // Set color (SolidWorks uses Win32 COLORREF format: 0x00BBGGRR)
                            int colorRef = ColorTranslator.ToWin32(layerInfo.Color);
                            swLayer.Color = colorRef;

                            // Set line style (0=continuous, 1=hidden, 2=center, 3=phantom, 4=dot)
                            swLayer.Style = layerInfo.LineType;

                            // Set visibility
                            swLayer.Visible = layerInfo.Visible;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to set layer {layerInfo.Name}: {ex.Message}");
                    }
                }

                // Save the document
                swDoc.Save();
            }
            finally
            {
                swApp.CloseDoc(swDoc.GetTitle());
            }
        }

        private dynamic GetSolidWorksApp()
        {
            try
            {
                return Marshal.GetActiveObject("SldWorks.Application");
            }
            catch
            {
                try
                {
                    var type = Type.GetTypeFromProgID("SldWorks.Application");
                    if (type != null)
                    {
                        dynamic app = Activator.CreateInstance(type);
                        app.Visible = false;
                        return app;
                    }
                }
                catch { }
            }
            return null;
        }

        private string GetLineTypeName(int lineType)
        {
            switch (lineType)
            {
                case 0: return "实线";
                case 1: return "虚线";
                case 2: return "点划线";
                case 3: return "双点划线";
                case 4: return "细实线";
                default: return "实线";
            }
        }

        /// <summary>
        /// Converts an integer line type enum to the SolidWorks style index.
        /// </summary>
        public static int GetLineStyleIndex(string lineTypeName)
        {
            switch (lineTypeName)
            {
                case "实线": return 0;
                case "虚线": return 1;
                case "点划线": return 2;
                case "双点划线": return 3;
                case "细实线": return 4;
                default: return 0;
            }
        }
    }
}
