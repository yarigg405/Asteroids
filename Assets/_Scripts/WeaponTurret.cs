using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTurret : WeaponBaseController
{
    protected override float boltSpeed => 5f;
    protected override float boltLifeTime => 10f;

    public WeaponTurret(WeaponFactoryBase factory) : base(factory)
    { }

    protected override Transform GetBoltPrefab()
    {
        return prefabsStorage.GetBullet(0);
    }


}

