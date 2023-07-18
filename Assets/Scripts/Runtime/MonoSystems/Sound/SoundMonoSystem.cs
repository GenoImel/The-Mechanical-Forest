using Akashic.Core;
using Akashic.Runtime.Controllers.OptionsMenu;
using Akashic.Runtime.MonoSystems.PlayerPrefs;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Sound 
{
     internal sealed class SoundMonoSystem : MonoBehaviour, ISoundMonoSystem
    {
        [Header("Audio Sources")] 
        [SerializeField] private AudioSource effectsAudioSource;

        [SerializeField] private AudioSource musicAudioSource;
        
        [Header("Sound Settings")] 
        [Range(0, 1)]
        [SerializeField] private float effectsVolume = 0.75f;
        
        [Range(0, 1)]
        [SerializeField] private float musicVolume = 0.75f;
        
        private IPlayerPreferencesMonoSystem playerPreferencesMonoSystem;

        private void Awake()
        {
            playerPreferencesMonoSystem = GameManager.GetMonoSystem<IPlayerPreferencesMonoSystem>();
        }

        private void OnEnable()
        {
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }
        
        public void PlayMusic(AudioClip clip, bool loop = true) 
        {
            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
            }
            
            musicAudioSource.loop = loop;
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public void StopMusic() 
        {
            musicAudioSource.Stop();
        }
        
        public void PlaySound(AudioClip clip, bool overrideAudio = false) 
        {
            if (overrideAudio) 
            {
                effectsAudioSource.Stop();
            }

            effectsAudioSource.PlayOneShot(clip);
        }

        public void StopSound() 
        {
            effectsAudioSource.Stop();
        }
        
        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
            SetAudioSourceVolumeLevels();
        }

        public void SetEffectsVolume(float volume)
        {
            effectsVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
            SetAudioSourceVolumeLevels();
        }

        private void SetAudioSourceVolumeLevels()
        {
            musicAudioSource.volume = playerPreferencesMonoSystem.GetMusicVolume();
            effectsAudioSource.volume = playerPreferencesMonoSystem.GetEffectsVolume();
        }
        
        private void OnSettingsMenuClosedMessage(OptionsMenuClosedMessage message)
        {
            playerPreferencesMonoSystem.UpdateSoundPreferences(musicVolume, effectsVolume);
        }
        
        private void OnPlayerPreferencesLoadedMessage(PlayerPreferencesLoadedMessage message)
        {
            SetAudioSourceVolumeLevels();
        }
        
        private void AddListeners()
        {
            GameManager.AddListener<OptionsMenuClosedMessage>(OnSettingsMenuClosedMessage);
            GameManager.AddListener<PlayerPreferencesLoadedMessage>(OnPlayerPreferencesLoadedMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<OptionsMenuClosedMessage>(OnSettingsMenuClosedMessage);
            GameManager.RemoveListener<PlayerPreferencesLoadedMessage>(OnPlayerPreferencesLoadedMessage);
        }
    }
}
