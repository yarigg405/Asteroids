using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseController : IUpdate, IDisposable
{
    public static List<IUpdate> AllUpdates;


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
    }

    public abstract void OnUpdate(float deltatime);
}

