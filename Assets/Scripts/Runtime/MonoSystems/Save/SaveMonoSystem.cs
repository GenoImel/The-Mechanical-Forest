using System;
using System.Collections.Generic;
using System.IO;
using Akashic.Runtime.Serializers;
using Akashic.Runtime.Utilities.FileStream;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Save
{
    internal sealed class SaveMonoSystem : MonoBehaviour, ISaveMonoSystem
    {
        [Header("Save Data File Path")]
        [SerializeField] private string saveFolderName;
        
        [SerializeField] private string saveFileName;
        
        private string saveFilePath;

        private SaveFile saveFile;
        
        private FileStreamer fileStreamer;
        
        private bool savingInProgress;

        private void Awake()
        {
            saveFilePath = Path.Combine(Application.persistentDataPath, saveFolderName);
            fileStreamer = new FileStreamer(saveFilePath, saveFileName);
        }
        
        private void OnEnable()
        { 
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }

        public List<PartyMember> GetPartyMembers()
        {
            if (saveFile == null)
            {
                throw new NullReferenceException("Save file is null");
            }
            
            return saveFile.PartyMembers;
        }

        public async void SaveFileAsync()
        {
            
        }

        public async void LoadFileAsync()
        {
            
        }
        
        public void InitializeNewFile()
        {
            
        }

        private void AddListeners()
        {
        }
        
        private void RemoveListeners()
        {
        }
    }
}