using UnityEngine;

namespace NotDecided
{
    public class PieceController : MonoBehaviour
    {
        [SerializeField]
        private ColorType colorType;

        private void Start()
        {
            var gameManager = GameManager.Instance;
            var renderer = GetComponent<Renderer>();

            renderer.material.color = gameManager.GetColorByType(colorType);
        }

        private void Update()
        {
            
        }
    }
}
