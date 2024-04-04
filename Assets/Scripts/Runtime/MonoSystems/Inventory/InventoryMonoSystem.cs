using Akashic.Core;
using Akashic.Runtime.Converters;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.GameDebug;
using Akashic.Runtime.ScriptableObjects.Inventory;
using Akashic.Runtime.Serializers.Save;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Inventory
{
	internal sealed class InventoryMonoSystem : MonoBehaviour, IInventoryMonoSystem
	{
		private PartyInventory currentInventory;

		private IConfigMonoSystem configMonoSystem;
		private IDebugMonoSystem debugMonoSystem;

		private void Awake()
		{
			configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
			debugMonoSystem = GameManager.GetMonoSystem<IDebugMonoSystem>();
		}

		public void CreateNewInventory()
		{
			var inventoryData = debugMonoSystem.IsDebugMode ? 
				debugMonoSystem.GetDebugInventory() : configMonoSystem.GetDefaultInventory();
			
			InitializeInventory(inventoryData);
		}

		private void InitializeInventory(InventoryData inventoryData)
		{
			var inventoryItems = ItemConverter.ConvertInventoryDataToPartyInventory(inventoryData);
			currentInventory = new PartyInventory(inventoryItems);
		}

		public PartyInventory GetInventory()
		{
			return currentInventory;
		}
	}
}