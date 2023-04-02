using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown_Group : MonoBehaviour
{
    private DropDown_func[] dd_menus;//list of all menus
    private DropDown_func activeMenu;//tracks what menu is currently open

    private void Awake()
    {
        dd_menus = FindObjectsOfType<DropDown_func>();//looks for the script that holds all the animation code
    }

    public void ToggleMenuOpen(DropDown_func selected_menu)
    {
        DropDown_func loopMenu = null;

        foreach (DropDown_func menu in dd_menus)
        {
            if (menu != selected_menu)//if I open a new dropdown menu but one is already open
            {
                if (menu == activeMenu)
                    //shuts the currently open menu without showing a transition
                    menu.DoAnimation3();
                
            }
            else if(menu == selected_menu)
            { 
                
                if (menu == activeMenu)//if I manually try to close an open menu
                {
                    //closes the drop down menu with a transition(like DoAnimation1 but closing isnstead)
                    menu.DoAnimation2();
                    loopMenu = null;
                }
                else//for when I open a new drop down menu
                {
                    //opens the drop down menu with a transition
                    menu.DoAnimation1();
                    loopMenu = menu;
                }
            }
        }

        activeMenu = loopMenu;
    }
}