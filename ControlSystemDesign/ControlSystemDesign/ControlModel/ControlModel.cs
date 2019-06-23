using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace ControlSystemDesign.ControlModel
{

    /// <summary>
    /// 控制系统模型
    /// 这个是广义上的控制系统模型，在该模型下，需要满足最基本的控制系统的要求
    /// 该类是两个类的基类，对应的派生类为传递函数模型类和状态空间模型类
    /// 本系统并不能用来实例化对象，事实上用其来实例化对象也毫无用处
    ///                             LiangZid 2019.6.20
    /// </summary>
    abstract class ControlModel
    {
        /// <summary>
        /// 判断系统是否稳定的函数，必须被描述
        /// </summary>
        /// <returns>
        /// 布尔变量，描述是否稳定
        /// </returns>
        public abstract bool IsStable();

        /// <summary>
        /// 获取系统的所有极点，必须被描述
        /// </summary>
        /// <returns></returns>
        public abstract Vector<MathNet.Numerics.Complex32> GetPole();

        /// <summary>
        /// 返回系统的时滞系数，必须被描述
        /// </summary>
        /// <returns></returns>
        public abstract double GetTimeDelay();

        /// <summary>
        /// 判断系统连续或离散，必须被描述
        /// </summary>
        /// <returns></returns>
        public abstract bool IsContinous();

        /// <summary>
        /// 获取系统的采样时间，如果是连续系统，返回值为0
        /// </summary>
        /// <returns></returns>
        public abstract double GetTs();

        /// <summary>
        /// 打印系统模型信息
        /// </summary>
        public virtual void PrintModel()
        {
            Console.WriteLine("这是一个控制系统模型！");
        }

        /// <summary>
        /// 方法：实现将两个系统进行并联，返回并联之后的系统
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public abstract ControlModel ParallelConnection(ControlModel a, ControlModel b);

        /// <summary>
        /// 方法：实现将两个系统进行串联，返回串联之后的系统
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public abstract ControlModel SeriesConnectioin(ControlModel a, ControlModel b);

        /// <summary>
        /// 方法：实现将两个系统进行反馈连接，返回之后得到的系统
        /// 其中：a为正向通道系统模型，b为反向通道系统模型，isPositive检测是否为正反馈
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="isPositive"></param>
        /// <returns></returns>
        public abstract ControlModel FeedbackConnection(ControlModel a, ControlModel b, bool isPositive);

        /////////////////////////////////////这里是关于运算符重载的部分////////////////////////////////////////////
        ///由于运算符无法被abstract 修饰，所以此处暂且略去，作为接口进行实现。
    }
}
