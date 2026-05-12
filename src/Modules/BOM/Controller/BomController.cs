using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using McBom.Model;

namespace McBom.Controller
{
    /// <summary>
    /// SolidWorks BOM提取控制器
    /// 负责与SolidWorks COM交互，提取装配体结构和零件属性
    /// </summary>
    public class BomController : IDisposable
    {
        private dynamic _swApp;
        private bool _ownsApp;

        public BomController()
        {
        }

        /// <summary>
        /// 获取或创建SolidWorks应用实例
        /// </summary>
        private dynamic GetSwApp()
        {
            if (_swApp != null) return _swApp;

            try
            {
                // 尝试连接已运行的SolidWorks实例
                _swApp = Marshal.GetActiveObject("SldWorks.Application");
                _ownsApp = false;
            }
            catch (COMException)
            {
                // 启动新实例
                var swType = Type.GetTypeFromProgID("SldWorks.Application");
                if (swType == null)
                    throw new InvalidOperationException("未找到SolidWorks安装。请确保SolidWorks已安装。");

                _swApp = Activator.CreateInstance(swType);
                _swApp.Visible = false;
                _ownsApp = true;
            }

            return _swApp;
        }

        /// <summary>
        /// 从装配体文件提取BOM数据
        /// </summary>
        public List<BomItem> ExtractBomFromAssembly(string filePath, BomConfig config)
        {
            var swApp = GetSwApp();
            int errors = 0, warnings = 0;

            dynamic swModel = swApp.OpenDoc6(
                filePath,
                2, // swDocASSEMBLY
                1, // swOpenDocOptions_Silent
                "",
                ref errors,
                ref warnings);

            if (swModel == null)
                throw new IOException($"无法打开装配体文件: {filePath} (错误码: {errors})");

            try
            {
                var items = new List<BomItem>();
                dynamic swAssembly = swModel;
                int seq = 0;
                ExtractComponents(swModel, swAssembly, -1, 0, items, ref seq, config);
                return items;
            }
            finally
            {
                swApp.CloseDoc(swModel.GetTitle());
            }
        }

