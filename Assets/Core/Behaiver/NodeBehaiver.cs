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
    /// <summary>
    /// 节点
    /// </summary>
    public class NodeBehaiver : RunTimeObjectHolder, INode
    {
        public WFNode Info { get; private set; }
        public UnityAction<NodeBehaiver> onHover { get; set; }
        public UnityAction<NodeBehaiver> onClicked { get; set; }

        public void Hide()
        {
            if(instenceObj != null)
            {
                instenceObj.gameObject.SetActive(false);
            }
        }
        internal void OnInitialized(WFNode node)
        {
            this.Info = node;
            transform.position = Info.position;
        }
        private void OnMouseDown()
        {
            if (onClicked != null) onClicked.Invoke(this);
        }
        private void OnMouseOver()
        {
            if (onHover != null) onHover.Invoke(this);
        }

        public void SetSize(float r_node)
        {
            transform.localScale = Vector3.one * r_node;
        }
    }
}