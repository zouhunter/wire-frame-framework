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
using WireFrame;

public class DefultTest : MonoBehaviour
{
    public enum ControllerType
    {
        点线绘制测试,
        三角锥型,

        正交正放四角锥型,
        正交斜放四角锥型,
        斜置四角锥型,

        正交正放桁架型,
        正交斜放桁架型,
        三向交叉型桁架型,

        抽空四角锥型网架,
        棋盘四角锥网架,
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
            case ControllerType.点线绘制测试:
                creater = new SimpleGenerate();
                break;
            case ControllerType.正交正放桁架型:
                creater = new OrthonormalTrussTypeTrussTypeGridFrame();
                break;
            case ControllerType.三角锥型:
                creater = new TriangularPyramidNetworkFrame();
                break;
            case ControllerType.正交正放四角锥型:
                creater = new OrthonormalFourAnglePyramidMeshFrame();
                break;
            case ControllerType.正交斜放四角锥型:
                creater = new OrthogonalSlantingFourAnglePyramidGridFrame();
                break;
            case ControllerType.斜置四角锥型:
                creater = new ObliqueFourAnglePyramidGridFrame();
                break;
            case ControllerType.正交斜放桁架型:
                creater = new OrthonormalTrussedTrussTypeGridFrame();
                break;
            case ControllerType.三向交叉型桁架型:
                creater = new ThreeDirectionIntersectingGridFrame();
                break;
            case ControllerType.抽空四角锥型网架:
                creater = new FourAnglePyramidSpaceTrussGridFrame();
                break;
            case ControllerType.棋盘四角锥网架:
                creater = new ChessboardFourPyramidGridFrame();
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        wireFrame = creater.Create(node, bar, fulcrum, clamp);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("ShowLine"))
        {
            wireFrame.SwitchToLine();
        }
        if (GUILayout.Button("ShowModel"))
        {
            wireFrame.SwitchToModel();
        }
    }
}
