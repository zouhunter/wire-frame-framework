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
        public GameObject Body { get { return gameObject; } }
        public WFBar Info { get; private set; }
        private VRLineRenderer lineRender;
        private float normalWidth;
        private const float normalDistence = 10;
        private float currentWidth;
        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer(LayerSetting.gridBar);
        }
        private void Start()
        {
            if (lineRender == null)
                InitLineRenderer();
        }
        private void Update()
        {
            if (lineRender != null && Camera.main && lineRender.positionCount > 0)
            {
                currentWidth = normalWidth * Vector3.Distance(transform.position, Camera.main.transform.position) / normalDistence;
                if (Mathf.Abs(currentWidth - lineRender.widthStart) > 0.01f)
                {
                    lineRender.SetWidth(currentWidth, currentWidth);
                }
            }

        }
        private void InitLineRenderer()
        {
            var holder = new GameObject("lineRenderer");
            holder.AddComponent<MeshFilter>();
            holder.AddComponent<MeshRenderer>();
            lineRender = holder.AddComponent<VRLineRenderer>();
            lineRender.SetVertexCount(0);
            lineRender.useWorldSpace = true;
            lineRender.transform.SetParent(transform.parent);
        }

        public void OnInitialized(WFBar bar)
        {
            this.Info = bar;
            CreateCollider();
        }

        public void ShowLine(Material mat, float width)
        {
            normalWidth = width;
            lineRender.GetComponent<Renderer>().material = mat;

            var poss = new Vector3[2];
            poss[0] = transform.forward * longness * 0.5f + transform.position;
            poss[1] = -transform.forward * longness * 0.5f + transform.position;

            lineRender.SetVertexCount(2);
            lineRender.SetPositions(poss);
            lineRender.EditorCheckForUpdate();

            if (instenceObj != null)
            {
                instenceObj.gameObject.SetActive(false);
            }
        }

        public override void ShowModel(GameObject pfb)
        {
            base.ShowModel(pfb);
            OnModelUpdated();
            if(lineRender) lineRender.SetVertexCount(0);
        }

        internal void ReSetLength(float longness)
        {
            this.longness = longness;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, longness);
            OnModelUpdated();
        }
        private void OnMouseUp()
        {
            if (onClicked != null && !IsMousePointOnUI() && HaveExecuteTwince(ref timer)) onClicked.Invoke(this);
        }
        private void OnMouseOver()
        {
            if (onHover != null && !IsMousePointOnUI()) onHover.Invoke(this);
        }

        public void SetSize(float r_Bar)
        {
            this.diameter = r_Bar * 2;
            transform.localScale = new Vector3(diameter, diameter, longness);
            OnModelUpdated();
        }

        private void OnModelUpdated()
        {
            ResetMaterialTile(new Vector2(1, longness / diameter));
        }
    }
}
