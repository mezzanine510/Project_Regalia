using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public float healthPoints = 20f;
        bool isDead = false;
        ActionScheduler actionScheduler;

        private void Awake() {
            actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage, Vector3 direction)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            actionScheduler.CancelCurrentAction();
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}