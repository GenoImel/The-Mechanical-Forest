using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDown_func : MonoBehaviour
{
    //what starts the script
    public enum AnimationState { Open, Close, FastClose, None }//state machine
    AnimationState state = AnimationState.None;

    DropDown_Group dd_Group;
    RectTransform dd_menu;

    [SerializeField] private Vector2 startDest = new Vector2(0, 0);//where the menu starts on screen awake or on close
    [SerializeField] public Vector2 endDest = new Vector2(0, -142);//where the menu will stop when bring opened


    private void Awake()
    {
        dd_menu = GetComponent<RectTransform>();
        dd_Group = FindObjectOfType<DropDown_Group>();
    }

    public void Update()
    {
        //depending on what's true, the drop down menu will either open or close
        if (state == AnimationState.Open)
        {
            transition();
            //Debug.Log("Update Transition");
        }
        else if (state == AnimationState.Close)
        {
            Close();
            //Debug.Log("Update Close");
        }
        else if (state == AnimationState.FastClose)
        {
            FClose();
            //Debug.Log("Update FClose");
        }
    }

    public void transition()
    {
        int movespeed = 260;
        //animation for opening the menu
        dd_menu.localPosition = Vector2.MoveTowards(dd_menu.localPosition, endDest, Time.deltaTime * movespeed);

        if (dd_menu.localPosition.y == endDest.y)
            state = AnimationState.None;
    }

    public void Close()
    {
        if (dd_menu != true)
        {
            
        }
        int movespeed = 260;
        //animation for closing the menu
        dd_menu.localPosition = Vector2.MoveTowards(dd_menu.localPosition, startDest, Time.deltaTime * movespeed);

        if (dd_menu.localPosition.y == startDest.y)
            state = AnimationState.None;
    }

    public void FClose()
    {
        dd_menu.localPosition = startDest;
        //animation for force closing a menu
        if (dd_menu.localPosition.y == startDest.y)
            state = AnimationState.None;
    }

    #region MenuObject stuff


    public void DoAnimation1()
    {
        Debug.Log(name + ", Open");
        state = AnimationState.Open;
    }
    public void DoAnimation2()
    {
        Debug.Log(name + ", Close");
        state = AnimationState.Close;
    }
    public void DoAnimation3()
    {
        Debug.Log(name + ", FastClose");
        state = AnimationState.FastClose;
    }
    #endregion
}