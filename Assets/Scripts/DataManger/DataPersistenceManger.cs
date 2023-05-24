using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DataPersistenceManger : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField] private string filename;

    public static DataPersistenceManger instance { get; private set; }

    private GameData gameData;
    private List<IDataPresistence> dataPresistencesObjects;
    private XMLFileHandler xmlFileHandler;

    /*
     *  Code to be ran whem a new game is created 
     */
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    
    /*
     * Saves the game data to XML file
     */
    public void SaveGame()
    {
        foreach (IDataPresistence obj in dataPresistencesObjects)
        {
            obj.SaveData(ref gameData);
        }

        xmlFileHandler.Save(gameData);

    }

    /*
     * Loads game data from a XML file
     */
    public void LoadGame()
    {
        this.gameData = xmlFileHandler.Load();

        if (this.gameData == null) NewGame();

        foreach (IDataPresistence obj in dataPresistencesObjects)
        {
            obj.LoadData(gameData);
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

    private void Awake()
    {
        // Checks if more than one Persistence Manger exist in the current scene
        if (instance != null) throw new Exception("More than one Data Persistence manger found in scene!");
        instance = this;
    }

    private void Start()
    {
        this.dataPresistencesObjects = FindAllDataPersistenceObjects();
        this.xmlFileHandler = new XMLFileHandler("GameData", filename, true);
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

