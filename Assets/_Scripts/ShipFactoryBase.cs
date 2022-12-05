using UnityEngine;

public abstract class ShipFactoryBase : UnityFactoryBase
{
    public ShipFactoryBase(PrefabsStorage prefabStorage) : base(prefabStorage)
    {}

    public abstract ShipData CreateShipData();

    public abstract WeaponBaseController CreateMainWeapon();

    public abstract WeaponBaseController CreateSecondaryWeapon();
}

