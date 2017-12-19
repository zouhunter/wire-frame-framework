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

    public static class InstenceUtility
    {

        public static GameObject CreateInstence(GameObject pfb,Transform parent,Vector3 localPos,Quaternion localRot,Vector3 localSize)
        {
            var instenceObj = Object. Instantiate(pfb);
            instenceObj.transform.SetParent(parent);//不是ui
            instenceObj.transform.localPosition = localPos;
            instenceObj.transform.localRotation = localRot;
            instenceObj.transform.localScale = localSize;
            return instenceObj;
        }
    }
}