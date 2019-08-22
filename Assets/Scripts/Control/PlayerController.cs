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
            print("Nothing to do.");
        }
        
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            
            foreach (RaycastHit hit in hits)
            {
                if (Input.GetMouseButton(0) && hit.collider.gameObject.GetComponent<CombatTarget>())
                {
                    GameObject target = hit.collider.gameObject;
                    fighter.Attack(target);
                    return true;
                }
            }

            return false;
        }

        public bool InteractWithMovement()
        {
            RaycastHit hit;
            bool rayHitSomething = Physics.Raycast(GetMouseRay(), out hit);

            if (rayHitSomething)
            {
                if (Input.GetMouseButton(0))
                {
                    fighter.CancelAttack();
                    mover.MoveTo(hit.point);
                }
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