using System.Collections.Generic;
using Akashic.Assets.Scripts.Runtime.MonoSystems.Config.DefaultConfigData;
using Akashic.ScriptableObjects.ConfigBase;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Config
{
    internal sealed class ConfigMonoSystem : MonoBehaviour, IConfigMonoSystem
    {
        [SerializeField] private ConfigBaseData configData;

        private SaveConfigSettings saveConfigSettings;

        void Awake()
        {
            InitializeConfigSettings();
        }

        public SaveConfigSettings GetSaveConfigSettings()
        {
            return saveConfigSettings;
        }

        private void InitializeConfigSettings()
        {
            saveConfigSettings = new SaveConfigSettings(configData);
        }
    }
}