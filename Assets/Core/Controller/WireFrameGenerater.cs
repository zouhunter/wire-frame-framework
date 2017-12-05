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

public abstract class WireFrameGenerater : IWireCreater
{
    public abstract bool CanCreate(Clamp clamp);
    public WireFrameBehaiver Create(NodeBehaiver nodePrefab, BarBehaiver barPrefab, FulcrumBehaiver fulcrum, Clamp clamp)
    {
        var wfData = GenerateWFData(clamp);
        return CreateInternal(nodePrefab, barPrefab, fulcrum, clamp, wfData.wfNodes, wfData.wfBars);
    }

    protected abstract WFData GenerateWFData(Clamp clamp);

    private WireFrameBehaiver CreateInternal(NodeBehaiver nodePrefab, BarBehaiver barPrefab, FulcrumBehaiver fulcrum, Clamp clamp, List<WFNode> wfNodes, List<WFBar> wfBars)
    {
        var wireFrame = new GameObject("SimpleWireFrame").AddComponent<WireFrameBehaiver>();
        var nodeGroup = CreateChildObject(wireFrame.transform, "NodeGroup");
        var barGroup = CreateChildObject(wireFrame.transform, "BarGroup");
        var fulcrumGroup = CreateChildObject(wireFrame.transform, "FulcrumGroup");

        var nodes = new List<NodeBehaiver>();
        foreach (var wfNode in wfNodes)
        {
            var node = UnityEngine.Object.Instantiate(nodePrefab);
            node.transform.SetParent(nodeGroup);
            node.transform.position = wfNode.position;
            nodes.Add(node);
        }
        wireFrame.RegistNodeBehaivers(nodes);
        var bars = new List<BarBehaiver>();
        foreach (var item in wfBars)
        {
            var startNode = wfNodes.Find(x => x.m_id == item.m_fromNodeId);
            var endNode = wfNodes.Find(x => x.m_id == item.m_toNodeId);
            if(startNode != null && endNode != null)
            {
                var barPos = (startNode.position + endNode.position) * 0.5f;
                var bar = UnityEngine.Object.Instantiate(barPrefab);
                bar.transform.SetParent(barGroup);
                bar.transform.position = barPos;
                bar.transform.forward = endNode.position - startNode.position;
                bar.ReSetLength(Vector3.Distance(endNode.position,startNode.position));
                bars.Add(bar);
            }
            else
            {
                Debug.LogError("bar info not correct:" + item.m_id);
            }
        }
        wireFrame.RegistBarBehaivers(bars);
        return wireFrame;
    }
    private Transform CreateChildObject(Transform parent, string objName)
    {
        var obj = new GameObject(objName);
        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero;
        return obj.transform;
    }
}
