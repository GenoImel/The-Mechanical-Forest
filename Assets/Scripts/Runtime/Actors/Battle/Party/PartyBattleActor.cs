using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Environment;
using Akashic.Runtime.Actors.Battle.Planning;
using Akashic.Runtime.Serializers.Save;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyBattleActor : BattleActor
    {
        [SerializeField] public PartyBattleEquipmentHandler equipmentHandler;
        
        public void InitializePartyBattleActor(PartyMember partyMember)
        {
            equipmentHandler.InitializeEquipmentReferences(partyMember);

            var parameters = new BattleActorInitializationParameters();
            parameters.SetPartyMember(partyMember);
            parameters.SetPartyBattleActor(this);
            
            statHandler.InitializeBattleActorStats(parameters);
            battleActorAnimationHandler.InitializeAnimationHandler();
        }
        
        protected override void SetSelected()
        {
            battleActorAnimationHandler.SetSelected(statHandler.ActionPips);
        }
        
        private void OnAttackChosen()
        {
            GameManager.Publish(new PartyMemberActionChosenMessage(this, partyMemberSkillsHandler.attackSkill));
        }
        
        private void OnDefendChosen()
        {
            GameManager.Publish(new PartyMemberActionChosenMessage(this, partyMemberSkillsHandler.defendSkill));
        }
        
        private void OnSkillChosen()
        {

        }
        
        private void OnItemChosen()
        {

        }

        protected override void AddListeners()
        {
            base.AddListeners();
            
            ((PartyMemberAnimationHandler)battleActorAnimationHandler).OnAttackActionSelected += OnAttackChosen;
            ((PartyMemberAnimationHandler)battleActorAnimationHandler).OnDefendActionSelected += OnDefendChosen;
            ((PartyMemberAnimationHandler)battleActorAnimationHandler).OnSkillChosen += OnSkillChosen;
            ((PartyMemberAnimationHandler)battleActorAnimationHandler).OnItemChosen += OnItemChosen;
        }
        
        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            
            ((PartyMemberAnimationHandler)battleActorAnimationHandler).OnAttackActionSelected -= OnAttackChosen;
            ((PartyMemberAnimationHandler)battleActorAnimationHandler).OnDefendActionSelected -= OnDefendChosen;
            ((PartyMemberAnimationHandler)battleActorAnimationHandler).OnSkillChosen -= OnSkillChosen;
            ((PartyMemberAnimationHandler)battleActorAnimationHandler).OnItemChosen -= OnItemChosen;
        }
    }
}