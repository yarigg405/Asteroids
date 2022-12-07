using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UnityFactoryBase
{
    protected LinksMaster linksMaster;


    public UnityFactoryBase(LinksMaster _linksMaster)
    {
        linksMaster = _linksMaster;
    }

    public virtual Transform CreateUnityTransform()
    {
        var transform = linksMaster.Spawner.SpawnUnityTransform(GetPrefabType(), GetPrefabId());
        transform.localScale = Vector3.one;
        return transform;
    }

    public LinksMaster GetLinksMaster()
    {
        return linksMaster;
    }

    protected abstract PrefabType GetPrefabType();

    protected abstract int GetPrefabId();
}

