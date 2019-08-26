using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        float timeSinceLastAttack;

        Animator animator;
        Mover mover;
        ActionScheduler actionScheduler;
        NavMeshAgent navMeshAgent;
        GameObject target;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            timeSinceLastAttack = timeBetweenAttacks + 1; // avoids bug on scene load where character doesn't attack right away 
        }

        private void Update()
        {
            if (!target) return;
            if (target.GetComponent<Health>().IsDead()) return;

            timeSinceLastAttack += Time.deltaTime;
            bool targetInAttackRange = TargetInAttackRange(target);

            if (!targetInAttackRange)
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                AttackBehaviour();
                mover.StopMoving();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                actionScheduler.StartAction(this);
                timeSinceLastAttack = 0;
                TriggerAttack(); // triggers Hit()
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public void SetAttackTarget(GameObject gameObject)
        {
            target = gameObject;
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            if (combatTarget.GetComponent<Health>().IsDead()) return false;
            return true;
        }

        // Animation Event: occurs when attack animation makes contact
        private void Hit()
        {
            if (target == null) return;
            DealDamage(weaponDamage, target);
        }

        private void DealDamage(float damage, GameObject combatTarget)
        {
            Vector3 direction = transform.position;
            combatTarget.GetComponent<Health>().TakeDamage(damage, direction);
            print("Health: " + combatTarget.GetComponent<Health>().healthPoints);
        }

        // Use .sqrMagnitude to measure distance - it avoids .magnitude square root operation. Then, compare distance^2 with weaponRange^2
        private bool TargetInAttackRange(GameObject target)
        {
            if (DistanceSquared(target) < WeaponRangeSquared()) return true;
            return false;
        }

        private float DistanceSquared(GameObject target)
        {
            
            return (transform.position - target.transform.position).sqrMagnitude;
        }

        private float WeaponRangeSquared()
        {
            return weaponRange * weaponRange;
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }
    }
}