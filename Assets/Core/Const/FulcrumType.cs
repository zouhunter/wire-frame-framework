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
    public const string upPoint = "上弦点支承";
    public const string upBound = "上弦周边支承";
    public const string downPoint = "下弦点支承";
    public const string downBound = "下弦周边支承";//Chord
}
