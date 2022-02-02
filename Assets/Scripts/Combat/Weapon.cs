using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]

    public class Weapon : ScriptableObject
    {  
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedWeaponPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyCurrentWeapon(rightHand, leftHand);
            
            if (equippedWeaponPrefab != null)
            {
                GameObject weapon = Instantiate(equippedWeaponPrefab, GetTransform(rightHand, leftHand));
                weapon.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            
        }

        private void DestroyCurrentWeapon(Transform rightHand, Transform leftHand)
        {
            Transform currentWeapon = rightHand.Find(weaponName);

            if (!rightHand.Find(weaponName))
            {
                currentWeapon = leftHand.Find(weaponName);
            }

            if (!currentWeapon) return;

            currentWeapon.name = "DESTROYING"; // avoids confusing which weapon is being destroyed
            Destroy(currentWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;

            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;

            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }
    }
}