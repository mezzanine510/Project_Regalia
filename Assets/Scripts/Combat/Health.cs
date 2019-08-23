using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour {
        [SerializeField] public float health = 20f;

        public void TakeDamage(float damage, Vector3 direction)
        {
            health -= damage;

            if (health <= 0)
            {
                Vector3 explosionPos = direction;
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.AddExplosionForce(2000f, explosionPos, 500f, 0.25f);
                print("Destroyed " + gameObject);
            }
        }

        private void OnDestroy() 
        {

        }
    }
}