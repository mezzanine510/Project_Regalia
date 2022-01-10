using System;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        TextMeshProUGUI levelDisplay;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            levelDisplay = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            levelDisplay.text = String.Format("{0:0}", baseStats.GetLevel());
            Debug.Log(baseStats.GetLevel());
        }
    }
}