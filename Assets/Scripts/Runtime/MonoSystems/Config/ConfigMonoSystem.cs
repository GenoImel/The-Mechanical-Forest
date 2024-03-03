using System.Collections.Generic;
using Akashic.Runtime.MonoSystems.Config.Settings;
using Akashic.ScriptableObjects.Config;
using Akashic.ScriptableObjects.Exploration;
using Akashic.ScriptableObjects.Inventory;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Config
{
    internal sealed class ConfigMonoSystem : MonoBehaviour, IConfigMonoSystem
	{
		[Header("File/folder settings")]
		[SerializeField] private SaveConfigData saveConfigData;
		
		[Header("Starting party settings")]
		[SerializeField] private InventoryData inventoryData;
		[SerializeField] private PartyData partyData;
		
		[Header("Starting exploration scene")]
		[SerializeField] private ExplorationEnvironmentData explorationEnvironmentData;

		[Header("Game settings")]
		[SerializeField] private GameConfigData gameConfigData;

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

		public InventoryData GetDefaultInventory()
		{
			return inventoryData;
		}
		
		public string GetRoomId()
		{
			return explorationEnvironmentData.roomId;
		}
		
		public string GetSpawnPointId()
		{
			return "default";
		}
		
		public GameConfigData GetBattleConfigData()
		{
			return gameConfigData;
		}

		private void InitializeConfigSettings()
		{
			saveConfigSettings = new SaveConfigSettings(saveConfigData);
		}
	}
}