using System;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox
{
    public class PoolManager : IDisposable
    {
        private readonly Dictionary<int, Pool> pools = new();

        public PoolManager()
        {
            foreach (PoolType pType in (PoolType[])Enum.GetValues(typeof(PoolType)))
            {
                AddPool(pType);
            }
        }

        public Pool PopulateWith(PoolType id, GameObject prefab, int amount)
        {
            var obj = pools[(int)id].PopulateWith(prefab, amount);
            return obj;
        }
        private void AddPool(PoolType id, bool reParent = true)
        {
            if (pools.TryGetValue((int)id, out var pool) == false)
            {
                pool = new Pool();
                pools.Add((int)id, pool);
                if (reParent)
                {
                    var poolsGo = GameObject.Find("[POOLS]") ?? new GameObject("[POOLS]");
                    var poolGo = new GameObject("Pool:" + id);
                    poolGo.transform.SetParent(poolsGo.transform);
                    pool.SetParent(poolGo.transform);
                }
            }
        }



        public GameObject Spawn(PoolType id, GameObject prefab, Vector3 position = default,
            Quaternion rotation = default,
            Transform parent = null)
        {
            return pools[(int)id].Spawn(prefab, position, rotation, parent);
        }

        public void Despawn(PoolType id, GameObject obj)
        {
            pools[(int)id].Despawn(obj);
        }

        public void Dispose()
        {
            foreach (var poolsValue in pools.Values)
                poolsValue.Dispose();
            pools.Clear();
        }

    }
}