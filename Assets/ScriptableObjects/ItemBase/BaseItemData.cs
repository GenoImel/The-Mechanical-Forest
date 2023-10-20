using UnityEngine;

namespace Akashic.ScriptableObjects.ItemBase
{
    /// <summary>
	/// Abstract class containg the base item data
    /// </summary>
    public abstract class BaseItemData : ScriptableObject
	{
		/// <summary>
		/// Item name displayed to the player
		/// </summary>
		public string itemName = string.Empty;
		/// <summary>
		/// Internal identifier of the item
		/// </summary>
		public string itemId = string.Empty;
		/// <summary>
		/// Description of the item
		/// </summary>
		public string description = string.Empty;
		/// <summary>
		/// Icon of the item
		/// </summary>
		public Sprite icon;
	}
}