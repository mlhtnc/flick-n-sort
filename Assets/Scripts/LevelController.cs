using System;
using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class LevelController : MonoBehaviour
    {
        private PieceController[] pieceControllers;

        private LevelManager levelManager;

        private bool isLevelCompleted;

        private Dictionary<ColorType, int> totalCountByColorDict;

        private Dictionary<ColorType, int> countByColorDict;

        private ColorPercentageUIController colorPercentageUIController;

        private List<ColorType> allColors;

        private List<Vector3> initialPiecePositions;

        private void Start()
        {
            pieceControllers            = GetComponentsInChildren<PieceController>(true);
            colorPercentageUIController = GetComponent<ColorPercentageUIController>();
            levelManager                = LevelManager.Instance;

            CacheInitialPositions();
            CalculateTotalCountByColor();
        }

        private void CacheInitialPositions()
        {
            initialPiecePositions = new List<Vector3>();

            for(int i = 0; i < pieceControllers.Length; ++i)
            {
                initialPiecePositions.Add(pieceControllers[i].transform.position);
            }
        }

        private void CalculateTotalCountByColor()
        {
            totalCountByColorDict = new Dictionary<ColorType, int>();
            countByColorDict = new Dictionary<ColorType, int>();
            allColors = new List<ColorType>();

            for(int i = 0; i < pieceControllers.Length; ++i)
            {
                var pieceColor = pieceControllers[i].ColorType;
                if(totalCountByColorDict.ContainsKey(pieceColor))
                {
                    totalCountByColorDict[pieceColor]++;
                }
                else
                {
                    totalCountByColorDict.Add(pieceColor, 1);
                    allColors.Add(pieceColor);
                }
            } 
        }

        private void Update()
        {
            if(isLevelCompleted)
                return;

            countByColorDict.Clear();
            for(int i = 0; i < allColors.Count; ++i)
            {
                if(countByColorDict.ContainsKey(allColors[i]) == false)
                {
                    countByColorDict[allColors[i]] = 0;
                }
            }

            int count = 0;
            for(int i = 0; i < pieceControllers.Length; ++i)
            {
                if(pieceControllers[i].IsPieceInCorrectPlace)
                {
                    ++count;

                    var pieceColor = pieceControllers[i].ColorType;
                    if(countByColorDict.ContainsKey(pieceColor))
                    {
                        countByColorDict[pieceColor]++;
                    }
                    else
                    {
                        countByColorDict.Add(pieceColor, 1);
                    }
                }
            }

            colorPercentageUIController.UpdateColorPercentages(countByColorDict, totalCountByColorDict);
            UIManager.Instance.UpdateLevelProgress((float) count / pieceControllers.Length);

            if(count == pieceControllers.Length)
            {
                isLevelCompleted = true;
                levelManager.NotifyOnLevelCompleted();
            }
        }

        public void ResetLevel()
        {
            for(int i = 0; i < pieceControllers.Length; ++i)
            {
                pieceControllers[i].transform.position = initialPiecePositions[i];
            }

            isLevelCompleted = false;
        }
    }
}
