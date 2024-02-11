using Akashic.Core.MonoSystems;
using UnityEngine;
using FMODUnity;

namespace Akashic.Runtime.MonoSystems.Audio 
{
    /// <Summary>
    /// MonoSystem responsible for playing music and sound effects.
    /// </Summary>
    internal interface IAudioMonoSystem : IMonoSystem 
    {
        /// <summary>
        /// Plays a music track.
        /// </summary>
        public void PlayMusic(EventReference musicEventReference);

        /// <summary>
        /// Stops playing the current music track.
        /// </summary>
        public void StopMusic();

        /// <summary>
        /// Plays and ambience track.
        /// </summary>
        public void PlayAmbience(EventReference ambienceEventReference);

        /// <summary>
        /// Stops playing the current ambience track.
        /// </summary>
        public void StopAmbience();

        /// <summary>
        /// Sets a parameter for the currently playing ambience track.
        /// </summary>
        /// <param name="parameterName">Name of the parameter from the FMOD
        /// project for this ambience track.</param>
        /// <param name="parameterValue">Value of the parameter from the FMOD
        /// project for this ambience track.</param>
        public void SetAmbienceParameter(string parameterName, float parameterValue);
        
        /// <summary>
        /// Plays a sound effect. Will interrupt currently playing sound effects if
        /// </summary>
        public void PlayOneShot(EventReference sound, Vector3 worldPosition);

        
        /// <summary>
        /// Sets the volume for the master audio bus in FMOD.
        /// </summary>
        public void SetMasterVolume(float volume);

        /// <summary>
        /// Sets the volume for the music audio bus in FMOD.
        /// </summary>
        public void SetMusicVolume(float volume);

        /// <summary>
        /// Sets the volume for the sound effects audio bus in FMOD.
        /// </summary>
        public void SetEffectsVolume(float volume);
    }
}
