public abstract class ShipFactoryBase : UnityFactoryBase
{
    public ShipFactoryBase(IServiceLocator _serviceLocator) : base(_serviceLocator)
    { }

    public abstract ShipData CreateShipData();

    public abstract WeaponBaseController CreateMainWeapon();

    public abstract WeaponBaseController CreateSecondaryWeapon();
}

