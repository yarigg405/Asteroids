using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseController : BaseController
{
    private BoltFactoryBase boltFactory;
    private TransformInfo ownerShipTransform;

    private float maxCooldown;
    private float currentCooldown;

    public WeaponBaseController(WeaponFactoryBase factory)
    {
        maxCooldown = factory.maxCooldown;
        boltFactory = factory.CreateBoltFactory();
        ownerShipTransform = factory.CreateTransformInfo();
    }


    public override void OnUpdate(float deltatime)
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= deltatime;
        }
    }

    public void TryShoot()
    {
        if (IsCanShoot())
        {
            currentCooldown = maxCooldown;
            var bolt = new BoltController(boltFactory, ownerShipTransform);
        }
    }

    protected virtual bool IsCanShoot()
    {
        return currentCooldown <= 0;
    }


}

