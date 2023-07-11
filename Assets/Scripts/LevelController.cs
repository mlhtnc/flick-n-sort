using UnityEngine;

namespace NotDecided
{
    public class LevelController : MonoBehaviour
    {
        private PieceController[] pieceControllers;

        private LevelManager levelManager;

        private bool isLevelCompleted;

        private void Start()
        {
            pieceControllers = GetComponentsInChildren<PieceController>();
            levelManager = LevelManager.Instance;
        }

        private void Update()
        {
            if(isLevelCompleted)
                return;
            
            int count = 0;
            for(int i = 0; i < pieceControllers.Length; ++i)
            {
                if(pieceControllers[i].IsPieceInCorrectPlace)
                    ++count;
            }

            if(count == pieceControllers.Length)
            {
                isLevelCompleted = true;
                levelManager.OnLevelCompleted();
            }
        }
    }
}
