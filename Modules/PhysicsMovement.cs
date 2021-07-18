using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : InteractableMovement
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

        if (target.InHand)
        {
            Move();
        }
    }

    public override void Move()
    {
        base.Move();

        rb.MovePosition(Vector3.MoveTowards(rb.position, target.interactor.transform.position, movementSpeed * Time.fixedDeltaTime));
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, target.interactor.transform.rotation, rotationSpeed * Time.fixedDeltaTime));
    }
}
