using UnityEngine;

namespace Akashic.Runtime.SkillSelect
{
    public class SkillMenuManager : MonoBehaviour
    {
        public GameObject[] skillMenuLists;
        
        public void MenuOff()
        {
            foreach (var skill in skillMenuLists)
            {
                skill.SetActive(false);
            }
        }
    }
}
