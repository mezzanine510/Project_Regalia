using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private ActionScheduler actionScheduler;
        private Animator animator;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update() 
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void StopMoving()
        {
            navMeshAgent.isStopped = true;
        }

        public void Cancel()
        {
            StopMoving();
            print("Cancelling action: " + this);
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }
}