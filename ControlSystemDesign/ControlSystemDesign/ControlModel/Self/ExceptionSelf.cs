using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystemDesign.ControlModel.Self
{
    /// <summary>
    /// 命名空间，里面存放着所有自定义的异常类
    /// </summary>
    namespace ExceptionSelf  
    {

        /// <summary>
        /// 当两个模型特性不一导致无法进行结合操作时需要抛出的异常
        /// </summary>
        class NotTheSameModelException:ArgumentException
        {
            /// <summary>
            /// 使用默认的构造函数即可
            /// </summary>
            public NotTheSameModelException() : base() { }

            /// <summary>
            /// 私用默认的构造函数即可
            /// </summary>
            /// <param name="s"></param>
            public NotTheSameModelException(string s) : base(s) { }
        }

        /// <summary>
        /// 当两个矩阵的行数和列数不匹配（比如矩阵相乘左边的列数不等于右边的行数）时抛出此错误
        /// </summary>
        class MatrixNotTheSameRowCol:ApplicationException
        {
            /// <summary>
            /// 默认构造函数方式
            /// </summary>
            public MatrixNotTheSameRowCol() : base() { }

            /// <summary>
            /// 默认构造函数方式
            /// </summary>
            /// <param name="s"></param>
            public MatrixNotTheSameRowCol(string s) : base(s) { }
        }


        /// <summary>
        /// 输入参数格式或者范围错误异常
        /// </summary>
        class WrongInputArgument:ArgumentException
        {
            public WrongInputArgument() : base()
            {

            }

            public WrongInputArgument(string s):base(s)
            {
                //待补充，还未想好
            }
        }

        /// <summary>
        /// 错误的输入矩阵格式
        /// </summary>
        class WrongInputMatrix:WrongInputArgument
        {
            public WrongInputMatrix() : base() { }
            
            public WrongInputMatrix(string s):base(s)
            {
                
            }

        }

    }
}
