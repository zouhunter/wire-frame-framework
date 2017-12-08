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
    /// 正交斜放桁架型网架
    /// </summary>
    public class OrthonormalTrussedTrussTypeSpaceGrid : ObliqueTrussTypeSpaceGrid
    {
        protected override WFData GenerateWFData(Rule clamp)
        {
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;
            var startPos = -new Vector3(clamp.size1, clamp.height, clamp.size2 - y_Size) * 0.5f;
            WFData wfData = new WFData();

            var bundNodes = new List<WFNode>();

            for (int i = 0; i < clamp.num1; i++)
            {
                for (int j = 0; j < clamp.num2; j++)
                {
                    WFData data = CalcuteUtility.TrussTypeDiamondSpaceGrid_Unit(x_Size, y_Size, clamp.height);
                    var position = startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward;
                    data.AppendPosition(position);
                    wfData.InsertData(data);

                    if (i == 0)//左
                    {
                        var nodes = data.wfNodes.FindAll(x => IsPointSmilarity(x.position, position));
                        bundNodes.AddRange(nodes);
                    }
                    if (j == 0)
                    {//下

                        var downPos = position + x_Size * Vector3.right * 0.5f - y_Size * Vector3.forward * 0.5f;
                        var nodes = data.wfNodes.FindAll(x => IsPointSmilarity(x.position, downPos));
                        bundNodes.AddRange(nodes);
                    }
                    if (i == clamp.num1 - 1)//右
                    {
                        var rightPos = position + x_Size * Vector3.right;
                        var nodes = data.wfNodes.FindAll(x => IsPointSmilarity(x.position, rightPos));
                        bundNodes.AddRange(nodes);
                    }
                    if (j == clamp.num2 - 1)//上
                    {
                        var upPos = position + x_Size * Vector3.right * 0.5f + y_Size * Vector3.forward * 0.5f;
                        var nodes = data.wfNodes.FindAll(x => IsPointSmilarity(x.position, upPos));
                        bundNodes.AddRange(nodes);
                    }
                }
            }

            var bundData = CalcuteUtility.ConnectNeerBy(bundNodes, Mathf.Sqrt(Mathf.Pow(x_Size, 2) + Mathf.Pow(y_Size, 2)), BarPosType.boundBar);
            wfData.InsertData(bundData);

            return wfData;
        }

        private bool IsPointSmilarity(Vector3 sourcePoint, Vector3 targetPoint)
        {
            if (Mathf.Abs(sourcePoint.x - targetPoint.x) > 0.1f) return false;
            if (Mathf.Abs(sourcePoint.z - targetPoint.z) > 0.1f) return false;
            return true;
        }
     
    }
}