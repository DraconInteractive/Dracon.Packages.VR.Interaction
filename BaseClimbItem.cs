using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClimbItem : MonoBehaviour
{
    public static List<BaseClimbItem> All = new List<BaseClimbItem>();

    public static BaseClimbItem Closest(Vector3 pos, float maxDist = 1000)
    {
        BaseClimbItem target = null;

        foreach (var point in All)
        {
            float dist = point.Evaluate(pos);
            if (dist < maxDist)
            {
                maxDist = dist;
                target = point;
            }
        }

        return target;
    }

    public Collider col;

    private void OnEnable()
    {
        All.Add(this);
    }

    private void OnDisable()
    {
        All.Remove(this);
    }

    public virtual float Evaluate (Vector3 _in)
    {
        return Mathf.Infinity;
    }
}
