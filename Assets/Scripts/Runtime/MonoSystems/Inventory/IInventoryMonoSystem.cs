using Akashic.Core.MonoSystems;
using Akashic.Runtime.Serializers.Save;

namespace Akashic.Runtime.MonoSystems.Inventory
{
    internal interface IInventoryMonoSystem : IMonoSystem
	{
		/// <summary>
		/// Creates a new inventory using default values
		/// specified by the scriptable objects.
		/// </summary>
		public void CreateNewInventory();

		/// <summary>
		/// Returns the current <see cref="PartyInventory"/>.
		/// </summary>
		public PartyInventory GetInventory();
	}
}