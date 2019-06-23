using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace ControlSystemDesign.ControlModel.SimuControl
{
    /// <summary>
    /// simuControl中对子模块进行描述的类
    /// 子模块的类仅仅支持输入和输出运算，对模型类下的一些功能进行了抽象
    /// 
    /// </summary>
    class SubSimuModel:ISimu
    {
        private ArrayList Input;
        private ArrayList Output;
        private ArrayList InterValues;



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
