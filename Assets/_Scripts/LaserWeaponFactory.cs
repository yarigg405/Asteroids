using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeaponFactory : WeaponFactoryBase
{
    public override float maxCooldown => 1f;

    public LaserWeaponFactory(PrefabsStorage prefabStorage) : base(prefabStorage)
    { }

    public override TransformInfo CreateTransformInfo()
    {
        return new TransformInfo();
    }

    protected override Transform GetPrefab()
    {
        return null;
    }
}



