using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.SoundManagement 
{
    /// <Summary>
    /// Interface responsible for makeing the 'SoundMonoSystem' script a MonoSystem
    /// </Summary>
    internal interface ISoundMonoSystem : IMonoSystem {
    	/// <Summary>
        /// Responsible for Playing Sound Effects
        /// NOTE - Has optional parameter "overrideAudio" that will cancel 
        /// all other sound bytes currently playing
        /// </Summary>
        public void PlaySound(AudioClip clip, bool overrideAudio = false);

        /// <Summary>
        /// Stops all Sound Effects Currently Playing
        /// NOTE - Music should be stopped with the "StopMusic" function
        /// </Summary>
        public void StopAudio();

        /// <Summary>
        /// Plays Music
        /// NOTE - Has optional parameter "loop" that loops the music
        /// and is set to 'true' by default
        /// </Summary>
        public void PlayMusic(AudioClip clip, bool loop = true);

        /// <Summary>
        /// Stops all music currently playing
        /// </Summary>
        public void StopMucic(); 
    }
}
