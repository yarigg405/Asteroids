﻿using UnityEngine;

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

    public TransformInfo(TransformInfo tr)
    {
        velocity = tr.velocity;
        position = tr.position;
        currentRadians = tr.currentRadians;
    }
}


