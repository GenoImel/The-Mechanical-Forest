using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    [Serializable]
    internal sealed class PartyMember
    {
        [JsonProperty("partyMemberName")] 
        public string PartyMemberName { private set; get; }
        
		[JsonProperty("skills")]
		public List<string> SkillIds { private set; get; }

		[JsonProperty("equipment")]
		public PartyMemberEquipment PartyMemberEquipment { private set; get; }
		
		[JsonProperty("stats")]
		public PartyMemberStats PartyMemberStats { private set; get; }

		[JsonConstructor]
		public PartyMember
		(
			[JsonProperty("partyMemberName")] string partyMemberName,
			[JsonProperty("skills")] List<string> skillIds,
			[JsonProperty("equipment")] PartyMemberEquipment partyMemberEquipment,
			[JsonProperty("stats")] PartyMemberStats partyMemberStats
		)
		{
			PartyMemberName = partyMemberName;
			SkillIds = skillIds;
			PartyMemberEquipment = partyMemberEquipment;
			PartyMemberStats = partyMemberStats;
		}
    }
}