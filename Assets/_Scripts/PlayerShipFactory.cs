using UnityEngine;


public class PlayerShipFactory : ShipFactoryBase
{
    private const float playerSpeedMod = 3f;
    private const float playerRotationMod = 5f;

    public PlayerShipFactory(PrefabsStorage prefabStorage) : base(prefabStorage)
    { }

    public override ShipData CreateShipData()
    {
        var stats = new StatsContainer(playerRotationMod, playerSpeedMod);
        return new ShipData(stats);

    }

    public override Transform CreateUnityTransform()
    {
        var transform = GameObject.Instantiate(storage.GetPlayer(), Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;

        return transform;
    }




}

