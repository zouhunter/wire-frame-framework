using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class FulcrumType  {
    public static string[] NameList { get; private set; }
    static FulcrumType()
    {
        var fields = typeof(FulcrumType).GetFields();
        NameList = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            NameList[i] = fields[i].GetValue(null).ToString();
        }
    }
    public const string point = "点支承";
    public const string bound = "周边支承";
    public const string up = "上弦支承";
    public const string down = "下弦支承";
}
