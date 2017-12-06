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

public enum BoundConnectType {
    XAxisOnly,//仅同在x轴向上
    YAxisOnly,//仅同在y轴向上
    XOrYAxis,//仅在x或主轴向上
    NoXAxis,//不在x轴向上
    NoYAxis,//不在y轴向上
    NoXAndYAxis//不在x和y轴向上
}
