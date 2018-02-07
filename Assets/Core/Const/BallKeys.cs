using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class BallKeys  {
    internal static string[] keys { get; private set; }
    static BallKeys()
    {
        var props = typeof(BallKeys).GetProperties(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.Static|System.Reflection.BindingFlags.GetProperty);
        keys = new string[props.Length];
        for (int i = 0; i < props.Length; i++)
        {
            keys[i] = props[i].GetValue(null,null).ToString();
        }
    }
    public static string bolt { get { return "螺栓节点"; } }
    public static string welding
    {
        get
        { return "焊接节点"; }
    }
}
