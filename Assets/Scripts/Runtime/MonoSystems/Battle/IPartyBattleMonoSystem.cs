using Akashic.Core.MonoSystems;
using Akashic.Runtime.Actors.Battle;

namespace Akashic.Runtime.MonoSystems.Battle
{
    internal interface IPartyBattleMonoSystem : IMonoSystem
    {
        public void AddPartyBattleActor(PartyBattleActor partyBattleActor);

        public void InitializeAbilityPoints();
    }
}