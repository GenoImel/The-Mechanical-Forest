using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Party
{
    [Serializable]
    internal sealed class PartyMember
    {
        [JsonProperty("partyMemberName")] 
        public string PartyMemberName { private set; get; }

		[JsonProperty("skills")]
		public List<string> SkillIds { private set; get; }

		[JsonProperty("accessory")]
		public string Accessory { private set; get; }

		[JsonProperty("relic")]
		public string Relic { private set; get; }

		[JsonProperty("level")]
        public int Level { private set; get; }
        
        [JsonProperty("currentExperience")]
        public int CurrentExperience { private set; get; }

        [JsonProperty("maxExperience")]
        public int MaxExperience { private set; get; }
        
        [JsonProperty("currentHealth")]
        public int CurrentHealth { private set; get; }
        
        [JsonProperty("maxHealth")]
        public int MaxHealth { private set; get; }
        
        [JsonProperty("basePhysicalAttack")]
        public int BasePhysicalAttack { private set; get; }
        
        [JsonProperty("baseMagicalAttack")]
        public int BaseMagicalAttack { private set; get; }
        
        [JsonProperty("baseAccuracy")]
        public float BaseAccuracy { private set; get; }
        
        [JsonProperty("basePhysicalDefense")]
        public int BasePhysicalDefense { private set; get; }
        
        [JsonProperty("baseMagicalDefense")]
        public int BaseMagicalDefense { private set; get; }
        
        [JsonProperty("baseEvade")]
        public float BaseEvade { private set; get; }

		[JsonConstructor]
        public PartyMember(
			[JsonProperty("partyMemberName")] string partyMemberName,
			[JsonProperty("skills")] List<string> skills,
			[JsonProperty("accessory")] string accessory,
			[JsonProperty("Relic")] string relic,
			[JsonProperty("level")] int level,
            [JsonProperty("currentExperience")] int currentExperience,
            [JsonProperty("maxExperience")] int maxExperience,
            [JsonProperty("currentHealth")] int currentHealth,
            [JsonProperty("maxHealth")] int maxHealth,
            [JsonProperty("basePhysicalAttack")] int basePhysicalAttack,
            [JsonProperty("baseMagicalAttack")] int baseMagicalAttack,
            [JsonProperty("baseAccuracy")] float baseAccuracy,
            [JsonProperty("basePhysicalDefense")] int basePhysicalDefense,
            [JsonProperty("baseMagicalDefense")] int baseMagicalDefense,
            [JsonProperty("baseEvade")] float baseEvade
        )
		{
			PartyMemberName = partyMemberName;
			SkillIds = skills;
			Accessory = accessory;
			Relic = relic;
			Level = level;
            CurrentExperience = currentExperience;
            MaxExperience = maxExperience;
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
            BasePhysicalAttack = basePhysicalAttack;
            BaseMagicalAttack = baseMagicalAttack;
            BaseAccuracy = baseAccuracy;
            BasePhysicalDefense = basePhysicalDefense;
            BaseMagicalDefense = baseMagicalDefense;
            BaseEvade = baseEvade;
        }
    }
}