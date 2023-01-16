using UnityEngine;

public class EnemiesSpawner : IUpdate
{
    private IServiceLocator serviceLocator;
    private ShipFactoryBase ufoShipFactory;

    #region SpawnSettings
    private float minSpawnTime = 4f;
    private float maxSpawnTime = 10f;
    private float spawnTime = 3f;

    private int minSpawnCount = 1;
    private int maxSpawnCount = 4;

    private float ufoSpawnRatio = 0.15f;

    #endregion



    public EnemiesSpawner(IServiceLocator _serviceLocator)
    {
        serviceLocator = _serviceLocator;
        serviceLocator.Get<IUpdater>().AddToUpdateList(this);
        ufoShipFactory = new EnemyShipFactory(_serviceLocator);
    }

    public void OnUpdate(float deltaTime)
    {
        spawnTime -= deltaTime;
        if (spawnTime < 0)
        {
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            SpawnTick();
        }
    }

    private void SpawnTick()
    {
        var count = Random.Range(minSpawnCount, maxSpawnCount);
        for (int i = 0; i < count; i++)
        {
            if (Random.Range(0f, 1f) < ufoSpawnRatio)
            {
                SpawnUFO();
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

    private void SpawnUFO()
    {
        var trInfo = GetRandomTransformInfo();
        var ufoShip = new UfoController(trInfo, ufoShipFactory);
        ufoShip.transformInfo.position = trInfo.position;
        ufoShip.OnUpdate(0);
        var pilot = new AiPilotController(serviceLocator, 2f, ufoShip);
    }

    private TransformInfo GetRandomTransformInfo()
    {
        var trInfo = new TransformInfo();
        var spd = Random.Range(1f, 3f);

        var radians = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        trInfo.currentRadians = radians;
        var velX = Mathf.Sin(radians) * spd;
        var velY = Mathf.Cos(radians) * spd;
        trInfo.velocity = new Vector2(-velX, velY);

        trInfo.position = Vector2.zero.GetRandomCoordinatesAroundPoint(50f, true);

        return trInfo;
    }
}

