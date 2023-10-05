using System.Collections.Generic;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryEvent
    {
        public List<StoryPoint> storyPoints = new List<StoryPoint>();

        public StoryEvent(List<StoryPoint> points)
        {
            storyPoints = points;
        }
    }
}