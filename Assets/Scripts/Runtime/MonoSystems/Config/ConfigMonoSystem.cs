using System.Collections.Generic;
using Akashic.ScriptableObjects.ConfigBase;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Config
{
    internal sealed class ConfigMonoSystem : MonoBehaviour, IConfigMonoSystem
    {
        [SerializeField] private ConfigBaseData configData;

        public string GetParentSaveFolderName()
        {
            return configData.saveFolderParentName;
        }
        
        public List<string> GetSaveSlotFolderNames()
        {
            return configData.saveSlotFolderNames;
        }

        public List<string> GetSaveSlotFileNames()
        {
            return configData.saveSlotFileNames;
        }
    }
}