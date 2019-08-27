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

        public void SetAttackTarget(GameObject gameObject)
        {
            target = gameObject;
        }
        
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            
            // Cancelling the attack/target might need to be put elsewhere
            if (target.GetComponent<Health>().IsDead())
            {
                Cancel();
                return;
            }

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                actionScheduler.StartAction(this);
                timeSinceLastAttack = 0;
                TriggerAttack();
            }
        }

        private void TriggerAttack() // triggers Hit() timed by animation
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            Health targetToTest = combatTarget.GetComponent<Health>();
            if (targetToTest.IsDead() || combatTarget == null) return false;

            return true;
        }

        private void Hit()
        {
            if (target == null) return;
            DealDamage(weaponDamage, target);
        }

        private void DealDamage(float damage, GameObject target)
        {
            Vector3 direction = transform.position;
            target.GetComponent<Health>().TakeDamage(damage, direction);
            
            print("Health: " + target.GetComponent<Health>().healthPoints);
        }

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
            TriggerStopAttack();
            target = null;
        }

        private void TriggerStopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }
    }
}