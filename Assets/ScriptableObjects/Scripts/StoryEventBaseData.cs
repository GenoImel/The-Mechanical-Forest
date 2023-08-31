using System.Collections.Generic;
using UnityEngine;
using Akashic.Runtime.MonoSystems.Dialogue;

namespace Akashic.ScriptableObjects.Scripts
{
    [CreateAssetMenu(menuName = "Akashic/Story Event/New Base Story Event")]
    internal sealed class StoryEventBaseData : ScriptableObject
    {
        public List<StoryPoint> storyPoints;
    }
}
