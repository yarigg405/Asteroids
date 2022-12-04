using System;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControl : IUpdate
{
    IPlayerControlled player;

    public PlayerControl(IPlayerControlled playerControlled)
    {
        BaseController.AllUpdates.Add(this);
        player = playerControlled;
    }

    public void OnUpdate(float deltaTime)
    {
        var hor = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        player.SetMovementInput(hor, vert);
    }
}

