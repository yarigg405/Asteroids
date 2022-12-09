using System;


public class UfoController : ShipController
{
    protected override PrefabType prefabType => PrefabType.UFO;

    public UfoController(TransformInfo transformInfo, ShipFactoryBase factory) : base(factory)
    {
        team = Team.Enemy;
        shipData.transformInfo.velocity = transformInfo.velocity;
        shipData.transformInfo.position = transformInfo.position;

    }

    protected override Type GetConcreteType()
    {
        return typeof(ShipController);
    }
}

