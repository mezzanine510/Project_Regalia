using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        Health target = null;
        float damage = 0;

        void Update()
        {
            if (!target) return;

            transform.LookAt(GetAimLocation());
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
