using UnityEngine;


public class PlayerShipFactory : ShipFactoryBase
{
    private const float playerSpeedMod = 2f;
    private const float playerRotationMod = 5f;

    private const int laserBoltsCount = 5;
    private const float laserBoltsRechargeTime = 3f;

    public PlayerShipFactory(LinksMaster links) : base(links)
    { }

    public override WeaponBaseController CreateMainWeapon()
    {
        var weaponFactory = new TurretWeaponFactory(linksMaster);
        var weapon = new WeaponTurret(weaponFactory);

        return weapon;
    }

    public override WeaponBaseController CreateSecondaryWeapon()
    {
        var weaponFactory = new LaserWeaponFactory(linksMaster);
        var weapon = new WeaponLaser(weaponFactory);
        weapon.SetMaxBoltsCount(laserBoltsCount);
        weapon.SetBoltsRechargingTime(laserBoltsRechargeTime);

        return weapon;
    }

    public override ShipData CreateShipData()
    {
        var stats = new ShipStatsContainer(playerRotationMod, playerSpeedMod);
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

