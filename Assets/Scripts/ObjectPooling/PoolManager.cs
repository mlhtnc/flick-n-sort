using System.Collections.Generic;
using UnityEngine;

namespace NotDecided.ObjectPooling
{
    public static class PoolManager
    {
        private static Dictionary<GameObject, ObjectPool> prefabLookup;
        private static Dictionary<GameObject, ObjectPool> instanceLookup;

        public static Transform Root { get; private set; }
       
        private static void Init(GameObject prefab)
        {
            if(prefabLookup == null)
            {
                prefabLookup = new Dictionary<GameObject, ObjectPool>();
            }

            if(instanceLookup == null)
            {
                instanceLookup = new Dictionary<GameObject, ObjectPool>();
            }

            var success = prefabLookup.TryGetValue(prefab, out ObjectPool pool);
            if(success == false)
            {
                prefabLookup.Add(prefab, new ObjectPool(prefab));
            }
        }

        public static void SetRoot(Transform root)
        {
            PoolManager.Root = root;
        }

        public static void Preload(GameObject prefab, Transform parent)
        {
            Init(prefab);

            var pool = prefabLookup[prefab];
            for(int i = 0; i < parent.childCount; ++i)
            {
                var preloadable = parent.GetChild(i);
                pool.PreloadObject(preloadable.gameObject);
            }
        }

        public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion quaternion)
        {
            Init(prefab);

            var pool = prefabLookup[prefab];
            var go = pool.Spawn(pos, quaternion);

            instanceLookup.Add(go, pool);

            return go;
        }

        public static void Despawn(GameObject go)
        {
            var poolFound = instanceLookup.TryGetValue(go, out ObjectPool pool);
            if(poolFound)
            {
                pool.Despawn(go);
                instanceLookup.Remove(go);
            }
            else
            {
                Debug.Log($"Gameobject called '{go.name}' is not spawned by a Pool. Leaving as it is");
                return;
            }
        }
    }
}