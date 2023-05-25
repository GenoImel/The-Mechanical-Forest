using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;
using UnityEngine;
using System.Text;

public class XMLFileHandler
{
    // File paths infomation
    private string _dirPath = "";
    private string _filename = "";
    private readonly string _fileExtension = ".xml";
    // Encrpytion data
    private bool _encryptData;
    private readonly string _encryptionKey = "Poggers";
    private SymmetricAlgorithm _key;

    // Create a MD5 hash from a string
    private static byte[] CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return hashBytes;
        }
    }

    /*
     * Genrate the encryption key. This encryption will stop the average joe from messing with the game files
     * however this is a pretty bad encryption and should probability be modifyed in the future.
     */
    private void InitiazliesEncryptor()
    {
        _key = new DESCryptoServiceProvider();
        byte[] fullHash = CreateMD5(_encryptionKey);
        byte[] keyBytes = new byte[8];
        Array.Copy(fullHash, keyBytes, 8);
        _key.Key = keyBytes;
        _key.IV = keyBytes;
    }

    public XMLFileHandler(string dirPath, string filename, bool encryptData = true)
    {
        _dirPath = dirPath;
        _filename = filename;
        _encryptData = encryptData;

        if (encryptData) InitiazliesEncryptor();
    }

    /*
     * Loads a gane data object from an XML file
     */
    public GameData Load(string profileID)
    {
        string path = Path.Combine(Application.dataPath, _dirPath, profileID, _filename + _fileExtension);
        GameData data = null;
        if (File.Exists(path))
        {
            try
            {
                XmlSerializer serializer = new(typeof(GameData));
                
                using (FileStream fileStream = new(path, FileMode.Open))
                {
                    if (_encryptData) using (CryptoStream stream = new(fileStream, _key.CreateDecryptor(), CryptoStreamMode.Read)) { data = (GameData)serializer.Deserialize(stream); }
                    else data = (GameData)serializer.Deserialize(fileStream);
                }

            } catch (Exception e) 
            {
                Debug.Log("the following error occured loading the game data to file: " + path + "\n" + e);
            }
        }

        return data;
    }

    /*
     *  Saves a game data object to an XML file
     */
    public void Save(GameData data, string profileID)
    {
        string path = Path.Combine(Application.dataPath, _dirPath, profileID, _filename + _fileExtension);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            XmlSerializer serializer = new(typeof(GameData));

            using (FileStream fileStream = new(path, FileMode.Create)) 
            {
                if (_encryptData) using (CryptoStream stream = new(fileStream, _key.CreateEncryptor(), CryptoStreamMode.Write)) { serializer.Serialize(stream, data); }
                else serializer.Serialize(fileStream, data);

            }
        } catch (Exception e)
        {
            Debug.Log("the following error occured saving game data to file: " + path + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAndGetAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(Path.Combine(Application.dataPath, _dirPath)).EnumerateDirectories();

        foreach (DirectoryInfo dir in dirInfos)
        {
            string profileID = dir.Name;
            string path = Path.Combine(Application.dataPath, _dirPath, profileID, _filename + _fileExtension);
            if (!File.Exists(path)) continue;
            GameData profileData = Load(profileID);
            if (profileData != null) profileDictionary.Add(profileID, profileData);
            else Debug.Log("ERROR: Failed Loading ProfileID: " + profileID);
        }

        return profileDictionary;
    }
}
