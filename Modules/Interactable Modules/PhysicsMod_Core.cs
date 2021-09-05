using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMod_Core : InteractableModule
{
    Rigidbody rb;

    public bool throwOnRelease;
    public float throwForce = 1;

    public override void Setup(Interactable _target)
    {
        base.Setup(_target);

        rb = GetComponent<Rigidbody>();
    }

    public override void OnGrab (GrabPoint point)
    {
        rb.isKinematic = true;
    }

    public override void OnRelease (GrabPoint point)
    {
        if (point.owner.ActiveInteractors == 0)
        {
            rb.isKinematic = false;

            if (throwOnRelease)
            {
                //rb.velocity *= 2.5f;
                //rb.angularVelocity *= 2.5f;
                rb.velocity = point.interactor.Velocity * throwForce;
            }
        }
    }
}
