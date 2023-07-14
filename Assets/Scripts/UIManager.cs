using TMPro;
using UnityEngine;

namespace NotDecided
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI levelText;

        [SerializeField]
        private SlicedFilledImage levelProgressFiller;

        private LevelManager levelManager;

        private void Start()
        {
            levelManager = LevelManager.Instance;
        }

        private void Update()
        {
            levelText.SetText($"LEVEL {levelManager.Level.ToString()}");
            // levelProgressFiller.fillAmount = 

        }
    }
}