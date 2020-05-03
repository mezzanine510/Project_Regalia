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
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;
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
            timeSinceLastAttack = Mathf.Infinity;
            SpawnWeapon();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if (!target) return;

            if (!TargetInAttackRange(target))
            {
                mover.StartMoveAction(target.transform.position);
            }
            else
            {
                AttackBehaviour();
            }
        }

        public void Attack(GameObject gameObject)
        {
            target = gameObject;
        }

        private void SpawnWeapon()
        {
            Instantiate(weaponPrefab, handTransform);
        }
        
        private void AttackBehaviour()
        {
            if (target.GetComponent<Health>().IsDead())
            {   
                return;
            }

            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                actionScheduler.StartAction(this);
                RestartAttackTimer();
                TriggerAttack();
            }
        }

        private void RestartAttackTimer()
        {
            timeSinceLastAttack = 0;
        }

        private void TriggerAttack() // triggers Hit() timed by animation
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public bool CanAttack(GameObject target)
        {
            Health targetHealthComponent = target.GetComponent<Health>();

            if (    targetHealthComponent == null
                 || targetHealthComponent.IsDead()) return false;
            else return true;
        }

        // Animation Event
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
            if (DistanceToTargetSquared(target) < WeaponRangeSquared()) return true;
            return false;
        }

        private float DistanceToTargetSquared(GameObject target)
        {
            return (transform.position - target.transform.position).sqrMagnitude;
        }

        private float WeaponRangeSquared()
        {
            return weaponRange * weaponRange;
        }

        private void ResetAttackAnimationTriggers()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public void Cancel()
        {
            ResetAttackAnimationTriggers();
            DropTarget();
            mover.Cancel();
        }

        private void DropTarget()
        {
            target = null;
        }
    }
}