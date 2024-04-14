using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Party;
using Akashic.Runtime.Actors.Battle.Planning;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BattleActor : MonoBehaviour
    {
        [Header("Party Member Info")]
        [SerializeField] protected string actorName;

        [Header("Handlers")]
        [SerializeField] public BattleActorStatHandler statHandler;
        
        [SerializeField] public PartyMemberSkillsHandler partyMemberSkillsHandler;
        
        [SerializeField] public BattleActorAnimationHandler battleActorAnimationHandler;
        
        [SerializeField] public PartyMemberEffectHandler partyMemberEffectHandler;
        
        [SerializeField] public PartyMemberSoundHandler partyMemberSoundHandler;
        
        public string ActorName => actorName;

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }
        

        protected virtual void AddListeners()
        {
            
        }

        protected virtual void RemoveListeners()
        {
            
        }
    }
}