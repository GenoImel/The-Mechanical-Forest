using System.Collections.Generic;
using Akashic.Runtime.Controllers.BattlePartyMember;
using Akashic.Runtime.Serializers;

namespace Akashic.Runtime.Converters
{
    internal static class PartyMemberConverter
    {
        public static PartyMember ConvertControllerToPartyMember(BattlePartyMemberController battlePartyMemberController)
        {
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
    }
}