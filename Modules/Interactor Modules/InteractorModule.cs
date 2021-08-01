using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorModule : MonoBehaviour
{
    protected Interactor target;

    public virtual void Setup (Interactor _target)
    {
        target = _target;
    }

    public virtual void OnUpdateEx ()
    {

    }

    public virtual void OnInputUpdateEx ()
    {

    }

    public virtual void OnGrabEx ()
    {

    }

    public virtual void OnReleaseEx ()
    {

    }
}
