using Akashic.Core;
using Akashic.Runtime.Converters;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.Debugger;
using Akashic.Runtime.Serializers;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Inventory
{
	internal sealed class InventoryMonoSystem : MonoBehaviour, IInventoryMonoSystem
	{
		private PartyInventory currentInventory;

		private IConfigMonoSystem configMonoSystem;
		private IDebuggerMonoSystem debuggerMonoSystem;

		private void Awake()
		{
			configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
			debuggerMonoSystem = GameManager.GetMonoSystem<IDebuggerMonoSystem>();
		}

		public void CreateNewInventory()
		{
			var inventoryData = configMonoSystem.GetDefaultInventory();
			var inventoryItems = ItemConverter.ConvertInventoryDataToPartyInventory(inventoryData);
			currentInventory = new PartyInventory(inventoryItems);
		}

		public PartyInventory GetInventory()
		{
			return currentInventory;
		}
	}
}