public class LaserWeaponFactory : WeaponFactoryBase
{
    public override float MaxCooldown => 1f;

    public LaserWeaponFactory(IServiceLocator serviceLocator) : base(serviceLocator)
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



