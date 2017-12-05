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
/// 网架系统
/// </summary>
public class WireFrameBehaiver : MonoBehaviour {
    public NodeGroupBehaiver nodeGroup;
    public BarGroupBehaiver barGroup;
    public FulcrumsGroupBehaiver fulcrumGroup;
}
