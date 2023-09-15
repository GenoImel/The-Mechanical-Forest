using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Party;
using Akashic.Runtime.MonoSystems.Scene;
using Akashic.Runtime.Serializers;
using Akashic.Runtime.Utilities.FileStream;
using Newtonsoft.Json;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Save
{
    internal sealed class SaveMonoSystem : MonoBehaviour, ISaveMonoSystem
    {
        [Header("Save Data File Path")]
        [SerializeField] private string saveFolderName;
        
        [SerializeField] private List<string> saveSlotFolderNames = new List<string>();
        
        [SerializeField] private string saveFileName;
        
        private string saveFilePath;

        private SaveFile currentSaveFile;

        private readonly List<FileStreamer> fileStreamers = new List<FileStreamer>();
        
        private IDictionary<string, FileStreamer> saveFiles = new Dictionary<string, FileStreamer>();

        private bool savingInProgress;
        
        private ISceneMonoSystem sceneMonoSystem;
        private IPartyMonoSystem partyMonoSystem;

        private void Awake()
        {
            sceneMonoSystem = GameManager.GetMonoSystem<ISceneMonoSystem>();
            partyMonoSystem = GameManager.GetMonoSystem<IPartyMonoSystem>();
        }

        private void Start()
        {
            IndexSaveFiles();
        }
        
        private void OnEnable()
        { 
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }
        
        public async Task<string> FindSaveFile(string saveSlotFileName)
        {
            var file = saveFiles[saveSlotFileName];
            if (!file.DoesFileExist())
            {
                return null;
            }

            var saveFileText = await file.ReadFileAsync();
            var saveFile = JsonConvert.DeserializeObject<SaveFile>(saveFileText);

            return saveFile.SaveFileName;
        }

        public List<PartyMember> GetPartyMembers()
        {
            if (currentSaveFile == null)
            {
                throw new NullReferenceException("Save file is null");
            }
            
            return currentSaveFile.PartyMembers;
        }

        public async void SaveFileAsync()
        {
            
        }

        public async void LoadFileAsync()
        {
            
        }
        
        public void InitializeNewFile(SaveFile saveFile, string saveSlotFileName)
        {
            if (saveFile == null)
            {
                throw new NullReferenceException("Save file is null");
            }
            
            if (string.IsNullOrEmpty(saveSlotFileName))
            {
                throw new NullReferenceException("Save slot file name is null or empty");
            }
            
            currentSaveFile = saveFile;
        }

        private void IndexSaveFiles()
        {
            foreach (var slot in saveSlotFolderNames)
            {
                string path = Path.Combine(Application.persistentDataPath, saveFolderName, slot);
                FileStreamer fileStreamer = new FileStreamer(path, saveFileName);
                fileStreamers.Add(fileStreamer);
            }
            
            saveFiles = saveSlotFolderNames.Zip(
                fileStreamers, 
                (k, v) => new { k, v }
            ).ToDictionary(x => x.k, x => x.v);
        }

        private void AddListeners()
        {
        }
        
        private void RemoveListeners()
        {
        }
    }
}