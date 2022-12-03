using UnityEngine;

public abstract class ShipFactoryBase
{
    protected PrefabsStorage storage;


    public ShipFactoryBase(PrefabsStorage prefabStorage)
    {
        storage = prefabStorage;
    }

    public abstract ShipData CreateShipData();

    public abstract Transform CreateUnityTransform();

}

