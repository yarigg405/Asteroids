using UnityEngine;

public class WeaponLaser : WeaponBaseController
{
    protected override float BoltSpeed => 7f;
    protected override int BoltDamage => 2;

    protected override PrefabType PrefabType => PrefabType.Null;


    private int maxCountOfBolts;
    private int currentCountOfBolts;

    private float rechargeTime;
    private float currentRechargeTime;




    public WeaponLaser(WeaponFactoryBase factory) : base(factory)
    { }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (currentRechargeTime < rechargeTime)
        {
            currentRechargeTime += deltaTime;

            Locator.Get<PlayerShipConditionLogger>().CurrentLaserRechargeTime = rechargeTime - currentRechargeTime;
        }
        else
        {
            if (currentCountOfBolts < maxCountOfBolts)
            {
                currentCountOfBolts++;
                Locator.Get<PlayerShipConditionLogger>().CurrentLaserCount = currentCountOfBolts;
                if (currentCountOfBolts < maxCountOfBolts)
                    currentRechargeTime = 0;
                Locator.Get<PlayerShipConditionLogger>().CurrentLaserRechargeTime = rechargeTime - currentRechargeTime;
            }
        }


    }

    protected override Transform InstantiateBolt()
    {
        return Locator.Get<ISpawner>().SpawnUnityTransform(PrefabType.Bullet, 1);
    }

    public void SetMaxBoltsCount(int count)
    {
        maxCountOfBolts = count;
        currentCountOfBolts = count;

        Locator.Get<ILogicDelayer>().AddDelay(() =>
        {
            Locator.Get<PlayerShipConditionLogger>().CurrentLaserCount = currentCountOfBolts;
        });
    }

    public void SetBoltsRechargingTime(float time)
    {
        rechargeTime = time;
        currentRechargeTime = time;
        Locator.Get<ILogicDelayer>().AddDelay(() =>
        {
            Locator.Get<PlayerShipConditionLogger>().CurrentLaserRechargeTime = rechargeTime - currentRechargeTime;
        });
    }

    protected override void ConsumeBolts()
    {
        currentCountOfBolts--;
        currentRechargeTime = 0;
        Locator.Get<PlayerShipConditionLogger>().CurrentLaserRechargeTime = rechargeTime - currentRechargeTime;
        Locator.Get<PlayerShipConditionLogger>().CurrentLaserCount = currentCountOfBolts;
    }

    protected override bool IsCanShoot()
    {
        if (currentCountOfBolts > 0)
            return base.IsCanShoot();

        return false;
    }

}


