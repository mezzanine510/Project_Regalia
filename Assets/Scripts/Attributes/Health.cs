using UnityEngine;
using RPG.Core;
using RPG.Stats;
using RPG.Saving;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] public float healthPoints = 20f;
        bool isDead = false;
        ActionScheduler actionScheduler;

        private void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(float damage, Vector3 direction)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints <= 0)
            {
                Die();
            }
        }

        public float GetPercentage()
        {
            return (healthPoints / GetComponent<BaseStats>().GetHealth()) * 100;
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

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}