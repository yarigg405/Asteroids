using UnityEngine;

public interface ISpawner
{
    public Transform SpawnUnityTransform(PrefabType prefabType, int prefabId);
}

