using Akashic.ScriptableObjects.Database;
using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;
using System.Linq;
using Akashic.ScriptableObjects.Battle;
using Akashic.ScriptableObjects.Exploration;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Resource
{
	internal sealed class ResourceMonoSystem : MonoBehaviour, IResourceMonoSystem
	{
		[Header("Items")]
		[SerializeField] private ConsumablesDatabase consumablesDatabase;
		[SerializeField] private NonConsumablesDatabase nonConsumablesDatabase;
		
		[Header("Equipment")]
		[SerializeField] private WeaponsDatabase weaponsDatabase;
		[SerializeField] private ArmorDatabase armorDatabase;
		[SerializeField] private RelicsDatabase relicsDatabase;
		[SerializeField] private AccessoriesDatabase accessoriesDatabase;
		
		[Header("Exploration")]
		[SerializeField] private ExplorationEnvironmentDatabase explorationEnvironmentDatabase;
		
		[Header("Battle")]
		[SerializeField] private EnemyDatabase enemiesDatabase;
		[SerializeField] private EncounterDatabase encountersDatabase;

		public AccessoryData GetAccessoryById(string itemId)
		{
			return accessoriesDatabase.accessories.First(accessory => accessory.itemId == itemId);
		}

		public List<AccessoryData> GetAccessoriesByIds(List<string> itemIds)
		{
			return accessoriesDatabase.accessories.Where(accessory => itemIds.Any(it => it == accessory.itemId)).ToList();
		}

		public ConsumableData GetConsumableById(string itemId)
		{
			return consumablesDatabase.consumables.First(consumable => consumable.itemId == itemId);
		}

		public List<ConsumableData> GetConsumablesByIds(List<string> itemIds)
		{
			return consumablesDatabase.consumables.Where(consumable => itemIds.Any(it => it == consumable.itemId)).ToList();
		}
		
		public NonConsumableData GetNonConsumableById(string itemId)
		{
			return nonConsumablesDatabase.nonConsumables.First(nonConsumable => nonConsumable.itemId == itemId);
		}
		
		public List<NonConsumableData> GetNonConsumablesByIds(List<string> itemIds)
		{
			return nonConsumablesDatabase.nonConsumables.Where(nonConsumable => itemIds.Any(it => it == nonConsumable.itemId)).ToList();
		}
		
		public WeaponData GetWeaponById(string itemId)
		{
			return weaponsDatabase.weapons.First(weapon => weapon.itemId == itemId);
		}
		
		public List<WeaponData> GetWeaponsByIds(List<string> itemIds)
		{
			return weaponsDatabase.weapons.Where(weapon => itemIds.Any(it => it == weapon.itemId)).ToList();
		}
		
		public ArmorData GetArmorById(string itemId)
		{
			return armorDatabase.armors.First(armor => armor.itemId == itemId);
		}
		
		public List<ArmorData> GetArmorByIds(List<string> itemIds)
		{
			return armorDatabase.armors.Where(armor => itemIds.Any(it => it == armor.itemId)).ToList();
		}
		
		public RelicData GetRelicById(string itemId)
		{
			return relicsDatabase.relics.First(relic => relic.itemId == itemId);
		}
		
		public List<RelicData> GetRelicsByIds(List<string> itemIds)
		{
			return relicsDatabase.relics.Where(relic => itemIds.Any(it => it == relic.itemId)).ToList();
		}
		
		public ExplorationEnvironmentData GetExplorationEnvironmentById(string roomId)
		{
			return explorationEnvironmentDatabase.environments.First(environment => environment.roomId == roomId);
		}
		
		public EnemyData GetEnemyById(string enemyId)
		{
			return enemiesDatabase.enemies.First(enemy => enemy.enemyId == enemyId);
		}
		
		public List<EnemyData> GetEnemiesByIds(List<string> enemyIds)
		{
			return enemiesDatabase.enemies.Where(enemy => enemyIds.Any(it => it == enemy.enemyId)).ToList();
		}
		
		public EncounterData GetEncounterById(string encounterId)
		{
			return encountersDatabase.encounters.First(encounter => encounter.encounterId == encounterId);
		}
	}
}