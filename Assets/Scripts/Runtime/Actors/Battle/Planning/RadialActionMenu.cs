using System;
using Akashic.Core;
using Akashic.Runtime.Utilities.GameMath;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Akashic.Runtime.Actors.Battle.Planning
{
    internal sealed class RadialActionMenu : MonoBehaviour
    {
        [Header("Player Input")]
        [SerializeField] private InputActionReference selectInputAction;
        [SerializeField] private InputActionReference navigateInputAction;
        
        private bool isWaitingForInput;
        private bool isShown;

        private void OnEnable()
        {
            throw new NotImplementedException();
        }
        
        private void OnDisable()
        {
            throw new NotImplementedException();
        }

        private void FixedUpdate()
        {
            if (isWaitingForInput)
            {
                var axesDirection = InputMath.GetAxisValueAsInt(
                    navigateInputAction.action.ReadValue<Vector2>().x
                );
                
                //then do something with it i guess
            }
        }

        public void SetWaitForInput(bool waitingForInput)
        {
            isWaitingForInput = waitingForInput;
        }
        
        private void OnSelectStarted(InputAction.CallbackContext context)
        {
            if (!isWaitingForInput)
            {
                return;
            }

            isShown = true;
            GameManager.Publish(new RadialActionMenuActiveMessage());
        }
        
        private void OnSelectCanceled(InputAction.CallbackContext context)
        {
            if (!isWaitingForInput)
            {
                return;
            }
            
            isShown = false;
            GameManager.Publish(new RadialActionMenuInactiveMessage());
        }
        
        private void AddListeners()
        {
            selectInputAction.action.started += OnSelectStarted;
            selectInputAction.action.canceled += OnSelectCanceled;
            selectInputAction.action.Enable();
        }
        
        private void RemoveListeners()
        {
            selectInputAction.action.started -= OnSelectStarted;
            selectInputAction.action.canceled -= OnSelectCanceled;
            selectInputAction.action.Disable();
        }
    }
}