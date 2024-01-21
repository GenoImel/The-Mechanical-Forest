using UnityEngine;
using UnityEngine.InputSystem;

namespace Akashic.Runtime.Actors.Player
{
    internal sealed class PlayerActor : MonoBehaviour
    {
        [Header("Handlers")]
        [SerializeField] private PlayerMovementHandler playerMovementHandler;
        
        [SerializeField] private InputActionReference moveInputActionReference;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void OnMoveInputActionPerformed(InputAction.CallbackContext context)
        {
            var axis = context.ReadValue<Vector2>();
            var absoluteMoveDirection = new Vector3(axis.x, 0.0f, axis.y).normalized;
            playerMovementHandler.UpdatePlayerMoveDirection(absoluteMoveDirection);
        }
        
        private void OnMoveInputActionCanceled(InputAction.CallbackContext context)
        {
            playerMovementHandler.StopMovement();
        }

        private void AddListeners()
        {
            moveInputActionReference.action.performed += OnMoveInputActionPerformed;
            moveInputActionReference.action.canceled += OnMoveInputActionCanceled;
        }

        private void RemoveListeners()
        {
            moveInputActionReference.action.performed -= OnMoveInputActionPerformed;
            moveInputActionReference.action.canceled -= OnMoveInputActionCanceled;
        }
    }
}