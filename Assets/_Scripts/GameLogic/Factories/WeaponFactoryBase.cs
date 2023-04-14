public abstract class WeaponFactoryBase : UnityFactoryBase
{
    public virtual float MaxCooldown => 0f;

    protected WeaponFactoryBase(IServiceLocator serviceLocator) : base(serviceLocator)
    { }

    public abstract TransformInfo CreateTransformInfo();

}

