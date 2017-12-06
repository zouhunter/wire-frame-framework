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
/// <summary>
/// 三层抽空四角锥网架
/// </summary>
public class ThreeLayerFourAnglePyramidGridFrame: FourAnglePyramidSpaceTrussGridFrame
{
    protected override WFData GenerateWFData(Clamp clamp)
    {
        var oldData = base.GenerateWFData(clamp);
        var wfData = new WFData();
        var oldCopy = oldData.Copy();
        oldCopy.AppendRotation(Quaternion.Euler(new Vector3(180, 0, 0)));
        oldCopy.AppendPosition(new Vector3(0, -clamp.height, 0));
        wfData.InsertData(oldData.Copy());
        wfData.InsertData(oldCopy);
        return wfData;
    }
}
