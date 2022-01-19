using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField]
        CharacterClass characterClass;

        [SerializeField]
        Progression progression = null;

        [Range(1, 99)]
        [SerializeField]
        int startingLevel = 1;

        int currentLevel = 0;

        private void Awake()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = GetLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
            }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if (currentLevel < 1) // prevents currentLevel from being 0, the default value
            {
                currentLevel = CalculateLevel();
            }

            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null) 
            {
                Debug.Log("Experience is null!!");
                return startingLevel;
            }

            Debug.Log("experience component: ", experience);

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);

                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}