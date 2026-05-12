using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoPartNo
{
    /// <summary>
    /// 零件号生成规则
    /// </summary>
    public class NumberingRule
    {
        /// <summary>前缀 (如 "MD", "PART")</summary>
        public string Prefix { get; set; } = "MD";

        /// <summary>分隔符 (如 "-", "_", "")</summary>
        public string Separator { get; set; } = "-";

        /// <summary>序号起始值</summary>
        public int StartNumber { get; set; } = 1;

        /// <summary>序号步长</summary>
        public int Step { get; set; } = 1;

        /// <summary>序号位数 (补零)</summary>
        public int DigitCount { get; set; } = 4;

        /// <summary>后缀 (如 "A", "ASM")</summary>
        public string Suffix { get; set; } = "";

        /// <summary>是否跳过已分配零件号的组件</summary>
        public bool SkipExisting { get; set; } = true;

        /// <summary>零件号正则验证 (为空则不验证)</summary>
        public string ValidationPattern { get; set; }

        /// <summary>根据规则格式化零件号</summary>
        public string FormatNumber(int number)
        {
            var numStr = number.ToString().PadLeft(DigitCount, '0');
            var result = $"{Prefix}{Separator}{numStr}";
            if (!string.IsNullOrEmpty(Suffix))
                result += $"{Separator}{Suffix}";
            return result;
        }
    }

    /// <summary>
    /// 零件号生成器 - 为SolidWorks组件自动分配零件号
    /// </summary>
    public class PartNumberGenerator
    {
        private int _currentNumber;
        private HashSet<string> _usedNumbers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly List<string> _generatedLog = new List<string>();

        public NumberingRule Rule { get; set; }

        public PartNumberGenerator()
        {
            Rule = new NumberingRule();
            _currentNumber = Rule.StartNumber;
        }

        public PartNumberGenerator(NumberingRule rule)
        {
            Rule = rule ?? throw new ArgumentNullException(nameof(rule));
            _currentNumber = rule.StartNumber;
        }

        /// <summary>
        /// 生成下一个零件号
        /// </summary>
        public string GenerateNext()
        {
            string partNo;
            do
            {
                partNo = Rule.FormatNumber(_currentNumber);
                _currentNumber += Rule.Step;
            }
            while (_usedNumbers.Contains(partNo));

            _usedNumbers.Add(partNo);
            _generatedLog.Add($"{DateTime.Now:HH:mm:ss} - 生成: {partNo}");
            return partNo;
        }

        /// <summary>
        /// 批量生成零件号
        /// </summary>
        public List<string> GenerateBatch(int count)
        {
            var result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                result.Add(GenerateNext());
            }
            return result;
        }

        /// <summary>
        /// 生成零件号列表 (带预览)
        /// </summary>
        public List<string> PreviewBatch(int count, int startFrom = -1)
        {
            var savedNum = _currentNumber;
            var savedUsed = new HashSet<string>(_usedNumbers);
            var result = new List<string>();

            if (startFrom >= 0)
                _currentNumber = startFrom;

            for (int i = 0; i < count; i++)
            {
                result.Add(GenerateNext());
            }

            _currentNumber = savedNum;
            _usedNumbers = savedUsed;
            return result;
        }

        /// <summary>
        /// 验证零件号格式
        /// </summary>
        public bool ValidatePartNo(string partNo)
        {
            if (string.IsNullOrWhiteSpace(partNo)) return false;
            if (string.IsNullOrEmpty(Rule.ValidationPattern)) return true;
            return Regex.IsMatch(partNo, Rule.ValidationPattern);
        }

        /// <summary>
        /// 注册已存在的零件号 (避免重复)
        /// </summary>
        public void RegisterExisting(string partNo)
        {
            if (!string.IsNullOrWhiteSpace(partNo))
            {
                _usedNumbers.Add(partNo);
            }
        }

        /// <summary>
        /// 批量注册已存在的零件号
        /// </summary>
        public void RegisterExistingRange(IEnumerable<string> partNumbers)
        {
            foreach (var pn in partNumbers)
            {
                RegisterExisting(pn);
            }
        }

        /// <summary>
        /// 从文件加载已使用的零件号
        /// </summary>
        public void LoadUsedNumbersFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                    _usedNumbers.Add(trimmed);
            }
        }

        /// <summary>
        /// 保存已使用的零件号到文件
        /// </summary>
        public void SaveUsedNumbersToFile(string filePath)
        {
            File.WriteAllLines(filePath, _usedNumbers.OrderBy(n => n).ToArray());
        }

        /// <summary>
        /// 获取生成日志
        /// </summary>
        public IReadOnlyList<string> GetLog()
        {
            return _generatedLog.AsReadOnly();
        }

        /// <summary>
        /// 获取已使用零件号集合
        /// </summary>
        public IReadOnlyCollection<string> GetUsedNumbers()
        {
            return _usedNumbers;
        }

        /// <summary>
        /// 重置生成器
        /// </summary>
        public void Reset()
        {
            _currentNumber = Rule.StartNumber;
            _generatedLog.Clear();
        }

        /// <summary>
        /// 完全重置 (包括已使用记录)
        /// </summary>
        public void FullReset()
        {
            Reset();
            _usedNumbers.Clear();
        }

        /// <summary>
        /// 预设常用编号规则
        /// </summary>
        public static class PresetRules
        {
            public static NumberingRule Standard() => new NumberingRule
            {
                Prefix = "MD",
                Separator = "-",
                StartNumber = 1,
                Step = 1,
                DigitCount = 4
            };

            public static NumberingRule Assembly() => new NumberingRule
            {
                Prefix = "ASM",
                Separator = "-",
                StartNumber = 1,
                Step = 1,
                DigitCount = 4
            };

            public static NumberingRule Part() => new NumberingRule
            {
                Prefix = "PRT",
                Separator = "-",
                StartNumber = 1,
                Step = 1,
                DigitCount = 4
            };

            public static NumberingRule Custom(string prefix, int digits = 4, string separator = "-")
            {
                return new NumberingRule
                {
                    Prefix = prefix,
                    Separator = separator,
                    StartNumber = 1,
                    Step = 1,
                    DigitCount = digits
                };
            }
        }
    }
}
