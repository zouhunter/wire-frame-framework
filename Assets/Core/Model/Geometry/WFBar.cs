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
        public string m_id;
        public string m_type;
        public string m_fromNodeId;
        public string m_toNodeId;
        public WFBar(string fromID, string toID, string type = "")
        {
            this.m_type = type;
            this.m_fromNodeId = fromID;
            this.m_toNodeId = toID;
            m_id = System.Guid.NewGuid().ToString();
        }
        public bool IsSame(WFBar otherBar)
        {
            if (otherBar.m_fromNodeId == m_fromNodeId && otherBar.m_toNodeId == m_toNodeId)
                return true;
            if (otherBar.m_fromNodeId == m_toNodeId && otherBar.m_toNodeId == m_fromNodeId)
                return true;
            return false;
        }

        internal WFBar Copy()
        {
            return new WFBar(m_fromNodeId, m_toNodeId, m_type);
        }
    }
}