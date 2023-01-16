using ToolBox;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] PrefabsStorage prefabsStorage;
    [SerializeField] Updater updater;
    [SerializeField] PlayerConditionWindow conditionWindow;
    [SerializeField] GameOverWindow gameOverWindow;

    private void Start()
    {
        IServiceLocator serviceLocator = new ServiceLocator();
        var minMax = new MinMaxBounds()
        {
            minX = -4.8f,
            maxX = 4.8f,
            minY = -5.54f,
            maxY = 7.54f,
        };
        serviceLocator.Register(minMax);

        var poolManager = CreatePoolManager();
        var spawner = new PrefabSpawner(prefabsStorage, poolManager);
        var logicDelayer = new LogicDelayer();
        updater.SetLogicDelayer(logicDelayer);

        serviceLocator.Register<IUpdater>(updater);
        serviceLocator.Register<ISpawner>(spawner);
        serviceLocator.Register<IDespawner>(spawner);
        serviceLocator.Register<ILogicDelayer>(logicDelayer);

        var posHandler = new PositionsHandler(minMax);
        serviceLocator.Register<IPositionsHandler>(posHandler);

        var shipFactory = new PlayerShipFactory(serviceLocator);
        var playerShip = new ShipController(shipFactory);
        var playerControl = new PlayerControl(playerShip);
        updater.AddToUpdateList(playerControl);
        posHandler.PlayerTransform = playerShip.shipData.transformInfo;

        var logger = new PlayerShipConditionLogger(conditionWindow, playerShip.transformInfo);
        updater.AddToUpdateList(logger);
        serviceLocator.Register(logger);

        serviceLocator.Register(new PlayerScoresContainer());
        serviceLocator.Register<IGameOverWindow>(gameOverWindow);

        var enemiesSpawner = new EnemiesSpawner(serviceLocator);
    }

    private PoolManager CreatePoolManager()
    {
        var poolManager = new PoolManager();
        poolManager.PopulateWith(PoolType.Bullets, prefabsStorage.GetBullet(0).gameObject, 50);
        poolManager.PopulateWith(PoolType.Bullets, prefabsStorage.GetBullet(1).gameObject, 50);

        poolManager.PopulateWith(PoolType.Enemies, prefabsStorage.GetAsteroid(0).gameObject, 15);
        poolManager.PopulateWith(PoolType.Enemies, prefabsStorage.GetAsteroid(1).gameObject, 15);
        poolManager.PopulateWith(PoolType.Enemies, prefabsStorage.GetAsteroid(2).gameObject, 15);

        return poolManager;
    }
}
