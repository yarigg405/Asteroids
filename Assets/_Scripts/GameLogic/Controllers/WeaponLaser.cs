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

            serviceLocator.Get<PlayerShipConditionLogger>().currentLaserRechargeTime = rechargeTime - currentRechargeTime;
        }
        else
        {
            if (currentCountOfBolts < maxCountOfBolts)
            {
                currentCountOfBolts++;
                serviceLocator.Get<PlayerShipConditionLogger>().currentLaserCount = currentCountOfBolts;
                if (currentCountOfBolts < maxCountOfBolts)
                    currentRechargeTime = 0;
                serviceLocator.Get<PlayerShipConditionLogger>().currentLaserRechargeTime = rechargeTime - currentRechargeTime;
            }
        }


    }

    protected override Transform InstantiateBolt()
    {
        return serviceLocator.Get<ISpawner>().SpawnUnityTransform(PrefabType.Bullet, 1);
    }

    public void SetMaxBoltsCount(int count)
    {
        maxCountOfBolts = count;
        currentCountOfBolts = count;

        serviceLocator.Get<ILogicDelayer>().AddDelay(() =>
        {
            serviceLocator.Get<PlayerShipConditionLogger>().currentLaserCount = currentCountOfBolts;
        });
    }

    public void SetBoltsRechargingTime(float time)
    {
        rechargeTime = time;
        currentRechargeTime = time;
        serviceLocator.Get<ILogicDelayer>().AddDelay(() =>
        {
            serviceLocator.Get<PlayerShipConditionLogger>().currentLaserRechargeTime = rechargeTime - currentRechargeTime;
        });
    }

    protected override void ConsumeBolts()
    {
        currentCountOfBolts--;
        currentRechargeTime = 0;
        serviceLocator.Get<PlayerShipConditionLogger>().currentLaserRechargeTime = rechargeTime - currentRechargeTime;
        serviceLocator.Get<PlayerShipConditionLogger>().currentLaserCount = currentCountOfBolts;
    }

    protected override bool IsCanShoot()
    {
        if (currentCountOfBolts > 0)
            return base.IsCanShoot();

        return false;
    }

}


