using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : BaseController, IBolt
{
    public BoltStatsContainer boltStats { get; private set; }
    public TransformInfo transformInfo { get; private set; }

    public BoltController(BoltStatsContainer stats, TransformInfo trInfo, Transform unityTransform)
    {
        boltStats = stats;
        transformInfo = trInfo;
        this.unityTransform = unityTransform;
    }

    //public BoltController(BoltFactoryBase factory, TransformInfo transformInfo) : base()
    //{
    //    if (factory == null)
    //    {
    //        throw new ArgumentNullException(nameof(factory));
    //    }

    //    boltData = factory.CreateBoltData(transformInfo);
    //    unityTransform = factory.CreateUnityTransform();
    //    unityTransform.position = transformInfo.position;
    //    unityTransform.eulerAngles = new Vector3(0, 0, transformInfo.currentRadians * Mathf.Rad2Deg);
    //}

    public override void OnUpdate(float deltatime)
    {
        transformInfo.position += transformInfo.velocity * deltatime;
        unityTransform.position = transformInfo.position;

        boltStats.lifeTime -= deltatime;
        if (boltStats.lifeTime <= 0) Dispose();
    }
}

public class BoltStatsContainer
{
    public float boltSpeed { get; private set; }
    public float lifeTime { get; set; }
    public int boltDamage { get; private set; }

    public BoltStatsContainer(float speed, float lifeTime, int damage)
    {
        boltSpeed = speed;
        this.lifeTime = lifeTime;
        boltDamage = damage;
    }
}

public interface IBolt
{
}
