using Akashic.Runtime.Actors.Battle.Base;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyMemberAnimationHandler : BattleActorAnimationHandler
    {
        public override void SetSelected(BattleActor battleActor)
        {
            selector.SetSelected(battleActor.statHandler.ActionPips);
        }

        public override void SetSelectedAsTarget()
        {
            selector.SetSelectedAsTarget();
        }
    }
}