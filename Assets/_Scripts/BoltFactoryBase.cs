
using UnityEngine;

//public abstract class BoltFactoryBase : UnityFactoryBase
//{
//    public BoltFactoryBase(PrefabsStorage prefabStorage) : base(prefabStorage)
//    { }

//    public abstract BoltData CreateBoltData(TransformInfo boltTransformInfo);
//}


public abstract class UnityFactoryBase
{
    protected PrefabsStorage storage;

    public UnityFactoryBase(PrefabsStorage prefabStorage)
    {
        storage = prefabStorage;
    }

    public virtual Transform CreateUnityTransform()
    {
        var prefab = GetPrefab();
        var transform = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;

        return transform;
    }

    public PrefabsStorage GetPrefabStorage()
    {
        return storage;
    }

    protected abstract Transform GetPrefab();
}

