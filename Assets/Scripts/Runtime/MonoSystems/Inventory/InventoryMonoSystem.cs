using Akashic.Runtime.Controllers.PartyMemberBattle;
using Akashic.Runtime.Serializers;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Inventory
{
	internal sealed class InventoryMonoSystem : MonoBehaviour, IInventoryMonoSystem
	{
		private PartyInventory inventory;

		public void CreateNewInventory()
		{
			inventory = new PartyInventory(new List<InventoryItem>());
		}

		public PartyInventory GetInventory()
		{
			return inventory;
		}
	}
}