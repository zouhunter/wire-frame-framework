using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PonitPosType {
    public static string[] keys { get; private set; }
    static PonitPosType()
    {
        var fields = typeof(PonitPosType).GetFields();
        keys = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            keys[i] = fields[i].GetValue(null).ToString();
        }
    }
    public const string upPoint = "上弦节点";
    public const string downPoint = "下弦节点";
}
