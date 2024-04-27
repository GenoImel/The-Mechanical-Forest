using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Akashic.Runtime.Actors.Battle.Planning
{
    internal sealed class RadialActionMenu : MonoBehaviour
    {
        [Header("Player Input")]
        [SerializeField] private InputActionReference selectInputAction;
        [SerializeField] private InputActionReference navigateInputAction;
        
        [Header("Primary Sprite")]
        [SerializeField] private GameObject primarySprite;

        [Header("Menu Items")]
        [SerializeField] private HighlightSprite upAction;
        [SerializeField] private HighlightSprite downAction;
        [SerializeField] private HighlightSprite leftAction;
        [SerializeField] private HighlightSprite rightAction;

        private HighlightSprite[] menuItems;
        private int currentItemIndex = -1;
        private bool isWaitingForInput;
        private bool isShown;

        public Action OnAttackActionSelected;
        public Action OnDefendActionSelected;
        public Action OnSkillActionSelected;
        public Action OnItemActionSelected;

        private void Awake()
        {
            menuItems = new HighlightSprite[] { upAction, downAction, leftAction, rightAction };
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void Update()
        {
            if (!isShown || !isWaitingForInput)
            {
                return;
            }
            
            var inputVector = navigateInputAction.action.ReadValue<Vector2>();
            UpdateHighlightedAction(inputVector);
        }

        private void UpdateHighlightedAction(Vector2 direction)
        {
            var newIndex = -1;
            
            if (direction.y > 0.5) newIndex = 0; // Up
            else if (direction.y < -0.5) newIndex = 1; // Down
            else if (direction.x < -0.5) newIndex = 2; // Left
            else if (direction.x > 0.5) newIndex = 3; // Right

            if (newIndex == currentItemIndex)
            {
                return;
            }

            if (currentItemIndex >= 0)
            {
                menuItems[currentItemIndex].SetHighlighted(false);
            }
            
            currentItemIndex = newIndex;

            if (currentItemIndex >= 0)
            {
                menuItems[currentItemIndex].SetHighlighted(true);
            }
        }

        public void SetWaitForInput(bool waitingForInput)
        {
            isWaitingForInput = waitingForInput;
            
            if (!isWaitingForInput)
            {
                primarySprite.SetActive(false);
            }
        }

        private void OnSelectStarted(InputAction.CallbackContext context)
        {
            isShown = true;
            primarySprite.SetActive(true);
        }

        private void OnSelectCanceled(InputAction.CallbackContext context)
        {
            if (isShown && currentItemIndex >= 0)
            {
                menuItems[currentItemIndex].InvokeAction();
            }
            
            primarySprite.SetActive(false);
            isWaitingForInput = false;
            isShown = false;
        }

        private void OnAttackActionInvoked()
        {
            OnAttackActionSelected?.Invoke();
        }

        private void OnDefendActionInvoked()
        {
            OnDefendActionSelected?.Invoke();
        }
        
        private void OnSkillActionInvoked()
        {
            OnSkillActionSelected?.Invoke();
        }
        
        private void OnItemActionInvoked()
        {
            OnItemActionSelected?.Invoke();
        }
        
        private void AddListeners()
        {
            upAction.OnSelectAction += OnAttackActionInvoked;
            downAction.OnSelectAction += OnDefendActionInvoked;
            leftAction.OnSelectAction += OnSkillActionInvoked;
            rightAction.OnSelectAction += OnItemActionInvoked;
            
            selectInputAction.action.started += OnSelectStarted;
            selectInputAction.action.canceled += OnSelectCanceled;
            selectInputAction.action.Enable();
            
            navigateInputAction.action.Enable();
        }

        private void RemoveListeners()
        {
            upAction.OnSelectAction -= OnAttackActionInvoked;
            downAction.OnSelectAction -= OnDefendActionInvoked;
            leftAction.OnSelectAction -= OnSkillActionInvoked;
            rightAction.OnSelectAction -= OnItemActionInvoked;
            
            selectInputAction.action.started -= OnSelectStarted;
            selectInputAction.action.canceled -= OnSelectCanceled;
            selectInputAction.action.Disable();
            
            navigateInputAction.action.Disable();
        }
    }
}