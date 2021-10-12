using System;
using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        GameObject enemy;
        Health enemyHealth;
        TextMeshProUGUI healthDisplay;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthDisplay = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            enemy = fighter.GetTarget();

            if (enemy == null)
            {
                healthDisplay.text = "N/A";
                return;
            }

            enemyHealth = enemy.GetComponent<Health>();
            healthDisplay.text = String.Format("{0:0}%", enemyHealth.GetPercentage());
        }
    }
}