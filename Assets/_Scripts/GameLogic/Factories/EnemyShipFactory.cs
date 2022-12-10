

public class EnemyShipFactory : ShipFactoryBase
{
    private const float speedMod = 1f;
    private const float rotationMod = 5f;

    public EnemyShipFactory(LinksMaster _linksMaster) : base(_linksMaster)
    { }

    public override WeaponBaseController CreateMainWeapon()
    {
        var weaponFactory = new TurretWeaponFactory(linksMaster);
        var weapon = new WeaponTurret(weaponFactory);

        return weapon;
    }

    public override WeaponBaseController CreateSecondaryWeapon()
    {
        //enemy ships has no second weapon
        throw new System.NotImplementedException();
    }

    public override ShipData CreateShipData()
    {
        var stats = new ShipStatsContainer(rotationMod, speedMod);
        return new ShipData(stats)
            .SetMainWeapon(CreateMainWeapon(), Team.Enemy);
    }

    protected override int GetPrefabId()
    {
        return 0;
    }

    protected override PrefabType GetPrefabType()
    {
        return PrefabType.UFO;
    }
}

