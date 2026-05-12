using System;

namespace ShaftDesign
{
    /// <summary>
    /// 载荷类型 (循环特性)
    /// </summary>
    public enum LoadCycleType
    {
        /// <summary>脉动循环 (alpha = 0.6)</summary>
        Pulsating,
        /// <summary>对称循环 (alpha = 1.0)</summary>
        Symmetric,
        /// <summary>静载荷 (alpha = 0.3)</summary>
        Static
    }

    /// <summary>
    /// 轴上零件位置定义
    /// </summary>
    public class ShaftComponent
    {
        public string Name { get; set; }
        public string Type { get; set; } // "Gear", "Bearing", "Coupling", "Pulley", "Sprocket"
        public double Position { get; set; } // 距左端距离 (mm)
        public double Diameter { get; set; } // 该段轴径 (mm)
    }

    /// <summary>
    /// 作用在轴上的载荷
    /// </summary>
    public class ShaftLoad
    {
        public string Name { get; set; }
        public double Position { get; set; } // 作用位置距左端距离 (mm)
        /// <summary>径向力 Fr (N)</summary>
        public double RadialForce { get; set; }
        /// <summary>切向力 Ft (N)</summary>
        public double TangentialForce { get; set; }
        /// <summary>轴向力 Fa (N)</summary>
        public double AxialForce { get; set; }
    }

    /// <summary>
    /// 计算截面结果
    /// </summary>
    public class SectionResult
    {
        public double Position { get; set; }
        public string Description { get; set; }
        public double Diameter { get; set; }
        public double BendingMoment { get; set; } // 弯矩 M (N.mm)
        public double Torque { get; set; } // 扭矩 T (N.mm)
        public double EquivalentMoment { get; set; } // 当量弯矩 Me (N.mm)
        public double BendingStress { get; set; } // 弯曲应力 sigma (MPa)
        public double TorsionStress { get; set; } // 扭转切应力 tau (MPa)
        public double CombinedStress { get; set; } // 合成应力 sigma_e (MPa)
        public double SafetyFactor { get; set; } // 安全系数 n
        public bool PassesStrengthCheck { get; set; }

        // 疲劳强度校核
        public double FatigueSafetyFactorS { get; set; } // 疲劳安全系数 S
        public double FatigueSafetySigma { get; set; } // S_sigma
        public double FatigueSafetyTau { get; set; } // S_tau
        public bool PassesFatigueCheck { get; set; }
    }

    /// <summary>
    /// 弯矩图计算结果
    /// </summary>
    public class BendingMomentDiagram
    {
        public double[] Positions { get; set; }
        public double[] Moments { get; set; } // 合成弯矩 sqrt(My^2+Mz^2) (N.mm)
        public double[] MomentsY { get; set; } // 水平面弯矩 (N.mm)
        public double[] MomentsZ { get; set; } // 垂直面弯矩 (N.mm)
        public double[] ShearForcesY { get; set; } // 水平剪力 (N)
        public double[] ShearForcesZ { get; set; } // 垂直剪力 (N)
        public double MaxMoment { get; set; }
        public double MaxMomentPosition { get; set; }
    }

    /// <summary>
    /// 轴设计计算器 - 核心计算引擎
    /// </summary>
    public class ShaftCalculator
    {
        private const double PI = Math.PI;

        /// <summary>
        /// 许用安全系数 [n] (静强度)
        /// </summary>
        public double AllowableSafetyFactor { get; set; } = 1.5;

        /// <summary>
        /// 许用疲劳安全系数 [S]
        /// </summary>
        public double AllowableFatigueSafetyFactor { get; set; } = 1.5;

        /// <summary>
        /// 初估轴径 d >= A * (P/n)^(1/3)
        /// </summary>
        /// <param name="power">传递功率 P (kW)</param>
        /// <param name="speed">转速 n (rpm)</param>
        /// <param name="coefficientA">材料系数 A</param>
        /// <returns>最小轴径 (mm)</returns>
        public double EstimateDiameter(double power, double speed, double coefficientA)
        {
            if (power <= 0 || speed <= 0)
                throw new ArgumentException("功率和转速必须大于零");
            return coefficientA * Math.Pow(power / speed, 1.0 / 3.0);
        }

