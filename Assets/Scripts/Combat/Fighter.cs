using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;

        Mover mover;
        ActionScheduler actionScheduler;
        NavMeshAgent navMeshAgent;
        GameObject target;
        GameObject targetHolder;

        private void Awake() {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update() {
            if (!target) return;

            targetHolder = target;
            bool inAttackRange = TargetInAttackRange(target);

            if (!inAttackRange)
            {
                mover.MoveTo(target.transform.position);
                target = targetHolder;
            }
            else
            {
                // mover.StopMoving();
                actionScheduler.StartAction(this);
            }
        }

        public void Attack(GameObject combatTarget)
        {
            // actionScheduler.StartAction(this);
            target = combatTarget;
        }

        public void Cancel()
        {
            target = null;
            print("Cancelling action: " + this);
        }
        
        // compare distance^2 with weaponRange^2
        private bool TargetInAttackRange(GameObject target)
        {
            float sqrDistance = GetSqrDistance(transform.position, target.transform.position);
            if (sqrDistance < weaponRange * weaponRange) return true;
            return false;
        }

        // using .sqrMagnitude to measure distance is faster than .magnitude - it avoids square root operation
        private float GetSqrDistance(Vector3 origin, Vector3 target)
        {
            return (origin - target).sqrMagnitude;
        }
    }
}