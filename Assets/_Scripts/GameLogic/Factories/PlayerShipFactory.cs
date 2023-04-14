public class PlayerShipFactory : ShipFactoryBase
{
    private const float PlayerSpeedMod = 2f;
    private const float PlayerRotationMod = 5f;

    private const int LaserBoltsCount = 5;
    private const float LaserBoltsRechargeTime = 3f;

    public PlayerShipFactory(IServiceLocator serviceLocator) : base(serviceLocator)
    { }

    public virtual WeaponBaseController CreateMainWeapon()
    {
        var weaponFactory = new TurretWeaponFactory(ServiceLocator);
        var weapon = new WeaponTurret(weaponFactory);

        return weapon;
    }

    public virtual WeaponBaseController CreateSecondaryWeapon()
    {
        var weaponFactory = new LaserWeaponFactory(ServiceLocator);
        var weapon = new WeaponLaser(weaponFactory);
        weapon.SetMaxBoltsCount(LaserBoltsCount);
        weapon.SetBoltsRechargingTime(LaserBoltsRechargeTime);

        return weapon;
    }

    public override ShipData CreateShipData()
    {
        var stats = new ShipStatsContainer(PlayerRotationMod, PlayerSpeedMod);
        return new ShipData(stats)
            .SetMainWeapon(CreateMainWeapon(), Team.Player)
            .SetSecondaryWeapon(CreateSecondaryWeapon(), Team.Player)
        ;
    }

    protected override PrefabType GetPrefabType()
    {
        return PrefabType.PlayerShip;
    }

    protected override int GetPrefabId()
    {
        return 0;
    }




}

