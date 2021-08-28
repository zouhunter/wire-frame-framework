using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace WireFrame
{
    public static class WireFrameType
    {
        internal static string[] NameList { get; private set; }
        static WireFrameType()
        {
            var props = typeof(WireFrameType).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);
            NameList = new string[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                NameList[i] = props[i].GetValue(null, null).ToString();
            }
        }
        /// <summary>
        /// 正交正方四角锥
        /// </summary>
        public static string OrthonormalFourAnglePyramidMeshFrame { get { return "正交正方四角锥"; } }
        /// <summary>
        /// 斜置四角锥网架
        /// </summary>
        public static string ObliqueFourAnglePyramidSpaceGrid { get { return "斜置四角锥网架"; } }
        /// <summary>
        /// 正交斜放四角锥网架
        /// </summary>
        public static string OrthogonalSlantingFourAnglePyramidSpaceGrid { get { return "正交斜放四角锥网架"; } }
        /// <summary>
        /// 正交正放桁架型网架
        /// </summary>
        public static string OrthonormalTrussTypeTrussTypeSpaceGrid { get { return "正交正放桁架型网架"; } }
        /// <summary>
        /// 正交斜放桁架型网架
        /// </summary>
        public static string OrthonormalTrussedTrussTypeSpaceGrid { get { return "正交斜放桁架型网架"; } }
        /// <summary>
        /// 三向交叉桁架型网架
        /// </summary>
        public static string ThreeDirectionIntersectingSpaceGrid { get { return "三向交叉桁架型网架"; } }
        /// <summary>
        /// 抽空四角锥网架
        /// </summary>
        public static string FourAnglePyramidSpaceTrussSpaceGrid { get { return "抽空四角锥网架"; } }
        /// <summary>
        /// 棋盘四角锥网架
        /// </summary>
        public static string ChessboardFourPyramidSpaceGrid { get { return "棋盘四角锥网架"; } }
        /// <summary>
        ///三角锥网架 
        /// </summary>
        public static string TriangularPyramidSpaceGrid { get { return "三角锥网架"; } }
        /// <summary>
        /// 三层正放四角锥网架
        /// </summary>
        public static string OrthonormalFourAnglePyramidMeshFrame_3 { get { return "三层正放四角锥网架"; } }
        /// <summary>
        /// 三层抽空四角锥网架
        /// </summary>
        public static string FourAnglePyramidSpaceTrussSpaceGrid_3 { get { return "三层抽空四角锥网架"; } }
        /// <summary>
        /// 三层棋盘四角锥网架
        /// </summary>
        public static string ChessboardFourPyramidSpaceGrid_3 { get { return "三层棋盘四角锥网架"; } }
        /// <summary>
        /// 三层斜放四角锥网架
        /// </summary>
        public static string OrthogonalSlantingFourAnglePyramidSpaceGrid_3 { get { return "三层斜放四角锥网架"; } }
    }
}