        /// <summary>
        /// 初估轴径 (考虑键槽)
        /// </summary>
        /// <param name="power">传递功率 P (kW)</param>
        /// <param name="speed">转速 n (rpm)</param>
        /// <param name="coefficientA">材料系数 A</param>
        /// <param name="keywayCount">键槽数量</param>
        /// <returns>考虑键槽后的最小轴径 (mm)</returns>
        public double EstimateDiameterWithKeyway(double power, double speed, double coefficientA, int keywayCount)
        {
            double d = EstimateDiameter(power, speed, coefficientA);
            switch (keywayCount)
            {
                case 1: d *= 1.05; break;
                case 2: d *= 1.10; break;
            }
            return d;
        }

        /// <summary>
        /// 计算扭矩 T = 9550 * P / n (N.m)
        /// </summary>
        /// <param name="power">功率 P (kW)</param>
        /// <param name="speed">转速 n (rpm)</param>
        /// <returns>扭矩 (N.m)</returns>
        public double CalcTorque(double power, double speed)
        {
            if (speed <= 0) throw new ArgumentException("转速必须大于零");
            return 9550.0 * power / speed;
        }

        /// <summary>
        /// 计算扭矩 (N.mm)
        /// </summary>
        public double CalcTorqueNm(double power, double speed)
        {
            return CalcTorque(power, speed) * 1000.0;
        }

        /// <summary>
        /// 获取应力校正系数 alpha
        /// </summary>
        /// <param name="cycleType">循环类型</param>
        /// <returns>alpha 值</returns>
        public double GetAlpha(LoadCycleType cycleType)
        {
            switch (cycleType)
            {
                case LoadCycleType.Pulsating: return 0.6;
                case LoadCycleType.Symmetric: return 1.0;
                case LoadCycleType.Static: return 0.3;
                default: return 0.6;
            }
        }

