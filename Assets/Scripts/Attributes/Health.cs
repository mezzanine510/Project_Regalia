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
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            BaseStats baseStats = GetComponent<BaseStats>();
            if (baseStats != null)
            {
                baseStats.onLevelUp += HealOnLevelUp;
            }
        }

        // 'Vector3 direction' can be used for death animation movement direction
        public void TakeDamage(float damage, GameObject instigator, Vector3 direction)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints <= 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();

            if (experience == null) return;
            
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100f;
        }

        private void HealOnLevelUp()
        {
            float baseHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            float percentageOfHealth = baseHealth * 0.75f;
            // Debug.Log("baseHealth: " + baseHealth);
            // Debug.Log("percentageOfHealth: " + percentageOfHealth);
            if (healthPoints > percentageOfHealth) return;
            healthPoints = Mathf.Max(healthPoints, percentageOfHealth);
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

        // SAVE SYSTEM
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