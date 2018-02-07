using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

namespace WireFrame
{

    public class WireFrameUtility
    {
        private static List<KeyValuePair<FulcrumType, string>> fulCrumPair;
        private static List<KeyValuePair<GridType, string>> gridTypePair;

        static WireFrameUtility()
        {
            fulCrumPair = new List<KeyValuePair<FulcrumType, string>>();
            fulCrumPair.Add(new KeyValuePair<FulcrumType, string>(FulcrumType.upPoint, "上弦点支承"));
            fulCrumPair.Add(new KeyValuePair<FulcrumType, string>(FulcrumType.upBound, "上弦周边支承"));
            fulCrumPair.Add(new KeyValuePair<FulcrumType, string>(FulcrumType.downPoint, "下弦点支承"));
            fulCrumPair.Add(new KeyValuePair<FulcrumType, string>(FulcrumType.downBound, "下弦周边支承"));

            gridTypePair = new List<KeyValuePair<GridType, string>>();
            gridTypePair.Add(new KeyValuePair<GridType, string>(GridType.Cones,"三角锥"));
            gridTypePair.Add(new KeyValuePair<GridType, string>(GridType.Square,"四角锥"));
            gridTypePair.Add(new KeyValuePair<GridType, string>(GridType.Cube, "立方体"));
        }
        public static string GetChineseFulcrumType(string typestr)
        {
            var type = (FulcrumType)System.Enum.Parse(typeof(FulcrumType), typestr);
            var y = fulCrumPair.Find(x => x.Key == type).Value;
            return y;
        }
        public static string GetChineseFulcrumType(FulcrumType type)
        {
            var y = fulCrumPair.Find(x => x.Key == type).Value;
            return y;
        }
        public static string[] ResolveFulcrumType(FulcrumType type)
        {
            var list = new List<string>();

            if((type & FulcrumType.downBound) == FulcrumType.downBound)
            {
                list.Add(FulcrumType.downBound.ToString());
            }
            if ((type & FulcrumType.downPoint) == FulcrumType.downPoint)
            {
                list.Add(FulcrumType.downPoint.ToString());

            }
            if ((type & FulcrumType.upBound) == FulcrumType.upBound)
            {
                list.Add(FulcrumType.upBound.ToString());

            }
            if ((type & FulcrumType.upPoint) == FulcrumType.upPoint)
            {
                list.Add(FulcrumType.upPoint.ToString());
            }
            return list.ToArray();
        }
        public static FulcrumType GetFulcrumTypeFromChinese(string chineseFulcrum)
        {
            var y = fulCrumPair.Find(x => x.Value == chineseFulcrum).Key;
            return y;
        }
        public static bool GenerateCreater(string type, out IWireCreater creater)
        {
            bool doubleLayer = false;
            if (type == WireFrameType.OrthonormalTrussTypeTrussTypeSpaceGrid)
                creater = new OrthonormalTrussTypeTrussTypeSpaceGrid();
            else if (type == WireFrameType.TriangularPyramidSpaceGrid)
                creater = new TriangularPyramidSpaceGrid();
            else if (type == WireFrameType.OrthonormalFourAnglePyramidMeshFrame)
                creater = new OrthonormalFourAnglePyramidMeshFrame();
            else if (type == WireFrameType.OrthogonalSlantingFourAnglePyramidSpaceGrid)
                creater = new OrthogonalSlantingFourAnglePyramidSpaceGrid();
            else if (type == WireFrameType.ObliqueFourAnglePyramidSpaceGrid)
                creater = new ObliqueFourAnglePyramidSpaceGrid();
            else if (type == WireFrameType.OrthonormalTrussedTrussTypeSpaceGrid)
                creater = new OrthonormalTrussedTrussTypeSpaceGrid();
            else if (type == WireFrameType.ThreeDirectionIntersectingSpaceGrid)
                creater = new ThreeDirectionIntersectingSpaceGrid();
            else if (type == WireFrameType.FourAnglePyramidSpaceTrussSpaceGrid)
                creater = new FourAnglePyramidSpaceTrussSpaceGrid();
            else if (type == WireFrameType.ChessboardFourPyramidSpaceGrid)
                creater = new ChessboardFourPyramidSpaceGrid();
            else if (type == WireFrameType.OrthonormalFourAnglePyramidMeshFrame_3)
            {
                creater = new OrthonormalFourAnglePyramidMeshFrame();
                doubleLayer = true;
            }

            else if (type == WireFrameType.FourAnglePyramidSpaceTrussSpaceGrid_3)
            {
                creater = new FourAnglePyramidSpaceTrussSpaceGrid();
                doubleLayer = true;
            }

            else if (type == WireFrameType.ChessboardFourPyramidSpaceGrid_3)
            {
                creater = new ChessboardFourPyramidSpaceGrid();
                doubleLayer = true;
            }

            else if (type == WireFrameType.OrthogonalSlantingFourAnglePyramidSpaceGrid_3)
            {
                creater = new OrthogonalSlantingFourAnglePyramidSpaceGrid();
                doubleLayer = true;
            }
            else
            {
                Debug.LogError("new type :" + type);
                creater = null;
            }

            return doubleLayer;
        }

        internal static GridType GridTypeFromChinese(string gridType)
        {
            var y = gridTypePair.Find(x => x.Value == gridType).Key;
            return y;
        }

        internal static string GetChineseGridType(GridType rightType)
        {
            var y = gridTypePair.Find(x => x.Key == rightType).Value;
            return y;
        }
    }
}