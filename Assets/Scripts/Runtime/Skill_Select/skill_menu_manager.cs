using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill_menu_manager : MonoBehaviour
{
    public GameObject[] skill_Menu_lists;
    


    public void menu_off()
    {
        for(int i = 0; i < skill_Menu_lists.Length; i++)
        {
            skill_Menu_lists[i].SetActive(false);
        }

    }



}
