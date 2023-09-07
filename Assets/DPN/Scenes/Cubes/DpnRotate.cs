using UnityEngine;
using System.Collections;

public class DpnRotate : MonoBehaviour
{

    Quaternion rotated = Quaternion.identity;
    Vector3 moved = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.rotation *= Quaternion.Inverse(rotated);
            rotated = Quaternion.identity;
            transform.position += -moved;
            moved = Vector3.zero;
            return;
        }

        Quaternion value = Quaternion.identity;
        if (Input.GetKey(KeyCode.W) || dpn.DpnDaydreamController.ButtonClickUp)
        {
            value *= Quaternion.Euler(new Vector3(-10.0f / 60.0f, 0, 0));
        }
        if (Input.GetKey(KeyCode.S) || dpn.DpnDaydreamController.ButtonClickDown)
        {
            value *= Quaternion.Euler(new Vector3(10.0f / 60.0f, 0, 0));
        }
        if (Input.GetKey(KeyCode.A) || dpn.DpnDaydreamController.ButtonClickLeft)
        {
            value *= Quaternion.Euler(new Vector3(0, -10.0f / 60.0f, 0));
        }
        if (Input.GetKey(KeyCode.D) || dpn.DpnDaydreamController.ButtonClickRight)
        {
            value *= Quaternion.Euler(new Vector3(0, 10.0f / 60.0f, 0));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            value *= Quaternion.Euler(new Vector3(0, 0, 10.0f / 60.0f));
        }
        if (Input.GetKey(KeyCode.E))
        {
            value *= Quaternion.Euler(new Vector3(0, 0, -10.0f / 60.0f));
        }
        if (value != Quaternion.identity)
        {
            rotated *= value;
            transform.rotation *= value;
            return;
        }


        Vector3 move = Vector3.zero;
 
        if (Input.GetKey(KeyCode.I)||  dpn.DpnDaydreamController.TouchDownGestureUp)
        {
            MoveForward();
            return;
        }
        if (Input.GetKey(KeyCode.K) ||  dpn.DpnDaydreamController.TouchDownGestureDown)
        {
            MoveBack();
            return;
        }
        if (Input.GetKey(KeyCode.J) ||  dpn.DpnDaydreamController.TouchDownGestureLeft)
        {
            MoveLeft();
            return;
        }
        if (Input.GetKey(KeyCode.L) || dpn.DpnDaydreamController.TouchDownGestureRight)
        {
            MoveRight();
            return;
        }
        if (Input.GetKey(KeyCode.U))
        {
            move += new Vector3(0, 10.0f / 600.0f, 0);
        }
        if (Input.GetKey(KeyCode.O))
        {
            move += new Vector3(0, -10.0f / 600.0f, 0);
        }
        if (move != Vector3.zero)
        {
            moved += move;
            transform.position += move;
            return;
        }
    }


    void MoveForward()
    {
        transform.Translate(Vector3.forward * 4f * Time.deltaTime);
    }
    void MoveBack()
    {
        transform.Translate(Vector3.back * 4f * Time.deltaTime);
    }
    void MoveLeft()
    {
        transform.Translate(Vector3.left * 4f * Time.deltaTime);
    }
    void MoveRight()
    {
        transform.Translate(Vector3.right * 4f * Time.deltaTime);
    }
}
