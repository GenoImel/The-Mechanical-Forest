using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.Controllers.OptionsMenu;
using Akashic.Runtime.MonoSystems.PlayerPrefs;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Audio 
{
     internal sealed class AudioMonoSystem : MonoBehaviour, IAudioMonoSystem
    {
        [Header("Sound Settings")] 
        [Range(0, 1)]
        [SerializeField] private float masterVolume = 0.5f;
        
        [Range(0, 1)]
        [SerializeField] private float effectsVolume = 0.75f;
        
        [Range(0, 1)]
        [SerializeField] private float musicVolume = 0.75f;
        
        private AudioClip currentMusicClip;

        private Bus masterBus;
        private Bus sfxBus;
        private Bus musicBus;
        
        private List<EventInstance> eventInstances;
        private List<StudioEventEmitter> eventEmitters;

        private EventInstance ambienceEventInstance;
        private EventInstance musicEventInstance;
        
        private IPlayerPreferencesMonoSystem playerPreferencesMonoSystem;

        private void Awake()
        {
            playerPreferencesMonoSystem = GameManager.GetMonoSystem<IPlayerPreferencesMonoSystem>();
            
            eventInstances = new List<EventInstance>();
            eventEmitters = new List<StudioEventEmitter>();
        }
        
        private void Start()
        {
            masterBus = RuntimeManager.GetBus("bus:/");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");
            musicBus = RuntimeManager.GetBus("bus:/Music");
        }

        private void OnEnable()
        {
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }
        
        public void PlayMusic(EventReference musicEventReference) 
        {
            musicEventInstance = CreateInstance(musicEventReference);
            musicEventInstance.start();
        }
        
        public void StopMusic() 
        {
            musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        
        /*public void SetMusicArea(MusicArea area)
        {
        // wow the tutorial I followed has such terrible coding practices lol
        // TODO: make this scalable and not hard-coded or reliant on enums
            musicEventInstance.setParameterByName("area", (float) area);
        }*/
        
        public void PlayAmbience(EventReference ambienceEventReference)
        {
            ambienceEventInstance = CreateInstance(ambienceEventReference);
            ambienceEventInstance.start();
        }
        
        public void StopAmbience() 
        {
            ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        public void SetAmbienceParameter(string parameterName, float parameterValue)
        {
            ambienceEventInstance.setParameterByName(parameterName, parameterValue);
        }

        public void PlayOneShot(EventReference sound, Vector3 worldPosition) 
        {
            RuntimeManager.PlayOneShot(sound, worldPosition);
        }
        
       // TODO: need a way to track all one shot sounds and destroy them when they're done playing
       // this will let us choose to stop them if we want to

        public void SetMasterVolume(float volume)
        {
            masterVolume = volume;
            masterBus.setVolume(masterVolume);
        }
        
        public void SetMusicVolume(float volume)
        {
            musicVolume = volume;
            musicBus.setVolume(musicVolume);
        }

        public void SetEffectsVolume(float volume)
        {
            effectsVolume = volume;
            sfxBus.setVolume(effectsVolume);
        }

        private void InitializeVolumeSettings()
        {
            masterVolume = playerPreferencesMonoSystem.GetMasterVolume();
            musicVolume = playerPreferencesMonoSystem.GetMusicVolume();
            effectsVolume = playerPreferencesMonoSystem.GetEffectsVolume();
            
            masterBus.setVolume(masterVolume);
            musicBus.setVolume(musicVolume);
            sfxBus.setVolume(effectsVolume);
        }
        
        public EventInstance CreateInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstances.Add(eventInstance);
            return eventInstance;
        }
        
        public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, StudioEventEmitter eventEmitter)
        {
            eventEmitter.EventReference = eventReference;
            eventEmitters.Add(eventEmitter);
            return eventEmitter;
        }
        
        private void CleanUp()
        {
            foreach (var eventInstance in eventInstances)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }

            foreach (var emitter in eventEmitters)
            {
                emitter.Stop();
            }
        }

        private void OnDestroy()
        {
            CleanUp();
        }
        
        private void OnSettingsMenuClosedMessage(OptionsMenuClosedMessage message)
        {
            playerPreferencesMonoSystem.UpdateSoundPreferences(masterVolume, musicVolume, effectsVolume);
        }
        
        private void OnPlayerPreferencesLoadedMessage(PlayerPreferencesLoadedMessage message)
        {
            InitializeVolumeSettings();
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
