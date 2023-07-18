using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Akashic.Runtime.Utilities.FileStream
{
    internal sealed class FileStreamer
    {
        private readonly string directoryPath;
        private readonly string fileName;
        private readonly string fullFilePath;

        public FileStreamer(string directoryPath, string fileName)
        {
            this.directoryPath = directoryPath;
            this.fileName = fileName;
            
            fullFilePath = Path.Combine(directoryPath, fileName);
            InitializeFileDirectory();
        }

        public bool DoesFileExist()
        {
            return File.Exists(fullFilePath);
        }

        public IEnumerable<string> GetFiles()
        {
            var files = Directory.EnumerateFiles(directoryPath)
                .Where(x => Path.GetFileName(x).Equals(fileName)).ToList();

            return files;
        }

        public async Task WriteFileAsync(string fileText)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fullFilePath, false))
                {
                    await writer.WriteAsync(fileText);
                    await writer.FlushAsync();
                }

                Debug.Log("File successfully written.");
            }
            catch (Exception e)
            {
                Debug.LogError("Error writing file: " + e.Message);
            }
        }
    
        public async Task<string> ReadFileAsync()
        {
            var fileText = "";
            try
            {
                using (StreamReader reader = new StreamReader(fullFilePath))
                {
                    fileText = await reader.ReadToEndAsync();
                }

                Debug.Log("File successfully read.");
            }
            catch (Exception e)
            {
                Debug.LogError("Error reading file: " + e.Message);
            }

            return fileText;
        }

        public void ClearData()
        {
            if (File.Exists(fullFilePath))
            {
                try
                {
                    File.Delete(fullFilePath);
                    Debug.Log("File successfully deleted.");
                }
                catch (Exception e)
                {
                    Debug.LogError("Error deleting file: " + e.Message);
                }
            }
            else
            {
                Debug.Log("No file exists to be deleted.");
            }
        }

        private void InitializeFileDirectory()
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Debug.Log($"Created file directory at ${directoryPath}");
                }
                else
                {
                    Debug.Log($"File directory already exists at ${directoryPath}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error creating file directory: ${e.Message}");
            }
        }
    }
}