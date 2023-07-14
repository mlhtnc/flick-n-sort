using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NotDecided
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField]
        private Button soundButton;

        [SerializeField]
        private Button hapticButton;

        [SerializeField]
        private Sprite[] icons;

        private bool isSoundOn;

        private bool isHapticOn;

        private void Start()
        {
            isSoundOn = true;
            isHapticOn = true;

            soundButton.image.sprite = icons[0];
            hapticButton.image.sprite = icons[2];

            soundButton.onClick.AddListener(() => {
                int iconIndex;
                if(isSoundOn)
                {
                    iconIndex = 1;
                    AudioManager.Instance.Mute();
                }
                else
                {
                    iconIndex = 0;                    
                    AudioManager.Instance.Unmute();
                }

                soundButton.image.sprite = icons[iconIndex];
                isSoundOn = !isSoundOn;
            });

            hapticButton.onClick.AddListener(() => {
                int iconIndex;
                if(isHapticOn)
                {
                    iconIndex = 3;
                    VibrationManager.Instance.DisableVibration();
                }
                else
                {
                    VibrationManager.Instance.EnableVibration();
                    iconIndex = 2;                    
                }

                hapticButton.image.sprite = icons[iconIndex];
                isHapticOn = !isHapticOn;
            });
        }
    }
}
