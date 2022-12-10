using UnityEngine;

public class WeaponTurret : WeaponBaseController
{
    protected override float boltSpeed => 5f;
    protected override PrefabType prefabType => PrefabType.Null;

    public WeaponTurret(WeaponFactoryBase factory) : base(factory)
    { }


    protected override Transform InstantiateBolt()
    {
        return linksMaster.Spawner.SpawnUnityTransform(PrefabType.Bullet, 0);
    }
}

