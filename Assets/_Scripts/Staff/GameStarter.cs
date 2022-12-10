using System.Collections;
using System.Collections.Generic;
using ToolBox;
using UnityEngine;



public class GameStarter : MonoBehaviour
{
    [SerializeField] PrefabsStorage prefabsStorage;
    [SerializeField] Updater updater;
    [SerializeField] PlayerConditionWindow conditionWindow;
    [SerializeField] GameOverWIndow gameOverWindow;

    private void Start()
    {
        LinksMaster master = new LinksMaster();
        master.MinMaxBounds = new MinMaxBounds()
        {
            minX = -4.8f,
            maxX = 4.8f,
            minY = -5.54f,
            maxY = 7.54f,
        };
        var poolManager = CreatePoolManager();
        var spawner = new PrefabSpawner(prefabsStorage, poolManager);
        master.Updater = updater;
        master.Spawner = spawner;
        master.Despawner = spawner;
        master.PositionsHandler = CreatePositionHandler(master.MinMaxBounds);
        var logicDelayer = new LogicDelayer();
        master.LogicDelayer = logicDelayer;
        updater.SetLogicDelayer(logicDelayer);
        var shipFactory = new PlayerShipFactory(master);
        var playerShip = new ShipController(shipFactory);
        var playerControl = new PlayerControl(playerShip, updater);
        master.PositionsHandler.PlayerTransform = playerShip.shipData.transformInfo;
        master.PlayerLogger = new PlayerShipConditionLogger(conditionWindow, playerShip.transformInfo);
        updater.AddToUpdateList(master.PlayerLogger);
        var enemiesSpawner = new EnemiesSpawner(master);
        master.PlayerScoresContainer = new PlayerScoresContainer();
        master.GameOverWindow = gameOverWindow;
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

    private IPositionsHandler CreatePositionHandler(MinMaxBounds bounds)
    {
        var positionsHandler = new PositionsHandler(bounds);

        return positionsHandler;
    }

}
