using System;
using System.Collections.Generic;

namespace CamDesign
{
    /// <summary>
    /// 凸轮机构核心计算类
    /// 包含位移、速度、加速度、压力角、基圆半径等计算方法
    /// </summary>
    public class CamCalculator
    {
        private const double PI = Math.PI;
        private const double TwoPI = 2.0 * Math.PI;

        #region 位移计算

        /// <summary>
        /// 根据运动规律计算从动件位移
        /// </summary>
        /// <param name="motionLaw">运动规律类型</param>
        /// <param name="theta">当前角度位置（弧度），范围 [0, beta]</param>
        /// <param name="beta">运动阶段的角度范围（弧度）</param>
        /// <param name="h">升程/行程（mm）</param>
        /// <returns>从动件位移（mm）</returns>
        public double CalcDisplacement(MotionLaw motionLaw, double theta, double beta, double h)
        {
            if (beta <= 0) return 0;
            double t = theta / beta; // 归一化角度 [0, 1]

            switch (motionLaw)
            {
                case MotionLaw.ConstantVelocity:
                    return CalcDisplacementConstantVelocity(t, h);

                case MotionLaw.ConstantAcceleration:
                    return CalcDisplacementConstantAcceleration(t, h);

                case MotionLaw.CosineAcceleration:
                    return CalcDisplacementCosine(t, h);

                case MotionLaw.SineAcceleration:
                    return CalcDisplacementSine(t, h);

                case MotionLaw.FifthOrderPolynomial:
                    return CalcDisplacementFifthOrder(t, h);

                case MotionLaw.ModifiedTrapezoidal:
                    return CalcDisplacementModifiedTrapezoidal(t, h);

                case MotionLaw.ModifiedSine:
                    return CalcDisplacementModifiedSine(t, h);

                case MotionLaw.Stop:
                    return 0;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// 等速运动（直线运动规律）
        /// s = h * theta / beta = h * t
        /// </summary>
        private double CalcDisplacementConstantVelocity(double t, double h)
        {
            return h * t;
        }

        /// <summary>
        /// 等加速等减速运动（抛物线运动规律）
        /// 前半程（加速段）: s = 2h * t^2
        /// 后半程（减速段）: s = h - 2h * (1-t)^2
        /// </summary>
        private double CalcDisplacementConstantAcceleration(double t, double h)
        {
            if (t <= 0.5)
            {
                // 加速段: s = 2h * t^2
                return 2.0 * h * t * t;
            }
            else
            {
                // 减速段: s = h - 2h * (1-t)^2
                double onemt = 1.0 - t;
                return h - 2.0 * h * onemt * onemt;
            }
        }

        /// <summary>
        /// 余弦加速度运动（简谐运动规律）
        /// s = h/2 * (1 - cos(pi * theta / beta))
        /// </summary>
        private double CalcDisplacementCosine(double t, double h)
        {
            return h / 2.0 * (1.0 - Math.Cos(PI * t));
        }

        /// <summary>
        /// 正弦加速度运动（摆线运动规律）
        /// s = h * (theta/beta - sin(2*pi*theta/beta) / (2*pi))
        /// </summary>
        private double CalcDisplacementSine(double t, double h)
        {
            return h * (t - Math.Sin(TwoPI * t) / TwoPI);
        }

        /// <summary>
        /// 五次多项式运动规律
        /// s = h * (10*t^3 - 15*t^4 + 6*t^5)
        /// 满足边界条件: s(0)=0, s(1)=h, v(0)=v(1)=0, a(0)=a(1)=0
        /// </summary>
        private double CalcDisplacementFifthOrder(double t, double h)
        {
            double t3 = t * t * t;
            double t4 = t3 * t;
            double t5 = t4 * t;
            return h * (10.0 * t3 - 15.0 * t4 + 6.0 * t5);
        }

        /// <summary>
        /// 改进梯形加速度运动规律（摆线-抛物线-摆线组合）
        /// 分段计算: 0~1/8 正弦加速, 1/8~3/8 等加速, 3/8~5/8 正弦, 5/8~7/8 等减速, 7/8~1 正弦减速
        /// </summary>
        private double CalcDisplacementModifiedTrapezoidal(double t, double h)
        {
            double t1 = 0.125; // 1/8
            double t2 = 0.375; // 3/8
            double t3 = 0.625; // 5/8
            double t4 = 0.875; // 7/8

            // 修正系数，保证连续性
            double c1 = TwoPI;
            double sinPart = 2.0 / c1; // 2/(2*pi) = 1/pi

            if (t <= t1)
            {
                // 正弦加速段: s = h * [2t/(4+pi) - sin(4*pi*t) / (pi*(4+pi))]
                double K = 1.0 / (4.0 + PI);
                return h * (2.0 * K * t - K * Math.Sin(4.0 * PI * t) / (2.0 * PI));
            }
            else if (t <= t2)
            {
                // 等加速段
                double K = 1.0 / (4.0 + PI);
                double s1 = h * (2.0 * K * t1 - K * Math.Sin(4.0 * PI * t1) / (2.0 * PI));
                double dt = t - t1;
                return s1 + h * K * (4.0 * PI * dt + 2.0) * dt;
            }
            else if (t <= t3)
            {
                // 正弦过渡段
                double K = 1.0 / (4.0 + PI);
                double s2_end = 0.5;
                return h * s2_end + h * K * (t - 0.5) - h * K * Math.Sin(4.0 * PI * (t - 0.5)) / (2.0 * PI);
            }
            else if (t <= t4)
            {
                // 等减速段
                double K = 1.0 / (4.0 + PI);
                double s3 = h * (1.0 - 2.0 * K * (1.0 - t3) + K * Math.Sin(4.0 * PI * (1.0 - t3)) / (2.0 * PI));
                double dt = t - t3;
                return s3 + h * K * (4.0 * PI * dt + 2.0) * dt;
            }
            else
            {
                // 正弦减速段
                double K = 1.0 / (4.0 + PI);
                double remainder = 1.0 - t;
                return h * (1.0 - 2.0 * K * remainder + K * Math.Sin(4.0 * PI * remainder) / (2.0 * PI));
            }
        }

        /// <summary>
        /// 改进正弦加速度运动规律
        /// </summary>
        private double CalcDisplacementModifiedSine(double t, double h)
        {
            double t1 = 0.25;
            double K = 1.0 / (4.0 + PI);

            if (t <= t1)
            {
                return h * K * (PI * t - 0.25 * Math.Sin(4.0 * PI * t));
            }
            else
            {
                double s1 = h * K * (PI * t1 - 0.25 * Math.Sin(4.0 * PI * t1));
                double dt = t - t1;
                return s1 + h * (1.0 - 2.0 * K) * dt + h * K * 0.25 * (Math.Sin(4.0 * PI * t) - Math.Sin(4.0 * PI * t1));
            }
        }

        #endregion

        #region 速度计算

        /// <summary>
        /// 根据运动规律计算从动件速度
        /// v = ds/dt = (ds/dtheta) * omega
        /// </summary>
        /// <param name="motionLaw">运动规律类型</param>
        /// <param name="theta">当前角度位置（弧度）</param>
        /// <param name="beta">运动阶段的角度范围（弧度）</param>
        /// <param name="h">升程/行程（mm）</param>
        /// <param name="omega">凸轮角速度（rad/s）</param>
        /// <returns>从动件速度（mm/s）</returns>
        public double CalcVelocity(MotionLaw motionLaw, double theta, double beta, double h, double omega)
        {
            if (beta <= 0) return 0;
            double ds_dtheta = CalcDsDtheta(motionLaw, theta, beta, h);
            return ds_dtheta * omega;
        }

        /// <summary>
        /// 计算 ds/dtheta (位移对角度的导数)
        /// </summary>
        public double CalcDsDtheta(MotionLaw motionLaw, double theta, double beta, double h)
        {
            if (beta <= 0) return 0;
            double t = theta / beta;
            double ds_dt = 0;

            switch (motionLaw)
            {
                case MotionLaw.ConstantVelocity:
                    // ds/dt = h => ds/dtheta = h / beta
                    ds_dt = h;
                    break;

                case MotionLaw.ConstantAcceleration:
                    if (t <= 0.5)
                        ds_dt = 4.0 * h * t;
                    else
                        ds_dt = 4.0 * h * (1.0 - t);
                    break;

                case MotionLaw.CosineAcceleration:
                    // ds/dt = h*pi/2 * sin(pi*t)
                    ds_dt = h * PI / 2.0 * Math.Sin(PI * t);
                    break;

                case MotionLaw.SineAcceleration:
                    // ds/dt = h * (1 - cos(2*pi*t))
                    ds_dt = h * (1.0 - Math.Cos(TwoPI * t));
                    break;

                case MotionLaw.FifthOrderPolynomial:
                    // ds/dt = h * (30*t^2 - 60*t^3 + 30*t^4)
                    double t2 = t * t;
                    double t3 = t2 * t;
                    double t4 = t3 * t;
                    ds_dt = h * (30.0 * t2 - 60.0 * t3 + 30.0 * t4);
                    break;

                case MotionLaw.ModifiedTrapezoidal:
                    ds_dt = CalcDsDtModifiedTrapezoidal(t, h);
                    break;

                case MotionLaw.ModifiedSine:
                    ds_dt = CalcDsDtModifiedSine(t, h);
                    break;

                case MotionLaw.Stop:
                    ds_dt = 0;
                    break;
            }

            return ds_dt / beta;
        }

        private double CalcDsDtModifiedTrapezoidal(double t, double h)
        {
            double K = 1.0 / (4.0 + PI);
            double t1 = 0.125;
            double t2 = 0.375;
            double t3 = 0.625;
            double t4 = 0.875;

            if (t <= t1)
            {
                return h * K * (2.0 - 2.0 * Math.Cos(4.0 * PI * t));
            }
            else if (t <= t2)
            {
                return h * K * 4.0 * PI;
            }
            else if (t <= t3)
            {
                return h * K * (2.0 + 2.0 * Math.Cos(4.0 * PI * (t - 0.5)));
            }
            else if (t <= t4)
            {
                return h * K * 4.0 * PI;
            }
            else
            {
                return h * K * (2.0 - 2.0 * Math.Cos(4.0 * PI * (1.0 - t)));
            }
        }

        private double CalcDsDtModifiedSine(double t, double h)
        {
            double K = 1.0 / (4.0 + PI);
            double t1 = 0.25;

            if (t <= t1)
            {
                return h * K * (PI - PI * Math.Cos(4.0 * PI * t));
            }
            else
            {
                return h * (1.0 - 2.0 * K) + h * K * PI * Math.Cos(4.0 * PI * t);
            }
        }

        #endregion

        #region 加速度计算

        /// <summary>
        /// 根据运动规律计算从动件加速度
        /// a = d^2s/dt^2 = (d^2s/dtheta^2) * omega^2
        /// </summary>
        /// <param name="motionLaw">运动规律类型</param>
        /// <param name="theta">当前角度位置（弧度）</param>
        /// <param name="beta">运动阶段的角度范围（弧度）</param>
        /// <param name="h">升程/行程（mm）</param>
        /// <param name="omega">凸轮角速度（rad/s）</param>
        /// <returns>从动件加速度（mm/s^2）</returns>
        public double CalcAcceleration(MotionLaw motionLaw, double theta, double beta, double h, double omega)
        {
            if (beta <= 0) return 0;
            double d2s_dtheta2 = CalcD2sDtheta2(motionLaw, theta, beta, h);
            return d2s_dtheta2 * omega * omega;
        }

        /// <summary>
        /// 计算 d^2s/dtheta^2 (位移对角度的二阶导数)
        /// </summary>
        public double CalcD2sDtheta2(MotionLaw motionLaw, double theta, double beta, double h)
        {
            if (beta <= 0) return 0;
            double t = theta / beta;
            double d2s_dt2 = 0;

            switch (motionLaw)
            {
                case MotionLaw.ConstantVelocity:
                    // d2s/dt2 = 0
                    d2s_dt2 = 0;
                    break;

                case MotionLaw.ConstantAcceleration:
                    if (t <= 0.5)
                        d2s_dt2 = 4.0 * h;
                    else
                        d2s_dt2 = -4.0 * h;
                    break;

                case MotionLaw.CosineAcceleration:
                    // d2s/dt2 = h*pi^2/2 * cos(pi*t)
                    d2s_dt2 = h * PI * PI / 2.0 * Math.Cos(PI * t);
                    break;

                case MotionLaw.SineAcceleration:
                    // d2s/dt2 = h * 2*pi * sin(2*pi*t)
                    d2s_dt2 = h * TwoPI * Math.Sin(TwoPI * t);
                    break;

                case MotionLaw.FifthOrderPolynomial:
                    // d2s/dt2 = h * (60*t - 180*t^2 + 120*t^3)
                    double t2 = t * t;
                    double t3 = t2 * t;
                    d2s_dt2 = h * (60.0 * t - 180.0 * t2 + 120.0 * t3);
                    break;

                case MotionLaw.ModifiedTrapezoidal:
                    d2s_dt2 = CalcD2sDt2ModifiedTrapezoidal(t, h);
                    break;

                case MotionLaw.ModifiedSine:
                    d2s_dt2 = CalcD2sDt2ModifiedSine(t, h);
                    break;

                case MotionLaw.Stop:
                    d2s_dt2 = 0;
                    break;
            }

            return d2s_dt2 / (beta * beta);
        }

        private double CalcD2sDt2ModifiedTrapezoidal(double t, double h)
        {
            double K = 1.0 / (4.0 + PI);
            double t1 = 0.125;
            double t2 = 0.375;
            double t3 = 0.625;
            double t4 = 0.875;

            if (t <= t1)
            {
                return h * K * 8.0 * PI * Math.Sin(4.0 * PI * t);
            }
            else if (t <= t2)
            {
                return 0;
            }
            else if (t <= t3)
            {
                return -h * K * 8.0 * PI * Math.Sin(4.0 * PI * (t - 0.5));
            }
            else if (t <= t4)
            {
                return 0;
            }
            else
            {
                return h * K * 8.0 * PI * Math.Sin(4.0 * PI * (1.0 - t));
            }
        }

        private double CalcD2sDt2ModifiedSine(double t, double h)
        {
            double K = 1.0 / (4.0 + PI);
            double t1 = 0.25;

            if (t <= t1)
            {
                return h * K * 4.0 * PI * PI * Math.Sin(4.0 * PI * t);
            }
            else
            {
                return -h * K * 4.0 * PI * PI * Math.Sin(4.0 * PI * t);
            }
        }

        #endregion

        #region 压力角计算

        /// <summary>
        /// 计算凸轮机构压力角
        /// 对心直动滚子从动件: alpha = arctan((ds/dtheta) / (rb + s))
        /// 偏置直动滚子从动件: alpha = arctan((ds/dtheta - e) / (rb + s))
        /// 其中 e 为偏距，rb 为基圆半径，s 为位移
        /// </summary>
        /// <param name="ds_dtheta">位移对角度的导数 ds/dtheta</param>
        /// <param name="e">从动件偏距 (mm)，对心时为0</param>
        /// <param name="rb">基圆半径 (mm)</param>
        /// <param name="s">当前位移 (mm)</param>
        /// <returns>压力角（度）</returns>
        public double CalcPressureAngle(double ds_dtheta, double e, double rb, double s)
        {
            double numerator = ds_dtheta - e;
            double denominator = rb + s;

            if (Math.Abs(denominator) < 1e-10)
                return 90.0;

            return Math.Atan(numerator / denominator) * 180.0 / PI;
        }

        /// <summary>
        /// 计算摆动从动件凸轮机构的压力角
        /// alpha = arctan(l * dpsi/dtheta - a*sin(psi0+psi)) / (a*cos(psi0+psi) - l*(1+dpsi/dtheta)))
        /// </summary>
        /// <param name="dpsi_dtheta">摆角对凸轮转角的导数</param>
        /// <param name="a">中心距 (mm)</param>
        /// <param name="l">摆杆长度 (mm)</param>
        /// <param name="psi">当前摆角（弧度）</param>
        /// <param name="psi0">初始摆角（弧度）</param>
        /// <returns>压力角（度）</returns>
        public double CalcPressureAngleOscillating(double dpsi_dtheta, double a, double l, double psi, double psi0)
        {
            double totalAngle = psi0 + psi;
            double numerator = l * dpsi_dtheta - a * Math.Sin(totalAngle);
            double denominator = a * Math.Cos(totalAngle) - l * (1.0 + dpsi_dtheta);

            if (Math.Abs(denominator) < 1e-10)
                return 90.0;

            return Math.Atan(numerator / denominator) * 180.0 / PI;
        }

        #endregion

        #region 基圆半径计算

        /// <summary>
        /// 计算最小基圆半径
        /// 对心直动从动件: rb >= (ds/dtheta_max) / tan(alpha_allowable) - s_min
        /// 偏置直动从动件: rb >= (ds/dtheta_max - e) / tan(alpha_allowable) - s_min
        /// </summary>
        /// <param name="ds_dtheta_max">最大 ds/dtheta 值</param>
        /// <param name="alpha_allow">许用压力角（度）</param>
        /// <param name="e">从动件偏距 (mm)</param>
        /// <param name="s_min">最小位移（通常为0）(mm)</param>
        /// <returns>最小基圆半径 (mm)</returns>
        public double CalcBaseCircleRadius(double ds_dtheta_max, double alpha_allow, double e = 0, double s_min = 0)
        {
            double tanAlpha = Math.Tan(alpha_allow * PI / 180.0);
            if (Math.Abs(tanAlpha) < 1e-10)
                return double.MaxValue;

            double rb = (ds_dtheta_max - e) / tanAlpha - s_min;
            return Math.Max(rb, 0);
        }

        /// <summary>
        /// 校核基圆半径是否满足压力角要求
        /// </summary>
        /// <param name="rb">当前基圆半径 (mm)</param>
        /// <param name="ds_dtheta_max">最大 ds/dtheta 值</param>
        /// <param name="alpha_allow">许用压力角（度）</param>
        /// <param name="e">偏距 (mm)</param>
        /// <returns>是否满足要求</returns>
        public bool VerifyBaseCircleRadius(double rb, double ds_dtheta_max, double alpha_allow, double e = 0)
        {
            double rb_min = CalcBaseCircleRadius(ds_dtheta_max, alpha_allow, e);
            return rb >= rb_min;
        }

        #endregion

        #region 凸轮轮廓生成

        /// <summary>
        /// 生成盘形凸轮理论轮廓坐标
        /// 直动从动件对心: x = (rb + s) * cos(theta), y = (rb + s) * sin(theta)
        /// 偏置从动件: x = (rb+s)*cos(theta) - e*sin(theta), y = (rb+s)*sin(theta) + e*cos(theta)
        /// </summary>
        /// <param name="rb">基圆半径 (mm)</param>
        /// <param name="s">位移 (mm)</param>
        /// <param name="theta">凸轮转角（弧度）</param>
        /// <param name="e">偏距 (mm)</param>
        /// <param name="x">输出 X 坐标</param>
        /// <param name="y">输出 Y 坐标</param>
        public void CalcTheoreticalProfile(double rb, double s, double theta, double e,
            out double x, out double y)
        {
            double r = rb + s;
            x = r * Math.Cos(theta) - e * Math.Sin(theta);
            y = r * Math.Sin(theta) + e * Math.Cos(theta);
        }

        /// <summary>
        /// 生成盘形凸轮实际轮廓坐标（内包络线）
        /// 对于滚子从动件，实际轮廓 = 理论轮廓 - 滚子半径
        /// </summary>
        /// <param name="rb">基圆半径 (mm)</param>
        /// <param name="s">位移 (mm)</param>
        /// <param name="ds_dtheta">位移导数</param>
        /// <param name="theta">凸轮转角（弧度）</param>
        /// <param name="e">偏距 (mm)</param>
        /// <param name="rr">滚子半径 (mm)</param>
        /// <param name="x">输出 X 坐标</param>
        /// <param name="y">输出 Y 坐标</param>
        public void CalcActualProfile(double rb, double s, double ds_dtheta, double theta,
            double e, double rr, out double x, out double y)
        {
            double r = rb + s;
            double dx = ds_dtheta * Math.Cos(theta) - r * Math.Sin(theta) - e * Math.Cos(theta);
            double dy = ds_dtheta * Math.Sin(theta) + r * Math.Cos(theta) - e * Math.Sin(theta);
            double norm = Math.Sqrt(dx * dx + dy * dy);

            if (norm < 1e-10)
            {
                x = (r) * Math.Cos(theta) - e * Math.Sin(theta);
                y = (r) * Math.Sin(theta) + e * Math.Cos(theta);
                return;
            }

            // 法线方向（指向凸轮体外侧）
            double nx = dy / norm;
            double ny = -dx / norm;

            // 理论轮廓点
            double tx = r * Math.Cos(theta) - e * Math.Sin(theta);
            double ty = r * Math.Sin(theta) + e * Math.Cos(theta);

            // 实际轮廓 = 理论轮廓 - 滚子半径 * 法线方向
            x = tx - rr * nx;
            y = ty - rr * ny;
        }

        /// <summary>
        /// 生成完整的凸轮轮廓数据
        /// </summary>
        /// <param name="steps">运动阶段列表</param>
        /// <param name="rb">基圆半径 (mm)</param>
        /// <param name="e">偏距 (mm)</param>
        /// <param name="omega">角速度 (rad/s)</param>
        /// <param name="angleStep">角度步长（度）</param>
        /// <returns>凸轮轮廓数据</returns>
        public CamProfileData GenerateCamProfile(List<CamStep> steps, double rb, double e,
            double omega, double angleStep = 1.0)
        {
            var profile = new CamProfileData
            {
                BaseCircleRadius = rb,
                Offset = e,
                AngularVelocity = omega
            };

            double maxPressureAngle = 0;
            double maxDisplacement = 0;
            double maxVelocity = 0;
            double maxAcceleration = 0;
            double maxDsDtheta = 0;

            foreach (var step in steps)
            {
                if (step.MotionLaw == MotionLaw.Stop)
                {
                    // 停止阶段 - 记录起止点
                    double sStart = step.HStart;
                    double sEnd = step.HEnd;

                    profile.Points.Add(new CamProfilePoint
                    {
                        Theta = step.StartAngle,
                        Displacement = sStart,
                        Velocity = 0,
                        Acceleration = 0,
                        PressureAngle = 0
                    });

                    profile.Points.Add(new CamProfilePoint
                    {
                        Theta = step.EndAngle,
                        Displacement = sEnd,
                        Velocity = 0,
                        Acceleration = 0,
                        PressureAngle = 0
                    });
                    continue;
                }

                double betaRad = step.AngleRange * PI / 180.0;
                double h = step.Stroke;
                double hStart = step.HStart;

                int numPoints = Math.Max(2, (int)(step.AngleRange / angleStep) + 1);

                for (int i = 0; i <= numPoints; i++)
                {
                    double angleDeg = step.StartAngle + i * step.AngleRange / numPoints;
                    double thetaRad = (angleDeg - step.StartAngle) * PI / 180.0;

                    double s = hStart + CalcDisplacement(step.MotionLaw, thetaRad, betaRad, h);
                    double ds_dtheta = CalcDsDtheta(step.MotionLaw, thetaRad, betaRad, h);
                    double v = ds_dtheta * omega;
                    double a = CalcD2sDtheta2(step.MotionLaw, thetaRad, betaRad, h) * omega * omega;
                    double alpha = CalcPressureAngle(ds_dtheta, e, rb, s);

                    double thetaAll = angleDeg * PI / 180.0;
                    double profX, profY;
                    CalcTheoreticalProfile(rb, s, thetaAll, e, out profX, out profY);

                    profile.Points.Add(new CamProfilePoint
                    {
                        Theta = angleDeg,
                        Displacement = s,
                        Velocity = v,
                        Acceleration = a,
                        PressureAngle = alpha,
                        TheoreticalX = profX,
                        TheoreticalY = profY,
                        X = profX,
                        Y = profY
                    });

                    if (Math.Abs(alpha) > maxPressureAngle)
                        maxPressureAngle = Math.Abs(alpha);
                    if (Math.Abs(s) > maxDisplacement)
                        maxDisplacement = Math.Abs(s);
                    if (Math.Abs(v) > maxVelocity)
                        maxVelocity = Math.Abs(v);
                    if (Math.Abs(a) > maxAcceleration)
                        maxAcceleration = Math.Abs(a);
                    if (Math.Abs(ds_dtheta) > maxDsDtheta)
                        maxDsDtheta = Math.Abs(ds_dtheta);
                }
            }

            profile.MaxPressureAngle = maxPressureAngle;
            profile.MaxDisplacement = maxDisplacement;
            profile.MaxVelocity = maxVelocity;
            profile.MaxAcceleration = maxAcceleration;
            profile.MaxDsDtheta = maxDsDtheta;

            return profile;
        }

        #endregion

        #region 圆柱凸轮计算

        /// <summary>
        /// 计算圆柱凸轮的展开轮廓
        /// 圆柱凸轮展开为平面后，相当于移动凸轮
        /// x = R * theta (展开角度), y = s (位移)
        /// </summary>
        /// <param name="cylinderRadius">圆柱半径 (mm)</param>
        /// <param name="s">位移 (mm)</param>
        /// <param name="theta">凸轮转角（弧度）</param>
        /// <param name="x">展开后的 X 坐标</param>
        /// <param name="y">展开后的 Y 坐标</param>
        public void CalcCylindricalProfile(double cylinderRadius, double s, double theta,
            out double x, out double y)
        {
            x = cylinderRadius * theta;
            y = s;
        }

        /// <summary>
        /// 计算圆柱凸轮的压力角
        /// alpha = arctan(ds/dx) = arctan(ds/(R*dtheta))
        /// </summary>
        /// <param name="ds_dtheta">位移对角度的导数</param>
        /// <param name="cylinderRadius">圆柱半径 (mm)</param>
        /// <returns>压力角（度）</returns>
        public double CalcCylindricalPressureAngle(double ds_dtheta, double cylinderRadius)
        {
            if (Math.Abs(cylinderRadius) < 1e-10)
                return 90.0;

            return Math.Atan(ds_dtheta / cylinderRadius) * 180.0 / PI;
        }

        #endregion

        #region 凸轮槽宽计算

        /// <summary>
        /// 计算沟槽凸轮的槽宽
        /// W = 2 * rr + delta
        /// 其中 rr 为滚子半径，delta 为间隙
        /// </summary>
        /// <param name="rollerRadius">滚子半径 (mm)</param>
        /// <param name="clearance">配合间隙 (mm)</param>
        /// <returns>槽宽 (mm)</returns>
        public double CalcGrooveWidth(double rollerRadius, double clearance = 0.1)
        {
            return 2.0 * rollerRadius + clearance;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static double DegToRad(double degrees)
        {
            return degrees * PI / 180.0;
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static double RadToDeg(double radians)
        {
            return radians * 180.0 / PI;
        }

        /// <summary>
        /// 角速度换算: rpm -> rad/s
        /// </summary>
        public static double RpmToRadPerSec(double rpm)
        {
            return rpm * PI / 30.0;
        }

        /// <summary>
        /// 角速度换算: rad/s -> rpm
        /// </summary>
        public static double RadPerSecToRpm(double radPerSec)
        {
            return radPerSec * 30.0 / PI;
        }

        #endregion
    }
}
