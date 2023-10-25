using System.Collections.Generic;
using Akashic.Core;
using Akashic.Core.MonoSystems;
using Akashic.Runtime.MonoSystems.Config.Settings;

namespace Akashic.Runtime.MonoSystems.Config
{
    /// <summary>
    /// Provides access to configuration data for the game, which is set in a
    /// ConfigBaseData ScriptableObject.
    /// </summary>
    internal interface IConfigMonoSystem : IMonoSystem
    {
        public SaveConfigSettings GetSaveConfigSettings();
    }
}