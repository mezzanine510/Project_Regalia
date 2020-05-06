﻿using UnityEngine;

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

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedWeaponPrefab != null)
            {
                Transform handTransform;

                if (isRightHanded)
                {
                    handTransform = rightHand;
                }
                else{
                    handTransform = leftHand;
                }

                Instantiate(equippedWeaponPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
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