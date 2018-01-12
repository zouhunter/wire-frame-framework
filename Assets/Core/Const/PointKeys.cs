using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public static class PointKeys
{
    public static string[] keys { get; private set; }
    static PointKeys()
    {
        var fields = typeof(PointKeys).GetFields();
        keys = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            keys[i] = fields[i].GetValue(null).ToString();
        }
    }
    public const string ball = "球节点";
    public const string support = "支座节点";
    public const string corbel = "支托节点";
}
