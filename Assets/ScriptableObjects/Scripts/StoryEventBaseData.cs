using System;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.Scripts
{
    [CreateAssetMenu(menuName = "Akashic/Story Event/New Base Story Event")]
    internal sealed class StoryEventBaseData : ScriptableObject
    {
        public List<StoryPoint> storyPoints;

        [Serializable]
        public class StoryPoint
        {
            public string dialogueLine;

            public Sprite profilePicture;

            public Sprite backgroundImage;

            public AudioSource music;
        }
    }
}
