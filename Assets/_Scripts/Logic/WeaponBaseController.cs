using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseController : BaseController
{
    private TransformInfo ownerShipTransform;
    protected PrefabsStorage prefabsStorage;

    //weaponStats
    private float maxCooldown;
    private float currentCooldown;

    //boltsStats
    protected virtual float boltSpeed => 1f;
    protected virtual float boltLifeTime => 1f;
    protected virtual int boltDamage => 1;

    public WeaponBaseController(WeaponFactoryBase factory)
    {
        maxCooldown = factory.maxCooldown;
        ownerShipTransform = factory.CreateTransformInfo();
        prefabsStorage = factory.GetPrefabStorage();
    }

    public WeaponBaseController SetOwnerShipTransform(TransformInfo transformInfo)
    {
        ownerShipTransform = transformInfo;
        return this;
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
            var bolt = CreateBolt();
            ConsumeBolts();
        }
    }

    private BoltController CreateBolt()
    {
        var stats = new BoltStatsContainer(boltSpeed, boltLifeTime, boltDamage);
        var trInfo = new TransformInfo(ownerShipTransform);

        var velX = Mathf.Sin(trInfo.currentRadians) * stats.boltSpeed;
        var velY = Mathf.Cos(trInfo.currentRadians) * stats.boltSpeed;
        trInfo.velocity = new Vector2(-velX, velY);

        var prefab = GetBoltPrefab();
        var unityTr = GameObject.Instantiate(prefab);
        unityTr.transform.localScale = Vector3.one;
        unityTr.position = trInfo.position;
        unityTr.eulerAngles = new Vector3(0, 0, trInfo.currentRadians * Mathf.Rad2Deg);

        return new BoltController(stats, trInfo, unityTr);
    }

    protected abstract Transform GetBoltPrefab();


    protected virtual bool IsCanShoot()
    {
        return currentCooldown <= 0;
    }

    protected virtual void ConsumeBolts()
    {
    }


}

