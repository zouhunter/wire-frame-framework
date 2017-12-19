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
    /// 斜放桁架型网架
    /// </summary>
    public abstract class ObliqueTrussTypeSpaceGrid : TrussTypeSpaceGrid
    {
        protected override WFData GenerateWFDataUnit(FrameRule clamp)
        {
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;
            WFData data = CalcuteUtility.TrussTypeDiamondSpaceGrid_Unit(x_Size, y_Size, clamp.height);
            return data;
        }

        public override List<WFFul> CalcFulcrumPos(FrameRule clamp)
        {
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;
            var startPos = -new Vector3(clamp.size1, -clamp.height, clamp.size2 - y_Size) * 0.5f;

            List<WFFul> positions = new List<WFFul>();
            for (int i = 0; i < clamp.num1; i++)
            {
                for (int j = 0; j < clamp.num2; j++)
                {
                    switch (clamp.fulcrumType)
                    {
                        case FulcrumType.downBound:
                            CalcuteUtility.RecordQuadXieBound(i, j, clamp.num1, clamp.num2, startPos, x_Size, y_Size, positions, FulcrumType.downBound, clamp.height);
                            break;
                        default:
                            break;
                    }

                }
            }
            if (clamp.fulcrumType == FulcrumType.downBound && clamp.doubleLayer)
            {
                foreach (var item in positions)
                {
                    item.AppendPosition(clamp.height * Vector3.down);
                }
            }
            return positions;
        }
    }

}
