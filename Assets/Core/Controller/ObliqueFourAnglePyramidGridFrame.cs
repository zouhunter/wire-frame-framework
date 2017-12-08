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
namespace WireFrame
{

    public class ObliqueFourAnglePyramidGridFrame : WireFrameGenerater
    {
        public override bool CanCreate(Rule clamp)
        {
            return true;
        }

        protected override WFData GenerateWFData(Rule clamp)
        {
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;
            var startPos = -new Vector3(clamp.size1, -clamp.height, clamp.size2 - y_Size) * 0.5f;
            WFData wfData = new WFData();


            WFNode[,] topNodes = new WFNode[clamp.num1, clamp.num2];
            List<WFNode> bundNodes = new List<WFNode>();
            List<WFNode> istNodes = new List<WFNode>();
            for (int i = 0; i < clamp.num1; i++)
            {
                for (int j = 0; j < clamp.num2; j++)
                {
                    WFData data = CalcuteUtility.QuadDiamondGridFrame_Unit(x_Size, y_Size, clamp.height);
                    var position = startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward;
                    data.AppendPosition(position);
                    topNodes[i, j] = (data.wfNodes.Find(x => x.type == NodePosType.taperedTop));
                    wfData.InsertData(data);
                    istNodes.Add(topNodes[i, j]);

                    if (i > 0 && i < clamp.num1 - 1 && j < clamp.num2 - 1)//插入一个单元格
                    {
                        var istPos = position - x_Size * 0.5f * Vector3.right + y_Size * 0.5f * Vector3.forward;
                        var istData = CalcuteUtility.QuadDiamondGridFrame_Unit(x_Size, y_Size, clamp.height);
                        istData.AppendPosition(istPos);
                        istNodes.Add(istData.wfNodes.Find(x => x.type == NodePosType.taperedTop));
                        wfData.InsertData(istData);
                    }

                    if (i == 0)//左
                    {
                        var node = data.wfNodes.Find(x => Vector3.Distance(x.position, position) < 0.1f);
                        bundNodes.Add(node);
                    }
                    if (j == 0)//下
                    {

                        var downPos = position + x_Size * Vector3.right * 0.5f - y_Size * Vector3.forward * 0.5f;
                        var node = data.wfNodes.Find(x => Vector3.Distance(x.position, downPos) < 0.1f);
                        bundNodes.Add(node);
                    }
                    if (i == clamp.num1 - 1)//右
                    {
                        var rightPos = position + x_Size * Vector3.right;
                        var node = data.wfNodes.Find(x => Vector3.Distance(x.position, rightPos) < 0.1f);
                        bundNodes.Add(node);
                    }
                    if (j == clamp.num2 - 1)//上
                    {
                        var upPos = position + x_Size * Vector3.right * 0.5f + y_Size * Vector3.forward * 0.5f;
                        var node = data.wfNodes.Find(x => Vector3.Distance(x.position, upPos) < 0.1f);
                        bundNodes.Add(node);
                    }
                }
            }

            var downData = CalcuteUtility.ConnectNeerBy(topNodes, BarPosType.downBar);
            wfData.InsertData(downData);

            var insetData = CalcuteUtility.ConnectNeerBy(istNodes, Mathf.Sqrt(Mathf.Pow(x_Size, 2) + Mathf.Pow(y_Size, 2)), BarPosType.downBar, BoundConnectType.NoXAndYAxis);
            wfData.InsertData(insetData);

            var bundData = CalcuteUtility.ConnectNeerBy(bundNodes, Mathf.Sqrt(Mathf.Pow(x_Size, 2) + Mathf.Pow(y_Size, 2)), BarPosType.boundBar);
            wfData.InsertData(bundData);

            return wfData;
        }

        protected override WFData GenerateWFDataUnit(Rule clamp)
        {
            throw new NotImplementedException();
        }
        public override List<Vector3> CalcFulcrumPos(Rule clamp)
        {
            return new List<Vector3>();
        }
    }
}