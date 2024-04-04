using System.Linq;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.MonoSystems.Timeline;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Enemy.FodderImp
{
    internal sealed class ImpFodderBehaviour : BaseEnemyBehaviour, IEnemyBehaviour
    {
        public override void ChooseAction()
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
        }

        private void ChooseToDefend()
        {
            var targetBattleActor = ChooseTarget();

            var moveIndex = ChooseRandomMove();

            var moveToSet = new TimelineMove()
                .SetSourceBattleActor(sourceBattleActor)
                .SetTargetBattleActor(targetBattleActor)
                .SetSkill(defendSkill)
                .Occupy();
            
            timelineMonoSystem.SetMove(moveIndex, moveToSet);
        }

        private void ChooseToAttack()
        {
            var targetBattleActor = ChooseTarget();

            var moveIndex = ChooseRandomMove();

            var moveToSet = new TimelineMove()
                .SetSourceBattleActor(sourceBattleActor)
                .SetTargetBattleActor(targetBattleActor)
                .SetSkill(attackSkill)
                .Occupy();
            
            timelineMonoSystem.SetMove(moveIndex, moveToSet);
        }

        private BattleActor ChooseTarget()
        {
            var battleActors = partyBattleMonoSystem.GetBattleActors();

            if (battleActors.Count == 0)
            {
                Debug.LogError("No battle actors found");
                return null;
            }

            var randomIndex = Random.Range(0, battleActors.Count);
            return battleActors[randomIndex];
        }

        private int ChooseRandomMove()
        {
            var moves = timelineMonoSystem.TimelineMoves
                .Where(move => !move.isOccupied && !move.isReservedForParty).ToList();

            return Random.Range(0, moves.Count);
        }

        private int CoinFlip()
        {
            return Random.Range(0, 2);
        }
    }
}