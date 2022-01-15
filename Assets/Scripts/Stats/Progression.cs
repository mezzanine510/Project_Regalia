using UnityEngine;
using System.Collections.Generic;

namespace RPG.Stats
{
	[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
	public class Progression : ScriptableObject
	{
		[SerializeField] 
		ProgressionClass[] characterClasses = null;

		Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

		public float GetStat(Stat stat, CharacterClass characterClass, int level)
		{
			BuildLookup();
			float[] statLevels = lookupTable[characterClass][stat];

			if (statLevels.Length < level)
			{
				Debug.Log("statLevels.Length < level");
				return 0;
			}
			
			// Debug.Log("statLevels[level - 1]: " + statLevels[level - 1]);
			return statLevels[level - 1];
		}

		public int GetLevels(Stat stat, CharacterClass characterClass)
		{
			BuildLookup();
			float[] statLevels = lookupTable[characterClass][stat];
			return statLevels.Length;
		}

		private void BuildLookup()
		{
			if (lookupTable != null) return;

			lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

			foreach (ProgressionClass progressionClass in characterClasses)
			{
				var statLookupTable = new Dictionary<Stat, float[]>();

				foreach (ProgressionStat progressionStat in progressionClass.stats)
				{
					statLookupTable[progressionStat.stat] = progressionStat.levels;
				}
				
				lookupTable[progressionClass.characterClass] = statLookupTable;
			}
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