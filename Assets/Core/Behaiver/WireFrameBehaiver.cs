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
    /// 网架系统
    /// </summary>
    public class WireFrameBehaiver : MonoBehaviour
    {
        private List<INode> nodes = new List<INode>();
        private List<IBar> bars = new List<IBar>();
        private List<IFulcrum> fulcrums = new List<IFulcrum>();

        public GameObject Body
        {
            get
            {
                return _gameObject;
            }
        }
        private GameObject _gameObject;
        public event UnityAction<IBar>     onBarHover;
        public event UnityAction<IBar>     onBarClicked;
        public event UnityAction<INode>    onNodeHover;
        public event UnityAction<INode>    onNodeClicked;
        public event UnityAction<IFulcrum> onFulcrumHover;
        public event UnityAction<IFulcrum> onFulcrumClicked;

        private int totalNodes;
        private int totalBars;
        private int totalFulcrums;
        private const int group = 30;

        public bool compleInit { get { return totalNodes == nodes.Count && totalBars == bars.Count && totalFulcrums == fulcrums.Count; } }
        public float progress { get { return (nodes.Count + bars.Count + fulcrums.Count) / (float)(totalNodes + totalBars + totalFulcrums); } }

        private void Awake()
        {
            _gameObject = gameObject;
        }

        public IEnumerator SwitchToModel(ModelRule rule)
        {
            yield return null;

            for (int i = 0; i < nodes.Count; i += group)
            {
                for (int j = 0; j < group && j + i < nodes.Count; j++)
                {
                    var item = nodes[i + j];
                    item.ShowModel(rule.node);
                }
                yield return null;
            }

            for (int i = 0; i < bars.Count; i += group)
            {
                for (int j = 0; j < group && j + i < bars.Count; j++)
                {
                    var item = bars[j + i];
                    item.ShowModel(rule.bar);
                }
                yield return null;
            }

            for (int i = 0; i < fulcrums.Count; i += group)
            {
                for (int j = 0; j < group && j + i < fulcrums.Count; j++)
                {
                    var item = fulcrums[j + i];
                    item.ShowModel(rule.fulcrum);
                }
                yield return null;
            }
        }

        public void SwitchToLine(LineRule rule)
        {
            foreach (var item in nodes)
            {
                item.Hide();
            }
            foreach (var item in bars)
            {
                item.ShowLine(rule.lineMat, rule.lineWidth);
            }
            foreach (var item in fulcrums)
            {
                item.Hide();
            }
        }
        public void ReSetSize(SizeRule sizeRule)
        {
            foreach (var item in nodes)
            {
                item.SetSize(sizeRule.r_node);
            }
            foreach (var item in bars)
            {
                switch (item.Info.type)
                {
                    case BarPosType.upBar:
                        item.SetSize(sizeRule.r_upBar);
                        break;
                    case BarPosType.downBar:
                        item.SetSize(sizeRule.r_upBar);
                        break;
                    case BarPosType.centerBar:
                        item.SetSize(sizeRule.r_centerBar);
                        break;
                    default:
                        break;
                }
            }
            foreach (var item in fulcrums)
            {
                switch (item.Info.type)
                {
                    case FulcrumType.upPoint:
                        item.SetSize(sizeRule.r_upPoint,sizeRule.l_upPoint);
                        break;
                    case FulcrumType.upBound:
                        item.SetSize(sizeRule.r_downPoint, sizeRule.l_downPoint);
                        break;
                    case FulcrumType.downPoint:
                        item.SetSize(sizeRule.r_upBound, sizeRule.l_upBound);
                        break;
                    case FulcrumType.downBound:
                        item.SetSize(sizeRule.r_downBound, sizeRule.l_downBound);
                        break;
                    default:
                        break;
                }
            }
        }

        public void StartInit(int totalNodes,  int totalBars,int totalFulcrums)
        {
            this.totalNodes = totalNodes;
            this.totalBars = totalBars;
            this.totalFulcrums = totalFulcrums;
        }

        internal void RegistNode(NodeBehaiver node)
        {
            if (!this.nodes.Contains(node)){
                node.onHover = OnNodeHover;
                node.onClicked = OnNodeClicked;
                this.nodes.Add(node);
            }
        }
        internal void RegistBar(BarBehaiver bar)
        {
            if (!this.bars.Contains(bar))
            {
                bar.onHover = OnBarHover;
                bar.onClicked = OnBarClicked;
                this.bars.Add(bar);
            }
        }
        internal void RegistFulcrum(FulcrumBehaiver fulcrum)
        {
            if (!this.fulcrums.Contains(fulcrum))
            {
                fulcrum.onHover = OnFulcrumHover;
                fulcrum.onClicked = OnFulcrumClicked;
                this.fulcrums.Add(fulcrum);
            }
        }
        private void OnBarHover(BarBehaiver bar)
        {
            if(this.onBarHover != null)
            {
                onBarHover(bar);
            }
        }
        private void OnBarClicked(BarBehaiver bar)
        {
            if (this.onBarClicked != null)
            {
                onBarClicked(bar);
            }
        }
        private void OnNodeHover(NodeBehaiver node)
        {
            if (onNodeHover != null)
            {
                onNodeHover.Invoke(node);
            }
        }
        private void OnNodeClicked(NodeBehaiver node)
        {
            if (onNodeClicked != null)
            {
                onNodeClicked.Invoke(node);
            }
        }

        private void OnFulcrumHover(FulcrumBehaiver fulcrum)
        {
            if(onFulcrumHover != null)
            {
                onFulcrumHover.Invoke(fulcrum);
            }
        }
        private void OnFulcrumClicked(FulcrumBehaiver fulcrum)
        {
            if (onFulcrumClicked != null)
            {
                onFulcrumClicked.Invoke(fulcrum);
            }
        }

       
    }
}