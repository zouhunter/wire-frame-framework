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

        public List<NodeBehaiver> nodes = new List<NodeBehaiver>();
        public List<BarBehaiver> bars = new List<BarBehaiver>();

        public void SwitchToModel()
        {
            foreach (var item in nodes)
            {
                item.UnHide();
            }
            foreach (var item in bars)
            {
                item.ShowModel();
            }
        }
        public void SwitchToLine()
        {
            foreach (var item in nodes)
            {
                item.Hide();
            }
            foreach (var item in bars)
            {
                item.ShowLine();
            }
        }

        internal void RegistNodeBehaivers(List<NodeBehaiver> nodes)
        {
            foreach (var node in nodes)
            {
                if (!this.nodes.Contains(node))
                {
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
                    this.bars.Add(bar);
                }
            }
        }
    }
}