using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dpn3DofArmModel : MonoBehaviour {

    // Use this for initialization
    
    [SerializeField]
    private Transform rightElbow;

    [SerializeField]
    private Transform rightWrist;

    [SerializeField]
    private Transform leftElbow;

    [SerializeField]
    private Transform leftWrist;

    [SerializeField]
    private Transform controller;

    public int interactiveHand = 0;

    void Start () {

    }

	// Update is called once per frame
	void Update () {
        Quaternion cameraRigRot = dpn.DpnCameraRig.Instance.transform.rotation;

        float y = dpn.DpnCameraRig.Instance.GetPose().eulerAngles.y;
        transform.localRotation = cameraRigRot * Quaternion.Euler(0, y, 0);

        if (interactiveHand == 0)
        {// right hand
            rightElbow.transform.rotation = cameraRigRot * dpn.DpnDaydreamController.Orientation;

            controller.rotation = rightWrist.transform.rotation;
            controller.position = rightWrist.transform.position;
        }
        else
        {// left hand
            leftElbow.transform.rotation = cameraRigRot * dpn.DpnDaydreamController.Orientation;

            controller.rotation = leftWrist.transform.rotation;
            controller.position = leftWrist.transform.position;
        }
    }
}
