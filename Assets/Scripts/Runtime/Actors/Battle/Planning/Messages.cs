using Akashic.Core.Messages;
using Akashic.Runtime.Actors.Battle.Base;

namespace Akashic.Runtime.Actors.Battle.Planning
{
    internal sealed class DeselectAllBattleActorsMessage : IMessage
    {
        
    }

    internal sealed class SetBattleActorSelectedMessage : IMessage
    {
        public readonly BattleActor selectedBattleActor;
        
        public SetBattleActorSelectedMessage(BattleActor battleActorToSelect)
        {
            selectedBattleActor = battleActorToSelect;
        }
    }

    internal sealed class SetBattleActorSelectedAsTargetMessage : IMessage
    {
        public readonly BattleActor targetedBattleActor;
        
        public SetBattleActorSelectedAsTargetMessage(BattleActor battleActorToTarget)
        {
            targetedBattleActor = battleActorToTarget;
        }
    }

    internal sealed class RadialActionMenuActiveMessage : IMessage
    {
        
    }
    
    internal sealed class RadialActionMenuInactiveMessage : IMessage
    {
        
    }
}