        /// <summary>
        /// 递归提取装配体组件
        /// </summary>
        private void ExtractComponents(
            dynamic parentModel,
            dynamic assembly,
            int parentId,
            int level,
            List<BomItem> items,
            ref int seq,
            BomConfig config)
        {
            object[] components = null;
            try
            {
                components = (object[])assembly.GetComponents(false);
            }
            catch (COMException) { }

            if (components == null) return;

            foreach (dynamic comp in components)
            {
                try
                {
                    dynamic compModel = comp.GetModelDoc2();
                    if (compModel == null) continue;

                    bool isAssembly = compModel.GetType() == 2; // swDocASSEMBLY
                    string configName = comp.ReferencedConfiguration ?? "";

                    var item = new BomItem
                    {
                        ID = ++seq,
                        Level = level + 1,
                        ParentID = parentId,
                        IsAssembly = isAssembly,
                        ModelId = comp.GetID(),
                        FilePath = compModel.GetPathName(),
                        Configuration = configName,
                        IsHidden = comp.IsHidden(false, null)
                    };

                    // 提取属性
                    ExtractPartProperties(compModel, configName, item, config);
                    item.Quantity = GetComponentQuantity(comp);

                    // 计算重量
                    if (config.AutoCalculateWeight && item.UnitWeight <= 0)
                    {
                        item.UnitWeight = CalculateWeight(compModel, configName, config);
                    }

                    items.Add(item);

                    // 递归处理子装配体
                    if (isAssembly && config.IncludeSubAssemblies)
                    {
                        dynamic subAssembly = compModel;
                        ExtractComponents(compModel, subAssembly, item.ID, level + 1,
                            items, ref seq, config);
                    }
                }
                catch (COMException ex)
                {
                    Debug.WriteLine($"处理组件时COM错误: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 提取零件自定义属性
        /// </summary>
        public void ExtractPartProperties(dynamic model, string configName, BomItem item, BomConfig config)
        {
            if (model == null) return;

            dynamic swCustPropMgr = model.Extension.CustomPropertyManager[""];
            dynamic swConfigPropMgr = configName != ""
                ? model.Extension.CustomPropertyManager[configName]
                : null;

            // 按优先级从配置属性 -> 文件属性读取
            foreach (var mapping in config.PropertyMapping)
            {
                string swPropName = mapping.Key;
                string bomField = mapping.Value;
                string value = null;

                // 先从配置属性读取
                if (swConfigPropMgr != null)
                {
                    value = GetCustomPropertyValue(swConfigPropMgr, swPropName);
                }

                // 再从文件属性读取
                if (string.IsNullOrEmpty(value) && swCustPropMgr != null)
                {
                    value = GetCustomPropertyValue(swCustPropMgr, swPropName);
                }

                if (string.IsNullOrEmpty(value)) continue;

                switch (bomField)
                {
                    case "PartNo":
                        if (string.IsNullOrEmpty(item.PartNo)) item.PartNo = value;
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(item.Name)) item.Name = value;
                        break;
                    case "Material":
                        if (string.IsNullOrEmpty(item.Material)) item.Material = value;
                        break;
                    case "UnitWeight":
                        if (item.UnitWeight <= 0 && double.TryParse(value, out double w))
                            item.UnitWeight = w;
                        break;
                    case "Remark":
                        if (string.IsNullOrEmpty(item.Remark)) item.Remark = value;
                        break;
                }

                item.CustomProperties[swPropName] = value;
            }

            // 回退: 使用文件名作为名称
            if (string.IsNullOrEmpty(item.Name))
            {
                item.Name = Path.GetFileNameWithoutExtension(model.GetPathName());
            }
        }

        /// <summary>
        /// 获取自定义属性值
        /// </summary>
        private string GetCustomPropertyValue(dynamic propMgr, string propName)
        {
            string valOut = "";
            bool wasResolved = false;
            int resolvedType = 0;

            object unusedPropVal = null;
            propMgr.Get6(propName, false, out valOut, out unusedPropVal, out resolvedType, out wasResolved);
            return valOut?.Trim();
        }

        /// <summary>
        /// 获取组件数量
        /// </summary>
        public int GetComponentQuantity(dynamic comp)
        {
            try
            {
                // 检查是否为阵列实例
                var patternType = comp.GetPatternType();
                if (patternType != 0)
                {
                    return comp.GetPatternComponentCount();
                }
                return 1;
            }
            catch (COMException)
            {
                return 1;
            }
        }

        /// <summary>
        /// 计算零件重量 (基于体积和材料密度)
        /// </summary>
        public double CalculateWeight(dynamic model, string configName, BomConfig config)
        {
            if (model == null) return 0;

            try
            {
                dynamic swModel = model;
                dynamic massProp = ((dynamic)swModel).GetMassProperty(
                    configName,
                    1, // swThisConfiguration
                    0);

                if (massProp != null)
                {
                    // SolidWorks返回的质量单位为 kg (如果单位设置正确)
                    double volume = massProp.Volume; // m3
                    double density = GetMaterialDensity(model, configName, config);
                    return volume * density;
                }
            }
            catch (COMException ex)
            {
                Debug.WriteLine($"计算重量COM错误: {ex.Message}");
            }
            catch (InvalidCastException)
            {
                // 可能不是PartDoc
            }

            return 0;
        }

        /// <summary>
        /// 获取材料密度
        /// </summary>
        private double GetMaterialDensity(dynamic model, string configName, BomConfig config)
        {
            try
            {
                dynamic swModel = model;
                string materialName = "";

                // 尝试从SolidWorks材料数据库获取密度
                try
                {
                    object unusedMatVal = null;
                    dynamic swMaterial = swModel.GetMaterialPropertyName2(configName, out unusedMatVal);
                    if (!string.IsNullOrEmpty(swMaterial))
                    {
                        materialName = swMaterial;
                    }
                }
                catch { }

                // 查找匹配的密度
                if (!string.IsNullOrEmpty(materialName))
                {
                    foreach (var kvp in config.MaterialDensityTable)
                    {
                        if (materialName.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return kvp.Value;
                        }
                    }
                }
            }
            catch (COMException) { }

            return config.DefaultDensity;
        }

        /// <summary>
        /// 获取组件列表 (不递归)
        /// </summary>
        public List<dynamic> GetComponents(dynamic assembly)
        {
            var result = new List<dynamic>();
            object[] components = null;
            try
            {
                components = (object[])assembly.GetComponents(false);
            }
            catch (COMException) { }

            if (components != null)
            {
                foreach (dynamic comp in components)
                {
                    result.Add(comp);
                }
            }
            return result;
        }

        /// <summary>
        /// 构建装配体树结构
        /// </summary>
        public List<BomItem> BuildAssemblyTree(string filePath)
        {
            var config = new BomConfig { IncludeSubAssemblies = true };
            return ExtractBomFromAssembly(filePath, config);
        }

        /// <summary>
        /// 截取WPF窗口为图片
        /// </summary>
        public void CaptureScreenshot(Window window, string filePath)
        {
            var size = new System.Windows.Size(window.ActualWidth, window.ActualHeight);
            var renderTarget = new RenderTargetBitmap(
                (int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            renderTarget.Render(window);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));

            using (var stream = File.Create(filePath))
            {
                encoder.Save(stream);
            }
        }

        public void Dispose()
        {
            if (_ownsApp && _swApp != null)
            {
                try { _swApp.ExitApp(); } catch { }
                Marshal.ReleaseComObject(_swApp);
                _swApp = null;
            }
        }
    }
}
