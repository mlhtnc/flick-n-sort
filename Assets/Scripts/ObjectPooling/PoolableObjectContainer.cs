using UnityEngine;

namespace NotDecided.ObjectPooling
{
    public class PoolableObjectContainer : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        private void Awake()
        {
            PoolManager.Preload(prefab, transform);
        }
    }
}