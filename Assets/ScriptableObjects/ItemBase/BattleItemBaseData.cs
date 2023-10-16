using UnityEngine;

namespace Akashic.Assets.ScriptableObjects.ItemBase
{
	[CreateAssetMenu(menuName = "Akashic/Items/New Base Battle Item")]
	internal sealed class BattleItemBaseData : ScriptableObject
	{
		public string itemName;
		public string description;
		public Sprite icon;
	}
}
