using System.Collections.Generic;
using Akashic.Runtime.MonoSystems.Story;
using UnityEngine;

namespace Akashic.ScriptableObjects.StoryBase
{
    [CreateAssetMenu(menuName = "Akashic/Story Event/New Base Story Event")]
    internal sealed class StoryEventBaseData : ScriptableObject
    {
        public List<StoryPoint> storyPoints;
    }
}