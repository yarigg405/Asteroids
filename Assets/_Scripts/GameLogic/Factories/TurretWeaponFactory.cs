public class TurretWeaponFactory : WeaponFactoryBase
{
    public override float maxCooldown => 0.1f;

    public TurretWeaponFactory(LinksMaster _linksMaster) : base(_linksMaster)
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



