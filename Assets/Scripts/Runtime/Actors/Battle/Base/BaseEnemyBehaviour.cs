using Akashic.Core;
using Akashic.Runtime.MonoSystems.Battle;
using Akashic.Runtime.MonoSystems.Resource;
using Akashic.Runtime.MonoSystems.Timeline;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BaseEnemyBehaviour : MonoBehaviour
    {
        protected BattleActor sourceBattleActor;
        
        protected BaseSkill attackSkill;
        protected BaseSkill defendSkill;
        
        protected IPartyBattleMonoSystem partyBattleMonoSystem;
        protected IEnemyBattleMonoSystem enemyBattleMonoSystem;
        protected ITimelineMonoSystem timelineMonoSystem;
        protected IResourceMonoSystem resourceMonoSystem;

        private void Awake()
        {
            partyBattleMonoSystem = GameManager.GetMonoSystem<IPartyBattleMonoSystem>();
            enemyBattleMonoSystem = GameManager.GetMonoSystem<IEnemyBattleMonoSystem>();
            timelineMonoSystem = GameManager.GetMonoSystem<ITimelineMonoSystem>();
            resourceMonoSystem = GameManager.GetMonoSystem<IResourceMonoSystem>();
        }

        public abstract void ChooseAction();

        public void SetSourceBattleActor(BattleActor source)
        {
            sourceBattleActor = source;
        }

        private void SetBaseSkills()
        {
            var attackSkillData = resourceMonoSystem.GetSkillById("attack");
            attackSkill = attackSkillData.GetSkillScript();

            var defendSkillData = resourceMonoSystem.GetSkillById("defend");
            defendSkill = defendSkillData.GetSkillScript();
        }
    }
}