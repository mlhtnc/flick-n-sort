using UnityEngine;

namespace NotDecided
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private AudioSource audioSource;

        private float timer;

        private bool isCooldowned;

        private void Awake()
        {
            if(Instance != null)
            {
                return;
            }

            Instance = this;

            audioSource = GetComponent<AudioSource>();
            isCooldowned = true;
        }

        private void Update()
        {
            if(isCooldowned)
                return;
            
            timer += Time.deltaTime;
            if(timer >= 0.1f)
            {
                isCooldowned = true;
            }
        }

        public void Play(AudioClip clip)
        {
            if(isCooldowned == false)
                return;
            
            audioSource.PlayOneShot(clip);
            isCooldowned = false;
            timer = 0f;
        }

        public void Mute()
        {
            audioSource.mute = true;
        }

        public void Unmute()
        {
            audioSource.mute = false;
        }
    }
}