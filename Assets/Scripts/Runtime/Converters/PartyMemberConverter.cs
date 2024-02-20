using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Actors.Battle;
using Akashic.Runtime.Serializers.Party;
using Akashic.ScriptableObjects.PartyMember;

namespace Akashic.Runtime.Converters
{
    internal static class PartyMemberConverter
	{
		public static PartyMember ConvertControllerToPartyMember(PartyBattleActor partyBattleActor)
		{
			// TODO: Add skills, accessory, relic... these need to be pulled from the ResourceMonoSystem.
			var partyMember = new PartyMember(
				partyBattleActor.PartyMemberName,
				new List<string>(),
				null,
				null,
				partyBattleActor.partyMemberStatHandler.CurrentLevel,
				partyBattleActor.partyMemberResourceHandler.CurrentExperience,
				partyBattleActor.partyMemberResourceHandler.MaxExperience,
				partyBattleActor.partyMemberResourceHandler.CurrentHealth,
				partyBattleActor.partyMemberResourceHandler.MaxHealth,
				partyBattleActor.partyMemberStatHandler.BaseAttackStats.PhysicalAttack,
				partyBattleActor.partyMemberStatHandler.BaseAttackStats.MagicalAttack,
				partyBattleActor.partyMemberStatHandler.BaseAttackStats.Accuracy,
				partyBattleActor.partyMemberStatHandler.BaseDefenseStats.PhysicalDefense,
				partyBattleActor.partyMemberStatHandler.BaseDefenseStats.MagicalDefense,
				partyBattleActor.partyMemberStatHandler.BaseDefenseStats.Evade
				);

			return partyMember;
		}

		public static List<PartyMember> ConvertPartyMemberDataListToParyMemberList(List<PartyMemberData> partyMembersData)
		{
			var partyMembers = new List<PartyMember>();
			foreach (var partyMemberData in partyMembersData)
			{
				partyMembers.Add(ConvertPartyMemberDataToPartyMember(partyMemberData));
			}
			return partyMembers;
		}

		public static PartyMember ConvertPartyMemberDataToPartyMember(PartyMemberData partyMemberData)
		{
			return new PartyMember(
				partyMemberData.partyMemberName,
				partyMemberData.skills.Select(skill => skill.skillId).ToList(),
				partyMemberData.Accessory.itemId,
				partyMemberData.relic.itemId,
				partyMemberData.baseLevel,
				partyMemberData.baseExp,
				partyMemberData.baseExp,
				partyMemberData.baseHealth,
				partyMemberData.baseHealth,
				partyMemberData.basePhysicalAttack,
				partyMemberData.baseMagicalAttack,
				partyMemberData.baseAccuracy,
				partyMemberData.baseMagicalAttack,
				partyMemberData.baseMagicalDefense,
				partyMemberData.baseEvade
				);
		}
	}
}