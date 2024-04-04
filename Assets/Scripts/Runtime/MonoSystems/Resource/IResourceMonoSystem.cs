using System.Collections.Generic;
using Akashic.Core.MonoSystems;
using Akashic.Runtime.ScriptableObjects.Battle;
using Akashic.Runtime.ScriptableObjects.Exploration;
using Akashic.Runtime.ScriptableObjects.Inventory;

namespace Akashic.Runtime.MonoSystems.Resource
{
	/// <summary>
	/// Retrieves items, enemies, encounters, and more from the game's resources.
	/// </summary>
	internal interface IResourceMonoSystem : IMonoSystem
	{
		/// <summary>
		/// Get a consumable item by the item id.
		/// </summary>
		/// <returns><see cref="ConsumableData"/></returns>
		public ConsumableData GetConsumableById(string itemId);
		
		/// <summary>
		/// Get a list of consumable items by their item ids.
		/// </summary>
		/// <returns><see cref="ConsumableData"/></returns>
		public List<ConsumableData> GetConsumablesByIds(List<string> itemId);

		/// <summary>
		/// Get a non-consumable item by the item id.
		/// </summary>
		/// <returns><see cref="NonConsumableData"/></returns>
		public NonConsumableData GetNonConsumableById(string itemId);

		/// <summary>
		/// Get a list of non-consumable items by their item ids.
		/// </summary>
		/// <returns><see cref="NonConsumableData"/></returns>
		public List<NonConsumableData> GetNonConsumablesByIds(List<string> itemIds);
		
		/// <summary>
		/// Get a weapon item by the item id.
		/// </summary>
		/// <returns><see cref="WeaponData"/></returns>
		public WeaponData GetWeaponById(string itemId);
		
		/// <summary>
		///	Get a list of weapon items by their item ids.
		/// </summary>
		/// <returns><see cref="WeaponData"/></returns>
		public List<WeaponData> GetWeaponsByIds(List<string> itemIds);
		
		/// <summary>
		/// Get an armor item by the item id.
		/// </summary>
		/// <returns><see cref="ArmorData"/></returns>
		public ArmorData GetArmorById(string itemId);
		
		/// <summary>
		/// Get a list of armor items by their item ids.
		/// </summary>
		/// <returns><see cref="ArmorData"/></returns>
		public List<ArmorData> GetArmorByIds(List<string> itemIds);
		
		/// <summary>
		/// Get a relic item by the item id.
		/// </summary>
		/// <returns><see cref="RelicData"/></returns>
		public RelicData GetRelicById(string itemId);
		
		/// <summary>
		/// Get a list of relic items by their item ids.
		/// </summary>
		/// <returns><see cref="RelicData"/></returns>
		public List<RelicData> GetRelicsByIds(List<string> itemIds);

		/// <summary>
		/// Get an accessory item by the item id.
		/// </summary>
		/// <returns><see cref="AccessoryData"/></returns>
		public AccessoryData GetAccessoryById(string itemId);
		
		/// <summary>
		/// Get a list of accessory items by their item ids.
		/// </summary>
		/// <returns><see cref="AccessoryData"/></returns>
		public List<AccessoryData> GetAccessoriesByIds(List<string> itemId);
		
		/// <summary>
		/// Get an exploration environment by the room id.
		/// </summary>
		/// <returns><see cref="ExplorationEnvironmentData"/></returns>
		public ExplorationEnvironmentData GetExplorationEnvironmentById(string roomId);
		
		/// <summary>
		/// Get an enemy by the enemy id.
		/// </summary>
		/// <returns><see cref="EnemyData"/></returns>
		public EnemyData GetEnemyById(string enemyId);
		
		/// <summary>
		/// Get a list of enemies by their enemy ids.
		/// </summary>
		/// <returns><see cref="EnemyData"/></returns>
		public List<EnemyData> GetEnemiesByIds(List<string> enemyId);
		
		/// <summary>
		/// Get an encounter by the encounter id.
		/// </summary>
		/// <returns><see cref="EncounterData"/></returns>
		public EncounterData GetEncounterById(string encounterId);

		/// <summary>
		/// Get a skill using the skill id.
		/// </summary>
		/// <param name="skillId"></param>
		/// <returns></returns>
		public SkillData GetSkillById(string skillId);

		/// <summary>
		/// Get a list of skills by their skill ids.
		/// </summary>
		/// <param name="skillIds"></param>
		/// <returns></returns>
		public List<SkillData> GetSkillsById(List<string> skillIds);
	}
}