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
            if(instenceObj != null )
            {
                var render = instenceObj.GetComponentInChildren<Renderer>();
                if(render != null && render .material != null)
                {
                    render.material.mainTextureScale = till;
                }
            }
        }
    }
}