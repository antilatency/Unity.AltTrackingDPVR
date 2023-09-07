/************************************************************************************

Copyright   :   Copyright 2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/
using UnityEngine;
using System.Collections;

namespace dpn
{
    public class DpnBaseRay : MonoBehaviour
    {
        Vector3 orig = Vector3.zero;
        Vector3 direction = Vector3.forward;

        virtual public void Set(Vector3 start, Vector3 end)
        {
            orig = start;
            direction = (end - start).normalized;
            if (direction == Vector3.zero)
                return;

            transform.position = orig;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        IEnumerator _SetVisible()
        {
            yield return new WaitForEndOfFrame();
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
                renderer.enabled = true;
        }

        virtual public void SetVisible(bool value)
        {
            if (value)
            {
                StartCoroutine(_SetVisible());
            }
            else
            {
                Renderer renderer = GetComponent<Renderer>();
                if (renderer != null)
                    renderer.enabled = value;
            }
        }
    }
}
