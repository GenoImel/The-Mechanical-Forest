using System.Collections.Generic;
using Akashic.ScriptableObjects.Scripts.ConfigBase;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Config
{
    internal sealed class ConfigMonoSystem : MonoBehaviour, IConfigMonoSystem
    {
        [SerializeField] private ConfigBaseData configData;

        public List<string> GetSaveSlotFileNames()
        {
            return configData.saveSlotFileNames;
        }
    }
}