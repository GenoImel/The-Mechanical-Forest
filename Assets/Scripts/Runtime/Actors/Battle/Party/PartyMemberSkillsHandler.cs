using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.MonoSystems.Resource;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyMemberSkillsHandler : MonoBehaviour
    {
        public BaseSkill attackSkill;
        public BaseSkill defendSkill;
        
        private IResourceMonoSystem resourceMonoSystem;
        
        private void Awake()
        {
            resourceMonoSystem = GameManager.GetMonoSystem<IResourceMonoSystem>();
        }

        public void InitializeSkills()
        {
            var attackSkillData = resourceMonoSystem.GetSkillById("attack");
            attackSkill = Instantiate(attackSkillData.GetSkillScript(), transform);
            attackSkill.SetSkillData(attackSkillData);
            
            var defendSkillData = resourceMonoSystem.GetSkillById("defend");
            defendSkill = Instantiate(defendSkillData.GetSkillScript(), transform);
            defendSkill.SetSkillData(defendSkillData);
        }
    }
}