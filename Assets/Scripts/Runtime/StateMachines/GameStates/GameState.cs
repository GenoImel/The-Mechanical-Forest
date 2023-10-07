using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.GameStates
{
    internal sealed class GameState : IState
    {
        public Type GetFiniteStateType()
        {
            return typeof(GameFiniteState);
        }
    }
}