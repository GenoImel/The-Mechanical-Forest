using System.Collections.Generic;
using Akashic.Runtime.Character;
using UnityEngine;

namespace Akashic.Runtime.DataManager
{
    /// <summary>
    ///  All data that needs to be save should be stored within this class. All subdata class 
    /// for example CharacterData needs to be marked with [System.Serializable].
    /// </summary>
    
    [System.Serializable]
    internal sealed class GameData
    {
        [HideInInspector] public List<CharacterData> characters;

        /// <summary>
        /// Default values for a new game
        /// </summary>
        public GameData()
        {
            characters = new List<CharacterData>();
        }
    }
}
