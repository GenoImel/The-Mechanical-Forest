using UnityEngine;

namespace Akashic.Assets.ScriptableObjects.ItemBase
{
	[CreateAssetMenu(menuName = "Akashic/Items/New Base Battle Item")]
	internal sealed class BattleItemBaseData : ScriptableObject
	{
		public string itemId;
		public string itemDisplayName;
		public string description;
		public Sprite icon;
	}
}
