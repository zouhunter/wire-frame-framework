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
using System;
namespace WireFrame
{

    /// <summary>
    /// 网架杆件的几何数据
    /// </summary>
    [System.Serializable]
    public class WFBar
    {
        private string _id;
        private string _type;
        private string _fromNodeId;
        private string _toNodeId;

        public string id { get { return _id; }private set { _id = value;  } }
        public string type { get { return _type; }private set { _type = value;  } }
        public string fromNodeId { get { return _fromNodeId; }private set { _fromNodeId = value;  } }
        public string toNodeId { get { return _toNodeId; }private set { _toNodeId = value;  } }
        public WFBar(string fromID, string toID, string type = "")
        {
            this._type = type;
            this._fromNodeId = fromID;
            this._toNodeId = toID;
            _id = System.Guid.NewGuid().ToString();
        }
        public bool IsSame(WFBar otherBar)
        {
            if (otherBar._fromNodeId == _fromNodeId && otherBar._toNodeId == _toNodeId)
                return true;
            if (otherBar._fromNodeId == _toNodeId && otherBar._toNodeId == _fromNodeId)
                return true;
            return false;
        }
        public void ResetToNodeID(string toNodeId)
        {
            this.toNodeId = toNodeId;
        }
        public void ResetFromNodeID(string fromNodeId)
        {
            this.fromNodeId = fromNodeId;
        }
        internal WFBar Copy()
        {
            return new WFBar(_fromNodeId, _toNodeId, _type);
        }
    }
}