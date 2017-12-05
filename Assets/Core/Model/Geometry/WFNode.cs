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
/// [网架节点几何数据]
/// 用于记录节点的几何数据
/// </summary>
[System.Serializable]
public class WFNode
{
    public string m_id;
    public Vector3 position;
    public WFNode(Vector3 position)
    {
        this.position = position;
        m_id = System.Guid.NewGuid().ToString();
    }
}
