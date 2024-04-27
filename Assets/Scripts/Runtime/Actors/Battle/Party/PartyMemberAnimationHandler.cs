using System;
using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Planning;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyMemberAnimationHandler : BattleActorAnimationHandler
    {
        [SerializeField] private RadialActionMenu radialActionMenu;
        
        public Action OnAttackActionSelected;
        public Action OnDefendActionSelected;
        public Action OnSkillChosen;
        public Action OnItemChosen;

        private bool keepSelectorVisible;
        
        public override void SetSelected(int numberOfPips)
        {
            selector.SetSelected(numberOfPips);
            
            if(numberOfPips > 0)
            {
                radialActionMenu.SetWaitForInput(true);
            }
        }
        
        public override void SetDeselected()
        {
            if(keepSelectorVisible)
            {
                return;
            }
            
            selector.SetDeselected();
            radialActionMenu.SetWaitForInput(false);
        }

        public override void SetSelectedAsTarget()
        {
            selector.SetSelectedAsTarget();
        }
        
        private void OnAttackInvoked()
        {
            radialActionMenu.SetWaitForInput(false);
            keepSelectorVisible = true;
            OnAttackActionSelected?.Invoke();
        }
        
        private void OnDefendInvoked()
        {
            radialActionMenu.SetWaitForInput(false);
            keepSelectorVisible = true;
            OnDefendActionSelected?.Invoke();
        }
        
        private void OnSkillInvoked()
        {
            radialActionMenu.SetWaitForInput(false);
            keepSelectorVisible = true;
            // show skill menu
        }
        
        private void OnItemInvoked()
        {
            radialActionMenu.SetWaitForInput(false);
            keepSelectorVisible = true;
            // show item menu
        }
        
        private void OnActionConfirmedMessage(TimelineMovePlacedMessage message)
        {
            if (keepSelectorVisible)
            {
                keepSelectorVisible = false;
            }
        }
        
        protected override void AddListeners()
        {
            base.AddListeners();
            
            GameManager.AddListener<TimelineMovePlacedMessage>(OnActionConfirmedMessage);
            
            radialActionMenu.OnAttackActionSelected += OnAttackInvoked;
            radialActionMenu.OnDefendActionSelected += OnDefendInvoked;
            radialActionMenu.OnSkillActionSelected += OnSkillInvoked;
            radialActionMenu.OnItemActionSelected += OnItemInvoked;
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            
            GameManager.RemoveListener<TimelineMovePlacedMessage>(OnActionConfirmedMessage);
            
            radialActionMenu.OnAttackActionSelected -= OnAttackInvoked;
            radialActionMenu.OnDefendActionSelected -= OnDefendInvoked;
            radialActionMenu.OnSkillActionSelected -= OnSkillInvoked;
            radialActionMenu.OnItemActionSelected -= OnItemInvoked;
        }
    }
}