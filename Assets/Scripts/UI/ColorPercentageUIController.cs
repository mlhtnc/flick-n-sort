using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class ColorPercentageUIController : MonoBehaviour
    {
        private ColorPercentageUIContainer[] colorPercentageUIContainers;

        private void Start()
        {
            colorPercentageUIContainers = GetComponentsInChildren<ColorPercentageUIContainer>(true);
        }

        public void UpdateColorPercentages(Dictionary<ColorType, int> countByColorDict, Dictionary<ColorType, int> totalCountByColorDict)
        {
            for(int i = 0; i < colorPercentageUIContainers.Length; ++i)
            {
                var container = colorPercentageUIContainers[i];
                var percentage = (int)((float) countByColorDict[container.ColorType] / totalCountByColorDict[container.ColorType] * 100f);
                
                container.PercentageText.SetText($"%{percentage}");
                container.PercentageText.color = GameManager.Instance.GetColorByType(container.ColorType);
            }
        }
    }
}