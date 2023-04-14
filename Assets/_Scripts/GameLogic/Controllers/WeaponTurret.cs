using UnityEngine;

public class WeaponTurret : WeaponBaseController
{
    protected override float BoltSpeed => 5f;
    protected override PrefabType PrefabType => PrefabType.Null;

    public WeaponTurret(WeaponFactoryBase factory) : base(factory)
    { }


    protected override Transform InstantiateBolt()
    {
        return Locator.Get<ISpawner>().SpawnUnityTransform(PrefabType.Bullet, 0);
    }
}

