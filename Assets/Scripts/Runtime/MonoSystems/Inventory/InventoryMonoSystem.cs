using Akashic.Runtime.Controllers.PartyMemberBattle;
using Akashic.Runtime.Serializers;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Inventory
{
	internal sealed class InventoryMonoSystem : MonoBehaviour, IInventoryMonoSystem
	{
		public void CreateNewInventory()
		{
			
		}

		public PartyInventory GetInventory()
		{
			throw new System.NotImplementedException();
		}
	}
}