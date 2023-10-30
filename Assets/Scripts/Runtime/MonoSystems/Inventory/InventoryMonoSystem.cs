using Akashic.Core;
using Akashic.Runtime.Converters;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.Debugger;
using Akashic.Runtime.MonoSystems.Party;
using Akashic.Runtime.MonoSystems.Scene;
using Akashic.Runtime.Serializers;
using System.Collections.Generic;
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
			currentInventory = PartyInventoryConverter.ConvertItemDataListToPartyInventory(configMonoSystem.GetDefaultInventory());
		}

		public PartyInventory GetInventory()
		{
			return currentInventory;
		}
	}
}