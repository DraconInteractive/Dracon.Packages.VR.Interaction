using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbArea : BaseClimbItem
{
    public override float Evaluate(Vector3 _in)
    {
        if (col.bounds.Contains(_in))
        {
            return 0;
        }
        else
        {
            return Mathf.Infinity;
        }
    }
}
