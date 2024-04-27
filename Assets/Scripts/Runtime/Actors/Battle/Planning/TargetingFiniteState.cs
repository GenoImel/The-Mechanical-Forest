using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.Actors.Battle.Planning
{
    internal abstract class TargetingFiniteState : IFiniteState
    {
        public Type GetFiniteStateType()
        {
            return typeof(TargetingFiniteState);
        }
        
        public abstract void ExecuteTargetSelection();
        
        public abstract void ExecuteTargetConfirmation();
    }
}