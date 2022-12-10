

public class ShipData
{
    public ShipStatsContainer stats { get; private set; }
    public TransformInfo transformInfo { get; protected set; }
    public IWeapon mainWeapon { get; internal set; }
    public IWeapon secondaryWeapon { get; internal set; }


    public ShipData(ShipStatsContainer shipStats)
    {
        transformInfo = new TransformInfo();
        transformInfo.size = 0.4f;
        stats = shipStats;
    }


}

public static class ShipDataExtensions
{
    public static ShipData SetMainWeapon(this ShipData ship, IWeapon wep, Team team)
    {
        ship.mainWeapon = wep;
        wep.SetOwnerShipTransform(ship.transformInfo, team);
        return ship;
    }

    public static ShipData SetSecondaryWeapon(this ShipData ship, IWeapon wep, Team team)
    {
        ship.secondaryWeapon = wep;
        wep.SetOwnerShipTransform(ship.transformInfo, team);
        return ship;
    }

}


