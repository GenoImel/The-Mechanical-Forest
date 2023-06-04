using System;
using UnityEngine;

namespace Akashic.Runtime.DropDown_Menu
{
    internal sealed class DropDownFunction : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] RectTransform dropDownMenu;
        
        [Header("Animation State")]
        [SerializeField] AnimationState state = AnimationState.None;
        
        [Header("Animation Settings")]
        [SerializeField] private float moveSpeed = 260;
        [SerializeField] private Vector2 startDest = new Vector2(0, 0);
        [SerializeField] private Vector2 endDest = new Vector2(0, -142);
        
        private enum AnimationState
        {
            Open, 
            Close, 
            FastClose, 
            None
        }
        
        public void Update()
        {
            switch (state)
            {
                case AnimationState.Open:
                    Transition();
                    break;
                case AnimationState.Close:
                    Close();
                    break;
                case AnimationState.FastClose:
                    FastClose();
                    break;
            }
        }
        
        public void OpenAnimation()
        {
            state = AnimationState.Open;
        }
        
        public void CloseAnimation()
        {
            state = AnimationState.Close;
        }
        
        public void FastCloseAnimation()
        {
            state = AnimationState.FastClose;
        }

        private void Transition()
        {
            dropDownMenu.localPosition = Vector2.MoveTowards(
                dropDownMenu.localPosition, 
                endDest, 
                Time.deltaTime * moveSpeed
                );

            if (Math.Abs(dropDownMenu.localPosition.y - endDest.y) < float.Epsilon)
            {
                state = AnimationState.None; 
            }
        }

        private void Close()
        {
            dropDownMenu.localPosition = Vector2.MoveTowards(
                dropDownMenu.localPosition, 
                startDest, 
                Time.deltaTime * moveSpeed
                );

            if (Math.Abs(dropDownMenu.localPosition.y - startDest.y) < float.Epsilon)
            {
                state = AnimationState.None;
            }
        }

        private void FastClose()
        {
            dropDownMenu.localPosition = startDest;

            if (Math.Abs(dropDownMenu.localPosition.y - startDest.y) < float.Epsilon)
            {
                state = AnimationState.None;
            }
        }
    }
}