﻿using UnityEngine;

namespace RPG.Stats
{
	[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
	public class Progression : ScriptableObject
	{
		[SerializeField] 
		ProgressionClass[] characterClasses = null;

		public float GetStat(Stat stat, CharacterClass characterClass, int level)
		{
			foreach (ProgressionClass progressionClass in characterClasses)
			{
				foreach (ProgressionStat progressionStat in progressionClass.stats)
				{
					if (progressionStat.stat != stat) continue;

					if (progressionStat.levels.Length < level) continue;
					
					return progressionStat.levels[level - 1];
				}
			}
			
			return 0;
		}

		[System.Serializable]
		class ProgressionClass
		{
			public CharacterClass characterClass;
			public ProgressionStat[] stats;
		}

		[System.Serializable]
		class ProgressionStat
		{
			public Stat stat;
			public float[] levels;
		}
	}
}