using System;
using System.Collections.Generic;
using System.IO;

namespace BearingDesign
{
    public class BearingRecord
    {
        public string Code { get; set; }
        public double d { get; set; }    // 内径 mm
        public double D { get; set; }    // 外径 mm
        public double B { get; set; }    // 宽度/高度 mm
        public double Cr { get; set; }   // 额定动载荷 kN
        public double Cor { get; set; }  // 额定静载荷 kN
        public int NlimZ { get; set; }   // 脂润滑极限转速 r/min
        public int NlimY { get; set; }   // 油润滑极限转速 r/min
        // 圆锥滚子轴承特有
        public double e { get; set; }
        public double Y { get; set; }
        public double Yo { get; set; }
    }

    public class XYCoeff
    {
        public double FaCorRatio { get; set; }  // Fa/Cor
        public double e { get; set; }
        public double X0 { get; set; }  // 单列, Fa/Fr<=e
        public double Y0 { get; set; }
        public double X1 { get; set; }  // 单列, Fa/Fr>e
        public double Y1 { get; set; }
    }

    public static class BearingRepository
    {
        private static string DataDir
        {
            get
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string dataDir = Path.Combine(baseDir, "data");
                if (Directory.Exists(dataDir)) return dataDir;
                // 开发时回退到源码目录
                dataDir = Path.Combine(baseDir, @"..\..\..\data");
                if (Directory.Exists(dataDir)) return Path.GetFullPath(dataDir);
                return baseDir;
            }
        }

        public static List<BearingRecord> LoadBearingTable(string filename)
        {
            var list = new List<BearingRecord>();
            string path = Path.Combine(DataDir, filename);
            if (!File.Exists(path)) return list;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return list;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                if (parts.Length < 9) continue;
                var rec = new BearingRecord();
                try
                {
                    rec.Code = parts[1];
                    rec.d = ParseDouble(parts[2]);
                    rec.D = ParseDouble(parts[3]);
                    rec.B = ParseDouble(parts[4]);
                    // 不同表的列顺序不同
                    if (filename.Contains("tapered"))
                    {
                        // BCode|d|DD|T|e|Y|Yo|Cr|Cor|nlimz|nlimy
                        rec.e = ParseDouble(parts[5]);
                        rec.Y = ParseDouble(parts[6]);
                        rec.Yo = ParseDouble(parts[7]);
                        rec.Cr = ParseDouble(parts[8]);
                        rec.Cor = ParseDouble(parts[9]);
                        rec.NlimZ = (int)ParseDouble(parts[10]);
                        rec.NlimY = (int)ParseDouble(parts[11]);
                    }
                    else if (filename.Contains("thrust_ball"))
                    {
                        // BCode|d|DD|T|Ca|Coa|nlimz|nlimy
                        rec.Cr = ParseDouble(parts[5]);  // Ca
                        rec.Cor = ParseDouble(parts[6]); // Coa
                        rec.NlimZ = (int)ParseDouble(parts[7]);
                        rec.NlimY = (int)ParseDouble(parts[8]);
                    }
                    else if (filename.Contains("thrust_roller"))
                    {
                        // BCode|d|DD|T1|H|Ca|Coa|nlimy
                        rec.Cr = ParseDouble(parts[6]);  // Ca
                        rec.Cor = ParseDouble(parts[7]); // Coa
                        rec.NlimY = (int)ParseDouble(parts[8]);
                    }
                    else if (filename.Contains("self_aligning"))
                    {
                        // ID|BCode|圆锥孔|d|DD|B|Cr|Cor|nlimz|nlimy
                        rec.Code = parts[1]; // 已在上面赋值
                        rec.d = ParseDouble(parts[3]);
                        rec.D = ParseDouble(parts[4]);
                        rec.B = ParseDouble(parts[5]);
                        rec.Cr = ParseDouble(parts[6]);
                        rec.Cor = ParseDouble(parts[7]);
                        rec.NlimZ = (int)ParseDouble(parts[8]);
                        rec.NlimY = (int)ParseDouble(parts[9]);
                    }
                    else
                    {
                        // 深沟球/角接触球: BCode|d|DD|B|Cr|Cor|nlimz|nlimy
                        rec.Cr = ParseDouble(parts[5]);
                        rec.Cor = ParseDouble(parts[6]);
                        rec.NlimZ = (int)ParseDouble(parts[7]);
                        rec.NlimY = (int)ParseDouble(parts[8]);
                    }
                    list.Add(rec);
                }
                catch { }
            }
            return list;
        }

        public static List<XYCoeff> LoadXYTable(string filename)
        {
            var list = new List<XYCoeff>();
            string path = Path.Combine(DataDir, filename);
            if (!File.Exists(path)) return list;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return list;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                if (parts.Length < 7) continue;
                try
                {
                    var c = new XYCoeff();
                    c.FaCorRatio = ParseDouble(parts[1]);
                    // 跳过 Fa/zDw^2 (parts[2])
                    c.X0 = ParseDouble(parts[3]);
                    c.Y0 = ParseDouble(parts[4]);
                    c.X1 = ParseDouble(parts[5]);
                    c.Y1 = ParseDouble(parts[6]);
                    c.e = ParseDouble(parts[parts.Length - 1]); // e 在最后
                    list.Add(c);
                }
                catch { }
            }
            return list;
        }

        private static double ParseDouble(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;
            s = s.Trim();
            // 处理特殊值如 "11.1" 可能有逗号
            double val;
            if (double.TryParse(s, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out val))
                return val;
            return 0;
        }

        // 载荷系数 fp 查表
        public static double GetLoadFactor(string loadType)
        {
            switch (loadType)
            {
                case "平稳": return 1.0;
                case "轻微冲击": return 1.2;
                case "中等冲击": return 1.5;
                case "强烈冲击": return 1.8;
                case "剧烈冲击": return 2.0;
                default: return 1.0;
            }
        }

        // 温度系数 ft 查表
        public static double GetTempFactor(double temp)
        {
            if (temp <= 120) return 1.0;
            if (temp <= 150) return 0.90;
            if (temp <= 175) return 0.85;
            if (temp <= 200) return 0.75;
            if (temp <= 225) return 0.65;
            if (temp <= 250) return 0.55;
            return 0.50;
        }

        // 可靠性寿命修正系数 a1
        public static double GetReliabilityFactor(double reliability)
        {
            if (reliability >= 99) return 0.21;
            if (reliability >= 98) return 0.33;
            if (reliability >= 97) return 0.44;
            if (reliability >= 96) return 0.53;
            if (reliability >= 95) return 0.62;
            return 1.0; // 90%
        }

        // 插值查找 X/Y 系数
        public static void FindXY(List<XYCoeff> table, double faCorRatio,
            out double X, out double Y, out double e)
        {
            if (table == null || table.Count == 0)
            {
                X = 1; Y = 0; e = 0.4;
                return;
            }

            // 找到 faCorRatio 对应的区间
            for (int i = 0; i < table.Count; i++)
            {
                if (faCorRatio <= table[i].FaCorRatio || i == table.Count - 1)
                {
                    e = table[i].e;
                    X = table[i].X1;
                    Y = table[i].Y1;
                    return;
                }
            }
            // 超出范围取最后一条
            var last = table[table.Count - 1];
            e = last.e;
            X = last.X1;
            Y = last.Y1;
        }
    }
}
