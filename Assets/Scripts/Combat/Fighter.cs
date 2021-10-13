using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] string defaultWeaponName = "Sword";
        Weapon currentWeapon = null;
        float timeSinceLastAttack;

        Animator animator;
        Mover mover;
        ActionScheduler actionScheduler;
        NavMeshAgent navMeshAgent;
        public GameObject target;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            timeSinceLastAttack = Mathf.Infinity;

            if (currentWeapon == null) EquipWeapon(defaultWeapon);
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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public GameObject GetTarget()
        {
            return target;
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

            if (targetHealthComponent == null || targetHealthComponent.IsDead()) return false;
            else return true;
        }

        // Animation event
        private void Hit()
        {
            if (target == null) return;

            if (currentWeapon.HasProjectile())currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target.GetComponent<Health>(), gameObject);
            else DealDamage(currentWeapon.GetWeaponDamage(), target);
        }

        private void Shoot()
        {
            Hit();
        }

        private void DealDamage(float damage, GameObject target)
        {
            Vector3 direction = transform.position;
            target.GetComponent<Health>().TakeDamage(damage, gameObject, direction);
            print("Health: " + target.GetComponent<Health>().healthPoints);
        }

        private bool TargetInAttackRange(GameObject target)
        {
            if (DistanceToTargetSquared(target) < WeaponRangeSquared()) return true;
            else return false;
        }

        private float DistanceToTargetSquared(GameObject target)
        {
            return (transform.position - target.transform.position).sqrMagnitude;
        }

        private float WeaponRangeSquared()
        {
            float weaponRange = currentWeapon.GetWeaponRange();
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

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}