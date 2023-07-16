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
            isSoundOn = SaveManager.GetData("sound") == 1 ? true : false;
            isHapticOn = SaveManager.GetData("haptic") == 1 ? true : false;

            if(isSoundOn)
            {
                soundButton.image.sprite = icons[0];
                AudioManager.Instance.Unmute();
            }
            else
            {
                soundButton.image.sprite = icons[1];
                AudioManager.Instance.Mute();
            }

            if(isHapticOn)
            {
                hapticButton.image.sprite = icons[2];
                VibrationManager.Instance.EnableVibration();
            }
            else
            {
                hapticButton.image.sprite = icons[3];
                VibrationManager.Instance.DisableVibration();
            }

            soundButton.onClick.AddListener(() => {
                int iconIndex;
                if(isSoundOn)
                {
                    iconIndex = 1;
                    AudioManager.Instance.Mute();
                    SaveManager.SaveData("sound", 0);
                }
                else
                {
                    iconIndex = 0;                    
                    AudioManager.Instance.Unmute();
                    SaveManager.SaveData("sound", 1);
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
                    SaveManager.SaveData("haptic", 0);
                }
                else
                {
                    VibrationManager.Instance.EnableVibration();
                    iconIndex = 2;
                    SaveManager.SaveData("haptic", 1);
                }

                hapticButton.image.sprite = icons[iconIndex];
                isHapticOn = !isHapticOn;
            });
        }
    }
}
