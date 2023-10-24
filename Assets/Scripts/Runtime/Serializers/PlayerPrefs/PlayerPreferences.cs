using System;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers
{
    [Serializable]
    internal sealed class PlayerPreferences
    {
        [JsonProperty("musicVolume")]
        public float MusicVolume { get; private set; }
        
        [JsonProperty("effectsVolume")]
        public float EffectsVolume { get; private set; }
        
        [JsonConstructor]
        public PlayerPreferences(
            [JsonProperty("musicVolume")] float musicVolume,
            [JsonProperty("sfxVolume")] float effectsVolume
        )
        {
            MusicVolume = musicVolume;
            EffectsVolume = effectsVolume;
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