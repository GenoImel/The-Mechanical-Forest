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
            return configData.parentSaveFolderName;
        }
        
        public List<string> GetSaveFolderNames()
        {
            return configData.saveFolderNames;
        }

        public List<string> GetSaveFileNames()
        {
            return configData.saveFileNames;
        }
        
        public int GetSaveSlotNameCharacterLimit()
        {
            return configData.saveSlotNameCharacterLimit;
        }
    }
}