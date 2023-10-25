using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.Inventory;
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
        private string saveFolderParentName;
        
        private List<string> saveSlotFolderNames = new List<string>();
        
        private List<string> saveSlotFileNames = new List<string>();

        private string saveFilePath;

        private SaveFile currentSaveData;
        private string currentSaveSlot;

        private readonly List<FileStreamer> fileStreamers = new List<FileStreamer>();
        
        private IDictionary<string, FileStreamer> saveFiles = new Dictionary<string, FileStreamer>();

        private bool savingInProgress;

        private IConfigMonoSystem configMonoSystem;
        private ISceneMonoSystem sceneMonoSystem;
		private IPartyMonoSystem partyMonoSystem;
		private IInventoryMonoSystem inventoryMonoSystem;

		private void Awake()
        {
            configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
            sceneMonoSystem = GameManager.GetMonoSystem<ISceneMonoSystem>();
			partyMonoSystem = GameManager.GetMonoSystem<IPartyMonoSystem>();
			inventoryMonoSystem = GameManager.GetMonoSystem<IInventoryMonoSystem>();
		}

        private void Start()
        {
            InitializeFileInfoFromConfig();
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
			if (currentSaveData == null)
			{
				throw new NullReferenceException("Save file is null");
			}

			return currentSaveData.PartyMembers;
		}

		public PartyInventory GetPartyInventory()
		{
			if (currentSaveData == null)
			{
				throw new NullReferenceException("Save file is null");
			}

			return currentSaveData.PartyInventory;
		}

		public async void SaveFileAsync()
        {
            savingInProgress = true;
            
            var fileStreamer = saveFiles[currentSaveSlot];

            if (savingInProgress)
            {
                await Task.Yield();
            }
            
            var preferencesText = JsonConvert.SerializeObject(currentSaveData);
            await fileStreamer.WriteFileAsync(preferencesText);
            
            savingInProgress = false;
        }

        public async void LoadFileAsync()
        {
            var fileStreamer = saveFiles[currentSaveSlot];
            
            var saveFileText = await fileStreamer.ReadFileAsync();
            currentSaveData = JsonConvert.DeserializeObject<SaveFile>(saveFileText);
            
            GameManager.Publish(new SaveFileLoadedMessage());
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

            currentSaveSlot = saveSlotFileName;
            currentSaveData = saveFile;
            
            SaveFileAsync();
        }
        
        private void InitializeFileInfoFromConfig()
        {
            saveFolderParentName = configMonoSystem.GetParentSaveFolderName();
            saveSlotFolderNames = configMonoSystem.GetSaveFolderNames();
            saveSlotFileNames = configMonoSystem.GetSaveFileNames();
        }

        private void IndexSaveFiles()
        {
            for (var i = 0; i < saveSlotFolderNames.Count; i++)
            {
                string path = Path.Combine(Application.persistentDataPath, saveFolderParentName, saveSlotFolderNames[i]);
                FileStreamer fileStreamer = new FileStreamer(path, saveSlotFileNames[i]);
                fileStreamers.Add(fileStreamer);
            }
            
            saveFiles = saveSlotFileNames.Zip(
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