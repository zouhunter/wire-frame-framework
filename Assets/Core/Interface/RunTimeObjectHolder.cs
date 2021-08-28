using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace WireFrame
{
    public abstract class RunTimeObjectHolder : MonoBehaviour
    {
        protected GameObject instenceObj;
        protected GameObject pfb;
        protected PointerEventData pointData;
        protected List<RaycastResult> rayCasts = new List<RaycastResult>();
        protected float timer;
        protected Stack<GameObject> m_pool;

        public virtual void ShowModel(GameObject pfb, Stack<GameObject> pool)
        {
            m_pool = pool;

            if (instenceObj != null)
            {
                SaveBackInstance();
            }

            if (pfb != null && this)
            {
                instenceObj = GetInstance(pfb);
            }
            
            if(!instenceObj)
            {
                Debug.LogWarning("Failed Create Instence!");
            }
        }

        protected GameObject GetInstance(GameObject pfb)
        {
            GameObject instance = null;
            if(m_pool != null && m_pool.Count > 0)
            {
                for (int i = 0; i < m_pool.Count; i++)
                {
                    instance = m_pool.Pop();
                    if (instance != null)
                        break;
                }
            }
            if (!instance && pfb)
            {
                instance = Instantiate<GameObject>(pfb);
            }

            if(instance)
            {
                instance.transform.SetParent(transform);
                instance.transform.localRotation = Quaternion.identity;
                instance.transform.localPosition = Vector3.zero;
                instance.transform.localScale = Vector3.one;
                instance.gameObject.SetActive(true);
            }
            return instance;
        }

        protected void SaveBackInstance()
        {
            if (instenceObj)
            {
                if (m_pool == null)
                {
                    GameObject.Destroy(instenceObj);
                }
                else
                {
                    m_pool.Push(instenceObj);
                    instenceObj.gameObject.SetActive(false);
                }
            }
            instenceObj = null;
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
        protected virtual bool HaveExecuteTwince(ref float timer, float time = 0.5f)
        {
            if (Time.time - timer < time)
            {
                timer = 0;
                return true;
            }
            else
            {
                timer = Time.time;
                return false;
            }
        }

        protected virtual void OnDestroy()
        {
            SaveBackInstance();
        }
    }
}