using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.TurnStates
{
    internal class TurnFiniteState : IFiniteState
    {
        public Type GetFiniteStateType()
        {
            return typeof(TurnFiniteState);
        }

        internal sealed class Initializing : TurnFiniteState
        {
            
        }
        
        internal sealed class Promise : TurnFiniteState
        {
            
        }
        
        internal sealed class EnemyPlanning : TurnFiniteState
        {
            
        }
        
        internal sealed class PartyPlanning : TurnFiniteState
        {
            
        }
        
        internal sealed class Execution : TurnFiniteState
        {
            
        }
        
        internal sealed class PostExecution : TurnFiniteState
        {
            
        }
        
        internal sealed class None : TurnFiniteState
        {
            
        }
    }
}