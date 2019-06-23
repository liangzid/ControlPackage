using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 引用线性代数运算库
/// </summary>
using MathNet.Numerics.LinearAlgebra;

/// <summary>
/// 引入自己写的工具包
/// </summary>
using ControlSystemDesign.ControlModel.Self;

namespace ControlSystemDesign.ControlModel.ModernControl
{
    class StatementSpace : ControlModel
    {
        ///该系统的ss模型为：
        ///diff(x)=A*x+B*u;
        ///y=C*x+D*u;
        private Matrix<double> A;
        private Matrix<double> B;
        private Matrix<double> C;
        private Matrix<double> D;
        private double Ts; //采样时间
        private double TimeDelay;//，这一部分代码暂时没有设计

        /// <summary>
        /// 什么也没有，这个就是用来初始化各个抽象矩阵以表明这是个状态方程类型的变量
        /// </summary>
        public StatementSpace()
        {
            ///
            A = Matrix<double>.Build.Dense(1, 1, 1.0);
            B = Matrix<double>.Build.Dense(1, 1, 0.0);
            C = Matrix<double>.Build.Dense(1, 1, 0.0);
            D = Matrix<double>.Build.Dense(1, 1, 0.0);
            Ts = 0;
            TimeDelay = 0;
        }
        /// <summary>
        /// 自治系统，x用来初始化A
        /// </summary>
        /// <param name="x"></param>
        public StatementSpace(Matrix<double> A)
        {
            B = Matrix<double>.Build.Dense(A.ColumnCount, 1, 0.0);
            C = Matrix<double>.Build.Dense(1, A.ColumnCount, 0.0);
            D = Matrix<double>.Build.Dense(1, 1, 0.0);
            Ts = 0.0;
            TimeDelay = 0;
        }

