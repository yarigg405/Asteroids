public abstract class WeaponFactoryBase : UnityFactoryBase
{
    public virtual float maxCooldown { get => 0f; }

    public WeaponFactoryBase(LinksMaster _linksMaster) : base(_linksMaster)
    { }

    public abstract TransformInfo CreateTransformInfo();

}

