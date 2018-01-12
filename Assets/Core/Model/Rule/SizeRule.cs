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
    [System.Serializable]
    public class SizeRule
    {
        public float r_upBar = 0.08f;//杆半径
        public float r_downBar = 0.08f;
        public float r_centerBar = 0.045f;

        public float l_upPoint = 1.59f;//支撑长度
        public float l_upBound = 3.761f;
        public float l_downPoint = 1.59f;
        public float l_downBound = 2.571f;

        public float r_upPoint = 0.4f;//支撑半径
        public float r_upBound = 0.4f; 
        public float r_downPoint = 0.4f;
        public float r_downBound = 0.4f;


        public float r_node = 0.15f;//球半径
    }
}

