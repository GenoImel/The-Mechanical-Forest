using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Resource;
using Akashic.Runtime.Serializers.Save;
using Akashic.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class PartyBattleEquipmentHandler : MonoBehaviour
    {
        private ArmorData armorData;
        private WeaponData weaponData;
        private List<AccessoryData> accessoryData;
        private RelicData relicData;

        public ArmorData ArmorData => armorData;
        public WeaponData WeaponData => weaponData;
        public List<AccessoryData> AccessoryData => accessoryData;
        public RelicData RelicData => relicData;

        private IResourceMonoSystem resourceMonoSystem;

        private void Awake()
        {
            resourceMonoSystem = GameManager.GetMonoSystem<IResourceMonoSystem>();
        }

        public void InitializeEquipmentReferences(PartyMember partyMember)
        {
            if (resourceMonoSystem == null)
            {
                resourceMonoSystem = GameManager.GetMonoSystem<IResourceMonoSystem>();
            }
            
            if (!string.IsNullOrEmpty(partyMember.PartyMemberEquipment.ArmorSlot.ItemId))
            {
                armorData = resourceMonoSystem.GetArmorById(partyMember.PartyMemberEquipment.ArmorSlot.ItemId);
            }

            if (!string.IsNullOrEmpty(partyMember.PartyMemberEquipment.WeaponSlot.ItemId))
            {
                weaponData = resourceMonoSystem.GetWeaponById(partyMember.PartyMemberEquipment.WeaponSlot.ItemId);
            }

            if (partyMember.PartyMemberEquipment.AccessorySlots.Count > 0)
            {
                accessoryData = resourceMonoSystem
                    .GetAccessoriesByIds(partyMember.PartyMemberEquipment.AccessorySlots
                        .ConvertAll(x => x.ItemId));
            }

            if (!string.IsNullOrEmpty(partyMember.PartyMemberEquipment.RelicSlot.ItemId))
            {
                relicData = resourceMonoSystem.GetRelicById(partyMember.PartyMemberEquipment.RelicSlot.ItemId);
            }
        }
    }
}