using UnityEngine;
using WireFrame;

public class Demo1 : MonoBehaviour
{
    public ModelRule modelRule;
    public SizeRule sizeRule;
    public LineRule lineRule;

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

    IWireCreater creater;
    IWire wireFrame;
    public FrameRule clamp;
    public ControllerType controllerType;

    private void OnEnable()
    {
        switch (controllerType)
        {
            case ControllerType.点线绘制测试:
                creater = new SimpleGenerate();
                break;
            case ControllerType.正交正放桁架型:
                creater = new OrthonormalTrussTypeTrussTypeSpaceGrid();
                break;
            case ControllerType.三角锥型:
                creater = new TriangularPyramidSpaceGrid();
                break;
            case ControllerType.正交正放四角锥型:
                creater = new OrthonormalFourAnglePyramidMeshFrame();
                break;
            case ControllerType.正交斜放四角锥型:
                creater = new OrthogonalSlantingFourAnglePyramidSpaceGrid();
                break;
            case ControllerType.斜置四角锥型:
                creater = new ObliqueFourAnglePyramidSpaceGrid();
                break;
            case ControllerType.正交斜放桁架型:
                creater = new OrthonormalTrussedTrussTypeSpaceGrid();
                break;
            case ControllerType.三向交叉型桁架型:
                creater = new ThreeDirectionIntersectingSpaceGrid();
                break;
            case ControllerType.抽空四角锥型网架:
                creater = new FourAnglePyramidSpaceTrussSpaceGrid();
                break;
            case ControllerType.棋盘四角锥网架:
                creater = new ChessboardFourPyramidSpaceGrid();
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        wireFrame = creater.Create(clamp);
        StartCoroutine(wireFrame.SwitchToModel(modelRule));
        wireFrame.ReSetSize(sizeRule);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("ShowLine"))
        {
            StartCoroutine(wireFrame.SwitchToLine(lineRule));
        }
        if (GUILayout.Button("ShowModel"))
        {
            StartCoroutine(wireFrame.SwitchToModel(modelRule));
        }
    }
}
