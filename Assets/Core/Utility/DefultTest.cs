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
    public enum ControllerType
    {
        Simple,//测试
        OrthonormalTrussTypeTrussTypeGridFrame,//正交正放桁架型网架
        TriangularPyramidNetworkFrame,//三角锥网架
        OrthonormalFourAnglePyramidMeshFrame,//正交正放四角锥
        OrthogonalSlantingFourAnglePyramidGridFrame,//正交斜放四角锥
        OrthonormalTrussedTrussTypeGridFrame,//正交斜放桁架型
        ThreeDirectionIntersectingGridFrame,//三向交叉型桁架
    }

    public BarBehaiver bar;
    public NodeBehaiver node;
    public FulcrumBehaiver fulcrum;
    IWireCreater creater;
    WireFrameBehaiver wireFrame;
    public Clamp clamp;
    public ControllerType controllerType;

    private void OnEnable()
    {
        switch (controllerType)
        {
            case ControllerType.Simple:
                creater = new SimpleGenerate();
                break;
            case ControllerType.OrthonormalTrussTypeTrussTypeGridFrame:
                creater = new OrthonormalTrussTypeTrussTypeGridFrame();
                break;
            case ControllerType.TriangularPyramidNetworkFrame:
                creater = new TriangularPyramidNetworkFrame();
                break;
            case ControllerType.OrthonormalFourAnglePyramidMeshFrame:
                creater = new OrthonormalFourAnglePyramidMeshFrame();
                break;
            case ControllerType.OrthogonalSlantingFourAnglePyramidGridFrame:
                creater = new OrthogonalSlantingFourAnglePyramidGridFrame();
                break;
            case ControllerType.OrthonormalTrussedTrussTypeGridFrame:
                creater = new OrthonormalTrussedTrussTypeGridFrame();
                break;
            case ControllerType.ThreeDirectionIntersectingGridFrame:
                creater = new ThreeDirectionIntersectingGridFrame();
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        wireFrame = creater.Create(node,bar,fulcrum, clamp);
    }

    private void OnGUI()
    {
        if(GUILayout.Button("ShowLine"))
        {
            wireFrame.SwitchToLine();
        }
        if(GUILayout.Button("ShowModel"))
        {
            wireFrame.SwitchToModel();
        }
    }
}
