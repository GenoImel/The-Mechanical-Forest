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
                partyMemberController.partyMemberStatHandler.BasePhysicalAttack,
                partyMemberController.partyMemberStatHandler.BaseMagicalAttack,
                partyMemberController.partyMemberStatHandler.BaseAccuracy,
                partyMemberController.partyMemberStatHandler.BasePhysicalDefense,
                partyMemberController.partyMemberStatHandler.BaseMagicalDefense,
                partyMemberController.partyMemberStatHandler.BaseEvade
                );

            return partyMember;
        }
    }
}