using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Runtime.Controllers.SaveMenu
{
    internal sealed class SaveFileContainer : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SaveSlot saveSlot;

        [SerializeField] private int numSlots;

        private List<SaveSlot> saveSlots = new List<SaveSlot>();

        private void Start()
        {
            CreateSaveSlots();
        }

        private void CreateSaveSlots()
        {
            for (int i = 0; i < numSlots; i++)
            {
                var slot = Instantiate(saveSlot, transform);
                saveSlots.Add(slot);
            }
        }
    }
}