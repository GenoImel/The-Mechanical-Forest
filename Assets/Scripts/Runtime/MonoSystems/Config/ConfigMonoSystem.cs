using System.Collections.Generic;
using Akashic.ScriptableObjects.Scripts.ConfigBase;
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
    }
}