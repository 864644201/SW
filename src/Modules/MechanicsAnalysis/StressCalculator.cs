using System;

namespace MDSolids
{
    /// <summary>
    /// 应力计算工具类 - 纯计算逻辑，无UI依赖
    /// </summary>
    public static class StressCalculator
    {
        /// <summary>
        /// 计算弯曲应力: sigma = M * c / I
        /// M: 弯矩 (N·mm)
        /// I: 截面惯性矩 (mm^4)
        /// c: 距中性轴距离 (mm)
        /// 返回: 弯曲应力 (MPa)
        /// </summary>
        public static double CalcBendingStress(double M, double I, double c)
        {
            if (I <= 0)
                throw new ArgumentException("截面惯性矩 I 必须大于零");
            return M * c / I;
        }

        /// <summary>
        /// 计算扭转切应力: tau = T * c / J
        /// T: 扭矩 (N·mm)
        /// J: 极惯性矩 (mm^4)
        /// c: 半径 (mm)
        /// 返回: 切应力 (MPa)
        /// </summary>
        public static double CalcTorsionStress(double T, double J, double r)
        {
            if (J <= 0)
                throw new ArgumentException("极惯性矩 J 必须大于零");
            return T * r / J;
        }

        /// <summary>
        /// Von Mises 等效应力: sigma_vm = sqrt(sigma^2 + 3*tau^2)
        /// sigma: 正应力 (MPa)
        /// tau: 切应力 (MPa)
        /// 返回: 等效应力 (MPa)
        /// </summary>
        public static double CalcVonMises(double sigma, double tau)
        {
            return Math.Sqrt(sigma * sigma + 3.0 * tau * tau);
        }

        /// <summary>
        /// 安全系数: n = sigma_yield / sigma_vm
        /// sigma_yield: 屈服强度 (MPa)
        /// sigma_vm: Von Mises 等效应力 (MPa)
        /// 返回: 安全系数
        /// </summary>
        public static double CalcSafetyFactor(double sigma_yield, double sigma_vm)
        {
            if (sigma_vm <= 0)
                throw new ArgumentException("等效应力必须大于零");
            return sigma_yield / sigma_vm;
        }

        /// <summary>
        /// 悬臂梁挠度: delta = F * L^3 / (3 * E * I)
        /// F: 集中力 (N)
        /// L: 梁长 (mm)
        /// E: 弹性模量 (MPa)
        /// I: 截面惯性矩 (mm^4)
        /// 返回: 最大挠度 (mm)
        /// </summary>
        public static double CalcDeflectionCantilever(double F, double L, double E, double I)
        {
            if (E <= 0 || I <= 0)
                throw new ArgumentException("E 和 I 必须大于零");
            return F * L * L * L / (3.0 * E * I);
        }

        /// <summary>
        /// 简支梁均布载荷挠度: delta = 5 * w * L^4 / (384 * E * I)
        /// w: 均布载荷 (N/mm)
        /// L: 梁长 (mm)
        /// E: 弹性模量 (MPa)
        /// I: 截面惯性矩 (mm^4)
        /// 返回: 最大挠度 (mm)
        /// </summary>
        public static double CalcDeflectionSimplySupported(double w, double L, double E, double I)
        {
            if (E <= 0 || I <= 0)
                throw new ArgumentException("E 和 I 必须大于零");
            return 5.0 * w * Math.Pow(L, 4) / (384.0 * E * I);
        }

        /// <summary>
        /// 矩形截面惯性矩: I = b * h^3 / 12
        /// b: 宽度 (mm)
        /// h: 高度 (mm)
        /// 返回: 惯性矩 (mm^4)
        /// </summary>
        public static double CalcMomentOfInertia_Rect(double b, double h)
        {
            return b * Math.Pow(h, 3) / 12.0;
        }

        /// <summary>
        /// 圆形截面惯性矩: I = pi * d^4 / 64
        /// d: 直径 (mm)
        /// 返回: 惯性矩 (mm^4)
        /// </summary>
        public static double CalcMomentOfInertia_Circle(double d)
        {
            return Math.PI * Math.Pow(d, 4) / 64.0;
        }

        /// <summary>
        /// 圆形截面极惯性矩: J = pi * d^4 / 32
        /// d: 直径 (mm)
        /// 返回: 极惯性矩 (mm^4)
        /// </summary>
        public static double CalcPolarMoment_Circle(double d)
        {
            return Math.PI * Math.Pow(d, 4) / 32.0;
        }

        /// <summary>
        /// 截面模量: W = I / c
        /// I: 惯性矩 (mm^4)
        /// c: 距中性轴最远距离 (mm)
        /// 返回: 截面模量 (mm^3)
        /// </summary>
        public static double CalcSectionModulus(double I, double c)
        {
            if (c <= 0)
                throw new ArgumentException("距离 c 必须大于零");
            return I / c;
        }

        /// <summary>
        /// 圆形截面模量: W = pi * d^3 / 32
        /// d: 直径 (mm)
        /// 返回: 截面模量 (mm^3)
        /// </summary>
        public static double CalcSectionModulus_Circle(double d)
        {
            return Math.PI * Math.Pow(d, 3) / 32.0;
        }

        /// <summary>
        /// 矩形截面模量: W = b * h^2 / 6
        /// b: 宽度 (mm)
        /// h: 高度 (mm)
        /// 返回: 截面模量 (mm^3)
        /// </summary>
        public static double CalcSectionModulus_Rect(double b, double h)
        {
            return b * h * h / 6.0;
        }

        /// <summary>
        /// 空心圆截面惯性矩: I = pi * (D^4 - d^4) / 64
        /// D: 外径 (mm)
        /// d: 内径 (mm)
        /// 返回: 惯性矩 (mm^4)
        /// </summary>
        public static double CalcMomentOfInertia_HollowCircle(double D, double d)
        {
            return Math.PI * (Math.Pow(D, 4) - Math.Pow(d, 4)) / 64.0;
        }

        /// <summary>
        /// 空心圆极惯性矩: J = pi * (D^4 - d^4) / 32
        /// D: 外径 (mm)
        /// d: 内径 (mm)
        /// 返回: 极惯性矩 (mm^4)
        /// </summary>
        public static double CalcPolarMoment_HollowCircle(double D, double d)
        {
            return Math.PI * (Math.Pow(D, 4) - Math.Pow(d, 4)) / 32.0;
        }

        /// <summary>
        /// 欧拉压杆临界力: P_cr = pi^2 * E * I / (K * L)^2
        /// E: 弹性模量 (MPa)
        /// I: 最小惯性矩 (mm^4)
        /// K: 长度系数
        /// L: 杆长 (mm)
        /// 返回: 临界力 (N)
        /// </summary>
        public static double CalcEulerBuckling(double E, double I, double K, double L)
        {
            double KL = K * L;
            if (KL <= 0)
                throw new ArgumentException("有效长度必须大于零");
            return Math.PI * Math.PI * E * I / (KL * KL);
        }
    }
}
