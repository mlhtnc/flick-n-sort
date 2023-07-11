using UnityEngine;

namespace NotDecided
{
    public class PartController : MonoBehaviour
    {
        [SerializeField]
        private ColorType colorType;

        public ColorType ColorType => colorType;
    }
}