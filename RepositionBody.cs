using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionBody : MonoBehaviour
{

    [SerializeField] private Transform anchor;
    [SerializeField] private float down;
    [SerializeField] private float back;
    [SerializeField] private bool rotate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = anchor.position + (Vector3.down * down) + (Vector3.back * back);
        if (rotate)
        {
            Vector3 anchorDir = anchor.forward;
            anchorDir.y = 0;
            anchorDir.Normalize();

            Vector3 rootDir = transform.forward;
            rootDir.y = 0;
            rootDir.Normalize();

            Quaternion headAngle = Quaternion.LookRotation(anchorDir, Vector3.up);
            Quaternion rootAngle = Quaternion.LookRotation(rootDir, Vector3.up);
            if (Quaternion.Angle(headAngle, rootAngle) > 15)
            {
                transform.rotation = Quaternion.RotateTowards(rootAngle, headAngle, 90 * Time.deltaTime);
            }
        }
    }
}
