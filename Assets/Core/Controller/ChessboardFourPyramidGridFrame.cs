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
namespace WireFrame
{

    /// <summary>
    /// 棋盘四角锥网架
    /// </summary>
    public class ChessboardFourPyramidGridFrame : WireFrameGenerater
    {

        public override bool CanCreate(Clamp clamp)
        {
            return true;
        }

        protected override WFData GenerateWFData(Clamp clamp)
        {
            var startPos = -new Vector3(clamp.x_Size, -clamp.height, clamp.y_Size) * 0.5f;
            WFData wfData = new WFData();
            float x_Size = clamp.x_Size / clamp.x_num;
            float y_Size = clamp.y_Size / clamp.y_num;

            var topNodes = new List<WFNode>();

            for (int i = 0; i < clamp.x_num; i++)
            {
                for (int j = 0; j < clamp.y_num; j++)
                {
                    if (i > 0 && i < clamp.x_num - 1 && j > 0 && j < clamp.y_num - 1 && (i + j) % 2 == 0)
                    {
                        continue;
                    }
                    WFData data = CalcuteUtility.QuadrangularGridFrame_Unit(x_Size, y_Size, clamp.height);
                    data.AppendPosition(startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward);
                    topNodes.Add(data.wfNodes.Find(x => x.type == NodePosType.taperedTop));
                    Debug.Log(data.wfNodes.Find(x => x.type == NodePosType.taperedTop).m_id);
                    wfData.InsertData(data);
                }
            }
            var downData = CalcuteUtility.ConnectNeerBy(topNodes, Mathf.Sqrt(Mathf.Pow(x_Size, 2) + Mathf.Pow(y_Size, 2)), BarPosType.downBar, BoundConnectType.NoRule);
            wfData.InsertData(downData);

            return wfData;
        }

        protected override WFData GenerateWFDataUnit(Clamp clamp)
        {
            return null;
        }
    }
}