using System;
using NotDecided.ObjectPooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NotDecided
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField]
        private TextMeshProUGUI levelText;

        [SerializeField]
        private TextMeshProUGUI levelCompleteInfoText;

        [SerializeField]
        private TextMeshProUGUI progressText;
        
        [SerializeField]
        private SlicedFilledImage levelProgressFiller;

        [SerializeField]
        private Button nextLevelButton;

        [SerializeField]
        private Button tapToStartButton;
        
        [SerializeField]
        private Image levelCompletedImage;

        private LevelManager levelManager;

        private Camera mainCamera;

        private Vector3 initialCameraPos;

        [SerializeField]
        private float cameraAnimationDistance;

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
            levelManager = LevelManager.Instance;
            mainCamera = Camera.main;

            nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
            tapToStartButton.onClick.AddListener(OnTapToStartButtonClicked);

            levelManager.OnLevelCompleted   += OnLevelCompleted;

            levelCompletedImage.gameObject.SetActive(false);

            initialCameraPos = mainCamera.transform.position;
            mainCamera.transform.Translate(0f, 5f, 0f, Space.World);
        }

        private void Update()
        {
            levelText.SetText($"LEVEL {levelManager.Level.ToString()}");
        }

        private void OnLevelCompleted()
        {
            LeanTween.delayedCall(0.5f, () =>
            {
                AudioManager.Instance.Play(GameManager.Instance.LevelSuccessAudioClip);

                var prefab = GameManager.Instance.ConfettiParticlePrefab;
                var confettiGo = PoolManager.Spawn(prefab, prefab.transform.position, prefab.transform.rotation);

                var c = levelCompletedImage.color;
                c.a = 0f;
                levelCompletedImage.color = c;

                levelCompletedImage.gameObject.SetActive(true);
                LeanTween.value(
                    levelCompletedImage.gameObject,
                    (val) =>
                    {
                        var c = levelCompletedImage.color;
                        c.a = val;
                        levelCompletedImage.color = c;
                    },
                    0f,
                    0.7f,
                    1.5f
                )
                .setDelay(2.5f);

                levelCompleteInfoText.gameObject.SetActive(false);
                LeanTween.value(
                    levelCompleteInfoText.gameObject,
                    (val) =>
                    {
                        levelCompleteInfoText.fontSize = val;
                        levelCompleteInfoText.gameObject.SetActive(true);
                    },
                    120f,
                    250f,
                    0.5f
                )
                .setDelay(3.5f)
                .setEaseInOutBack();

                nextLevelButton.gameObject.SetActive(false);
                LeanTween.delayedCall(4.5f, () => nextLevelButton.gameObject.SetActive(true));
            });
        }

        private void OnTapToStartButtonClicked()
        {
            LeanTween.move(mainCamera.gameObject, initialCameraPos, 0.3f).setEaseInBack();

            tapToStartButton.gameObject.SetActive(false);
            levelManager.StartNextLevel();
        }

        private void OnNextLevelButtonClicked()
        {
            mainCamera.transform.Translate(0f, 5f, 0f, Space.World);

            levelCompletedImage.gameObject.SetActive(false);
            tapToStartButton.gameObject.SetActive(true);

            levelManager.SetupNextLevel();
        }

        public void UpdateLevelProgress(float progress)
        {
            levelProgressFiller.fillAmount = progress;
            progressText.SetText($"%{(int)(progress * 100f)}");
        }
    }
}