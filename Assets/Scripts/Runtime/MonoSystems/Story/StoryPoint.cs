using System;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Story
{
    [Serializable]
    internal sealed class StoryPoint
    {
        public string characterName;
        public string dialogueLine;
        public Sprite profilePicture;
        public Sprite backgroundImage;
        public AudioSource music;
    }
}