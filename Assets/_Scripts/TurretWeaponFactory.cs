using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWeaponFactory : WeaponFactoryBase
{
    public override float maxCooldown => 0.1f;

    public TurretWeaponFactory(PrefabsStorage prefabStorage) : base(prefabStorage)
    {}

    public override TransformInfo CreateTransformInfo()
    {
        return new TransformInfo();
    }

    protected override Transform GetPrefab()
    {
        return null;
    }
}



