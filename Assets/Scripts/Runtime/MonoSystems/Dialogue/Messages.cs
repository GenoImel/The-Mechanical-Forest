using Akashic.Core;
using Akashic.ScriptableObjects.Scripts;

namespace Akashic.Runtime.MonoSystems.Dialogue
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