        /// <summary>
        /// 简支梁模型 - 计算弯矩图
        /// 假设两轴承位于 shaftSpan 的两端
        /// </summary>
        /// <param name="loads">载荷列表</param>
        /// <param name="leftBearingPos">左轴承位置 (mm)</param>
        /// <param name="rightBearingPos">右轴承位置 (mm)</param>
        /// <param name="numPoints">计算点数</param>
        /// <returns>弯矩图</returns>
        public BendingMomentDiagram CalcBendingMomentDiagram(
            ShaftLoad[] loads, double leftBearingPos, double rightBearingPos, int numPoints = 200)
        {
            double L = rightBearingPos - leftBearingPos;
            if (L <= 0) throw new ArgumentException("右轴承位置必须大于左轴承位置");

            // 计算垂直面 (XY平面, 径向力Fr + 轴向力Fa产生的弯矩) 支反力
            double sumFy = 0;
            double sumMomentAboutLeft = 0;
            foreach (var load in loads)
            {
                double a = load.Position - leftBearingPos;
                sumFy += load.RadialForce;
                sumMomentAboutLeft += load.RadialForce * a;
            }

            // 右轴承支反力 (垂直面)
            double Ry = sumMomentAboutLeft / L;
            // 左轴承支反力 (垂直面)
            double Ly = sumFy - Ry;

            // 计算水平面 (XZ平面, 切向力Ft产生的弯矩) 支反力
            double sumFz = 0;
            double sumMomentAboutLeftZ = 0;
            foreach (var load in loads)
            {
                double a = load.Position - leftBearingPos;
                sumFz += load.TangentialForce;
                sumMomentAboutLeftZ += load.TangentialForce * a;
            }

            double Rz = sumMomentAboutLeftZ / L;
            double Lz = sumFz - Rz;

            // 生成计算截面
            double startPos = Math.Min(leftBearingPos, loads.Length > 0 ? loads[0].Position : leftBearingPos);
            double endPos = Math.Max(rightBearingPos, loads.Length > 0 ? loads[loads.Length - 1].Position : rightBearingPos);
            startPos = Math.Min(startPos, leftBearingPos);
            endPos = Math.Max(endPos, rightBearingPos);

            // 包含所有载荷点和支承点
            var allPoints = new System.Collections.Generic.SortedSet<double>();
            allPoints.Add(leftBearingPos);
            allPoints.Add(rightBearingPos);
            foreach (var load in loads)
            {
                allPoints.Add(load.Position);
            }
            // 在每两个关键点之间插入等分点
            var pointList = new System.Collections.Generic.List<double>(allPoints);
            var finalPoints = new System.Collections.Generic.List<double>();
            finalPoints.Add(pointList[0]);
            for (int i = 1; i < pointList.Count; i++)
            {
                double p0 = pointList[i - 1];
                double p1 = pointList[i];
                int segPoints = Math.Max(5, (int)((p1 - p0) / (endPos - startPos) * numPoints));
                for (int j = 1; j <= segPoints; j++)
                {
                    double x = p0 + (p1 - p0) * j / segPoints;
                    if (!finalPoints.Contains(x))
                        finalPoints.Add(x);
                }
            }

            double[] positions = finalPoints.ToArray();
            double[] momentsY = new double[positions.Length];
            double[] momentsZ = new double[positions.Length];
            double[] moments = new double[positions.Length];
            double[] shearY = new double[positions.Length];
            double[] shearZ = new double[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                double x = positions[i] - leftBearingPos;

                // 垂直面弯矩 (XY)
                double My = Ly * x;
                double Vy = Ly;
                foreach (var load in loads)
                {
                    double a = load.Position - leftBearingPos;
                    if (x > a)
                    {
                        My -= load.RadialForce * (x - a);
                        Vy -= load.RadialForce;
                    }
                }
                momentsY[i] = My;
                shearY[i] = Vy;

                // 水平面弯矩 (XZ)
                double Mz = Lz * x;
                double Vz = Lz;
                foreach (var load in loads)
                {
                    double a = load.Position - leftBearingPos;
                    if (x > a)
                    {
                        Mz -= load.TangentialForce * (x - a);
                        Vz -= load.TangentialForce;
                    }
                }
                momentsZ[i] = Mz;
                shearZ[i] = Vz;

                // 合成弯矩
                moments[i] = Math.Sqrt(My * My + Mz * Mz);
            }

            // 找最大弯矩
            double maxM = 0;
            double maxPos = positions[0];
            for (int i = 0; i < moments.Length; i++)
            {
                if (Math.Abs(moments[i]) > maxM)
                {
                    maxM = Math.Abs(moments[i]);
                    maxPos = positions[i];
                }
            }

            return new BendingMomentDiagram
            {
                Positions = positions,
                Moments = moments,
                MomentsY = momentsY,
                MomentsZ = momentsZ,
                ShearForcesY = shearY,
                ShearForcesZ = shearZ,
                MaxMoment = maxM,
                MaxMomentPosition = maxPos
            };
        }

        /// <summary>
        /// 当量弯矩 Me = sqrt(M^2 + (alpha*T)^2)
        /// </summary>
        /// <param name="bendingMoment">弯矩 M (N.mm)</param>
        /// <param name="torque">扭矩 T (N.mm)</param>
        /// <param name="alpha">应力校正系数</param>
        /// <returns>当量弯矩 Me (N.mm)</returns>
        public double EquivalentMoment(double bendingMoment, double torque, double alpha)
        {
            return Math.Sqrt(bendingMoment * bendingMoment + (alpha * torque) * (alpha * torque));
        }

        /// <summary>
        /// 弯曲应力 sigma = 32*Me/(pi*d^3)
        /// </summary>
        /// <param name="equivalentMoment">当量弯矩 Me (N.mm)</param>
        /// <param name="diameter">轴径 d (mm)</param>
        /// <returns>弯曲应力 (MPa)</returns>
        public double BendingStress(double equivalentMoment, double diameter)
        {
            if (diameter <= 0) throw new ArgumentException("轴径必须大于零");
            return 32.0 * equivalentMoment / (PI * Math.Pow(diameter, 3));
        }

