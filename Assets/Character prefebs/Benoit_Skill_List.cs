using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benoit_Skill_List : MonoBehaviour
{
    public delegate void Skill();
    public List<Skill> SkillList = new List<Skill>();

    private void addSkill()
    {
        SkillList.Add(Attack_One);
        SkillList.Add(Attack_Two);
        SkillList.Add(Attack_Three);
        SkillList.Add(Attack_four);
    }

    private void Attack_One()
    {

    }
    private void Attack_Two()
    {

    }

    private void Attack_Three()
    {

    }
    private void Attack_four()
    {

    }
}
