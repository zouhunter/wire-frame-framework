﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件所做的更改。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace WireFrame
{

    /// <remarks>空间生成器</remarks>
    public interface IWireCreater
    {
        bool CanCreate(FrameRule clamp);
        IWire Unit(FrameRule clamp);
        List<WFFul> CalcFulcrumPos(FrameRule rule);
        IWire Create(FrameRule clamp);
    }

}