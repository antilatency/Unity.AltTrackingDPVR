
using System.Collections;
using UnityEngine;
namespace dpn
{
    public class DpnVolumeTips : MonoBehaviour
    {

        Material material;
        Texture[] textures = new Texture[16];

        public GameObject tips;

        public float duration = 3.0f;

        void Awake()
        {
            if (tips == null)
                return;

            tips.SetActive(false);

            Renderer meshRenderer = tips.GetComponent<Renderer>();
            material = meshRenderer.material;

            for (int i = 0; i < textures.Length; ++i)
            {
                textures[i] = Resources.Load<Texture2D>("DPN/Volume/icon_sound_" + i.ToString());
            }
        }

        void OnVolumeUpdate()
        {
            IsVisible = (true);
        }

        IEnumerator delayHide = null;

        void Hide()
        {
            if (!IsVisible)
                return;

            if (delayHide != null)
            {
                StopCoroutine(delayHide);
                delayHide = null;
            }

            tips.SetActive(false);
        }

        public bool IsVisible
        {
            get
            {
                return (tips != null && tips.activeSelf);
            }
            set
            {
                tips.SetActive(value);

                if (value)
                {
                    if (delayHide != null)
                        StopCoroutine(delayHide);

                    delayHide = DelayHide();
                    StartCoroutine(delayHide);

                    textureIndex = DpnAudioManager.MusicVolume;
                    material.SetTexture("_MainTex", textures[textureIndex]);
                }
                else
                {
                    if (delayHide != null)
                    {
                        StopCoroutine(delayHide);
                        delayHide = null;
                    }
                }
            }
        }

        IEnumerator DelayHide()
        {
            yield return new WaitForSeconds(duration);
            delayHide = null;
            Hide();
        }


        int textureIndex = 0;

        void Update()
        {
            if (!IsVisible)
                return;

            if (textureIndex != DpnAudioManager.MusicVolume)
            {
                textureIndex = DpnAudioManager.MusicVolume;
                material.SetTexture("_MainTex", textures[textureIndex]);
            }
        }


        void OnKeyVolumeUpDown(string param)
        {
            OnVolumeUpdate();
        }

        void OnKeyVolumeDownDown(string param)
        {
            OnVolumeUpdate();
        }
    }
}
