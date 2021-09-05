using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMovement : InteractableModule
{

    public bool invertRotation;

    public virtual void Move ()
    {

    }

    public (Vector3, Quaternion) TransformResult ()
    {
        var activeInteractors = target.ActiveInteractors;
        if (activeInteractors == 0)
        {
            return (Vector3.zero, Quaternion.identity);
        }

        Vector3 posOffset = target.firstGrab.posOffset;
        Quaternion rotOffset = target.firstGrab.rotOffset;

        Interactor ftInt = target.firstGrab.interactor;

        if (activeInteractors == 1)
        {
            Vector3 targetPos = ftInt.transform.position + ftInt.transform.rotation * posOffset;
            Quaternion targetRot = ftInt.transform.rotation * rotOffset;

            return (targetPos, targetRot);
        }
        else if (activeInteractors == 2)
        {
            
            Vector3 dir = target.secondGrab.interactor.transform.position - target.firstGrab.interactor.transform.position;
            dir.Normalize();
            Quaternion baseRot = Quaternion.LookRotation(dir);
            Quaternion targetRot = baseRot * rotOffset;

            Vector3 targetPos = ftInt.transform.position + targetRot * posOffset;
            return (targetPos, targetRot);
        }
        

        return (Vector3.zero, Quaternion.identity);

    }
}
