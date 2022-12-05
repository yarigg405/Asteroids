using System.ComponentModel.Design.Serialization;
using UnityEngine;


public class PlayerShipFactory : ShipFactoryBase
{
    private const float playerSpeedMod = 3f;
    private const float playerRotationMod = 5f;

    private const int laserBoltsCount = 5;
    private const float laserBoltsRechargeTime = 3f;

    public PlayerShipFactory(PrefabsStorage prefabStorage) : base(prefabStorage)
    { }

    public override WeaponBaseController CreateMainWeapon()
    {
        var weaponFactory = new TurretWeaponFactory(storage);
        var weapon = new WeaponTurret(weaponFactory);

        return weapon;
    }

    public override WeaponBaseController CreateSecondaryWeapon()
    {
        var weaponFactory = new LaserWeaponFactory(storage);
        var weapon = new WeaponLaser(weaponFactory);
        weapon.SetMaxBoltsCount(laserBoltsCount);
        weapon.SetBoltsRechargingTime(laserBoltsRechargeTime);

        return weapon;
    }

    public override ShipData CreateShipData()
    {
        var stats = new ShipStatsContainer(playerRotationMod, playerSpeedMod);
        return new ShipData(stats);
    }

    protected override Transform GetPrefab()
    {
        return storage.GetPlayer();
    }




}

