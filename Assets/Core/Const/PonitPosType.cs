using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace WireFrame
{
    public class PonitPosType
    {
        internal static string[] keys { get; private set; }
        static PonitPosType()
        {
            var props = typeof(PonitPosType).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.GetProperty);
            keys = new string[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                keys[i] = props[i].GetValue(null, null).ToString();
            }
        }
        public static string upPoint { get { return "上弦节点"; } }
        public static string downPoint
        {
            get
            { return "下弦节点"; }
        }
    }
}