using System;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Dialogue
{
    [Serializable]
    internal sealed class StoryPoint
    {
        public string dialogueLine;
        public Sprite profilePicture;
        public Sprite backgroundImage;
        public AudioSource music;
    }
}
