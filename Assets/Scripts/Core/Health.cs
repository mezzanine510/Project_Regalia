using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public float healthPoints = 20f;
        bool isDead = false;

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
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}