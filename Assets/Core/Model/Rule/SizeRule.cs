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
        public float r_upBar;//杆半径
        public float r_downBar;
        public float r_centerBar;

        public float l_upPoint;//支撑长度
        public float l_upBound;
        public float l_downPoint;
        public float l_downBound;

        public float r_upPoint;//支撑半径
        public float r_upBound; 
        public float r_downPoint;
        public float r_downBound;


        public float r_node;//球半径
    }
}

