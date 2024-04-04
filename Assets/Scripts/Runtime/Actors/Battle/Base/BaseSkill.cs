using Akashic.Runtime.ScriptableObjects.Battle;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BaseSkill
    {
        protected SkillData SkillData { get; set; }
        
        public void SetSkillData(SkillData skillData)
        {
            SkillData = skillData;
        }
    }
}