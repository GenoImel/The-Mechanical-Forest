using Akashic.Core;
using Akashic.ScriptableObjects.Exploration;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Akashic.Runtime.Actors.Exploration
{
    internal sealed class LoadingTrigger : MonoBehaviour
    {
        [SerializeField] private string triggerId;
        [SerializeField] private ExplorationEnvironmentData targetExplorationEnvironment;
        [SerializeField] private InputActionReference interactInputAction; 
        
        private bool playerInsideTrigger; 

        private void Awake()
        {
            interactInputAction.action.Disable();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                return;
            }

            playerInsideTrigger = true;
            interactInputAction.action.Enable(); 
            interactInputAction.action.performed += HandleLoadSceneInput;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                return;
            }
            
            playerInsideTrigger = false;
            Cleanup();
        }

        private void HandleLoadSceneInput(InputAction.CallbackContext context)
        {
            if (!playerInsideTrigger)
            {
                return;
            } 

            GameManager.Publish(
                new LoadExplorationEnvironmentFromTriggerMessage(
                    targetExplorationEnvironment,
                    triggerId
                ));
            Cleanup();
        }

        private void Cleanup()
        {
            interactInputAction.action.Disable(); 
            interactInputAction.action.performed -= HandleLoadSceneInput; 
        }

        private void OnDestroy()
        {
            Cleanup();
        }
    }
}