using UnityEngine;

namespace Akashic.Assets.ScriptableObjects.ItemBase
{
	[CreateAssetMenu(menuName = "Akashic/Items/New Base Accessory")]
	internal sealed class AccessoryBaseData : ScriptableObject
	{
		public string itemId;
		public string itemDisplayName;
		public string description;
		public Sprite icon;
	}
}
