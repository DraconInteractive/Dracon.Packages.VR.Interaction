using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbPoint : BaseClimbItem
{
    public override float Evaluate (Vector3 _in)
    {
        return Vector3.Distance(transform.position, _in);
    }
}
