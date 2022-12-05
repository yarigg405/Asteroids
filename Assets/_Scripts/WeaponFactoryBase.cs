using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFactoryBase : UnityFactoryBase
{
    public virtual float maxCooldown { get => 0f; }

    public WeaponFactoryBase(PrefabsStorage prefabStorage) : base(prefabStorage)
    { }

    public abstract TransformInfo CreateTransformInfo();

}

