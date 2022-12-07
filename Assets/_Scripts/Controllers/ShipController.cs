﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipController : BaseController, IPlayerControlled
{
    private ShipData shipData;
    private Vector2 playerInput;

    public ShipController(ShipFactoryBase factory) : base(factory.GetLinksMaster())
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        playerInput = Vector2.zero;
        shipData = factory.CreateShipData()
            .SetMainWeapon(factory.CreateMainWeapon())
            .SetSecondaryWeapon(factory.CreateSecondaryWeapon());
        unityTransform = factory.CreateUnityTransform();
    }


    public override void OnUpdate(float deltaTime)
    {
        var hor = playerInput.x * deltaTime;
        var vert = playerInput.y > 0 ? playerInput.y * deltaTime : 0;

        Thrust(vert);
        Rotate(hor);
        CalcMovement(deltaTime);

        MoveGameObject(shipData.transformInfo);
        playerInput.x = 0;
        playerInput.y = 0;
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


}