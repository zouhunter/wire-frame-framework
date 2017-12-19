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
    /// 杆件
    /// </summary>
    public class BarBehaiver : RunTimeObjectHolder, IBar
    {
        private float longness = 1;//长度加权
        private float diameter = 1;
        public UnityAction<BarBehaiver> onHover { get; set; }
        public UnityAction<BarBehaiver> onClicked { get; set; }
        public WFBar Info { get; private set; }
        private LineRenderer lineRender;
        private void Awake()
        {
            lineRender = gameObject.AddComponent<LineRenderer>();
#if UNITY_5_3
            lineRender.SetVertexCount(0);

#else
            lineRender.positionCount = 0;
#endif
            gameObject.layer = LayerMask.NameToLayer(LayerSetting.gridBar);
        }
        public void OnInitialized(WFBar bar)
        {
            this.Info = bar;
            CreateCollider();
        }

        public void ShowLine(Material mat, float width)
        {
            lineRender.material = mat;

            var poss = new Vector3[2];
            poss[0] = transform.forward * longness * 0.5f + transform.position;
            poss[1] = -transform.forward * longness * 0.5f + transform.position;
#if UNITY_5_3
            lineRender.SetVertexCount(2);
            lineRender.SetWidth(width, width);
#else
            lineRender.positionCount = 2;
            lineRender.startWidth = lineRender.endWidth = width;
#endif

            lineRender.SetPositions(poss);
            if (instenceObj != null)
            {
                instenceObj.gameObject.SetActive(false);
            }
        }

        public override void ShowModel(GameObject pfb)
        {
            base.ShowModel(pfb);
#if UNITY_5_3
            lineRender.SetVertexCount(0);
#else
            lineRender.positionCount = 0;
#endif
            OnModelUpdated();
        }

        internal void ReSetLength(float longness)
        {
            this.longness = longness;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, longness);
            OnModelUpdated();
        }
        private void OnMouseDown()
        {
            if (onClicked != null) onClicked.Invoke(this);
        }
        private void OnMouseOver()
        {
            if (onHover != null) onHover.Invoke(this);
        }

        public void SetSize(float r_Bar)
        {
            this.diameter = r_Bar * 2;
            transform.localScale = new Vector3(diameter, diameter, longness);
            OnModelUpdated();
        }

        private void OnModelUpdated()
        {
            ResetMaterialTile(new Vector2(1, longness/ diameter));
        }
    }
}
