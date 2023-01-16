﻿using UnityEngine;

public class AsteroidController : BaseController
{
    protected override PrefabType prefabType => PrefabType.Asteroid;
    private int numOfPrefab = 0;
    protected override int scoresByDestroy { get => 100; }

    public AsteroidController(IServiceLocator _serviceLocator, TransformInfo trInfo, Transform unityTr)
        : base(_serviceLocator)
    {
        transformInfo = trInfo;
        unityTransform = unityTr;
    }

    public void GetDamage(int damage)
    {
        if (damage == 1 && numOfPrefab < 2)
        {
            SpawnShards();
        }

        Dispose();
    }

    protected override void CheckCollisions()
    {
        var nearestBolts = fieldCell.Get<BoltController>();
        foreach (var item in nearestBolts)
        {
            var bolt = item as BoltController;
            if (bolt.team == Team.Player)
            {
                var distance = (transformInfo.position - bolt.transformInfo.position).sqrMagnitude;
                var radius = (transformInfo.size + bolt.transformInfo.size) *
                    (transformInfo.size + bolt.transformInfo.size);
                if (distance < radius)
                {
                    var dmg = bolt.boltStats.boltDamage;
                    GetDamage(dmg);

                    if (dmg < 2)
                        bolt.Dispose();
                }

            }
        }
    }

    private void SpawnShards()
    {
        var shardsCount = Random.Range(2, 4);

        for (int i = 0; i < shardsCount; i++)
        {
            var trInfo = new TransformInfo(transformInfo);
            trInfo.size = transformInfo.size * 0.5f;
            var spd = transformInfo.velocity.magnitude;
            spd *= 1.3f;
            var radians = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var velX = Mathf.Sin(radians) * spd;
            var velY = Mathf.Cos(radians) * spd;
            trInfo.velocity = new Vector2(-velX, velY);

            var unityTr = serviceLocator.Get<ISpawner>().SpawnUnityTransform(PrefabType.Asteroid, ++numOfPrefab);

            var shard = new AsteroidController(serviceLocator, trInfo, unityTr);
            shard.numOfPrefab = numOfPrefab;
            unityTr.position = trInfo.position;
        }
    }


}

