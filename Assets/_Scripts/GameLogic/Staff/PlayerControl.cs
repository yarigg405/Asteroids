using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : IUpdate
{
    private readonly IPlayerControlled player;

    private Vector2 move;
    private bool shoot;
    private bool alternativeShoot;



    public PlayerControl(IPlayerControlled playerControlled)
    {
        player = playerControlled;
        var plInput = Object.FindObjectOfType<PlayerInput>();

        var controls = plInput.actions;
        var actionMap = controls.FindActionMap("Player");

        var moving = actionMap.FindAction("Move");
        moving.started += MoveAction;
        moving.performed += MoveAction;
        moving.canceled += MoveAction;

        var shooting = actionMap.FindAction("Fire");
        shooting.started += FireAction;
        shooting.performed += FireAction;
        shooting.canceled += FireAction;

        var altShooting = actionMap.FindAction("AlternativeFire");
        altShooting.started += AlternativeFireAction;
        altShooting.performed += AlternativeFireAction;
        altShooting.canceled += AlternativeFireAction;
    }

    public void OnUpdate(float deltaTime)
    {
        player.SetMovementInput(move.x, move.y);

        if (shoot)
            player.SetWeaponFire();

        if (alternativeShoot)
            player.SetAlternativeWeaponFire();
    }

    private void MoveAction(InputAction.CallbackContext obj)
    {
        move = obj.ReadValue<Vector2>();
    }

    private void FireAction(InputAction.CallbackContext obj)
    {
        shoot = obj.ReadValueAsButton();
    }

    private void AlternativeFireAction(InputAction.CallbackContext obj)
    {
        alternativeShoot = obj.ReadValueAsButton();
    }
}

