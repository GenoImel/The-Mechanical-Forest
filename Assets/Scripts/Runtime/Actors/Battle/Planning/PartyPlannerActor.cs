using System;
using System.Collections.Generic;
using System.Linq;
using Akashic.Core;
using Akashic.Core.StateMachines;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Enemy;
using Akashic.Runtime.Actors.Battle.Party;
using Akashic.Runtime.MonoSystems.Battle;
using Akashic.Runtime.StateMachines.TurnStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Akashic.Runtime.Actors.Battle.Planning
{
    internal sealed class PartyPlannerActor : MonoBehaviour
    {
        [Header("Player Input")]
        [SerializeField] private InputActionReference navigateInputAction;

        [SerializeField] private InputActionReference selectInputAction;
        
        [Header("Settings")]
        [SerializeField] private float inputDelaySeconds = 0.25f;
        
        private static List<PartyBattleActor> partyBattleActors;
        
        private static List<EnemyBattleActor> enemyBattleActors;
        
        private static List<BattleActor> allBattleActors = new();
        
        private static int partyIndex;

        private static int entityIndex;

        private static int axesDirection;
        
        private static float delayCounterSeconds;
        
        private PlannerActorFiniteState currentState;
        private PlannerActorFiniteState prevState;

        private IPartyBattleMonoSystem partyBattleMonoSystem;
        private IEnemyBattleMonoSystem enemyBattleMonoSystem;
        private ITurnStateMachine turnStateMachine;

        private void Awake()
        {
            partyBattleMonoSystem = GameManager.GetMonoSystem<IPartyBattleMonoSystem>();
            enemyBattleMonoSystem = GameManager.GetMonoSystem<IEnemyBattleMonoSystem>();
            turnStateMachine = GameManager.GetStateMachine<ITurnStateMachine>();
        }

        public void Start()
        {
            currentState = new Inactive();
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
                
                axesDirection = GetAxisValueAsInt(navigateInputAction.action.ReadValue<Vector2>().x);

                if (axesDirection == 0)
                {
                    return;
                }
                
                currentState.Execute();
            }
        }
        
        private int GetAxisValueAsInt(float value)
        {
            if (value == 0f)
            {
                return 0;
            }
            
            return value < 0 ? -1 : 1;
        }
        
        private sealed class Inactive : PlannerActorFiniteState
        {
            public override void Execute()
            {

            }
        }
        
        private sealed class AllEntitiesSelectable : PlannerActorFiniteState
        {
            public override void Execute()
            {
                EntityIndexChanged(axesDirection);
            }
        }
        
        private sealed class PartyMembersSelectable : PlannerActorFiniteState
        {
            public override void Execute()
            {
                
            }
        }
        
        private class EnemiesSelectable : PlannerActorFiniteState
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
        
        private static void EntityIndexChanged(int direction)
        {
            entityIndex = (entityIndex + direction + allBattleActors.Count) % allBattleActors.Count;
            
            SelectEntity(entityIndex);
            
            delayCounterSeconds = 0;
        }
        
        private static void SelectEntity(int index)
        {
            var selectedEntity = allBattleActors.ElementAt(index);
            GameManager.Publish(new SetBattleActorSelectedMessage(selectedEntity));
        }

        private void SetState(PlannerActorFiniteState nextState)
        {
            if(nextState is null)
            {
                throw new ArgumentException("Invalid state type");
            }
            
            if (currentState is Hold)
            {
                return;
            }

            CleanUp();

            if (nextState is PartyMembersSelectable)
            {
                
            }
            
            if (nextState is EnemiesSelectable)
            {
                
            }
            
            if (nextState is AllEntitiesSelectable)
            {
                
            }

            prevState = currentState;
            currentState = nextState;
        }

        private void CleanUp()
        {
            GameManager.Publish(new DeselectAllBattleActorsMessage());
            //reset selection index?
        }
        
        private void OnTurnStateChangedMessage(TurnStateChangedMessage message)
        {
            if (message.NextState is not TurnFiniteState.PartyPlanning)
            {
                currentState = new Inactive();
            }
            
            if (message.NextState is TurnFiniteState.PartyPlanning)
            {
                currentState = new AllEntitiesSelectable();
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
        
        private void AddListeners()
        {
            GameManager.AddListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
            
            navigateInputAction.action.Enable();
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
            
            navigateInputAction.action.Disable();
        }
    }
}