        /// <summary>
        /// 初始化所有参数，直接进行。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        public StatementSpace(Matrix<double> a,Matrix<double> b,Matrix<double> c,Matrix<double> d)
        {
            A = a;
            B = b;
            C = c;
            D = d;

            if(A.RowCount!=B.RowCount)
            {
                Console.WriteLine("错误！A矩阵行数{0}不等于B矩阵行数{1}", A.RowCount, B.RowCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            if(A.ColumnCount!=C.ColumnCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。A矩阵列数{0}不等于B矩阵列数{1}", A.ColumnCount, B.ColumnCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            if(D.RowCount!=C.RowCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。D矩阵行数{0}不等于C矩阵行数{1}", D.RowCount, C.RowCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            if (D.ColumnCount != B.ColumnCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。D矩阵列数{0}不等于B矩阵列数{1}", D.ColumnCount, B.ColumnCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            Ts = 0.0;
            TimeDelay = 0;
        }

        /// <summary>
        /// 史上最全的构造函数，初始化了全部参数，直接进行
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="timeDelay"></param>
        /// <param name="ts"></param>
        public StatementSpace(Matrix<double> a, Matrix<double> b, Matrix<double> c, Matrix<double> d,double timeDelay,double ts)
        {
            A = a;
            B = b;
            C = c;
            D = d;

            if (A.RowCount != B.RowCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。A矩阵行数{0}不等于B矩阵行数{1}", A.RowCount, B.RowCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            if (A.ColumnCount != C.ColumnCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。A矩阵列数{0}不等于B矩阵列数{1}", A.ColumnCount, B.ColumnCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            if (D.RowCount != C.RowCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。D矩阵行数{0}不等于C矩阵行数{1}", D.RowCount, C.RowCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            if (D.ColumnCount != B.ColumnCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。D矩阵列数{0}不等于B矩阵列数{1}", D.ColumnCount, B.ColumnCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }


            TimeDelay = timeDelay;
            Ts = ts;
            
        }

        /// <summary>
        /// 构造函数其中一种，默认直接传递项为0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public StatementSpace(Matrix<double> a, Matrix<double> b, Matrix<double> c)
        {
            A = a;
            B = b;
            C = c;
            D = Matrix<double>.Build.Dense(C.RowCount, B.ColumnCount,0.0);

            ///执行是否错误的诊断
            if (A.RowCount != B.RowCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。A矩阵行数{0}不等于B矩阵行数{1}", A.RowCount, B.RowCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            if (A.ColumnCount != C.ColumnCount)
            {
                Console.WriteLine("错误。矩阵尺度不一致。A矩阵列数{0}不等于B矩阵列数{1}", A.ColumnCount, B.ColumnCount);
                throw new Self.ExceptionSelf.MatrixNotTheSameRowCol("矩阵尺度不一致");
            }
            Ts = 0;
            TimeDelay = 0;
        }

        /// <summary>
        /// 传递函数模型转化为状态空间模型
        /// </summary>
        /// <param name="a"></param>
        public StatementSpace(ClassicalControl.TransferFunction a)
        {
            //时间紧张，此部分暂且跳过
            ;
        }

        /// <summary>
        /// 判断系统是否稳定的函数，必须被描述
        /// </summary>
        /// <returns></returns>
        public override bool IsStable()
        {
            return true;


        }

        /// <summary>
        /// 获取系统的所有极点，必须被描述
        /// </summary>
        /// <returns></returns>
        public override Vector<MathNet.Numerics.Complex32> GetPole()
        {
            //此处需要一个求取特征值的函数
            return Vector<MathNet.Numerics.Complex32>.Build.Dense(1, new MathNet.Numerics.Complex32(-2, 2));
        }

        /// <summary>
        /// 返回系统的时滞系数，必须被描述
        /// </summary>
        /// <returns></returns>
        public override double GetTimeDelay()
        {
            return TimeDelay;
        }

        /// <summary>
        /// 判断系统连续或离散，必须被描述
        /// </summary>
        /// <returns></returns>
        public override bool IsContinous()
        {
            return (Ts - 0 == 0.0) ? true : false;
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
        /// 用来输出或者打印状态方程信息
        /// </summary>
        public override void PrintModel()
        {
            Console.WriteLine("状态方程可以转化为：");
            Console.WriteLine("A:\n{0}", A.ToString());
            Console.WriteLine("B:\n{0}", B.ToString());
            Console.WriteLine("C:\n{0}", C.ToString());
            Console.WriteLine("D:\n{0}", D.ToString());
            if((Ts-0)==0.0)
            {
                Console.WriteLine("该系统为连续系统.");
            }
            else
            {
                Console.WriteLine("该系统为离散系统，其采样周期为：" + Ts.ToString());
            }
            Console.Write("\n");
        }

        /// <summary>
        /// 方法：实现将两个系统进行并联，返回并联之后的系统
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public override ControlModel ParallelConnection(ControlModel aa, ControlModel bb)
        {
            var a = (StatementSpace)aa;//拆箱操作
            var b = (StatementSpace)bb;

            //初始化
            var A = Matrix<double>.Build.Dense(a.A.RowCount + b.A.RowCount, a.A.RowCount + b.A.RowCount, 0);
            var B = Matrix<double>.Build.Dense(a.A.RowCount + b.A.RowCount, a.B.ColumnCount,0);
            var C = Matrix<double>.Build.Dense(a.C.RowCount, a.A.RowCount + b.A.RowCount, 0);
            var D = Matrix<double>.Build.Dense(a.C.RowCount, a.C.ColumnCount);

            //赋值操作
            for(int i=0;i<A.RowCount;i++)
                for(int j=0;j<A.ColumnCount;j++)
                {
                    if (i < a.A.RowCount && j < a.A.ColumnCount)
                    {
                        A[i, j] = a.A[i, j];
                    }
                    else if (i > a.A.RowCount&&j>a.A.ColumnCount)
                    {
                        A[i, j] = b.A[i - a.A.RowCount, j - a.A.ColumnCount];

                    }
                }

            for (int i=0;i<B.RowCount;i++)
                for(int j=0;j<B.ColumnCount;j++)
                {
                    if(i<a.B.RowCount)
                    {
                        B[i, j] = a.B[i, j];
                    }
                    else
                    {
                        B[i, j] = b.B[i - a.B.RowCount, j];

                    }
                }

            for(int i=0;i<C.RowCount;i++)
                for(int j=0;j<C.ColumnCount;j++)
                {
                    if(j<a.C.ColumnCount)
                    {
                        C[i, j] = a.C[i, j];
                    }
                    else
                    {
                        C[i, j] = b.C[i, j - a.C.ColumnCount];
                    }
                }

            D = a.D + b.D;

            return new StatementSpace(A, B, C, D,a.TimeDelay,a.Ts);
        }

        /// <summary>
        /// 方法：实现将两个系统进行串联，返回串联之后的系统
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public override ControlModel SeriesConnectioin(ControlModel aa, ControlModel bb)
        {
            //拆箱操作
            var a = (StatementSpace)aa;
            var b = (StatementSpace)bb;

            //在此处定义新的ABCD矩阵
            Matrix<double> A = Matrix<double>.Build.Dense(a.A.RowCount + b.A.RowCount, a.A.ColumnCount + b.A.ColumnCount,0.0);
            Matrix<double> B = Matrix<double>.Build.Dense(a.B.RowCount + b.B.RowCount, a.B.ColumnCount,0.0);
            Matrix<double> C = Matrix<double>.Build.Dense(a.C.RowCount, a.C.ColumnCount + b.C.ColumnCount,0.0);
            Matrix<double> D = Matrix<double>.Build.Dense(b.D.RowCount, a.D.ColumnCount,0.0);

            for(int i=0;i<a.A.RowCount+b.A.RowCount;i++)
                for(int j=0;j<a.A.ColumnCount+b.A.ColumnCount;j++)
                {
                    if(i<a.A.RowCount)
                    {
                        if(j<a.A.ColumnCount)
                        {
                            A[i, j] = a.A[i, j];
                        }
                        else
                        {
                            A[i, j] = 0;
                        }
                    }
                    else
                    {
                        if (j < a.A.ColumnCount)
                        {
                            A[i, j] = (b.B * a.C)[i-a.A.RowCount, j];
                        }
                        else
                        {
                            A[i, j] = b.A[i - a.A.RowCount, j - a.A.ColumnCount];
                        }
                    }
                }

            for(int i=0;i< a.B.RowCount + b.B.RowCount;i++)
                for(int j=0;i<a.B.ColumnCount;j++)
                {
                    if(i< a.B.RowCount)
                    {
                        B[i, j] = a.B[i, j];
                    }
                    else
                    {
                        B[i, j] = (b.B * a.D)[i - a.B.RowCount, j];
                    }
                }

            for(int i=0;i<a.C.RowCount;i++)
                for(int j=0;j< a.C.ColumnCount + b.C.ColumnCount;j++)
                {
                    if(j<a.C.ColumnCount)
                    {
                        C[i, j] = (b.D * a.C)[i, j];

                    }
                    else
                    {
                        C[i, j] = b.C[i, j - a.C.ColumnCount];
                    }
                }

            for(int i=0;i<b.D.RowCount;i++)
                for(int j=0;j<a.D.ColumnCount;j++)
                {
                    D[i, j] = (b.D * a.D)[i, j];
                }

            Ts = a.Ts;
            TimeDelay = a.TimeDelay;//这一行此处有问题，以后补充

            return new StatementSpace(A, B, C, D, TimeDelay, Ts);

        }

        public ControlModel FeedbackConnection(ControlModel aa,ControlModel bb)
        {
            bool isPositive = false;
            return FeedbackConnection(aa, bb, isPositive);
        }
        
        /// <summary>
        /// 方法：实现将两个系统进行反馈连接，返回之后得到的系统
        /// 其中：a为正向通道系统模型，b为反向通道系统模型，isPositive检测是否为正反馈
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="isPositive"></param>
        /// <returns></returns>
        public override ControlModel FeedbackConnection(ControlModel aa, ControlModel bb, bool isPositive)
        {
            var a = (StatementSpace)aa;
            var b = (StatementSpace)bb;//拆箱操作

            int i = isPositive ? (-1) : 1;//正反馈则为-1,负反馈则为1
            
            ///此处还欠缺两个模型的尺度检查这个问题。

            if(!isPositive)
            {
                //计算出来合适的分块矩阵形式
                var A11 = a.A - a.B * 
                    ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.D * a.C;
                var A12 = -a.B * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.C;
                var A21 = b.B * a.C- b.B * a.D * 
                    ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.D * a.C;
                var A22 = b.A - b.B * a.D * 
                    ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.C;

                var B1 = a.B * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse());
                var B2 = b.B * a.D * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse());

                var C1 = a.C - a.D * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse())
                    * b.D * a.C;
                var C2 = -a.D * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.C;

                //计算出ABCD四个矩阵
                var A = Matrix<double>.Build.Dense(A11.RowCount + A21.RowCount, A11.ColumnCount + A12.ColumnCount, 0);
                var B = Matrix<double>.Build.Dense(B1.RowCount + B2.RowCount, B1.ColumnCount,0.0);
                var C = Matrix<double>.Build.Dense(C1.RowCount, C1.ColumnCount + C2.ColumnCount,0.0);
                var D = Matrix<double>.Build.Dense(a.D.RowCount, a.D.ColumnCount,0.0);
                
                //分块矩阵赋值，得到系统矩阵A
                for(i=0;i<A.RowCount; i++)
                    for(int j=0;j<A.ColumnCount;j++)
                    {
                        if(i<A11.RowCount)
                        {
                            if(j<A11.ColumnCount)
                            {
                                A[i, j] = A11[i, j];
                            }
                            else
                            {
                                A[i, j] = A12[i, j - A11.ColumnCount];
                            }
                        }
                        else
                        {
                            if (j < A11.ColumnCount)
                            {
                                A[i, j] = A21[i - A11.RowCount, j];
                            }
                            else
                            {
                                A[i, j] = A22[i - A11.RowCount, j - A11.ColumnCount];
                            }

                        }
                    }

                //分块矩阵赋值操作，得到输入（控制）矩阵B
                for(i=0;i<B.RowCount;i++)
                    for(int j=0;j<B.ColumnCount;j++)
                    {
                        if(i<B1.RowCount)
                        {
                            B[i, j] = B1[i, j];
                        }
                        else
                        {
                            B[i, j] = B2[i - B1.RowCount, j];
                        }
                    }

                //分块矩阵赋值操作，得到输出矩阵C
                for(i=0;i<C.RowCount;i++)
                    for(int j=0;j<C.ColumnCount;j++)
                    {
                        if(j<C1.ColumnCount)
                        {
                            C[i, j] = C1[i, j];
                        }
                        else
                        {
                            C[i, j] = C2[i, j - C1.ColumnCount];

                        }
                    }

                D = a.D * (Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse();

                return new StatementSpace(A, B, C, D, a.TimeDelay, a.Ts);
            }
            else//正反馈，这一部分还没有写
            {
                Console.WriteLine("Warning！这一部分还没有被写就！默认按照负反馈进行！\n");
                //计算出来合适的分块矩阵形式
                var A11 = a.A - a.B *
                    ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.D * a.C;
                var A12 = -a.B * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.C;
                var A21 = b.B * a.C - b.B * a.D *
                    ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.D * a.C;
                var A22 = b.A - b.B * a.D *
                    ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.C;

                var B1 = a.B * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse());
                var B2 = b.B * a.D * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse());

                var C1 = a.C - a.D * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse())
                    * b.D * a.C;
                var C2 = -a.D * ((Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse()) * b.C;

                //计算出ABCD四个矩阵
                var A = Matrix<double>.Build.Dense(A11.RowCount + A21.RowCount, A11.ColumnCount + A12.ColumnCount, 0);
                var B = Matrix<double>.Build.Dense(B1.RowCount + B2.RowCount, B1.ColumnCount, 0.0);
                var C = Matrix<double>.Build.Dense(C1.RowCount, C1.ColumnCount + C2.ColumnCount, 0.0);
                var D = Matrix<double>.Build.Dense(a.D.RowCount, a.D.ColumnCount, 0.0);

                //分块矩阵赋值，得到系统矩阵A
                for (i = 0; i < A.RowCount; i++)
                    for (int j = 0; j < A.ColumnCount; j++)
                    {
                        if (i < A11.RowCount)
                        {
                            if (j < A11.ColumnCount)
                            {
                                A[i, j] = A11[i, j];
                            }
                            else
                            {
                                A[i, j] = A12[i, j - A11.ColumnCount];
                            }
                        }
                        else
                        {
                            if (j < A11.ColumnCount)
                            {
                                A[i, j] = A21[i - A11.RowCount, j];
                            }
                            else
                            {
                                A[i, j] = A22[i - A11.RowCount, j - A11.ColumnCount];
                            }

                        }
                    }

                //分块矩阵赋值操作，得到输入（控制）矩阵B
                for (i = 0; i < B.RowCount; i++)
                    for (int j = 0; j < B.ColumnCount; j++)
                    {
                        if (i < B1.RowCount)
                        {
                            B[i, j] = B1[i, j];
                        }
                        else
                        {
                            B[i, j] = B2[i - B1.RowCount, j];
                        }
                    }

                //分块矩阵赋值操作，得到输出矩阵C
                for (i = 0; i < C.RowCount; i++)
                    for (int j = 0; j < C.ColumnCount; j++)
                    {
                        if (j < C1.ColumnCount)
                        {
                            C[i, j] = C1[i, j];
                        }
                        else
                        {
                            C[i, j] = C2[i, j - C1.ColumnCount];

                        }
                    }

                D = a.D * (Matrix<double>.Build.DiagonalIdentity(b.D.RowCount) + b.D * a.D).Inverse();

                return new StatementSpace(A, B, C, D, a.TimeDelay, a.Ts);
            }
        }

        ///// <summary>
        ///// 实现矩阵的增广功能的函数，返回一个增光的数值 C#无法实现泛型编程，所以这里就只能写一个自己用到的但是通用意义不算特别强的
        ///// 这里面的axis是进行增广的尺度，如果为0，表示横向增广，如果为1，表示纵向增广。
        ///// 
        ///// </summary>
        //private static Matrix<T> MatrixZengGuang<T>(Matrix<T> matrix1, Matrix<T> matrix2, int axis)
        //{
        //    if (axis == 0)
        //    {
        //        Matrix<T> matrix = Matrix<T>.Build.Dense(matrix1.RowCount, matrix1.ColumnCount + matrix2.ColumnCount);
        //        for (int i = 0; i < matrix1.RowCount; i++)
        //            for (int j = 0; j < matrix1.ColumnCount + matrix2.ColumnCount; j++)
        //            {
        //                if (j < matrix1.ColumnCount)
        //                    matrix(i, j) = matrix1(i, j);
        //                else
        //                    matrix(i, j) = matrix2(i, j - matrix1.ColumnCount);
        //            }
        //        return matrix;
        //    }
        //    else if (axis == 1)
        //    {
        //        Matrix<T> matrix = Matrix<T>.Build.Dense(matrix1.RowCount + matrix2.RowCount, matrix1.ColumnCount);
        //        for (int i = 0; i < matrix1.RowCount + matrix2.RowCount; i++)
        //            for (int j = 0; j < matrix1.ColumnCount; j++)
        //            {
        //                if (i < matrix1.RowCount)
        //                    matrix(i, j) = matrix1(i, j);
        //                else
        //                    matrix(i, j) = matrix2(i - matrix1.RowCount, j);
        //            }
        //        return matrix;
        //    }
        //    else
        //    {
        //        Console.WriteLine("错误的输入尺度。矩阵只能在行或者列的方向上进行增广");
        //        //报出异常

        //    }
        //}

        /// <summary>
        /// 私有方法，用来得到能控标准型矩阵
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private Matrix<double> GetPBHOfQc(Matrix<double> A, Matrix<double> B,int n)
        {
            if(n<=0)
            {
                //Console.WriteLine("错误！错误的输入参数 n");
                throw new Self.ExceptionSelf.WrongInputArgument("输入参数：n 格式错误");
                //return Matrix<double>.Build.Dense(1, 1, -1);
            }
            else
            {
                if (n == 1)
                    return B;
                else
                    return ToolSelf.MatrixZengGuang(GetPBHOfQc(A, B, n - 2), ToolSelf.PowerSelf(A, n-1) * B, 0);
            }
            
        }

        /// <summary>
        /// 布尔变量，返回一个状态模型是否能控
        /// </summary>
        /// <returns></returns>
        public bool IsControllable()
        {
            var m = GetPBHOfQc(A, B, A.RowCount);
            if(m.Rank()==A.RowCount)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到秩判据中的状态观测增广矩阵
        /// </summary>
        /// <param name="A"></param>
        /// <param name="C"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static Matrix<double> GetPBHOfQo(Matrix<double> A,Matrix<double> C,int n)
        {
            if (n <= 0)
            {
                Console.WriteLine("错误！错误的输入参数 n");
                throw new Self.ExceptionSelf.WrongInputArgument("输入参数：n 格式错误");
                //return Matrix<double>.Build.Dense(1, 1, -1);
            }
            else
            {
                if (n == 1)
                    return C;
                else
                    return ToolSelf.MatrixZengGuang(GetPBHOfQo(A, C, n - 2), C*ToolSelf.PowerSelf(A, n - 1), 1);
            }
        }

        /// <summary>
        /// 布尔变量，返回一个状态模型是否可观测
        /// </summary>
        /// <returns></returns>
        public bool IsObservable()
        {
            var m = GetPBHOfQo(A, C, A.ColumnCount);
            if(m.Rank()==A.ColumnCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 对输入向量x经过可逆变换P之后得到新的状态模型
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public StatementSpace TransformWithP_ReversibleStateMentModel(Matrix<double> P)
        {
            if(P.RowCount!=P.ColumnCount)
            {
                //Console.WriteLine("错误！输入的可逆矩阵必须是方阵！");
                throw new Self.ExceptionSelf.WrongInputMatrix("矩阵输入格式错误，P必须是方阵");
                //return new StatementSpace();
            }
            if(P.Rank()<P.RowCount)
            {
                //Console.WriteLine("错误！输入的矩阵P必须可逆！");
                throw new Self.ExceptionSelf.WrongInputMatrix("矩阵输入格式错误，P必须可逆");
                //return new StatementSpace();
            }

            //初始化新的特征矩阵
            var Anew = Matrix<double>.Build.Dense(A.RowCount, A.ColumnCount, 0.0);
            var Bnew = Matrix<double>.Build.Dense(B.RowCount, B.ColumnCount, 0.0);
            var Cnew = Matrix<double>.Build.Dense(C.RowCount, C.ColumnCount, 0.0);
            var Dnew = Matrix<double>.Build.Dense(D.RowCount, D.ColumnCount, 0.0);

            //计算和赋值
            Anew = P * A * P.Inverse();
            Bnew = P * B;
            Cnew = C * P.Inverse();
            Dnew = D;

            return new StatementSpace(Anew, Bnew, Cnew, Dnew, TimeDelay, Ts);

        }

        /// <summary>
        /// 运算符重载，实现两个模型并联的操作
        /// </summary>
        /// <param name="aa"></param>
        /// <param name="BB"></param>
        /// <returns></returns>
        public static  StatementSpace operator + (StatementSpace a,StatementSpace b)
        {
            //初始化
            var A = Matrix<double>.Build.Dense(a.A.RowCount + b.A.RowCount, a.A.RowCount + b.A.RowCount, 0);
            var B = Matrix<double>.Build.Dense(a.A.RowCount + b.A.RowCount, a.B.ColumnCount, 0);
            var C = Matrix<double>.Build.Dense(a.C.RowCount, a.A.RowCount + b.A.RowCount, 0);
            var D = Matrix<double>.Build.Dense(a.C.RowCount, a.C.ColumnCount);

            //赋值操作
            for (int i = 0; i < A.RowCount; i++)
                for (int j = 0; j < A.ColumnCount; j++)
                {
                    if (i < a.A.RowCount && j < a.A.ColumnCount)
                    {
                        A[i, j] = a.A[i, j];
                    }
                    else if (i > a.A.RowCount && j > a.A.ColumnCount)
                    {
                        A[i, j] = b.A[i - a.A.RowCount, j - a.A.ColumnCount];

                    }
                }

            for (int i = 0; i < B.RowCount; i++)
                for (int j = 0; j < B.ColumnCount; j++)
                {
                    if (i < a.B.RowCount)
                    {
                        B[i, j] = a.B[i, j];
                    }
                    else
                    {
                        B[i, j] = b.B[i - a.B.RowCount, j];

                    }
                }

            for (int i = 0; i < C.RowCount; i++)
                for (int j = 0; j < C.ColumnCount; j++)
                {
                    if (j < a.C.ColumnCount)
                    {
                        C[i, j] = a.C[i, j];
                    }
                    else
                    {
                        C[i, j] = b.C[i, j - a.C.ColumnCount];
                    }
                }

            D = a.D + b.D;

            return new StatementSpace(A, B, C, D, a.TimeDelay, a.Ts);
        }

        /// <summary>
        /// 实现两个状态空间模型的级联
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static StatementSpace operator * (StatementSpace a,StatementSpace b)
        {
            //在此处定义新的ABCD矩阵
            Matrix<double> A = Matrix<double>.Build.Dense(a.A.RowCount + b.A.RowCount, a.A.ColumnCount + b.A.ColumnCount, 0.0);
            Matrix<double> B = Matrix<double>.Build.Dense(a.B.RowCount + b.B.RowCount, a.B.ColumnCount, 0.0);
            Matrix<double> C = Matrix<double>.Build.Dense(a.C.RowCount, a.C.ColumnCount + b.C.ColumnCount, 0.0);
            Matrix<double> D = Matrix<double>.Build.Dense(b.D.RowCount, a.D.ColumnCount, 0.0);

            for (int i = 0; i < a.A.RowCount + b.A.RowCount; i++)
                for (int j = 0; j < a.A.ColumnCount + b.A.ColumnCount; j++)
                {
                    if (i < a.A.RowCount)
                    {
                        if (j < a.A.ColumnCount)
                        {
                            A[i, j] = a.A[i, j];
                        }
                        else
                        {
                            A[i, j] = 0;
                        }
                    }
                    else
                    {
                        if (j < a.A.ColumnCount)
                        {
                            A[i, j] = (b.B * a.C)[i - a.A.RowCount, j];
                        }
                        else
                        {
                            A[i, j] = b.A[i - a.A.RowCount, j - a.A.ColumnCount];
                        }
                    }
                }

            for (int i = 0; i < a.B.RowCount + b.B.RowCount; i++)
                for (int j = 0; i < a.B.ColumnCount; j++)
                {
                    if (i < a.B.RowCount)
                    {
                        B[i, j] = a.B[i, j];
                    }
                    else
                    {
                        B[i, j] = (b.B * a.D)[i - a.B.RowCount, j];
                    }
                }

            for (int i = 0; i < a.C.RowCount; i++)
                for (int j = 0; j < a.C.ColumnCount + b.C.ColumnCount; j++)
                {
                    if (j < a.C.ColumnCount)
                    {
                        C[i, j] = (b.D * a.C)[i, j];

                    }
                    else
                    {
                        C[i, j] = b.C[i, j - a.C.ColumnCount];
                    }
                }

            for (int i = 0; i < b.D.RowCount; i++)
                for (int j = 0; j < a.D.ColumnCount; j++)
                {
                    D[i, j] = (b.D * a.D)[i, j];
                }

            var Ts = a.Ts;
            var TimeDelay = a.TimeDelay;//这一行此处有问题，以后补充

            return new StatementSpace(A, B, C, D, TimeDelay, Ts);
        }


        /// <summary>
        /// 定义相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator == (StatementSpace a,StatementSpace b)
        {
            bool equalA = (a.A == b.A);
            bool equalB = (a.B == b.B);
            bool equalC = (a.C == b.C);
            bool equalD = (a.D == b.D);

            bool equalTimeDelay = (a.TimeDelay == b.TimeDelay);
            bool equalTs = (a.Ts == b.Ts);
            if(equalA&&equalB&&equalC&&equalD&&equalTimeDelay&&equalTs)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 定义不相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator != (StatementSpace a,StatementSpace b)
        {
            return (!(a == b));
        }





    }
}
