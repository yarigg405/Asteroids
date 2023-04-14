public class ShipStatsContainer
{
    public float RotationModifier { get; }
    public float SpeedModifier { get; }

    public ShipStatsContainer(float rotation, float speed)
    {
        RotationModifier = rotation;
        SpeedModifier = speed;
    }
}


