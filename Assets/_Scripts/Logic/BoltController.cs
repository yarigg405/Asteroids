using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : BaseController, IBolt
{
    private BoltData boltData;


    public BoltController(BoltFactoryBase factory, TransformInfo transformInfo) : base()
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        boltData = factory.CreateBoltData(transformInfo);
        unityTransform = factory.CreateUnityTransform();
        unityTransform.position = transformInfo.position;
        unityTransform.eulerAngles = new Vector3(0, 0, transformInfo.currentRadians * Mathf.Rad2Deg);
    }

    public override void OnUpdate(float deltatime)
    {
        var tr = boltData.transformInfo;
        tr.position += tr.velocity * deltatime;
        unityTransform.position = tr.position;
    }
}

public class BoltData
{
    public TransformInfo transformInfo { get; private set; }
    public BoltStatsContainer statsContainer { get; private set; }

    public BoltData(BoltStatsContainer boltStats, TransformInfo boltTransformInfo)
    {
        transformInfo = new TransformInfo(boltTransformInfo);
        statsContainer = boltStats;
    }
}

public class BoltStatsContainer
{
    public float boltSpeed { get; private set; }
    public float lifeTime { get; private set; }

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
