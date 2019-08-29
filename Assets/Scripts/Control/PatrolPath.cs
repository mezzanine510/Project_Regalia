using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.2f;
        private void OnDrawGizmos() {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int waypointIndex)
        {
            return (waypointIndex + 1) % transform.childCount;
        }
    }
}
