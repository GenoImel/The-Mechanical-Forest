using UnityEngine;
using UnityEngine.InputSystem;

namespace Akashic.Runtime.Actors.Player
{
    internal sealed class PlayerActor : MonoBehaviour
    {
        [Header("Handlers")]
        [SerializeField] private PlayerMovementHandler playerMovementHandler;
        
        [SerializeField] private InputActionReference moveInputActionReference;

        private Camera mainCamera;
        
        private void Awake()
        {
            mainCamera = Camera.main;
        }

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
            
            var cameraTransform = mainCamera.transform;
            
            var forward = cameraTransform.forward;
            var right = cameraTransform.right;
            
            forward.y = 0f;
            right.y = 0f;
            
            forward.Normalize();
            right.Normalize();
            
            var relativeMoveDirection = (forward * axis.y + right * axis.x).normalized;

            playerMovementHandler.UpdatePlayerMoveDirection(relativeMoveDirection);
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