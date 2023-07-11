using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class LevelController : MonoBehaviour
    {
        private PieceController[] pieceControllers;

        private void Start()
        {
            pieceControllers = GetComponentsInChildren<PieceController>();
        }

        private void Update()
        {
            int count = 0;
            for(int i = 0; i < pieceControllers.Length; ++i)
            {
                if(pieceControllers[i].IsPieceInCorrectPlace)
                    ++count;
            }

            if(count == pieceControllers.Length)
            {
                Debug.Log("Level Completed");
            }
        }
    }
}
