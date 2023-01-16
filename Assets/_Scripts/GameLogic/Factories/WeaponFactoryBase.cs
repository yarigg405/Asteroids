public abstract class WeaponFactoryBase : UnityFactoryBase
{
    public virtual float maxCooldown { get => 0f; }

    public WeaponFactoryBase(IServiceLocator _serviceLocator) : base(_serviceLocator)
    { }

    public abstract TransformInfo CreateTransformInfo();

}

