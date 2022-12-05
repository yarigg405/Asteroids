using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour
{
    private void Update()
    {
        var deltaTime = Time.deltaTime;
        foreach (var item in BaseController.AllUpdates)
        {
            if (item != null)
                item.OnUpdate(deltaTime);
        }

        foreach (var item in BaseController.AddToUpdates)
        {
           BaseController.AllUpdates.Add(item);
        }
        BaseController.AddToUpdates.Clear();

        foreach (var item in BaseController.RemoveFromUpdates)
        {
            if (BaseController.AllUpdates.Contains(item))
                BaseController.AllUpdates.Remove(item);
        }
        BaseController.RemoveFromUpdates.Clear();
         
    }
}
