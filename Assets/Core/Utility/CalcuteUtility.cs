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
    /// 生成一组[三角锥]单元信息
    /// 上面下角为原点
    /// </summary>
    /// <param name="x_Size"></param>
    /// <param name="y_Size"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    internal static WFData TrigonumGridFrame_Unit(float edge, float height)
    {
        var wfData = new WFData();
        var edgeHeight = edge * 0.5f * Mathf.Tan(Mathf.Deg2Rad * 60);
        var centr1 = edge * 0.5f / Mathf.Cos(Mathf.Deg2Rad * 30);
        var node1 = new WFNode(new Vector3(0, 0, 0));
        var node2 = new WFNode(new Vector3(edge * 0.5f, 0, edgeHeight));
        var node3 = new WFNode(new Vector3(-edge * 0.5f, 0, edgeHeight));
        var node4 = new WFNode(new Vector3(0, -height, centr1));

        wfData.wfNodes.Add(node1);
        wfData.wfNodes.Add(node2);
        wfData.wfNodes.Add(node3);
        wfData.wfNodes.Add(node4);

        wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id));//1-2
        wfData.wfBars.Add(new WFBar(node1.m_id, node3.m_id));//1-3
        wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id));//1-4
        wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id));//2-3
        wfData.wfBars.Add(new WFBar(node2.m_id, node4.m_id));//2-4
        wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id));//3-4
        return wfData;
    }

    /// <summary>
    /// 顺序连接(边界)
    /// </summary>
    /// <param name="bundNodes"></param>
    /// <param name="barType"></param>
    /// <returns></returns>
    internal static WFData ConnectNeerBy(List<WFNode> bundNodes, float distence, string barType)
    {
        var data = new WFData();
        for (int i = 0; i < bundNodes.Count; i++)
        {
            var node = bundNodes[i];
            data.wfNodes.Add(node);
            if(i < bundNodes.Count - 1)
            {
                for (int j = i + 1; j < bundNodes.Count; j++)
                {
                    if(Vector3.Distance(node.position, bundNodes[j].position) < distence)
                    {
                        data.wfBars.Add(new WFBar(node.m_id, bundNodes[j].m_id, barType));

                    }
                }
            }
        }
        return data;
    }

    /// <summary>
    /// 连接相邻点
    /// </summary>
    /// <param name="topNodes"></param>
    /// <returns></returns>
    internal static WFData ConnectNeerBy(WFNode[,] topNodes, string barType)
    {
        WFData data = new global::WFData();
        var xCount = topNodes.GetLength(0);
        var yCount = topNodes.GetLength(1);

        for (int i = 0; i < xCount; i++)
        {
            for (int j = 0; j < yCount; j++)
            {
                var node = topNodes[i, j];
                data.wfNodes.Add(node);
                if (i > 0)//左
                {
                    var node_l = topNodes[i - 1, j];
                    data.wfBars.Add(new WFBar(node.m_id, node_l.m_id, barType));
                }
                if (i < xCount - 1)//右
                {
                    var node_r = topNodes[i + 1, j];
                    data.wfBars.Add(new WFBar(node.m_id, node_r.m_id, barType));
                }
                if (j > 0)//下
                {
                    var node_d = topNodes[i, j - 1];
                    data.wfBars.Add(new WFBar(node.m_id, node_d.m_id, barType));
                }
                if (j < yCount - 1)//上
                {
                    var node_u = topNodes[i, j + 1];
                    data.wfBars.Add(new WFBar(node.m_id, node_u.m_id, barType));
                }
                //Debug.Log(i + "" + j + topNodes[i, j].m_id);
            }
        }
        return data;
    }

    /// <summary>
    /// 生成一组[桁架型]单元信息
    /// 左下前角为原点
    /// </summary>
    /// <param name="clamp"></param>
    /// <returns></returns>
    internal static WFData TrussTypeGridFrame_Unit(float x_Size, float y_Size, float height)
    {
        var wfData = new WFData();
        var node1 = new WFNode(new Vector3(0, 0, 0));
        var node2 = new WFNode(new Vector3(x_Size, 0, 0));
        var node3 = new WFNode(new Vector3(x_Size, height, 0));
        var node4 = new WFNode(new Vector3(0, height, 0));
        var node5 = new WFNode(new Vector3(0, 0, y_Size));
        var node6 = new WFNode(new Vector3(x_Size, 0, y_Size));
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


        wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id));//1-2
        wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id));//1-4
        wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id));//1-5
        wfData.wfBars.Add(new WFBar(node1.m_id, node8.m_id));//1-8
        wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id));//2-3
        wfData.wfBars.Add(new WFBar(node2.m_id, node4.m_id));//2-4
        wfData.wfBars.Add(new WFBar(node2.m_id, node6.m_id));//2-6
        wfData.wfBars.Add(new WFBar(node2.m_id, node7.m_id));//2-7
        wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id));//3-4
        wfData.wfBars.Add(new WFBar(node3.m_id, node7.m_id));//3-7
        wfData.wfBars.Add(new WFBar(node4.m_id, node8.m_id));//4-8
        wfData.wfBars.Add(new WFBar(node5.m_id, node6.m_id));//5-6
        wfData.wfBars.Add(new WFBar(node5.m_id, node8.m_id));//5-8
        wfData.wfBars.Add(new WFBar(node6.m_id, node7.m_id));//6-7
        wfData.wfBars.Add(new WFBar(node6.m_id, node8.m_id));//6-8
        wfData.wfBars.Add(new WFBar(node7.m_id, node8.m_id));//7-8
        return wfData;
    }

    /// <summary>
    /// 生成一组[四角锥]单元信息
    /// </summary>
    /// <param name="x_Size"></param>
    /// <param name="y_Size"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    internal static WFData QuadrangularGridFrame_Unit(float x_Size, float y_Size, float height)
    {
        var wfData = new WFData();
        var node1 = new WFNode(new Vector3(0, 0, 0), NodePosType.taperedBottom);
        var node2 = new WFNode(new Vector3(x_Size, 0, 0), NodePosType.taperedBottom);
        var node3 = new WFNode(new Vector3(x_Size, 0, y_Size), NodePosType.taperedBottom);
        var node4 = new WFNode(new Vector3(0, 0, y_Size), NodePosType.taperedBottom);
        var node5 = new WFNode(new Vector3(x_Size * 0.5f, -height, y_Size * 0.5f), NodePosType.taperedTop);

        wfData.wfNodes.Add(node1);
        wfData.wfNodes.Add(node2);
        wfData.wfNodes.Add(node3);
        wfData.wfNodes.Add(node4);
        wfData.wfNodes.Add(node5);

        wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id, BarPosType.upBar));//1-2
        wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id, BarPosType.upBar));//1-4
        wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id, BarPosType.centerBar));//1-5
        wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id, BarPosType.upBar));//2-3
        wfData.wfBars.Add(new WFBar(node2.m_id, node5.m_id, BarPosType.centerBar));//2-5
        wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id, BarPosType.upBar));//3-4
        wfData.wfBars.Add(new WFBar(node3.m_id, node5.m_id, BarPosType.centerBar));//3-5
        wfData.wfBars.Add(new WFBar(node4.m_id, node5.m_id, BarPosType.centerBar));//4-5
        return wfData;
    }


    /// <summary>
    /// 生成一组[四角锥(菱形)]单元信息
    /// </summary>
    /// <param name="x_Size"></param>
    /// <param name="y_Size"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    internal static WFData QuadDiamondGridFrame_Unit(float x_Size, float y_Size, float height)
    {
        var wfData = new WFData();
        var node1 = new WFNode(new Vector3(0, 0, 0), NodePosType.taperedBottom);
        var node2 = new WFNode(new Vector3(x_Size * 0.5f, 0, -y_Size * 0.5f), NodePosType.taperedBottom);
        var node3 = new WFNode(new Vector3(x_Size, 0, 0), NodePosType.taperedBottom);
        var node4 = new WFNode(new Vector3(x_Size * 0.5f, 0, y_Size * 0.5f), NodePosType.taperedBottom);
        var node5 = new WFNode(new Vector3(x_Size * 0.5f, -height, 0), NodePosType.taperedTop);

        wfData.wfNodes.Add(node1);
        wfData.wfNodes.Add(node2);
        wfData.wfNodes.Add(node3);
        wfData.wfNodes.Add(node4);
        wfData.wfNodes.Add(node5);

        wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id, BarPosType.upBar));//1-2
        wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id, BarPosType.upBar));//1-4
        wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id, BarPosType.centerBar));//1-5
        wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id, BarPosType.upBar));//2-3
        wfData.wfBars.Add(new WFBar(node2.m_id, node5.m_id, BarPosType.centerBar));//2-5
        wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id, BarPosType.upBar));//3-4
        wfData.wfBars.Add(new WFBar(node3.m_id, node5.m_id, BarPosType.centerBar));//3-5
        wfData.wfBars.Add(new WFBar(node4.m_id, node5.m_id, BarPosType.centerBar));//4-5
        return wfData;
    }
}
