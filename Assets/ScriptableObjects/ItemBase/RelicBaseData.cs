using UnityEngine;

namespace Akashic.Assets.ScriptableObjects.ItemBase
{
	[CreateAssetMenu(menuName = "Akashic/Items/New Base Relic")]
	internal sealed class RelicBaseData : ScriptableObject
	{
		public string itemId;
		public string itemDisplayName;
		public string description;
		public Sprite icon;
	}
}
