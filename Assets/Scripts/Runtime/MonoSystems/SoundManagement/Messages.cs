using UnityEngine;
using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.SoundManagement 
{
	/// <summary>
    /// Holds Sound Data to be passed onto the built in Message Interface
    /// </summary>
    internal sealed class UpdateGlobalSoundMessage : IMessage 
    {
        public float GlobalVolume { get; private set; }
        public float GlobalMusicVolume { get; private set; }

        public UpdateGlobalSoundMessage(float _globalVolume, float _globalMusicVolume) 
        {
            GlobalVolume = Mathf.Clamp(_globalVolume, 0.01f, _globalVolume);
            GlobalMusicVolume = Mathf.Clamp(_globalMusicVolume, 0.01f, _globalMusicVolume);
        }
    }
}
