using UnityEngine;


public class AiPilotController : BaseController
{
    private ShipController controlledShip;
    private readonly float fireRate;
    private float currentRechargeTime;

    protected override PrefabType PrefabType => PrefabType.Null;

    public AiPilotController(IServiceLocator locator, float fireRate, ShipController shipController) : base(
        locator)
    {
        this.fireRate = fireRate;
        controlledShip = shipController;
    }



    public override void OnUpdate(float deltaTime)
    {
        if (controlledShip == null || controlledShip.IsDisposed)
        {
            Dispose();
            return;
        }


        currentRechargeTime -= deltaTime;
        if (currentRechargeTime <= 0)
        {
            currentRechargeTime = fireRate;
            controlledShip.SetWeaponFire();
        }

        var player = Locator.Get<IPositionsHandler>().PlayerTransform;
        var ship = controlledShip.ShipData;

        var deltaVector = (player.Position - ship.TransformInfo.Position).normalized;
        ship.TransformInfo.Velocity = deltaVector * ship.Stats.SpeedModifier;
        var angle = Mathf.Atan2(deltaVector.y, deltaVector.x) - 1.570796f; //90 degrees in rads
        ship.TransformInfo.CurrentRadians = angle;
    }

    public override void Dispose()
    {
        controlledShip = null;
        base.Dispose();
    }
}

