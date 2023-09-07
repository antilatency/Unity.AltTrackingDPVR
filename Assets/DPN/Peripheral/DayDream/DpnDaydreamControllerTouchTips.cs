

using UnityEngine;

namespace dpn
{
    public class DpnDaydreamControllerTouchTips : MonoBehaviour
    {
        Transform knob = null;

        void Start()
        {
            knob = transform.Find("Knob");

            if (knob != null)
                knob.gameObject.SetActive(false);
        }

        void Update()
        {
            if (knob != null)
            {
                if (DpnDaydreamController.TouchDown)
                {
                    knob.gameObject.SetActive(true);
                }
                else if (DpnDaydreamController.TouchUp)
                {
                    knob.gameObject.SetActive(false);
                }
                if (DpnDaydreamController.IsTouching)
                {
                    Vector2 position = DpnDaydreamController.TouchPos;
                    switch (DpnManager.touchPosOrig)
                    {
                        case TouchPosOrig.CENTER:
                            break;
                        case TouchPosOrig.TOP_LEFT:
                            position = position * 2.0f - Vector2.one;
                            break;
                    }

                    knob.localPosition = new Vector3(position.x, position.y, 0.0f);
                }
            }

        }
    }

}