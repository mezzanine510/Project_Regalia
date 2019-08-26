using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        // GameObject target;

        private void Update() {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null) return;

            if (TargetInRange(player))
            {
                print("Chasing " + player.name + "!!!");
            }
        }

        private bool TargetInRange(GameObject target)
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