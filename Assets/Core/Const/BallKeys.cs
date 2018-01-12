using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class BallKeys  {
    public static string[] keys { get; private set; }
    static BallKeys()
    {
        var fields = typeof(BallKeys).GetFields();
        keys = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            keys[i] = fields[i].GetValue(null).ToString();
        }
    }
    public const string bolt = "螺栓节点";
    public const string welding = "焊接节点";
}
