using Akashic.Core.Messages;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyBattleActorAddedMessage : IMessage
    {
        public PartyBattleActor partyBattleActor;
        
        public PartyBattleActorAddedMessage(PartyBattleActor partyBattleActor)
        {
            this.partyBattleActor = partyBattleActor;
        }
    }
}