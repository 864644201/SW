using System;

namespace TrunnionCalculation
{
    /// <summary>
    /// 材料系数数据
    /// </summary>
    public class MaterialCoefficient
    {
        /// <summary>材料名称</summary>
        public string Name { get; set; }
        /// <summary>系数 A</summary>
        public double A { get; set; }
        /// <summary>许用剪切应力 tau (MPa)</summary>
        public double Tau { get; set; }
        /// <summary>许用弯曲应力 sigma (MPa)</summary>
        public double Sigma { get; set; }
        /// <summary>适用最低转速 (rpm)</summary>
        public int MinRpm { get; set; }
        /// <summary>是否合金钢</summary>
        public bool IsAlloy { get; set; }
    }

    /// <summary>
    /// 键槽类型
    /// </summary>
    public enum KeywayType
    {
        /// <summary>新轴（无键槽）</summary>
        NewShaft = 0,
        /// <summary>单键槽</summary>
        SingleKeyway = 1,
        /// <summary>双键槽</summary>
        DoubleKeyway = 2
    }

    /// <summary>
    /// 载荷类型
    /// </summary>
    public enum LoadType
    {
        /// <summary>平稳载荷</summary>
        Steady = 0,
        /// <summary>轻微冲击</summary>
        LightShock = 1,
        /// <summary>中等冲击</summary>
        MediumShock = 2,
        /// <summary>严重冲击</summary>
        HeavyShock = 3
    }

    /// <summary>
    /// 轴径计算结果
    /// </summary>
    public class TrunnionResult
    {
        /// <summary>输入功率 P (kW)</summary>
        public double Power { get; set; }
        /// <summary>输入转速 n (rpm)</summary>
        public double Rpm { get; set; }
        /// <summary>材料名称</summary>
        public string MaterialName { get; set; }
        /// <summary>转速等级</summary>
        public int RpmGrade { get; set; }
        /// <summary>键槽类型</summary>
        public KeywayType Keyway { get; set; }
        /// <summary>载荷类型</summary>
        public LoadType Load { get; set; }
        /// <summary>系数 A</summary>
        public double A { get; set; }
        /// <summary>许用剪切应力 tau (MPa)</summary>
        public double Tau { get; set; }
        /// <summary>许用弯曲应力 sigma (MPa)</summary>
        public double Sigma { get; set; }
        /// <summary>载荷系数 K</summary>
        public double LoadFactor { get; set; }
        /// <summary>键槽增大系数</summary>
        public double KeywayFactor { get; set; }
        /// <summary>基础计算轴径 d0 (mm)</summary>
        public double BaseDiameter { get; set; }
        /// <summary>考虑键槽后轴径 d (mm)</summary>
        public double FinalDiameter { get; set; }
        /// <summary>圆整后标准轴径 (mm)</summary>
        public double StandardDiameter { get; set; }
        /// <summary>扭矩 T (N.m)</summary>
        public double Torque { get; set; }
    }

    /// <summary>
    /// 轴径计算器
    /// </summary>
    public static class TrunnionCalculator
    {
        /// <summary>
        /// 内置材料系数表
        /// </summary>
        private static readonly MaterialCoefficient[] Coefficients =
        {
            new MaterialCoefficient { Name = "400+碳素钢", A = 130, Tau = 70, Sigma = 40, MinRpm = 400, IsAlloy = false },
            new MaterialCoefficient { Name = "400+合金钢", A = 100, Tau = 50, Sigma = 30, MinRpm = 400, IsAlloy = true },
            new MaterialCoefficient { Name = "500+碳素钢", A = 170, Tau = 75, Sigma = 45, MinRpm = 500, IsAlloy = false },
            new MaterialCoefficient { Name = "500+合金钢", A = 120, Tau = 70, Sigma = 40, MinRpm = 500, IsAlloy = true },
            new MaterialCoefficient { Name = "600碳素钢",  A = 200, Tau = 95, Sigma = 55, MinRpm = 600, IsAlloy = false },
            new MaterialCoefficient { Name = "600合金钢",  A = 200, Tau = 95, Sigma = 55, MinRpm = 600, IsAlloy = true },
            new MaterialCoefficient { Name = "700碳素钢",  A = 230, Tau = 110, Sigma = 65, MinRpm = 700, IsAlloy = false },
            new MaterialCoefficient { Name = "700合金钢",  A = 230, Tau = 110, Sigma = 65, MinRpm = 700, IsAlloy = true },
            new MaterialCoefficient { Name = "800碳素钢",  A = 270, Tau = 130, Sigma = 75, MinRpm = 800, IsAlloy = false },
            new MaterialCoefficient { Name = "800合金钢",  A = 270, Tau = 130, Sigma = 75, MinRpm = 800, IsAlloy = true },
            new MaterialCoefficient { Name = "1000碳素钢", A = 330, Tau = 150, Sigma = 90, MinRpm = 1000, IsAlloy = false },
            new MaterialCoefficient { Name = "1000合金钢", A = 330, Tau = 150, Sigma = 90, MinRpm = 1000, IsAlloy = true },
        };

