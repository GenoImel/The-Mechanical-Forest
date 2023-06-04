using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Runtime.DropDown_Menu
{
    internal sealed class DropDownMenuTracker : MonoBehaviour
    {
        [SerializeField] private List<DropDownFunction> dropDownMenus;
        [SerializeField] private DropDownFunction activeMenu;

        public void ToggleMenuOpen(DropDownFunction selectedMenu)
        {
            DropDownFunction loopMenu = null;

            foreach (var menu in dropDownMenus)
            {
                if (menu != selectedMenu)
                {
                    if (menu == activeMenu)
                    {
                        menu.FastCloseAnimation();
                        return;
                    }
                }
                else if (menu == selectedMenu)
                {
                    if (menu == activeMenu)
                    {
                        menu.CloseAnimation();
                        loopMenu = null;
                    }
                    else
                    {
                        menu.OpenAnimation();
                        loopMenu = menu;
                    }
                }
            }

            activeMenu = loopMenu;
        }
    }
}