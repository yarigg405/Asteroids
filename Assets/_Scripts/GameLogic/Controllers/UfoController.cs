using System;


public class UfoController : ShipController
{
    protected override PrefabType PrefabType => PrefabType.Ufo;
    protected override int ScoresByDestroy => 300;

    public UfoController(TransformInfo transformInfo, ShipFactoryBase factory) : base(factory)
    {
        Team = Team.Enemy;
        ShipData.TransformInfo.Velocity = transformInfo.Velocity;
        ShipData.TransformInfo.Position = transformInfo.Position;

    }

    protected override Type GetConcreteType()
    {
        return typeof(ShipController);
    }
}

