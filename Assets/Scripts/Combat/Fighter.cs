// using UnityEngine;
// using RPG.Movement;

// namespace RPG.Combat
// {
//     public class Fighter : MonoBehaviour
//     {
//         [SerializeField] float weaponRange = 2f;
//         Transform target;

//         private void Update() {
//             if (target !=null)   
//             {
//                 GetComponent<Mover>().MoveTo(target.position);
//             }
//         }

//         // public void Attack(GameObject target)
//         public void Attack(GameObject combatTarget)
//         {
//             target = combatTarget.transform;
//             Vector3 position = transform.position;
//             Vector3 targetPosition = target.transform.position;
//             float distance = (position - targetPosition).magnitude;

//             if (distance <= weaponRange)
//             {
//                 print("Take that you peasant! You.. you PEON!!!");
//                 print("You hit: " + target.name);
//             }
//         }
//     }
// }



// My version

using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        // public void Attack(GameObject target)
        public void Attack(GameObject target)
        {
            Vector3 position = transform.position;
            Vector3 targetPosition = target.transform.position;
            float distance = (position - targetPosition).magnitude;

            if (distance <= weaponRange)
            {
                print("Take that you peasant! You.. you PEON!!!");
                print("You hit: " + target.name);
            }
        }
    }
}