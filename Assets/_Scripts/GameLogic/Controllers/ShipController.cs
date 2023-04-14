using System;
using UnityEngine;

public class ShipController : BaseController, IPlayerControlled
{
    public ShipData ShipData { get;}
    private Vector2 playerInput;
    public override TransformInfo TransformInfo => ShipData.TransformInfo;

    protected override PrefabType PrefabType => PrefabType.PlayerShip;

    public Team Team { get; protected set; }

    public ShipController(ShipFactoryBase factory) : base(factory.GetServiceLocator())
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        playerInput = Vector2.zero;
        ShipData = factory.CreateShipData();
        UnityTransform = factory.CreateUnityTransform();

        Team = Team.Player;
    }


    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        var hor = playerInput.x * deltaTime;
        var vert = playerInput.y > 0 ? playerInput.y * deltaTime : 0;

        Thrust(vert);
        Rotate(hor);
        CalcMovement(deltaTime);

        MoveGameObject(ShipData.TransformInfo);
        playerInput.x = 0;
        playerInput.y = 0;
    }

    protected override void CheckCollisions()
    {
        CheckCollisionBolts();
        if (Team == Team.Player)
        {
            CheckCollisionAsteroid();
            CheckCollisionUfos();
        }
    }

    private void CheckCollisionBolts()
    {
        var nearestBolts = FieldCell.Get<BoltController>();
        foreach (var item in nearestBolts)
        {
            var bolt = item as BoltController;
            if (bolt.Team != Team)
            {
                if (IsCollide(ShipData.TransformInfo, bolt.TransformInfo))
                {
                    if (Team == Team.Player)
                    {
                        PlayerDeath();
                    }

                    bolt.Dispose();
                    Dispose();
                }

            }
        }
    }

    private void CheckCollisionAsteroid()
    {
        var nearestAsteroids = FieldCell.Get<AsteroidController>();
        foreach (var item in nearestAsteroids)
        {
            var aster = item as AsteroidController;
            if (IsCollide(ShipData.TransformInfo, aster.TransformInfo))
            {
                PlayerDeath();

                aster.GetDamage(1);
                Dispose();
            }

        }
    }

    private void CheckCollisionUfos()
    {
        var ufos = FieldCell.Get<ShipController>();
        foreach (var item in ufos)
        {
            var ufo = item as ShipController;
            if (ufo.Team == Team.Enemy)
            {
                if (IsCollide(ShipData.TransformInfo, ufo.ShipData.TransformInfo))
                {
                    PlayerDeath();

                    ufo.Dispose();
                    Dispose();
                }
            }
        }
    }

    private static bool IsCollide(TransformInfo first, TransformInfo second)
    {
        var distance = (first.Position - second.Position).sqrMagnitude;
        var radius = (first.Size + second.Size) * (first.Size + second.Size);

        return distance < radius;
    }

    private void PlayerDeath()
    {
        var scores = Locator.Get<PlayerScoresContainer>().Scores;
        Locator.Get<IGameOverWindow>().Show(scores);
    }




    public void SetMovementInput(float horizontal, float vertical)
    {
        playerInput.x = -horizontal;
        playerInput.y = vertical;
    }

    public void SetWeaponFire()
    {
        if (ShipData.MainWeapon != null)
            ShipData.MainWeapon.TryShoot();
    }

    public void SetAlternativeWeaponFire()
    {
        if (ShipData.SecondaryWeapon != null)
            ShipData.SecondaryWeapon.TryShoot();
    }



    private void Thrust(float value)
    {
        var tr = ShipData.TransformInfo;
        var st = ShipData.Stats;

        var xDir = Mathf.Sin(tr.CurrentRadians) * st.SpeedModifier * value;
        var yDir = Mathf.Cos(tr.CurrentRadians) * st.SpeedModifier * value;

        tr.Velocity = new Vector2(tr.Velocity.x - xDir, tr.Velocity.y + yDir);

    }

    private void Rotate(float value)
    {
        ShipData.TransformInfo.CurrentRadians += ShipData.Stats.RotationModifier * value;
    }

    private void CalcMovement(float dt)
    {
        var tr = ShipData.TransformInfo;
        tr.Position += tr.Velocity * dt;
    }



    private void MoveGameObject(TransformInfo trInfo)
    {
        if (!UnityTransform) return;
        SetPosition(trInfo);
        SetRotation(trInfo);
    }

    private void SetPosition(TransformInfo trInfo)
    {
        var pos = UnityTransform.position;
        pos.x = trInfo.Position.x;
        pos.y = trInfo.Position.y; 
        pos.z = 0;

        UnityTransform.position = pos;
    }

    private void SetRotation(TransformInfo trInfo)
    {
        var newAngle = Mathf.Rad2Deg * trInfo.CurrentRadians;
        var rot = new Vector3(0, 0, newAngle);

        UnityTransform.eulerAngles = rot;
    }

    public override void Dispose()
    {
        if (ShipData.MainWeapon != null)
            ShipData.MainWeapon.Dispose();
        if (ShipData.SecondaryWeapon != null)
            ShipData.SecondaryWeapon.Dispose();
        base.Dispose();
    }


}
