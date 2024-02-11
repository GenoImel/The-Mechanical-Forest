using Akashic.Core;
using Akashic.Runtime.MonoSystems.Audio;
using Akashic.Runtime.MonoSystems.PlayerPrefs;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.OptionsMenu
{
    internal sealed class SoundSettingsContainer : MonoBehaviour
    {
        [Header("Volume Sliders")] 
        [SerializeField] private Slider masterVolumeSlider;
        
        [SerializeField] private Slider musicVolumeSlider;

        [SerializeField] private Slider effectsVolumeSlider;
        
        private IPlayerPreferencesMonoSystem playerPreferencesMonoSystem;
        private IAudioMonoSystem audioMonoSystem;

        private void Awake()
        {
            playerPreferencesMonoSystem = GameManager.GetMonoSystem<IPlayerPreferencesMonoSystem>();
            audioMonoSystem = GameManager.GetMonoSystem<IAudioMonoSystem>();
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void OnMasterVolumeChanged(float value)
        {
            audioMonoSystem.SetMasterVolume(value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            audioMonoSystem.SetMusicVolume(value);
        }

        private void OnEffectsVolumeChanged(float value)
        {
            audioMonoSystem.SetEffectsVolume(value);
        }
        
        private void OnPlayerPreferencesLoadedMessage(PlayerPreferencesLoadedMessage message)
        {
            masterVolumeSlider.value = playerPreferencesMonoSystem.GetMasterVolume();
            musicVolumeSlider.value = playerPreferencesMonoSystem.GetMusicVolume();
            effectsVolumeSlider.value = playerPreferencesMonoSystem.GetEffectsVolume();
        }
        
        private void AddListeners()
        {
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
            
            GameManager.AddListener<PlayerPreferencesLoadedMessage>(OnPlayerPreferencesLoadedMessage);
        }
        
        private void RemoveListeners()
        {
            musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            effectsVolumeSlider.onValueChanged.RemoveListener(OnEffectsVolumeChanged);
            
            GameManager.RemoveListener<PlayerPreferencesLoadedMessage>(OnPlayerPreferencesLoadedMessage);
        }
    }
}