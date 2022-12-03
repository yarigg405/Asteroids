using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;


public class Ship : MonoBehaviour
{
    private float rotationModifier = 5f;
    private float thrustModifier = 3f;

    private Vector2 velocity = Vector2.zero;

    [Space]
    private float currentRadians;
    private Vector2 position;



    private void Update()
    {
        Update(Time.deltaTime);
    }


    private void Update(float deltaTime)
    {
        var hor = -Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        Thrust(vert * deltaTime);
        Rotate(hor * deltaTime);
        CalcMovement(deltaTime);
    }


    public void Thrust(float value)
    {
        var xDir = Mathf.Sin(currentRadians) * thrustModifier * value;
        var yDir = Mathf.Cos(currentRadians) * thrustModifier * value;

        velocity = new Vector2(velocity.x - xDir, velocity.y + yDir);

    }

    public void Rotate(float value)
    {
        currentRadians += rotationModifier * value;
    }

    private void CalcMovement(float dt)
    {
        position += velocity * dt;
        transform.position = new Vector3(position.x, position.y, 0);
        var newAngle = Mathf.Rad2Deg * currentRadians;
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }





}