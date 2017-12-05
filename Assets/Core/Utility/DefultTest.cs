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
//[ExecuteInEditMode]
public class DefultTest : MonoBehaviour {
    public WFData wfData;
    IWireCreater creater;
    WireFrameBehaiver wireFrame;
    private void OnEnable()
    {
        wfData = CalcuteUtility.GetDefultData();
        creater = new SimpleGenerate();
    }

    private void Start()
    {
        wireFrame = creater.Create(wfData);
    }
}
