using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (health.IsDead()) return;
            
            // if (player == null) return;

            if (TargetInAggroRange(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool TargetInAggroRange(GameObject target)
        {
            if (DistanceToTargetSquared(target) < ChaseDistanceSquared()) return true;
            return false;
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
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}