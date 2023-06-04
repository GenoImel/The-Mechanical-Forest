using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Akashic.Runtime.DataManager
{
    internal sealed class DataPersistenceManager : MonoBehaviour
    {
        [Header("Debugging")]
        [SerializeField] private bool createNewGameIfNull = true;
        [SerializeField] private bool useEncryption = true;

        [Header("File Storage Configuration")]
        [SerializeField] private string directory = "GameData";
        [SerializeField] private string filename = "Game1";
        [SerializeField] private string currentProfileID  = "Development";

        public static DataPersistenceManager Instance { get; private set; }

        private GameData gameData;
        private List<IDataPersistence> dataPersistenceObjects;
        private XMLFileHandler xmlFileHandler;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
        
            Instance = this;

            DontDestroyOnLoad(this.gameObject);

            xmlFileHandler = new XMLFileHandler(directory, filename, useEncryption);
        }
        
        private void OnEnable()
        {
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
        
        public Dictionary<string, GameData> GetAllProfilesGameData()
        {
            return xmlFileHandler.LoadAndGetAllProfiles();
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
        {
            dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }

        public void OnSceneUnloaded(Scene scene)
        {
            SaveGame();
        }

        /// <summary>
        /// Run when a new game is created.
        /// </summary>
        public void NewGame()
        {
            gameData = new GameData();
        }

        /// <summary>
        /// Save the game to an XML file.
        /// </summary>
        public void SaveGame()
        {
            if (gameData == null)
            {
                throw new Exception("GameData is null. Please call NewGame() before SaveGame().");
            }

            foreach (var obj in dataPersistenceObjects)
            {
                obj.SaveData(ref gameData);
            }

            xmlFileHandler.Save(gameData, currentProfileID);
        }

        /// <summary>
        /// Loads a game from XML file.
        /// </summary>
        public void LoadGame()
        {
            gameData = xmlFileHandler.Load(currentProfileID);

            // Future Bug: If LoafGame is called before in a spot where data has not be initialized game save will be deleted!
            if (gameData == null && createNewGameIfNull)
            {
                NewGame();
            }
            else if (gameData == null)
            {
                return;
            }

            foreach (var obj in dataPersistenceObjects)
            {
                obj.LoadData(gameData);
            }
        }

        /// <summary>
        /// Return all GameObjects with a IDataPersistence component.
        /// </summary>
        /// <returns></returns>
        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            
            //TODO: Find a way to get all objects with IDataPersistence without using FindObjectsOfType
            //      This is a very expensive operation.
            var dataPersistenceObjects = 
                FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
            
            return dataPersistenceObjects.ToList();
        }
        
        private void AddListeners()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void RemoveListeners()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}

