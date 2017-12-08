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
using System;
namespace WireFrame
{

    /// <summary>
    /// 正交正放桁架型网架
    /// </summary>
    public class OrthonormalTrussTypeTrussTypeGridFrame : WireFrameGenerater
    {
        public override bool CanCreate(Rule clamp)
        {
            return true;
        }
        protected override WFData GenerateWFData(Rule clamp)
        {
            var startPos = -new Vector3(clamp.size1, clamp.height, clamp.size2) * 0.5f;
            WFData wfData = new WFData();
            float x_Size = clamp.size1 / clamp.num1;
            float y_Size = clamp.size2 / clamp.num2;
            for (int i = 0; i < clamp.num1; i++)
            {
                for (int j = 0; j < clamp.num2; j++)
                {
                    WFData data = CalcuteUtility.TrussTypeGridFrame_Unit(x_Size, y_Size, clamp.height);
                    data.AppendPosition(startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward);
                    wfData.InsertData(data);
                }
            }

            return wfData;
        }

        protected override WFData GenerateWFDataUnit(Rule clamp)
        {
            throw new NotImplementedException();
        }
        public override List<Vector3> CalcFulcrumPos(Rule clamp)
        {
            return new List<Vector3>();
        }
    }
}