using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.MonoSystems.Battle;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Enemy.FodderImp
{
    internal sealed class ImpFodderBehaviour : BaseEnemyBehaviour, IEnemyBehaviour
    {
        private IPartyBattleMonoSystem PartyMonoSystem => 
            GameManager.GetMonoSystem<IPartyBattleMonoSystem>();
        
        private IEnemyBattleMonoSystem EnemyBattleMonoSystem => 
            GameManager.GetMonoSystem<IEnemyBattleMonoSystem>();
        
        public void ChooseAction()
        {
            var coinFlip = CoinFlip();

            if (coinFlip == 0)
            {
                ChooseToAttack();
            }
            else
            {
                
            }
        }

        private void ChooseToDefend()
        {
            
        }

        private void ChooseToAttack()
        {
            var battleActors = PartyMonoSystem.GetBattleActors();

            var chosenBattleActor = ChooseRandomBattleActor(battleActors);
        }

        private int CoinFlip()
        {
            return UnityEngine.Random.Range(0, 2);
        }
        
        public BattleActor ChooseRandomBattleActor(List<BattleActor> battleActors)
        {
            if (battleActors.Count == 0)
            {
                Debug.LogError("No battle actors found");
                return null;
            }

            var randomIndex = Random.Range(0, battleActors.Count);
            return battleActors[randomIndex];
        }
    }
}