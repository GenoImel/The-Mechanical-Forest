using Akashic.Runtime.Actors.Battle.Base;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyMemberAnimationHandler : BattleActorAnimationHandler
    {
        protected override void SetSelected(int numberOfPips)
        {
            selector.SetSelected(numberOfPips);
        }

        protected override void SelectedAsTarget()
        {
            selector.SetSelectedAsTarget();
        }
    }
}