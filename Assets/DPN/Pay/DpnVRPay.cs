using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class DpnVRPay
{

    public delegate void OnIPayResultCallback(int resultCode, IntPtr resultData);

    [DllImport("deepoon_vrpay")]
    public static extern void InitVRPayWithAppID(string appID, string secret);

    [DllImport ("deepoon_vrpay")]
    public static extern void Pay(string tradeno, string amount, string subject, string body);

    [DllImport("deepoon_vrpay")]
    public static extern void SetOnIPayResultCallback(OnIPayResultCallback callback);
}
