using UnityEngine;

public abstract class UnityFactoryBase
{
    protected IServiceLocator ServiceLocator;


    protected UnityFactoryBase(IServiceLocator serviceLocator)
    {
        ServiceLocator = serviceLocator;
    }

    public virtual Transform CreateUnityTransform()
    {
        var transform = ServiceLocator.Get<ISpawner>().SpawnUnityTransform(GetPrefabType(), GetPrefabId());
        transform.localScale = Vector3.one;
        return transform;
    }

    public IServiceLocator GetServiceLocator()
    {
        return ServiceLocator;
    }

    protected abstract PrefabType GetPrefabType();

    protected abstract int GetPrefabId();
}

