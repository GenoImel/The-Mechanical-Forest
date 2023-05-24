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
    private string dirPath = "";
    private string filename = "";
    private readonly string fileExtension = ".xml";
    // Encrpytion data
    private bool encryptData;
    private readonly string encryptionKey = "Poggers";
    private SymmetricAlgorithm key;

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
        key = new DESCryptoServiceProvider();
        byte[] fullHash = CreateMD5(encryptionKey);
        byte[] keyBytes = new byte[8];
        Array.Copy(fullHash, keyBytes, 8);
        key.Key = keyBytes;
        key.IV = keyBytes;
    }

    public XMLFileHandler(string dirPath, string filename, bool encryptData = true)
    {
        this.dirPath = dirPath;
        this.filename = filename;
        this.encryptData = encryptData;

        if (encryptData) InitiazliesEncryptor();
    }

    /*
     * Loads a gane data object from an XML file
     */
    public GameData Load()
    {
        string path = Path.Combine(Path.Combine(Application.dataPath, dirPath), filename + fileExtension);
        GameData data = null;
        if (File.Exists(path))
        {
            try
            {
                XmlSerializer serializer = new(typeof(GameData));
                
                using (FileStream fileStream = new(path, FileMode.Open))
                {
                    if (encryptData) using (CryptoStream stream = new(fileStream, key.CreateDecryptor(), CryptoStreamMode.Read)) { data = (GameData)serializer.Deserialize(stream); }
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
    public void Save(GameData data)
    {
        string path = Path.Combine(Path.Combine(Application.dataPath, dirPath), filename + fileExtension);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            XmlSerializer serializer = new(typeof(GameData));

            using (FileStream fileStream = new(path, FileMode.Create)) 
            {
                if (encryptData) using (CryptoStream stream = new(fileStream, key.CreateEncryptor(), CryptoStreamMode.Write)) { serializer.Serialize(stream, data); }
                else serializer.Serialize(fileStream, data);

            }
        } catch (Exception e)
        {
            Debug.Log("the following error occured saving game data to file: " + path + "\n" + e);
        }
    }
}
