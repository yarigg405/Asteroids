using UnityEngine;

public abstract class WeaponBaseController : BaseController, IWeapon
{
    private TransformInfo ownerShipTransform;

    //weaponStats
    private float maxCooldown;
    private float currentCooldown;

    //boltsStats
    protected virtual float boltSpeed => 1f;
    protected virtual float boltLifeTime => 1f;
    protected virtual int boltDamage => 1;

    public Team team { get; private set; }

    public WeaponBaseController(WeaponFactoryBase factory) : base(factory.GetServiceLocator())
    {
        maxCooldown = factory.maxCooldown;
        ownerShipTransform = factory.CreateTransformInfo();
    }

    public void SetOwnerShipTransform(TransformInfo transformInfo, Team _team)
    {
        ownerShipTransform = transformInfo;
        team = _team;
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
        trInfo.size = 0.1f;

        var unityTr = InstantiateBolt();
        unityTr.transform.localScale = Vector3.one;
        unityTr.position = trInfo.position;
        unityTr.eulerAngles = new Vector3(0, 0, trInfo.currentRadians * Mathf.Rad2Deg);

        var bolt = new BoltController(serviceLocator)
            .SetStats(stats)
            .SetTransformInfo(trInfo);
        bolt.SetUnityTransform(unityTr);
        bolt.SetTeam(this);
        return bolt;
    }

    protected abstract Transform InstantiateBolt();


    protected virtual bool IsCanShoot()
    {
        return currentCooldown <= 0;
    }

    protected virtual void ConsumeBolts()
    {
    }
}

