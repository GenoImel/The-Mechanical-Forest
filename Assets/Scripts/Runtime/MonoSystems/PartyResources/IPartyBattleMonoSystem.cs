using Akashic.Core.MonoSystems;
using Akashic.Runtime.Actors.Battle;

namespace Akashic.Runtime.MonoSystems.PartyResources
{
    internal interface IPartyBattleMonoSystem : IMonoSystem
    {
        public void AddPartyBattleActor(PartyBattleActor partyBattleActor);

        public void InitializeAbilityPoints();
    }
}