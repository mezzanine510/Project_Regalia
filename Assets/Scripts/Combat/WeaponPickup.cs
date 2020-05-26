using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5;

        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
                // Destroy(gameObject);
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool showShow)
        {
            GetComponent<SphereCollider>().enabled = showShow;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = showShow;
        }
    }
}
