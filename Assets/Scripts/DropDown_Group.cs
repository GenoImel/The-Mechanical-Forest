using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown_Group : MonoBehaviour
{
    private DropDown_func[] dd_menus;
    private DropDown_func activeMenu;

    private void Awake()
    {
        dd_menus = FindObjectsOfType<DropDown_func>();
    }

    public void ToggleMenuOpen(DropDown_func selected_menu)
    {
        DropDown_func loopMenu = null;

        foreach (DropDown_func menu in dd_menus)
        {
            if (menu != selected_menu)
            {
                if (menu == activeMenu)
                    menu.DoAnimation3();
                
            }
            else if(menu == selected_menu)
            { 
                if (menu == activeMenu)
                {
                    menu.DoAnimation2();
                    loopMenu = null;
                }
                else
                {
                    menu.DoAnimation1();
                    loopMenu = menu;
                }
            }
        }

        activeMenu = loopMenu;
    }
}