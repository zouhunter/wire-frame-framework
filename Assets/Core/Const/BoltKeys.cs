using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class BoltKeys {
    public static string[] keys { get; private set; }
    static BoltKeys()
    {
        var fields = typeof(BoltKeys).GetFields();
        keys = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            keys[i] = fields[i].GetValue(null).ToString();
        }
    }
    public const string sealingPlate = "封板型";
    public const string conicalHead = "锥头型";
}
