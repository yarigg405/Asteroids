public class TurretWeaponFactory : WeaponFactoryBase
{
    public override float maxCooldown => 0.1f;

    public TurretWeaponFactory(IServiceLocator _serviceLocator) : base(_serviceLocator)
    { }

    public override TransformInfo CreateTransformInfo()
    {
        return new TransformInfo();
    }

    protected override PrefabType GetPrefabType()
    {
        return PrefabType.Null;
    }

    protected override int GetPrefabId()
    {
        return -1;
    }
}



