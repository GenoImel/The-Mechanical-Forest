using Akashic.Runtime.Serializers;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Inventory
{
	internal sealed class InventoryMonoSystem : MonoBehaviour, IInventoryMonoSystem
	{
		private PartyInventory currentInventory;

		public void CreateNewInventory()
		{
			currentInventory = new PartyInventory(new List<InventoryItem>());
		}

		public PartyInventory GetInventory()
		{
			return currentInventory;
		}
	}
}