using Akashic.Core;
using Akashic.ScriptableObjects.StoryBase;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class NewStoryEventMessage : IMessage
    {
        public StoryEventBaseData StoryEventBaseData { get; }

        public NewStoryEventMessage(StoryEventBaseData storyEventBaseData)
        {
            StoryEventBaseData = storyEventBaseData;
        }
    }

    internal sealed class StoryEventAvailableMessage : IMessage
    {
    }
}
