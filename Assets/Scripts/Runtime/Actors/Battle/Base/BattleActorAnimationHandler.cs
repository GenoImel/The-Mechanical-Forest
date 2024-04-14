using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Planning;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BattleActorAnimationHandler : MonoBehaviour
    {
        [SerializeField] protected BattleActorSelector selector;

        private BattleActor battleActor;
        
        protected virtual void OnEnable()
        {
            AddListeners();
        }

        protected virtual void OnDisable()
        {
            RemoveListeners();
        }
        
        public virtual void InitializeAnimationHandler(BattleActor battleActor)
        {
            selector.SetDeselected();
            this.battleActor = battleActor;
        }
        
        protected virtual void SetSelected(int numberOfPips)
        {
            selector.SetSelected(numberOfPips);
        }

        protected virtual void SetSelected()
        {
            
        }

        protected abstract void SelectedAsTarget();
        
        protected virtual void SetDeselected()
        {
            selector.SetDeselected();
        }
        
        protected virtual void OnSetBattleActorSelectedMessage(SetBattleActorSelectedMessage message)
        {
            if (message.selectedBattleActor == battleActor)
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
            if (message.targetedBattleActor == battleActor)
            {
                SelectedAsTarget();
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