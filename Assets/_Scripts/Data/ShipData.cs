using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class ShipData
{
    public ShipStatsContainer stats { get; private set; }
    public TransformInfo transformInfo { get; private set; }
    public WeaponBaseController mainWeapon { get; internal set; }
    public WeaponBaseController secondaryWeapon { get; internal set; }


    public ShipData(ShipStatsContainer shipStats)
    {
        transformInfo = new TransformInfo();
        stats = shipStats;
    }
}

internal static class ShipDataExtensions
{
    public static ShipData SetMainWeapon(this ShipData ship, WeaponBaseController wep)
    {
        ship.mainWeapon = wep;
        wep.SetOwnerShipTransform(ship.transformInfo);
        return ship;
    }

    public static ShipData SetSecondaryWeapon(this ShipData ship, WeaponBaseController wep)
    {
        ship.secondaryWeapon = wep;
        wep.SetOwnerShipTransform(ship.transformInfo);
        return ship;
    }

}


