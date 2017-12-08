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
    public class Rule
    {
        public float size1;
        public float size2;
        public int num1;
        public int num2;
        public float height;
        public bool doubleLayer;
        public FulcrumType fulcrumType;
    }
}