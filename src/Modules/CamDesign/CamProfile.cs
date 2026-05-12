using System;
using System.Collections.Generic;

namespace CamDesign
{
    /// <summary>
    /// 凸轮轮廓数据点
    /// </summary>
    public class CamProfilePoint
    {
        /// <summary>
        /// 角度位置（度或弧度，取决于上下文）
        /// </summary>
        public double Theta { get; set; }

        /// <summary>
        /// 位移值
        /// </summary>
        public double Displacement { get; set; }

        /// <summary>
        /// 速度值
        /// </summary>
        public double Velocity { get; set; }

        /// <summary>
        /// 加速度值
        /// </summary>
        public double Acceleration { get; set; }

        /// <summary>
        /// 压力角（度）
        /// </summary>
        public double PressureAngle { get; set; }

        /// <summary>
        /// 凸轮轮廓 X 坐标
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 凸轮轮廓 Y 坐标
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 理论轮廓 X 坐标
        /// </summary>
        public double TheoreticalX { get; set; }

        /// <summary>
        /// 理论轮廓 Y 坐标
        /// </summary>
        public double TheoreticalY { get; set; }
    }

    /// <summary>
    /// 凸轮运动阶段
    /// </summary>
    public class CamStep
    {
        /// <summary>
        /// 阶段名称（推程/回程/停止等）
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 起始角度（度）
        /// </summary>
        public double StartAngle { get; set; }

        /// <summary>
        /// 终止角度（度）
        /// </summary>
        public double EndAngle { get; set; }

        /// <summary>
        /// 起始位移（mm）
        /// </summary>
        public double HStart { get; set; }

        /// <summary>
        /// 终止位移（mm）
        /// </summary>
        public double HEnd { get; set; }

        /// <summary>
        /// 该阶段的角度范围（度）
        /// </summary>
        public double AngleRange
        {
            get { return EndAngle - StartAngle; }
        }

        /// <summary>
        /// 该阶段的位移量（mm）
        /// </summary>
        public double Stroke
        {
            get { return HEnd - HStart; }
        }

        /// <summary>
        /// 运动规律类型
        /// </summary>
        public MotionLaw MotionLaw { get; set; }

        /// <summary>
        /// 计算得到的数据点集合 [角度, 位移]
        /// </summary>
        public double[,] Points { get; set; }

        /// <summary>
        /// 下一阶段
        /// </summary>
        public CamStep Next { get; set; }

        /// <summary>
        /// 上一阶段
        /// </summary>
        public CamStep Previous { get; set; }

        public CamStep()
        {
            MotionLaw = MotionLaw.ConstantVelocity;
        }

        public CamStep(string name, double startAngle, double endAngle, double hStart, double hEnd, MotionLaw law)
        {
            StepName = name;
            StartAngle = startAngle;
            EndAngle = endAngle;
            HStart = hStart;
            HEnd = hEnd;
            MotionLaw = law;
        }
    }

    /// <summary>
    /// 凸轮轮廓数据 - 存储计算后的完整轮廓信息
    /// </summary>
    public class CamProfileData
    {
        /// <summary>
        /// 轮廓数据点列表
        /// </summary>
        public List<CamProfilePoint> Points { get; set; }

        /// <summary>
        /// 基圆半径 (mm)
        /// </summary>
        public double BaseCircleRadius { get; set; }

        /// <summary>
        /// 从动件偏距 (mm)
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// 凸轮转速 (rad/s)
        /// </summary>
        public double AngularVelocity { get; set; }

        /// <summary>
        /// 最大压力角（度）
        /// </summary>
        public double MaxPressureAngle { get; set; }

        /// <summary>
        /// 最大位移 (mm)
        /// </summary>
        public double MaxDisplacement { get; set; }

        /// <summary>
        /// 最大速度
        /// </summary>
        public double MaxVelocity { get; set; }

        /// <summary>
        /// 最大加速度
        /// </summary>
        public double MaxAcceleration { get; set; }

        /// <summary>
        /// 最大 ds/dtheta
        /// </summary>
        public double MaxDsDtheta { get; set; }

        /// <summary>
        /// 凸轮机构类型
        /// </summary>
        public CamType CamType { get; set; }

        /// <summary>
        /// 从动件类型
        /// </summary>
        public FollowerType FollowerType { get; set; }

        public CamProfileData()
        {
            Points = new List<CamProfilePoint>();
        }
    }
}
