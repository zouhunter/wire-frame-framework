﻿using UnityEngine;
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
        public abstract bool CanCreate(FrameRule clamp);
        public abstract bool CanDouble { get; }
        public IWire Unit(FrameRule clamp)
        {
            var wfData = GenerateWFDataUnit(clamp);
            return CreateInternal(clamp, wfData.wfNodes, wfData.wfBars, CalcFulcrumPos(clamp));
        }
        public IWire Create(FrameRule clamp)
        {
            var wfData = GenerateWFData(clamp);
            if (CanDouble && clamp.doubleLayer)
            {
                CreateDoubleLayer(wfData, clamp.height);
            }
            return CreateInternal(clamp, wfData.wfNodes, wfData.wfBars, CalcFulcrumPos(clamp));
        }

        protected abstract WFData GenerateWFData(FrameRule clamp);
        protected abstract WFData GenerateWFDataUnit(FrameRule clamp);
        public abstract List<WFFul> CalcFulcrumPos(FrameRule clamp);
        private WireFrameBehaiver CreateInternal(FrameRule clamp, List<WFNode> wfNodes, List<WFBar> wfBars, List<WFFul> fuls)
        {
            var wireFrame = new GameObject("SimpleWireFrame").AddComponent<WireFrameBehaiver>();

            var nodeGroup = CreateChildObject(wireFrame.transform, "NodeGroup");
            var nodes = new List<NodeBehaiver>();
            foreach (var wfNode in wfNodes)
            {
                var go = new GameObject(wfNode.type);
                var node = go.AddComponent<NodeBehaiver>();
                node.transform.SetParent(nodeGroup);
                node.OnInitialized(wfNode);
                nodes.Add(node);
            }
            wireFrame.RegistNodeBehaivers(nodes);


            var barGroup = CreateChildObject(wireFrame.transform, "BarGroup");
            var bars = new List<BarBehaiver>();
            foreach (var item in wfBars)
            {
                var startNode = wfNodes.Find(x => x.m_id == item.fromNodeId);
                var endNode = wfNodes.Find(x => x.m_id == item.toNodeId);
                if (startNode != null && endNode != null)
                {
                    var barPos = (startNode.position + endNode.position) * 0.5f;
                    var go = new GameObject(item.type);
                    var bar = go.AddComponent<BarBehaiver>();
                    bar.transform.SetParent(barGroup);
                    bar.transform.position = barPos;
                    bar.transform.forward = endNode.position - startNode.position;
                    bar.ReSetLength(Vector3.Distance(endNode.position, startNode.position));
                    bar.OnInitialized(item);
                    bars.Add(bar);
                }
                else
                {
                    Debug.LogError("bar info not correct:" + item.id);
                }
            }
            wireFrame.RegistBarBehaivers(bars);

            var fulcrumGroup = CreateChildObject(wireFrame.transform, "FulcrumGroup");
            var fulcrums = new List<FulcrumBehaiver>();
            foreach (var item in fuls)
            {
                var go = new GameObject(WireFrameUtility.GetChineseFulcrumType(item.type));
                var fu = go.AddComponent<FulcrumBehaiver>();
                fu.transform.SetParent(fulcrumGroup);
                fu.transform.position = item.position;
                fu.OnInitialized(item);
                fulcrums.Add(fu);
            }
            wireFrame.RegistFulcrumBehaivers(fulcrums);
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