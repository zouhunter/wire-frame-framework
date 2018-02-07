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
namespace WireFrame
{

    /// <summary>
    /// 正交正放四角锥型
    /// </summary>
    public class OrthonormalFourAnglePyramidMeshFrame : RectangularSquareSpaceGrid
    {
        protected override WFData GenerateWFData(FrameRule clamp)
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