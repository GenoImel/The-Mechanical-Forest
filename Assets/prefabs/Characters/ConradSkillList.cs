using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Prefabs.Characters
{
    public class ConradSkillList : MonoBehaviour
    {
        private delegate void Skill();
        private readonly List<Skill> skillList =  new List<Skill>();

        private void AddSkill()
        {
            skillList.Add(AttackOne);
            skillList.Add(AttackTwo);
            skillList.Add(AttackThree);
            skillList.Add(AttackFour);
        }

        private void AttackOne()
        {
        }
        
        private void AttackTwo()
        {
        }

        private void AttackThree()
        {
        }
        
        private void AttackFour()
        {
        }
    }
}
