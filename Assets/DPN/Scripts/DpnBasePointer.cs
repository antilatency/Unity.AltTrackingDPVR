/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace dpn
{
    public class DpnBasePointer : MonoBehaviour
	{
        virtual public void OnEnterObject(GameObject targetObject) { }

        virtual public void OnHoverObject(GameObject targetObject) { }

        virtual public void OnExitObject(GameObject targetObject) { }

        virtual public void OnClickDown() { }

        virtual public void OnClickUp() { }

        public Vector2 GetScreenPosition()
		{
			return DpnCameraRig.WorldToScreenPoint(transform.position);
		}

        public Vector3 GetWorldPosition()
        {
            return transform.position;
        }

        virtual public void SetVisible(bool value)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer)
                renderer.enabled = value;
        }

        public bool IsVisible
        {
            set
            {
                SetVisible(value);
            }
            get
            {
                Renderer renderer = GetComponent<Renderer>();
                return renderer != null ? renderer.enabled : false;
            }
        }
	}
}
