using UnityEngine;
using RPG.Combat;
using RPG.Movement;

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
            if (health.IsDead())
            {
                mover.StopMoving();
                return;
            }
            
            if (player == null) return;

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

        private float DistanceToTargetSquared(GameObject target)
        {
            return (transform.position - target.transform.position).sqrMagnitude;
        }

        private float ChaseDistanceSquared()
        {
            return chaseDistance * chaseDistance;
        }
    }

}