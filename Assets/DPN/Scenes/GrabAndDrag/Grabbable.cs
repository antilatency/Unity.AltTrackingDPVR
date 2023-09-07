using dpn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Grabbable : MonoBehaviour
{
    //ץȡ
    private bool isGrabbed = false;
    //�ͷ�
    private bool isReleased = false;
    //�ƶ��ٶ�
    private float moveSpeed = 10.0f;
    //�Ƿ�����ƶ�
    private bool isSel = false;
    //���߽���
    private bool isEnter = false;
    //����
    private float fx;
    //����
    private float fx02;
    //�Ƕ�
    float angle;
    //��˼��㷽��
    Vector3 offe;
    private void Update()
    {
        if (DpnDaydreamController.TriggerButtonDown && isSel)
        {
            if (!isGrabbed && !isReleased)
            {
                Grab();
            }
        }
        else if (DpnDaydreamController.TriggerButtonUp)
        {
            if (isGrabbed && !isReleased)
            {
                Release();
            }
        }

        if (isGrabbed && !isReleased )
        {
            Vector3 vec= DpnPointerManager.Instance.pointer.transform.position - DpnCameraRig.Instance.transform.position;

            Vector3 fangx01 = vec.normalized * fx;
            Vector3 fangx02 = Quaternion.AngleAxis(angle,offe.normalized)* vec.normalized * fx02;
            Vector3 fangx = fangx01 + fangx02;

            if (Vector3.Distance(this.transform.position, fangx) > 0.05F)
            {
                Vector3 newPosition = Vector3.Lerp(transform.position, fangx, moveSpeed * Time.deltaTime);
                transform.position = newPosition;
            }

        }
    }

    public void Grab()
    {
        Vector3 vec02 = DpnPointerManager.Instance.pointer.transform.position - DpnCameraRig.Instance.transform.position;
        Vector3 vec03 = this.transform.position-DpnPointerManager.Instance.pointer.transform.position;

        angle = Vector3.Angle(vec02, vec03);
        offe = Vector3.Cross(vec02, vec03);
        fx = vec02.magnitude;
        fx02 = vec03.magnitude;
        isGrabbed = true;
        isReleased = false;
    }

    public void Release()
    {
        isGrabbed = false;
        isReleased = false;
        if (!isEnter)
        {
            isSel = false;
        }
    }

    public bool IsGrabbed()
    {
        return isGrabbed;
    }

    public void EnterSeclectObj()
    {
        isSel = true;
        isEnter = true;
    }
    public void ExitSelectObj()
    {
        if (!isGrabbed)
        {
            isSel = false;
        }
        isEnter = false;
    }
}
