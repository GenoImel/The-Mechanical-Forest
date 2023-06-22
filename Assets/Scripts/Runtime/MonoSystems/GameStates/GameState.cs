namespace Akashic.Runtime.MonoSystems.GameStates
{
    internal enum GameState
    {
        /// <summary>
        /// Main menu scene is active.
        /// </summary>
        MainMenu,
        
        /// <summary>
        /// Exploration scene is active.
        /// </summary>
        Exploration,
        
        /// <summary>
        /// Battle scene is active.
        /// </summary>
        Battle,
        
        /// <summary>
        /// Dialogue panel is being shown.
        /// Accessible through multiple scenes.
        /// </summary>
        Dialogue,
        
        /// <summary>
        /// Pause panel is being shown.
        /// Accessible through multiple scenes.
        /// </summary>
        Paused
    }
}