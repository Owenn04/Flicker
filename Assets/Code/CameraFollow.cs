using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;
    public float tLimit = 30;
    public float bLimit = -30;
    public float lLimit = -30;
    public float rLimit = 48; 

    void FixedUpdate()
    {
        Vector3 movePos = target.position + offset;
        Vector3 toMove = Vector3.SmoothDamp(transform.position, movePos, ref velocity, damping);
        //if (toMove.y < tLimit && toMove.y > bLimit && toMove.x > lLimit && toMove.x < rLimit) {
        transform.position = toMove;
        //}
    }
}
