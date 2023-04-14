using ToolBox;
using UnityEngine;

public class PrefabSpawner : ISpawner, IDespawner, IService
{
    private readonly PrefabsStorage prefabsStorage;
    private readonly PoolManager poolManager;

    public PrefabSpawner(PrefabsStorage prefabsStorage, PoolManager poolManager)
    {
        this.prefabsStorage = prefabsStorage;
        this.poolManager = poolManager;
    }

    public Transform SpawnUnityTransform(PrefabType prefabType, int prefabId)
    {
        PoolType pType;
        GameObject prefab;

        switch (prefabType)
        {
            case PrefabType.Null:
                return null;

            case PrefabType.PlayerShip:
                pType = PoolType.Entities;
                prefab = prefabsStorage.GetPlayer().gameObject;
                break;

            case PrefabType.Bullet:
                pType = PoolType.Bullets;
                prefab = prefabsStorage.GetBullet(prefabId).gameObject;
                break;


            default:
                pType = PoolType.Enemies;
                prefab = (prefabType == PrefabType.Asteroid ?
                    prefabsStorage.GetAsteroid(prefabId) :
                    prefabsStorage.GetUfos(prefabId)).gameObject;
                break;
        }

        return poolManager.Spawn(pType, prefab).transform;
    }

    public void Despawn(PrefabType prefabType, Transform tr)
    {
        PoolType pType;

        switch (prefabType)
        {
            case PrefabType.PlayerShip:
                pType = PoolType.Entities;
                break;

            case PrefabType.Bullet:
                pType = PoolType.Bullets;
                break;


            default:
                pType = PoolType.Enemies;
                break;
        }

        poolManager.Despawn(pType, tr.gameObject);
    }
}

