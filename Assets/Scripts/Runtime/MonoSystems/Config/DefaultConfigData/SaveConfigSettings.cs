using Akashic.ScriptableObjects.ConfigBase;
using System.Collections.Generic;

namespace Akashic.Assets.Scripts.Runtime.MonoSystems.Config.DefaultConfigData
{
    internal struct SaveConfigSettings
    {
        public string parentSaveFolderName;
        public List<string> saveFolderNames, saveFileNames;
        public int saveSlotNameCharacterLimit;

        public SaveConfigSettings(ConfigBaseData config)
        {
            parentSaveFolderName = config.parentSaveFolderName;
            saveFolderNames = config.saveFolderNames;
            saveFileNames = config.saveFileNames;
            saveSlotNameCharacterLimit = config.saveSlotNameCharacterLimit;
        }
    }
}
