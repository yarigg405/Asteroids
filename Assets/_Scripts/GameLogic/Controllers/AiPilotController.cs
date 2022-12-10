using UnityEngine;


public class AiPilotController : BaseController
{
    private ShipController controlledShip;
    private float fireRate;
    private float currentRechargeTime = 0;

    protected override PrefabType prefabType => PrefabType.Null;

    public AiPilotController(LinksMaster _linksMaster, float _fireRate, ShipController shipController) : base(_linksMaster)
    {
        fireRate = _fireRate;
        controlledShip = shipController;
    }



    public override void OnUpdate(float deltatime)
    {
        if (controlledShip == null || controlledShip.isDisposed)
        {
            Dispose();
            return;
        }


        currentRechargeTime -= deltatime;
        if (currentRechargeTime <= 0)
        {
            currentRechargeTime = fireRate;
            controlledShip.SetWeaponFire();
        }

        var player = linksMaster.PositionsHandler.PlayerTransform;
        var ship = controlledShip.shipData;

        var deltaVector = (player.position - ship.transformInfo.position).normalized;
        ship.transformInfo.velocity = deltaVector * ship.stats.speedModifier;
        var angle = Mathf.Atan2(deltaVector.y, deltaVector.x) - 1.570796f;  //90 degs in rads
        ship.transformInfo.currentRadians = angle;
    }

    public override void Dispose()
    {
        if (controlledShip != null)
        {
            controlledShip = null;
        }

        base.Dispose();
    }
}

