namespace ConnDesign
{
    static class ConnDesignData
    {
        // 平键截面尺寸: 轴径min, 轴径max, b, h (mm)
        public static readonly double[][] FlatKeySections = {
            new double[] { 6,   8,   2,  2 },
            new double[] { 8,   10,  3,  3 },
            new double[] { 10,  12,  4,  4 },
            new double[] { 12,  17,  5,  5 },
            new double[] { 17,  22,  6,  6 },
            new double[] { 22,  30,  8,  7 },
            new double[] { 30,  38,  10, 8 },
            new double[] { 38,  44,  12, 8 },
            new double[] { 44,  50,  14, 9 },
            new double[] { 50,  58,  16, 10 },
            new double[] { 58,  65,  18, 11 },
            new double[] { 65,  75,  20, 12 },
            new double[] { 75,  85,  22, 14 },
            new double[] { 85,  95,  25, 14 },
            new double[] { 95,  110, 28, 16 },
            new double[] { 110, 130, 32, 18 },
            new double[] { 130, 150, 36, 20 },
            new double[] { 150, 170, 40, 22 },
            new double[] { 170, 200, 45, 25 },
            new double[] { 200, 230, 50, 28 },
            new double[] { 230, 260, 56, 32 },
            new double[] { 260, 290, 63, 32 },
            new double[] { 290, 330, 70, 36 },
            new double[] { 330, 380, 80, 40 },
            new double[] { 380, 440, 90, 45 },
            new double[] { 440, 500, 100, 50 }
        };

        // 螺栓公称直径系列 (mm)
        public static readonly double[] BoltDiameters = {
            1, 1.2, 1.6, 2, 2.5, 3, 4, 5, 6, 8, 10, 12, 16, 20, 24, 30, 36, 42, 48, 56, 64
        };

        // 螺栓小径 d1 (mm)，与 BoltDiameters 一一对应
        public static readonly double[] BoltMinorDia = {
            0.729, 0.929, 1.221, 1.567, 2.019, 2.459, 3.242, 4.134, 4.918,
            6.647, 8.376, 10.106, 13.835, 17.294, 20.752, 26.211, 31.670,
            37.129, 42.588, 50.046, 57.505
        };

        // 螺栓材料: 名称, 抗拉强度(MPa), 屈服强度(MPa), 疲劳极限(MPa)
        public static readonly object[][] BoltMaterials = {
            new object[] { "Q215-A",  380, 210, 135 },
            new object[] { "Q235-A",  380, 220, 135 },
            new object[] { "35",      440, 240, 140 },
            new object[] { "45",      540, 320, 195 },
            new object[] { "15MnVB",  610, 360, 220 },
            new object[] { "40Cr",   1100, 800, 320 },
            new object[] { "30CrMnSi", 900, 770, 290 },
            new object[] { "40CrNiMo",1140, 900, 340 }
        };

        // 许用挤压应力 (材料, 载荷类型) -> [sigma_p] MPa
        // 材料: 0=钢, 1=铸铁; 载荷: 0=静载, 1=轻微冲击, 2=冲击
        public static readonly int[,] AllowBearingStress = {
            { 135, 100, 60 },  // 钢
            { 75,  55,  35 }   // 铸铁
        };

        // 许用压强 (动连接): (材料, 载荷类型) -> [p] MPa
        public static readonly int[,] AllowPressure = {
            { 50, 40, 30 },  // 钢
            { 28, 20, 15 }   // 铸铁
        };

        /// <summary>
        /// 根据轴径查找平键截面尺寸
        /// </summary>
        public static void FindKeySection(double shaftDia, out double b, out double h)
        {
            b = 0; h = 0;
            for (int i = 0; i < FlatKeySections.Length; i++)
            {
                if (shaftDia > FlatKeySections[i][0] && shaftDia <= FlatKeySections[i][1])
                {
                    b = FlatKeySections[i][2];
                    h = FlatKeySections[i][3];
                    return;
                }
            }
            // 默认取最大的
            b = FlatKeySections[FlatKeySections.Length - 1][2];
            h = FlatKeySections[FlatKeySections.Length - 1][3];
        }

        /// <summary>
        /// 查找螺栓小径
        /// </summary>
        public static double FindBoltMinorDia(double nominalDia)
        {
            for (int i = 0; i < BoltDiameters.Length; i++)
            {
                if (System.Math.Abs(BoltDiameters[i] - nominalDia) < 0.01)
                    return BoltMinorDia[i];
            }
            return nominalDia * 0.85; // 近似
        }
    }
}
