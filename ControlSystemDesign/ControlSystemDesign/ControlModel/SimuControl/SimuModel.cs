using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

/// <summary>
/// 使用线性代数工具库
/// </summary>
using MathNet.Numerics.LinearAlgebra;


namespace ControlSystemDesign.ControlModel.SimuControl
{
    class SimuModel:ISimu
    {

        private ArrayList Input;        //ISimu 输入
        private ArrayList Output;       //ISimu 输出
        private ArrayList Submodules;   //SubSimuModel 子模型
        private ArrayList Link;         //连接关系

        /// <summary>
        /// 默认构造函数，生成一个空SimuModel模型
        /// </summary>
        public SimuModel()
        {
            Input = new ArrayList();
            Output = new ArrayList();
            Submodules = new ArrayList();
            Link = new ArrayList();
        }

        /// <summary>
        /// 简单的单输入单输出且均为double类型时的构造函数构造
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="submodules"></param>
        /// <param name="link"></param>
        public SimuModel(double input,double output,ArrayList submodules,ArrayList link)
        {
            Input = new ArrayList();
            Output = new ArrayList();
            Submodules = new ArrayList();
            Link = new ArrayList();

            Input.Add(input);
            Output.Add(output);
            Submodules = (ArrayList) submodules.Clone();
            Link = (ArrayList)link.Clone();
        }

        /// <summary>
        /// 最寻常的构造函数初始化过程
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="submodeules"></param>
        /// <param name="link"></param>
        public SimuModel(ArrayList input,ArrayList output,ArrayList submodeules,ArrayList link)
        {
            Input = new ArrayList();
            Output = new ArrayList();
            Submodules = new ArrayList();
            Link = new ArrayList();

            Input = (ArrayList)input.Clone();
            Output = (ArrayList)output.Clone();
            Submodules = (ArrayList)submodeules.Clone();
            Link = (ArrayList)link.Clone();
        }

        /// <summary>
        /// 通过加载一个已经存储的SimuModel来实现构造函数初始化
        /// </summary>
        /// <param name="path"></param>
        public SimuModel(string path)
        {
            ;//此处还没有进行过实现
        }

        /// <summary>
        /// 返回加载的模型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static SimuModel load(string path)
        {
            return new SimuModel(path);
        }









        string ISimu.GetInfo()
        {
            throw new NotImplementedException();
        }

        string ISimu.GetMainTag()
        {
            throw new NotImplementedException();
        }

        SubSimuModel ISimu.GetModel()
        {
            throw new NotImplementedException();
        }

        ArrayList ISimu.GetSubTags()
        {
            throw new NotImplementedException();
        }

        ArrayList ISimu.GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
