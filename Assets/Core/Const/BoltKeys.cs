using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class BoltKeys {
    internal static string[] keys { get; private set; }
    static BoltKeys()
    {
        var props = typeof(BoltKeys).GetProperties(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.Static|System.Reflection.BindingFlags.GetProperty);
        keys = new string[props.Length];
        for (int i = 0; i < props.Length; i++)
        {
            keys[i] = props[i].GetValue(null,null).ToString();
        }
    }
    public static string sealingPlate { get { return "封板型"; } }
    public static string conicalHead { get { return "锥头型"; } }
}
