using Akashic.Runtime.Actors.Battle.Base;

namespace Akashic.Runtime.MonoSystems.Timeline
{
    internal sealed class TimelineMove
    {
        public BattleActor SourceBattleActor { get; private set; }
        public BattleActor TargetBattleActor { get; private set; }
        public ISkill Skill { get; private set; }
        public bool isReservedForParty;
        public bool isOccupied;

        public TimelineMove SetSourceBattleActor(BattleActor sourceBattleActor)
        {
            SourceBattleActor = sourceBattleActor;
            return this;
        }
        
        public TimelineMove SetTargetBattleActor(BattleActor targetBattleActor)
        {
            TargetBattleActor = targetBattleActor;
            return this;
        }
        
        public TimelineMove SetSkill(ISkill skill)
        {
            Skill = skill;
            return this;
        }

        public TimelineMove ReserveForParty()
        {
            isReservedForParty = true;
            return this;
        }
        
        public TimelineMove Occupy()
        {
            isOccupied = true;
            return this;
        }
    }
}