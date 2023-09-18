using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.Save;
using UnityEngine;

namespace Akashic.Runtime.Controllers.SaveMenu
{
    internal sealed class SaveFileContainer : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SaveSlot saveSlot;

        [SerializeField] private List<SaveSlot> saveSlots = new List<SaveSlot>();
        
        [SerializeField] private string defaultEmptySaveSlotText = "No Data";

        private ICollection<string> saveSlotNames = new List<string>();

        private ISaveMonoSystem saveMonoSystem;
        private IConfigMonoSystem configMonoSystem;

        private void Awake()
        {
            saveMonoSystem = GameManager.GetMonoSystem<ISaveMonoSystem>();
            configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
        }

        private void Start()
        {
            saveSlotNames = configMonoSystem.GetSaveSlotFileNames();
        }

        public async void FindSaveFiles()
        {
            var listFileNames = saveSlotNames.ToList();
            
            for (var i = 0; i < saveSlots.Count; i++)
            {
                var fileName = await FindSaveFile(listFileNames[i]);
                
                saveSlots[i].SetSaveSlotName(string.IsNullOrEmpty(fileName) ? defaultEmptySaveSlotText : fileName);
                saveSlots[i].SetSaveSlotFileName(listFileNames[i]);
            }
        }
        
        private async Task<string> FindSaveFile(string saveSlotName)
        {
            var fileName =  await saveMonoSystem.FindSaveFile(saveSlotName);
            return fileName;
        }
        
    }
}