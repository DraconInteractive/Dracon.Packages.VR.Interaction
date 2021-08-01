using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMod_Direct : InteractableMovement
{
    public float movementSpeed, rotationSpeed;

    public override void UpdateEx()
    {
        base.UpdateEx();

        if (target.InHand)
        {
            Move();
        }
    }

    public override void Move()
    {
        base.Move();

        transform.position = Vector3.MoveTowards(transform.position, target.interactor.transform.position, movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.interactor.transform.rotation, rotationSpeed * Time.deltaTime);
    }
}
