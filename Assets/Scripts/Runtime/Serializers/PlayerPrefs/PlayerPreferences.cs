using System;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.PlayerPrefs
{
    [Serializable]
    internal sealed class PlayerPreferences
    {
        [JsonProperty("masterVolume")]
        public float MasterVolume { get; private set; }
        
        [JsonProperty("musicVolume")]
        public float MusicVolume { get; private set; }
        
        [JsonProperty("effectsVolume")]
        public float EffectsVolume { get; private set; }
        
        [JsonConstructor]
        public PlayerPreferences(
            [JsonProperty("masterVolume")] float masterVolume,
            [JsonProperty("musicVolume")] float musicVolume,
            [JsonProperty("sfxVolume")] float effectsVolume
        )
        {
            MasterVolume = masterVolume;
            MusicVolume = musicVolume;
            EffectsVolume = effectsVolume;
        }
        
        public void SetMasterVolume(float masterVolume)
        {
            MasterVolume = masterVolume;
        }
        
        public void SetMusicVolume(float musicVolume)
        {
            MusicVolume = musicVolume;
        }
        
        public void SetSfxVolume(float effectsVolume)
        {
            EffectsVolume = effectsVolume;
        }
    }
}