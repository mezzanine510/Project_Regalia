using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float speed = 10f;
        
        void Awake() {
            // target = gameObject.GetComponent<Fighter>().target;
        }

        // Start is called before the first frame update
        void Start()
        {
            // transform.LookAt(target.transform);
        }

        // Update is called once per frame
        void Update()
        {
            if (!target) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsuleCollider = target.GetComponent<CapsuleCollider>();
            if (!targetCapsuleCollider) return target.position;
            return target.position + Vector3.up * targetCapsuleCollider.height / 2;
        }
    }

}
