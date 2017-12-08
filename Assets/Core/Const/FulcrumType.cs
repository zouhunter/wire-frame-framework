using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum FulcrumType  {
   upPoint = 1,// = "上弦点支承";
   upBound = 1<<1, //= "上弦周边支承";
   downPoint = 1<<2,// = "下弦点支承";
   downBound = 1<<3, //= "下弦周边支承";//Chord
}
