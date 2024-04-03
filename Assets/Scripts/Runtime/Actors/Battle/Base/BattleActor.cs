using Akashic.Runtime.Actors.Battle.Party;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal class BattleActor : MonoBehaviour
    {
        [Header("Party Member Info")]
        [SerializeField] protected string actorName;

        [Header("Handlers")]
        [SerializeField] public BattleActorStatHandler statHandler;
        
        [SerializeField] public PartyMemberSkillsHandler partyMemberSkillsHandler;
        
        [SerializeField] public PartyMemberAnimationHandler partyMemberAnimationHandler;
        
        [SerializeField] public PartyMemberEffectHandler partyMemberEffectHandler;
        
        [SerializeField] public PartyMemberSoundHandler partyMemberSoundHandler;
        
        public string ActorName => actorName;
    }
}