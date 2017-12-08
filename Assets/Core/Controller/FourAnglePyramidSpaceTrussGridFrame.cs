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
/// 抽空四角锥网架
/// </summary>
namespace WireFrame
{

    public class FourAnglePyramidSpaceTrussSpaceGrid : RectangularSquareSpaceGrid
    {
        protected override WFData GenerateWFData(Rule clamp)
        {
            var startPos = -new Vector3(clamp.size1, -clamp.height, clamp.size2) * 0.5f;
            WFData wfData = new WFData();
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;

            WFNode[,] topNodes = new WFNode[clamp.num1, clamp.num2];

            for (int i = 0; i < clamp.num1; i++)
            {
                for (int j = 0; j < clamp.num2; j++)
                {
                    if (i > 0 && i < clamp.num1 - 1 && j > 0 && j < clamp.num2 - 1 && i % 2 != 0 && j % 2 != 0)
                    {
                        continue;
                    }
                    WFData data = CalcuteUtility.QuadrangularSpaceGrid_Unit(x_Size, y_Size, clamp.height);
                    data.AppendPosition(startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward);
                    topNodes[i, j] = data.wfNodes.Find(x => x.type == NodePosType.taperedTop);
                    wfData.InsertData(data);
                }
            }
            var downData = CalcuteUtility.ConnectNeerBy(topNodes, BarPosType.downBar);
            wfData.InsertData(downData);

            return wfData;
        }
    }
}
