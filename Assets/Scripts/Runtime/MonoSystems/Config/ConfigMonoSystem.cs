using System.Collections.Generic;
using Akashic.Runtime.MonoSystems.Config.Settings;
using Akashic.ScriptableObjects.Config;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Config
{
    internal sealed class ConfigMonoSystem : MonoBehaviour, IConfigMonoSystem
    {
        [SerializeField] private ConfigData configData;

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