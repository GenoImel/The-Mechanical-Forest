using System.Collections.Generic;
using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.Config
{
    internal interface IConfigMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Retrieves the save file names from config.
        /// </summary>
        public List<string> GetSaveSlotFileNames();
    }
}