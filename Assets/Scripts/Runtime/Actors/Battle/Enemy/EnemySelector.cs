using Akashic.Runtime.Actors.Battle.Base;

namespace Akashic.Runtime.Actors.Battle.Enemy
{
    internal sealed class EnemySelector : BattleActorSelector
    {
        public override void SetSelected()
        {
            targetSelector.enabled = true;
        }

        public override void SetDeselected()
        {
            targetSelector.enabled = false;
        }
    }
}