using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour, IUpdater
{
    private List<IUpdate> allUpdates = new List<IUpdate>();
    private List<IUpdate> addToUpdates = new List<IUpdate>();
    private List<IUpdate> removeFromUpdates = new List<IUpdate>();
    private ILogicDelayer logicDelayer;

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        foreach (var item in allUpdates)
        {
            if (item != null)
                item.OnUpdate(deltaTime);
        }

        if (addToUpdates.Count > 0)
        {
            foreach (var item in addToUpdates)
            {
                allUpdates.Add(item);
            }
            addToUpdates.Clear();
        }

        if (removeFromUpdates.Count > 0)
        {
            foreach (var item in removeFromUpdates)
            {
                if (allUpdates.Contains(item))
                    allUpdates.Remove(item);
            }
            removeFromUpdates.Clear();
        }

        if (logicDelayer != null)
        {
            logicDelayer.DoDelayedLogic();
        }
    }

    public void AddToUpdateList(IUpdate obj)
    {
        addToUpdates.Add(obj);
    }

    public void RemoveFromUpdateList(IUpdate obj)
    {
        removeFromUpdates.Add(obj);
    }

    public void SetLogicDelayer(ILogicDelayer delayer)

    {
        logicDelayer = delayer; ;
    }

}

public interface IUpdater
{
    public void AddToUpdateList(IUpdate obj);
    public void RemoveFromUpdateList(IUpdate obj);

}
