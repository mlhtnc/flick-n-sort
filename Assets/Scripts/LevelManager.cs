using UnityEngine;
using NotDecided.InputManagament;
using System;

namespace NotDecided
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        private int level = 1;

        private LevelController[] levelControllers;

        public int Level => level;

        public bool IsLevelCompleted { get; private set; }

        public event Action OnLevelStartedEvent;
        public event Action OnLevelCompletedEvent;

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
            levelControllers = GetComponentsInChildren<LevelController>(true);

            SetupLevels();
        }

        private void SetupLevels()
        {
            levelControllers[0].gameObject.SetActive(true);

            for(int i = 1; i < levelControllers.Length; ++i)
            {
                levelControllers[i].gameObject.SetActive(false);
            }
        }

        private void Update()
        {
        }

        public void OnLevelCompleted()
        {
            int zeroIndexedLevel = level - 1;
            if(zeroIndexedLevel + 1 == levelControllers.Length)
                return;

            levelControllers[zeroIndexedLevel].gameObject.SetActive(false);
            levelControllers[zeroIndexedLevel + 1].gameObject.SetActive(true);

            IsLevelCompleted = true;

            ++level;

            OnLevelCompletedEvent?.Invoke();
        }
    }
}
