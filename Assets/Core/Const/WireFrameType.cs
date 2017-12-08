using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public static class WireFrameType  {
    public static string[] NameList { get; private set; }
    static WireFrameType()
    {
        var fields = typeof(WireFrameType).GetFields();
        NameList = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++){
            NameList[i] = fields[i].GetValue(null).ToString();
        }
    }

    public const string OrthonormalFourAnglePyramidMeshFrame = "正交正方四角锥";
    public const string ObliqueFourAnglePyramidSpaceGrid = "斜置四角锥网架";
    public const string OrthogonalSlantingFourAnglePyramidSpaceGrid = "正交斜放四角锥网架";
    public const string OrthonormalTrussTypeTrussTypeSpaceGrid = "正交正放桁架型网架";
    public const string OrthonormalTrussedTrussTypeSpaceGrid = "正交斜放桁架型网架";
    public const string ThreeDirectionIntersectingSpaceGrid = "三向交叉桁架型网架";
    public const string FourAnglePyramidSpaceTrussSpaceGrid = "抽空四角锥网架";
    public const string ChessboardFourPyramidSpaceGrid = "棋盘四角锥网架";
    public const string TriangularPyramidSpaceGrid = "三角锥网架";
    public const string OrthonormalFourAnglePyramidMeshFrame_3 = "三层正放四角锥网架";
    public const string FourAnglePyramidSpaceTrussSpaceGrid_3 = "三层抽空四角锥网架";
    public const string ChessboardFourPyramidSpaceGrid_3 = "三层棋盘四角锥网架";
    public const string OrthogonalSlantingFourAnglePyramidSpaceGrid_3 = "三层斜放四角锥网架";
}
