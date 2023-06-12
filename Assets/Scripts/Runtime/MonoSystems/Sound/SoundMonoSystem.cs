using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Sound 
{
    internal sealed class SoundMonoSystem : MonoBehaviour, ISoundMonoSystem 
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource genericAudioHandler;
        
        [SerializeField] private AudioSource musicAudioSource;
        
        private float globalVolume = 1f;
        
        private float globalMusicVolume = 1f;
        
        private void OnEnable() 
        {
            GameManager.AddListener<UpdateSoundSettingsMessage>(OnUpdateSoundSettingsMessage);
        }

        private void OnDisable() 
        {
            GameManager.RemoveListener<UpdateSoundSettingsMessage>(OnUpdateSoundSettingsMessage);
        }

        private void Start() 
        {    
            genericAudioHandler.volume = globalVolume;
            musicAudioSource.volume = globalMusicVolume;
        }

        public void PlaySound(AudioClip clip, bool overrideAudio = false) 
        {
            if (overrideAudio) 
            {
                genericAudioHandler.Stop();

                return;
            }

            genericAudioHandler.PlayOneShot(clip);
        }

        public void StopSound() 
        {
            genericAudioHandler.Stop();
        }

        public void PlayMusic(AudioClip clip, bool loop = true) 
        {
            musicAudioSource.loop = loop;
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public void StopMusic() 
        {
            musicAudioSource.Stop();
        }

        /// <Summary>
        /// Function that listens for when the Global Volume Settings gets updated
        /// </Summary>
        private void OnUpdateSoundSettingsMessage(UpdateSoundSettingsMessage newSettingsMessage) 
        {
            globalVolume = newSettingsMessage.SoundEffectsVolume;
            genericAudioHandler.volume = globalVolume;

            globalMusicVolume = newSettingsMessage.MusicVolume;
            musicAudioSource.volume = globalMusicVolume;
        }
    }
}
