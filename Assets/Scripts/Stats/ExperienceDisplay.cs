using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        // float experiencePoints;
        TextMeshProUGUI experienceDisplay;

        private void Awake()
        {   
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            experienceDisplay = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            Debug.Log("experience: " + experience.GetPoints());
            experienceDisplay.text = experience.GetPoints().ToString();
        }
    }
}