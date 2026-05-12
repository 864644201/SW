using System;

namespace Leadscrew
{
    /// <summary>
    /// 丝杠类型枚举
    /// </summary>
    public enum ScrewType
    {
        /// <summary>梯形螺纹 (Tr)</summary>
        Trapezoidal,
        /// <summary>锯齿形螺纹 (S)</summary>
        Buttress,
        /// <summary>滚珠丝杠</summary>
        BallScrew
    }

    /// <summary>
    /// 支撑方式枚举
    /// </summary>
    public enum SupportType
    {
        /// <summary>一端固定一端自由 (悬臂)</summary>
        FixedFree,
        /// <summary>一端固定一端铰支</summary>
        FixedPinned,
        /// <summary>两端铰支</summary>
        PinnedPinned,
        /// <summary>两端固定</summary>
        FixedFixed
    }

    /// <summary>
    /// 丝杠设计计算结果
    /// </summary>
    public class LeadscrewResult
    {
        // 输入参数
        public ScrewType ScrewType { get; set; }
        public SupportType SupportType { get; set; }
        public double D { get; set; }        // 公称直径 (mm)
        public double Ph { get; set; }       // 导程 (mm)
        public double F { get; set; }        // 轴向载荷 (N)
        public double RPM { get; set; }      // 转速 (rpm)
        public double L { get; set; }        // 行程/丝杠长度 (mm)

        // 几何参数
        public double D2 { get; set; }       // 中径 (mm)
        public double D3 { get; set; }       // 小径 (mm)
        public double As { get; set; }       // 应力截面积 (mm^2)
        public double I { get; set; }        // 截面惯性矩 (mm^4)

        // 计算结果
        /// <summary>导程角 lambda (度)</summary>
        public double Lambda { get; set; }
        /// <summary>当量摩擦角 rho (度)</summary>
        public double Rho { get; set; }
        /// <summary>效率 eta</summary>
        public double Eta { get; set; }
        /// <summary>驱动扭矩 T (N.mm)</summary>
        public double T { get; set; }
        /// <summary>拉伸应力 sigma (MPa)</summary>
        public double Sigma { get; set; }
        /// <summary>扭转剪应力 tau (MPa)</summary>
        public double Tau { get; set; }
        /// <summary>复合应力 sigma_v (MPa)</summary>
        public double SigmaV { get; set; }
        /// <summary>临界转速 nc (rpm)</summary>
        public double Nc { get; set; }
        /// <summary>压杆临界载荷 F_cr (N)</summary>
        public double Fcr { get; set; }
        /// <summary>压杆稳定性安全系数</summary>
        public double SStability { get; set; }
        /// <summary>强度安全系数</summary>
        public double SStrength { get; set; }
        /// <summary>滚珠丝杠额定动载荷 Ca (N)</summary>
        public double Ca { get; set; }
        /// <summary>额定寿命 L10 (10^6 rev)</summary>
        public double L10 { get; set; }
        /// <summary>额定寿命小时数 (h)</summary>
        public double L10h { get; set; }

        // 校核结果
        public bool IsStable { get; set; }
        public bool IsStrengthOk { get; set; }
        public bool IsSpeedOk { get; set; }
        public bool IsLifeOk { get; set; }
    }

    /// <summary>
    /// 丝杠设计计算器
    /// </summary>
    public static class LeadscrewCalculator
    {
        // 弹性模量 (钢)
        private const double E = 210000.0; // MPa
        // 密度 (钢 kg/mm^3)
        private const double Rho_steel = 7.85e-6;

