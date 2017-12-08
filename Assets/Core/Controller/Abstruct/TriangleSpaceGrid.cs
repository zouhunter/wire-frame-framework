using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.Scripting;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.Assertions.Comparers;
using System.Collections;
using System.Collections.Generic;

namespace WireFrame
{
    /// <summary>
    /// 三角锥网架
    /// </summary>
    public abstract class TriangleSpaceGrid : WireFrameGenerater
    {
        public override bool CanCreate(Rule clamp)
        {
            if (clamp.doubleLayer && !CanDouble) return false;
            if (clamp.size1 < clamp.num1 || clamp.size2 < clamp.num2) return false;
            if (clamp.size1 < 1 || clamp.size2 < 1) return false;
            return true;
        }

        protected override WFData GenerateWFDataUnit(Rule clamp)
        {
            var num = clamp.num1;
            var unitSize = clamp.size1 / num;
            return CalcuteUtility.TrigonumSpaceGrid_Unit(unitSize, clamp.height);
        }
    }
}