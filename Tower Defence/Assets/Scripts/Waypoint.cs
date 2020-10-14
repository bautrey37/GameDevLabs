using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint Next;

    public Waypoint GetNextWaypoint()
    {
        return Next;
    }

    void OnDrawGizmos()
    {
        if (Next == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Next.transform.position);
    }
}
