using UnityEngine;

namespace Akashic.Assets.ScriptableObjects.ItemBase
{
	[CreateAssetMenu(menuName = "Akashic/Items/New Base Item")]
	internal sealed class ItemBaseData : ScriptableObject
	{
		public string itemName;
		public string description;
		public Sprite icon;
	}
}
