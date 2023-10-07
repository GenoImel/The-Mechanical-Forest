using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.ExplorationStates
{
    internal sealed class ExplorationState : IState
    {
        public Type GetFiniteStateType()
        {
            return typeof(ExplorationFiniteState);
        }
    }
}