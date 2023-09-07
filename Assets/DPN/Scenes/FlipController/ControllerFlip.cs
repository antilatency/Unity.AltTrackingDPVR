using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace dpn
{

    public class ControllerFlip : MonoBehaviour
    {
        Button btn;
        Text txt;
        Text TouchPosition;
        Text Gesture;

        // Use this for initialization
        void Start()
        {
            btn = GameObject.Find("Button").GetComponent<Button>(); 
            txt = btn.transform.Find("Text").GetComponent<Text>();
            TouchPosition= GameObject.Find("Touch Position").GetComponent<Text>();
            Gesture = GameObject.Find("Gesture").GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            TouchPosition.text = DpnDaydreamController.TouchPos.ToString();
            if (DpnDaydreamController.ClickButtonDown)
            {
                txt.text = "ClickButtonDown";
            }
            if (DpnDaydreamController.ClickButtonUp)
            {
                txt.text = "ClickButtonUp";
            }
            
            if (DpnDaydreamController.TriggerButtonDown)
            {
                txt.text = "TriggerButtonDown";
            }
            if (DpnDaydreamController.TriggerButtonUp)
            {
                txt.text = "TriggerButtonUp";
            }
            if (DpnDaydreamController.TouchDown)
            {
                txt.text = "TouchDown";
            }
            if (DpnDaydreamController.TouchUp)
            {
                txt.text = "TouchUp";
            }
            if (DpnDaydreamController.TouchGestureDown)

            {
                Gesture.text = "TouchGestureDown";
            }
            if (DpnDaydreamController.TouchGestureUp)

            {
                Gesture.text = "TouchGestureUp";
            }
            if (DpnDaydreamController.TouchGestureLeft)

            {
                Gesture.text = "TouchGestureLeft";
            }
            if (DpnDaydreamController.TouchGestureRight)

            {
                Gesture.text = "TouchGestureRight";
            }





        }
    }
}
