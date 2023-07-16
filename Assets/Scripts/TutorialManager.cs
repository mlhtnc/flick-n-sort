using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance;

        [SerializeField]
        private GameObject handSprite;

        private bool isTutorialCompleted;

        private void Awake()
        {
            if(Instance != null)
            {
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            handSprite.gameObject.SetActive(false);

            LevelManager.Instance.OnLevelStarted += OnLevelStarted;
        }

        private void OnLevelStarted()
        {
            LeanTween.move(handSprite.gameObject, new Vector3(-10.84f, 5.73f, 6.11f), 1.25f).setRepeat(-1).setLoopClamp();
            handSprite.gameObject.SetActive(true);
        }

        public void OnPieceMoved()
        {
            if(isTutorialCompleted)
                return;

            LeanTween.cancel(handSprite.gameObject);
            handSprite.gameObject.SetActive(false);
            isTutorialCompleted = true;

            LevelManager.Instance.OnLevelStarted -= OnLevelStarted;
        }
    }
}