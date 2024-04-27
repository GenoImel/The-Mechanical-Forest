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
    
    internal sealed class ChooseTimelinePlacementMessage : IMessage
    {
        
    }
    
    internal sealed class TimelinePlacementEscapedMessage : IMessage
    {
        
    }

    internal sealed class PartyMemberActionChosenMessage : IMessage
    {
        public readonly BattleActor SourceActor;
        public readonly BaseSkill ChosenSkill;
        
        public PartyMemberActionChosenMessage(BattleActor sourceActor, BaseSkill ChosenSkill)
        {
            SourceActor = sourceActor;
            ChosenSkill = ChosenSkill;
        }
    }
    
    internal sealed class TimelineMovePlacedMessage : IMessage
    {
        public int TimelinePosition;
        
        public TimelineMovePlacedMessage(int timelinePosition)
        {
            TimelinePosition = timelinePosition;
        }
    }
}