        /// <summary>
        /// 扭转切应力 tau = 16*T/(pi*d^3)
        /// </summary>
        /// <param name="torque">扭矩 T (N.mm)</param>
        /// <param name="diameter">轴径 d (mm)</param>
        /// <returns>扭转切应力 (MPa)</returns>
        public double TorsionShearStress(double torque, double diameter)
        {
            if (diameter <= 0) throw new ArgumentException("轴径必须大于零");
            return 16.0 * torque / (PI * Math.Pow(diameter, 3));
        }

        /// <summary>
        /// 合成应力 (第三强度理论) sigma_e = sqrt(sigma^2 + 4*tau^2)
        /// </summary>
        /// <param name="sigma">弯曲应力 (MPa)</param>
        /// <param name="tau">扭转切应力 (MPa)</param>
        /// <returns>合成应力 (MPa)</returns>
        public double CombinedStress(double sigma, double tau)
        {
            return Math.Sqrt(sigma * sigma + 4.0 * tau * tau);
        }

        /// <summary>
        /// 静强度安全系数 n = sigma_s / sigma_e
        /// </summary>
        /// <param name="yieldStrength">屈服强度 sigma_s (MPa)</param>
        /// <param name="combinedStress">合成应力 sigma_e (MPa)</param>
        /// <returns>安全系数</returns>
        public double StaticSafetyFactor(double yieldStrength, double combinedStress)
        {
            if (combinedStress <= 0) return double.MaxValue;
            return yieldStrength / combinedStress;
        }

        /// <summary>
        /// 疲劳强度校核 - 弯曲应力幅和平均应力
        /// 假设脉动循环: sigma_a = sigma_m = sigma/2
        /// 假设对称循环: sigma_a = sigma, sigma_m = 0
        /// </summary>
        /// <param name="sigma">应力幅 (MPa)</param>
        /// <param name="cycleType">循环类型</param>
        /// <param name="sigmaA">输出应力幅</param>
        /// <param name="sigmaM">输出平均应力</param>
        public void GetStressComponents(double sigma, LoadCycleType cycleType,
            out double sigmaA, out double sigmaM)
        {
            switch (cycleType)
            {
                case LoadCycleType.Symmetric:
                    sigmaA = sigma;
                    sigmaM = 0;
                    break;
                case LoadCycleType.Pulsating:
                    sigmaA = sigma / 2.0;
                    sigmaM = sigma / 2.0;
                    break;
                case LoadCycleType.Static:
                    sigmaA = 0;
                    sigmaM = sigma;
                    break;
                default:
                    sigmaA = sigma / 2.0;
                    sigmaM = sigma / 2.0;
                    break;
            }
        }

        /// <summary>
        /// 有效应力集中系数 K_sigma (弯曲)
        /// 粗略估算: 光轴 K_sigma=1.0, 有键槽 K_sigma=1.5~2.0, 有圆角过渡 K_sigma=1.2~1.8
        /// </summary>
        /// <param name="hasKeyway">是否有键槽</param>
        /// <param name="hasShoulder">是否有轴肩过渡</param>
        /// <returns>K_sigma</returns>
        public double GetEffectiveStressConcentrationFactor(bool hasKeyway, bool hasShoulder)
        {
            double k = 1.0;
            if (hasKeyway) k *= 1.8;
            if (hasShoulder) k *= 1.5;
            return k;
        }

        /// <summary>
        /// 有效应力集中系数 K_tau (扭转)
        /// </summary>
        /// <param name="hasKeyway">是否有键槽</param>
        /// <returns>K_tau</returns>
        public double GetEffectiveStressConcentrationFactorTorsion(bool hasKeyway)
        {
            return hasKeyway ? 1.6 : 1.0;
        }

        /// <summary>
        /// 尺寸系数 epsilon_sigma
        /// d <= 30mm: 1.0, d=50: 0.85, d=100: 0.75, d=200: 0.65, d>300: 0.6
        /// </summary>
        public double GetSizeFactor(double diameter)
        {
            if (diameter <= 30) return 1.0;
            if (diameter <= 50) return 0.85;
            if (diameter <= 100) return 0.75;
            if (diameter <= 200) return 0.65;
            if (diameter <= 300) return 0.60;
            return 0.55;
        }

