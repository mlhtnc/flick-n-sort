using System.Collections.Generic;
using UnityEngine;

namespace NotDecided.ObjectPooling
{
    public class ObjectPool
    {
        private GameObject prefab;

        private Stack<GameObject> inactiveStack;
        private HashSet<GameObject> activeLookup;

        public ObjectPool(GameObject prefab)
        {
            this.prefab = prefab;

            inactiveStack = new Stack<GameObject>();
            activeLookup = new HashSet<GameObject>();
        }

        public void PreloadObject(GameObject go)
        {
            go.SetActive(false);
            
            inactiveStack.Push(go);
        }

        public GameObject Spawn(Vector3 pos, Quaternion quaternion)
        {
            GameObject go;
            if(inactiveStack.Count == 0)
            {
                go = InstantiateObject(prefab);
            }
            else
            {
                go = inactiveStack.Pop();
            }

            activeLookup.Add(go);
            go.transform.SetPositionAndRotation(pos, quaternion);
            go.SetActive(true);

            return go;
        }

        public void Despawn(GameObject go)
        {
            activeLookup.Remove(go);
            inactiveStack.Push(go);
            
            go.SetActive(false);
        }


        private GameObject InstantiateObject(GameObject prefab)
        {
            return GameObject.Instantiate(prefab, PoolManager.Root);
        }
    }
}