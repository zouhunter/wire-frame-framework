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
namespace WireFrame
{

    /// <summary>
    /// 杆件的信息,材料 截面形式 直径 壁厚 长度 长细比
    /// [使用时创建信息实例]
    /// </summary>
    [System.Serializable]
    public class BarInfo
    {
        public string stuff;//材料
        public string sectionForm;//截面形式
        public float diameter;//直径
        public float thickness;//壁厚
        public float longness;//长度
        public float slendernessRatio;//长细比
        public BarType barType;//类型

    }
}