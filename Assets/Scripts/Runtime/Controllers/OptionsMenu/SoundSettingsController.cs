using Akashic.Core;
using Akashic.Runtime.MonoSystems.PlayerPrefs;
using Akashic.Runtime.MonoSystems.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.OptionsMenu
{
    internal sealed class SoundSettingsController : MonoBehaviour
    {
        [Header("Volume Sliders")] 
        [SerializeField] private Slider musicVolumeSlider;

        [SerializeField] private Slider effectsVolumeSlider;
        
        private IPlayerPreferencesMonoSystem playerPreferencesMonoSystem;
        private ISoundMonoSystem soundMonoSystem;

        private void Awake()
        {
            playerPreferencesMonoSystem = GameManager.GetMonoSystem<IPlayerPreferencesMonoSystem>();
            soundMonoSystem = GameManager.GetMonoSystem<ISoundMonoSystem>();
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnMusicVolumeChanged(float value)
        {
            soundMonoSystem.SetMusicVolume(value);
        }

        private void OnEffectsVolumeChanged(float value)
        {
            soundMonoSystem.SetEffectsVolume(value);
        }
        
        private void OnPlayerPreferencesLoadedMessage(PlayerPreferencesLoadedMessage message)
        {
            musicVolumeSlider.value = playerPreferencesMonoSystem.GetMusicVolume();
            effectsVolumeSlider.value = playerPreferencesMonoSystem.GetEffectsVolume();
        }
        
        private void AddListeners()
        {
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