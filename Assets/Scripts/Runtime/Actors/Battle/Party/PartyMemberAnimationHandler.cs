using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Planning;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyMemberAnimationHandler : BattleActorAnimationHandler
    {
        [SerializeField] private RadialActionMenu radialActionMenu;
        
        public override void SetSelected(BattleActor battleActor)
        {
            selector.SetSelected(battleActor.statHandler.ActionPips);
            radialActionMenu.SetWaitForInput(true);
        }
        
        public override void SetDeselected()
        {
            selector.SetDeselected();
            radialActionMenu.SetWaitForInput(false);
        }

        public override void SetSelectedAsTarget()
        {
            selector.SetSelectedAsTarget();
        }
    }
}