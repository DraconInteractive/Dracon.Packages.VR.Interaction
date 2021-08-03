using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionMod : InteractorModule
{
    public Transform body, head;
    public float movementSpeed, rotationSpeed;

    public override void OnInputUpdateEx()
    {
        base.OnInputUpdateEx();

        Vector3 dir = head.forward;
        dir.y = 0;
        body.position += dir * movementSpeed * target.thumbstick.y * Time.deltaTime;
        body.RotateAround(head.position, Vector3.up, rotationSpeed * target.thumbstick.x * Time.deltaTime);
    }
}
