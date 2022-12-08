using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AsteroidController : BaseController
{
    protected override PrefabType prefabType => PrefabType.Asteroid;
    private int numOfPrefab = 0;
    public TransformInfo transformInfo { get; internal set; }


    public AsteroidController(LinksMaster _linksMaster, TransformInfo trInfo, Transform unityTr)
        : base(_linksMaster)
    {
        transformInfo = trInfo;
        unityTransform = unityTr;
    }


    public override void OnUpdate(float deltatime)
    {
        transformInfo.position += transformInfo.velocity * deltatime;
        unityTransform.position = transformInfo.position;
    }

    public void GetDamage(int damage)
    {
        if (damage == 1 && numOfPrefab < 2)
        {
            SpawnShards();
        }

        Dispose();
    }

    private void SpawnShards()
    {
        var shardsCount = Random.Range(2, 4);

        for (int i = 0; i < shardsCount; i++)
        {
            var trInfo = new TransformInfo(transformInfo);
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

