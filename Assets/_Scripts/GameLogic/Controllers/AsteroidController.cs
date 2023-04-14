using UnityEngine;

public class AsteroidController : BaseController
{
    protected override PrefabType PrefabType => PrefabType.Asteroid;
    private int numOfPrefab;
    protected override int ScoresByDestroy => 100;

    private readonly TransformInfo transformInfo;
    public override TransformInfo TransformInfo => transformInfo;

    public AsteroidController(IServiceLocator locator, TransformInfo trInfo, Transform unityTr)
        : base(locator)
    {
        transformInfo = trInfo;
        UnityTransform = unityTr;
    }

    public void GetDamage(int damage)
    {
        if (damage == 1 && numOfPrefab < 2)
        {
            SpawnShards();
        }

        Dispose();
    }

    protected override void CheckCollisions()
    {
        var nearestBolts = FieldCell.Get<BoltController>();
        foreach (var item in nearestBolts)
        {
            var bolt = item as BoltController;
            if (bolt.Team == Team.Player)
            {
                var distance = (TransformInfo.Position - bolt.TransformInfo.Position).sqrMagnitude;
                var radius = (TransformInfo.Size + bolt.TransformInfo.Size) *
                    (TransformInfo.Size + bolt.TransformInfo.Size);
                if (distance < radius)
                {
                    var dmg = bolt.BoltStats.BoltDamage;
                    GetDamage(dmg);

                    if (dmg < 2)
                        bolt.Dispose();
                }

            }
        }
    }

    private void SpawnShards()
    {
        var shardsCount = Random.Range(2, 4);

        for (int i = 0; i < shardsCount; i++)
        {
            var trInfo = new TransformInfo(TransformInfo)
            {
                Size = TransformInfo.Size * 0.5f
            };
            var spd = TransformInfo.Velocity.magnitude;
            spd *= 1.3f;
            var radians = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var velX = Mathf.Sin(radians) * spd;
            var velY = Mathf.Cos(radians) * spd;
            trInfo.Velocity = new Vector2(-velX, velY);

            var unityTr = Locator.Get<ISpawner>().SpawnUnityTransform(PrefabType.Asteroid, ++numOfPrefab);

            var shard = new AsteroidController(Locator, trInfo, unityTr);
            shard.numOfPrefab = numOfPrefab;
            unityTr.position = trInfo.Position;
        }
    }


}

