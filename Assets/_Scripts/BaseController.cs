using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseController : IUpdate, IDisposable
{
    public static List<IUpdate> AllUpdates;
    public static List<IUpdate> AddToUpdates;
    public static List<IUpdate> RemoveFromUpdates;

    protected Transform unityTransform;

    public BaseController()
    {
        AddToUpdates.Add(this);
    }

    public virtual void Dispose()
    {
        RemoveFromUpdates.Add(this);
        if (unityTransform != null)
        {
            GameObject.Destroy(unityTransform.gameObject);
        }
    }

    public abstract void OnUpdate(float deltatime);
}

