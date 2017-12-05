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
    public BarBehaiver bar;
    public NodeBehaiver node;
    public FulcrumBehaiver fulcrum;
    public WFData data;
    IWireCreater creater;
    WireFrameBehaiver wireFrame;
    public Clamp clamp;
    private void OnEnable()
    {
        creater = new SimpleGenerate();
        data = SimpleGenerate.GetTestData();
    }

    private void Start()
    {
        wireFrame = creater.Create(node,bar,fulcrum, clamp);
    }
}
