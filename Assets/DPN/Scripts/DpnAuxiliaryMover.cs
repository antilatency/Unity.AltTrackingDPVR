/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;

namespace dpn
{
    public class DpnAuxiliaryMover : MonoBehaviour
    {
#if UNITY_EDITOR
        Vector3 _prevMousePos;
        Transform _transformCenterEye = null;

        void Start()
        {
            // as script component of DpnCameraRig
            _transformCenterEye = gameObject.transform.Find("TrackingSpace/CenterEyeAnchor");
        }

        [SerializeField]
        bool altKey = false;

        [SerializeField]
        MouseButton mouseButton = MouseButton.Left;

        public enum MouseButton
        {
            Left,
            Right,
            Middle,
        }

        private void Update()
        {
            if (transform == null)
                return;

            if (_transformCenterEye == null)
                return;

            if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                if (Input.GetMouseButtonDown((int)mouseButton))
                {
                    if (_prevMousePos != Input.mousePosition)
                    {
                        _prevMousePos = Input.mousePosition;
                    }
                }

                if (Input.GetMouseButton(1))
                {
                    Vector3 offsetMousePos = Input.mousePosition - _prevMousePos;

                    if(offsetMousePos != Vector3.zero)
                    {
                        transform.eulerAngles = transform.eulerAngles + new Vector3(-offsetMousePos.y * 0.1f, offsetMousePos.x * 0.1f, 0);
                    }

                    _prevMousePos = Input.mousePosition;
                }
            }
        }
#endif
    }
}