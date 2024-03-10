using UnityEngine;

namespace Akashic.ScriptableObjects.Config
{
    [CreateAssetMenu(menuName = "Akashic/Config/New Game Config Settings")]
    public class GameConfigData : ScriptableObject
    {
        public int maximumNumberAccessorySlots;

        public int maximumLevel;
        
        /// <summary>
        /// The default number of pips a character starts with at the beginning of battle.
        /// </summary>
        public int basePips;
    }
}