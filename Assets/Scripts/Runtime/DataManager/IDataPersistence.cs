namespace Akashic.Runtime.DataManager
{
    internal interface IDataPersistence
    {
        /// <summary>
        /// Load the <paramref name="data"/>.
        /// Current implementation requires this be done between scenes.
        /// </summary>
        void LoadData(GameData data);
        
        /// <summary>
        /// save the <paramref name="data"/>.
        /// Current implementation requires this be done between scenes.
        /// </summary>
        void SaveData(ref GameData data);
    }
}
