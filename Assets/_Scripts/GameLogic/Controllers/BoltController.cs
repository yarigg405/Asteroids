using UnityEngine;

public class BoltController : BaseController
{
    public BoltStatsContainer boltStats { get; internal set; }

    public Team team { get; private set; }

    protected override PrefabType prefabType => PrefabType.Bullet;

    public BoltController(LinksMaster linkMaster) : base(linkMaster)
    { }

    public void SetUnityTransform(Transform unityTransform)
    {
        this.unityTransform = unityTransform;
    }

    public void SetTeam(WeaponBaseController weapon)
    {
        team = weapon.team;
    }


    public override void OnUpdate(float deltatime)
    {
        base.OnUpdate(deltatime);

        boltStats.lifeTime -= deltatime;
        if (boltStats.lifeTime <= 0) Dispose();
    }
}

public static class BoltControllerExtensions
{
    public static BoltController SetStats(this BoltController bolt, BoltStatsContainer stats)
    {
        bolt.boltStats = stats;
        return bolt;
    }

    public static BoltController SetTransformInfo(this BoltController bolt, TransformInfo trInfo)
    {
        bolt.transformInfo = trInfo;
        return bolt;
    }
}
