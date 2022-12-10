using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ShipController : BaseController, IPlayerControlled
{
    public ShipData shipData { get; private set; }
    private Vector2 playerInput;
    public override TransformInfo transformInfo => shipData.transformInfo;

    protected override PrefabType prefabType => PrefabType.PlayerShip;

    public Team team { get; protected set; }

    public ShipController(ShipFactoryBase factory) : base(factory.GetLinksMaster())
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        playerInput = Vector2.zero;
        shipData = factory.CreateShipData();
        unityTransform = factory.CreateUnityTransform();

        team = Team.Player;
    }


    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        var hor = playerInput.x * deltaTime;
        var vert = playerInput.y > 0 ? playerInput.y * deltaTime : 0;

        Thrust(vert);
        Rotate(hor);
        CalcMovement(deltaTime);

        MoveGameObject(shipData.transformInfo);
        playerInput.x = 0;
        playerInput.y = 0;
    }

    protected override void CheckCollisions()
    {
        CheckCollissionBolts();
        if (team == Team.Player)
        {
            CheckCollissionAsteroid();
            CheckCollisionUfos();
        }
    }

    private void CheckCollissionBolts()
    {
        var nearestBolts = fieldCell.Get<BoltController>();
        foreach (var item in nearestBolts)
        {
            var bolt = item as BoltController;
            if (bolt.team != team)
            {
                if (IsCollide(shipData.transformInfo, bolt.transformInfo))
                {
                    if (team == Team.Player)
                    {
                        PlayerDeath();
                    }

                    bolt.Dispose();
                    Dispose();
                }

            }
        }
    }

    private void CheckCollissionAsteroid()
    {
        var nearestAsteroids = fieldCell.Get<AsteroidController>();
        foreach (var item in nearestAsteroids)
        {
            var aster = item as AsteroidController;
            if (IsCollide(shipData.transformInfo, aster.transformInfo))
            {
                PlayerDeath();

                aster.GetDamage(1);
                Dispose();
            }

        }
    }

    private void CheckCollisionUfos()
    {
        var ufos = fieldCell.Get<ShipController>();
        foreach (var item in ufos)
        {
            var ufo = item as ShipController;
            if (ufo.team == Team.Enemy)
            {
                if (IsCollide(shipData.transformInfo, ufo.shipData.transformInfo))
                {
                    PlayerDeath();

                    ufo.Dispose();
                    Dispose();
                }
            }
        }
    }

    private bool IsCollide(TransformInfo first, TransformInfo second)
    {
        var distance = (first.position - second.position).sqrMagnitude;
        var radius = (first.size + second.size) * (first.size + second.size);

        return distance < radius;
    }

    private void PlayerDeath()
    {
        linksMaster.GameOverWindow.Show(linksMaster.PlayerScoresContainer.scores);
    }




    public void SetMovementInput(float horizontal, float vertical)
    {
        playerInput.x = -horizontal;
        playerInput.y = vertical;
    }

    public void SetWeaponFire()
    {
        if (shipData.mainWeapon != null)
            shipData.mainWeapon.TryShoot();
    }

    public void SetAlternativeWeaponFire()
    {
        if (shipData.secondaryWeapon != null)
            shipData.secondaryWeapon.TryShoot();
    }



    private void Thrust(float value)
    {
        var tr = shipData.transformInfo;
        var st = shipData.stats;

        var xDir = Mathf.Sin(tr.currentRadians) * st.speedModifier * value;
        var yDir = Mathf.Cos(tr.currentRadians) * st.speedModifier * value;

        tr.velocity = new Vector2(tr.velocity.x - xDir, tr.velocity.y + yDir);

    }

    private void Rotate(float value)
    {
        shipData.transformInfo.currentRadians += shipData.stats.rotationModifier * value;
    }

    private void CalcMovement(float dt)
    {
        var tr = shipData.transformInfo;
        tr.position += tr.velocity * dt;
    }



    private void MoveGameObject(TransformInfo transformInfo)
    {
        if (!unityTransform) return;
        SetPosition(transformInfo);
        SetRotation(transformInfo);
    }

    private void SetPosition(TransformInfo transformInfo)
    {
        var pos = unityTransform.position;
        pos.x = transformInfo.position.x;
        pos.y = transformInfo.position.y; ;
        pos.z = 0;

        unityTransform.position = pos;
    }

    private void SetRotation(TransformInfo transformInfo)
    {
        var newAngle = Mathf.Rad2Deg * transformInfo.currentRadians;
        var rot = new Vector3(0, 0, newAngle);

        unityTransform.eulerAngles = rot;
    }

    public override void Dispose()
    {
        if (shipData.mainWeapon != null)
            shipData.mainWeapon.Dispose();
        if (shipData.secondaryWeapon != null)
            shipData.secondaryWeapon.Dispose();
        base.Dispose();
    }


}
