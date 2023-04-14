using UnityEngine;

public abstract class WeaponBaseController : BaseController, IWeapon
{
    private TransformInfo ownerShipTransform;

    //weaponStats
    private readonly float maxCooldown;
    private float currentCooldown;

    //boltsStats
    protected virtual float BoltSpeed => 1f;
    protected virtual float BoltLifeTime => 1f;
    protected virtual int BoltDamage => 1;

    public Team Team { get; private set; }

    protected WeaponBaseController(WeaponFactoryBase factory) : base(factory.GetServiceLocator())
    {
        maxCooldown = factory.MaxCooldown;
        ownerShipTransform = factory.CreateTransformInfo();
    }

    public void SetOwnerShipTransform(TransformInfo trInfo, Team team)
    {
        ownerShipTransform = trInfo;
        Team = team;
    }


    public override void OnUpdate(float deltaTime)
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= deltaTime;
        }
    }

    public void TryShoot()
    {
        if (IsCanShoot())
        {
            currentCooldown = maxCooldown;
            CreateBolt();
            ConsumeBolts();
        }
    }

    private void CreateBolt()
    {
        var stats = new BoltStatsContainer(BoltSpeed, BoltLifeTime, BoltDamage);
        var trInfo = new TransformInfo(ownerShipTransform);

        var velX = Mathf.Sin(trInfo.CurrentRadians) * stats.BoltSpeed;
        var velY = Mathf.Cos(trInfo.CurrentRadians) * stats.BoltSpeed;
        trInfo.Velocity = new Vector2(-velX, velY);
        trInfo.Size = 0.1f;

        var unityTr = InstantiateBolt();
        unityTr.transform.localScale = Vector3.one;
        unityTr.position = trInfo.Position;
        unityTr.eulerAngles = new Vector3(0, 0, trInfo.CurrentRadians * Mathf.Rad2Deg);

        var bolt = new BoltController(Locator)
            .SetStats(stats)
            .SetTransformInfo(trInfo);
        bolt.SetUnityTransform(unityTr);
        bolt.SetTeam(this);
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

