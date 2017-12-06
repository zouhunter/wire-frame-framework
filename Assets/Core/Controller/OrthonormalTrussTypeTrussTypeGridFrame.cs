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
/// 正交正放桁架型网架
/// </summary>
public class OrthonormalTrussTypeTrussTypeGridFrame : WireFrameGenerater
{
    public override bool CanCreate(Clamp clamp)
    {
        return true;
    }
    protected override WFData GenerateWFData(Clamp clamp)
    {
        var startPos = -new Vector3(clamp.x_Size, clamp.height, clamp.y_Size) * 0.5f;
        WFData wfData = new WFData();
        float x_Size = clamp.x_Size / clamp.x_num;
        float y_Size = clamp.y_Size / clamp.y_num;
        for (int i = 0; i < clamp.x_num; i++)
        {
            for (int j = 0; j < clamp.y_num; j++)
            {
                WFData data = CalcuteUtility.TrussTypeGridFrame_Unit(x_Size, y_Size, clamp.height);
                data.SetPosition(startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward);
                wfData.InsertData(data);
            }
        }

        return wfData;
    }

    protected override WFData GenerateWFDataUnit(Clamp clamp)
    {
        throw new NotImplementedException();
    }
}
