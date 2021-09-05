using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMod_Physics : InteractableMovement
{
    public float movementSpeed, rotationSpeed;

    protected Rigidbody rb;

    public override void Setup(Interactable _target)
    {
        base.Setup(_target);

        rb = GetComponent<Rigidbody>();
    }

    public override void FixedUpdateEx()
    {
        base.FixedUpdateEx();

        if (InHand)
        {
            Move();
        }
    }

    public override void Move()
    {
        base.Move();

        var t = TransformResult();

        rb.MovePosition(Vector3.MoveTowards(rb.position, t.Item1, movementSpeed * Time.fixedDeltaTime));
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, t.Item2, rotationSpeed * Time.fixedDeltaTime));
    }
}
