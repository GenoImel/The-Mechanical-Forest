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
using Akashic.Runtime.Serializers.Save;
using Akashic.Runtime.Serializers.Settings;
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

			var settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				Formatting = Formatting.Indented,
			};

			var saveFileText = await file.ReadFileAsync();
            var saveFile = JsonConvert.DeserializeObject<SaveFile>(saveFileText, settings);

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
		
		public string GetRoomId()
		{
			if (currentSaveData == null)
			{
				throw new NullReferenceException("Save file is null");
			}

			return currentSaveData.PartyLocation.RoomId;
		}
		
		public string GetSpawnPointId()
		{
			if (currentSaveData == null)
			{
				throw new NullReferenceException("Save file is null");
			}

			return currentSaveData.PartyLocation.SpawnPointId;
		}

		public async void SaveFileAsync()
        {
            savingInProgress = true;
            
            var fileStreamer = saveFiles[currentSaveSlot];

            if (savingInProgress)
            {
                await Task.Yield();
            }

			var settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				Formatting = Formatting.Indented,
			};

			var preferencesText = JsonConvert.SerializeObject(currentSaveData, settings);
            await fileStreamer.WriteFileAsync(preferencesText);
            
            savingInProgress = false;
        }

        public async void LoadFileAsync()
        {
            var fileStreamer = saveFiles[currentSaveSlot];
            
            var saveFileText = await fileStreamer.ReadFileAsync();

			var settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				SerializationBinder = new SaveFileSerializationBinder(),
                Formatting = Formatting.Indented,
			};

			currentSaveData = JsonConvert.DeserializeObject<SaveFile>(saveFileText, settings);

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
            var tempSaveSettings = configMonoSystem.GetSaveConfigSettings();
            
            saveFolderParentName = tempSaveSettings.parentSaveFolderName;
            saveSlotFolderNames = tempSaveSettings.saveFolderNames;
            saveSlotFileNames = tempSaveSettings.saveFileNames;
        }

        private void IndexSaveFiles()
        {
            for (var i = 0; i < saveSlotFolderNames.Count; i++)
            {
                var path = Path.Combine(Application.persistentDataPath, saveFolderParentName, saveSlotFolderNames[i]);
                var fileStreamer = new FileStreamer(path, saveSlotFileNames[i]);
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