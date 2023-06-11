using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akashic.Core;
using System;

namespace Akashic.Runtime.MonoSystems.SoundManagement 
{

    /// <Summary>
    /// MonoSystem responsible for handling all sound output
    /// </Summary>
    public class SoundMonoSystem : MonoBehaviour, ISoundMonoSystem 
    {

        private AudioSource genericAudioHandler;
        private AudioSource musicAudioSource;

        [Header("General")]
        public float globalVolume = 1f;
        public float globalMusicVolume = 1f;

        public static SoundMonoSystem instance;

        // Action responding to Global sound variable change
        private Action<UpdateGlobalSoundMessage> OnSettingsChange;

        private void Start() 
        {
            // sets up a singleton
            if (instance != null) 
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            
            genericAudioHandler = this.gameObject.AddComponent<AudioSource>();

            // Hooking the music up to a separate AudioSource prevents any overlap
            // or accidental music cancelling if we wanted to stop all sound effects
            musicAudioSource = this.gameObject.AddComponent<AudioSource>();

            //  I Don't know how the save system is going to work, but here is just a reference
            //  to how it could be set up
            //
            // float savedGlovalVolume = SaveSystem.GetSavedOptions().globalVolume;
            // float savedMusicVolume = SaveSystem.GetSavedOptions().globalMusicVolume;
            // globalVolume = savedGlovalVolume;
            // globalMusicVolume = savedMusicVolume
            genericAudioHandler.volume = globalVolume;
            musicAudioSource.volume = globalMusicVolume;

            // Listens for the sound event message
            OnSettingsChange += OnMasterVolumeChange;
            GameManager.AddListener(OnSettingsChange);
        }

        /// <Summary>
        /// Responsible for Playing Sound Effects
        /// NOTE - Has optional parameter "overrideAudio" that will cancel 
        /// all other sound bytes currently playing
        /// </Summary>
        public static void PlaySound(AudioClip clip, bool overrideAudio = false) 
        {
            // cancels all other audio
            if (overrideAudio) 
            {
                instance.genericAudioHandler.Stop();

                return;
            }

            instance.genericAudioHandler.PlayOneShot(clip);
        }

        /// <Summary>
        /// Stops all Sound Effects Currently Playing
        /// NOTE - Music should be stopped with the "StopMusic" function
        /// </Summary>
        public static void StopAudio() 
        {
            instance.genericAudioHandler.Stop();
        }

        /// <Summary>
        /// Plays Music
        /// NOTE - Has optional parameter "loop" that loops the music
        /// and is set to 'true' by default
        /// </Summary>
        public static void PlayMusic(AudioClip clip, bool loop = true) 
        {
            instance.musicAudioSource.loop = loop;
            instance.musicAudioSource.clip = clip;
            instance.musicAudioSource.Play();
        }

        /// <Summary>
        /// Stops all music currently playing
        /// </Summary>
        public static void StopMucic() 
        {
            instance.musicAudioSource.Stop();
        }

        /// <Summary>
        /// Function that listens for when the Global Volume Settings gets updated
        /// </Summary>
        private void OnMasterVolumeChange(UpdateGlobalSoundMessage newSettings) 
        {
            // updates local values to global settings
            globalVolume = newSettings.globalVolume;
            genericAudioHandler.volume = globalVolume;

            globalMusicVolume = newSettings.globalMusicVolume;
            musicAudioSource.volume = globalMusicVolume;
        }

        // implements 'ISoundMonoSystem'

        /// <Summary>
        /// Returns the SoundMonoSystem Component
        /// </Summary>
        public SoundMonoSystem GetComponent() {
            return this;
        }
    }
}
