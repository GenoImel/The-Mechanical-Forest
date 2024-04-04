using System.IO;
using Akashic.Core;
using Akashic.Runtime.Serializers.PlayerPrefs;
using Akashic.Runtime.Utilities.FileStream;
using Newtonsoft.Json;
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

        private void Awake()
        {
            preferencesFilePath = Path.Combine(Application.persistentDataPath, preferencesFolderName);
            fileStreamer = new FileStreamer(preferencesFilePath, preferencesFileName);
            
            InitializeExistingPreferencesData();
        }

        public void UpdateSoundPreferences(float masterVolume, float musicVolume, float effectsVolume)
        {
            playerPreferences.SetMasterVolume(masterVolume);
            playerPreferences.SetMusicVolume(musicVolume);
            playerPreferences.SetSfxVolume(effectsVolume);

            SavePreferencesAsync();
        }
        
        public float GetMasterVolume()
        {
            return playerPreferences.MasterVolume;
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
            Debug.Log("Saving player preferences.");
            var preferencesText = JsonConvert.SerializeObject(playerPreferences);
            await fileStreamer.WriteFileAsync(preferencesText);
        }
        
        private async void LoadPreferencesAsync()
        {
            Debug.Log("Loading player preferences.");
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
                playerPreferences = new PlayerPreferences(0.5f,0.5f, 0.5f);
                SavePreferencesAsync();
            }
        }
    }
}