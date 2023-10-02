using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.Scripts.ConfigBase
{
    [CreateAssetMenu(menuName = "Akashic/Config/New Game Config File")]
    public sealed class ConfigBaseData : ScriptableObject
    {
        [Header("Save File Settings")]
        public string saveFolderParentName;
        
        public List<string> saveSlotFolderNames = new List<string>();
        
        public List<string> saveSlotFileNames = new List<string>();
        
        public int saveSlotNameCharacterLimit = 16;
    }
}