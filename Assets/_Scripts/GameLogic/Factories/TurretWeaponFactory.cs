public class TurretWeaponFactory : WeaponFactoryBase
{
    public override float MaxCooldown => 0.1f;

    public TurretWeaponFactory(IServiceLocator serviceLocator) : base(serviceLocator)
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



