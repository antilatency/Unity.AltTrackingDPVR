

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace dpn
{
    /// <summary>
    /// Main entry point for the DpnStandaloneInputModule API.
    /// </summary>
    /// <seealso cref="UnityEngine.EventSystems.StandaloneInputModule" />
    public class DpnStandaloneInputModule : StandaloneInputModule
    {
        public static DpnStandaloneInputModule _instance;
        public static DpnStandaloneInputModule Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<DpnStandaloneInputModule>();
                }
                return _instance;
            }
        }

        public int touchScrollThreshold = 75;
        public float touchScrollFactor = 0.1f;

        new protected static PointerEventData.FramePressState StateForMouseButton(int buttonId)
        {
            // about controller click button event
            if (buttonId == 0)
            {
                // flip controller
                if(DpnCameraRig.Instance.CurrentPeripheral.PeripheralType == DPVRPeripheral.Flip)
                {
                    bool clickDown = DpnDaydreamController.ClickButtonDown;
                    bool clickUp = DpnDaydreamController.ClickButtonUp;
                    bool triggerDown = DpnDaydreamController.TriggerButtonDown;
                    bool triggerUp = DpnDaydreamController.TriggerButtonUp;

                    if ((clickDown && clickUp) || (triggerDown && triggerUp))
                    {
                        return PointerEventData.FramePressState.PressedAndReleased;
                    }
                    if (clickDown || triggerDown)
                    {
                        return PointerEventData.FramePressState.Pressed;
                    }
                    if (clickUp || triggerUp)
                    {
                        return PointerEventData.FramePressState.Released;
                    }
                }
                // nolo controller
                if(DpnCameraRig.Instance.CurrentPeripheral.PeripheralType == DPVRPeripheral.Nolo)
                {
                    bool leftClickDown = DpnNoloController.ClickButtonDown(NoloDevice.LeftController);
                    bool leftClickUp = DpnNoloController.ClickButtonUp(NoloDevice.LeftController);
                    bool leftTriggerDown = DpnNoloController.TriggerButtonDown(NoloDevice.LeftController);
                    bool leftTriggerUp = DpnNoloController.TriggerButtonUp(NoloDevice.LeftController);

                    if ((leftClickDown && leftClickUp) || (leftTriggerDown && leftTriggerUp))
                    {
                        return PointerEventData.FramePressState.PressedAndReleased;
                    }
                    if (leftClickDown || leftTriggerDown)
                    {
                        return PointerEventData.FramePressState.Pressed;
                    }
                    if (leftClickUp || leftTriggerUp)
                    {
                        return PointerEventData.FramePressState.Released;
                    }

                    bool rightClickDown = DpnNoloController.ClickButtonDown(NoloDevice.RightController);
                    bool rightClickUp = DpnNoloController.ClickButtonUp(NoloDevice.RightController);
                    bool rightTriggerDown = DpnNoloController.TriggerButtonDown(NoloDevice.RightController);
                    bool rightTriggerUp = DpnNoloController.TriggerButtonUp(NoloDevice.RightController);

                    if ((rightClickDown && rightClickUp) || (rightTriggerDown && rightTriggerUp))
                    {
                        return PointerEventData.FramePressState.PressedAndReleased;
                    }
                    if (rightClickDown || rightTriggerDown)
                    {
                        return PointerEventData.FramePressState.Pressed;
                    }
                    if (rightClickUp || rightTriggerUp)
                    {
                        return PointerEventData.FramePressState.Released;
                    }
                }

            }

            bool mouseButtonDown = Input.GetMouseButtonDown(buttonId) | Input.GetKeyDown(KeyCode.Return);
            bool mouseButtonUp = Input.GetMouseButtonUp(buttonId) | Input.GetKeyUp(KeyCode.Return);

            if (mouseButtonDown && mouseButtonUp)
            {
                return PointerEventData.FramePressState.PressedAndReleased;
            }
            if (mouseButtonDown)
            {
                return PointerEventData.FramePressState.Pressed;
            }
            if (mouseButtonUp)
            {
                return PointerEventData.FramePressState.Released;
            }
            return PointerEventData.FramePressState.NotChanged;
        }

        /// <summary>
        /// See BaseInputModule.
        /// </summary>
        public override void Process()
        {
            if (DpnCameraRig.Instance.CurrentPeripheral == null)
                return;
            if (DpnCameraRig.Instance.CurrentPeripheral.PeripheralType == DPVRPeripheral.None && !DpnManager.hmdPointerSwitchable)
            {
                return;
            }
            bool flag = this.SendUpdateEventToSelectedObject();
            if (base.eventSystem.sendNavigationEvents)
            {
                if (!flag)
                {
                    flag |= this.SendMoveEventToSelectedObject();
                }
                if (!flag)
                {
                    this.SendSubmitEventToSelectedObject();
                }
            }
            {
                this.ProcessMouseEvent();
            }
        }
   
        new protected void ProcessMouseEvent()
        {
            this.ProcessMouseEvent(0);
        }

        private readonly PointerInputModule.MouseState m_MouseState = new PointerInputModule.MouseState();

        protected override MouseState GetMousePointerEventData(int id)
        {
            PointerEventData pointerEventData;
            bool pointerData = this.GetPointerData(-1, out pointerEventData, true);
            pointerEventData.Reset();

            // use pointer position instead of mouse position
            Vector2 position = GetPointerScreenPosition();

            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            pointerEventData.delta = position - pointerEventData.position;

            pointerEventData.position = position;
            pointerEventData.scrollDelta = GetScrollDelta();
            pointerEventData.button = PointerEventData.InputButton.Left;
            base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
            RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
            pointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
            this.m_RaycastResultCache.Clear();
            PointerEventData pointerEventData2;
            this.GetPointerData(-2, out pointerEventData2, true);
            this.CopyFromTo(pointerEventData, pointerEventData2);
            pointerEventData2.button = PointerEventData.InputButton.Right;
            PointerEventData pointerEventData3;
            this.GetPointerData(-3, out pointerEventData3, true);
            this.CopyFromTo(pointerEventData, pointerEventData3);
            pointerEventData3.button = PointerEventData.InputButton.Middle;

            this.m_MouseState.SetButtonState(PointerEventData.InputButton.Left, StateForMouseButton(0), pointerEventData);
            this.m_MouseState.SetButtonState(PointerEventData.InputButton.Right, StateForMouseButton(1), pointerEventData2);
            this.m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, StateForMouseButton(2), pointerEventData3);
            return m_MouseState;
        }

        new protected void ProcessMouseEvent(int id)
        {
            PointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(id);
            PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;

            if(DpnPointerManager.Instance.IsRaycastEnabled)
            {
                this.ProcessMousePress(eventData);
                this.ProcessMove(eventData.buttonData);
                this.ProcessDrag(eventData.buttonData);
                this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData);
                this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
                this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
                this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
                if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
                {
                    GameObject eventHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject);
                    ExecuteEvents.ExecuteHierarchy<IScrollHandler>(eventHandler, eventData.buttonData, ExecuteEvents.scrollHandler);
                }
            }

            DpnPointerManager.Instance.ProcessMove(eventData.buttonData);

        }

        Vector2 deltaTouchPos = Vector3.zero;
        Vector2 prevTouchPos = Vector3.zero;

        Vector2 GetPointerScreenPosition()
        {
            Vector2 position;
            if (DpnManager.DPVRPointer)
            {
                Ray ray = DpnCameraRig.Instance.CurrentPeripheral.GetRay();

                Vector3 pos = ray.origin + ray.direction * 10000000.0f;
                position = DpnCameraRig.Instance._center_eye.WorldToScreenPoint(pos);
            }
            else
                position = Input.mousePosition;

            return position;
        }

        protected override void ProcessMove(PointerEventData pointerEvent)
        {

            base.ProcessMove(pointerEvent);
        }

        Vector2 GetScrollDelta()
        {
            Vector2 delta = Vector2.zero;
            var type = DpnCameraRig.Instance.CurrentPeripheral.PeripheralType;
            switch (type)
            {
                case DPVRPeripheral.None:
                    {
#if UNITY_ANDROID && !UNITY_EDITOR
                        if(Input.GetMouseButtonDown(0))
                        {
                            prevTouchPos = Input.mousePosition;
                        }
                        else if(Input.GetMouseButton(0))
                        {
                            delta = (Vector2)Input.mousePosition - prevTouchPos;
                            prevTouchPos = Input.mousePosition;
                            
                        }
#else
                        delta = Input.mouseScrollDelta;
#endif
                        break;
                    }
                case DPVRPeripheral.Flip:
                    {
                        if (DpnDaydreamController.TouchDown)
                        {
                            prevTouchPos = DpnDaydreamController.TouchPos;
                        }
                        else if (DpnDaydreamController.IsTouching)
                        {
                            delta = DpnDaydreamController.TouchPos - prevTouchPos;
                            prevTouchPos = DpnDaydreamController.TouchPos;
                        }
                        delta *= -Screen.height;
                        break;
                    }
                case DPVRPeripheral.Nolo:
                    {
                        break;
                    }
                default:
                    break;
            }

            if (Mathf.Abs(delta.x) < touchScrollThreshold)
            {
                delta.x = 0;
            }
            if (Mathf.Abs(delta.y) < touchScrollThreshold)
            {
                delta.y = 0;
            }
            delta *= touchScrollFactor;
            return delta;
        }
    }
}
