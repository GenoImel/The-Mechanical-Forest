using System.Collections.Generic;
using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.Config
{
    internal interface IConfigMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Retrieves the name of the parent save folder in
        /// PersistentDataPath from config SO.
        /// </summary>
        public string GetParentSaveFolderName();

        /// <summary>
        /// Retrieves the name of child folder names in
        /// PersistentDataPath from config SO.
        /// </summary>
        public List<string> GetSaveSlotFolderNames();

        /// <summary>
        /// Retrieves the save file names in
        /// PersistentDataPath from config SO.
        /// </summary>
        public List<string> GetSaveSlotFileNames();
        
        /// <summary>
        /// Retrieves the character limit for save slot names.
        /// </summary>
        public int GetSaveSlotNameCharacterLimit();
    }
}