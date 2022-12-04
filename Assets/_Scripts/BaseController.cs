using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseController : IUpdate, IDisposable
{
    public static List<IUpdate> AllUpdates;

    protected Transform unityTransform;

    public BaseController()
    {
        AllUpdates.Add(this);
    }

    public virtual void Dispose()
    {
        if (AllUpdates.Contains(this))
        {
            AllUpdates.Remove(this);
        }
        if (unityTransform != null)
        {
            GameObject.Destroy(unityTransform.gameObject);
        }
    }

    public abstract void OnUpdate(float deltatime);
}

