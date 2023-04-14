

public class BoltStatsContainer
{
    public float BoltSpeed { get; }
    public float LifeTime { get; set; }
    public int BoltDamage { get; }

    public BoltStatsContainer(float speed, float lifeTime, int damage)
    {
        BoltSpeed = speed;
        LifeTime = lifeTime;
        BoltDamage = damage;
    }
}
