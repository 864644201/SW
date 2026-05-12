using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace BatchReplace
{
    /// <summary>
    /// 替换操作类型
    /// </summary>
    public enum ReplaceAction
    {
        /// <summary>组件替换 - 替换装配体中的零件/子装配体</summary>
        ComponentSwap,

        /// <summary>特征压缩 - 批量压缩/解压缩指定特征</summary>
        FeatureSuppression,

        /// <summary>配置切换 - 批量切换到指定配置</summary>
        ConfigurationSwitch,

        /// <summary>属性修改 - 批量修改自定义属性</summary>
        PropertyChange
    }

    /// <summary>
    /// 替换规则
    /// </summary>
    public class ReplaceRule
    {
        /// <summary>操作类型</summary>
        public ReplaceAction Action { get; set; }

        /// <summary>查找模式 (文件名/零件号/路径)</summary>
        public string SearchPattern { get; set; }

        /// <summary>替换目标</summary>
        public string ReplaceWith { get; set; }

        /// <summary>是否区分大小写</summary>
        public bool CaseSensitive { get; set; }

        /// <summary>是否使用正则表达式</summary>
        public bool UseRegex { get; set; }

        /// <summary>特征操作: true=压缩, false=解压缩</summary>
        public bool SuppressFeature { get; set; } = true;

        /// <summary>属性名 (用于属性修改)</summary>
        public string PropertyName { get; set; }

        /// <summary>属性新值</summary>
        public string PropertyValue { get; set; }

        /// <summary>是否启用</summary>
        public bool Enabled { get; set; } = true;

        public override string ToString()
        {
            switch (Action)
            {
                case ReplaceAction.ComponentSwap:
                    return $"替换组件: {SearchPattern} -> {ReplaceWith}";
                case ReplaceAction.FeatureSuppression:
                    return $"特征{(SuppressFeature ? "压缩" : "解压缩")}: {SearchPattern}";
                case ReplaceAction.ConfigurationSwitch:
                    return $"切换配置: -> {ReplaceWith}";
                case ReplaceAction.PropertyChange:
                    return $"修改属性: {PropertyName} = {PropertyValue}";
                default:
                    return base.ToString();
            }
        }
    }

    /// <summary>
    /// 处理结果
    /// </summary>
    public class ReplaceResult
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int AffectedCount { get; set; }
        public TimeSpan Duration { get; set; }
    }

    /// <summary>
    /// 进度回调
    /// </summary>
    public class ProgressInfo
    {
        public int Current { get; set; }
        public int Total { get; set; }
        public string CurrentFile { get; set; }
        public string CurrentAction { get; set; }
        public bool Cancelled { get; set; }
        public float Percent => Total > 0 ? (float)Current / Total * 100 : 0;
    }

    /// <summary>
    /// 批量替换引擎 - 执行SolidWorks文件的批量替换操作
    /// </summary>
    public class ReplaceEngine : IDisposable
    {
        private dynamic _swApp;
        private bool _ownsApp;
        private bool _cancelRequested;

        public event Action<ProgressInfo> ProgressChanged;
        public event Action<ReplaceResult> FileProcessed;

        public ReplaceEngine()
        {
        }

        /// <summary>
        /// 连接SolidWorks
        /// </summary>
        private bool Connect()
        {
            if (_swApp != null) return true;

            try
            {
                _swApp = Marshal.GetActiveObject("SldWorks.Application");
                _ownsApp = false;
                return true;
            }
            catch (COMException)
            {
                try
                {
                    var swType = Type.GetTypeFromProgID("SldWorks.Application");
                    if (swType == null) return false;
                    _swApp = Activator.CreateInstance(swType);
                    _swApp.Visible = false;
                    _ownsApp = true;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 请求取消操作
        /// </summary>
        public void Cancel()
        {
            _cancelRequested = true;
        }

        /// <summary>
        /// 获取文件夹下所有SolidWorks文件
        /// </summary>
        public List<string> GetSolidWorksFiles(string folderPath, bool includeSubfolders)
        {
            var extensions = new[] { ".sldasm", ".sldprt" };
            var option = includeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            return Directory.GetFiles(folderPath, "*.*", option)
                .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                .OrderBy(f => f)
                .ToList();
        }

        /// <summary>
        /// 批量执行替换操作
        /// </summary>
        public List<ReplaceResult> ExecuteBatch(List<string> files, List<ReplaceRule> rules)
        {
            if (!Connect())
                throw new InvalidOperationException("无法连接SolidWorks");

            _cancelRequested = false;
            var results = new List<ReplaceResult>();

            for (int i = 0; i < files.Count; i++)
            {
                if (_cancelRequested) break;

                var filePath = files[i];
                var progress = new ProgressInfo
                {
                    Current = i + 1,
                    Total = files.Count,
                    CurrentFile = Path.GetFileName(filePath),
                    CurrentAction = "处理中..."
                };
                ProgressChanged?.Invoke(progress);

                var result = ProcessFile(filePath, rules);
                results.Add(result);
                FileProcessed?.Invoke(result);
            }

            return results;
        }

        /// <summary>
        /// 处理单个文件
        /// </summary>
        private ReplaceResult ProcessFile(string filePath, List<ReplaceRule> rules)
        {
            var result = new ReplaceResult
            {
                FilePath = filePath,
                FileName = Path.GetFileName(filePath)
            };

            var sw = Stopwatch.StartNew();
            dynamic swModel = null;

            try
            {
                int errors = 0, warnings = 0;
                int docType = filePath.EndsWith(".sldasm", StringComparison.OrdinalIgnoreCase)
                    ? 2  // swDocASSEMBLY
                    : 1; // swDocPART

                swModel = _swApp.OpenDoc6(filePath, docType,
                    1, // swOpenDocOptions_Silent
                    "", ref errors, ref warnings);

                if (swModel == null)
                {
                    result.Success = false;
                    result.Message = $"无法打开文件 (错误码: {errors})";
                    return result;
                }

                int totalAffected = 0;

                foreach (var rule in rules.Where(r => r.Enabled))
                {
                    switch (rule.Action)
                    {
                        case ReplaceAction.ComponentSwap:
                            totalAffected += DoComponentSwap(swModel, rule);
                            break;
                        case ReplaceAction.FeatureSuppression:
                            totalAffected += DoFeatureSuppression(swModel, rule);
                            break;
                        case ReplaceAction.ConfigurationSwitch:
                            totalAffected += DoConfigurationSwitch(swModel, rule);
                            break;
                        case ReplaceAction.PropertyChange:
                            totalAffected += DoPropertyChange(swModel, rule);
                            break;
                    }
                }

                // 保存
                if (totalAffected > 0)
                {
                    swModel.SaveSilent(filePath);
                }

                result.Success = true;
                result.AffectedCount = totalAffected;
                result.Message = $"成功处理 {totalAffected} 项";
            }
            catch (COMException ex)
            {
                result.Success = false;
                result.Message = $"COM错误: {ex.Message}";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"错误: {ex.Message}";
            }
            finally
            {
                if (swModel != null)
                {
                    try { _swApp.CloseDoc(swModel.GetTitle()); } catch { }
                }
                sw.Stop();
                result.Duration = sw.Elapsed;
            }

            return result;
        }

        /// <summary>
        /// 组件替换
        /// </summary>
        private int DoComponentSwap(dynamic model, ReplaceRule rule)
        {
            if (model.GetType() != 2) // swDocASSEMBLY
                return 0;

            dynamic assembly = model;
            int count = 0;

            object[] components = null;
            try { components = (object[])assembly.GetComponents(false); }
            catch (COMException) { return 0; }
            if (components == null) return 0;

            foreach (dynamic comp in components)
            {
                if (_cancelRequested) break;

                try
                {
                    string compName = Path.GetFileNameWithoutExtension(
                        ((dynamic)comp.GetModelDoc2())?.GetPathName() ?? comp.Name2 ?? "");

                    bool matches = MatchesPattern(compName, rule.SearchPattern, rule);
                    if (!matches) continue;

                    // 执行替换
                    int replaceErrors = 0;
                    bool success = assembly.ReplaceComponents2(
                        comp.Name2,
                        rule.ReplaceWith,
                        comp.ReferencedConfiguration ?? "",
                        true,
                        1, // swReplaceComponentConfiguration_Derive
                        ref replaceErrors);

                    if (success) count++;
                }
                catch (COMException) { }
            }

            return count;
        }

        /// <summary>
        /// 特征压缩/解压缩
        /// </summary>
        private int DoFeatureSuppression(dynamic model, ReplaceRule rule)
        {
            int count = 0;
            dynamic features = model.FirstFeature();

            while (features != null)
            {
                if (_cancelRequested) break;

                try
                {
                    string featName = features.Name ?? "";
                    if (MatchesPattern(featName, rule.SearchPattern, rule))
                    {
                        if (rule.SuppressFeature)
                        {
                            features.SetSuppression2(
                                0, // swSuppressFeature
                                0, // swAllConfiguration
                                null);
                        }
                        else
                        {
                            features.SetSuppression2(
                                1, // swUnSuppressFeature
                                0, // swAllConfiguration
                                null);
                        }
                        count++;
                    }
                }
                catch (COMException) { }

                features = features.GetNextFeature();
            }

            return count;
        }

        /// <summary>
        /// 配置切换
        /// </summary>
        private int DoConfigurationSwitch(dynamic model, ReplaceRule rule)
        {
            var configNames = (string[])model.GetConfigurationNames();
            if (configNames == null || configNames.Length == 0)
                return 0;

            string targetConfig = rule.ReplaceWith;
            if (string.IsNullOrEmpty(targetConfig)) return 0;

            // 检查目标配置是否存在
            if (!configNames.Contains(targetConfig))
                return 0;

            int count = 0;
            if (string.IsNullOrEmpty(rule.SearchPattern))
            {
                // 切换所有配置
                foreach (var configName in configNames)
                {
                    if (_cancelRequested) break;
                    model.ShowConfiguration2(configName);
                    model.EditRebuild3();
                    count++;
                }
            }
            else
            {
                // 只切换匹配的配置
                foreach (var configName in configNames)
                {
                    if (_cancelRequested) break;
                    if (MatchesPattern(configName, rule.SearchPattern, rule))
                    {
                        model.ShowConfiguration2(targetConfig);
                        model.EditRebuild3();
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// 属性修改
        /// </summary>
        private int DoPropertyChange(dynamic model, ReplaceRule rule)
        {
            if (string.IsNullOrEmpty(rule.PropertyName))
                return 0;

            int count = 0;

            // 修改文件级属性
            dynamic custProp = model.Extension.CustomPropertyManager[""];
            if (custProp != null)
            {
                custProp.Add3(rule.PropertyName,
                    30, // swCustomInfoText
                    rule.PropertyValue,
                    1); // swCustomInfoAddResult_OK
                count++;
            }

            // 修改每个配置的属性
            var configNames = (string[])model.GetConfigurationNames();
            if (configNames != null)
            {
                foreach (var configName in configNames)
                {
                    if (_cancelRequested) break;

                    if (!string.IsNullOrEmpty(rule.SearchPattern) &&
                        !MatchesPattern(configName, rule.SearchPattern, rule))
                        continue;

                    dynamic configProp = model.Extension.CustomPropertyManager[configName];
                    if (configProp != null)
                    {
                        configProp.Add3(rule.PropertyName,
                            30, // swCustomInfoText
                            rule.PropertyValue,
                            1); // swCustomInfoAddResult_OK
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// 模式匹配
        /// </summary>
        private bool MatchesPattern(string text, string pattern, ReplaceRule rule)
        {
            if (string.IsNullOrEmpty(text)) return false;
            if (string.IsNullOrEmpty(pattern)) return true;

            var comp = rule.CaseSensitive
                ? StringComparison.Ordinal
                : StringComparison.OrdinalIgnoreCase;

            if (rule.UseRegex)
            {
                var options = rule.CaseSensitive
                    ? System.Text.RegularExpressions.RegexOptions.None
                    : System.Text.RegularExpressions.RegexOptions.IgnoreCase;
                return System.Text.RegularExpressions.Regex.IsMatch(text, pattern, options);
            }

            return text.IndexOf(pattern, comp) >= 0;
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
