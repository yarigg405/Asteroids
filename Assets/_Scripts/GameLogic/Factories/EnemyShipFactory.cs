

public class EnemyShipFactory : ShipFactoryBase
{
    private const float SpeedMod = 1f;
    private const float RotationMod = 5f;

    public EnemyShipFactory(IServiceLocator serviceLocator) : base(serviceLocator)
    { }

    public virtual WeaponBaseController CreateMainWeapon()
    {
        var weaponFactory = new TurretWeaponFactory(ServiceLocator);
        var weapon = new WeaponTurret(weaponFactory);

        return weapon;
    }

    public override ShipData CreateShipData()
    {
        var stats = new ShipStatsContainer(RotationMod, SpeedMod);
        return new ShipData(stats)
            .SetMainWeapon(CreateMainWeapon(), Team.Enemy);
    }

    protected override int GetPrefabId()
    {
        return 0;
    }

    protected override PrefabType GetPrefabType()
    {
        return PrefabType.Ufo;
    }
}

