using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Controllers.BattlePartyMember;
using Akashic.Runtime.Serializers.Party;
using Akashic.ScriptableObjects.PartyMember;

namespace Akashic.Runtime.Converters
{
    internal static class PartyMemberConverter
	{
		public static PartyMember ConvertControllerToPartyMember(BattlePartyMemberController battlePartyMemberController)
		{
			// TODO: Add skills, accessory, relic... these need to be pulled from the ResourceMonoSystem.
			var partyMember = new PartyMember(
				battlePartyMemberController.PartyMemberName,
				new List<string>(),
				null,
				null,
				battlePartyMemberController.partyMemberStatHandler.CurrentLevel,
				battlePartyMemberController.partyMemberResourceHandler.CurrentExperience,
				battlePartyMemberController.partyMemberResourceHandler.MaxExperience,
				battlePartyMemberController.partyMemberResourceHandler.CurrentHealth,
				battlePartyMemberController.partyMemberResourceHandler.MaxHealth,
				battlePartyMemberController.partyMemberStatHandler.BaseAttackStats.PhysicalAttack,
				battlePartyMemberController.partyMemberStatHandler.BaseAttackStats.MagicalAttack,
				battlePartyMemberController.partyMemberStatHandler.BaseAttackStats.Accuracy,
				battlePartyMemberController.partyMemberStatHandler.BaseDefenseStats.PhysicalDefense,
				battlePartyMemberController.partyMemberStatHandler.BaseDefenseStats.MagicalDefense,
				battlePartyMemberController.partyMemberStatHandler.BaseDefenseStats.Evade
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