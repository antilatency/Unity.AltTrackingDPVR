using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace dpn
{

    public class NoloControllerTest : MonoBehaviour
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
            if (DpnNoloController.ClickButtonUp(NoloDevice.LeftController))
            {
                txt.text = "ClickButtonUp";
            }

            if (DpnNoloController.TriggerButtonDown(NoloDevice.LeftController))
            {
                txt.text = "TriggerButtonDown";
            }
            if (DpnNoloController.TriggerButtonUp(NoloDevice.LeftController))
            {
                txt.text = "TriggerButtonUp";
            }
            if (DpnNoloController.TouchDown(NoloDevice.LeftController))
            {
                txt.text = "TouchDown";
            }
            if (DpnNoloController.TouchUp(NoloDevice.LeftController))
            {
                txt.text = "TouchUp";
            }
            if (DpnNoloController.SystemButtonDown(NoloDevice.LeftController))
            {
                txt.text = "SystemButtonDown";
            }
            if (DpnNoloController.SystemButtonUp(NoloDevice.LeftController))
            {
                txt.text = "SystemButtonUp";
            }
            if (DpnNoloController.MenuButtonDown(NoloDevice.LeftController))
            {
                txt.text = "MenuButtonDown";
            }
            if (DpnNoloController.MenuButtonUp(NoloDevice.LeftController))
            {
                txt.text = "MenuButtonUp";
            }
            if (DpnNoloController.ClickButtonDown(NoloDevice.LeftController))
            {
                txt.text = "ClickButtonDown";
            }
            if (DpnNoloController.ClickButtonUp(NoloDevice.LeftController))
            {
                txt.text = "ClickButtonUp";
            }
        }
    }
}
