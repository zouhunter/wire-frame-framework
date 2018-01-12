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
    public abstract class RunTimeObjectHolder : MonoBehaviour
    {
        protected GameObject instenceObj;
        protected GameObject pfb;
        protected PointerEventData pointData;
        protected List<RaycastResult> rayCasts = new List<RaycastResult>();
        public virtual void ShowModel(GameObject pfb)
        {
            if (instenceObj != null)
            {
                if (this.pfb == pfb)
                {
                    instenceObj.SetActive(true);
                }
                else
                {
                    Destroy(instenceObj);
                }
            }

            if (instenceObj == null)
            {
                instenceObj = InstenceUtility.CreateInstence(pfb, transform, Vector3.zero, Quaternion.identity, Vector3.one);
                this.pfb = pfb;
            }
        }

        protected void ResetMaterialTile(Vector2 till)
        {
            if (instenceObj != null)
            {
                var render = instenceObj.GetComponentInChildren<Renderer>();
                if (render != null && render.material != null)
                {
                    render.material.mainTextureScale = till;
                }
            }
        }
        protected void CreateCollider(PrimitiveType primitiveType = PrimitiveType.Cube)
        {
            switch (primitiveType)
            {
                case PrimitiveType.Sphere:
                    break;
                case PrimitiveType.Capsule:
                    break;
                case PrimitiveType.Cylinder:
                    break;
                case PrimitiveType.Cube:
                    gameObject.AddComponent<BoxCollider>();
                    break;
                case PrimitiveType.Plane:
                    break;
                case PrimitiveType.Quad:
                    break;
                default:
                    break;
            }

        }

        protected bool IsMousePointOnUI()
        {
            if (EventSystem.current != null)
            {
                if (pointData == null)
                {
                    pointData = new PointerEventData(EventSystem.current);
                }
                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayCasts);
                foreach (var item in rayCasts)
                {
                    if (item.gameObject.layer == 5)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}