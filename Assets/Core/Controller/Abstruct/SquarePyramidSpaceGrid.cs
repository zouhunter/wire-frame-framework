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
    /// 四角锥网架
    /// </summary>
    public abstract class SquarePyramidSpaceGrid :WireFrameGenerater{

        public override bool CanCreate(Rule clamp)
        {
            if (clamp.layer == 0 || clamp.layer > 2) return false;
            if (clamp.size1 < clamp.num1 || clamp.size2 < clamp.num2) return false;
            if (clamp.size1 < 1 || clamp.size2 < 1) return false;
            return true;
        }
        protected Vector3 DoubleLayerPos(Vector3 vector3, float height)
        {
            vector3 = Quaternion.Euler(Vector3.right * 180) * vector3;
            vector3 += Vector3.down * height;
            return vector3;
        }
    }
  
}