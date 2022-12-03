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
    }
}
