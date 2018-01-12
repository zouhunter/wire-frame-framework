using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace WireFrame
{

    public class WireFrameUtility 
    {
        private static List< KeyValuePair<FulcrumType, string> > fulCrumPair;
        static WireFrameUtility()
        {
            fulCrumPair = new List<KeyValuePair<FulcrumType, string>>();
            fulCrumPair.Add(new KeyValuePair<FulcrumType, string>(FulcrumType.upPoint, "上弦点支承"));
            fulCrumPair.Add(new KeyValuePair<FulcrumType, string>(FulcrumType.upBound, "上弦周边支承"));
            fulCrumPair.Add(new KeyValuePair<FulcrumType, string>(FulcrumType.downPoint, "下弦点支承"));
            fulCrumPair.Add(new KeyValuePair<FulcrumType, string>(FulcrumType.downBound, "下弦周边支承"));
        }
        public static string GetChineseFulcrumType(FulcrumType type)
        {
            var y = fulCrumPair.Find(x => x.Key == type).Value;
            return y;
        }
        public static FulcrumType GetFulcrumTypeFromChinese(string chineseFulcrum)
        {
            var y = fulCrumPair.Find(x => x.Value == chineseFulcrum).Key;
            return y;
        }
    }
}