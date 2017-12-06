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
/// 利用模拟的几何数据加载出点和线
/// </summary>
public class SimpleGenerate : WireFrameGenerater
{
    public WFData wfData;
    public override bool CanCreate(Clamp clamp)
    {
        return true;
    }
    protected override WFData GenerateWFData(Clamp clamp)
    {
        return GetTestData();
    } 

    public static WFData GetTestData()
    {
        var data = new WFData();
        data.wfNodes = new List<WFNode>();
        var node1 = new WFNode(Vector3.zero);
        data.wfNodes.Add(node1);
        var node2 = new WFNode(Vector3.one);
        data.wfNodes.Add(node2);

        data.wfBars = new List<WFBar>();
        var bar = new WFBar(node1.m_id, node2.m_id);
        data.wfBars.Add(bar);
        return data;
    }

    protected override WFData GenerateWFDataUnit(Clamp clamp)
    {
        throw new NotImplementedException();
    }
}
