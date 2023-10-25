using System.Collections.Generic;
using Akashic.ScriptableObjects.Config;

namespace Akashic.Runtime.MonoSystems.Config.Settings
{
    internal struct SaveConfigSettings
    {
        public readonly string parentSaveFolderName;
        public readonly List<string> saveFolderNames;
        public readonly List<string> saveFileNames;
        public readonly int saveSlotNameCharacterLimit;

        public SaveConfigSettings(ConfigData config)
        {
            parentSaveFolderName = config.parentSaveFolderName;
            saveFolderNames = config.saveFolderNames;
            saveFileNames = config.saveFileNames;
            saveSlotNameCharacterLimit = config.saveSlotNameCharacterLimit;
        }
    }
}
