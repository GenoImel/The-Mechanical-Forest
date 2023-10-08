using Akashic.Runtime.Controllers.PartyMemberBattle;
using Akashic.Runtime.Serializers;

namespace Akashic.Runtime.Converters
{
    internal static class PartyMemberConverter
    {
        public static PartyMember ConvertControllerToPartyMember(PartyMemberController partyMemberController)
        {
            var partyMember = new PartyMember(
                partyMemberController.PartyMemberName,
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
    }
}