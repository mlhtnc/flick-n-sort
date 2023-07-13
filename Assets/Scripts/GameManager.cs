using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private ColorConfig[] colorConfigs;

        [SerializeField]
        private GameObject particlePrefab;

        private Dictionary<ColorType, Color> colorDict;

        public GameObject ParticlePrefab => particlePrefab;

        private void Awake()
        {
            if(Instance != null)
            {
                return;
            }

            Instance = this;
            
            SetupColorDict();
        }

        private void SetupColorDict()
        {
            colorDict = new Dictionary<ColorType, Color>();
            for(int i = 0; i < colorConfigs.Length; ++i)
            {
                var success = ColorUtility.TryParseHtmlString($"#{colorConfigs[i].ColorHex}", out Color color);
                if(success == false)
                {
                    Debug.LogError($"Had issues with parsing this hex code -> {colorConfigs[i].ColorHex}");
                    return;
                }

                colorDict.Add(colorConfigs[i].ColorType, color);
            }
        }

        public Color GetColorByType(ColorType colorType)
        {
            var found = colorDict.TryGetValue(colorType, out Color color);
            if(found == false)
            {
                Debug.LogError($"Color not found -> {colorType}");
                return default(Color);
            }

            return color;
        }
    }
}
