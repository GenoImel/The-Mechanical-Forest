using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Save;
using UnityEngine;

namespace Akashic.Runtime.Controllers.SaveMenu
{
    internal sealed class SaveFileContainer : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SaveSlot saveSlot;

        [SerializeField] private List<SaveSlot> saveSlots = new List<SaveSlot>();

        private void Start()
        {
            
        }
        
    }
}