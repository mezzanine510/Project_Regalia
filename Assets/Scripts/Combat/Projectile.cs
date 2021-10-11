using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float maxLifetime = 4;
        [SerializeField] float lifeAfterImpact = 2;
        [SerializeField] bool isHomingProjectile = false;
        [SerializeField] float speed = 10f;

        Health target = null;
        float damage = 0;

        private void Awake()
        {
            Destroy(gameObject, maxLifetime); // Destroy after 4 seconds
        }

        private void Start()
        {
            transform.LookAt(GetAimLocation());    
        }

        void Update()
        {
            if (!target) return;
            if (isHomingProjectile && !target.IsDead()) transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsuleCollider = target.GetComponent<CapsuleCollider>();
            if (!targetCapsuleCollider) return target.transform.position;
            return target.transform.position + Vector3.up * targetCapsuleCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;

            if (hitEffect) Instantiate(hitEffect, transform.position, transform.rotation);
            speed = 1.5f;
            target.TakeDamage(damage, gameObject.transform.position);

            foreach(GameObject partOfObject in destroyOnHit)
            {
                Destroy(partOfObject);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}