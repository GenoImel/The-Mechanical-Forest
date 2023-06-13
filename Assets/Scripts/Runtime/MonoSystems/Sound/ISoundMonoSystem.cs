using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Sound 
{
    /// <Summary>
    /// MonoSystem responsible for playing music and sound effects.
    /// </Summary>
    internal interface ISoundMonoSystem : IMonoSystem 
    {
        /// <summary>
        /// Plays a music track. Will loop if <paramref name="loop"/> is true.
        /// </summary>
        public void PlayMusic(AudioClip clip, bool loop = true);

        /// <summary>
        /// Stops playing the current music track.
        /// </summary>
        public void StopMusic();
        
        /// <summary>
        /// Plays a sound effect. Will interrupt currently playing sound effects if
        /// <paramref name="overrideAudio"/> is true, and otherwise will play the
        /// <paramref name="clip"/> concurrently if <paramref name="overrideAudio"/>
        /// is false.
        /// </summary>
        public void PlaySound(AudioClip clip, bool overrideAudio = false);

        /// <summary>
        /// Stops all currently playing sound effects.
        /// </summary>
        public void StopSound();

        /// <summary>
        /// Sets the volume for the music audio source.
        /// </summary>
        public void SetMusicVolume(float volume);

        /// <summary>
        /// Sets the volume for the sound effects audio source.
        /// </summary>
        public void SetEffectsVolume(float volume);
    }
}
