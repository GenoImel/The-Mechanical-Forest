using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using UnityEngine;

namespace Akashic.Runtime.DataManager
{
    internal class XMLFileHandler
    {
        private string dirPath = "";
        private string filename = "";
        private readonly string fileExtension = ".xml";
        private bool encryptData;
        private readonly string encryptionKey = "Poggers";
        private SymmetricAlgorithm key;

        /// <summary>
        /// Create a MD5 hash from a string from <paramref name="input"/>.
        /// </summary>
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

        /// <summary>
        /// Generate the encrpytion key. This will need to be modified in the future
        /// for better security.
        /// </summary>
        private void InitializesEncryptor()
        {
            key = new DESCryptoServiceProvider();
            var fullHash = CreateMD5(encryptionKey);
            var keyBytes = new byte[8];
            Array.Copy(fullHash, keyBytes, 8);
            key.Key = keyBytes;
            key.IV = keyBytes;
        }

        public XMLFileHandler(string dirPath, string filename, bool encryptData = true)
        {
            this.dirPath = dirPath;
            this.filename = filename;
            this.encryptData = encryptData;

            if (encryptData) InitializesEncryptor();
        }

        /// <summary>
        /// Loads a GameData object from an XML file using <paramref name="profileID"/>.
        /// </summary>
        public GameData Load(string profileID)
        {
            var path = Path.Combine(Application.dataPath, dirPath, profileID, filename + fileExtension);
            
            GameData data = null;
            
            if (File.Exists(path))
            {
                try
                {
                    XmlSerializer serializer = new(typeof(GameData));
                
                    using (FileStream fileStream = new(path, FileMode.Open))
                    {
                        if (encryptData)
                        {
                            using CryptoStream stream = new (
                                fileStream, 
                                key.CreateDecryptor(), 
                                CryptoStreamMode.Read
                                );
                            
                            data = (GameData)serializer.Deserialize(stream);
                        }
                        else data = (GameData)serializer.Deserialize(fileStream);
                    }

                } catch (Exception e)
                {
                    throw new Exception($"An error occured loading the game data to file: " +
                                        $"{path} \n {e}");
                }
            }

            return data;
        }

        /// <summary>
        /// Saves the <paramref name="data"/> to an XML file using <paramref name="profileID"/>.
        /// </summary>
        public void Save(GameData data, string profileID)
        {
            var path = Path.Combine(Application.dataPath, dirPath, profileID, filename + fileExtension);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                XmlSerializer serializer = new(typeof(GameData));

                using (FileStream fileStream = new(path, FileMode.Create)) 
                {
                    if (encryptData)
                    {
                        using CryptoStream stream = new (
                            fileStream, 
                            key.CreateEncryptor(), 
                            CryptoStreamMode.Write
                            );
                        
                        serializer.Serialize(stream, data);
                    }
                    else
                    {
                        serializer.Serialize(fileStream, data);
                    }
                }
            } catch (Exception e)
            {
                throw new Exception($"An error occured loading the game data to file: " +
                                    $"{path} \n {e}");
            }
        }

        public Dictionary<string, GameData> LoadAndGetAllProfiles()
        {
            Dictionary<string, GameData> profileDictionary = new();

            var dirInfos = new DirectoryInfo(
                Path.Combine(Application.dataPath, dirPath))
                .EnumerateDirectories();

            foreach (DirectoryInfo dir in dirInfos)
            {
                var profileID = dir.Name;
                
                var path = Path.Combine(Application.dataPath, dirPath, profileID, filename + fileExtension);

                if (!File.Exists(path))
                {
                    continue;
                }
                
                var profileData = Load(profileID);
                if (profileData != null)
                {
                    profileDictionary.Add(profileID, profileData);
                }
                else
                {
                    throw new Exception($"ERROR: Failed Loading ProfileID: {profileID}");
                }
            }

            return profileDictionary;
        }
    }
}
