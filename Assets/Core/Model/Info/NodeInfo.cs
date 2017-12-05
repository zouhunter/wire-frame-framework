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
/// 节点信息
/// 螺栓球、套筒、锥头、封板、高强螺栓、插销、杆件等；
/// 焊接球、加劲板、插板、杆件、相贯线等。
/// </summary>
[System.Serializable]
public class NodeInfo {
    public NodeType nodeType;
}
