using TMPro;
using UnityEngine;

namespace NotDecided
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI levelText;

        private LevelManager levelManager;

        private void Start()
        {
            levelManager = LevelManager.Instance;
        }

        private void Update()
        {
            levelText.SetText(levelManager.Level.ToString());
        }
    }
}