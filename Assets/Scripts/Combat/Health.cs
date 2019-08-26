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

            if (healthPoints == 0)
            {
                Die();
                // Vector3 explosionPos = direction;
                // Rigidbody rigidbody = GetComponent<Rigidbody>();
                // rigidbody.AddExplosionForce(2000f, explosionPos, 500f, 0.25f);
                // print("Destroyed " + gameObject);
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
        }

        private void OnDestroy()
        {

        }
    }
}