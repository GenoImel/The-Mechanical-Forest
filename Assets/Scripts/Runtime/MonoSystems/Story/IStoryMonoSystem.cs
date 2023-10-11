using Akashic.Core.MonoSystems;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal interface IStoryMonoSystem : IMonoSystem
    {
        public StoryPoint GetCurrentStoryPoint();
        public void AdvanceStoryPoint();
    }
}