        /// <summary>
        /// 表面质量系数 beta
        /// 粗糙: 0.7, 半精车: 0.85, 精车/磨: 1.0, 抛光: 1.1
        /// </summary>
        public double GetSurfaceFactor(double roughnessRa)
        {
            if (roughnessRa >= 12.5) return 0.7;   // 粗车 Ra 12.5
            if (roughnessRa >= 6.3) return 0.80;    // 半精车 Ra 6.3
            if (roughnessRa >= 3.2) return 0.85;    // 半精车 Ra 3.2
            if (roughnessRa >= 1.6) return 0.90;    // 精车 Ra 1.6
            if (roughnessRa >= 0.8) return 1.0;     // 磨削 Ra 0.8
            return 1.1;                              // 抛光
        }

        /// <summary>
        /// 弯曲疲劳安全系数 S_sigma
        /// S_sigma = sigma-1 / (K_sigma * sigma_a / (epsilon_sigma * beta) + psi_sigma * sigma_m)
        /// 其中 psi_sigma 为等效系数, 对于碳钢约 0.1~0.2, 合金钢约 0.2~0.3
        /// </summary>
        /// <param name="sigmaMinus1">对称循环弯曲疲劳极限 (MPa)</param>
        /// <param name="sigmaA">应力幅 (MPa)</param>
        /// <param name="sigmaM">平均应力 (MPa)</param>
        /// <param name="kSigma">有效应力集中系数</param>
        /// <param name="epsilonSigma">尺寸系数</param>
        /// <param name="beta">表面质量系数</param>
        /// <param name="psiSigma">等效系数 (弯曲), 默认 0.15</param>
        /// <returns>S_sigma</returns>
        public double FatigueSafetySigma(double sigmaMinus1, double sigmaA, double sigmaM,
            double kSigma, double epsilonSigma, double beta, double psiSigma = 0.15)
        {
            double denominator = kSigma * sigmaA / (epsilonSigma * beta) + psiSigma * sigmaM;
            if (denominator <= 0) return double.MaxValue;
            return sigmaMinus1 / denominator;
        }

        /// <summary>
        /// 扭转疲劳安全系数 S_tau
        /// S_tau = tau-1 / (K_tau * tau_a / (epsilon_tau * beta) + psi_tau * tau_m)
        /// </summary>
        /// <param name="tauMinus1">对称循环扭转疲劳极限 (MPa)</param>
        /// <param name="tauA">扭转应力幅 (MPa)</param>
        /// <param name="tauM">扭转平均应力 (MPa)</param>
        /// <param name="kTau">有效应力集中系数</param>
        /// <param name="epsilonTau">尺寸系数</param>
        /// <param name="beta">表面质量系数</param>
        /// <param name="psiTau">等效系数 (扭转), 默认 0.10</param>
        /// <returns>S_tau</returns>
        public double FatigueSafetyTau(double tauMinus1, double tauA, double tauM,
            double kTau, double epsilonTau, double beta, double psiTau = 0.10)
        {
            double denominator = kTau * tauA / (epsilonTau * beta) + psiTau * tauM;
            if (denominator <= 0) return double.MaxValue;
            return tauMinus1 / denominator;
        }

        /// <summary>
        /// 疲劳强度综合安全系数 S = S_sigma*S_tau/sqrt(S_sigma^2+S_tau^2)
        /// </summary>
        /// <param name="sSigma">弯曲疲劳安全系数 S_sigma</param>
        /// <param name="sTau">扭转疲劳安全系数 S_tau</param>
        /// <returns>综合安全系数 S</returns>
        public double CombinedFatigueSafetyFactor(double sSigma, double sTau)
        {
            if (sSigma <= 0 || sTau <= 0) return 0;
            return (sSigma * sTau) / Math.Sqrt(sSigma * sSigma + sTau * sTau);
        }

