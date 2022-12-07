

public class BoltStatsContainer
{
    public float boltSpeed { get; private set; }
    public float lifeTime { get; set; }
    public int boltDamage { get; private set; }

    public BoltStatsContainer(float speed, float lifeTime, int damage)
    {
        boltSpeed = speed;
        this.lifeTime = lifeTime;
        boltDamage = damage;
    }
}
