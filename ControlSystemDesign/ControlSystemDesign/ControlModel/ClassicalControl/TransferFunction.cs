using System;
using System.Collections.Generic;
using System.Text;

using MathNet.Numerics.LinearAlgebra;


namespace ControlSystemDesign.ControlModel.ClassicalControl
{
    /// <summary>
    /// 传递函数模型系统
    /// </summary>
    class TransferFunction:ControlModel
    {
        private double[,,] Den;//分母
        private double[,,] Num;//分子
        private double[,] TimeDelay;
        private double Ts;

        /// <summary>
        /// 默认构造出来的传递函数矩阵为1*1的单位传递函数，传函为1，即不做任何变换.
        /// 属于连续系统
        /// 无时滞
        /// </summary>
        public TransferFunction()
        {
            Den[1,1,1] = 1.0;
            Num[1,1,1] = 1.0;
            TimeDelay[1,1] = 1.0;
            Ts = 0.0;
        }


        /// <summary>
        /// 根据传递函数矩阵中各个元素分子分母从高阶到低阶排列的顺序向量来确定传递函数矩阵的表达式
        /// </summary>
        /// <param name="den"></param>
        /// <param name="num"></param>
        public TransferFunction(double[,,] den,double[,,] num)
        {
            for(int i=0; i<den.GetLength(0);i++)
                for(int j=0;j<den.GetLength(1);j++)
                    for(int k=0;k<den.GetLength(2);k++)
                    {
                        Den[i,j,k]= den[i,j,k];
                        Num[i,j,k]= num[i,j,k];
                    }

            TimeDelay[0,0] = 0.0;
            Ts = 0.0;

        }

        /// <summary>
        /// 根据传递函数矩阵中各个元素分子分母从高阶到低阶排列的顺序向量 时滞 采样周期 来确定传递函数矩阵的表达式
        /// </summary>
        /// <param name="den"></param>
        /// <param name="num"></param>
        /// <param name="timeDelay"></param>
        /// <param name="ts"></param>
        public TransferFunction(double[,,]den,double[,,]num,double[,]timeDelay,double ts)
        {
            for (int i = 0; i < den.GetLength(0); i++)
                for (int j = 0; j < den.GetLength(1); j++)
                    for (int k = 0; k < den.GetLength(2); k++)
                    {
                        Den[i,j,k] = den[i,j,k];
                        Num[i,j,k] = num[i,j,k];
                    }
            for(int i=0;i<timeDelay.GetLength(0);i++)
                for(int j=0;j<timeDelay.GetLength(1);j++)
                {
                    TimeDelay[i,j] = timeDelay[i,j];
                }
            Ts = ts;
        }

        /// <summary>
        /// 判断系统是否稳定的函数，必须被描述
        /// </summary>
        /// <returns></returns>
        public override bool IsStable()
        {
            var poles = GetPole();
            foreach(var pole in poles)
            {
                if (pole.Real >= 0)//如果实部大于0，则系统不稳定
                    return false;
            }
            //如果所有极点都小于0
            return true;
        }

        /// <summary>
        /// 获取系统的所有极点，必须被描述
        /// </summary>
        /// <returns></returns>
        public override Vector<MathNet.Numerics.Complex32> GetPole()
        {
            //求解高阶线性方程组的数值根

            //未实现
            return Vector<MathNet.Numerics.Complex32>.Build.Dense(1, 0);
        }

        /// <summary>
        /// 返回系统的时滞系数，必须被描述
        /// </summary>
        /// <returns></returns>
        public override double GetTimeDelay()
        {
            //这里有点问题
            return TimeDelay[0,0];
        }

        /// <summary>
        /// 判断系统连续或离散，必须被描述
        /// </summary>
        /// <returns></returns>
        public override bool IsContinous()
        {
            if (Ts - 0 == 0.0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取系统的采样时间，如果是连续系统，返回值为0
        /// </summary>
        /// <returns></returns>
        public override double GetTs()
        {
            return Ts;
        }

        /// <summary>
        /// 打印当前模型的所有信息
        /// </summary>
        public override void PrintModel()
        {

            //这里需要重写一下以更加美好的展示出来传递函数的形式
            Console.WriteLine("该模型的信息如下：");
            Console.WriteLine("该系统的");
            Console.WriteLine("这一部分需要重写");
            //throw new NotImplementedException();
        }




        /// <summary>
        /// 方法：实现将两个系统进行并联，返回并联之后的系统
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public override ControlModel ParallelConnection(ControlModel aa, ControlModel bb)
        {
            var a = (TransferFunction)aa;//拆箱操作
            var b = (TransferFunction)bb;
            int highestOrder = Math.Max(a.Den.GetLength(2), b.Den.GetLength(2));
            //本质上就是一个输入可以进入到两个子模型的输入中，两个子模型的输出为总的输出，即行不变，列相加
            double[,,] den=new double[a.Den.GetLength(0),a.Den.GetLength(1)+b.Den.GetLength(1),highestOrder];
            double[,,] num=new double[a.Den.GetLength(0), a.Den.GetLength(1) + b.Den.GetLength(1), highestOrder];
            double[,] TimeDelay = new double[a.Den.GetLength(0), a.Den.GetLength(1) + b.Den.GetLength(1)];

            for (int i=0;i<a.Den.GetLength(0);i++)
                for (int j = 0; j < a.Den.GetLength(1) + b.Den.GetLength(1); j++)
                    for (int k = 0; k < Math.Max(a.Den.GetLength(2),b.Den.GetLength(2)); k++)
                    {
                       
                        if(j<a.Den.GetLength(1))
                        {
                            //if (a.Den.GetLength(2) == highestOrder)
                            //    den[i, j, k] = a.Den[i, j, k];
                            //else
                            den[i, j, k] = a.Den[i, j, k];//哈哈，后来换了一种想法

                            num[i, j, k] = a.Num[i, j, k];
                            TimeDelay[i, j] = a.TimeDelay[i, j];
                        }
                        else
                        {
                            den[i,j,k]=b.Den[i, j - a.Den.GetLength(1), k];
                            num[i, j, k] = b.Den[i, j - a.Den.GetLength(1), k];
                            TimeDelay[i, j] = b.TimeDelay[i, j - a.Den.GetLength(1)];
                        }
                    }
            if(a.Ts!=b.Ts)
            {
                //Console.WriteLine("错误！两个并联系统需要有相同的采样时间,否则无法进行合并");

                //抛出自定义的异常
                throw new Self.ExceptionSelf.NotTheSameModelException("错误！两个并联系统需要有相同的采样时间,否则无法进行合并");
            }
            double Ts= a.Ts;

            return new TransferFunction(den, num, TimeDelay, Ts);
        }

        /// <summary>
        /// 方法：实现将两个系统进行串联，返回串联之后的系统
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public override ControlModel SeriesConnectioin(ControlModel a, ControlModel b)
        {
            //此处暂时不先写。做法是作克罗内克积来获得相对应的系数，以得到结果
            return new TransferFunction();
        }

        /// <summary>
        /// 方法：实现将两个系统进行反馈连接，返回之后得到的系统
        /// 其中：a为正向通道系统模型，b为反向通道系统模型，isPositive检测是否为正反馈
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="isPositive"></param>
        /// <returns></returns>
        public override ControlModel FeedbackConnection(ControlModel a, ControlModel b, bool isPositive)
        {
            int cc = isPositive ? (-1) : (+1);//负反馈使用1进行运算，正反馈使用-1进行运算

            //下面使用传递函数矩阵的反馈来完成
            return new TransferFunction();

        }
        
    }
}
