using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Sound 
{
    /// <Summary>
    /// MonoSystem responsible for playing music and sound effects.
    /// </Summary>
    internal interface ISoundMonoSystem : IMonoSystem 
    {
    	/// <Summary>
        /// Plays a sound effect.
        /// </Summary>
        public void PlaySound(AudioClip clip, bool overrideAudio = false);

        /// <Summary>
        /// Stops playing all sound effects.
        /// </Summary>
        public void StopSound();

        /// <Summary>
        /// Plays music. <paramref name="loop"/> is set to true by default.
        /// </Summary>
        public void PlayMusic(AudioClip clip, bool loop = true);

        /// <Summary>
        /// Stops the current music from playing.
        /// </Summary>
        public void StopMusic(); 
    }
}
