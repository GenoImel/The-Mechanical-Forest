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
    internal sealed class SaveSlotContainer : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private List<SaveSlot> saveSlots = new List<SaveSlot>();
        
        [SerializeField] private string defaultEmptySaveSlotText = "No Data";

        private ICollection<string> saveFileNames = new List<string>();

        private ISaveMonoSystem saveMonoSystem;
        private IConfigMonoSystem configMonoSystem;

        private void Awake()
        {
            saveMonoSystem = GameManager.GetMonoSystem<ISaveMonoSystem>();
            configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
        }

        private void Start()
        {
            saveFileNames = configMonoSystem.GetSaveFileNames();
        }

        public async void FindSaveFiles()
        {
            var listFileNames = saveFileNames.ToList();
            
            for (var i = 0; i < saveSlots.Count; i++)
            {
                var fileName = await FindSaveFile(listFileNames[i]);
                
                saveSlots[i].SetSaveSlotName(string.IsNullOrEmpty(fileName) ? defaultEmptySaveSlotText : fileName);
                saveSlots[i].SetSaveFileName(listFileNames[i]);
            }
        }
        
        private async Task<string> FindSaveFile(string saveSlotName)
        {
            var fileName =  await saveMonoSystem.FindSaveFile(saveSlotName);
            return fileName;
        }
        
    }
}