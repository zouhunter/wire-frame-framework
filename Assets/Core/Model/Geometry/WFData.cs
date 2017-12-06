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
            else if(!formNodeSame && toNodeSame)
            {
                var newBar = item.Copy();
                newBar.m_toNodeId = guidChanged[item.m_toNodeId];
                wfBars.Add(newBar);
            }
            else if(!formNodeSame && !toNodeSame)
            {
                wfBars.Add(item);
            }
            else
            {
                var newBar = item.Copy();
                newBar.m_fromNodeId = guidChanged[item.m_fromNodeId];
                newBar.m_toNodeId = guidChanged[item.m_toNodeId];
                if(wfBars.Find(x=>x.IsSame(newBar)) == null)
                {
                    wfBars.Add(newBar);
                }
            }
        }
    }

    internal void SetPosition(Vector3 startPos)
    {
        if (wfNodes.Count == 0) return;

        var oldStartPos = wfNodes[0].position;
        var appendPos = startPos - oldStartPos;

        for (int i = 0; i < wfNodes.Count; i++)
        {
            wfNodes[i].position += appendPos;
        }
    }
}
