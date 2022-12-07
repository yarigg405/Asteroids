using UnityEngine;

public abstract class ShipFactoryBase : UnityFactoryBase
{
    public ShipFactoryBase(LinksMaster _linksMaster) : base(_linksMaster)
    { }

    public abstract ShipData CreateShipData();

    public abstract WeaponBaseController CreateMainWeapon();

    public abstract WeaponBaseController CreateSecondaryWeapon();
}

