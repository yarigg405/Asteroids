using UnityEngine;

public class BoltController : BaseController
{
    public BoltStatsContainer BoltStats { get; internal set; }

    public Team Team { get; private set; }

    protected override PrefabType PrefabType => PrefabType.Bullet;

    public BoltController(IServiceLocator locator) : base(locator)
    { }

    public void SetUnityTransform(Transform unityTransform)
    {
        this.UnityTransform = unityTransform;
    }

    public void SetTeam(WeaponBaseController weapon)
    {
        Team = weapon.Team;
    }


    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        BoltStats.LifeTime -= deltaTime;
        if (BoltStats.LifeTime <= 0) Dispose();
    }
}

public static class BoltControllerExtensions
{
    public static BoltController SetStats(this BoltController bolt, BoltStatsContainer stats)
    {
        bolt.BoltStats = stats;
        return bolt;
    }

    public static BoltController SetTransformInfo(this BoltController bolt, TransformInfo trInfo)
    {
        bolt.TransformInfo = trInfo;
        return bolt;
    }
}
