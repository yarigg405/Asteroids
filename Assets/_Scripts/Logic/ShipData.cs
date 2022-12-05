using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

public class TransformInfo
{
    public Vector2 velocity { get; set; }
    public Vector2 position { get; set; }
    public float currentRadians { get; set; }

    public TransformInfo()
    {
        velocity = Vector2.zero;
        position = Vector2.zero;
        currentRadians = 0f;
    }

    public TransformInfo(TransformInfo tr)
    {
        velocity = tr.velocity;
        position = tr.position;
        currentRadians = tr.currentRadians;
    }
}



public class ShipStatsContainer
{
    public float rotationModifier { get; private set; }
    public float speedModifier { get; private set; }

    public ShipStatsContainer(float rotation, float speed)
    {
        rotationModifier = rotation;
        speedModifier = speed;
    }
}


