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
        
        [Header("Components")]
        [SerializeField] private AudioSource genericAudioHandler;
        [SerializeField] private AudioSource musicAudioSource;

        [Header("General")]
        public float globalVolume = 1f;
        public float globalMusicVolume = 1f;

        private void Start() 
        {    
            genericAudioHandler.volume = globalVolume;
            musicAudioSource.volume = globalMusicVolume;
        }

        private void OnEnable() {
            GameManager.AddListener<UpdateGlobalSoundMessage>(OnMasterVolumeChange);
        }

        private void OnDisable() {
            GameManager.RemoveListener<UpdateGlobalSoundMessage>(OnMasterVolumeChange);
        }

        public void PlaySound(AudioClip clip, bool overrideAudio = false) 
        {
            // cancels all other audio
            if (overrideAudio) 
            {
                genericAudioHandler.Stop();

                return;
            }

            genericAudioHandler.PlayOneShot(clip);
        }

        public void StopAudio() 
        {
            genericAudioHandler.Stop();
        }

        public void PlayMusic(AudioClip clip, bool loop = true) 
        {
            musicAudioSource.loop = loop;
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public void StopMucic() 
        {
            musicAudioSource.Stop();
        }

        /// <Summary>
        /// Function that listens for when the Global Volume Settings gets updated
        /// </Summary>
        private void OnMasterVolumeChange(UpdateGlobalSoundMessage newSettings) 
        {
            // updates local values to global settings
            globalVolume = newSettings.GlobalVolume;
            genericAudioHandler.volume = globalVolume;

            globalMusicVolume = newSettings.GlobalMusicVolume;
            musicAudioSource.volume = globalMusicVolume;
        }
    }
}
