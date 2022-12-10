

public class ShipData
{
    public ShipStatsContainer stats { get; private set; }
    public TransformInfo transformInfo { get; protected set; }
    public WeaponBaseController mainWeapon { get; internal set; }
    public WeaponBaseController secondaryWeapon { get; internal set; }


    public ShipData(ShipStatsContainer shipStats)
    {
        transformInfo = new TransformInfo();
        transformInfo.size = 0.5f;
        stats = shipStats;
    }


}

internal static class ShipDataExtensions
{
    public static ShipData SetMainWeapon(this ShipData ship, WeaponBaseController wep, Team team)
    {
        ship.mainWeapon = wep;
        wep.SetOwnerShipTransform(ship.transformInfo, team);
        return ship;
    }

    public static ShipData SetSecondaryWeapon(this ShipData ship, WeaponBaseController wep, Team team)
    {
        ship.secondaryWeapon = wep;
        wep.SetOwnerShipTransform(ship.transformInfo, team);
        return ship;
    }

}


