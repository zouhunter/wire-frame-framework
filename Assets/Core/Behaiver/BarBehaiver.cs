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
    public class BarBehaiver : MonoBehaviour,IBar
    {
        public string key;
        public BarInfo barInfo;
        [SerializeField]
        private Material mat;
        [SerializeField]
        private float lineWidth = 0.1f;
        [SerializeField]
        private GameObject renderObj;

        private float lengthPara = 1;//长度加权
        public float longness { get { return barInfo.longness * lengthPara; } }
        public float slendernessRatio { get { return longness / barInfo.diameter; } }
        public string barPosType { get; private set; }
        private LineRenderer lineRender;
        private void Awake()
        {
            lineRender = gameObject.AddComponent<LineRenderer>();
            lineRender.SetVertexCount(0);
            lineRender.material = mat;
            lineRender.SetWidth(lineWidth, lineWidth);
        }
        public void OnInitialized(string barPosType)
        {
            this.barPosType = barPosType ;
        }

        internal void ShowLine()
        {
            var poss = new Vector3[2];
            poss[0] = transform.forward * longness * 0.5f + transform.position;
            poss[1] = -transform.forward * longness * 0.5f + transform.position;
            lineRender.SetVertexCount(2);
            lineRender.SetPositions(poss);
            renderObj.gameObject.SetActive(false);
        }

        internal void ShowModel()
        {
            lineRender.SetVertexCount(0);
            renderObj.gameObject.SetActive(true);
        }

        internal void ReSetLength(float longness)
        {
            lengthPara = longness / barInfo.longness;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, lengthPara);
        }
    }
}
