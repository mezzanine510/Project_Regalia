using System;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI healthDisplay;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthDisplay = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            healthDisplay.text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}