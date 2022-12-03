using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameStarter : MonoBehaviour
{
    [SerializeField] PrefabsStorage prefabsStorage;

    private void Start()
    {
        BaseController.AllUpdates = new List<IUpdate>();

        ShipFactoryBase shipFactory = new PlayerShipFactory(prefabsStorage);
        var playerShip = new ShipController(shipFactory);

        var playerControl = new PlayerControl(playerShip);

    }
}
