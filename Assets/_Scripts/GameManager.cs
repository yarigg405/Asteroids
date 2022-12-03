using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<IUpdate> updatedObjects = new List<IUpdate>();


    private void Update()
    {
        var dt = Time.deltaTime;
        foreach (var upd in updatedObjects)
        {
            upd.OnUpdate(dt);
        }
    }


    public void AddToUpdateList(IUpdate upd)
    {
        updatedObjects.Add(upd);
    }


    public void RemoveFromUpdateList(IUpdate upd)
    {
        if (updatedObjects.Contains(upd))
        {
            updatedObjects.Remove(upd);
        }
    }
}
