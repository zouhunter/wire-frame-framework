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

public static class CalcuteUtility {
    public static WFData GetDefultData()
    {
        var data = new WFData();
        data.wfNodes = new List<WFNode>();
        var node1 = new WFNode(Vector3.zero);
        var id1 = node1.AddConnection();
        data.wfNodes.Add(node1);
        var node2 = new WFNode(Vector3.one);
        var id2 = node2.AddConnection();
        data.wfNodes.Add(node2);

        data.wfBars = new List<WFBar>();
        var bar = new WFBar();
        bar.m_fromNodeConnectionPointId = id1;
        bar.m_fromNodeId = node1.m_id;
        bar.m_toNodeConnectionPoiontId = id2;
        bar.m_toNodeId = node2.m_id;
        data.wfBars.Add(bar);
        return data;
    }
}
