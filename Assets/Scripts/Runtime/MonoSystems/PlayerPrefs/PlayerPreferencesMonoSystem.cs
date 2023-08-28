using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Akashic.Core;
using Akashic.Runtime.Controllers.OptionsMenu;
using Akashic.Runtime.Serializers;
using Akashic.Runtime.Utilities.FileStream;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.PlayerPrefs
{
    internal sealed class PlayerPreferencesMonoSystem : MonoBehaviour, IPlayerPreferencesMonoSystem
    {
        [Header("Preferences File Path")]
        [SerializeField] private string preferencesFolderName;
        
        [SerializeField] private string preferencesFileName;
        
        private string preferencesFilePath;
        
        private PlayerPreferences playerPreferences;

        private FileStreamer fileStreamer;

        private bool savingInProgress;
        
        private void Awake()
        {
            preferencesFilePath = Path.Combine(Application.persistentDataPath, preferencesFolderName);
            fileStreamer = new FileStreamer(preferencesFilePath, preferencesFileName);
            
            InitializeExistingPreferencesData();
        }

        private void OnEnable()
        { 
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        public void UpdateSoundPreferences(float musicVolume, float effectsVolume)
        {
            playerPreferences.SetMusicVolume(musicVolume);
            playerPreferences.SetSfxVolume(effectsVolume);
        }
        
        public float GetMusicVolume()
        {
            return playerPreferences.MusicVolume;
        }
        
        public float GetEffectsVolume()
        {
            return playerPreferences.EffectsVolume;
        }
        
        private async void SavePreferencesAsync()
        {
            savingInProgress = true;

            if (savingInProgress)
            {
                await Task.Yield();
            }
            
            var preferencesText = JsonConvert.SerializeObject(playerPreferences);
            await fileStreamer.WriteFileAsync(preferencesText);
            
            savingInProgress = false;
        }
        
        private async void LoadPreferencesAsync()
        {
            var preferencesText = await fileStreamer.ReadFileAsync();
            playerPreferences = JsonConvert.DeserializeObject<PlayerPreferences>(preferencesText);
            
            GameManager.Publish(new PlayerPreferencesLoadedMessage());
        }
        
        private void InitializeExistingPreferencesData()
        {
            if (fileStreamer.DoesFileExist())
            {
                LoadPreferencesAsync();
            }
            else
            {
                playerPreferences = new PlayerPreferences(0.5f, 0.5f);
                SavePreferencesAsync();
            }
        }

        private void OnSettingsMenuClosedMessage(OptionsMenuClosedMessage message)
        {
            SavePreferencesAsync();
        }

        private void AddListeners()
        {
            GameManager.AddListener<OptionsMenuClosedMessage>(OnSettingsMenuClosedMessage);
        }

        private void RemoveListeners()
        {
            GameManager.RemoveListener<OptionsMenuClosedMessage>(OnSettingsMenuClosedMessage);
        }
    }
}