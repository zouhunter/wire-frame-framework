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
    /// 三角锥网架(正六边形)
    /// </summary>
    public class TriangularPyramidSpaceGrid : TriangleSpaceGrid
    {
        public override bool CanDouble
        {
            get
            {
                return false;
            }
        }
        protected override WFData GenerateWFData(FrameRule clamp)
        {
            var data = new WFData();
            var num = clamp.num1;
            var size = clamp.size1;
            var unitSize = clamp.size1 / num;
            var unitHeight = Mathf.Sin(Mathf.Deg2Rad * 60) * unitSize;
            var startPos = new Vector3(-size * 0.5f, clamp.height * 0.5f, -Mathf.Sin(Mathf.Deg2Rad * 60) * size);

            var boundNodes = new List<WFNode>();
            var topNodes = new List<WFNode>();

            for (int i = -num; i < num; i++)
            {
                for (int j = 0; j < 2 * num - Mathf.Abs(i + 1); j++)
                {
                    var pos = startPos +
                        (j * unitSize - 0.5f * unitSize * (num - Mathf.Abs(i + 1))) * Vector3.right +
                        unitHeight * (i + num) * Vector3.forward;
                    var tdata = CalcuteUtility.TrigonumSpaceGrid_Unit(unitSize, clamp.height);
                    tdata.AppendPosition(pos);
                    data.InsertData(tdata);
                    //记录顶点用于连线
                    topNodes.Add(tdata.wfNodes.Find(x => x.type == NodePosType.taperedTop));
                    //记录边界节点用于连线
                    RecordBoundNode(i, j, num, tdata, pos, unitSize, unitHeight, boundNodes);
                }
            }

            var topData = CalcuteUtility.ConnectNeerBy(topNodes, unitSize, BarPosType.downBar, BoundConnectType.NoRule);
            data.InsertData(topData);

            var bundData = CalcuteUtility.ConnectNeerBy(boundNodes, unitSize, BarPosType.upBar, BoundConnectType.NoRule);
            data.InsertData(bundData);

            return data;
        }

        public override List<WFFul> CalcFulcrumPos(FrameRule clamp)
        {
            ///以下这个方法只实现了一种
            //clamp.fulcrumType
            //也只实现了一层

            var positions = new List<WFFul>();
            var num = clamp.num1;
            var size = clamp.size1;
            var unitSize = clamp.size1 / num;
            var unitHeight = Mathf.Sin(Mathf.Deg2Rad * 60) * unitSize;
            var startPos = new Vector3(-size * 0.5f, clamp.height * 0.5f, -Mathf.Sin(Mathf.Deg2Rad * 60) * size);

            for (int i = -num; i < num; i++)
            {
                for (int j = 0; j < 2 * num - Mathf.Abs(i + 1); j++)
                {

                    switch (clamp.fulcrumType)
                    {
                        case FulcrumType.upBound:
                            CalcuteUtility.RecordSixBound(i, j, num, startPos, unitSize, unitHeight, positions, clamp.fulcrumType);
                            break;
                        case FulcrumType.downBound:
                                CalcuteUtility.RecordSixBoundAngular(i, j, num, startPos, unitSize, unitHeight, clamp.height, positions, clamp.fulcrumType);
                            break;
                        default:
                            break;
                    }


                }
            }

         
            return positions;
        }

        private void RecordBoundNode(int i, int j, int num, WFData tdata, Vector3 pos, float unitSize, float unitHeight, List<WFNode> boundNodes)
        {
            if (i == -num)
            {
                var downNode = tdata.wfNodes.Find(x => CalcuteUtility.IsSimulatePos(x.position, pos));
                boundNodes.Add(downNode);
            }
            if (j == 0 && i >= -1)
            {
                var leftPos = pos - unitSize * 0.5f * Vector3.right + unitHeight * Vector3.forward;
                var leftNode = tdata.wfNodes.Find(x => CalcuteUtility.IsSimulatePos(x.position, leftPos));
                boundNodes.Add(leftNode);
            }
            if (j == 2 * num - Mathf.Abs(i + 1) - 1 && i >= -1)
            {
                var rightPos = pos + unitSize * 0.5f * Vector3.right + unitHeight * Vector3.forward;
                var rightNode = tdata.wfNodes.Find(x => CalcuteUtility.IsSimulatePos(x.position, rightPos));
                boundNodes.Add(rightNode);
            }
        }
    }
}