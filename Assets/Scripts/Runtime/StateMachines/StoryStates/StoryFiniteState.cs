using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.StoryStates
{
    internal class StoryFiniteState : IFiniteState
    {
        public Type GetStateType()
        {
            return typeof(StoryState);
        }

        internal sealed class Inactive : StoryFiniteState
        {
        }

        internal sealed class Active : StoryFiniteState
        {
        }
    }
}
