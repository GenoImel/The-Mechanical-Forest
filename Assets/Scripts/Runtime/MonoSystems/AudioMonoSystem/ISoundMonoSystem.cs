using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.SoundManagement 
{
    /// <Summary>
    /// Interface responsible for makeing the 'SoundMonoSystem' script a MonoSystem
    /// </Summary>
    internal interface ISoundMonoSystem : IMonoSystem {
        public SoundMonoSystem GetComponent();
    }
}
