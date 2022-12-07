using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaser : WeaponBaseController
{
    protected override float boltSpeed => 7f;
    protected override float boltLifeTime => 7f;
    protected override int boltDamage => 2;


    private int maxCountOfBolts;
    private int currentCountOfBolts;

    private float rechargeTime;
    private float currentRechargeTime;



    public WeaponLaser(WeaponFactoryBase factory) : base(factory)
    { }

    public override void OnUpdate(float deltatime)
    {
        base.OnUpdate(deltatime);
        if (currentRechargeTime < rechargeTime)
        {
            currentRechargeTime += deltatime;
        }
        else
        {
            if (currentCountOfBolts < maxCountOfBolts)
            {
                currentCountOfBolts++;
                currentRechargeTime = 0;
            }
        }


    }

    protected override Transform InstantiateBolt()
    {
        return linksMaster.Spawner.SpawnUnityTransform(PrefabType.Bullet, 1);
    }

    public void SetMaxBoltsCount(int count)
    {
        maxCountOfBolts = count;
        currentCountOfBolts = count;
    }

    public void SetBoltsRechargingTime(float time)
    {
        rechargeTime = time;
        currentRechargeTime = 0f;
    }

    protected override void ConsumeBolts()
    {
        currentCountOfBolts--;
    }

    protected override bool IsCanShoot()
    {
        if (currentCountOfBolts > 0)
            return base.IsCanShoot();

        return false;
    }

}


