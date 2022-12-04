using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Vector3 currentInertia;

    [Space]
    [SerializeField] float speedModificator;
    [SerializeField] Vector3 rotationModificator;



    private void Update()
    {
        var dt = Time.deltaTime;
        var hor = -Input.GetAxis("Horizontal");
        var vert = Mathf.Abs(-Input.GetAxis("Vertical"));


        currentInertia = Vector3.MoveTowards(currentInertia, transform.up, vert * dt * speedModificator);   


        var pos = transform.position;
        pos += currentInertia * dt;
        transform.position = pos;
        transform.Rotate(hor * rotationModificator * dt);
    }
}
