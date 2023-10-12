using Akashic.Core.Messages;
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

    internal sealed class StoryPointAvailableMessage : IMessage
    {
    }

    internal sealed class StoryEventEndedMessage : IMessage
    {
    }

    internal sealed class ToggleEventLog : IMessage
    {
        public bool allowLog { get; }

        public ToggleEventLog(bool log)
        {
            allowLog = log;
        }
    }

    internal sealed class StoryEventLogOpened : IMessage
    {
    }

    internal sealed class StoryEventLogClosed : IMessage
    {
    }

    internal sealed class DialogueEntryAvailableMessage: IMessage
    {
        public StoryPoint StoryPoint { get; }

        public DialogueEntryAvailableMessage(StoryPoint storyPoint)
        {
            StoryPoint = storyPoint;
        }
    }
}
