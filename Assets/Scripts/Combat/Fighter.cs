using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Mover mover;
        NavMeshAgent navMeshAgent;
        GameObject target;

        private void Start() {
            mover = GetComponent<Mover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update() {
            if (!target) return;
            bool inAttackRange = InAttackRange(target);

            if (!inAttackRange)
            {
                mover.MoveTo(target.transform.position);
            }
            else if (inAttackRange)
            {
                mover.Stop();
            }
            else
            {
                navMeshAgent.isStopped = false;
            }
        }

        public void Attack(GameObject combatTarget)
        {
            target = combatTarget;
        }

        public void CancelAttack()
        {
            target = null;
        }
        
        // compare distance^2 with weaponRange^2
        private bool InAttackRange(GameObject target)
        {
            float sqrDistance = GetSqrDistance(transform.position, target.transform.position);
            if (sqrDistance <= weaponRange * weaponRange) return true;
            return false;
        }

        // using .sqrMagnitude to measure distance is faster than .magnitude - it avoids square root operation
        private float GetSqrDistance(Vector3 origin, Vector3 target)
        {
            return (origin - target).sqrMagnitude;
        }
    }
}