using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;


public class ShipData
{
    public StatsContainer stats { get; private set; }
    public TransformInfo transformInfo { get; private set; }

    public ShipData(StatsContainer shipStats)
    {
        transformInfo = new TransformInfo();
        stats = shipStats;
    }
}

public class TransformInfo
{
    public Vector2 velocity { get; set; }
    public Vector2 position { get; set; }
    public float currentRadians { get; set; }

    public TransformInfo()
    {
        velocity = Vector2.zero;
        position = Vector2.zero;
        currentRadians = 0f;
    }
}



public class StatsContainer
{
    public float rotationModifier { get; private set; }
    public float speedModifier { get; private set; }

    public StatsContainer(float rotation, float speed)
    {
        rotationModifier = rotation;
        speedModifier = speed;
    }
}


