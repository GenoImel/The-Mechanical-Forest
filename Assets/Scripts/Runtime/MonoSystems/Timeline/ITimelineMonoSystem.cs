using System.Collections.Generic;
using Akashic.Core.MonoSystems;

namespace Akashic.Runtime.MonoSystems.Timeline
{
    internal interface ITimelineMonoSystem : IMonoSystem
    {
        public List<TimelineMove> TimelineMoves { get; }
    }
}