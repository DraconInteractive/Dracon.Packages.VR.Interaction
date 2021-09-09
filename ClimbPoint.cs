using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbPoint : MonoBehaviour
{
    public static List<ClimbPoint> All = new List<ClimbPoint>();

    public static ClimbPoint Closest (Vector3 pos, float maxDist = 1000)
    {
        ClimbPoint target = null;

        foreach (var point in All)
        {
            float dist = Vector3.Distance(point.transform.position, pos);
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

}
