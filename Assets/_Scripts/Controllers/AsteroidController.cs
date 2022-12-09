using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AsteroidController : BaseController
{
    protected override PrefabType prefabType => PrefabType.Asteroid;
    private int numOfPrefab = 0;

    public AsteroidController(LinksMaster _linksMaster, TransformInfo trInfo, Transform unityTr)
        : base(_linksMaster)
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
            trInfo.size = transformInfo.size * 0.7f;
            var spd = transformInfo.velocity.magnitude;
            spd *= 1.3f;
            var radians = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var velX = Mathf.Sin(radians) * spd;
            var velY = Mathf.Cos(radians) * spd;
            trInfo.velocity = new Vector2(-velX, velY);

            var unityTr = linksMaster.Spawner.SpawnUnityTransform(PrefabType.Asteroid, ++numOfPrefab);

            var shard = new AsteroidController(linksMaster, trInfo, unityTr);
            shard.numOfPrefab = numOfPrefab;
        }
    }


}

