using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace WireFrame
{
    public static class PointKeys
    {
        internal static string[] keys { get; private set; }
        static PointKeys()
        {
            var fields = typeof(PointKeys).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.Static);
            keys = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                keys[i] = fields[i].GetValue(null, null).ToString();
            }
        }
        public static string ball
        {
            get
            {
                return "球节点";
            }
        }
        public static string support
        {
            get
            {
                return "支座节点";
            }
        }
        public static string corbel { get { return "支托节点"; } }
    }
}