using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BattleActorAnimationHandler : MonoBehaviour
    {
        [SerializeField] protected BattleActorSelector selector;
        
        protected virtual void OnEnable()
        {
            AddListeners();
        }

        protected virtual void OnDisable()
        {
            RemoveListeners();
        }
        
        public virtual void InitializeAnimationHandler()
        {
            selector.SetDeselected();
        }

        public virtual void SetSelected(BattleActor battleActor)
        {

        }

        public abstract void SetSelectedAsTarget();
        
        public virtual void SetDeselected()
        {
            selector.SetDeselected();
        }
        
        protected virtual void AddListeners()
        {

        }

        protected virtual void RemoveListeners()
        {

        }
    }
}