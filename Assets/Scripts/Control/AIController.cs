using UnityEngine;
using System;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;

        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;

        Vector3 guardPosition;
        float timeSincePlayerSeen = Mathf.Infinity;
        int currentWaypointIndex = 0;
        // Transform[] waypoints;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
        }

        private void Update()
        {
            print(GetComponent<ActionScheduler>().currentAction);
            if (health.IsDead()) return;

            if (TargetInAggroRange(player) && fighter.CanAttack(player))
            {
                timeSincePlayerSeen = 0;
                AttackBehaviour();
                // print("Attacking"); // debug
            }
            else if (timeSincePlayerSeen < suspicionTime)
            {
                SuspicionBehaviour();
                // print("Suspicious"); // debug
            }
            else
            {
                PatrolBehaviour();
                // print("Patrolling"); // debug
            }

            timeSincePlayerSeen += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            fighter.Cancel(); // potential bug
            // GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        // compare squared distance for better performance
        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private bool TargetInAggroRange(GameObject target)
        {
            if (DistanceToTargetSquared(target) < ChaseDistanceSquared()) return true;
            return false;
            // else return false;
        }

        // TODO: refactor - duplicate of method in Fighter.cs
        private float DistanceToTargetSquared(GameObject target)
        {
            return (transform.position - target.transform.position).sqrMagnitude;
        }

        // TODO: refactor - duplicate of method in Fighter.cs
        private float ChaseDistanceSquared()
        {
            return chaseDistance * chaseDistance;
        }

        // Called by Unity
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}