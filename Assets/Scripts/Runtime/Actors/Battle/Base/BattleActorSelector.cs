using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BattleActorSelector : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer targetSelector;

        public virtual void SetSelected(int numberOfPips)
        {
            
        }
        
        public virtual void SetSelected()
        {
            
        }

        public virtual void SetSelectedAsTarget()
        {
            targetSelector.enabled = true;
        }
        
        public virtual void SetDeselected()
        {
            targetSelector.enabled = false;
        }
    }
}