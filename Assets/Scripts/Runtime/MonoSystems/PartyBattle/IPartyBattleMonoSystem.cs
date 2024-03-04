using Akashic.Core.MonoSystems;
using Akashic.Runtime.Actors.Battle;

namespace Akashic.Runtime.MonoSystems.PartyBattle
{
    internal interface IPartyBattleMonoSystem : IMonoSystem
    {
        public void AddPartyBattleActor(PartyBattleActor partyBattleActor);

        public void InitializeAbilityPoints();
    }
}