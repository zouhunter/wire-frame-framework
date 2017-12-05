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

public class SimpleGenerate : WireFrameGenerater
{
    public override WireFrameBehaiver Create(WFData wfData)
    {
        Debug.Log(wfData);
        return null;

    }
}
