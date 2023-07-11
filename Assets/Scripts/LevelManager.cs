using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class LevelManager : MonoBehaviour
    {
        private int level = 1;

        private LevelController[] levelControllers;

        private void Start()
        {
            levelControllers = GetComponentsInChildren<LevelController>(true);
        }

        private void Update()
        {
            
        }
    }
}