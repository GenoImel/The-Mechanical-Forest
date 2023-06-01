using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool _createNewGameIfNull = true;
    [SerializeField] private bool _useEncryption       = true;

    [Header("File Storage Configuration")]
    [SerializeField] private string _directory         = "GameData";
    [SerializeField] private string _filename          = "Game1";
    [SerializeField] private string _currentProfileID  = "Development";

    public static DataPersistenceManager Instance { get; private set; }

    private GameData _gameData;
    private List<IDataPresistence> _dataPresistencesObjects;
    private XMLFileHandler _xmlFileHandler;

    /*
     *  Code to be ran whem a new game is created 
     */
    public void NewGame()
    {
        this._gameData = new GameData();
    }

    
    /*
     * Saves the game data to XML file
     */
    public void SaveGame()
    {
        if (this._gameData == null) Debug.Log("A New Game Needs To Be Started Before Data Can Be Loaded!");

        foreach (IDataPresistence obj in _dataPresistencesObjects)
        {
            obj.SaveData(ref _gameData);
        }

        _xmlFileHandler.Save(_gameData, _currentProfileID);

    }

    /*
     * Loads game data from a XML file
     */
    public void LoadGame()
    {
        this._gameData = _xmlFileHandler.Load(_currentProfileID);

        // Future Bug: If LoafGame is called before in a spot where data has not be initalzied game save will be deleted!
        if (this._gameData == null && _createNewGameIfNull) NewGame();
        else if (this._gameData == null) return;

        foreach (IDataPresistence obj in _dataPresistencesObjects)
        {
            obj.LoadData(_gameData);
        }
    }

    /*
     * Finds all Objects that inherit from MonoBehaviour that implemnts the interface IDataPresistences
     */
    private List<IDataPresistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPresistence> dataPresistencesObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPresistence>();
        return new List<IDataPresistence>(dataPresistencesObjects);
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _xmlFileHandler.LoadAndGetAllProfiles();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        this._dataPresistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void Awake()
    {
        // Checks if more than one Persistence Manger exist in the current scene
        if (Instance != null)
        {
            Debug.Log("More than one Data Persistence manger found in scene! Destorying Newest Scene!");
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        this._xmlFileHandler = new XMLFileHandler(_directory, _filename, _useEncryption);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

