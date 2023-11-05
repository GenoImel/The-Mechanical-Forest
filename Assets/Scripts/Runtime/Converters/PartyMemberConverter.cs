using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Controllers.PartyMemberBattle;
using Akashic.Runtime.Serializers.Party;
using Akashic.ScriptableObjects.PartyMember;

namespace Akashic.Runtime.Converters
{
    internal static class PartyMemberConverter
	{
		public static PartyMember ConvertControllerToPartyMember(PartyMemberController partyMemberController)
		{
			// TODO: Add skills, accessory, relic... these need to be pulled from the ResourceMonoSystem.
			var partyMember = new PartyMember(
				partyMemberController.PartyMemberName,
				new List<string>(),
				null,
				null,
				partyMemberController.partyMemberStatHandler.CurrentLevel,
				partyMemberController.partyMemberResourceHandler.CurrentExperience,
				partyMemberController.partyMemberResourceHandler.MaxExperience,
				partyMemberController.partyMemberResourceHandler.CurrentHealth,
				partyMemberController.partyMemberResourceHandler.MaxHealth,
				partyMemberController.partyMemberStatHandler.BaseAttackStats.PhysicalAttack,
				partyMemberController.partyMemberStatHandler.BaseAttackStats.MagicalAttack,
				partyMemberController.partyMemberStatHandler.BaseAttackStats.Accuracy,
				partyMemberController.partyMemberStatHandler.BaseDefenseStats.PhysicalDefense,
				partyMemberController.partyMemberStatHandler.BaseDefenseStats.MagicalDefense,
				partyMemberController.partyMemberStatHandler.BaseDefenseStats.Evade
				);

			return partyMember;
		}

		public static List<PartyMember> ConvertPartyMemberDataListToParyMemberList(List<PartyMemberData> partyMembersData)
		{
			List<PartyMember> partyMembers = new List<PartyMember>();
			foreach (PartyMemberData partyMemberData in partyMembersData)
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