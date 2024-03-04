using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Serializers.Save;
using Akashic.Runtime.Utilities.GameMath.Resources;
using Akashic.Runtime.Utilities.GameMath.Stats;
using Akashic.ScriptableObjects.PartyMember;

namespace Akashic.Runtime.Converters
{
    internal static class PartyMemberConverter
	{
		/*public static PartyMember ConvertBattleActorToPartyMember(PartyBattleActor partyBattleActor)
		{
			var partyMemberStats = new PartyMemberStats
			(
				
				)
			
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
		}*/

		public static List<PartyMember> ConvertPartyMemberDataListToPartyMemberList(List<PartyMemberData> partyMembersData)
		{
			var partyMembers = new List<PartyMember>();
			foreach (var partyMemberData in partyMembersData)
			{
				partyMembers.Add(ConvertPartyMemberDataToPartyMember(partyMemberData));
			}
			return partyMembers;
		}

		private static PartyMember ConvertPartyMemberDataToPartyMember(PartyMemberData partyMemberData)
		{
			var weaponId = partyMemberData.weapon?.itemId ?? ""; // Assuming 0 is an invalid or "no item" ID
			var armorId = partyMemberData.armor?.itemId ?? "";
			var relicId = partyMemberData.relic?.itemId ?? "";

			// Safe handling for accessories: Filter out any null accessories before selecting itemId
			var accessoryIds = partyMemberData.accessories
				?.Where(accessory => accessory != null) // Ensure accessory is not null
				.Select(accessory => accessory.itemId)
				.ToList() ?? new List<string>(); // Provide an empty list if accessories are null

			var partyMemberEquipment = new PartyMemberEquipment(
				weaponId,
				armorId,
				relicId,
				accessoryIds
			);

			var calculatedHitPoints = StatsMath.CalculateMaxHitPoints(partyMemberData);
			
			var hitPoints = new HitPoints(
				      calculatedHitPoints,
			calculatedHitPoints
			);

			var abilityPoints = ResourcesMath.CalculateAbilityPoints(partyMemberData);

			var might = new Might(
				partyMemberData.baseMight,
				StatsMath.CalculateTotalMight(partyMemberData)
			);
			
			var deftness = new Deftness(
				partyMemberData.baseDeftness,
				StatsMath.CalculateTotalDeftness(partyMemberData)
			);
			
			var tenacity = new Tenacity(
				partyMemberData.baseTenacity,
				StatsMath.CalculateTotalTenacity(partyMemberData)
			);
			
			var resolve = new Resolve(
				partyMemberData.baseResolve,
				StatsMath.CalculateTotalResolve(partyMemberData)
			);

			var partyMemberStats = new PartyMemberStats(
				partyMemberData.baseLevel,
				hitPoints,
				abilityPoints,
				might,
				deftness,
				tenacity,
				resolve
			);

			return new PartyMember(
				partyMemberData.partyMemberName,
				partyMemberData.skills.Select(skill => skill.skillId).ToList(),
				partyMemberEquipment,
				partyMemberStats
				);
		}
	}
}