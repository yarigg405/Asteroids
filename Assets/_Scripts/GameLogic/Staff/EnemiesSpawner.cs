using UnityEngine;

public class EnemiesSpawner : IUpdate
{
    private readonly IServiceLocator serviceLocator;
    private readonly ShipFactoryBase ufoShipFactory;

    #region SpawnSettings
    private const float MinSpawnTime = 4f;
    private const float MaxSpawnTime = 10f;
    private float spawnTime = 3f;

    private const int MinSpawnCount = 1;
    private const int MaxSpawnCount = 4;

    private const float UfoSpawnRatio = 0.15f;

    #endregion



    public EnemiesSpawner(IServiceLocator serviceLocator)
    {
        this.serviceLocator = serviceLocator;
        this.serviceLocator.Get<IUpdater>().AddToUpdateList(this);
        ufoShipFactory = new EnemyShipFactory(serviceLocator);
    }

    public void OnUpdate(float deltaTime)
    {
        spawnTime -= deltaTime;
        if (spawnTime < 0)
        {
            spawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
            SpawnTick();
        }
    }

    private void SpawnTick()
    {
        var count = Random.Range(MinSpawnCount, MaxSpawnCount);
        for (var i = 0; i < count; i++)
        {
            if (Random.Range(0f, 1f) < UfoSpawnRatio)
            {
                SpawnUfo();
            }

            else
            {
                SpawnAsteroid();
            }

        }
    }

    private void SpawnAsteroid()
    {
        var trInfo = GetRandomTransformInfo();
        var unityTr = serviceLocator.Get<ISpawner>().SpawnUnityTransform(PrefabType.Asteroid, 0);
        var asteroid = new AsteroidController(serviceLocator, trInfo, unityTr);
        asteroid.OnUpdate(0);
    }

    private void SpawnUfo()
    {
        var trInfo = GetRandomTransformInfo();
        var ufoShip = new UfoController(trInfo, ufoShipFactory);
        ufoShip.TransformInfo.Position = trInfo.Position;
        ufoShip.OnUpdate(0);
        var pilot = new AiPilotController(serviceLocator, 2f, ufoShip);
    }

    private static TransformInfo GetRandomTransformInfo()
    {
        var trInfo = new TransformInfo();
        var spd = Random.Range(1f, 3f);

        var radians = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        trInfo.CurrentRadians = radians;
        var velX = Mathf.Sin(radians) * spd;
        var velY = Mathf.Cos(radians) * spd;
        trInfo.Velocity = new Vector2(-velX, velY);

        trInfo.Position = Vector2.zero.GetRandomCoordinatesAroundPoint(50f, true);

        return trInfo;
    }
}

