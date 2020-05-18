using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] bool isHomingProjectile = false;
        Health target = null;
        float damage = 0;

        private void Start()
        {
            transform.LookAt(GetAimLocation());    
        }

        void Update()
        {
            if (!target) return;
            if (isHomingProjectile == true) transform.LookAt(GetAimLocation());
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
            target.TakeDamage(damage, gameObject.transform.position);
            Destroy(gameObject);

        }
    }

}
