using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.BattleStates
{
    internal sealed class BattleState : IState
    {
        public Type GetFiniteStateType()
        {
            return typeof(BattleFiniteState);
        }
    }
}