        /// <summary>
        /// 综合计算: 对指定截面进行强度校核和疲劳校核
        /// </summary>
        /// <param name="material">材料</param>
        /// <param name="diameter">截面直径 (mm)</param>
        /// <param name="bendingMoment">弯矩 M (N.mm)</param>
        /// <param name="torque">扭矩 T (N.mm)</param>
        /// <param name="cycleType">载荷循环类型</param>
        /// <param name="hasKeyway">是否有键槽</param>
        /// <param name="hasShoulder">是否有轴肩</param>
        /// <param name="surfaceRoughness">表面粗糙度 Ra (um)</param>
        /// <returns>截面校核结果</returns>
        public SectionResult CheckSection(ShaftMaterial material, double diameter,
            double bendingMoment, double torque, LoadCycleType cycleType,
            bool hasKeyway = false, bool hasShoulder = false, double surfaceRoughness = 1.6)
        {
            double alpha = GetAlpha(cycleType);

            // 当量弯矩
            double Me = EquivalentMoment(bendingMoment, torque, alpha);

            // 应力计算
            double sigma = BendingStress(Me, diameter);
            double tau = TorsionShearStress(torque, diameter);
            double sigmaE = CombinedStress(sigma, tau);

            // 静强度安全系数
            double n = StaticSafetyFactor(material.SigmaS, sigmaE);

            // 疲劳强度校核
            // 弯曲分量
            double sigmaA, sigmaM;
            GetStressComponents(sigma, cycleType, out sigmaA, out sigmaM);

            // 扭转分量 (假设与弯曲同循环特性)
            double tauA, tauM;
            GetStressComponents(tau, cycleType, out tauA, out tauM);

            double kSigma = GetEffectiveStressConcentrationFactor(hasKeyway, hasShoulder);
            double kTau = GetEffectiveStressConcentrationFactorTorsion(hasKeyway);
            double epsilonSigma = GetSizeFactor(diameter);
            double epsilonTau = GetSizeFactor(diameter); // 扭转尺寸系数近似相同
            double beta = GetSurfaceFactor(surfaceRoughness);

            double sSigma = FatigueSafetySigma(material.SigmaMinus1, sigmaA, sigmaM,
                kSigma, epsilonSigma, beta);
            double sTau = FatigueSafetyTau(material.TauMinus1, tauA, tauM,
                kTau, epsilonTau, beta);
            double S = CombinedFatigueSafetyFactor(sSigma, sTau);

            return new SectionResult
            {
                Diameter = diameter,
                BendingMoment = bendingMoment,
                Torque = torque,
                EquivalentMoment = Me,
                BendingStress = sigma,
                TorsionStress = tau,
                CombinedStress = sigmaE,
                SafetyFactor = n,
                PassesStrengthCheck = n >= AllowableSafetyFactor,
                FatigueSafetySigma = sSigma,
                FatigueSafetyTau = sTau,
                FatigueSafetyFactorS = S,
                PassesFatigueCheck = S >= AllowableFatigueSafetyFactor
            };
        }

        /// <summary>
        /// 由许用应力反算最小轴径
        /// d >= (32*Me/(pi*[sigma]))^(1/3)
        /// </summary>
        /// <param name="equivalentMoment">当量弯矩 (N.mm)</param>
        /// <param name="allowableStress">许用弯曲应力 (MPa)</param>
        /// <returns>最小直径 (mm)</returns>
        public double MinDiameterFromStress(double equivalentMoment, double allowableStress)
        {
            if (allowableStress <= 0) throw new ArgumentException("许用应力必须大于零");
            return Math.Pow(32.0 * equivalentMoment / (PI * allowableStress), 1.0 / 3.0);
        }

        /// <summary>
        /// 由扭转强度反算最小轴径
        /// d >= (16*T/(pi*[tau]))^(1/3)
        /// </summary>
        /// <param name="torque">扭矩 T (N.mm)</param>
        /// <param name="allowableShearStress">许用扭转切应力 (MPa)</param>
        /// <returns>最小直径 (mm)</returns>
        public double MinDiameterFromTorsion(double torque, double allowableShearStress)
        {
            if (allowableShearStress <= 0) throw new ArgumentException("许用切应力必须大于零");
            return Math.Pow(16.0 * torque / (PI * allowableShearStress), 1.0 / 3.0);
        }
    }
}
