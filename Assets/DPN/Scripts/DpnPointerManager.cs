/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace dpn
{
    public class DpnPointerManager : MonoBehaviour
    {
        [SerializeField]
        public DpnBasePointer pointer;

        [SerializeField]
        public DpnBaseRay ray;
        static DpnPointerManager instance;
        static public DpnPointerManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<DpnPointerManager>();
                return instance;
            }
        }


        void OnDestroy()
        {
            instance = null;
        }

        void Awake()
        {
            IsRaycastEnabled = true;
        }

        void OnEnable()
        {
            DpnCameraRig.onPeripheralChanged += OnPeripheralChanged;
            DpnCameraRig.onRecenter += OnRencenter;
        }

        void OnDisable()
        {
            DpnCameraRig.onPeripheralChanged -= OnPeripheralChanged;
            DpnCameraRig.onRecenter -= OnRencenter;
        }

        void Start()
        {

        }

        IEnumerator _SetRaycastEnable()
        {
            yield return new WaitForEndOfFrame();
            pointer.gameObject.SetActive(true);
            ray.gameObject.SetActive(true);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRaycastEnabled
        {
            get;set;
        }

        GameObject _lastGameObejct = null;

        void OnNoInteraction(Ray castRay, Quaternion pointerRotation)
        {
            float z = (DpnCameraRig.Instance._center_eye.nearClipPlane + DpnCameraRig.Instance._center_eye.farClipPlane) * 0.5f;
            Vector3 worldPos = castRay.origin + castRay.direction * z;

            SetPointerMatrix(worldPos, pointerRotation);

            if (_lastGameObejct != null)
            {
                _lastGameObejct = null;
                pointer.transform.localScale = Vector3.one;
                pointer.OnExitObject(_lastGameObejct);
            }
        }

        public void ProcessMove(PointerEventData pointerEvent)
        {
            if (!DpnManager.DPVRPointer || DpnCameraRig.Instance == null)
                return;

            GameObject castObject = pointerEvent.pointerCurrentRaycast.gameObject;

            Ray castRay = DpnCameraRig.Instance.CurrentPeripheral.GetRay();
            // Transform rayOrig = DpnCameraRig._instance.CurrentPeripheral.RayOrig;

            Quaternion pointerRotation = Quaternion.LookRotation(castRay.direction);

            if (castObject && IsRaycastEnabled)
            {
                BaseRaycaster raycaster = pointerEvent.pointerCurrentRaycast.module;
                Ray intersectionRay = new Ray();
                if (raycaster != null)
                {
                    intersectionRay = castRay;
                }
                else if (pointerEvent.enterEventCamera != null)
                {
                    Camera cam = pointerEvent.enterEventCamera;
                    intersectionRay = new Ray(cam.transform.position, cam.transform.forward);
                }

                Vector3 intersectionPos = pointerEvent.pointerCurrentRaycast.worldPosition;
                if (intersectionPos == Vector3.zero)
                {
                    Camera camera = pointerEvent.enterEventCamera;
                    if (camera != null)
                    {
                        float intersectionDistance = pointerEvent.pointerCurrentRaycast.distance + camera.nearClipPlane;
                        intersectionPos = camera.transform.position + intersectionRay.direction * intersectionDistance;
                    }
                }

                SetPointerMatrix(intersectionPos, pointerRotation);

                if (_lastGameObejct == null || _lastGameObejct != castObject)
                {
                    _lastGameObejct = castObject;

                    pointer.OnEnterObject(_lastGameObejct);
                    pointer.transform.localScale = Vector3.zero;
                }
                else
                {
                    pointer.transform.localScale = Vector3.one;
                    pointer.OnHoverObject(_lastGameObejct);
                }

            }
            else
            {
                OnNoInteraction(castRay, pointerRotation);
            }

        }

        void SetPointerMatrix(Vector3 position, Quaternion rotaion)
        {
            if (pointer == null)
                return;

            if (DpnUICamera.instance != null && pointer.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                pointer.transform.position = new Vector3(0, 0, (position - DpnCameraRig.Instance.transform.position).magnitude);
                pointer.transform.rotation = Quaternion.identity;
            }
            else
            {
                pointer.transform.position = position;
                pointer.transform.rotation = rotaion;
            }

        }

        void LateUpdate()
        {
            if (DpnCameraRig.Instance.CurrentPeripheral == null)
                return;
            if(DpnStandaloneInputModule.Instance == null)
            {
                Ray castRay = DpnCameraRig.Instance.CurrentPeripheral.GetRay();
                Quaternion pointerRotation = Quaternion.LookRotation(castRay.direction);
                OnNoInteraction(castRay, pointerRotation);
            }

            Ray r = DpnCameraRig.Instance.CurrentPeripheral.GetRay();
            ray.Set(r.origin, pointer.transform.position);
        }

        IEnumerator _HidePointerOneFrame()
        {
            if (pointer.gameObject.activeSelf)
            {
                pointer.gameObject.SetActive(false);
                yield return new WaitForEndOfFrame();
                pointer.gameObject.SetActive(true);
            }
        }

        IEnumerator _SetRayVisible()
        {
            yield return new WaitForEndOfFrame();
            ray.SetVisible(DpnCameraRig.Instance.CurrentPeripheral.PeripheralType != DPVRPeripheral.None);
        }

        void OnPeripheralChanged(DpnBasePeripheral peripheral)
        {
            StartCoroutine(_SetRayVisible());
            StartCoroutine(_HidePointerOneFrame());
        }

        void OnRencenter()
        {
            pointer.SetVisible(false);
            ray.SetVisible(false);

            StartCoroutine(_OnRecenter());
        }

        IEnumerator _OnRecenter()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            pointer.SetVisible(true);
            ray.SetVisible(true);
        }

        bool isPointerEnabled = true;

        public bool IsPointerEnabled
        {
            get
            {
                return isPointerEnabled;
            }
            set
            {
                pointer.gameObject.SetActive(value);
                isPointerEnabled = value;
            }
            
        }

        bool isRayEnabled = true;

        public bool IsRayEnabled
        {
            get
            {
                return IsRayEnabled;
            }
            set
            {
                ray.gameObject.SetActive(value);
                isRayEnabled = value;
            }
        }
    }
}
