using Akashic.Runtime.Actors.Battle.Base;

namespace Akashic.Runtime.Actors.Battle.Enemy
{
    internal sealed class EnemyBattleActorAnimationHandler : BattleActorAnimationHandler
    {
        protected override void SetSelected()
        {
            selector.SetSelected();
        }
        
        protected override void SelectedAsTarget()
        {
            selector.SetSelectedAsTarget();
        }
    }
}