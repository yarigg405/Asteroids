using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "PrefabsStorage", menuName = "GameSettings/PrefabsStorage", order = 1)]
public class PrefabsStorage : ScriptableObject
{
    [SerializeField] Transform playerShipPrefab;
    [SerializeField] List<Transform> bulletPrefabs;
    [SerializeField] List<Transform> asteroidsPrefabs;
    [SerializeField] List<Transform> ufosPrefabs;

    public Transform GetPlayer()
    {
        return playerShipPrefab;
    }

    public Transform GetBullet(int index)
    {
        return Get(bulletPrefabs, index);
    }

    public Transform GetAsteroid(int index)
    {
        return Get(asteroidsPrefabs, index);
    }

    public Transform GetUfos(int index)
    {
        return Get(ufosPrefabs, index);
    }


    private Transform Get(List<Transform> list, int index)
    {
        if (index < 0)
            return list[0];

        if (index >= list.Count) return list.Last();

        return list[index];
    }



}
