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
            AddListeners();
        }

        protected virtual void OnDisable()
        {
            RemoveListeners();
        }
        
        protected virtual void SetSelected()
        {
            
        }

        protected virtual void SetSelectedAsTarget()
        {
            battleActorAnimationHandler.SetSelectedAsTarget();
        }
        
        public virtual void SetDeselected()
        {
            battleActorAnimationHandler.SetDeselected();
        }
        
        protected virtual void OnSetBattleActorSelectedMessage(SetBattleActorSelectedMessage message)
        {
            if (message.selectedBattleActor == this)
            {
                SetSelected();
            }
            else
            {
                SetDeselected();
            }
        }
        
        protected virtual void OnSetBattleActorSelectedAsTargetMessage(SetBattleActorSelectedAsTargetMessage message)
        {
            if (message.targetedBattleActor == this)
            {
                SetSelectedAsTarget();
            }
        }

        protected virtual void AddListeners()
        {
            GameManager.AddListener<SetBattleActorSelectedMessage>(OnSetBattleActorSelectedMessage);
            GameManager.AddListener<SetBattleActorSelectedAsTargetMessage>(OnSetBattleActorSelectedAsTargetMessage);
        }

        protected virtual void RemoveListeners()
        {
            GameManager.RemoveListener<SetBattleActorSelectedMessage>(OnSetBattleActorSelectedMessage);
            GameManager.RemoveListener<SetBattleActorSelectedAsTargetMessage>(OnSetBattleActorSelectedAsTargetMessage);
        }
    }
}