using UnityEngine;

public class WeaponLaser : WeaponBaseController
{
    protected override float boltSpeed => 7f;
    protected override int boltDamage => 2;

    protected override PrefabType prefabType => PrefabType.Null;


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
            linksMaster.PlayerLogger.currentLaserRechargeTime = rechargeTime - currentRechargeTime;
        }
        else
        {
            if (currentCountOfBolts < maxCountOfBolts)
            {
                currentCountOfBolts++;
                linksMaster.PlayerLogger.currentLaserCount = currentCountOfBolts;
                if (currentCountOfBolts < maxCountOfBolts)
                    currentRechargeTime = 0;
                linksMaster.PlayerLogger.currentLaserRechargeTime = rechargeTime - currentRechargeTime;
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
        linksMaster.LogicDelayer.AddDelay(() =>
        {
            linksMaster.PlayerLogger.currentLaserCount = currentCountOfBolts;
        });
    }

    public void SetBoltsRechargingTime(float time)
    {
        rechargeTime = time;
        currentRechargeTime = time;
        linksMaster.LogicDelayer.AddDelay(() =>
        {
            linksMaster.PlayerLogger.currentLaserRechargeTime = rechargeTime - currentRechargeTime;
        });
    }

    protected override void ConsumeBolts()
    {
        currentCountOfBolts--;
        currentRechargeTime = 0;
        linksMaster.PlayerLogger.currentLaserRechargeTime = rechargeTime - currentRechargeTime;
        linksMaster.PlayerLogger.currentLaserCount = currentCountOfBolts;
    }

    protected override bool IsCanShoot()
    {
        if (currentCountOfBolts > 0)
            return base.IsCanShoot();

        return false;
    }

}


