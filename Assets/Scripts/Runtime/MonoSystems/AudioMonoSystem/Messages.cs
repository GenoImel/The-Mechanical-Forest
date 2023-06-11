using UnityEngine;
using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.SoundManagement 
{
	/// <summary>
    /// Holds Sound Data to be passed onto the built in Message Interface
    /// </summary>
    internal sealed class UpdateGlobalSoundMessage : IMessage 
    {
        public float globalVolume;
        public float globalMusicVolume;

        public UpdateGlobalSoundMessage(float _globalVolume, float _globalMusicVolume) 
        {
            globalVolume = Mathf.Clamp(_globalVolume, 0.01f, _globalVolume);
            globalMusicVolume = Mathf.Clamp(_globalMusicVolume, 0.01f, _globalMusicVolume);
        }
    }
}
