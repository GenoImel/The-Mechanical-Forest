using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.Actors.Battle.Planning
{
    internal abstract class PlannerActorFiniteState : IFiniteState
    {
        public Type GetFiniteStateType()
        {
            return typeof(PlannerActorFiniteState);
        }

        public abstract void Execute();
    }
    

}