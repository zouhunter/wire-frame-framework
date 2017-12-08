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

    public abstract class WireFrameGenerater : IWireCreater
    {
        public abstract bool CanCreate(Rule clamp);

        public WireFrameBehaiver Unit(NodeBehaiver nodePrefab, BarBehaiver barPrefab, Rule clamp)
        {
            var wfData = GenerateWFDataUnit(clamp);
            return CreateInternal(nodePrefab, barPrefab, null, clamp, wfData.wfNodes, wfData.wfBars, CalcFulcrumPos(clamp));
        }
        public WireFrameBehaiver Create(NodeBehaiver nodePrefab, BarBehaiver barPrefab, FulcrumBehaiver fulcrum, Rule clamp)
        {
            var wfData = GenerateWFData(clamp);
            if (clamp.layer == 2){
                CreateDoubleLayer(wfData, clamp.height);
            }
            return CreateInternal(nodePrefab, barPrefab, fulcrum, clamp, wfData.wfNodes, wfData.wfBars, CalcFulcrumPos(clamp));
        }

        protected abstract WFData GenerateWFData(Rule clamp);
        protected abstract WFData GenerateWFDataUnit(Rule clamp);
        public abstract List<Vector3> CalcFulcrumPos(Rule clamp);
        private WireFrameBehaiver CreateInternal(NodeBehaiver nodePrefab, BarBehaiver barPrefab, FulcrumBehaiver fulcrum, Rule clamp, List<WFNode> wfNodes, List<WFBar> wfBars,List<Vector3> fuls)
        {
            var wireFrame = new GameObject("SimpleWireFrame").AddComponent<WireFrameBehaiver>();

            var nodeGroup = CreateChildObject(wireFrame.transform, "NodeGroup");
            var nodes = new List<NodeBehaiver>();
            foreach (var wfNode in wfNodes)
            {
                var node = UnityEngine.Object.Instantiate(nodePrefab);
                node.transform.SetParent(nodeGroup);
                node.transform.position = wfNode.position;
                node.OnInitialized(wfNode.type);
                nodes.Add(node);
            }
            wireFrame.RegistNodeBehaivers(nodes);


            var barGroup = CreateChildObject(wireFrame.transform, "BarGroup");
            var bars = new List<BarBehaiver>();
            foreach (var item in wfBars)
            {
                var startNode = wfNodes.Find(x => x.m_id == item.m_fromNodeId);
                var endNode = wfNodes.Find(x => x.m_id == item.m_toNodeId);
                if (startNode != null && endNode != null)
                {
                    var barPos = (startNode.position + endNode.position) * 0.5f;
                    var bar = UnityEngine.Object.Instantiate(barPrefab);
                    bar.transform.SetParent(barGroup);
                    bar.transform.position = barPos;
                    bar.transform.forward = endNode.position - startNode.position;
                    bar.ReSetLength(Vector3.Distance(endNode.position, startNode.position));
                    bar.OnInitialized(item.m_type);
                    bars.Add(bar);
                }
                else
                {
                    Debug.LogError("bar info not correct:" + item.m_id);
                }
            }
            wireFrame.RegistBarBehaivers(bars);

            var fulcrumGroup = CreateChildObject(wireFrame.transform, "FulcrumGroup");
            var fulcrums = new List<FulcrumBehaiver>();
            foreach (var item in fuls)
            {
                var fu = UnityEngine.Object.Instantiate(fulcrum);
                fu.transform.SetParent(fulcrumGroup);
                fu.transform.position = item;
                fulcrums.Add(fu);
            }
            return wireFrame;
        }
        private Transform CreateChildObject(Transform parent, string objName)
        {
            var obj = new GameObject(objName);
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero;
            return obj.transform;
        }
        private void CreateDoubleLayer(WFData wfData, float height)
        {
            var dataCopy = wfData.Copy();
            dataCopy.AppendRotation(Quaternion.Euler(new Vector3(180, 0, 0)));
            dataCopy.AppendPosition(new Vector3(0, -height, 0));
            wfData.InsertData(dataCopy);
        }

    }
}