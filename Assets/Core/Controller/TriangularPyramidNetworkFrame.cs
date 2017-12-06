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
using System;
/// <summary>
/// 三角锥网架
/// </summary>
public class TriangularPyramidNetworkFrame : WireFrameGenerater
{
    public override bool CanCreate(Clamp clamp)
    {
        return true;
    }

    protected override WFData GenerateWFData(Clamp clamp)
    {
        return CalcuteUtility.TrigonumGridFrame_Unit(5,5);
    }
}
