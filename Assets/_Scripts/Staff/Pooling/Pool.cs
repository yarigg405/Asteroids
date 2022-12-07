using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ToolBox
{
    public class Pool : IDisposable
    {
        private Transform parentPool;
        private Dictionary<int, Queue<GameObject>> cachedObjects = new Dictionary<int, Queue<GameObject>>();
        private Dictionary<int, int> cachedIds = new Dictionary<int, int>();
        protected int index;

        public Pool PopulateWith(GameObject prefab, int amount)
        {
            var key = prefab.GetInstanceID();
            Queue<GameObject> Queue;
            var Queueed = cachedObjects.TryGetValue(key, out Queue);
            if (Queueed == false)
                cachedObjects.Add(key, new Queue<GameObject>());

            while (amount > 0)
            {
                index = amount;
                var go = Populate(prefab, prefab.transform.position, prefab.transform.rotation, parentPool);
                go.SetActive(false);
                cachedIds.Add(go.GetInstanceID(), key);
                cachedObjects[key].Enqueue(go);
                amount--;
            }
            return this;
        }


        public void SetParent(Transform parent)
        {
            parentPool = parent;
        }


        public GameObject Spawn(GameObject prefab, Vector3 position = default(Vector3),
            Quaternion rotation = default(Quaternion), Transform parent = null)
        {
            index++;
            var key = prefab.GetInstanceID();
            Queue<GameObject> Queue;
            var Queueed = cachedObjects.TryGetValue(key, out Queue);

            if (Queueed && Queue.Count > 0)
            {
                var transform = Queue.Dequeue().transform;
                transform.SetParent(parent);
                transform.rotation = rotation;
                transform.gameObject.SetActive(true);
                if (parent) transform.position = position;
                else transform.localPosition = position;
                var poolable = transform.GetComponent<IPoolable>();
                if (poolable != null) poolable.OnSpawn();
                transform.localScale = prefab.transform.localScale;

                return transform.gameObject;
            }

            if (!Queueed) cachedObjects.Add(key, new Queue<GameObject>());
            var createdPrefab = Populate(prefab, position, rotation, parent);
            var isPoolable = createdPrefab.GetComponent<IPoolable>();
            if (isPoolable != null) isPoolable.OnSpawn();
            cachedIds.Add(createdPrefab.GetInstanceID(), key);
            createdPrefab.transform.localScale = prefab.transform.localScale;

            return createdPrefab;
        }


        public void Despawn(GameObject go)
        {
            index--;
            go.SetActive(false);
            cachedObjects[cachedIds[go.GetInstanceID()]].Enqueue(go);
            if (parentPool != null) go.transform.SetParent(parentPool);
            var poolable = go.GetComponent<IPoolable>();
            if (poolable != null) poolable.OnDespawn();
        }


        public void Dispose()
        {
            parentPool = null;
            cachedObjects.Clear();
            cachedIds.Clear();
        }


        GameObject Populate(GameObject prefab, Vector3 position = default(Vector3),
            Quaternion rotation = default(Quaternion), Transform parent = null)
        {
            var go = Object.Instantiate(prefab, position, rotation, parent).transform;
            go.transform.localScale = prefab.transform.localScale;
            go.name += "_" + index;
            if (parent == null) go.position = position;
            else go.localPosition = position;
            return go.gameObject;
        }

    }
}