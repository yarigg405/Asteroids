using UnityEngine;


public class PlayerShipFactory : ShipFactoryBase
{
    private const float playerSpeedMod = 3f;
    private const float playerRotationMod = 5f;

    public PlayerShipFactory(PrefabsStorage prefabStorage) : base(prefabStorage)
    { }

    public override ShipData CreateShipData()
    {
        var stats = new ShipStatsContainer(playerRotationMod, playerSpeedMod);
        return new ShipData(stats);
    }

    protected override Transform GetPrefab()
    {
        return storage.GetPlayer();
    }




}

