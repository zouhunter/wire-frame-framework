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
/// 斜置四角锥网架
/// </summary>
public class ObliqueFourAnglePyramidGridFrame : WireFrameGenerater
{
    public override bool CanCreate(Clamp clamp)
    {
        return true;
    }

    protected override WFData GenerateWFData(Clamp clamp)
    {
        throw new NotImplementedException();
    }
}
