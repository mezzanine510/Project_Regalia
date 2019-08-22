using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private Animator animator;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update() 
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("ForwardSpeed", speed);
        }
    }
}
