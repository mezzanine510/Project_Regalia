using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{

    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;

        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }
        
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                GameObject target = hit.collider.gameObject;
                if (!GetComponent<Fighter>().CanAttack(target.GetComponent<CombatTarget>())) continue;

                if (Input.GetMouseButton(0))
                {
                    // GameObject target = hit.collider.gameObject;
                    fighter.SetAttackTarget(target);
                    return true;
                }
                else
                {
                    continue;
                }
            }

            return false;
        }

        public bool InteractWithMovement()
        {
            if (!Input.GetMouseButton(0)) return false;

            RaycastHit hit;
            bool rayHitSomething = Physics.Raycast(GetMouseRay(), out hit);

            if (rayHitSomething)
            {
                mover.MoveTo(hit.point);
                return true;
            }
            
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}



// NOTE: ALTERNATE METHOD - check for possible bugs with old and new method

// private bool InteractWithCombat()
//         {
//             RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            
//             foreach (RaycastHit hit in hits)
//             {
//                 if (Input.GetMouseButton(0) && hit.collider.gameObject.GetComponent<CombatTarget>())
//                 {
//                     GameObject target = hit.collider.gameObject;
//                     fighter.Attack(target);
//                     return true;
//                 }
//             }

//             return false;
//         }