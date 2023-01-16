public class LaserWeaponFactory : WeaponFactoryBase
{
    public override float maxCooldown => 1f;

    public LaserWeaponFactory(IServiceLocator _serviceLocator) : base(_serviceLocator)
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



