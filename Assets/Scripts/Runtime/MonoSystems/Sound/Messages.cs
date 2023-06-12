using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Sound 
{
	/// <summary>
    /// Published when sound settings are changed in the options menu.
    /// </summary>
    internal sealed class UpdateSoundSettingsMessage : IMessage 
    {
        public float SoundEffectsVolume { get; }
        public float MusicVolume { get; }

        public UpdateSoundSettingsMessage(float soundEffectsVolume, float musicVolume) 
        {
            SoundEffectsVolume = Mathf.Clamp(soundEffectsVolume, 0.01f, soundEffectsVolume);
            MusicVolume = Mathf.Clamp(musicVolume, 0.01f, musicVolume);
        }
    }
}
