using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Environment;
using Akashic.Runtime.Serializers.Save;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyBattleActor : BattleActor
    {
        [SerializeField] public PartyBattleEquipmentHandler equipmentHandler;
        
        public void InitializePartyBattleActor(PartyMember partyMember)
        {
            equipmentHandler.InitializeEquipmentReferences(partyMember);

            var parameters = new BattleActorInitializationParameters();
            parameters.SetPartyMember(partyMember);
            parameters.SetPartyBattleActor(this);
            
            statHandler.InitializeBattleActorStats(parameters);
        }
    }
}