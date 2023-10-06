using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal interface IStoryMonoSystem : IMonoSystem
    {
        public StoryPoint GetCurrentStoryPoint();
    }
}