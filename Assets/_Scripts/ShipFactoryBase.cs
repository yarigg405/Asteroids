using UnityEngine;

public abstract class ShipFactoryBase : UnityFactoryBase
{
    public ShipFactoryBase(PrefabsStorage prefabStorage) : base(prefabStorage)
    {}

    public abstract ShipData CreateShipData();  
}

