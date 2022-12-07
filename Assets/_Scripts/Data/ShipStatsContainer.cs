public class ShipStatsContainer
{
    public float rotationModifier { get; private set; }
    public float speedModifier { get; private set; }

    public ShipStatsContainer(float rotation, float speed)
    {
        rotationModifier = rotation;
        speedModifier = speed;
    }
}


