using System.Collections.Generic;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryEvent
    {
        public bool allowLog;
        public List<StoryPoint> storyPoints = new List<StoryPoint>();

        public StoryEvent(List<StoryPoint> points, bool log = true)
        {
            allowLog = log;
            storyPoints = points;
        }
    }
}