        /// <summary>
        /// 计算丝杠参数
        /// </summary>
        public static LeadscrewResult Calculate(
            ScrewType screwType,
            SupportType supportType,
            double d,       // 公称直径 (mm)
            double Ph,      // 导程 (mm)
            double F,       // 轴向载荷 (N)
            double rpm,     // 转速 (rpm)
            double L,       // 丝杠长度 (mm)
            double Ca = 0)  // 额定动载荷 (N, 仅滚珠丝杠)
        {
            var result = new LeadscrewResult
            {
                ScrewType = screwType,
                SupportType = supportType,
                D = d,
                Ph = Ph,
                F = F,
                RPM = rpm,
                L = L,
                Ca = Ca
            };

            // ---- 几何参数计算 ----
            // 梯形螺纹: 牙型角30度, H = 1.866*P
            // 锯齿形螺纹: 工作面牙型角3度, 非工作面30度
            double P = Ph; // 单头螺纹导程=螺距

            if (screwType == ScrewType.Trapezoidal)
            {
                // 梯形螺纹 GB/T 5796
                result.D2 = d - 0.5 * P;               // 中径
                result.D3 = d - 2 * (0.5 * P + 0.5);   // 小径 (近似)
            }
            else if (screwType == ScrewType.Buttress)
            {
                // 锯齿形螺纹 GB/T 13576
                result.D2 = d - 0.75 * P;              // 中径
                result.D3 = d - 1.732 * P;             // 小径 (近似)
            }
            else // BallScrew
            {
                // 滚珠丝杠中径近似等于公称直径减去滚珠直径
                result.D2 = d - 0.6 * P;               // 近似
                result.D3 = d - 1.2 * P;               // 近似
            }

            result.As = Math.PI / 4.0 * result.D3 * result.D3;  // 应力截面积
            result.I = Math.PI / 64.0 * Math.Pow(result.D3, 4); // 惯性矩

            // ---- 导程角 ----
            result.Lambda = Math.Atan(Ph / (Math.PI * result.D2)) * 180.0 / Math.PI;

            // ---- 摩擦角与效率 ----
            if (screwType == ScrewType.BallScrew)
            {
                // 滚珠丝杠当量摩擦系数 ~0.003
                result.Rho = Math.Atan(0.003) * 180.0 / Math.PI;
                result.Eta = 0.95; // 滚珠丝杠典型效率 90%~98%
            }
            else
            {
                // 滑动丝杠摩擦系数: 钢-铜 f=0.08~0.12, 取0.1
                double f = (screwType == ScrewType.Trapezoidal) ? 0.10 : 0.12;
                result.Rho = Math.Atan(f / Math.Cos(15.0 * Math.PI / 180.0)) * 180.0 / Math.PI;
                double lambdaRad = result.Lambda * Math.PI / 180.0;
                double rhoRad = result.Rho * Math.PI / 180.0;
                result.Eta = Math.Tan(lambdaRad) / Math.Tan(lambdaRad + rhoRad);
            }

            // ---- 驱动扭矩 ----
            // T = F * Ph / (2 * pi * eta) [N.mm]
            if (result.Eta > 0)
            {
                result.T = F * Ph / (2.0 * Math.PI * result.Eta);
            }
            else
            {
                result.T = double.PositiveInfinity;
            }

            // ---- 强度校核 ----
            // 拉伸应力
            result.Sigma = F / result.As; // MPa (N/mm^2)

            // 扭转剪应力
            // tau = 16*T / (pi*d3^3), T in N.mm
            double d3 = result.D3;
            result.Tau = 16.0 * result.T / (Math.PI * Math.Pow(d3, 3));

            // 第四强度理论复合应力
            result.SigmaV = Math.Sqrt(result.Sigma * result.Sigma + 3.0 * result.Tau * result.Tau);

            // 强度安全系数 (取45钢 sigma_s=355 MPa)
            double sigmaS = 355.0;
            result.SStrength = sigmaS / result.SigmaV;
            result.IsStrengthOk = result.SStrength >= 2.0;

            // ---- 临界转速 ----
            // nc = 30/pi * sqrt(E*I/(rho*A*L^4)) * (pi/mu)^2
            // mu = 有效长度系数
            double mu = GetLengthCoefficient(supportType);
            double massPerLength = Rho_steel * result.As; // kg/mm
            double Leff = mu * L; // 有效长度 mm

            // 简化: nc = (30/pi) * (pi/Leff)^2 * sqrt(E*I/(rho*A))
            double lambdaCrit = Math.PI / Leff;
            result.Nc = (30.0 / Math.PI) * lambdaCrit * lambdaCrit
                        * Math.Sqrt(E * result.I / (Rho_steel * result.As));

            result.IsSpeedOk = rpm < result.Nc * 0.8; // 安全裕度 80%

            // ---- 压杆稳定性 ----
            // F_cr = pi^2 * E * I / (mu*L)^2
            result.Fcr = Math.PI * Math.PI * E * result.I / (Leff * Leff);
            result.SStability = F > 0 ? result.Fcr / F : double.PositiveInfinity;
            result.IsStable = result.SStability >= 2.5;

            // ---- 滚珠丝杠寿命 ----
            if (screwType == ScrewType.BallScrew && Ca > 0 && F > 0)
            {
                // 额定寿命 L = (Ca/Fa)^3 * 10^6 (rev)
                result.L10 = Math.Pow(Ca / F, 3.0);
                // 寿命小时 L10h = L10 * 10^6 / (60 * n)
                result.L10h = result.L10 * 1e6 / (60.0 * rpm);
                result.IsLifeOk = result.L10h >= 20000; // 一般要求20000h
            }
            else
            {
                result.L10 = 0;
                result.L10h = 0;
                result.IsLifeOk = true;
            }

            return result;
        }

        /// <summary>
        /// 获取支撑方式对应的长度系数 mu
        /// </summary>
        private static double GetLengthCoefficient(SupportType support)
        {
            switch (support)
            {
                case SupportType.FixedFree:    return 2.0;   // 悬臂
                case SupportType.FixedPinned:  return 0.7;   // 一端固定一端铰支
                case SupportType.PinnedPinned: return 1.0;   // 两端铰支
                case SupportType.FixedFixed:   return 0.5;   // 两端固定
                default:                       return 1.0;
            }
        }
    }
}
