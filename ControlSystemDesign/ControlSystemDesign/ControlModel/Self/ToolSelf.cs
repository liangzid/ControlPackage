using System;
using System.Collections.Generic;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace ControlSystemDesign.ControlModel.Self
{
    class ToolSelf
    {

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
        /// 自己写的小工具，用来实现增广矩阵的功能，此处的函数只支持double型数据
        /// axis==0为横向增广，axis==1为纵向增广
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static Matrix<double> MatrixZengGuang(Matrix<double> matrix1, Matrix<double> matrix2, int axis)
        {
            if (axis == 0)
            {
                Matrix<double> matrix = Matrix<double>.Build.Dense(matrix1.RowCount, matrix1.ColumnCount + matrix2.ColumnCount);
                for (int i = 0; i < matrix1.RowCount; i++)
                    for (int j = 0; j < matrix1.ColumnCount + matrix2.ColumnCount; j++)
                    {
                        if (j < matrix1.ColumnCount)
                            matrix[i, j] = matrix1[i, j];
                        else
                            matrix[i, j] = matrix2[i, j - matrix1.ColumnCount];
                    }
                return matrix;
            }
            else if (axis == 1)
            {
                Matrix<double> matrix = Matrix<double>.Build.Dense(matrix1.RowCount + matrix2.RowCount, matrix1.ColumnCount);
                for (int i = 0; i < matrix1.RowCount + matrix2.RowCount; i++)
                    for (int j = 0; j < matrix1.ColumnCount; j++)
                    {
                        if (i < matrix1.RowCount)
                            matrix[i, j] = matrix1[i, j];
                        else
                            matrix[i, j] = matrix2[i - matrix1.RowCount, j];
                    }
                return matrix;
            }
            else
            {
                Console.WriteLine("错误的输入尺度。矩阵只能在行或者列的方向上进行增广");

                throw new ExceptionSelf.WrongInputArgument("输入尺度错误");
                return Matrix<double>.Build.Dense(1, 1, 0);
            }
        }

        /// <summary>
        /// 方阵的乘方运算
        /// </summary>
        /// <param name="A"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Matrix<double> PowerSelf(Matrix<double> A, int n)
        {
            if (A.RowCount != A.ColumnCount)
            {
                Console.WriteLine("错误！矩阵不是方针，无法进行乘方运算！");
                //此处需要抛出一个异常
            }

            var AA = A;
            for (int i = 0; i < n - 1; i++)
            {
                AA = AA * A;
            }
            return AA;
        }

    }
}
