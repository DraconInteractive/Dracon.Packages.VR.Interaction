using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMod_Direct : InteractableMovement
{
    public float movementSpeed, rotationSpeed;

    public override void UpdateEx()
    {
        base.UpdateEx();

        if (InHand)
        {
            Move();
        }
    }

    public override void Move()
    {
        base.Move();

        var t = TransformResult();

        transform.position = Vector3.MoveTowards(transform.position, t.Item1, movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, t.Item2, rotationSpeed * Time.deltaTime);
    }
}
