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
    public const string ObliqueFourAnglePyramidGridFrame = "斜置四角锥网架";
    public const string OrthogonalSlantingFourAnglePyramidGridFrame = "正交斜放四角锥网架";
    public const string OrthonormalTrussTypeTrussTypeGridFrame = "正交正放桁架型网架";
    public const string OrthonormalTrussedTrussTypeGridFrame = "正交斜放桁架型网架";
    public const string ThreeDirectionIntersectingGridFrame = "三向交叉桁架型网架";
    public const string FourAnglePyramidSpaceTrussGridFrame = "抽空四角锥网架";
    public const string ChessboardFourPyramidGridFrame = "棋盘四角锥网架";
    public const string TriangularPyramidNetworkFrame = "三角锥网架";
    public const string OrthonormalFourAnglePyramidMeshFrame_3 = "三层正放四角锥网架";
    public const string FourAnglePyramidSpaceTrussGridFrame_3 = "三层抽空四角锥网架";
    public const string ChessboardFourPyramidGridFrame_3 = "三层棋盘四角锥网架";
    public const string OrthogonalSlantingFourAnglePyramidGridFrame_3 = "三层斜放四角锥网架";
}
