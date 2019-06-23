using System;
///使用线性代数库
using MathNet.Numerics.LinearAlgebra;
/// <summary>
/// 使用自己定义的库
/// </summary>
using ControlSystemDesign.ControlModel.ClassicalControl;
using ControlSystemDesign.ControlModel.ModernControl;
using ControlSystemDesign.ControlModel.Self;
using ControlSystemDesign.ControlModel.SimuControl;


namespace ControlSystemDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            //定义一个状态空间模型
            double[,] Aarray = { { 0, 1, 0 }, { 0, 0, 1 }, { -1, -2, -3 } };
            double[,] Barray = { { 0 }, { 0 }, { 1 } };
            double[,] Carray = { { 0, 0, 1 } };
            double[,] cc = { { 1, 2, 3 }, { 0, 0, 1 }, { 0, 2, 0 } };
            //double[,] Aarray = { { 1 } };
            //double[,] Barray = { { 1 } };
            //double[,] Carray = { { 1 } };

            var A = Matrix<double>.Build.DenseOfArray(Aarray);
            var B = Matrix<double>.Build.DenseOfArray(Barray);
            var C = Matrix<double>.Build.DenseOfArray(Carray);
            var D = Matrix<double>.Build.Dense(1, 1, 0);

            //生成状态空间模型
            var ssmodel = new ControlModel.ModernControl.StatementSpace(A, B, C, D);

            //打印信息
            ssmodel.PrintModel();


            //打印基本属性
            //if (ssmodel.IsStable())
            //    Console.WriteLine("该模型是稳定的");
            if (ssmodel.IsContinous())
                Console.WriteLine("该模型是连续的");
            if (ssmodel.IsControllable())
                Console.WriteLine("该模型是可控的");
            else
                Console.WriteLine("该模型不可控");
            if (ssmodel.IsObservable())
                Console.WriteLine("该模型是可观测的");
            else
                Console.WriteLine("该模型不可观测");

            //实现对当前模型进行一个线性变换从而得到一个新的状态空间模型
            var P = Matrix<double>.Build.DenseOfArray(cc);
            var ssmodel2 = ssmodel.TransformWithP_ReversibleStateMentModel(P);
            Console.WriteLine("可逆变换---------------------------------------");
            ssmodel2.PrintModel();
            //实现对当前模型进行串联连接
            //var ssmodel3 = ssmodel.SeriesConnectioin(ssmodel,ssmodel2);
            //Console.WriteLine("串联连接---------------------------------------");
            //ssmodel3.PrintModel();

            //实现对当前模型进行并联连接
            var ssmodel4 = ssmodel.ParallelConnection(ssmodel2, ssmodel);
            Console.WriteLine("并联连接---------------------------------------");
            ssmodel4.PrintModel();

            //实现对当前模型的反馈连接
            var ssmodel5 = ssmodel.FeedbackConnection(ssmodel, ssmodel2);
            Console.WriteLine("反馈连接---------------------------------------");
            ssmodel5.PrintModel();






        }
    }
}
