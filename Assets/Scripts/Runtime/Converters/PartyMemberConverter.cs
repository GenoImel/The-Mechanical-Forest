using Akashic.Runtime.Controllers.PartyMemberBattle;
using Akashic.Runtime.Serializers;

namespace Akashic.Runtime.Converters
{
    internal static class PartyMemberConverter
    {
        public static PartyMember ConvertControllerToPartyMember(PartyMemberController partyMemberController)
        {
            var partyMember = new PartyMember(
                partyMemberController.partyMemberName,
                partyMemberController.partyMemberStatHandler.currentLevel,
                partyMemberController.partyMemberResourceHandler.currentExperience,
                partyMemberController.partyMemberResourceHandler.maxExperience,
                partyMemberController.partyMemberResourceHandler.currentHealth,
                partyMemberController.partyMemberResourceHandler.maxHealth,
                partyMemberController.partyMemberStatHandler.basePhysicalAttack,
                partyMemberController.partyMemberStatHandler.baseMagicalAttack,
                partyMemberController.partyMemberStatHandler.baseAccuracy,
                partyMemberController.partyMemberStatHandler.basePhysicalDefense,
                partyMemberController.partyMemberStatHandler.baseMagicalDefense,
                partyMemberController.partyMemberStatHandler.baseEvade
                );

            return partyMember;
        }
    }
}