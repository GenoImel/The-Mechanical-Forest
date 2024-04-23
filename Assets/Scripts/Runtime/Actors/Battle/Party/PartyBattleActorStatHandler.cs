using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Environment;
using Akashic.Runtime.Utilities.GameMath;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyBattleActorStatHandler : BattleActorStatHandler
    {
        private PartyBattleActor partyBattleActor;

        public override void InitializeBattleActorStats(BattleActorInitializationParameters parameters)
        {
            currentLevel = parameters.PartyMember.PartyMemberStats.Level;
            hitPoints = parameters.PartyMember.PartyMemberStats.HitPoints;
            baseAbilityPoints = parameters.PartyMember.PartyMemberStats.BaseAbilityPoints;
            might = parameters.PartyMember.PartyMemberStats.Might;
            deftness = parameters.PartyMember.PartyMemberStats.Deftness;
            tenacity = parameters.PartyMember.PartyMemberStats.Tenacity;
            resolve = parameters.PartyMember.PartyMemberStats.Resolve;
            
            SetBattleActorReference(parameters.PartyBattleActor);
            RegeneratePips();
            RefreshBufferHitPoints();
        }
        
        private void SetBattleActorReference(PartyBattleActor battleActor)
        {
            partyBattleActor = battleActor;
        }

        protected override void RefreshBufferHitPoints()
        {
            //TO DO: Only generate these when defend action is chosen
            bufferHitPoints = ResourcesMath.CalculateBufferHitPoints(partyBattleActor);
        }
    }
}
