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
    public class OrthonormalFourAnglePyramidMeshFrame : WireFrameGenerater
    {
        public override bool CanCreate(Rule clamp)
        {
            return true;
        }

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
                    WFData data = CalcuteUtility.QuadrangularGridFrame_Unit(x_Size, y_Size, clamp.height);
                    data.AppendPosition(startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward);
                    topNodes[i, j] = data.wfNodes.Find(x => x.type == NodePosType.taperedTop);
                    wfData.InsertData(data);
                }
            }
            var downData = CalcuteUtility.ConnectNeerBy(topNodes, BarPosType.downBar);
            wfData.InsertData(downData);

            return wfData;
        }

        protected override WFData GenerateWFDataUnit(Rule clamp)
        {
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;
            return CalcuteUtility.QuadrangularGridFrame_Unit(x_Size, y_Size, clamp.height);
        }

        public override List<Vector3> CalcFulcrumPos(Rule clamp)
        {
            var startPos = -new Vector3(clamp.size1, -clamp.height, clamp.size2) * 0.5f;
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;

            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < clamp.num1; i++)
            {
                for (int j = 0; j < clamp.num2; j++)
                {
                    switch (clamp.fulcrumType)
                    {
                        case FulcrumType.upBound:
                            CalcuteUtility.RecordQuadBound(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, positions);
                            break;
                        case FulcrumType.downBound:
                            if(clamp.layer == 1)
                            {
                                CalcuteUtility.RecordQuadrAngular(i,j, clamp.num1, clamp.num2, startPos, x_Size, y_Size,clamp.height, positions);
                            }
                            else if(clamp.layer == 2)
                            {
                                CalcuteUtility.RecordQuadBound(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, positions);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }

            if(clamp.layer == 2 && clamp.fulcrumType == FulcrumType.downBound)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                   positions[i] =  DoubleLayerPos(positions[i],clamp.height);
                }
            }
            return positions;
        }

     
    }
}