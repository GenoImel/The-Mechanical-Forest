using Akashic.Runtime.Actors.Battle.Base;

namespace Akashic.Runtime.Actors.Battle.Enemy
{
    internal sealed class EnemyBattleActorAnimationHandler : BattleActorAnimationHandler
    {
        public override void SetSelected()
        {
            selector.SetSelected();
        }
        
        public override void SetSelectedAsTarget()
        {
            selector.SetSelectedAsTarget();
        }
    }
}