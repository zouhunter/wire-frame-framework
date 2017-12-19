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

    [CreateAssetMenu(menuName = "创建/FrameShow")]
    public class FrameShowObject : ScriptableObject
    {
        public ModelRule modelRule;
        public SizeRule sizeRule;
        public LineRule lineRule;
        public List<BarInfoHolder> barInfos;
        public List<NodeInfoHolder> nodeInfos;
    }

    [System.Serializable]
    public class BarInfoHolder
    {
        public string type;
        public BarInfo info;
    }

    [System.Serializable]
    public class NodeInfoHolder
    {
        public string type;
        public NodeInfo nodeInfo;
    }
}