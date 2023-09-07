/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

// DpnPay not implemented on PC
#if UNITY_ANDROID
using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace dpn
{
public class DpnPay 
{
    public const string LibDpn = "DpnUnity";

    public delegate void loginCallback(dpnnPayResult status, IntPtr userName, IntPtr nickName, IntPtr userId, bool isOneKeyRegister);

    public delegate void changeOneKeyRegisterCallback(dpnnPayResult status, IntPtr userName, IntPtr nickName, IntPtr userId);

    [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
    public extern static bool DpnuLogin(IntPtr activity, dpnnPayUiType type, loginCallback callback);

    [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr DpnuGetLoginUserName(IntPtr activity);

    [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr DpnuGetLoginNickName(IntPtr activity);

    [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr DpnuGetLoginUserId(IntPtr activity);

    [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
    public extern static bool DpnuLogout(IntPtr activity);

    [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
    public extern static bool DpnuIsOneKeyRegister(IntPtr activity);

    [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
    public extern static bool DpnuChangeOneKeyRegister(IntPtr activity, dpnnPayUiType type, changeOneKeyRegisterCallback callback);
}

public enum dpnnPayUiType 
{
    DPNN_PAY_UI_DEFAULT=0, // default-VR UI
    DPNN_PAY_UI_2D=1,
    DPNN_PAY_UI_VR=2,
    DPNN_PAY_UI_COUNT
};

public enum dpnnPayResult
{
    DPNN_PAY_RESULT_SUCCESS = 0,
    DPNN_PAY_RESULT_FAIL = 1
};
}
#endif
