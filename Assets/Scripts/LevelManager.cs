using UnityEngine;
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

        public event Action OnLevelStarted;
        public event Action OnLevelCompleted;

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
            level = SaveManager.GetData("level");

            SetupLevels();
        }

        private void SetupLevels()
        {
            for(int i = 0; i < levelControllers.Length; ++i)
            {
                levelControllers[i].gameObject.SetActive(false);
            }

            int zeroIndexedLevel = level - 1;
            levelControllers[zeroIndexedLevel].gameObject.SetActive(true);
        }

        public void NotifyOnLevelCompleted()
        {
            IsLevelCompleted = true;

            SaveManager.SaveData("level", level + 1);

            OnLevelCompleted?.Invoke();
        }

        public void SetupNextLevel()
        {
            int zeroIndexedLevel = level - 1;
            if(zeroIndexedLevel + 1 == levelControllers.Length)
                return;

            levelControllers[zeroIndexedLevel].gameObject.SetActive(false);
            levelControllers[zeroIndexedLevel + 1].gameObject.SetActive(true);

            ++level;
        }

        public void StartNextLevel()
        {
            OnLevelStarted?.Invoke();
        }
    }
}
