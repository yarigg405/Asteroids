using System.Collections;
using System.Collections.Generic;
using ToolBox;
using UnityEngine;



public class GameStarter : MonoBehaviour
{
    [SerializeField] PrefabsStorage prefabsStorage;
    [SerializeField] Updater updater;

    private void Start()
    {
        var poolManager = CreatePoolManager();
        var spawner = new PrefabSpawner(prefabsStorage, poolManager);
        LinksMaster master = new LinksMaster();
        master.Updater = updater;
        master.Spawner = spawner;
        master.Despawner = spawner;

        ShipFactoryBase shipFactory = new PlayerShipFactory(master);
        var playerShip = new ShipController(shipFactory);

        var playerControl = new PlayerControl(playerShip, updater);

    }

    private PoolManager CreatePoolManager()
    {
        var poolManager = new PoolManager();
        poolManager.PopulateWith(PoolType.Bullets, prefabsStorage.GetBullet(0).gameObject, 50);
        poolManager.PopulateWith(PoolType.Bullets, prefabsStorage.GetBullet(1).gameObject, 50);

        //TODO: Add enemies


        return poolManager;
    }

}
