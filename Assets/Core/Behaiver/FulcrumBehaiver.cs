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
    /// 支点
    /// </summary>
    public class FulcrumBehaiver : RunTimeObjectHolder,IFulcrum
    {
        public WFFul Info { get; private set; }
        public UnityAction<FulcrumBehaiver> onHover { get; set; }
        public UnityAction<FulcrumBehaiver> onClicked { get; set; }
        public GameObject Body { get { return gameObject; } }
        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer( LayerSetting.fulcrum);
        }
        public void Hide()
        {
            if(instenceObj != null)
            {
                instenceObj.SetActive(false);
            }
        }
        public void OnInitialized(WFFul ful)
        {
            this.Info = ful;
            CreateCollider();
        }
        private void OnMouseUp()
        {
            if (onClicked != null && !IsMousePointOnUI() && HaveExecuteTwince(ref timer)) onClicked.Invoke(this);
        }
        private void OnMouseOver()
        {
            if (onHover != null && !IsMousePointOnUI()) onHover.Invoke(this);
        }

        public void SetSize(float r,float length)
        {
            transform.localScale = new Vector3(r * 2,length ,r * 2);
        }
    }
}