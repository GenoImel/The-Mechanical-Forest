using System.Collections.Generic;
using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.Config
{
    /// <summary>
    /// Provides access to configuration data for the game, which is set in a
    /// ConfigBaseData ScriptableObject.
    /// </summary>
    internal interface IConfigMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Returns the name of the parent save folder in
        /// Application.persistentDataPath where all save folders will be stored.
        /// </summary>
        public string GetParentSaveFolderName();

        /// <summary>
        /// Returns the name of child folder names which correlate to an individual save slot.
        /// </summary>
        public List<string> GetSaveFolderNames();

        /// <summary>
        /// Returns the name of a save file correlating to an individual save slot.
        /// </summary>
        public List<string> GetSaveFileNames();
    }
}