using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Movement;
using System;

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

        private void Awake() {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            timeSinceLastAttack = timeBetweenAttacks + 1; // avoid bug on scene load: character doesn't attack right away 
        }

        private void Update() {
            if (!target) return;

            timeSinceLastAttack += Time.deltaTime;
            bool inAttackRange = TargetInAttackRange(target);

            if (!inAttackRange)
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
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                actionScheduler.StartAction(this);
                timeSinceLastAttack = 0;
                animator.SetTrigger("attack"); // triggers Hit()
            }
        }

        // Animation Event: occurs when attack makes contact
        private void Hit()
        {
            DealDamage(weaponDamage, target);
        }

        private void DealDamage(float damage, GameObject combatTarget)
        {
            Vector3 direction = transform.position;
            combatTarget.GetComponent<Health>().TakeDamage(damage, direction);
            print("Health: " + combatTarget.GetComponent<Health>().health);
        }

        public void SetAttackTarget(GameObject combatTarget)
        {
            target = combatTarget;
        }

        public void Cancel()
        {
            target = null;
            print("Cancelling action: " + this);
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
    }
}