using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableModule : MonoBehaviour
{
    protected Interactable target;

    public bool InHand => target.State != Interactable.GrabState.Idle;

    public virtual void Setup(Interactable _target)
    {
        target = _target;
    }

    public virtual void UpdateEx()
    {

    }

    public virtual void FixedUpdateEx ()
    {

    }

    public virtual void OnDestroyEx()
    {

    }

    public virtual void LateUpdateEx ()
    {

    }

    public virtual void OnGrab (GrabPoint point)
    {

    }

    public virtual void OnRelease (GrabPoint point)
    {

    }

    public virtual void OnInteract (GrabPoint point)
    {

    }
}
