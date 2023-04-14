public abstract class ShipFactoryBase : UnityFactoryBase
{
    protected ShipFactoryBase(IServiceLocator serviceLocator) : base(serviceLocator)
    { }

    public abstract ShipData CreateShipData();
}

