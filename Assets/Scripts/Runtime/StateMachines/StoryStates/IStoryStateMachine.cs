using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.StoryStates
{
    internal interface IStoryStateMachine : IStateMachine
    {
        public void SetInactiveState();

        public void SetActiveState();
    }
}
