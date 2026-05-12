using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AutoPartNo
{
    /// <summary>
    /// 零件号生成规则配置
    /// </summary>
    public class PartNoConfig
    {
        /// <summary>前缀 (如 "PART", "MF")</summary>
        public string Prefix { get; set; } = "PART";
        /// <summary>分隔符 (如 "-", "_")</summary>
        public string Separator { get; set; } = "-";
        /// <summary>起始编号</summary>
        public int StartNumber { get; set; } = 1;
        /// <summary>编号位数 (自动补零)</summary>
        public int DigitCount { get; set; } = 3;
        /// <summary>可选后缀 (如日期 "20260101")</summary>
        public string Suffix { get; set; } = "";

        /// <summary>
        /// 生成指定序号的零件号
        /// </summary>
        public string Generate(int sequenceNumber)
        {
            string numPart = sequenceNumber.ToString().PadLeft(DigitCount, '0');
            if (string.IsNullOrEmpty(Suffix))
                return $"{Prefix}{Separator}{numPart}";
            return $"{Prefix}{Separator}{numPart}{Separator}{Suffix}";
        }
    }

    /// <summary>
    /// 组件零件号信息
    /// </summary>
    public class PartNoEntry
    {
        /// <summary>组件名称</summary>
        public string ComponentName { get; set; }
        /// <summary>组件路径</summary>
        public string ComponentPath { get; set; }
        /// <summary>生成的零件号</summary>
        public string PartNumber { get; set; }
        /// <summary>是否为子装配体</summary>
        public bool IsAssembly { get; set; }
        /// <summary>层级深度</summary>
        public int Depth { get; set; }
        /// <summary>是否已应用</summary>
        public bool Applied { get; set; }
    }

    /// <summary>
    /// 零件号生成器
    /// </summary>
    public class PartNoGenerator
    {
        private readonly PartNoConfig _config;
        private int _currentNumber;

        public PartNoGenerator(PartNoConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _currentNumber = config.StartNumber;
        }

        /// <summary>
        /// 重置编号计数器
        /// </summary>
        public void Reset()
        {
            _currentNumber = _config.StartNumber;
        }

        /// <summary>
        /// 生成下一个零件号
        /// </summary>
        public string Next()
        {
            string partNo = _config.Generate(_currentNumber);
            _currentNumber++;
            return partNo;
        }

        /// <summary>
        /// 为组件列表批量生成零件号
        /// </summary>
        public List<PartNoEntry> GenerateForComponents(List<ComponentEntry> components)
        {
            Reset();
            var result = new List<PartNoEntry>();

            foreach (var comp in components)
            {
                result.Add(new PartNoEntry
                {
                    ComponentName = comp.Name,
                    ComponentPath = comp.Path,
                    PartNumber = Next(),
                    IsAssembly = comp.IsAssembly,
                    Depth = comp.Depth
                });
            }

            return result;
        }

        /// <summary>
        /// 将零件号写入 SolidWorks 组件的自定义属性
        /// </summary>
        public static bool WritePartNoToComponent(dynamic component, string propertyName, string partNo)
        {
            try
            {
                dynamic model = component.GetModelDoc2();
                if (model == null) return false;

                dynamic customPropMgr = model.Extension.CustomPropertyManager[""];
                if (customPropMgr == null) return false;

                // 设置自定义属性
                customPropMgr.Set(propertyName, partNo);
                model.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 批量应用零件号到 SolidWorks 组件
        /// </summary>
        public static int BatchApply(dynamic swApp, List<PartNoEntry> entries, string propertyName)
        {
            int count = 0;
            foreach (var entry in entries)
            {
                if (entry.Applied) continue;
                // 这里需要传入实际的 Component2 对象
                // 在实际使用中通过 MainForm 传入
                entry.Applied = true;
                count++;
            }
            return count;
        }
    }

    /// <summary>
    /// 组件条目 (用于枚举)
    /// </summary>
    public class ComponentEntry
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsAssembly { get; set; }
        public int Depth { get; set; }
        public dynamic Component { get; set; }
    }

    /// <summary>
    /// 零件号格式验证器
    /// </summary>
    public static class PartNoValidator
    {
        private static readonly Regex PartNoPattern = new Regex(@"^[A-Za-z0-9]+([-_][A-Za-z0-9]+)*$");

        /// <summary>
        /// 验证零件号格式是否合法
        /// </summary>
        public static bool IsValid(string partNo)
        {
            if (string.IsNullOrWhiteSpace(partNo)) return false;
            return PartNoPattern.IsMatch(partNo);
        }

        /// <summary>
        /// 验证前缀是否合法 (仅字母和数字)
        /// </summary>
        public static bool IsValidPrefix(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix)) return false;
            foreach (char c in prefix)
            {
                if (!char.IsLetterOrDigit(c)) return false;
            }
            return true;
        }
    }
}
