using System;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControl : IUpdate
{
    IPlayerControlled player;

    public PlayerControl(IPlayerControlled playerControlled)
    {
        BaseController.AddToUpdates.Add(this);
        player = playerControlled;
    }

    public void OnUpdate(float deltaTime)
    {
        var hor = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        player.SetMovementInput(hor, vert);

        if (Input.GetAxis("Jump") > 0) //default - space key
            player.SetWeaponFire();              
        if (Input.GetAxis("Fire3") > 0) //default - left shift key
            player.SetAlternativeWeaponFire();  
    }
}

