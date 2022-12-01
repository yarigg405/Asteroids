using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundedObject : MonoBehaviour
{    
    void Update()
    {
        var pos = transform.position;

        if (pos.x > 4.4f)
            pos.x = -4.4f;

        if (pos.x < -4.4f)
            pos.x = 4.4f;

        if (pos.y > 7.2f)
            pos.y = -5.2f;

        if (pos.y < -5.2f)
            pos.y = 7.2f;

        transform.position = pos;
    }
}
