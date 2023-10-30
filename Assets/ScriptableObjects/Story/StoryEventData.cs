using System.Collections.Generic;
using Akashic.Runtime.MonoSystems.Story;
using UnityEngine;

namespace Akashic.ScriptableObjects.Story
{
    [CreateAssetMenu(menuName = "Akashic/Story Event/New Story Event")]
    internal sealed class StoryEventData : ScriptableObject
    {
        public bool allowLog = false;
        public List<StoryPoint> storyPoints;
    }
}