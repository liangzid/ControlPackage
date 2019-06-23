using System;
using System.Collections;
using System.Text;

namespace ControlSystemDesign.ControlModel.SimuControl
{
    /// <summary>
    /// 接口类型，用来描述输入和输出特征
    /// </summary>
    interface ISimu
    {
        /// <summary>
        /// 返回目标输入输出的信息
        /// </summary>
        /// <returns></returns>
        string GetInfo();

        /// <summary>
        /// 得到目标地方的值
        /// </summary>
        /// <returns></returns>
        ArrayList GetValue();

        /// <summary>
        /// 返回对应的模型
        /// </summary>
        /// <returns></returns>
        SubSimuModel GetModel();

        /// <summary>
        /// 返回每个子模块独一无二的tag
        /// </summary>
        /// <returns></returns>
        string GetMainTag();

        /// <summary>
        /// 获取每个模块中输入输出对应的接口标签名字，在同一个模块中输出输入的标签名字都是确定的
        /// </summary>
        /// <returns></returns>
        ArrayList GetSubTags();

    }
}
