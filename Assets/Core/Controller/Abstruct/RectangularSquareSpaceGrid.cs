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
       
        protected override WFData GenerateWFDataUnit(FrameRule clamp)
        {
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;
            return CalcuteUtility.QuadrangularSpaceGrid_Unit(x_Size, y_Size, clamp.height);
        }

        public override List<WFFul> CalcFulcrumPos(FrameRule clamp)
        {
            var startPos = -new Vector3(clamp.size1, -clamp.height, clamp.size2) * 0.5f;
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;

            List<WFFul> positions = new List<WFFul>();
            for (int i = 0; i < clamp.num1; i++)
            {
                for (int j = 0; j < clamp.num2; j++)
                {
                    switch (clamp.fulcrumType)
                    {
                        case FulcrumType.upBound:
                            CalcuteUtility.RecordQuadBound(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, positions, clamp.fulcrumType);
                            break;
                        case FulcrumType.downBound:
                            if (!clamp.doubleLayer){
                                CalcuteUtility.RecordQuadrAngular(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, clamp.height, positions, clamp.fulcrumType);
                            }
                            else{
                                CalcuteUtility.RecordQuadBound(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, positions, clamp.fulcrumType, clamp.height);
                            }
                            break;
                        case FulcrumType.upPoint:
                            CalcuteUtility.RecordQuadPoint(i,j,clamp.num1,clamp.num2, startPos, x_Size, y_Size, positions, clamp.fulcrumType);
                            break;
                        case FulcrumType.downPoint:
                            if (!clamp.doubleLayer){
                                CalcuteUtility.RecordQuadAngularPoint(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, clamp.height, positions, clamp.fulcrumType);
                            }
                            else
                            {
                                CalcuteUtility.RecordQuadPoint(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, positions, clamp.fulcrumType, clamp.height);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
            return positions;
        }
    }
}