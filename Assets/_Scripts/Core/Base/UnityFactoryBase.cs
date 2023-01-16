using UnityEngine;

public abstract class UnityFactoryBase
{
    protected IServiceLocator serviceLocator;


    public UnityFactoryBase(IServiceLocator _serviceLocator)
    {
        serviceLocator = _serviceLocator;
    }

    public virtual Transform CreateUnityTransform()
    {
        var transform = serviceLocator.Get<ISpawner>().SpawnUnityTransform(GetPrefabType(), GetPrefabId());
        transform.localScale = Vector3.one;
        return transform;
    }

    public IServiceLocator GetServiceLocator()
    {
        return serviceLocator;
    }

    protected abstract PrefabType GetPrefabType();

    protected abstract int GetPrefabId();
}

