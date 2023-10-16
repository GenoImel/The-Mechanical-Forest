using UnityEngine;

namespace Akashic.Assets.ScriptableObjects.ItemBase
{
	[CreateAssetMenu(menuName = "Akashic/Items/New Base Relic")]
	internal sealed class RelicBaseData : ScriptableObject
	{
		public string itemName;
		public string description;
		public Sprite icon;
	}
}
