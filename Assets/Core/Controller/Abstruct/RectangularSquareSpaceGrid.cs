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

namespace WireFrame
{
    /// <summary>
    /// 正放四角锥网架
    /// </summary>
    public abstract class RectangularSquareSpaceGrid : SquarePyramidSpaceGrid
    {
       
        protected override WFData GenerateWFDataUnit(Rule clamp)
        {
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;
            return CalcuteUtility.QuadrangularSpaceGrid_Unit(x_Size, y_Size, clamp.height);
        }

        public override List<Vector3> CalcFulcrumPos(Rule clamp)
        {
            var startPos = -new Vector3(clamp.size1, -clamp.height, clamp.size2) * 0.5f;
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;

            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < clamp.num1; i++)
            {
                for (int j = 0; j < clamp.num2; j++)
                {
                    switch (clamp.fulcrumType)
                    {
                        case FulcrumType.upBound:
                            CalcuteUtility.RecordQuadBound(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, positions);
                            break;
                        case FulcrumType.downBound:
                            if (!clamp.doubleLayer)
                            {
                                CalcuteUtility.RecordQuadrAngular(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, clamp.height, positions);
                            }
                            else
                            {
                                CalcuteUtility.RecordQuadBound(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, positions);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }

            if (clamp.doubleLayer && clamp.fulcrumType == FulcrumType.downBound)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    positions[i] = DoubleLayerPos(positions[i], clamp.height);
                }
            }
            return positions;
        }
    }
}