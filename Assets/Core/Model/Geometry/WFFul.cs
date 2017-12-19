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
    /// 网架杆件的支撑
    /// </summary>
    [System.Serializable]
    public class WFFul
    {
        private string _id;
        private string _type;
        private Vector3 _position;

        public string id { get { return _id; } private set { _id = value; } }
        public string type { get { return _type; } private set { _type = value; } }
        public Vector3 position { get { return _position; } private set { _position = value; } }
        public WFFul(Vector3 position, FulcrumType type)
        {
            switch (type)
            {
                case FulcrumType.upPoint:
                    this.type = FulcrumPosType.upPoint;// "上弦点支承";
                    break;
                case FulcrumType.upBound:
                    this.type = FulcrumPosType.upBound; // "上弦周边支承";
                    break;
                case FulcrumType.downPoint:
                    this.type = FulcrumPosType.downPoint;// "下弦点支承";
                    break;
                case FulcrumType.downBound:
                    this.type = FulcrumPosType.downBound; //"下弦周边支承";
                    break;
                default:
                    break;
            }
            this.position = position;
            this.id = System.Guid.NewGuid().ToString();
        }
        public WFFul(Vector3 postion, string type)
        {
            this.position = position;
            this.type = type;
            this.id = System.Guid.NewGuid().ToString();
        }

        internal WFFul Copy()
        {
            return new WFFul(position, type);
        }

        public void DoubleLayerPos(float height)
        {
            position = Quaternion.Euler(Vector3.right * 180) * position;
            position += Vector3.down * height;
        }
        public void AppendPosition(Vector3 vector3)
        {
            position += vector3;
        }
    }
}