using Akashic.Core;
using Akashic.Runtime.MonoSystems.Battle;
using Akashic.Runtime.ScriptableObjects.Battle;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BaseSkill : MonoBehaviour
    {
        protected IPartyBattleMonoSystem partyBattleMonoSystem;

        protected IEnemyBattleMonoSystem enemyBattleMonoSystem;
        
        public SkillData SkillData { get; private set; }

        protected virtual void Awake()
        {
            partyBattleMonoSystem = GameManager.GetMonoSystem<IPartyBattleMonoSystem>();
            enemyBattleMonoSystem = GameManager.GetMonoSystem<IEnemyBattleMonoSystem>();
        }

        public void SetSkillData(SkillData skillData)
        {
            SkillData = skillData;
        }
    }
}