using System;
using System.Collections.Generic;
using System.Linq;
using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Enemy;
using Akashic.Runtime.Actors.Battle.Party;
using Akashic.Runtime.MonoSystems.Battle;
using Akashic.Runtime.MonoSystems.Timeline;
using Akashic.Runtime.StateMachines.TurnStates;
using Akashic.Runtime.Utilities.GameMath;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Akashic.Runtime.Actors.Battle.Planning
{
    internal sealed class PartyPlannerActor : MonoBehaviour
    {
        [Header("Player Input")]
        [SerializeField] private InputActionReference navigateInputAction;
        [SerializeField] private InputActionReference selectInputAction;
        [SerializeField] private InputActionReference backInputAction;
        
        [Header("Settings")]
        [SerializeField] private float inputDelaySeconds = 0.25f;

        private static TimelineMove currentMove;
        
        private static List<PartyBattleActor> partyBattleActors;
        private static List<EnemyBattleActor> enemyBattleActors;
        private static List<BattleActor> allBattleActors = new();
        
        private static int partyIndex;
        private static int enemyIndex;
        private static int entityIndex;

        private static int axesDirection;
        private static float delayCounterSeconds;

        private bool isSelectAsTarget;
        
        private static PlannerActorFiniteState currentPlanningState = new Inactive();
        private PlannerActorFiniteState prevPlanningState;
        
        private static TargetingFiniteState currentTargetingState = new NoneSelectable();
        private TargetingFiniteState prevTargetingState;

        private IPartyBattleMonoSystem partyBattleMonoSystem;
        private IEnemyBattleMonoSystem enemyBattleMonoSystem;
        private ITurnStateMachine turnStateMachine;
        private ITimelineMonoSystem timelineMonoSystem;

        private void Awake()
        {
            partyBattleMonoSystem = GameManager.GetMonoSystem<IPartyBattleMonoSystem>();
            enemyBattleMonoSystem = GameManager.GetMonoSystem<IEnemyBattleMonoSystem>();
            turnStateMachine = GameManager.GetStateMachine<ITurnStateMachine>();
            timelineMonoSystem = GameManager.GetMonoSystem<ITimelineMonoSystem>();
        }

        private void OnEnable()
        {
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }

        private void FixedUpdate()
        {
            if(turnStateMachine.CurrentState is TurnFiniteState.PartyPlanning)
            {
                delayCounterSeconds += Time.deltaTime;
                if(delayCounterSeconds < inputDelaySeconds)
                {
                    return;
                }
                
                axesDirection = InputMath.GetAxisValueAsInt(
                    navigateInputAction.action.ReadValue<Vector2>().x
                    );

                if (axesDirection == 0)
                {
                    return;
                }
                
                currentPlanningState.Execute();
            }
        }
        
        #region Planning States
        private sealed class Inactive : PlannerActorFiniteState
        {
            public override void Execute()
            {

            }
        }

        private class Hold : PlannerActorFiniteState
        {
            public override void Execute()
            {

            }
        }
        
        private class SelectingSource : PlannerActorFiniteState
        {
            public override void Execute()
            {
                currentTargetingState.ExecuteTargetSelection();
            }
        }
        
        private class SelectingTarget : PlannerActorFiniteState
        {
            public override void Execute()
            {
                currentTargetingState.ExecuteTargetSelection();
            }
        }
        #endregion
        
        #region Targeting States    
        private class NoneSelectable : TargetingFiniteState
        {
            public override void ExecuteTargetSelection()
            {
                
            }

            public override void ExecuteTargetConfirmation()
            {
                
            }
        }
        
        private class AllEntitiesSelectable : TargetingFiniteState
        {
            public override void ExecuteTargetSelection()
            {
                EntityIndexChanged(axesDirection);
            }
            
            public override void ExecuteTargetConfirmation()
            {
                currentMove.SetTargetBattleActor(allBattleActors.ElementAt(entityIndex));
            }
        }
        
        private class PartyMembersSelectable : TargetingFiniteState
        {
            public override void ExecuteTargetSelection()
            {
                PartyIndexChanged(axesDirection);
            }
            
            public override void ExecuteTargetConfirmation()
            {
                currentMove.SetTargetBattleActor(partyBattleActors.ElementAt(partyIndex));
            }
        }
        
        private class EnemiesSelectable : TargetingFiniteState
        {
            public override void ExecuteTargetSelection()
            {
                EnemyIndexChanged(axesDirection);
            }
            
            public override void ExecuteTargetConfirmation()
            {
                currentMove.SetTargetBattleActor(enemyBattleActors.ElementAt(enemyIndex));
            }
        }
        #endregion 
        
        private static void EntityIndexChanged(int direction)
        {
            entityIndex = (entityIndex + direction + allBattleActors.Count) % allBattleActors.Count;
            
            SelectEntity(entityIndex);
            
            delayCounterSeconds = 0;
        }
        
        private static void PartyIndexChanged(int direction)
        {
            partyIndex = (partyIndex + direction + partyBattleActors.Count) % partyBattleActors.Count;
            
            SelectEntity(partyIndex);
            
            delayCounterSeconds = 0;
        }
        
        private static void EnemyIndexChanged(int direction)
        {
            enemyIndex = (enemyIndex + direction + enemyBattleActors.Count) % enemyBattleActors.Count;
            
            SelectEntity(enemyIndex);
            
            delayCounterSeconds = 0;
        }
        
        private static void SelectEntity(int index)
        {
            var selectedEntity = allBattleActors.ElementAt(index);
            GameManager.Publish(new SetBattleActorSelectedMessage(selectedEntity));
        }

        private void SetPlanningState(PlannerActorFiniteState nextState)
        {
            if(nextState is null)
            {
                throw new ArgumentException("Invalid state type");
            }
            
            if (currentPlanningState is Hold)
            {
                return;
            }
            
            if(currentPlanningState is SelectingSource)
            {
                SetTargetingState(new AllEntitiesSelectable());
                return;
            }

            CleanUp();

            prevPlanningState = currentPlanningState;
            currentPlanningState = nextState;
        }

        private void SetTargetingState(TargetingFiniteState nextState)
        {
            if(nextState is null)
            {
                throw new ArgumentException("Invalid state type");
            }
            
            prevTargetingState = currentTargetingState;
            currentTargetingState = nextState;
        }

        private void CleanUp()
        {
            isSelectAsTarget = false;
            currentMove = new TimelineMove();
            GameManager.Publish(new DeselectAllBattleActorsMessage());
            //reset selection index?
        }
        
        private void OnTurnStateChangedMessage(TurnStateChangedMessage message)
        {
            if (message.NextState is not TurnFiniteState.PartyPlanning)
            {
                SetPlanningState(new Inactive());
            }
            
            if (message.NextState is TurnFiniteState.PartyPlanning)
            {
                SetPlanningState(new SelectingSource());
                return;
            }

            if (message.PrevState is TurnFiniteState.Initializing)
            {
                partyBattleActors = partyBattleMonoSystem.GetBattleActors();
                enemyBattleActors = enemyBattleMonoSystem.GetBattleActors();
            
                allBattleActors.AddRange(partyBattleActors);
                allBattleActors.AddRange(enemyBattleActors);
            }
        }
        
        private void OnRadialActionMenuActiveMessage(RadialActionMenuActiveMessage message)
        {
            SetPlanningState(new Hold());
        }
        
        private void OnRadialActionMenuInactiveMessage(RadialActionMenuInactiveMessage message)
        {
            SetPlanningState(prevPlanningState);
        }
        
        private void OnPartyMemberActionChosenMessage(PartyMemberActionChosenMessage message)
        {
            isSelectAsTarget = true;

            currentMove = new TimelineMove()
                .SetSourceBattleActor(message.SourceActor)
                .SetSkill(message.ChosenSkill)
                .Occupy();
            
            SetTargetingState(new AllEntitiesSelectable());
        }
        
        private void OnChooseTimelinePlacementMessage(ChooseTimelinePlacementMessage message)
        {
            SetPlanningState(new Hold());
            isSelectAsTarget = false;
        }
        
        private void OnTimelineMenuInactiveMessage(TimelinePlacementEscapedMessage message)
        {
            SetPlanningState(prevPlanningState);
        }
        
        private void OnSelectPerformed(InputAction.CallbackContext context)
        {
            currentTargetingState.ExecuteTargetConfirmation();

            SetPlanningState(new Hold()); // holding for timeline selection
            GameManager.Publish(new ChooseTimelinePlacementMessage());
        }

        private void OnBackPerformed(InputAction.CallbackContext context)
        {
            
        }
        
        private void AddListeners()
        {
            GameManager.AddListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
            GameManager.AddListener<RadialActionMenuActiveMessage>(OnRadialActionMenuActiveMessage);
            GameManager.AddListener<RadialActionMenuInactiveMessage>(OnRadialActionMenuInactiveMessage);
            
            GameManager.AddListener<PartyMemberActionChosenMessage>(OnPartyMemberActionChosenMessage);
            
            GameManager.AddListener<ChooseTimelinePlacementMessage>(OnChooseTimelinePlacementMessage);
            GameManager.AddListener<TimelinePlacementEscapedMessage>(OnTimelineMenuInactiveMessage);

            selectInputAction.action.performed += OnSelectPerformed;
            selectInputAction.action.Enable();
            
            backInputAction.action.performed += OnBackPerformed;
            backInputAction.action.Enable();
            
            navigateInputAction.action.Enable();
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
            GameManager.RemoveListener<RadialActionMenuActiveMessage>(OnRadialActionMenuActiveMessage);
            GameManager.RemoveListener<RadialActionMenuInactiveMessage>(OnRadialActionMenuInactiveMessage);
            
            GameManager.RemoveListener<PartyMemberActionChosenMessage>(OnPartyMemberActionChosenMessage);
            
            GameManager.RemoveListener<ChooseTimelinePlacementMessage>(OnChooseTimelinePlacementMessage);
            GameManager.RemoveListener<TimelinePlacementEscapedMessage>(OnTimelineMenuInactiveMessage);
            
            selectInputAction.action.performed -= OnSelectPerformed;
            selectInputAction.action.Disable();
            
            backInputAction.action.performed -= OnBackPerformed;
            backInputAction.action.Disable();
            
            navigateInputAction.action.Disable();
        }
    }
}