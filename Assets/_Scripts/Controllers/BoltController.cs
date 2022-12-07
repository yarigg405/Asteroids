using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : BaseController, IBolt
{
    public BoltStatsContainer boltStats { get; internal set; }
    public TransformInfo transformInfo { get; internal set; }

    protected override PrefabType prefabType => PrefabType.Bullet;

    public BoltController(LinksMaster linkMaster) : base(linkMaster)
    { }

    public void SetUnityTransform(Transform unityTransform)
    {
        this.unityTransform = unityTransform;
    }


    public override void OnUpdate(float deltatime)
    {
        transformInfo.position += transformInfo.velocity * deltatime;
        unityTransform.position = transformInfo.position;

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

public interface IBolt
{
}
