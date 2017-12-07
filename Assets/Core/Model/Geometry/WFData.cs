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
    /// [网架的几何数据]
    /// 用于生成三维模型或线条模型
    /// </summary>
    [System.Serializable]
    public class WFData
    {
        public List<WFNode> wfNodes;//节点信息
        public List<WFBar> wfBars;//连接信息
        public WFData()
        {
            wfNodes = new List<WFNode>();
            wfBars = new List<WFBar>();
        }

        public WFData Copy()
        {
            var newData = new WFData();
            var idDic = new Dictionary<string, string>();
            for (int i = 0; i < wfNodes.Count; i++)
            {
                var newNode = wfNodes[i].Copy();
                newData.wfNodes.Add(newNode);
                idDic.Add(wfNodes[i].m_id, newNode.m_id);
            }
            for (int i = 0; i < wfBars.Count; i++)
            {
                var newBar = wfBars[i].Copy();
                if (idDic.ContainsKey(newBar.m_fromNodeId))
                {
                    newBar.m_fromNodeId = idDic[newBar.m_fromNodeId];
                }
                if (idDic.ContainsKey(newBar.m_toNodeId))
                {
                    newBar.m_toNodeId = idDic[newBar.m_toNodeId];
                }
                newData.wfBars.Add(newBar);
            }
            return newData;
        }

        internal void InsertData(WFData data)
        {
            Dictionary<string, string> guidChanged = new Dictionary<string, string>();
            foreach (var item in data.wfNodes)
            {
                var same = wfNodes.Find(x => Vector3.Distance(x.position, item.position) < 0.1f);
                if (same != null)
                {
                    guidChanged.Add(item.m_id, same.m_id);
                }
                else
                {
                    wfNodes.Add(item);
                }
            }
            foreach (var item in data.wfBars)
            {
                bool formNodeSame = guidChanged.ContainsKey(item.m_fromNodeId);
                bool toNodeSame = guidChanged.ContainsKey(item.m_toNodeId);

                if (formNodeSame && !toNodeSame)
                {
                    var newBar = item.Copy();
                    newBar.m_fromNodeId = guidChanged[item.m_fromNodeId];
                    wfBars.Add(newBar);
                }
                else if (!formNodeSame && toNodeSame)
                {
                    var newBar = item.Copy();
                    newBar.m_toNodeId = guidChanged[item.m_toNodeId];
                    wfBars.Add(newBar);
                }
                else if (!formNodeSame && !toNodeSame)
                {
                    wfBars.Add(item);
                }
                else
                {
                    var newBar = item.Copy();
                    newBar.m_fromNodeId = guidChanged[item.m_fromNodeId];
                    newBar.m_toNodeId = guidChanged[item.m_toNodeId];
                    if (wfBars.Find(x => x.IsSame(newBar)) == null)
                    {
                        wfBars.Add(newBar);
                    }
                }
            }
        }

        internal void AppendRotation(Quaternion rotate)
        {
            for (int i = 0; i < wfNodes.Count; i++)
            {
                wfNodes[i].position = rotate * wfNodes[i].initposition;
            }
        }

        internal void AppendPosition(Vector3 pos)
        {
            for (int i = 0; i < wfNodes.Count; i++)
            {
                wfNodes[i].position += pos;
            }
        }
    }
}