using Akashic.Runtime.ScriptableObjects.Battle;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BaseSkill : MonoBehaviour
    {
        protected SkillData SkillData { get; set; }
        
        public void SetSkillData(SkillData skillData)
        {
            SkillData = skillData;
        }
    }
}