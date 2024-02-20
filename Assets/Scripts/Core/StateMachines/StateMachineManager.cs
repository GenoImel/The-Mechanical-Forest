using System;
using System.Collections.Generic;

namespace Akashic.Core.StateMachines
{
    internal sealed class StateMachineManager
    {
        private readonly IDictionary<Type, IStateMachine> stateMachines =
            new Dictionary<Type, IStateMachine>();
        
        public IDictionary<Type, IStateMachine> StateMachines => stateMachines;

        public void AddStateMachine<TState, TBindTo>(TState stateMachine)
            where TState : TBindTo, IStateMachine
        {
            if (stateMachine == null)
            {
                throw new NullReferenceException($"{nameof(stateMachine)} cannot be null");
            }

            var stateMachineType = typeof(TBindTo);
            
            if (stateMachines.ContainsKey(stateMachineType))
            {
                throw new Exception($"State Machine of type {stateMachineType} already exists.");
            }
            
            stateMachines[stateMachineType] = stateMachine;
        }
        
        public void AddStateMachine(IStateMachine stateMachine, Type bindToType)
        {
            if (stateMachine == null || bindToType == null)
            {
                throw new NullReferenceException($"{nameof(stateMachine)} cannot be null");
            }
    
            if (stateMachines.ContainsKey(bindToType))
            {
                throw new Exception($"State Machine of type {bindToType} already exists");
            }
    
            stateMachines[bindToType] = stateMachine;
        }
        
        public void RemoveStateMachine<TState, TBindTo>(TState stateMachine)
        where TState : TBindTo, IStateMachine
        {
            if (stateMachine == null)
            {
                throw new NullReferenceException($"{nameof(stateMachine)} cannot be null");
            }

            var stateType = typeof(TBindTo);
            
            if(!stateMachines.ContainsKey(stateType))
            {
                throw new Exception($"State Machine {stateType} not found. Cannot remove it.");
            }
            
            stateMachines.Remove(stateType);
        }
        
        public void RemoveStateMachine(IStateMachine stateMachine, Type bindToType)
        {
            if (stateMachine == null || bindToType == null)
            {
                throw new NullReferenceException($"{nameof(stateMachine)} cannot be null");
            }
    
            if (!stateMachines.ContainsKey(bindToType))
            {
                throw new Exception($"State Machine {bindToType} not found. Cannot remove it.");
            }
    
            stateMachines.Remove(bindToType);
        }

        public TState GetStateMachine<TState>()
        {
            var stateMachineType = typeof(TState);
            if (stateMachines.TryGetValue(stateMachineType, out var stateMachine))
            {
                return (TState)stateMachine;
            }

            throw new Exception($"State Machine {stateMachineType} does not exist");
        }
    }
}