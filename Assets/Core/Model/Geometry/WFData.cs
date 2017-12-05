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
/// <summary>
/// [网架的几何数据]
/// 用于生成三维模型或线条模型
/// </summary>
[System.Serializable]
public class WFData
{
    public List<WFNode> wfNodes;//节点信息
    public List<WFBar> wfBars;//连接信息
}
