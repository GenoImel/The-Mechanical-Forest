using System.Linq;
using System.Threading.Tasks;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.MonoSystems.Timeline;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Enemy.FodderImp
{
    internal sealed class ImpFodderBehaviour : BaseEnemyBehaviour
    {
        public override async Task ChooseActionAsync()
        {
            while (sourceBattleActor.statHandler.ActionPips > 0
                   && timelineMonoSystem.TimelineMoves
                       .Any(moves => !moves.isOccupied && !moves.isReservedForParty))
            {
                var coinFlip = CoinFlip();

                if (coinFlip == 0)
                {
                    ChooseToAttack();
                }
                else
                {
                    ChooseToDefend();
                }
                
                if (sourceBattleActor.statHandler.ActionPips <= 0)
                {
                    break;
                }
            }
        }

        private void ChooseToDefend()
        {
            var targetBattleActor = ChooseTarget();

            var moveToSet = ChooseRandomMove();

            if (moveToSet == null)
            {
                return;
            }

            moveToSet.SetSourceBattleActor(sourceBattleActor)
                .SetTargetBattleActor(targetBattleActor)
                .SetSkill(defendSkill)
                .Occupy();
            
            sourceBattleActor.statHandler.RemoveActionPips(defendSkill.SkillData.pipCost);
        }

        private void ChooseToAttack()
        {
            var targetBattleActor = ChooseTarget();

            var moveToSet = ChooseRandomMove();

            if (moveToSet == null)
            {
                return;
            }

            moveToSet.SetSourceBattleActor(sourceBattleActor)
                .SetTargetBattleActor(targetBattleActor)
                .SetSkill(attackSkill)
                .Occupy();
            
            sourceBattleActor.statHandler.RemoveActionPips(attackSkill.SkillData.pipCost);
        }

        private BattleActor ChooseTarget()
        {
            var battleActors = partyBattleMonoSystem.GetBattleActorsAsBase();

            if (battleActors.Count == 0)
            {
                Debug.LogError("No battle actors found");
                return null;
            }

            var randomIndex = Random.Range(0, battleActors.Count);
            return battleActors[randomIndex];
        }

        private TimelineMove ChooseRandomMove()
        {
            var availableMoves = timelineMonoSystem.TimelineMoves
                .Where(move => !move.isOccupied && !move.isReservedForParty)
                .ToList();

            if (availableMoves.Count == 0)
            {
                Debug.LogError("No available moves found");
                return null; 
            }

            var randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }

        private int CoinFlip()
        {
            return Random.Range(0, 2);
        }
    }
}