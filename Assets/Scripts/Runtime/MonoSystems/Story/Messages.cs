using Akashic.Core;
using Akashic.ScriptableObjects.Scripts;
using Akashic.ScriptableObjects.Scripts.StoryBase;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class DialogueStoryEventMessage : IMessage
    {
        public StoryEventBaseData StoryEvent { get; }

        public DialogueStoryEventMessage(StoryEventBaseData storyEvent)
        {
            StoryEvent = storyEvent;
        }
    }
}
