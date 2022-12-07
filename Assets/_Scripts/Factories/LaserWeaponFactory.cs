using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeaponFactory : WeaponFactoryBase
{
    public override float maxCooldown => 1f;

    public LaserWeaponFactory(LinksMaster links) : base(links)
    { }

    public override TransformInfo CreateTransformInfo()
    {
        return new TransformInfo();
    }

    protected override PrefabType GetPrefabType()
    {
        return PrefabType.Null;
    }

    protected override int GetPrefabId()
    {
        return -1;
    }
}



