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
    public class WireFrameBehaiver : MonoBehaviour,IWire
    {
        private List<INode> nodes = new List<INode>();
        private List<IBar> bars = new List<IBar>();
        private List<IFulcrum> fulcrums = new List<IFulcrum>();

        public GameObject Body
        {
            get
            {
                return gameObject;
            }
        }

        public event UnityAction<IBar>     onBarHover;
        public event UnityAction<IBar>     onBarClicked;
        public event UnityAction<INode>    onNodeHover;
        public event UnityAction<INode>    onNodeClicked;
        public event UnityAction<IFulcrum> onFulcrumHover;
        public event UnityAction<IFulcrum> onFulcrumClicked;

        public void SwitchToModel(ModelRule rule)
        {
            foreach (var item in nodes)
            {
                item.ShowModel(rule.node);
            }
            foreach (var item in bars)
            {
                item.ShowModel(rule.bar);
            }
            foreach (var item in fulcrums)
            {
                item.ShowModel(rule.fulcrum);
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
                    case FulcrumPosType.upPoint:
                        item.SetSize(sizeRule.r_upPoint,sizeRule.l_upPoint);
                        break;
                    case FulcrumPosType.downPoint:
                        item.SetSize(sizeRule.r_downPoint, sizeRule.l_downPoint);
                        break;
                    case FulcrumPosType.upBound:
                        item.SetSize(sizeRule.r_upBound, sizeRule.l_upBound);
                        break;
                    case FulcrumPosType.downBound:
                        item.SetSize(sizeRule.r_downBound, sizeRule.l_downBound);
                        break;
                    default:
                        break;
                }
            }
        }

        internal void RegistNodeBehaivers(List<NodeBehaiver> nodes)
        {
            foreach (var node in nodes)
            {
                if (!this.nodes.Contains(node))
                {
                    node.onHover = OnNodeHover;
                    node.onClicked = OnNodeClicked;
                    this.nodes.Add(node);
                }
            }
        }
        internal void RegistBarBehaivers(List<BarBehaiver> bars)
        {
            foreach (var bar in bars)
            {
                if (!this.bars.Contains(bar))
                {
                    bar.onHover = OnBarHover;
                    bar.onClicked = OnBarClicked;
                    this.bars.Add(bar);
                }
            }
        }
        internal void RegistFulcrumBehaivers(List<FulcrumBehaiver> fuls)
        {
            foreach (var fulcrum in fuls)
            {
                if (!this.fulcrums.Contains(fulcrum))
                {
                    fulcrum.onHover = OnFulcrumHover;
                    fulcrum.onClicked = OnFulcrumClicked;
                    this.fulcrums.Add(fulcrum);
                }
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