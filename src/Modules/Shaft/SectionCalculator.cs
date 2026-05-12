using System;

namespace ShaftDesign
{
    /// <summary>
    /// 轴截面几何性质计算
    /// </summary>
    public class SectionCalculator
    {
        private const double PI = Math.PI;

        /// <summary>
        /// 实心圆截面惯性矩 I = pi*d^4/64
        /// </summary>
        /// <param name="d">直径 (mm)</param>
        /// <returns>惯性矩 (mm^4)</returns>
        public static double SolidInertia(double d)
        {
            return PI * Math.Pow(d, 4) / 64.0;
        }

        /// <summary>
        /// 实心圆截面抗弯截面系数 W = pi*d^3/32
        /// </summary>
        /// <param name="d">直径 (mm)</param>
        /// <returns>抗弯截面系数 (mm^3)</returns>
        public static double SolidSectionModulus(double d)
        {
            return PI * Math.Pow(d, 3) / 32.0;
        }

        /// <summary>
        /// 实心圆截面极惯性矩 Ip = pi*d^4/32
        /// </summary>
        /// <param name="d">直径 (mm)</param>
        /// <returns>极惯性矩 (mm^4)</returns>
        public static double SolidPolarInertia(double d)
        {
            return PI * Math.Pow(d, 4) / 32.0;
        }

        /// <summary>
        /// 实心圆截面抗扭截面系数 Wt = pi*d^3/16
        /// </summary>
        /// <param name="d">直径 (mm)</param>
        /// <returns>抗扭截面系数 (mm^3)</returns>
        public static double SolidTorsionModulus(double d)
        {
            return PI * Math.Pow(d, 3) / 16.0;
        }

        /// <summary>
        /// 空心圆截面惯性矩 I = pi*(D^4-d^4)/64
        /// </summary>
        /// <param name="D">外径 (mm)</param>
        /// <param name="d">内径 (mm)</param>
        /// <returns>惯性矩 (mm^4)</returns>
        public static double HollowInertia(double D, double d)
        {
            return PI * (Math.Pow(D, 4) - Math.Pow(d, 4)) / 64.0;
        }

        /// <summary>
        /// 空心圆截面抗弯截面系数 W = pi*(D^4-d^4)/(32*D)
        /// </summary>
        /// <param name="D">外径 (mm)</param>
        /// <param name="d">内径 (mm)</param>
        /// <returns>抗弯截面系数 (mm^3)</returns>
        public static double HollowSectionModulus(double D, double d)
        {
            return PI * (Math.Pow(D, 4) - Math.Pow(d, 4)) / (32.0 * D);
        }

        /// <summary>
        /// 空心圆截面极惯性矩 Ip = pi*(D^4-d^4)/32
        /// </summary>
        /// <param name="D">外径 (mm)</param>
        /// <param name="d">内径 (mm)</param>
        /// <returns>极惯性矩 (mm^4)</returns>
        public static double HollowPolarInertia(double D, double d)
        {
            return PI * (Math.Pow(D, 4) - Math.Pow(d, 4)) / 32.0;
        }

        /// <summary>
        /// 空心圆截面抗扭截面系数 Wt = pi*(D^4-d^4)/(16*D)
        /// </summary>
        /// <param name="D">外径 (mm)</param>
        /// <param name="d">内径 (mm)</param>
        /// <returns>抗扭截面系数 (mm^3)</returns>
        public static double HollowTorsionModulus(double D, double d)
        {
            return PI * (Math.Pow(D, 4) - Math.Pow(d, 4)) / (16.0 * D);
        }

        /// <summary>
        /// 键槽对抗弯截面系数的折减系数
        /// 一个键槽: 约折减 10%~15%, 取 0.88
        /// 两个键槽(互成90度): 约折减 20%~25%, 取 0.78
        /// </summary>
        /// <param name="keywayCount">键槽数量 (0, 1, 2)</param>
        /// <returns>折减系数</returns>
        public static double KeywayReductionFactor(int keywayCount)
        {
            switch (keywayCount)
            {
                case 0: return 1.0;
                case 1: return 0.88;
                case 2: return 0.78;
                default: return 1.0;
            }
        }

        /// <summary>
        /// 带键槽的实心圆抗弯截面系数
        /// </summary>
        /// <param name="d">直径 (mm)</param>
        /// <param name="keywayCount">键槽数量</param>
        /// <returns>折减后的抗弯截面系数 (mm^3)</returns>
        public static double SolidSectionModulusWithKeyway(double d, int keywayCount)
        {
            return SolidSectionModulus(d) * KeywayReductionFactor(keywayCount);
        }

        /// <summary>
        /// 带键槽的实心圆抗扭截面系数
        /// </summary>
        /// <param name="d">直径 (mm)</param>
        /// <param name="keywayCount">键槽数量</param>
        /// <returns>折减后的抗扭截面系数 (mm^3)</returns>
        public static double SolidTorsionModulusWithKeyway(double d, int keywayCount)
        {
            return SolidTorsionModulus(d) * KeywayReductionFactor(keywayCount);
        }

        /// <summary>
        /// 截面积 A = pi*d^2/4
        /// </summary>
        /// <param name="d">直径 (mm)</param>
        /// <returns>截面积 (mm^2)</returns>
        public static double SolidArea(double d)
        {
            return PI * d * d / 4.0;
        }

        /// <summary>
        /// 空心截面积 A = pi*(D^2-d^2)/4
        /// </summary>
        /// <param name="D">外径 (mm)</param>
        /// <param name="d">内径 (mm)</param>
        /// <returns>截面积 (mm^2)</returns>
        public static double HollowArea(double D, double d)
        {
            return PI * (D * D - d * d) / 4.0;
        }
    }
}