        /// <summary>
        /// 获取所有可用材料系数
        /// </summary>
        public static MaterialCoefficient[] GetCoefficients()
        {
            return Coefficients;
        }

        /// <summary>
        /// 根据转速等级和材料类型查找系数
        /// </summary>
        /// <param name="rpmGrade">转速等级 (400/500/600/700/800/1000)</param>
        /// <param name="isAlloy">是否合金钢</param>
        /// <returns>匹配的材料系数，未找到返回 null</returns>
        public static MaterialCoefficient FindCoefficient(int rpmGrade, bool isAlloy)
        {
            foreach (var c in Coefficients)
            {
                if (c.MinRpm == rpmGrade && c.IsAlloy == isAlloy)
                    return c;
            }
            return null;
        }

        /// <summary>
        /// 获取载荷系数
        /// </summary>
        public static double GetLoadFactor(LoadType load)
        {
            switch (load)
            {
                case LoadType.Steady: return 1.0;
                case LoadType.LightShock: return 1.2;
                case LoadType.MediumShock: return 1.5;
                case LoadType.HeavyShock: return 2.0;
                default: return 1.0;
            }
        }

        /// <summary>
        /// 获取键槽增大系数
        /// </summary>
        public static double GetKeywayFactor(KeywayType keyway)
        {
            switch (keyway)
            {
                case KeywayType.NewShaft: return 1.0;
                case KeywayType.SingleKeyway: return 1.05;
                case KeywayType.DoubleKeyway: return 1.10;
                default: return 1.0;
            }
        }

        /// <summary>
        /// 执行轴径计算
        /// 公式: d >= A * (P/n)^(1/3) * K_load * K_keyway
        /// </summary>
        /// <param name="power">功率 P (kW)</param>
        /// <param name="rpm">转速 n (rpm)</param>
        /// <param name="rpmGrade">转速等级</param>
        /// <param name="isAlloy">是否合金钢</param>
        /// <param name="keyway">键槽类型</param>
        /// <param name="load">载荷类型</param>
        /// <returns>计算结果</returns>
        public static TrunnionResult Calculate(double power, double rpm, int rpmGrade,
            bool isAlloy, KeywayType keyway, LoadType load)
        {
            if (power <= 0) throw new ArgumentException("功率必须大于 0");
            if (rpm <= 0) throw new ArgumentException("转速必须大于 0");

            var coeff = FindCoefficient(rpmGrade, isAlloy);
            if (coeff == null)
                throw new ArgumentException($"未找到转速等级 {rpmGrade} {(isAlloy ? "合金钢" : "碳素钢")} 的系数");

            double loadFactor = GetLoadFactor(load);
            double keywayFactor = GetKeywayFactor(keyway);

            // 基础轴径: d0 = A * (P/n)^(1/3)
            double baseD = coeff.A * Math.Pow(power / rpm, 1.0 / 3.0);

            // 考虑载荷和键槽: d = d0 * K_load * K_keyway
            double finalD = baseD * loadFactor * keywayFactor;

            // 扭矩: T = 9550 * P / n (N.m)
            double torque = 9550.0 * power / rpm;

            // 圆整到标准值 (取最接近的较大整数或半整数)
            double stdD = RoundToStandard(finalD);

            return new TrunnionResult
            {
                Power = power,
                Rpm = rpm,
                MaterialName = coeff.Name,
                RpmGrade = rpmGrade,
                Keyway = keyway,
                Load = load,
                A = coeff.A,
                Tau = coeff.Tau,
                Sigma = coeff.Sigma,
                LoadFactor = loadFactor,
                KeywayFactor = keywayFactor,
                BaseDiameter = baseD,
                FinalDiameter = finalD,
                StandardDiameter = stdD,
                Torque = torque
            };
        }

        /// <summary>
        /// 圆整到标准轴径 (向上取 0.5 的倍数)
        /// </summary>
        private static double RoundToStandard(double d)
        {
            return Math.Ceiling(d * 2.0) / 2.0;
        }

        /// <summary>
        /// 获取常用标准轴径列表
        /// </summary>
        public static double[] GetStandardDiameters()
        {
            return new double[]
            {
                6, 7, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 25, 28, 30, 32, 35,
                38, 40, 42, 45, 48, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100,
                110, 120, 130, 140, 150, 160, 170, 180, 190, 200
            };
        }

        /// <summary>
        /// 查找最接近的标准轴径 (向上取)
        /// </summary>
        public static double FindNearestStandard(double d)
        {
            var standards = GetStandardDiameters();
            foreach (var s in standards)
            {
                if (s >= d) return s;
            }
            return standards[standards.Length - 1];
        }
    }
}
