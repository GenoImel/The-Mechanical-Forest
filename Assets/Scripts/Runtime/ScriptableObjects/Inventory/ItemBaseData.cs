using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Inventory
{
    /// <summary>
	/// Abstract class containg the base item data
    /// </summary>
    public abstract class ItemBaseData : ScriptableObject
	{
		/// <summary>
		/// Item name displayed to the player
		/// </summary>
		public string itemName;
		/// <summary>
		/// Internal identifier of the item
		/// </summary>
		public string itemId;
		/// <summary>
		/// Description of the item
		/// </summary>
		public string description;
		/// <summary>
		/// Icon of the item
		/// </summary>
		public Sprite icon;
	}
}