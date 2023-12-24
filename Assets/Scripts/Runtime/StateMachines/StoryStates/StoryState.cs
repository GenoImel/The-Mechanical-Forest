using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.StoryStates
{
    internal sealed class StoryState : IState
    {
        public Type GetFiniteStateType()
        {
            return typeof(StoryFiniteState);
        }
    }
}