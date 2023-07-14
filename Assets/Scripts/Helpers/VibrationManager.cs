using System;
using UnityEngine;

namespace NotDecided
{
    public class VibrationManager : MonoBehaviour
    {
        public static VibrationManager Instance;

        [SerializeField]
        private float cooldownTime;

        private float timer;

        private bool isCoolingDown;

        private bool isVibrationEnabled;

        private void Start()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            Vibration.Init();
        }

        private void Update()
        {
            if(isCoolingDown == false)
                return;

            timer += Time.deltaTime;
            if(timer >= cooldownTime)
            {
                isCoolingDown = false;
                timer = 0f;
            }
        }

        public void VibratePop()
        {
            if(isVibrationEnabled && isCoolingDown == false)
            {
                Vibration.VibratePop();
                isCoolingDown = true;
            }
        }

        public void EnableVibration()
        {
            isVibrationEnabled = true;
        }

        public void DisableVibration()
        {
            isVibrationEnabled = false;
        }
    }
}
