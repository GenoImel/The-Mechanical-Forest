using Akashic.Core;
using Akashic.Core.MonoSystems;

namespace Akashic.Runtime.MonoSystems.PlayerPrefs
{
    internal interface IPlayerPreferencesMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Updates the players volume preferences for music and effects.
        /// </summary>
        public void UpdateSoundPreferences(float masterVolume, float musicVolume, float effectsVolume);
        
        /// <summary>
        /// Returns the players master volume preference.
        /// </summary>
        public float GetMasterVolume();
        
        /// <summary>
        /// Returns the players music volume preference.
        /// </summary>
        public float GetMusicVolume();

        /// <summary>
        /// Returns the players effects volume preference.
        /// </summary>
        public float GetEffectsVolume();
    }
}