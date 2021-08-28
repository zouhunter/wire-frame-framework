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
    public class WireFrameBehaiver : MonoBehaviour, IWire
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

        public event UnityAction<IBar> onBarHover;
        public event UnityAction<IBar> onBarClicked;
        public event UnityAction<INode> onNodeHover;
        public event UnityAction<INode> onNodeClicked;
        public event UnityAction<IFulcrum> onFulcrumHover;
        public event UnityAction<IFulcrum> onFulcrumClicked;

        private int totalNodes;
        private int totalBars;
        private int totalFulcrums;
        private const int group = 30;
        protected SizeRule m_sizeRule;
        protected LineRule m_lineRule;
        protected ModelRule m_modelRule;
        protected int m_showState = 0;
        protected bool m_showing = false;
        protected Dictionary<int, Stack<GameObject>> m_poolMap = new Dictionary<int, Stack<GameObject>>();

        public bool compleInit { get { return totalNodes == nodes.Count && totalBars == bars.Count && totalFulcrums == fulcrums.Count; } }
        public float progress { get { return (nodes.Count + bars.Count + fulcrums.Count) / (float)(totalNodes + totalBars + totalFulcrums); } }

        private void Awake()
        {
            _gameObject = gameObject;
        }

        public IEnumerator SwitchToModel(ModelRule rule)
        {
            if (m_showing)
                yield break;

            if (m_showState == 1)
                yield break;
            m_showState = 1;

            m_modelRule = rule;
            m_showing = true;
            yield return null;

            for (int i = 0; i < nodes.Count; i += group)
            {
                for (int j = 0; j < group && j + i < nodes.Count; j++)
                {
                    var item = nodes[i + j];
                 
                    item.ShowModel(rule.node, GetModelPool(rule.node));
                }
                yield return null;
            }

            for (int i = 0; i < bars.Count; i += group)
            {
                for (int j = 0; j < group && j + i < bars.Count; j++)
                {
                    var item = bars[j + i];
                    item.ShowModel(rule.bar, GetModelPool(rule.node));
                }
                yield return null;
            }

            for (int i = 0; i < fulcrums.Count; i += group)
            {
                for (int j = 0; j < group && j + i < fulcrums.Count; j++)
                {
                    var item = fulcrums[j + i];
                    item.ShowModel(rule.fulcrum, GetModelPool(rule.node));
                }
                yield return null;
            }
            m_showing = false;
        }

        protected Stack<GameObject> GetModelPool(GameObject pfb)
        {
            var hash = pfb.GetInstanceID();
            if (!m_poolMap.TryGetValue(hash, out var pool))
                pool = m_poolMap[hash] = new Stack<GameObject>();
            return pool;
        }

        public IEnumerator SwitchToLine(LineRule rule)
        {
            if (m_showing)
                yield break;

            if (m_showState == 2)
                yield break;
            m_showState = 2;

            m_showing = true;
            m_lineRule = rule;
            yield return null;
            foreach (var item in nodes)
            {
                item.Hide();
            }
            for (int i = 0; i < bars.Count; i += group)
            {
                for (int j = 0; j < group && j + i < bars.Count; j++)
                {
                    var item = bars[j + i];
                    item.ShowLine(m_lineRule.lineMat, m_lineRule.lineWidth);
                }
                yield return null;
            }
            foreach (var item in fulcrums)
            {
                item.Hide();
            }
            m_showing = false;
        }

        protected void RefreshNode(INode node)
        {
            if (m_sizeRule != null)
                node.SetSize(m_sizeRule.r_node);

            if(m_modelRule != null && m_showState == 1)
                node.ShowModel(m_modelRule.node,GetModelPool(m_modelRule.node));
        }

        protected void RefreshBar(IBar bar)
        {
            if (m_sizeRule != null)
            {
                switch (bar.Info.type)
                {
                    case BarPosType.upBar:
                        bar.SetSize(m_sizeRule.r_upBar);
                        break;
                    case BarPosType.downBar:
                        bar.SetSize(m_sizeRule.r_upBar);
                        break;
                    case BarPosType.centerBar:
                        bar.SetSize(m_sizeRule.r_centerBar);
                        break;
                    default:
                        break;
                }
            }
            if (m_showState == 2 && m_lineRule != null)
                bar.ShowLine(m_lineRule.lineMat, m_lineRule.lineWidth);

            if (m_modelRule != null && m_showState == 1)
                bar.ShowModel(m_modelRule.bar, GetModelPool(m_modelRule.bar));
        }

        protected void RefreshFulcrums(IFulcrum fulcrum)
        {
            if (m_sizeRule == null)
                return;

            switch (fulcrum.Info.type)
            {
                case FulcrumType.upPoint:
                    fulcrum.SetSize(m_sizeRule.r_upPoint, m_sizeRule.l_upPoint);
                    break;
                case FulcrumType.upBound:
                    fulcrum.SetSize(m_sizeRule.r_downPoint, m_sizeRule.l_downPoint);
                    break;
                case FulcrumType.downPoint:
                    fulcrum.SetSize(m_sizeRule.r_upBound, m_sizeRule.l_upBound);
                    break;
                case FulcrumType.downBound:
                    fulcrum.SetSize(m_sizeRule.r_downBound, m_sizeRule.l_downBound);
                    break;
                default:
                    break;
            }

            if (m_modelRule != null && m_showState == 1)
                fulcrum.ShowModel(m_modelRule.fulcrum, GetModelPool(m_modelRule.fulcrum));
        }

        public void ReSetSize(SizeRule sizeRule)
        {
            this.m_sizeRule = sizeRule;
            foreach (var item in nodes)
            {
                RefreshNode(item);
            }
            foreach (var item in bars)
            {
                RefreshBar(item);
            }
            foreach (var item in fulcrums)
            {
                RefreshFulcrums(item);
            }
        }

        public void StartInit(int totalNodes, int totalBars, int totalFulcrums)
        {
            this.totalNodes = totalNodes;
            this.totalBars = totalBars;
            this.totalFulcrums = totalFulcrums;
        }

        public void RegistNode(NodeBehaiver node)
        {
            if (!this.nodes.Contains(node))
            {
                node.onHover = OnNodeHover;
                node.onClicked = OnNodeClicked;
                RefreshNode(node);
                this.nodes.Add(node);
            }
        }

        public void RegistBar(BarBehaiver bar)
        {
            if (!this.bars.Contains(bar))
            {
                bar.onHover = OnBarHover;
                bar.onClicked = OnBarClicked;
                RefreshBar(bar);
                this.bars.Add(bar);
            }
        }

        public void RegistFulcrum(FulcrumBehaiver fulcrum)
        {
            if (!this.fulcrums.Contains(fulcrum))
            {
                fulcrum.onHover = OnFulcrumHover;
                fulcrum.onClicked = OnFulcrumClicked;
                RefreshFulcrums(fulcrum);
                this.fulcrums.Add(fulcrum);
            }
        }
        private void OnBarHover(BarBehaiver bar)
        {
            if (this.onBarHover != null)
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
            if (onFulcrumHover != null)
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