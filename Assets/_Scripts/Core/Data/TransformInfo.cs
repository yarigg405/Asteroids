using UnityEngine;

public class TransformInfo
{
    public Vector2 Velocity { get; set; }
    public Vector2 Position { get; set; }
    public float CurrentRadians { get; set; }

    public float Size { get; set; }

    public TransformInfo()
    {
        Velocity = Vector2.zero;
        Position = Vector2.zero;
        CurrentRadians = 0f;
        Size = 1f;
    }

    public TransformInfo(TransformInfo tr)
    {
        Velocity = tr.Velocity;
        Position = tr.Position;
        CurrentRadians = tr.CurrentRadians;
        Size = tr.Size;
    }
}


