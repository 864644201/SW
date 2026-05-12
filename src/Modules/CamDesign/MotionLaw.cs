namespace CamDesign
{
    /// <summary>
    /// 凸轮从动件运动规律类型
    /// </summary>
    public enum MotionLaw
    {
        /// <summary>
        /// 等速运动（直线）- s = h * theta / beta
        /// </summary>
        ConstantVelocity = 0,

        /// <summary>
        /// 等加速等减速运动（抛物线）
        /// 加速段: s = 2h * theta^2 / beta^2
        /// 减速段: s = h - 2h * (beta - theta)^2 / beta^2
        /// </summary>
        ConstantAcceleration = 1,

        /// <summary>
        /// 余弦加速度运动（简谐运动）
        /// s = h/2 * (1 - cos(pi * theta / beta))
        /// </summary>
        CosineAcceleration = 2,

        /// <summary>
        /// 正弦加速度运动（摆线运动）
        /// s = h * (theta/beta - sin(2*pi*theta/beta) / (2*pi))
        /// </summary>
        SineAcceleration = 3,

        /// <summary>
        /// 五次多项式运动
        /// s = h * (10*(theta/beta)^3 - 15*(theta/beta)^4 + 6*(theta/beta)^5)
        /// </summary>
        FifthOrderPolynomial = 4,

        /// <summary>
        /// 改进梯形加速度（摆线-抛物线-摆线）
        /// 组合正弦和等加速运动
        /// </summary>
        ModifiedTrapezoidal = 5,

        /// <summary>
        /// 改进正弦加速度
        /// </summary>
        ModifiedSine = 6,

        /// <summary>
        /// 停止阶段 - 无运动
        /// </summary>
        Stop = 99
    }

    /// <summary>
    /// 凸轮机构类型
    /// </summary>
    public enum CamType
    {
        /// <summary>
        /// 盘形凸轮（平面转动凸轮）
        /// </summary>
        DiscCam = 0,

        /// <summary>
        /// 移动凸轮（平面往复运动凸轮）
        /// </summary>
        TranslatingCam = 1,

        /// <summary>
        /// 圆柱凸轮（空间圆柱凸轮）
        /// </summary>
        CylindricalCam = 2,

        /// <summary>
        /// 盘形沟槽凸轮
        /// </summary>
        GrooveDiscCam = 3,

        /// <summary>
        /// 端面凸轮（空间端面凸轮）
        /// </summary>
        EndFaceCam = 4
    }

    /// <summary>
    /// 从动件类型
    /// </summary>
    public enum FollowerType
    {
        /// <summary>
        /// 对心直动从动件
        /// </summary>
        InLineTranslating = 0,

        /// <summary>
        /// 偏置直动从动件
        /// </summary>
        OffsetTranslating = 1,

        /// <summary>
        /// 摆动从动件
        /// </summary>
        Oscillating = 2
    }

    /// <summary>
    /// 凸轮运动阶段名称
    /// </summary>
    public enum StepPhase
    {
        /// <summary>
        /// 推程（升程）
        /// </summary>
        Rise = 0,

        /// <summary>
        /// 远休止
        /// </summary>
        FarDwell = 1,

        /// <summary>
        /// 回程
        /// </summary>
        Return = 2,

        /// <summary>
        /// 近休止
        /// </summary>
        NearDwell = 3
    }
}
