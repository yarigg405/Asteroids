

public class ShipData
{
    public ShipStatsContainer Stats { get; }
    public TransformInfo TransformInfo { get; protected set; }
    public IWeapon MainWeapon { get; internal set; }
    public IWeapon SecondaryWeapon { get; internal set; }


    public ShipData(ShipStatsContainer shipStats)
    {
        TransformInfo = new TransformInfo
        {
            Size = 0.4f
        };
        Stats = shipStats;
    }


}

public static class ShipDataExtensions
{
    public static ShipData SetMainWeapon(this ShipData ship, IWeapon wep, Team team)
    {
        ship.MainWeapon = wep;
        wep.SetOwnerShipTransform(ship.TransformInfo, team);
        return ship;
    }

    public static ShipData SetSecondaryWeapon(this ShipData ship, IWeapon wep, Team team)
    {
        ship.SecondaryWeapon = wep;
        wep.SetOwnerShipTransform(ship.TransformInfo, team);
        return ship;
    }

}


