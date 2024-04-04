using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Config;

namespace Akashic.Runtime.MonoSystems.Config.Settings
{
    internal struct SaveConfigSettings
    {
        public readonly string parentSaveFolderName;
        public readonly List<string> saveFolderNames;
        public readonly List<string> saveFileNames;
        public readonly int saveSlotNameCharacterLimit;

        public SaveConfigSettings(SaveConfigData saveConfig)
        {
            parentSaveFolderName = saveConfig.parentSaveFolderName;
            saveFolderNames = saveConfig.saveFolderNames;
            saveFileNames = saveConfig.saveFileNames;
            saveSlotNameCharacterLimit = saveConfig.saveSlotNameCharacterLimit;
        }
    }
}
