using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.Scripting;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.Assertions.Comparers;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// 杆件
/// </summary>
public class BarBehaiver : MonoBehaviour {
    public BarInfo barInfo;
    private float lengthPara = 1;//长度加权
    public float longness { get { return barInfo.longness * lengthPara; } }
    public float slendernessRatio { get { return longness / barInfo.diameter; } }

    public string barPosType { get; private set; }

    public void OnInitialized(string barPosType)
    {
        this.barPosType = barPosType;
    }

    internal void ShowLine()
    {
        throw new NotImplementedException();
    }

    internal void ShowModel()
    {
        throw new NotImplementedException();
    }

    internal void ReSetLength(float longness)
    {
        lengthPara = longness / barInfo.longness;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, lengthPara);
    }
}
