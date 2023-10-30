using System.Collections.Generic;
using Akashic.Runtime.MonoSystems.Config.Settings;
using Akashic.ScriptableObjects.Config;
using Akashic.ScriptableObjects.Inventory;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Config
{
    internal sealed class ConfigMonoSystem : MonoBehaviour, IConfigMonoSystem
	{
		[SerializeField] private ConfigData configData;
		[SerializeField] private InventoryData inventoryData;
		[SerializeField] private PartyData partyData;

		private SaveConfigSettings saveConfigSettings;

        void Awake()
        {
            InitializeConfigSettings();
        }

		public SaveConfigSettings GetSaveConfigSettings()
		{
			return saveConfigSettings;
		}

		public List<PartyMemberData> GetDefaultParty()
		{
			return partyData.partyMembers;
		}

		public List<ItemBaseData> GetDefaultInventory()
		{
			return inventoryData.items;
		}

		private void InitializeConfigSettings()
		{
			saveConfigSettings = new SaveConfigSettings(configData);
		}
	}
}