using UnityEngine;

namespace NotDecided
{
    [CreateAssetMenu(fileName = "ColorConfig", menuName = "ScriptableObjects/ColorConfig", order = 1)]
    public class ColorConfig : ScriptableObject
    {
        public ColorType ColorType;
        public string ColorHex;
    }
}
