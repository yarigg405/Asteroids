using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseController : IUpdate, IDisposable
{
    protected LinksMaster linksMaster;

    protected Transform unityTransform;
    protected abstract PrefabType prefabType { get; }


    public BaseController(LinksMaster _linksMaster)
    {
        if (_linksMaster != null)
        {
            linksMaster = _linksMaster;

            linksMaster.Updater.AddToUpdateList(this);
        }
    }

    public virtual void Dispose()
    {
        linksMaster.Updater.RemoveFromUpdateList(this);
        if (unityTransform != null)
        {
            linksMaster.Despawner.Despawn(prefabType, unityTransform);
            unityTransform = null;
        }
    }

    public abstract void OnUpdate(float deltatime);
}

