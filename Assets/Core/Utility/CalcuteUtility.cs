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

public static class CalcuteUtility
{
    /// <summary>
    /// 生成一组正交正放单元信息
    /// </summary>
    /// <param name="clamp"></param>
    /// <returns></returns>
    internal static WFData TryGenerate_OrthonormalTrussTypeTrussTypeGridFrame_Unit(float x_Size, float y_Size, float height)
    {
        var wfData = new WFData();
        var node1 = new WFNode(new Vector3(0,0,0));
        var node2 = new WFNode(new Vector3(x_Size, 0,0));
        var node3 = new WFNode(new Vector3(x_Size, height, 0));
        var node4 = new WFNode(new Vector3(0, height, 0));
        var node5 = new WFNode(new Vector3(0,0, y_Size));
        var node6 = new WFNode(new Vector3(x_Size,0, y_Size));
        var node7 = new WFNode(new Vector3(x_Size, height, y_Size));
        var node8 = new WFNode(new Vector3(0, height, y_Size));

        wfData.wfNodes.Add(node1);
        wfData.wfNodes.Add(node2);
        wfData.wfNodes.Add(node3);
        wfData.wfNodes.Add(node4);

        wfData.wfNodes.Add(node5);
        wfData.wfNodes.Add(node6);
        wfData.wfNodes.Add(node7);
        wfData.wfNodes.Add(node8);


        wfData.wfBars.Add(new WFBar(node1.m_id,node2.m_id));//1-2
        wfData.wfBars.Add(new WFBar(node1.m_id,node4.m_id));//1-4
        wfData.wfBars.Add(new WFBar(node1.m_id,node5.m_id));//1-5
        wfData.wfBars.Add(new WFBar(node1.m_id,node8.m_id));//1-8
        wfData.wfBars.Add(new WFBar(node2.m_id,node3.m_id));//2-3
        wfData.wfBars.Add(new WFBar(node2.m_id,node4.m_id));//2-4
        wfData.wfBars.Add(new WFBar(node2.m_id,node6.m_id));//2-6
        wfData.wfBars.Add(new WFBar(node2.m_id,node7.m_id));//2-7
        wfData.wfBars.Add(new WFBar(node3.m_id,node4.m_id));//3-4
        wfData.wfBars.Add(new WFBar(node3.m_id,node7.m_id));//3-7
        wfData.wfBars.Add(new WFBar(node4.m_id,node8.m_id));//4-8
        wfData.wfBars.Add(new WFBar(node5.m_id,node6.m_id));//5-6
        wfData.wfBars.Add(new WFBar(node5.m_id,node8.m_id));//5-8
        wfData.wfBars.Add(new WFBar(node6.m_id,node7.m_id));//6-7
        wfData.wfBars.Add(new WFBar(node6.m_id,node8.m_id));//6-8
        wfData.wfBars.Add(new WFBar(node7.m_id,node8.m_id));//7-8
        return wfData;
    }
}
